import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { BaseService } from './base.service';
import { AliquotaIva, VoceIva } from './models';

@Injectable()
export class AliquotaIvaService extends BaseService<AliquotaIva> {
    constructor(protected httpClient: HttpClient) {
        super(httpClient);
        this.baseUrl += 'AliquotaIva';
    }
}
