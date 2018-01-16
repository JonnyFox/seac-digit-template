import { FormArray, FormControl, Validators } from '@angular/forms';
import { Component, OnInit, Inject, Input } from '@angular/core';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { DataSource } from '@angular/cdk/collections';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Observable } from 'rxjs/Observable';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { Subscription } from 'rxjs/Subscription';
import { ToastrService } from 'ngx-toastr';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

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
import { FeedbackService } from '../shared/feedback.service';
import { Response } from '@angular/http/src/static_response';
import { Jsonp } from '@angular/http/src/http';
import { DialogOverviewExampleDialogComponent } from '../dialog-overview-example-dialog/dialog-overview-example-dialog.component';
import { Subject } from 'rxjs/Subject';
import { AfterViewInit } from '@angular/core/src/metadata/lifecycle_hooks';
import { startTimeRange } from '@angular/core/src/profile/wtf_impl';
import { NgZone } from '@angular/core';

@Component({
    selector: 'app-document',
    templateUrl: './document.component.html',
    styleUrls: ['./document.component.scss']
})
export class DocumentComponent implements AfterViewInit {

    public effettoFeedback: EffettoFeedback = new EffettoFeedback;
    public description: string;
    private editDocumento: Documento;
    private syncSubscription: Subscription;
    public feedback: Feedback = new Feedback;
    public editItemForm: FormGroup;

    public displayedColumnsEffettoConto = ['rigaDigitataId', 'contoDareId',
        'contoAvereId', 'valore', 'variazioneFiscale'];
    public displayedColumnsEffettoIva = ['rigaDigitataId', 'voceIvaId', 'trattamento',
        'titoloInapplicabilita', 'aliquotaIvaId', 'imponibile', 'iva'];
    public displayedColumnsSituazioneConto = ['contoId', 'valore', 'variazioneFiscale', 'sospeso'];
    public displayedColumnsSituazioneVoceIVA = ['voceIvaId', 'trattamento',
        'titoloInapplicabilita', 'aliquotaIvaId', 'imponibile', 'iva', 'sospeso'];
    public displayedColumnsDocumento = ['id', 'totale', 'ritenutaAcconto',
        'sospeso', 'tipo', 'caratteristica', 'cliforId', 'registro', 'riferimentoDocumentoId'];
    public displayedColumnsRigaDigitata = ['id', 'documentoId', 'contoDareId',
        'contoAvereId', 'voceIvaId', 'trattamento', 'titoloInapplicabilitaId', 'aliquotaIvaId',
        'imponibile', 'iva', 'percentualeIndetraibilita', 'percentualeIndeducibilita', 'settore', 'note'];

    private _effettoCalcolo$: BehaviorSubject<EffettoCalcolo> = new BehaviorSubject(new EffettoCalcolo());
    public effettoCalcolo$: Observable<EffettoCalcolo> = this._effettoCalcolo$.asObservable();

    private _isValidDocument$: Subject<boolean> = new Subject();
    public isValidDocument$: Observable<boolean> = this._isValidDocument$.asObservable();
    public isValidDocumentValue = true;

    private _document$: Subject<Documento> = new Subject();
    public document$: Observable<Documento> = this._document$.asObservable();


    private _isSync$: BehaviorSubject<boolean> = new BehaviorSubject(true);
    public isSync$: Observable<boolean> = this._isSync$.asObservable();

    private _documentoEffetti$: BehaviorSubject<Documento[]> = new BehaviorSubject([]);
    public documentoEffetti$: Observable<Documento[]> = this._documentoEffetti$.asObservable();

    public dataSourceEffettoConto = new DataSourceEffettoConto(this.effettoCalcolo$);
    public dataSourceEffettoIva = new DataSourceEffettoIva(this.effettoCalcolo$);
    public dataSourceSituazioneConto = new DataSourceSituazioneConto(this.effettoCalcolo$);
    public dataSourceSituazioneVoceIva = new DataSourceSituazioneVoceIva(this.effettoCalcolo$);
    public dataSourceDocumento = new DataSourceDocumento(this.effettoCalcolo$);
    public dataSourceRigaDigitata = new DataSourceRigaDigitata(this.effettoCalcolo$);

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

    public feedbackEffetto: Feedback;
    public aliquotaIvaList: Array<AliquotaIva> = [];
    public contoList: Array<Conto> = [];
    public titoloInapplicabilitaList: Array<TitoloInapplicabilita> = [];
    public voceIvaList: Array<VoceIva> = [];
    public trattamento = TrattamentoEnum;
    public tipo = DocumentoTipoEnum;
    public caratteristica = DocumentoCaratteristicaEnum;
    public sospeso = DocumentoSospensioneEnum;
    public registro = RegistroTipoEnum;
    public documenti: any[];

    public isButtonSync$: Observable<boolean> = this.isSync$
        .combineLatest(this.isValidDocument$)
        .map(([s, v]) => s && v);


    private isFeedbackMode = false;

