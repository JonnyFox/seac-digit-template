import { EffettoCalcolo } from './models';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class EffettoCalcoloService {

    private storage: EffettoCalcolo[] = [];

    public getAll(): Observable<EffettoCalcolo[]> {
        return Observable.of(this.storage);
    }
}
