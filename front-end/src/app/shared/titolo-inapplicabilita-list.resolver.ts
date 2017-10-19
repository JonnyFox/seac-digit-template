import { Injectable } from '@angular/core';
import { TitoloInapplicabilita } from '../shared/models';

import { BaseListResolver } from './base-list.resolver';
import { TitoloInapplicabilitaService } from './titolo-inapplicabilita.service';

@Injectable()
export class TitoloInapplicabilitaListResolver extends BaseListResolver<TitoloInapplicabilita> {
    constructor(protected service: TitoloInapplicabilitaService) {
        super(service);
    }
}
