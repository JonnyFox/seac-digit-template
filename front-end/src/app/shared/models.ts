// tslint:disable:no-bitwise
export enum DocumentoSospensioneEnum {
    None,
    IVA,
    Conto,
    ContoIVA = IVA | Conto
}

export enum DocumentoTipoEnum {
    Fattura,
    NotaAccredito,
    NotaAddebito,
    BollaDoganale,
    CartaCarburante,
    Contabile
}

export enum DocumentoCaratteristicaEnum {
    Normale,
    Autofattura,
    ReverseCharge,
    FatturaUE,
    Differita
}

export enum RegistroTipoEnum {
    Acquisti,
    Emesse,
    Corrispettivi,
    Finanziari
}

export class Documento {
    id: number;
    numero: string;
    protocollo: number;
    totale: number;
    ritenutaAcconto: number;
    sospeso: DocumentoSospensioneEnum;
    tipo: DocumentoTipoEnum;
    caratteristica: DocumentoCaratteristicaEnum;
    cliforId: number;
    registro: RegistroTipoEnum;
    riferimentoDocumentoId: number | null;
    rigaDigitataList: RigaDigitata[];
    templateGenerazioneEffettoDocumentoId: number | null;
}

export enum CliforSoggettoEnum {
    Nazionale,
    UE,
    ExtraUE
}

export enum CliforIstituzionaleEnum {
    Istituzionale = 1,
    Commerciale,
    IstituzionaleCommerciale = Istituzionale | Commerciale
}

export class Clifor {
    id: number;
    nome: string;
    soggetto: CliforSoggettoEnum;
    istituzionale: CliforIstituzionaleEnum;
}

export class RigaDigitata {
    id: number;
    documentoId: number | null;
    contoDareId: number | null;
    contoAvereId: number | null;
    voceIvaId: number | null;
    trattamento: TrattamentoEnum;
    titoloInapplicabilitaId: number | null;
    aliquotaIvaId: number | null;
    imponibile: number | null;
    iva: number | null;
    percentualeIndetraibilita: number | null;
    percentualeIndeducibilita: number | null;
    settore: number | null;
    note: string | null;
}

export class Conto {
    id: number;
    nome: string;
}

export enum TrattamentoEnum {
    Detraibile = 1,
    IOgg,
    ISogg,
    Esente
}

export class SituazioneVoceIva {
    voceIvaId: number;
    aliquotaIvaId: number;
    trattamento: TrattamentoEnum;
    titoloInapplicabilita: number;
    valore: number;
}

export class SituazioneConto {
    contoId: number;
    valore: number;
    variazioneFiscale: number;
}

export class VoceIva {
    id: number;
    nome: string;
}

export class AliquotaIva {
    id: number;
    percentuale: number;
}

export class Effetto {
    id: number;
    documentoId: number;
    rigaDigitataId: number;
    contoDareId: number;
    contoAvereId: number;
    voceIvaId: number;
    trattamento: TrattamentoEnum;
    titoloInapplicabilita: number;
    aliquotaIvaId: number;
    valore: number;
    variazioneFiscale: number;
    imponibile: number;
    iva: number;
    riferimentoEffettoId: number;
}

export class TitoloInapplicabilita {
    id: number;
    nome: string;
}

export class EffettoCalcolo {
    constructor() {
        this.effettoContos =
            this.effettoIvas =
            this.situazioneContos =
            this.situazioneVoceIvas =
            this.effettoDocumentoList =
            this.effettoRigaList = [];
    }

    effettoContos: EffettoConto[];
    effettoIvas: EffettoIva[];
    situazioneContos: SituazioneConto[];
    situazioneVoceIvas: SituazioneVoceIva[];
    effettoDocumentoList: Documento[];
    effettoRigaList: RigaDigitata[];
}

export class EffettoConto {
    id: number;
    documentoId: number;
    rigaDigitataId: number;
    contoDareId: number;
    contoAvereId: number;
    imponibile: number;
    valore: number;
    variazioneFiscale: number;
    riferimentoEffettoId: number;
}

export class EffettoIva {
    id: number;
    documentoId: number;
    rigaDigitataId: number;
    voceIvaId: number;
    trattamento: TrattamentoEnum;
    titoloInapplicabilita: number;
    aliquotaIvaId: number;
    imponibile: number;
    iva: number;
    riferimentoEffettoId: number;
}
export class Feedback {
    id: number;
    Descrizione: string;
    Effetto: string;
}

export class EffettoFeedback {
    documento: Documento;
    effettoContos: EffettoConto[];
    effettoIvas: EffettoIva[];
    situazioneContos: SituazioneConto[];
    situazioneVoceIvas: SituazioneVoceIva[];
    effettoDocumentoList: Documento[];
    effettoRigaList: RigaDigitata[];
}

