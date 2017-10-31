import { Http, Response } from '@angular/http';
import { EffettoCalcolo, EffettoConto } from './models';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';

@Injectable()
export class EffettoCalcoloService {
    constructor(private http: Http) {

    }
    private storage: EffettoCalcolo[] = [];

    public getAll() {
        return this.http.get('http://localhost:6969/api/effetto/calculate/2')
            .map((res: Response) => res.json());
    }
}
