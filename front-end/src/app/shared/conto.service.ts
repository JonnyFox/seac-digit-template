import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { BaseService } from './base.service';
import { Conto } from './models';

@Injectable()
export class ContoService extends BaseService<Conto> {
    constructor(protected httpClient: HttpClient) {
        super(httpClient);
        this.baseUrl += 'Conto';
    }
}
