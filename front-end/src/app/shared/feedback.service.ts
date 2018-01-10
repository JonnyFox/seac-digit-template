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


@Injectable()
export class FeedbackService extends BaseService<Feedback> {

    constructor(private http: HttpClient) {
        super();
        this.baseUrl += 'feedback';
    }
}
