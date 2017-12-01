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

    public editItemForm: FormGroup;
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
        private fb: FormBuilder
    ) {
        this.createForm();
        this.onChange = this.editItemForm.valueChanges as Observable<Documento>;
        this.isValid = this.onChange.map(v => this.editItemForm.valid);
    }

    ngOnInit() {
        this.setFormValues();
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
        }, { updateOn: 'blur'});
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
        }, { updateOn: 'blur'});

        if (rd) {
            group.patchValue(rd);
        }

        return group;
    }

    private setFormValues(): void {
        if (this.editItem && this.editItem.id) {
            this.editItemForm.patchValue(this.editItem);
            this.rigaDigitataList = this.fb.array(this.editItem.rigaDigitataList.map(rd => this.createRigaDigitataFormGroup(rd)));
            this.editItemForm.setControl('rigaDigitataList', this.rigaDigitataList);
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
}
