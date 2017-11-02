import { BaseService } from './base.service';

import {
    Documento,
    DocumentoCaratteristicaEnum,
    DocumentoSospensioneEnum,
    DocumentoTipoEnum,
    RegistroTipoEnum,
    EffettoCalcolo,
    EffettoConto,
    EffettoIva,
    SituazioneConto,
    SituazioneVoceIva
} from './models';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Http, HttpModule, Response } from '@angular/http';

@Injectable()
export class DocumentoService extends BaseService<Documento> {

    constructor() {
        super();
        this.baseUrl += 'documento';
    }
}


