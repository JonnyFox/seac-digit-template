import { BaseService } from './base.service';
import { RigaDigitata, TrattamentoEnum } from './models';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class RigaDigitataService extends BaseService<RigaDigitata> {

    constructor() {
        super();
        this.baseUrl += 'rigaDigitata';
    }

    public getByDocumentoId(id: number): Observable<RigaDigitata[]> {
        return this.httpClient.get<RigaDigitata[]>(this.baseUrl + `/documento/${id}`);
    }

}
