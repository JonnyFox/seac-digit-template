import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { BaseListResolver } from './base-list.resolver';
import { AliquotaIvaService } from './aliquota-iva.service';
import { AliquotaIva } from '../shared/models';

@Injectable()
export class AliquotaIvaListResolver extends BaseListResolver<AliquotaIva> {
    constructor(protected service: AliquotaIvaService) {
        super(service);
    }
}

