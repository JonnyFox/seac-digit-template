import { RigaDigitataService } from '../shared/riga-digitata.service';
import { DocumentoService } from '../shared/documento.service';
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
    SituazioneVoceIVA,
    SituazioneConto,
} from '../shared/models';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { DataSource } from '@angular/cdk/collections';
import { Observable } from 'rxjs/Observable';

@Component({
    selector: 'app-document',
    templateUrl: './document.component.html',
    styleUrls: ['./document.component.scss']
})
export class DocumentComponent implements OnInit {

    public displayedColumnsEfettoConto = ['id', 'documentoId', 'rigaDigitataId', 'contoDareId',
        'contoAvereId', 'imponibile', 'valore', 'variazione', 'riferimentoEffettoId'];
    public displaysedColumnsEffettoIva = ['id', 'documentoId', 'rigaDigitataId', 'voceIvaId', 'trattamento',
    'titoloInapplicabilita', 'aliquotaIvaId', 'imponibile', 'iva', 'riferimentoEffettoId'];
    public displaysedColumnsSituazioneConto = ['contoId', 'valore', 'variazione'];
    public displaysedColumnsSituazioneVoceIVA = ['voceIvaId', 'trattamento', 'titoloInapplicabilita', 'aliquotaIvaId', 'valore'];

    public dataSourceEffettoConto: ExampleDataSourceEffettoConto | null;
    public dataSourceEffettoIva: ExampleDataSourceEffettoIva | null;
    public dataSourceSituazioneConto: ExampleDataSourceSituazioneConto | null;
    public dataSourceSituazioneVoceIva: ExampleDataSourceSituazioneVoceIva | null;

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

    constructor(
        private route: ActivatedRoute,
        private documentService: DocumentoService,
        private rigaDigitataService: RigaDigitataService
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


        // this.aliquotaIvaList = this.route.snapshot.data['aliquotaIvaList'];
        this.contoList = this.route.snapshot.data['contoList'];
        // this.titoloInapplicabilitaList = this.route.snapshot.data['titoloInapplicabilitaList'];
        // this.voceIVAList = this.route.snapshot.data['voceIvaList'];
    }
    ngOnInit() {
        this.dataSourceEffettoConto = new ExampleDataSourceEffettoConto(this.documentService);
        this.dataSourceEffettoIva = new ExampleDataSourceEffettoIva(this.documentService);
        this.dataSourceSituazioneConto = new ExampleDataSourceSituazioneConto(this.documentService);
        this.dataSourceSituazioneVoceIva = new ExampleDataSourceSituazioneVoceIva(this.documentService);
    }

    public addRigaDigitata(): void {
        this.editItem.rigaDigitataList.push(new RigaDigitata());
    }
}
export class ExampleDataSourceEffettoConto extends DataSource<any> {
    constructor(private documentService: DocumentoService) {
        super();
    }
    connect(): Observable<EffettoConto[]> {
        return this.documentService.getAllEffettoConto();
    }
    disconnect() { }
}
export class ExampleDataSourceEffettoIva extends DataSource<any> {
    constructor(private documentService: DocumentoService) {
        super();
    }
    connect(): Observable<EffettoIva[]> {
        return this.documentService.getAllEffettoIva();
    }
    disconnect() { }
}
export class ExampleDataSourceSituazioneConto extends DataSource<any> {
    constructor(private documentService: DocumentoService) {
        super();
    }
    connect(): Observable<SituazioneConto[]> {
        return this.documentService.getAllSituazioneConto();
    }
    disconnect() { }
}
export class ExampleDataSourceSituazioneVoceIva extends DataSource<any> {
    constructor(private documentService: DocumentoService) {
        super();
    }
    connect(): Observable<SituazioneVoceIVA[]> {
        return this.documentService.getAllSituazioneVoceIva();
    }
    disconnect() { }
}