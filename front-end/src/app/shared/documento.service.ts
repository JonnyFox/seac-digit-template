import { BaseService } from './base.service';

import { Response } from '@angular/http';
import { EffettoCalcolo, EffettoConto, RigaDigitata, Documento, Feedback } from './models';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { query } from '@angular/animations/src/animation_metadata';
import { stringify } from 'querystring';


@Injectable()
export class DocumentoService extends BaseService<Documento> {

    protected baseUrl = environment.apiUrl;
    constructor(private http: HttpClient) {
        super();
        this.baseUrl += 'documento';
    }
    public match(effettoDocumentoList: Documento[], effettoRigaList: RigaDigitata[]): Documento[] {
        for (const doc of effettoDocumentoList){
            doc.rigaDigitataList = new Array<RigaDigitata>();
            for (const rig of effettoRigaList){
                if (doc.id === rig.documentoId) {
                doc.rigaDigitataList.push(rig);
                }
            }
        }
    return effettoDocumentoList;
    }
    public SaveDocument(documento: Documento): Observable<any> {
        return <any>this.http.post(this.baseUrl + `/saveChanges`, documento, {
            observe: 'body',
            headers: new HttpHeaders().set('Content-Type', 'application/json'),
            responseType: 'text',
            withCredentials: false
        });
    }
}


