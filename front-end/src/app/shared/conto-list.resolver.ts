import { Injectable } from '@angular/core';
import { ContoService } from '../shared/conto.service';
import { Conto } from '../shared/models';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { BaseListResolver } from './base-list.resolver';

@Injectable()
export class ContoListResolver extends BaseListResolver<Conto> {
    constructor(protected service: ContoService) {
        super(service);
    }
}
