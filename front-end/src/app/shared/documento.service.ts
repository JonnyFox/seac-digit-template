import { HttpClient } from '@angular/common/http';
import { BaseService } from './base.service';
import {
    Documento,
    DocumentoCaratteristicaEnum,
    DocumentoSospensioneEnum,
    DocumentoTipoEnum,
    RegistroTipoEnum,
    EffettoCalcolo,
    EffettoConto
} from './models';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Http, HttpModule, Response } from '@angular/http';
import 'rxjs/add/operator/map';

@Injectable()
export class DocumentoService/* extends BaseService<Documento>*/ {

    constructor(private http: Http) {

    }

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

    private effettoContos: EffettoConto[] = [
        {
            id: 1,
            documentoId: 1,
            rigaDigitataId: 2,
            contoDareId: 1,
            contoAvereId: 2,
            imponibile: 3,
            valore: 1,
            variazione: 3,
            riferimentoEffettoId: 1
        },
        {
            id: 2,
            documentoId: 1,
            rigaDigitataId: 2,
            contoDareId: 1,
            contoAvereId: 2,
            imponibile: 3,
            valore: 1,
            variazione: 3,
            riferimentoEffettoId: 1
        },
    ];

    public getAll(): Observable<Documento[]> {
        return Observable.of(this.storage);
    }

    public getById(id: number): Observable<Documento> {
        return Observable.of(this.storage.filter(d => d.id === id)[0]);
    }

    public getEffettos(): Observable<EffettoConto[]> {
        // return this.http.get('http://localhost:6969/api/effetto/calculate/2')
        //     .map(res => res.json());
        return Observable.of(this.effettoContos);
    }
}
