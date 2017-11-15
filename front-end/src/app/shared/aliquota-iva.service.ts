import { Injectable } from '@angular/core';

import { BaseService } from './base.service';
import { AliquotaIva, VoceIva } from './models';

@Injectable()
export class AliquotaIvaService extends BaseService<AliquotaIva> {
    constructor() {
        super();
        this.baseUrl += 'aliquotaIva';
    }
}
