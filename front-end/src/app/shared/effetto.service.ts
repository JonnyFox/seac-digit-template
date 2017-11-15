import { Response } from '@angular/http';
import { EffettoCalcolo, EffettoConto, RigaDigitata, Documento } from './models';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Injectable()
export class EffettoService {

    protected baseUrl = environment.apiUrl;

    constructor(private http: HttpClient) {
        this.baseUrl += 'effetto';
    }

    public getEffettoList(documento: Documento): Observable<EffettoCalcolo> {
        return <any>this.http.post(this.baseUrl + `/calculatePost`, documento, {
            observe: 'body',
            headers: new HttpHeaders().set('Content-Type', 'application/json'),
            responseType: 'json',
            withCredentials: false
        });
    }
}
