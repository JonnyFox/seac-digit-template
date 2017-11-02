import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

import { AliquotaIva, VoceIva } from './models';
import { environment } from '../../environments/environment';

@Injectable()
export abstract class BaseService<T> {

    protected baseUrl = environment.apiUrl;

    constructor(protected httpClient: HttpClient) { }

    public getAll(): Observable<T[]> {
        return this.httpClient.get<T[]>(this.baseUrl);
    }

    public genericGetAll<U>(): Observable<U[]> {
        return this.httpClient.get<U[]>(this.baseUrl);
    }
}
