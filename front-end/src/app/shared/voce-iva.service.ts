import { environment } from '../../environments/environment';
import { VoceIva } from './models';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { BaseService } from './base.service';

@Injectable()
export class VoceIvaService extends BaseService<VoceIva> {
    constructor(protected httpClient: HttpClient) {
        super(httpClient);
        this.baseUrl += 'VoceIva';
    }
}

