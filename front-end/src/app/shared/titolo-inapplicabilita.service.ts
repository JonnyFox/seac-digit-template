import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BaseService } from './base.service';
import { TitoloInapplicabilita } from './models';

@Injectable()
export class TitoloInapplicabilitaService extends BaseService<TitoloInapplicabilita> {
    constructor(protected httpClient: HttpClient) {
        super(httpClient);
        this.baseUrl += 'titoloInapplicabilita';
    }
}
