namespace SeacDigitTemplate.Model
{
    public enum DocumentoSospensioneEnum
    {
        None,
        IVA,
        Conto,
        ContoIVA = IVA | Conto
    }

    public enum DocumentoTipoEnum
    {
        Fattura,
        NotaAccredito,
        NotaAddebito,
        BollaDoganale,
        CartaCarburante,
        Contabile
    }

    public enum DocumentoCaratteristicaEnum

    {
        Normale,
        Autofattura,
        ReverseCharge,
        FatturaUE,
        Differita
    }

    public enum RegistroTipoEnum
    {
        Acquisti,
        Emesse,
        Corrispettivi,
        Finanziari
    }

    public enum CliforSoggettoEnum
    {
        Nazionale,
        UE,
        ExtraUE
    }

    public enum CliforIstituzionaleEnum
    {
        Istituzionale = 1,
        Commerciale,
        IstituzionaleCommerciale = Istituzionale | Commerciale
    }

    public enum TrattamentoEnum
    {
        Detraibile = 1,
        IndetraibileOggettivo,
        IndetraibileSoggettivo,
        Esente
    }

    public enum TemplateEffetetoValoreEnum
    {
        Imponibile,
        Iva
    }
}