using System.ComponentModel;
using System.Text.RegularExpressions;

namespace ETS2_ModTool
{
    public class SpecialEventSettings
    {
        [Category("Spezial-Events (special_event_data.sii)")]
        [DisplayName("1. Freischaltungen & Spielstart")]
        [Description("Wann Banken, LKW-Händler und der Anhängerkauf für ein neues Profil freigeschaltet werden.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public SpecialEventUnlocksSettings Unlocks { get; set; } = new SpecialEventUnlocksSettings();

        [Category("Spezial-Events (special_event_data.sii)")]
        [DisplayName("2. Gesamtes Kreditlimit & Stufen")]
        [Description("Wie hoch deine gesamte Verschuldung bei der Bank je nach Spielerlevel maximal sein darf.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public SpecialEventCreditSettings Credit { get; set; } = new SpecialEventCreditSettings();

        public void Import(string content)
        {
            // 1. Unlocks & Delays
            Unlocks.JobsBeforeFirstLoan = ExtractValue(content, "jobs_before_first_loan", Unlocks.JobsBeforeFirstLoan);
            Unlocks.JobsBeforeCompanyInvitation = ExtractValue(content, "jobs_before_company_invitation", Unlocks.JobsBeforeCompanyInvitation);
            Unlocks.FirstDealerUnlockDelay = ExtractValue(content, "first_dealer_unlock_delay", Unlocks.FirstDealerUnlockDelay);
            Unlocks.FirstJobPreferredRadius = ExtractValue(content, "first_job_preferredRadius", Unlocks.FirstJobPreferredRadius);
            Unlocks.FirstJobPreferredRadius = ExtractValue(content, "first_job_preferred_radius", Unlocks.FirstJobPreferredRadius);
            Unlocks.TrailersUnlockLevel = ExtractValue(content, "trailers_unlock_level", Unlocks.TrailersUnlockLevel);

            // 2. Credit Tiers (order of array entries in the file)
            MatchCollection levelMatches = new Regex(@"level_for_credit_raise\[\]:\s*(?<value>[0-9eE.-]+)").Matches(content);
            MatchCollection limitMatches = new Regex(@"credit_limit\[\]:\s*(?<value>[0-9eE.-]+)").Matches(content);

            if (levelMatches.Count >= 2 && limitMatches.Count >= 2)
            {
                Credit.LevelTier1 = levelMatches[0].Groups["value"].Value;
                Credit.LimitTier1 = limitMatches[0].Groups["value"].Value;
                Credit.LevelTier2 = levelMatches[1].Groups["value"].Value;
                Credit.LimitTier2 = limitMatches[1].Groups["value"].Value;
            }
        }

        public void Export(ref string content)
        {
            // 1. Write Unlocks & Delays
            UpdateParameter(ref content, "jobs_before_first_loan", Unlocks.JobsBeforeFirstLoan);
            UpdateParameter(ref content, "jobs_before_company_invitation", Unlocks.JobsBeforeCompanyInvitation);
            UpdateParameter(ref content, "first_dealer_unlock_delay", Unlocks.FirstDealerUnlockDelay);
            UpdateParameter(ref content, "first_job_preferred_radius", Unlocks.FirstJobPreferredRadius);
            UpdateParameter(ref content, "trailers_unlock_level", Unlocks.TrailersUnlockLevel);

            // 2. Write Credit Tiers
            UpdateSequentialArrayItem(ref content, "level_for_credit_raise", 0, Credit.LevelTier1);
            UpdateSequentialArrayItem(ref content, "credit_limit", 0, Credit.LimitTier1);
            UpdateSequentialArrayItem(ref content, "level_for_credit_raise", 1, Credit.LevelTier2);
            UpdateSequentialArrayItem(ref content, "credit_limit", 1, Credit.LimitTier2);
        }

        private string ExtractValue(string content, string paramName, string fallback)
        {
            Match m = new Regex($@"\b{paramName}:\s*(?<value>[0-9eE.-]+)").Match(content);
            return m.Success ? m.Groups["value"].Value : fallback;
        }

        private void UpdateParameter(ref string content, string paramName, string val)
        {
            Regex r = new Regex($@"(\b{paramName}:\s*)[0-9eE.-]+");
            content = r.Replace(content, $"${{1}}{val}");
        }

        private void UpdateSequentialArrayItem(ref string content, string arrayName, int matchIndex, string val)
        {
            Regex regex = new Regex($@"({arrayName}\[\]:\s*)[0-9eE.-]+");
            int currentIndex = 0;
            content = regex.Replace(content, m =>
            {
                if (currentIndex++ == matchIndex)
                {
                    return $"{m.Groups[1].Value}{val}";
                }
                return m.Value;
            });
        }
    }

    public class SpecialEventUnlocksSettings
    {
        [DisplayName("Aufträge bis zur 1. Kreditfreigabe")]
        [Description("Standard: 2 Aufträge | Wie viele schnelle Aufträge du in einem neuen Profil abschließen musst, bevor dir die Bank die erste Kredit-E-Mail schickt.")]
        public string JobsBeforeFirstLoan { get; set; } = "2";

        [DisplayName("Aufträge bis zur Speditions-Einladung")]
        [Description("Standard: 2 Aufträge | Nach wie vielen Fahrten dich andere Speditionen per E-Mail einladen, weiterhin als Fahrer für sie zu arbeiten.")]
        public string JobsBeforeCompanyInvitation { get; set; } = "2";

        [DisplayName("Verzögerung 1. LKW-Händler (Sekunden)")]
        [Description("Standard: 120 Sek (2 Min) | Wartezeit nach Spielstart, bis die E-Mail mit der Freischaltung des ersten LKW-Händlers deiner Lieblingsmarke eintrifft.")]
        public string FirstDealerUnlockDelay { get; set; } = "120";

        [DisplayName("Bevorzugter Radius für 1. Auftrag (km)")]
        [Description("Standard: 0 (unbegrenzt) | Maximale Entfernung für den allerersten Einführungs-Auftrag eines neuen Profils.")]
        public string FirstJobPreferredRadius { get; set; } = "0";

        [DisplayName("Freischalt-Level für Anhänger-Händler")]
        [Description("Standard: 5 | Spielerlevel, ab dem der Anhänger-Händler freigeschaltet wird und du eigene Auflieger kaufen kannst (1 = sofort frei).")]
        public string TrailersUnlockLevel { get; set; } = "5";
    }

    public class SpecialEventCreditSettings
    {
        [DisplayName("Stufe 1: Benötigtes Spielerlevel")]
        [Description("Standard: 0 | Spielerlevel für das anfängliche Kreditlimit (0 = gilt sofort ab dem ersten Kreditangebot).")]
        public string LevelTier1 { get; set; } = "0";

        [DisplayName("Stufe 1: Maximales Kreditlimit (€)")]
        [Description("Standard: 100.000 € | Höchstbetrag aller gleichzeitigen Kredite auf Stufe 1.\n• Wichtig: Wenn du unter '1. Bank' den ersten Kredit erhöhst, passe ggf. auch dieses Limit an!")]
        public string LimitTier1 { get; set; } = "100000";

        [DisplayName("Stufe 2: Benötigtes Spielerlevel")]
        [Description("Standard: 3 | Spielerlevel, ab dem die Bank deinen maximalen Kreditrahmen auf Stufe 2 erhöht.")]
        public string LevelTier2 { get; set; } = "3";

        [DisplayName("Stufe 2: Maximales Kreditlimit (€)")]
        [Description("Standard: 500.000 € | Höchstbetrag aller gleichzeitigen Kredite auf Stufe 2.\n• Wichtig: Wenn du unter '1. Bank' Kredite über 500.000 € einstellst, musst du dieses Limit ebenfalls erhöhen (z. B. auf 1000000), sonst sind sie im Spiel ausgegraut!")]
        public string LimitTier2 { get; set; } = "500000";
    }
}
