import { Injectable } from '@angular/core';
import { ContoService } from '../shared/conto.service';
import { Feedback } from '../shared/models';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot, ActivatedRoute, ParamMap } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { BaseListResolver } from './base-list.resolver';
import { FeedbackService } from './feedback.service';

@Injectable()
export class FeedbackResolver implements Resolve<Feedback> {
    constructor(protected service: FeedbackService) { }

    resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<Feedback> {
        return this.service.getById(+route.paramMap.get('id'));
    }
}
