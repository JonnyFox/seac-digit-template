import { Http, Response } from '@angular/http';
import { EffettoCalcolo, EffettoConto } from './models';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';

@Injectable()
export class EffettoCalcoloService {

    constructor(private http: Http) { }

    public calculate(id: number): Observable<EffettoCalcolo> {
        return this.http.get('http://localhost:6969/api/effetto/calculate/' + id)
            .map((res: Response) => res.json());
    }
}
