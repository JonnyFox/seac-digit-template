import { BaseService } from './base.service';
import { Response } from '@angular/http';
import { EffettoCalcolo, EffettoConto, RigaDigitata, Documento, Feedback } from './models';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { query } from '@angular/animations/src/animation_metadata';
import { stringify } from 'querystring';
import { DocumentoService } from '../shared/documento.service';



@Injectable()
export class FeedbackService extends BaseService<Feedback> {

    private effect: string;
    private feedbackDesc: string;

    constructor(private http: HttpClient, private documentoService: DocumentoService) {
        super();
        this.baseUrl += 'feedback';
    }

    public getById(id: number) {
        return this.httpClient.get<Feedback>(this.baseUrl + `/${id}`);
    }

    public populateEffect(parsedData): EffettoCalcolo {
        const x = new EffettoCalcolo();
        x.effettoContos = parsedData.effettoContos;
        x.effettoIvas = parsedData.effettoIvas;
        x.situazioneContos = parsedData.situazioneContos;
        x.situazioneVoceIvas = parsedData.situazioneVoceIvas;
        return x;
    }
}


