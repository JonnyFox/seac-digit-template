import { Injectable } from '@angular/core';
import { VoceIva } from '../shared/models';

import { BaseListResolver } from './base-list.resolver';
import { VoceIvaService } from './voce-iva.service';

@Injectable()
export class VoceIvaListResolver extends BaseListResolver<VoceIva> {
    constructor(protected service: VoceIvaService) {
        super(service);
    }
}
