// tslint:disable:no-bitwise
export enum DocumentSuspensionEnum {
    None,
    IVA,
    Conto,
    ContoIVA = IVA | Conto
}

export enum DocumentTypeEnum {
    Fattura,
    NotaAccredito,
    NotaAddebito,
    BollaDoganale,
    CartaCarburante,
    Contabile
}

export enum DocumentFeature {
    Normale,
    Autofattura,
    ReverseCharge,
    FatturaUE,
    Differita
}

export class Document {
    id: number;
    totale: number;
    ritenutaAcconto: number;
    sospeso: DocumentSuspensionEnum;
    tipo: DocumentTypeEnum;
    caratteristica: DocumentFeature;
}
