import { BaseService } from './base.service';
import { Injectable } from '@angular/core';
import { AliquotaIvaService } from './aliquota-iva.service';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs/Observable';

@Injectable()
export abstract class BaseListResolver<T> implements Resolve<T[]> {
    constructor(protected service: BaseService<T>) { }
    resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<T[]> {
        return this.service.getAll();
    }
}
