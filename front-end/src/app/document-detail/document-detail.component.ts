import { Component, OnInit, Input, Output } from '@angular/core';
import {
    DocumentoSospensioneEnum,
    DocumentoTipoEnum,
    DocumentoCaratteristicaEnum,
    RegistroTipoEnum,
    TrattamentoEnum,
    Documento,
    AliquotaIva,
    Conto,
    TitoloInapplicabilita,
    VoceIva,
    RigaDigitata
} from '../shared/models';
import { FormArray, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AfterViewInit } from '@angular/core/src/metadata/lifecycle_hooks';
import { Observable } from 'rxjs/Observable';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { EffettoService } from '../shared/effetto.service';

@Component({
    selector: 'app-document-detail',
    templateUrl: './document-detail.component.html',
    styleUrls: ['./document-detail.component.scss']
})
export class DocumentDetailComponent implements OnInit {

    @Input() public editItem: Documento;

    @Output() public onChange: Observable<Documento>;
    @Output() public isValid: Observable<boolean>;

    @Input() public aliquotaIvaList: Array<AliquotaIva> = [];
    @Input() public contoList: Array<Conto> = [];
    @Input() public titoloInapplicabilitaList: Array<TitoloInapplicabilita> = [];
    @Input() public voceIvaList: Array<VoceIva> = [];

    private _isGenerated$: BehaviorSubject<boolean> = new BehaviorSubject(true);
    public isGenerated$: Observable<boolean> = this._isGenerated$.asObservable();


    public editItemForm: FormGroup;
    public rigaDigitataForm: FormGroup;
    public onChangeRiga: Observable<RigaDigitata[]>;

    public tert: Array<RigaDigitata>;

    public trattamento = TrattamentoEnum;

    public rigaDigitataList: FormArray = new FormArray([]);

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

    constructor(
        private effettoService: EffettoService,
        private fb: FormBuilder
    ) {
        this.createForm();
        this.onChange = this.editItemForm.valueChanges as Observable<Documento>;
        this.isValid = this.editItemForm.statusChanges.map(v => !!v && v !== 'INVALID');
        this.isValid = this.editItemForm.valueChanges.map(v => {
            const isOk = this.editItemForm.valid
                && this.editItemForm.value.rigaDigitataList
                && this.editItemForm.value.rigaDigitataList.length;
            return isOk;
        });
    }

    ngOnInit() {
        this.setFormValues();
        this.editItemForm.get('rigaDigitataList').valueChanges.subscribe(form => console.log(JSON.stringify(form.value.imponibile)));
    }

    private createForm(): void {
        this.editItemForm = this.fb.group({
            id: [],
            numero: [],
            protocollo: [],
            cliforId: [],
            totale: ['', Validators.required],
            ritenutaAcconto: ['', Validators.required],
            sospeso: [],
            tipo: [],
            caratteristica: [],
            registro: [],
            descrizione: [],
            rigaDigitataList: this.fb.array([])
        }, { updateOn: 'blur' });
    }

    private createRigaDigitataFormGroup(rd: RigaDigitata): FormGroup {

        this.rigaDigitataForm = this.fb.group({
            id: [],
            documentoId: [],
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
            note: [],
            toAdd: [],
        }, { updateOn: 'blur' });
        if (rd) {
            this.rigaDigitataForm.patchValue(rd);
        }

        return this.rigaDigitataForm;
    }

    private setFormValues(): void {
        if (this.editItem) {
            this.ceckGenerated(this.editItem);
            if (!this.editItem.id) {
                this.editItem.descrizione = 'Nuovo documento';
            }
            this.editItemForm.patchValue(this.editItem);
            this.rigaDigitataList = this.fb.array(this.editItem.rigaDigitataList.map(rd => this.createRigaDigitataFormGroup(rd)));
            this.editItemForm.setControl('rigaDigitataList', this.rigaDigitataList);
        }
    }

    public getDocumentDescription() {
        if (this.editItem) {
            return this.editItemForm.value.descrizione;
        }
    }

    public ceckGenerated(editItem: Documento): void {
        if (!editItem.isGenerated) {
            this._isGenerated$.next(!this._isGenerated$.value);
        }
    }

    private isFormValid(): boolean {
        return this.editItemForm.status === 'VALID';
    }

    public addRigaDigitata(): void {
        this.rigaDigitataList = this.editItemForm.get('rigaDigitataList') as FormArray;

        const newRigaDigitata = new RigaDigitata();
        newRigaDigitata.percentualeIndeducibilita = newRigaDigitata.percentualeIndetraibilita = 0;
        newRigaDigitata.documentoId = this.editItem.id;
        newRigaDigitata.toAdd = true;
        newRigaDigitata.id = 0;
        newRigaDigitata.imponibile = 0;

        this.rigaDigitataList.push(this.createRigaDigitataFormGroup(newRigaDigitata));
    }

    public deleteRigaDigitata(index: number): void {
        this.rigaDigitataList.removeAt(index);
    }
}
