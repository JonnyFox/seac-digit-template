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
    RegistroTipoEnum,
    RigaDigitata,
    TitoloInapplicabilita,
    TrattamentoEnum,
    VoceIva,
} from '../shared/models';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';

@Component({
    selector: 'app-document',
    templateUrl: './document.component.html',
    styleUrls: ['./document.component.scss']
})
export class DocumentComponent {

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

    public addRigaDigitata(): void {
        this.editItem.rigaDigitataList.push(new RigaDigitata());
    }
}
