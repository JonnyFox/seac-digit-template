
import { RigaDigitataService } from '../shared/riga-digitata.service';
import {
    AliquotaIva,
    Conto,
    Documento,
    DocumentoCaratteristicaEnum,
    DocumentoSospensioneEnum,
    DocumentoTipoEnum,
    EffettoCalcolo,
    EffettoConto,
    RegistroTipoEnum,
    RigaDigitata,
    TitoloInapplicabilita,
    TrattamentoEnum,
    VoceIva,
    EffettoIva,
    SituazioneVoceIva,
    SituazioneConto,
} from '../shared/models';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { DataSource } from '@angular/cdk/collections';
import { Observable } from 'rxjs/Observable';
import { DocumentoService } from '../shared/documento.service';
import { EffettoCalcoloService } from '../shared/effetto-calcolo.service';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';

@Component({
    selector: 'app-document',
    templateUrl: './document.component.html',
    styleUrls: ['./document.component.scss']
})
export class DocumentComponent implements OnInit {

    public displayedColumnsEfettoConto = [ 'rigaDigitataId', 'contoDareId',
        'contoAvereId', 'imponibile', 'valore', 'variazione'];
    public displayedColumnsEffettoIva = [ 'rigaDigitataId', 'voceIvaId', 'trattamento',
        'titoloInapplicabilita', 'aliquotaIvaId', 'imponibile', 'iva'];
    public displayedColumnsSituazioneConto = ['contoId', 'valore', 'variazione'];
    public displayedColumnsSituazioneVoceIVA = ['voceIvaId', 'trattamento', 'titoloInapplicabilita', 'aliquotaIvaId', 'valore'];

    private _effettoCalcolo$: BehaviorSubject<EffettoCalcolo> = new BehaviorSubject(new EffettoCalcolo());
    public effettoCalcolo$: Observable<EffettoCalcolo> = this._effettoCalcolo$.asObservable();

    public dataSourceEffettoConto = new DataSourceEffettoConto(this.effettoCalcolo$);
    public dataSourceEffettoIva = new DataSourceEffettoIva(this.effettoCalcolo$);
    public dataSourceSituazioneConto = new DataSourceSituazioneConto(this.effettoCalcolo$);
    public dataSourceSituazioneVoceIva = new DataSourceSituazioneVoceIva(this.effettoCalcolo$);

    public editItem: Documento = new Documento();

    public documentoSospensioneEnumValues = Object.keys(DocumentoSospensioneEnum)
        .filter(key => !isNaN(Number(DocumentoSospensioneEnum[key])));

    public documentoTipoEnumValues = Object.keys(DocumentoTipoEnum)
        .filter(key => !isNaN(Number(DocumentoTipoEnum[key])));

    public documentoCaratteristicaEnumValues = Object.keys(DocumentoCaratteristicaEnum)
        .filter(key => !isNaN(Number(DocumentoCaratteristicaEnum[key])));

    public registroTipoEnumEnumValues = Object.keys(RegistroTipoEnum)
        .filter(key => !isNaN(Number(RegistroTipoEnum[key])));

    public trattamentoEnumValues = Object.keys(TrattamentoEnum)
        .filter(key => !isNaN(Number(TrattamentoEnum[key])));

    public aliquotaIvaList: Array<AliquotaIva> = [];
    public contoList: Array<Conto> = [];
    public titoloInapplicabilitaList: Array<TitoloInapplicabilita> = [];
    public voceIvaList: Array<VoceIva> = [];
    public trattamento = TrattamentoEnum;

    constructor(
        private route: ActivatedRoute,
        private documentService: DocumentoService,
        private rigaDigitataService: RigaDigitataService,
        private effettoCalcoloService: EffettoCalcoloService
    ) {
        this.route.paramMap
            .switchMap((params: ParamMap) =>
                this.documentService.getById(+params.get('id')))
            .first()
            .switchMap((d: Documento) => {
                this.editItem = d;
                return this.rigaDigitataService.getByDocumentoId(d.id);
            })
            .first()
            .subscribe(rigaDigitataList => this.editItem.rigaDigitataList = rigaDigitataList);

        this.aliquotaIvaList = this.route.snapshot.data['aliquotaIvaList'];
        this.contoList = this.route.snapshot.data['contoList'];
        this.titoloInapplicabilitaList = this.route.snapshot.data['titoloInapplicabilitaList'];
        this.voceIvaList = this.route.snapshot.data['voceIvaList'];
    }

    ngOnInit() { }

    public addRigaDigitata(): void {
        this.editItem.rigaDigitataList.push(new RigaDigitata());
    }

    public calculate(): void {
        this.effettoCalcoloService.calculate(3)
            .subscribe(val => this._effettoCalcolo$.next(val));
    }

    public getContoDescription(id: number): string {
        return this.contoList.find(c => c.id === id).nome;
    }

    public getVoceIvaDescription(id: number | null): string {
        return this.voceIvaList.find(c => c.id === id).nome;
    }

    public getTitoloInapplicabilitaDescription(id: number): string {
        return id == null ? '' : this.titoloInapplicabilitaList.find(c => c.id === id).nome;
    }

    public getAliquotaDescription(id: number): string {
        return id == null ? '' : this.aliquotaIvaList.find(c => c.id === id).percentuale * 100 + '%';
    }
}
export class DataSourceEffettoConto extends DataSource<any> {
    constructor(private effettoCalcolo$: Observable<EffettoCalcolo>) {
        super();
    }
    connect(): Observable<EffettoConto[]> {
        return this.effettoCalcolo$.map(v => v.effettoContos || []);
    }
    disconnect() { }
}
export class DataSourceEffettoIva extends DataSource<any> {
    constructor(private effettoCalcolo$: Observable<EffettoCalcolo>) {
        super();
    }
    connect(): Observable<EffettoIva[]> {
        return this.effettoCalcolo$.map(v => v.effettoIvas || []);
    }
    disconnect() { }
}
export class DataSourceSituazioneConto extends DataSource<any> {
    constructor(private effettoCalcolo$: Observable<EffettoCalcolo>) {
        super();
    }
    connect(): Observable<SituazioneConto[]> {
        return this.effettoCalcolo$.map(v => v.situazioneContos || []);
    }
    disconnect() { }
}
export class DataSourceSituazioneVoceIva extends DataSource<any> {
    constructor(private effettoCalcolo$: Observable<EffettoCalcolo>) {
        super();
    }
    connect(): Observable<SituazioneVoceIva[]> {
        return this.effettoCalcolo$.map(v => v.situazioneVoceIvas || []);
    }
    disconnect() { }
}
