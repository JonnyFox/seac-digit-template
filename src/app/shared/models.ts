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
    totale: number;
    ritenutaAcconto: number;
    sospeso: DocumentoSospensioneEnum;
    tipo: DocumentoTipoEnum;
    caratteristica: DocumentoCaratteristicaEnum;
    cliforId: number;
    registro: RegistroTipoEnum;
    riferimentoDocumentoId: number;
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
    documentId: number;
    contoDareId: number;
    contoAvereId: number;
    voceIVAId: number;
    trattamento: TrattamentoEnum;
    titoloInapplicabilita: number;
    aliquotaIVAId: number;
    imponibile: number;
    IVA: number;
    percentualeIndetraibilita: number;
    percentualeIndeducibilita: number;
    settore: number;
    note: string;
}

export class Conto {
    id: number;
    nome: string;
}

export enum TrattamentoEnum {
    Detraibile,
    IdentraibileOggettivo,
    IdentraibileSoggettivo
}

export class SituazioneVoceIVA {
    voceIVAId: number;
    trattamento: TrattamentoEnum;
    titoloInapplicabilita: number;
    aliquotaIVAId: number;
    valore: number;
}

export class SituazioneConto {
    contoId: number;
    valore: number;
    variazione: number;
}

export class VoceIVA {
    id: number;
    nome: string;
}

export class AliquotaIVA {
    id: number;
    percentuale: number;
}
