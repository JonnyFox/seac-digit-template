import { BaseService } from './base.service';
import {
    Documento,
    DocumentoCaratteristicaEnum,
    DocumentoSospensioneEnum,
    DocumentoTipoEnum,
    RegistroTipoEnum,
    EffettoConto,
    EffettoIva,
    SituazioneConto,
    SituazioneVoceIVA
} from './models';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class DocumentoService extends BaseService<Documento> {

    private storage: Documento[] = [
        {
            id: 1,
            numero: '01',
            protocollo: 1,
            caratteristica: DocumentoCaratteristicaEnum.Normale,
            cliforId: 1,
            registro: RegistroTipoEnum.Acquisti,
            riferimentoDocumentoId: null,
            ritenutaAcconto: 0,
            sospeso: DocumentoSospensioneEnum.None,
            tipo: DocumentoTipoEnum.Fattura,
            totale: 1000,
            rigaDigitataList: []
        },
        {
            id: 2,
            numero: '02',
            protocollo: 2,
            caratteristica: DocumentoCaratteristicaEnum.Normale,
            cliforId: 1,
            registro: RegistroTipoEnum.Emesse,
            riferimentoDocumentoId: null,
            ritenutaAcconto: 0,
            sospeso: DocumentoSospensioneEnum.None,
            tipo: DocumentoTipoEnum.Fattura,
            totale: 1000,
            rigaDigitataList: []
        },
        {
            id: 3,
            numero: '03',
            protocollo: 3,
            caratteristica: DocumentoCaratteristicaEnum.Normale,
            cliforId: 1,
            registro: RegistroTipoEnum.Emesse,
            riferimentoDocumentoId: null,
            ritenutaAcconto: 0,
            sospeso: DocumentoSospensioneEnum.None,
            tipo: DocumentoTipoEnum.Fattura,
            totale: 1000,
            rigaDigitataList: []
        },
        {
            id: 4,
            numero: '04',
            protocollo: 4,
            caratteristica: DocumentoCaratteristicaEnum.Normale,
            cliforId: 1,
            registro: RegistroTipoEnum.Emesse,
            riferimentoDocumentoId: null,
            ritenutaAcconto: 0,
            sospeso: DocumentoSospensioneEnum.None,
            tipo: DocumentoTipoEnum.Fattura,
            totale: 1000,
            rigaDigitataList: []
        }
    ];

    private storageEffettoConto: EffettoConto[] = [
        {
            id: 1,
            documentoId: 2,
            rigaDigitataId: 1,
            contoDareId: 1,
            contoAvereId: 1,
            imponibile: 12,
            valore: 134,
            variazione: 9,
            riferimentoEffettoId: 1
        },
        {
            id: 2,
            documentoId: 3,
            rigaDigitataId: 2,
            contoDareId: 12,
            contoAvereId: 2,
            imponibile: 166,
            valore: 143,
            variazione: 10,
            riferimentoEffettoId: 2
        }
    ];
    private storageEffettoIva: EffettoIva[] = [
        {
            id: 1,
            documentoId: 2,
            rigaDigitataId: 3,
            voceIvaId: 4,
            trattamento: 12,
            titoloInapplicabilita: 2,
            aliquotaIvaId: 3,
            imponibile: 4,
            iva: 5,
            riferimentoEffettoId: 2
        }
    ];
    private storageSituazioneConto: SituazioneConto[] = [
        {
            contoId: 1,
            valore: 500,
            variazione: 12
        },
        {
            contoId: 2,
            valore: 300,
            variazione: 24
        }
    ];
    private storageSituazioneVoceIva: SituazioneVoceIVA[] = [
        {
            voceIvaId: 1,
            trattamento: 2,
            titoloInapplicabilita: 3,
            aliquotaIvaId: 4,
            valore: 100
        },
        {
            voceIvaId: 2,
            trattamento: 3,
            titoloInapplicabilita: 3,
            aliquotaIvaId: 4,
            valore: 2000
        }
    ];
    public getAll(): Observable<Documento[]> {
        return Observable.of(this.storage);
    }

    public getById(id: number): Observable<Documento> {
        return Observable.of(this.storage.filter(d => d.id === id)[0]);
    }

    public getAllEffettoConto(): Observable<EffettoConto[]> {
        return Observable.of(this.storageEffettoConto);
    }
    public getAllEffettoIva(): Observable<EffettoIva[]> {
        return Observable.of(this.storageEffettoIva);
    }
    public getAllSituazioneConto(): Observable<SituazioneConto[]> {
        return Observable.of(this.storageSituazioneConto);
    }
    public getAllSituazioneVoceIva(): Observable<SituazioneVoceIVA[]> {
        return Observable.of(this.storageSituazioneVoceIva);
    }
}