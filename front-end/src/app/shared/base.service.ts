import { Injectable, ApplicationModule } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

import { AliquotaIva, VoceIva } from './models';
import { environment } from '../../environments/environment';
import { AppInjector } from '../app.component';

@Injectable()
export abstract class BaseService<T> {

    protected baseUrl = environment.apiUrl;
    protected httpClient: HttpClient;

    constructor() {
        this.httpClient = AppInjector.get(HttpClient);
     }

    public getAll(): Observable<T[]> {
        return this.httpClient.get<T[]>(this.baseUrl);
    }

    public getById(id: number): Observable<T> {
        return this.httpClient.get<T>(this.baseUrl + `/${id}`);
    }

    public genericGetAll<U>(): Observable<U[]> {
        return this.httpClient.get<U[]>(this.baseUrl);
    }
    
}
