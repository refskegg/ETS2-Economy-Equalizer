using System.ComponentModel;
using System.Text.RegularExpressions;

namespace ETS2_ModTool
{
    public class SpecialEventSettings
    {
        [Category("Spezial-Events (special_event_data.sii)")]
        [DisplayName("1. Freischaltungen & Aufträge")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public SpecialEventUnlocksSettings Unlocks { get; set; } = new SpecialEventUnlocksSettings();

        [Category("Spezial-Events (special_event_data.sii)")]
        [DisplayName("2. Kreditlimits & Stufen")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public SpecialEventCreditSettings Credit { get; set; } = new SpecialEventCreditSettings();

        public void Import(string content)
        {
            // 1. Unlocks & Delays[cite: 5]
            Unlocks.JobsBeforeFirstLoan = ExtractValue(content, "jobs_before_first_loan", Unlocks.JobsBeforeFirstLoan);
            Unlocks.JobsBeforeCompanyInvitation = ExtractValue(content, "jobs_before_company_invitation", Unlocks.JobsBeforeCompanyInvitation);
            Unlocks.FirstDealerUnlockDelay = ExtractValue(content, "first_dealer_unlock_delay", Unlocks.FirstDealerUnlockDelay);
            Unlocks.FirstJobPreferredRadius = ExtractValue(content, "first_job_preferred_radius", Unlocks.FirstJobPreferredRadius);
            Unlocks.TrailersUnlockLevel = ExtractValue(content, "trailers_unlock_level", Unlocks.TrailersUnlockLevel);

            // 2. Kredit-Stufen (Reihenfolge der Array-Einträge im File)[cite: 5]
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
            // 1. Unlocks & Delays schreiben[cite: 5]
            UpdateParameter(ref content, "jobs_before_first_loan", Unlocks.JobsBeforeFirstLoan);
            UpdateParameter(ref content, "jobs_before_company_invitation", Unlocks.JobsBeforeCompanyInvitation);
            UpdateParameter(ref content, "first_dealer_unlock_delay", Unlocks.FirstDealerUnlockDelay);
            UpdateParameter(ref content, "first_job_preferred_radius", Unlocks.FirstJobPreferredRadius);
            UpdateParameter(ref content, "trailers_unlock_level", Unlocks.TrailersUnlockLevel);

            // 2. Kredit-Stufen schreiben[cite: 5]
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
        [DisplayName("Aufträge bis 1. Kredit")]
        [Description("Benötigte absolvierte Fahrten bis zur ersten Krediterlaubnis (Standard: 2)[cite: 5].")]
        public string JobsBeforeFirstLoan { get; set; } = "2";

        [DisplayName("Aufträge bis Einladung")]
        [Description("Fahrten bis zur Einladung durch Speditionen (Standard: 2)[cite: 5].")]
        public string JobsBeforeCompanyInvitation { get; set; } = "2";

        [DisplayName("Verzögerung 1. Händler (Sekunden)")]
        [Description("Wartezeit bis zur Freischaltung des ersten LKW-Händlers (Standard: 120 = 2 Min)[cite: 5].")]
        public string FirstDealerUnlockDelay { get; set; } = "120";

        [DisplayName("Bevorzugter Radius 1. Auftrag")]
        [Description("Distanzbeschränkung für den ersten Auftrag (Standard: 0)[cite: 5].")]
        public string FirstJobPreferredRadius { get; set; } = "0";

        [DisplayName("Anhänger-Kauf Level")]
        [Description("Spielerlevel, ab dem eigene Anhänger gekauft werden können (Standard: 5)[cite: 5].")]
        public string TrailersUnlockLevel { get; set; } = "5";
    }

    public class SpecialEventCreditSettings
    {
        [DisplayName("Stufe 1: Benötigtes Level")]
        [Description("Spielerstufe für die erste Kreditlimit-Erhöhung (Standard: 0)[cite: 5].")]
        public string LevelTier1 { get; set; } = "0";

        [DisplayName("Stufe 1: Kreditlimit (€)")]
        [Description("Maximaler Kreditbetrag für Stufe 1 (Standard: 100000)[cite: 5].")]
        public string LimitTier1 { get; set; } = "100000";

        [DisplayName("Stufe 2: Benötigtes Level")]
        [Description("Spielerstufe für die zweite Kreditlimit-Erhöhung (Standard: 3)[cite: 5].")]
        public string LevelTier2 { get; set; } = "3";

        [DisplayName("Stufe 2: Kreditlimit (€)")]
        [Description("Maximaler Kreditbetrag für Stufe 2 (Standard: 500000)[cite: 5].")]
        public string LimitTier2 { get; set; } = "500000";
    }
}