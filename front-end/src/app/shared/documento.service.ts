import { BaseService } from './base.service';
import {
    Documento,
    DocumentoCaratteristicaEnum,
    DocumentoSospensioneEnum,
    DocumentoTipoEnum,
    RegistroTipoEnum,
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

    public getAll(): Observable<Documento[]> {
        return Observable.of(this.storage);
    }

    public getById(id: number): Observable<Documento> {
        return Observable.of(this.storage.filter(d => d.id === id)[0]);
    }
}
