import { FormArray, FormControl, Validators } from '@angular/forms';
import { Component, OnInit, Inject } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { DataSource } from '@angular/cdk/collections';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Observable } from 'rxjs/Observable';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { Subscription } from 'rxjs/Subscription';
import { ToastrService } from 'ngx-toastr';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';

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
    Feedback,
    EffettoFeedback
} from '../shared/models';
import { DocumentoService } from '../shared/documento.service';
import { EffettoService } from '../shared/effetto.service';
import { NotificationService } from '../shared/notification.service';
import { Response } from '@angular/http/src/static_response';
import { Jsonp } from '@angular/http/src/http';
import { DialogOverviewExampleDialogComponent } from '../dialog-overview-example-dialog/dialog-overview-example-dialog.component';
import { Subject } from 'rxjs/Subject';

@Component({
    selector: 'app-document',
    templateUrl: './document.component.html',
    styleUrls: ['./document.component.scss']
})
export class DocumentComponent implements OnInit {

    public effettoFeedback: EffettoFeedback = new EffettoFeedback;
    public description: string;
    public isSync = false;
    private editDocumento: Documento;
    private syncSubscription: Subscription;
    public feedback: Feedback = new Feedback;
    public editItemForm: FormGroup;

    public displayedColumnsEffettoConto = ['rigaDigitataId', 'contoDareId',
        'contoAvereId', 'valore', 'variazioneFiscale'];
    public displayedColumnsEffettoIva = ['rigaDigitataId', 'voceIvaId', 'trattamento',
        'titoloInapplicabilita', 'aliquotaIvaId', 'imponibile', 'iva'];
    public displayedColumnsSituazioneConto = ['contoId', 'valore', 'variazioneFiscale'];
    public displayedColumnsSituazioneVoceIVA = ['voceIvaId', 'trattamento', 'titoloInapplicabilita', 'aliquotaIvaId', 'imponibile', 'iva'];
    public displayedColumnsDocumento = ['id', 'totale', 'ritenutaAcconto',
        'sospeso', 'tipo', 'caratteristica', 'cliforId', 'registro', 'riferimentoDocumentoId'];
    public displayedColumnsRigaDigitata = ['id', 'documentoId', 'contoDareId',
        'contoAvereId', 'voceIvaId', 'trattamento', 'titoloInapplicabilitaId', 'aliquotaIvaId',
        'imponibile', 'iva', 'percentualeIndetraibilita', 'percentualeIndeducibilita', 'settore', 'note'];

    private _effettoCalcolo$: BehaviorSubject<EffettoCalcolo> = new BehaviorSubject(new EffettoCalcolo());
    public effettoCalcolo$: Observable<EffettoCalcolo> = this._effettoCalcolo$.asObservable();

    private _isValidDocument$: BehaviorSubject<boolean> = new BehaviorSubject(true);
    public isValidDocument$: Observable<boolean> = this._isValidDocument$.asObservable();

    private _document$: Subject<Documento> = new Subject();
    private document$: Observable<Documento> = this._document$.asObservable();

    private _isSync$: BehaviorSubject<boolean> = new BehaviorSubject(true);
    public isSync$: Observable<boolean> = this._isSync$.asObservable();

    public dataSourceEffettoConto = new DataSourceEffettoConto(this.effettoCalcolo$);
    public dataSourceEffettoIva = new DataSourceEffettoIva(this.effettoCalcolo$);
    public dataSourceSituazioneConto = new DataSourceSituazioneConto(this.effettoCalcolo$);
    public dataSourceSituazioneVoceIva = new DataSourceSituazioneVoceIva(this.effettoCalcolo$);
    public dataSourceDocumento = new DataSourceDocumento(this.effettoCalcolo$);
    public dataSourceRigaDigitata = new DataSourceRigaDigitata(this._effettoCalcolo$);

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
    public tipo = DocumentoTipoEnum;
    public caratteristica = DocumentoCaratteristicaEnum;
    public sospeso = DocumentoSospensioneEnum;
    public registro = RegistroTipoEnum;

    public rigaDigitataList: FormArray = new FormArray([]);

    public isButtonSync$: Observable<boolean> = this.isSync$
        .combineLatest(this.isValidDocument$)
        .map(([s, v]) => s && v);

