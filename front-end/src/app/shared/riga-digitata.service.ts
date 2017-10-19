import { BaseService } from './base.service';
import { RigaDigitata, TrattamentoEnum } from './models';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class RigaDigitataService extends BaseService<RigaDigitata>{

    private storage: RigaDigitata[] = [
        {
            id: 1,
            documentoId: 1,
            contoDareId: 1,
            contoAvereId: 2,
            voceIvaId: 1,
            trattamento: TrattamentoEnum.None,
            titoloInapplicabilitaId: null,
            aliquotaIvaId: 22,
            imponibile: 1000,
            iva: 220,
            percentualeIndeducibilita: 0,
            percentualeIndetraibilita: 0,
            settore: null,
            note: null
        },
        {
            id: 2,
            documentoId: 1,
            contoDareId: 2,
            contoAvereId: 1,
            voceIvaId: 1,
            trattamento: TrattamentoEnum.Detraibile,
            titoloInapplicabilitaId: null,
            aliquotaIvaId: 22,
            imponibile: 500,
            iva: 110,
            percentualeIndeducibilita: 0,
            percentualeIndetraibilita: 0,
            settore: null,
            note: null
        }
    ];

    public getAll(): Observable<RigaDigitata[]> {
        return Observable.of(this.storage);
    }

    public getByDocumentoId(id: number): Observable<RigaDigitata[]> {
        return Observable.of(this.storage.filter(d => d.documentoId === id));
    }

}
