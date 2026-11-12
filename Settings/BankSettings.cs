using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ETS2_ModTool
{
    public class BankSettings
    {
        [Category("Bank (bank_data.sii)")]
        [DisplayName("Bank Allgemein")]
        [Description("Regeln für Kontoüberziehung (Dispo), Mahnungen und Sperrfristen nach Kreditrückzahlung.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public BankGeneralSettings General { get; set; } = new BankGeneralSettings();

        [Category("Bank (bank_data.sii)")]
        [DisplayName("Kredit 1 (Kleinkredit)")]
        [Description("Kleinster Sofortkredit für Reparaturen oder erste Engpässe am Spielanfang.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public LoanSettings Loan1 { get; set; } = new LoanSettings("10000", 23.0, "5");

        [Category("Bank (bank_data.sii)")]
        [DisplayName("Kredit 2 (Mittelkredit)")]
        [Description("Mittlerer Kredit, ideal für den ersten eigenen Anhänger oder gebrauchten LKW.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public LoanSettings Loan2 { get; set; } = new LoanSettings("50000", 20.0, "5");

        [Category("Bank (bank_data.sii)")]
        [DisplayName("Kredit 3 (Großkredit)")]
        [Description("Großer Kredit für den Kauf eines neuen LKWs oder den Ausbau einer Garage.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public LoanSettings Loan3 { get; set; } = new LoanSettings("100000", 18.0, "5");

        [Category("Bank (bank_data.sii)")]
        [DisplayName("Kredit 4 (Riesenkredit)")]
        [Description("Maximalkredit für den Kauf neuer Garagen und mehrerer Fahrzeuge inklusive Fahrer.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public LoanSettings Loan4 { get; set; } = new LoanSettings("400000", 12.0, "10");

        public void Import(string content)
        {
            General.MinAfterLoanRepay = ExtractValue(content, "min_after_loan_repay", General.MinAfterLoanRepay);
            General.OverdraftDuration = ExtractValue(content, "overdraft_duration", General.OverdraftDuration);
            General.OverdraftWarnDay = ExtractValue(content, "overdraft_warn_day", General.OverdraftWarnDay);

            Loan1.Amount = ExtractBlockValue(content, @"bank\.data\.loan1", "amount", Loan1.Amount);
            Loan1.InterestRate = ExtractInterestRate(content, @"bank\.data\.loan1", Loan1.InterestRate);
            Loan1.Duration = ExtractBlockValue(content, @"bank\.data\.loan1", "duration", Loan1.Duration);

            Loan2.Amount = ExtractBlockValue(content, @"bank\.data\.loan2", "amount", Loan2.Amount);
            Loan2.InterestRate = ExtractInterestRate(content, @"bank\.data\.loan2", Loan2.InterestRate);
            Loan2.Duration = ExtractBlockValue(content, @"bank\.data\.loan2", "duration", Loan2.Duration);

            Loan3.Amount = ExtractBlockValue(content, @"bank\.data\.loan3", "amount", Loan3.Amount);
            Loan3.InterestRate = ExtractInterestRate(content, @"bank\.data\.loan3", Loan3.InterestRate);
            Loan3.Duration = ExtractBlockValue(content, @"bank\.data\.loan3", "duration", Loan3.Duration);

            Loan4.Amount = ExtractBlockValue(content, @"bank\.data\.loan4", "amount", Loan4.Amount);
            Loan4.InterestRate = ExtractInterestRate(content, @"bank\.data\.loan4", Loan4.InterestRate);
            Loan4.Duration = ExtractBlockValue(content, @"bank\.data\.loan4", "duration", Loan4.Duration);
        }

        public void Export(ref string content)
        {
            UpdateParameter(ref content, "min_after_loan_repay", General.MinAfterLoanRepay);
            UpdateParameter(ref content, "overdraft_duration", General.OverdraftDuration);
            UpdateParameter(ref content, "overdraft_warn_day", General.OverdraftWarnDay);

            string l1Int = (Loan1.InterestRate / 100.0).ToString(CultureInfo.InvariantCulture);
            string l2Int = (Loan2.InterestRate / 100.0).ToString(CultureInfo.InvariantCulture);
            string l3Int = (Loan3.InterestRate / 100.0).ToString(CultureInfo.InvariantCulture);
            string l4Int = (Loan4.InterestRate / 100.0).ToString(CultureInfo.InvariantCulture);

            UpdateBlockParameter(ref content, @"bank\.data\.loan1", "amount", Loan1.Amount);
            UpdateBlockParameter(ref content, @"bank\.data\.loan1", "interest_rate", l1Int);
            UpdateBlockParameter(ref content, @"bank\.data\.loan1", "duration", Loan1.Duration);

            UpdateBlockParameter(ref content, @"bank\.data\.loan2", "amount", Loan2.Amount);
            UpdateBlockParameter(ref content, @"bank\.data\.loan2", "interest_rate", l2Int);
            UpdateBlockParameter(ref content, @"bank\.data\.loan2", "duration", Loan2.Duration);

            UpdateBlockParameter(ref content, @"bank\.data\.loan3", "amount", Loan3.Amount);
            UpdateBlockParameter(ref content, @"bank\.data\.loan3", "interest_rate", l3Int);
            UpdateBlockParameter(ref content, @"bank\.data\.loan3", "duration", Loan3.Duration);

            UpdateBlockParameter(ref content, @"bank\.data\.loan4", "amount", Loan4.Amount);
            UpdateBlockParameter(ref content, @"bank\.data\.loan4", "interest_rate", l4Int);
            UpdateBlockParameter(ref content, @"bank\.data\.loan4", "duration", Loan4.Duration);
        }

        private string ExtractValue(string content, string parameterName, string fallback)
        {
            Match match = new Regex($@"{parameterName}:\s*(?<value>[0-9eE.-]+)").Match(content);
            return match.Success ? match.Groups["value"].Value : fallback;
        }

        private void UpdateParameter(ref string content, string parameterName, string newValue)
        {
            Regex regex = new Regex($@"({parameterName}:\s*)[0-9eE.-]+");
            content = regex.Replace(content, $"${{1}}{newValue}");
        }

        private string ExtractBlockValue(string content, string blockId, string parameterName, string fallback)
        {
            Match match = new Regex($@"{blockId}[^}}]*?{parameterName}:\s*(?<value>[0-9eE.-]+)").Match(content);
            return match.Success ? match.Groups["value"].Value : fallback;
        }

        private void UpdateBlockParameter(ref string content, string blockId, string parameterName, string newValue)
        {
            Regex regex = new Regex($@"({blockId}[^}}]*?{parameterName}:\s*)[0-9eE.-]+");
            content = regex.Replace(content, $"${{1}}{newValue}", 1);
        }

        private double ExtractInterestRate(string content, string blockId, double fallback)
        {
            string valStr = ExtractBlockValue(content, blockId, "interest_rate", fallback.ToString(CultureInfo.InvariantCulture));
            if (double.TryParse(valStr, NumberStyles.Any, CultureInfo.InvariantCulture, out double parsed))
            {
                return parsed * 100.0;
            }
            return fallback;
        }
    }

    public class BankGeneralSettings
    {
        [DisplayName("Sperrfrist nach Kreditrückzahlung")]
        [Description("Standard: 0 | Wartezeit nach vollständiger Tilgung, bevor derselbe Kredit erneut aufgenommen werden kann.\n• Funktion: Verhindert direktes Umschulden, wenn ein Wert > 0 gesetzt wird.\n• Empfehlung: 0 belassen für freie Kreditaufnahme.")]
        public string MinAfterLoanRepay { get; set; } = "0";

        [DisplayName("Dauer Dispokredit (Tage)")]
        [Description("Standard: 5 Tage | Anzahl der Spieltage, die dein Konto im Minus (überzogen) sein darf.\n• Funktion: Ist das Konto nach Ablauf dieser Tage weiterhin negativ, pfändet die Bank automatisch LKWs oder Anhänger.\n• Tipp: Höhere Werte (z. B. 10) geben mehr Spielraum bei harten Wirtschafts-Mods.")]
        public string OverdraftDuration { get; set; } = "5";

        [DisplayName("Warnung Dispokredit (Tag)")]
        [Description("Standard: 2 Tage | Spieltag im Minus, an dem die Bank dir eine dringende Mahnungs-E-Mail schickt.\n• Hinweis: Sollte immer kleiner sein als die 'Dauer Dispokredit'.")]
        public string OverdraftWarnDay { get; set; } = "2";
    }

    public class LoanSettings
    {
        [DisplayName("Kredithöhe (€)")]
        [Description("Standard: 10.000 bis 400.000 € | Der Betrag, der dir bei Aufnahme dieses Kredits sofort aufs Konto überwiesen wird.\n• Hinweis: Beachte auch das gesamte Kreditlimit unter '7. Events & Freischaltung', damit große Kredite nicht vom Limit blockiert werden.")]
        public string Amount { get; set; } = "10000";

        [DisplayName("Zinssatz (%)")]
        [Description("Standard: 12% bis 23% | Jährlicher Nominalzinssatz in Prozent (z. B. 15 für 15%).\n• Funktion: Bestimmt zusammen mit der Laufzeit die tägliche Tilgungsrate (jeden Tag um 12:00 Uhr abgebucht).\n• Realismus-Tipp: Echte Nutzfahrzeug-Finanzierungen liegen meist zwischen 5% und 9%.")]
        public double InterestRate { get; set; } = 23.0;

        [DisplayName("Laufzeit (Wochen)")]
        [Description("Standard: 5 bzw. 10 Wochen | Rückzahlungsdauer in Spielwochen (1 Woche = 7 Spieltage).\n• Beispiel: '5' bedeutet 35 tägliche Raten. Eine längere Laufzeit senkt die tägliche Rate, erhöht aber die gesamten Zinskosten.")]
        public string Duration { get; set; } = "5";

        public LoanSettings()
        {
        }

        public LoanSettings(string amount, double interest, string duration)
        {
            Amount = amount;
            InterestRate = interest;
            Duration = duration;
        }
    }
}