    constructor(
        private route: ActivatedRoute,
        private documentService: DocumentoService,
        private rigaDigitataService: RigaDigitataService,
        private effettoService: EffettoService,
        private notificationService: NotificationService,
        private fb: FormBuilder,
        private dialog: MatDialog
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
            .subscribe((rigaDigitataList: RigaDigitata[]) => this.editItem.rigaDigitataList = rigaDigitataList);

        this.document$
            .withLatestFrom(this.isValidDocument$)
            .filter(([_, bool]) => bool)
            .map(([doc, _]) => doc)
            .withLatestFrom(this.isSync$)
            .filter(([_, bool]) => bool)
            .switchMap(([doc, _]) => {
                return this.effettoService.getEffettoList(doc);
            })
            .subscribe(val => this._effettoCalcolo$.next(val), err => this.notificationService.notifyError(err));

        this.aliquotaIvaList = this.route.snapshot.data['aliquotaIvaList'];
        this.contoList = this.route.snapshot.data['contoList'];
        this.titoloInapplicabilitaList = this.route.snapshot.data['titoloInapplicabilitaList'];
        this.voceIvaList = this.route.snapshot.data['voceIvaList'];

        this.createForm();
    }

    ngOnInit() { }

    public inputChange(documento: Documento) {
        this.editDocumento = documento;
        this._document$.next(documento);
    }

    public isDocumentValid(isValid: boolean) {
        this._isValidDocument$.next(isValid);
    }

    private createForm(): void {
        this.editItemForm = this.fb.group({
            numero: [],
            protocollo: [],
            totale: [],
            ritenutaAcconto: [],
            sospeso: [],
            tipo: [],
            caratteristica: [],
            cliforId: [],
            registro: [],
            rigaDigitataList: this.fb.array([])
        });
    }

    private createRigaDigitataFormGroup(rd: RigaDigitata): FormGroup {

        const group = this.fb.group({
            contoDareId: [],
            contoAvereId: [],
            voceIvaId: [],
            trattamento: [],
            titoloInapplicabilitaId: [],
            aliquotaIvaId: [],
            imponibile: [],
            iva: [],
            percentualeIndetraibilita: ['', Validators.required],
            percentualeIndeducibilita: ['', Validators.required],
            settore: [],
            note: []
        });

        if (rd) {
            group.patchValue(rd);
        }

        return group;
    }

    public getContoDescription(id: number): string {
        if (id != null) {
            return this.contoList.find(c => c.id === id).nome;
        }else {
            return null;
        }
    }

    public getVoceIvaDescription(id: number | null): string {
        return this.voceIvaList.find(c => c.id === id).nome;
    }

    public getTitoloInapplicabilitaDescription(id: number): string {
        return id == null ? '' : this.titoloInapplicabilitaList.find(c => c.id === id).nome;
    }

    public getAliquotaDescription(id: number): string {
        return id == null ? '' : this.aliquotaIvaList.find(c => c.id === id).percentuale + '%';
    }

    public toggleSync(): void {
        this._isSync$.next(!this._isSync$.value);
    }

    public openDialog(): void {
        const dialogRef = this.dialog.open(DialogOverviewExampleDialogComponent, {
          width: '500px',
          data: {  description: this.description }
        });

        dialogRef.afterClosed().subscribe(result => {

            this.feedback.Descrizione = result;
            this.effettoFeedback.documento = this.editDocumento;

            this.effettoService.getEffettoList(this.editDocumento)
            .first()
            .subscribe(ef => this.setValue(ef));

            this.description = '';
        });
    }

    private setValue(ef: EffettoCalcolo): void {

        this.effettoFeedback.effettoContos = ef.effettoContos;
        this.effettoFeedback.effettoDocumentoList = ef.effettoDocumentoList;
        this.effettoFeedback.effettoIvas = ef.effettoIvas;
        this.effettoFeedback.effettoRigaList = ef.effettoRigaList;

        this.feedback.Effetto = JSON.stringify(this.effettoFeedback);

        this.effettoService.sendFeedback(this.feedback).subscribe();
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
export class DataSourceDocumento extends DataSource<any> {
    constructor(private effettoCalcolo$: Observable<EffettoCalcolo>) {
        super();
    }
    connect(): Observable<Documento[]> {
        return this.effettoCalcolo$.map(v => v.effettoDocumentoList || []);
    }
    disconnect() { }

}
export class DataSourceRigaDigitata extends DataSource<any> {
    constructor(private effettoCalcolo$: Observable<EffettoCalcolo>) {
        super();
    }
    connect(): Observable<RigaDigitata[]> {
        return this.effettoCalcolo$.map(v => v.effettoRigaList || []);
    }
    disconnect() { }

}



