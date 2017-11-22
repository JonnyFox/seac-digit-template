import { FormArray, FormControl, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { DataSource } from '@angular/cdk/collections';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Observable } from 'rxjs/Observable';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { Subscription } from 'rxjs/Subscription';
import { ToastrService } from 'ngx-toastr';

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
    EffettoDocumento,
} from '../shared/models';
import { DocumentoService } from '../shared/documento.service';
import { EffettoService } from '../shared/effetto.service';
import { NotificationService } from '../shared/notification.service';

@Component({
    selector: 'app-document',
    templateUrl: './document.component.html',
    styleUrls: ['./document.component.scss']
})
export class DocumentComponent implements OnInit {

    public isSync = false;
    private syncSubscription: Subscription;

    public editItemForm: FormGroup;

    public displayedColumnsEffettoConto = ['rigaDigitataId', 'contoDareId',
        'contoAvereId', 'valore', 'variazioneFiscale'];
    public displayedColumnsEffettoIva = ['rigaDigitataId', 'voceIvaId', 'trattamento',
        'titoloInapplicabilita', 'aliquotaIvaId', 'imponibile', 'iva'];
    public displayedColumnsSituazioneConto = ['contoId', 'valore', 'variazioneFiscale'];
    public displayedColumnsSituazioneVoceIVA = ['voceIvaId', 'trattamento', 'titoloInapplicabilita', 'aliquotaIvaId', 'imponibile', 'iva'];
    public displayedColumnsEffettoDocumento = ['id', 'totale', 'ritenutaAcconto',
    'sospeso', 'tipo', 'caratteristica', 'cliforId', 'registro', 'riferimentoDocumentoId'];

    private _effettoCalcolo$: BehaviorSubject<EffettoCalcolo> = new BehaviorSubject(new EffettoCalcolo());
    public effettoCalcolo$: Observable<EffettoCalcolo> = this._effettoCalcolo$.asObservable();

    public dataSourceEffettoConto = new DataSourceEffettoConto(this.effettoCalcolo$);
    public dataSourceEffettoIva = new DataSourceEffettoIva(this.effettoCalcolo$);
    public dataSourceSituazioneConto = new DataSourceSituazioneConto(this.effettoCalcolo$);
    public dataSourceSituazioneVoceIva = new DataSourceSituazioneVoceIva(this.effettoCalcolo$);
    public dataSourceEffettoDocumento = new DataSourceEffettoDocumento(this.effettoCalcolo$);

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

    constructor(
        private route: ActivatedRoute,
        private documentService: DocumentoService,
        private rigaDigitataService: RigaDigitataService,
        private effettoService: EffettoService,
        private notificationService: NotificationService,
        private fb: FormBuilder
    ) {
        this.route.paramMap
            .switchMap((params: ParamMap) =>
                this.documentService.getById(+params.get('id')))
            .first()
            .switchMap((d: Documento) => {
                this.editItem = d;
                return this.rigaDigitataService.getByDocumentoId(d.id);
            })
            .switchMap((rigaDigitataList: RigaDigitata[]) => {
                this.editItem.rigaDigitataList = rigaDigitataList;
                this.setFormValues();

                return this.effettoService.getEffettoList(this.editItem);
            })
            .first()
            .subscribe(effettoList => {
                this._effettoCalcolo$.next(effettoList);
            });

        this.aliquotaIvaList = this.route.snapshot.data['aliquotaIvaList'];
        this.contoList = this.route.snapshot.data['contoList'];
        this.titoloInapplicabilitaList = this.route.snapshot.data['titoloInapplicabilitaList'];
        this.voceIvaList = this.route.snapshot.data['voceIvaList'];

        this.createForm();
    }

    ngOnInit() { }

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

    private subscribeFormValueChanges(): void {
        this.syncSubscription = this.editItemForm.valueChanges
            .takeWhile(() => this.isSync)
            .filter(() => this.isFormValid())
            .debounceTime(250)
            .distinctUntilChanged()
            .switchMap(editItem => this.effettoService.getEffettoList(editItem))
            .subscribe(val => this._effettoCalcolo$.next(val), err => this.notificationService.notifyError(err));
    }

    private setFormValues(): void {
        this.editItemForm.patchValue(this.editItem);
        this.rigaDigitataList = this.fb.array(this.editItem.rigaDigitataList.map(rd => this.createRigaDigitataFormGroup(rd)));
        this.editItemForm.setControl('rigaDigitataList', this.rigaDigitataList);
    }

    private isFormValid(): boolean {
        return this.editItemForm.status === 'VALID';
    }

    public addRigaDigitata(): void {
        this.rigaDigitataList = this.editItemForm.get('rigaDigitataList') as FormArray;

        const newRigaDigitata = new RigaDigitata();
        newRigaDigitata.percentualeIndeducibilita = newRigaDigitata.percentualeIndetraibilita = 0;
        newRigaDigitata.documentoId = this.editItem.id;

        const currentRigaDigitata = (this.rigaDigitataList.length > 0 ? this.rigaDigitataList.value[0] : null) as RigaDigitata;
        if (currentRigaDigitata) {
            newRigaDigitata.contoAvereId = currentRigaDigitata.contoAvereId;
            newRigaDigitata.contoDareId = currentRigaDigitata.contoDareId;
            newRigaDigitata.aliquotaIvaId = currentRigaDigitata.aliquotaIvaId;
            newRigaDigitata.titoloInapplicabilitaId = currentRigaDigitata.titoloInapplicabilitaId;
            newRigaDigitata.trattamento = currentRigaDigitata.trattamento;
            newRigaDigitata.voceIvaId = currentRigaDigitata.voceIvaId;
        }

        this.rigaDigitataList.push(this.createRigaDigitataFormGroup(newRigaDigitata));
    }

    public deleteRigaDigitata(index: number): void {
        this.rigaDigitataList.removeAt(index);
    }

    public getEffettos(): void {
        this.effettoService.getEffettoList(this.editItemForm.value)
            .first()
            .subscribe(val => this._effettoCalcolo$.next(val), err => this.notificationService.notifyError(err));
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
        return id == null ? '' : this.aliquotaIvaList.find(c => c.id === id).percentuale + '%';
    }

    public toggleSync(): void {
        this.isSync = !this.isSync;
        if (this.isSync && (!this.syncSubscription || this.syncSubscription.closed)) {
            this.subscribeFormValueChanges();
        }
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
export class DataSourceEffettoDocumento extends DataSource<any> {
    constructor(private effettoCalcolo$: Observable<EffettoCalcolo>) {
        super();
    }
    connect(): Observable<EffettoDocumento[]> {
         return this.effettoCalcolo$.map(v => v.effettoDocumentoList || []);
    }
    disconnect() { }

}
