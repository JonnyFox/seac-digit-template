import { Injectable } from '@angular/core';

import { BaseService } from './base.service';
import { Conto } from './models';

@Injectable()
export class ContoService extends BaseService<Conto> {
    constructor() {
        super();
        this.baseUrl += 'conto';
    }
}
