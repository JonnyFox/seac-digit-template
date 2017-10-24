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
    None,
    Detraibile,
    IdentraibileOggettivo,
    IdentraibileSoggettivo,
    Esente
}

export class SituazioneVoceIVA {
    voceIvaId: number;
    trattamento: TrattamentoEnum;
    titoloInapplicabilita: number;
    aliquotaIvaId: number;
    valore: number;
}

export class SituazioneConto {
    contoId: number;
    valore: number;
    variazione: number;
}

export class VoceIva {
    id: number;
    nome: string;
}

export class AliquotaIva {
    id: number;
    percentuale: number;
}

export class EffettoCalcolo {
    documento: Documento;
    rigaDigitataList: RigaDigitata[];
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
    imponibile: number;
    valore: number;
    variazione: number;
    riferimentoEffettoId: number;
}

export class TitoloInapplicabilita {
    id: number;
    nome: string;
}