    constructor(
        private route: ActivatedRoute,
        private documentoService: DocumentoService,
        private rigaDigitataService: RigaDigitataService,
        private effettoService: EffettoService,
        private notificationService: NotificationService,
        private feedbackService: FeedbackService,
        private fb: FormBuilder,
        private dialog: MatDialog,
        private router: Router,
        private zone: NgZone
    ) {

        const feedback = this.route.snapshot.data['feedback'] as Feedback;
        this.isFeedbackMode = feedback !== null;

        if (!this.isFeedbackMode) {
            this.populateDocument();

            this.document$
                .zip(this.isValidDocument$)
                .filter(([doc, bool]) => !!doc && !!bool)
                .map(([doc, _]) => doc)
                .withLatestFrom(this.isSync$)
                .filter(([_, bool]) => bool)
                .switchMap(([doc, _]) => this.effettoService.getEffettoList(doc))
                .subscribe(val => {
                    this._effettoCalcolo$.next(val);
                    this._documentoEffetti$.next(this.documentoService.match(val.effettoDocumentoList, val.effettoRigaList));
                });

        } else {
            this.populateFeedbackDocument(feedback.effetto);
        }

        this.aliquotaIvaList = this.route.snapshot.data['aliquotaIvaList'];
        this.contoList = this.route.snapshot.data['contoList'];
        this.titoloInapplicabilitaList = this.route.snapshot.data['titoloInapplicabilitaList'];
        this.voceIvaList = this.route.snapshot.data['voceIvaList'];
    }

    ngAfterViewInit() {
        if (!this.isFeedbackMode) {
            this._isValidDocument$.subscribe(v => this.isValidDocumentValue = v);
        }
    }


    public inputChangeEffetto(documento: Documento) {
    }
    public isDocumentValidEffetto(isValid: boolean) {
    }

    public inputChange(documento: Documento) {
        this.editDocumento = documento;
        this._document$.next(documento);
    }

    public isDocumentValid(isValid: boolean | null) {
        this._isValidDocument$.next(!!isValid);
    }

    private populateDocument() {
        this.route.paramMap
            .switchMap((params: ParamMap) => {
                const documentId = +params.get('id');
                if (!documentId) {
                    const newDocument = new Documento;
                    newDocument.id = documentId;
                    newDocument.caratteristica = DocumentoCaratteristicaEnum.Normale;
                    newDocument.cliforId = 1;
                    newDocument.isGenerated = false;
                    newDocument.numero = '0';
                    newDocument.protocollo = 0;
                    newDocument.registro = RegistroTipoEnum.Emesse;
                    newDocument.ritenutaAcconto = 0;
                    newDocument.sospeso = DocumentoSospensioneEnum.None;
                    newDocument.tipo = DocumentoTipoEnum.Fattura;
                    newDocument.totale = 0;
                    return Observable.of(newDocument);
                }
                return this.documentoService.getById(+params.get('id'));
            })
            .first()
            .switchMap((d: Documento) => {
                this.editItem = d;
                if (!d.id) {
                    return Observable.of([]);
                }
                return this.rigaDigitataService.getByDocumentoId(d.id);
            })
            .first()
            .subscribe(evt => this.editItem.rigaDigitataList = evt);
    }

    private populateFeedbackDocument(effettoFeed: string) {
        const parsedData = JSON.parse(effettoFeed);
        this.editItem = this.feedbackService.populateDoc(parsedData);
        this._effettoCalcolo$.next(this.feedbackService.populateEffect(parsedData));
        this._documentoEffetti$.next(this.documentoService.match(parsedData.effettoDocumentoList, parsedData.effettoRigaList));
    }

    public saveDocument() {
        for (let i = 0; i < this.editItem.rigaDigitataList.length; i++) {
            if (!this.editDocumento.rigaDigitataList.some(x => x.id === this.editItem.rigaDigitataList[i].id)) {
                this.editItem.rigaDigitataList[i].toAdd = false;
                this.editDocumento.rigaDigitataList.push(this.editItem.rigaDigitataList[i]);
            }
        }
        this.documentoService.SaveDocument(this.editDocumento).subscribe(x => {
            if (this.editDocumento.id === 0) {
                this.goBackToDashboard();
            }
        });
    }

    public getContoDescription(id: number): string {
        if (id != null) {
            return this.contoList.find(c => c.id === id).nome;
        } else {
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
            width: '1000px',
            data: { description: this.description }
        });

        dialogRef.afterClosed().subscribe(result => {
            if (result != null) {
                this.feedback.idDoc = this.editDocumento.id;
                this.feedback.descrizioneDoc = this.editDocumento.descrizione;
                this.feedback.descrizione = result;
                this.effettoFeedback.documento = this.editDocumento;

                this.effettoService.getEffettoList(this.editDocumento)
                    .first()
                    .subscribe(ef => this.setValue(ef));

                this.description = '';
            }
        });
    }

    private setValue(ef: EffettoCalcolo): void {

        this.effettoFeedback.effettoContos = ef.effettoContos;
        this.effettoFeedback.effettoDocumentoList = ef.effettoDocumentoList;
        this.effettoFeedback.effettoIvas = ef.effettoIvas;
        this.effettoFeedback.effettoRigaList = ef.effettoRigaList;
        this.effettoFeedback.situazioneContos = ef.situazioneContos;
        this.effettoFeedback.situazioneVoceIvas = ef.situazioneVoceIvas;

        this.feedback.effetto = JSON.stringify(this.effettoFeedback);

        this.effettoService.sendFeedback(this.feedback).subscribe();
    }

    public goBackToDashboard() {
        this.router.navigate(['/dashboard']);
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



