import { environment } from '../../environments/environment';
import { VoceIva } from './models';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { BaseService } from './base.service';

@Injectable()
export class VoceIvaService extends BaseService<VoceIva> {
    constructor() {
        super();
        this.baseUrl += 'voceIva';
    }
}

