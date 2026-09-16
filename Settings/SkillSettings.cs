using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ETS2_ModTool
{
    public class SkillSettings
    {
        [Category("Fähigkeiten (skill_data.sii)")]
        [DisplayName("1. Fernfahrt (Long Distance)")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public LongDistanceSkillSettings LongDistance { get; set; } = new LongDistanceSkillSettings();

        [Category("Fähigkeiten (skill_data.sii)")]
        [DisplayName("2. Wertvolle Fracht (High Value)")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public ValuableSkillSettings Valuable { get; set; } = new ValuableSkillSettings();

        [Category("Fähigkeiten (skill_data.sii)")]
        [DisplayName("3. Zerbrechliche Fracht (Fragile)")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public FragileSkillSettings Fragile { get; set; } = new FragileSkillSettings();

        [Category("Fähigkeiten (skill_data.sii)")]
        [DisplayName("4. Eillieferung (Just In Time)")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public UrgentSkillSettings Urgent { get; set; } = new UrgentSkillSettings();

        [Category("Fähigkeiten (skill_data.sii)")]
        [DisplayName("5. Sparsame Fahrweise (Eco Driving)")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public EcoDrivingSkillSettings EcoDriving { get; set; } = new EcoDrivingSkillSettings();

        [Category("Fähigkeiten (skill_data.sii)")]
        [DisplayName("6. Gefahrgut (ADR) XP")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public AdrSkillSettings ADR { get; set; } = new AdrSkillSettings();

        public void Import(string content)
        {
            // 1. Long Distance
            ImportRankArray(content, @"\.skills\.long_dist\.xp", "percentage_increase_per_rank", LongDistance.Xp);
            ImportRankArray(content, @"\.skills\.long_dist\.revenue", "percentage_increase_per_rank", LongDistance.Revenue);
            ImportRankArray(content, @"\.skills\.long_dist\.distances", "max_distance_per_rank", LongDistance.Distances);

            // 2. Valuable
            ImportRankArray(content, @"\.skills\.valuable\.xp", "percentage_increase_per_rank", Valuable.Xp);
            ImportRankArray(content, @"\.skills\.valuable\.revenue", "percentage_increase_per_rank", Valuable.Revenue);

            // 3. Fragile
            ImportRankArray(content, @"\.skills\.fragile\.xp", "percentage_increase_per_rank", Fragile.Xp);
            ImportRankArray(content, @"\.skills\.fragile\.revenue", "percentage_increase_per_rank", Fragile.Revenue);

            // 4. Urgent
            ImportRankArray(content, @"\.skills\.urgent\.xp", "percentage_increase_per_rank", Urgent.Xp);
            ImportRankArray(content, @"\.skills\.urgent\.revenue", "percentage_increase_per_rank", Urgent.Revenue);
            Urgent.ImportantJobXpBonusCoef = ExtractBlockValue(content, @"\.skills\.urgent\.coefs", "important_job_xp_bonus_coef", Urgent.ImportantJobXpBonusCoef);
            Urgent.ImportantJobRevenueBonusCoef = ExtractBlockValue(content, @"\.skills\.urgent\.coefs", "important_job_revenue_bonus_coef", Urgent.ImportantJobRevenueBonusCoef);

            // 5. Eco Driving
            ImportRankArray(content, @"\.skills\.ecodriving\.saved", "percentage_decrease_per_rank", EcoDriving.Saved);

            // 6. ADR
            ImportRankArray(content, @"\.skills\.adr\.xp", "percentage_increase_per_rank", ADR.Xp);
        }

        public void Export(ref string content)
        {
            // 1. Long Distance
            ExportRankArray(ref content, @"\.skills\.long_dist\.xp", "percentage_increase_per_rank", LongDistance.Xp);
            ExportRankArray(ref content, @"\.skills\.long_dist\.revenue", "percentage_increase_per_rank", LongDistance.Revenue);
            ExportRankArray(ref content, @"\.skills\.long_dist\.distances", "max_distance_per_rank", LongDistance.Distances);

            // 2. Valuable
            ExportRankArray(ref content, @"\.skills\.valuable\.xp", "percentage_increase_per_rank", Valuable.Xp);
            ExportRankArray(ref content, @"\.skills\.valuable\.revenue", "percentage_increase_per_rank", Valuable.Revenue);

            // 3. Fragile
            ExportRankArray(ref content, @"\.skills\.fragile\.xp", "percentage_increase_per_rank", Fragile.Xp);
            ExportRankArray(ref content, @"\.skills\.fragile\.revenue", "percentage_increase_per_rank", Fragile.Revenue);

            // 4. Urgent
            ExportRankArray(ref content, @"\.skills\.urgent\.xp", "percentage_increase_per_rank", Urgent.Xp);
            ExportRankArray(ref content, @"\.skills\.urgent\.revenue", "percentage_increase_per_rank", Urgent.Revenue);
            UpdateBlockParameter(ref content, @"\.skills\.urgent\.coefs", "important_job_xp_bonus_coef", Urgent.ImportantJobXpBonusCoef);
            UpdateBlockParameter(ref content, @"\.skills\.urgent\.coefs", "important_job_revenue_bonus_coef", Urgent.ImportantJobRevenueBonusCoef);

            // 5. Eco Driving
            ExportRankArray(ref content, @"\.skills\.ecodriving\.saved", "percentage_decrease_per_rank", EcoDriving.Saved);

            // 6. ADR
            ExportRankArray(ref content, @"\.skills\.adr\.xp", "percentage_increase_per_rank", ADR.Xp);
        }

        private void ImportRankArray(string content, string blockPattern, string propertyName, SkillRankValues target)
        {
            Match blockMatch = new Regex($@"{blockPattern}[^}}]*?\{{(?<inner>[^}}]+)\}}").Match(content);
            if (!blockMatch.Success) return;

            string blockBody = blockMatch.Groups["inner"].Value;
            MatchCollection matches = new Regex($@"{propertyName}\[\]:\s*(?<value>[0-9eE.-]+)").Matches(blockBody);

            if (matches.Count == 1)
            {
                string singleVal = matches[0].Groups["value"].Value;
                target.Rank1 = singleVal;
                target.Rank2 = singleVal;
                target.Rank3 = singleVal;
                target.Rank4 = singleVal;
                target.Rank5 = singleVal;
                target.Rank6 = singleVal;
            }
            else if (matches.Count >= 6)
            {
                target.Rank1 = matches[0].Groups["value"].Value;
                target.Rank2 = matches[1].Groups["value"].Value;
                target.Rank3 = matches[2].Groups["value"].Value;
                target.Rank4 = matches[3].Groups["value"].Value;
                target.Rank5 = matches[4].Groups["value"].Value;
                target.Rank6 = matches[5].Groups["value"].Value;
            }
        }

        private void ExportRankArray(ref string content, string blockPattern, string propertyName, SkillRankValues values)
        {
            Regex blockRegex = new Regex($@"(?<header>{blockPattern}[^}}]*?\{{)(?<inner>[^}}]+)\}}");
            Match match = blockRegex.Match(content);
            if (!match.Success) return;

            string header = match.Groups["header"].Value;
            string inner = match.Groups["inner"].Value;

            // Vorhandene Array-Zeilen dieses Attributs komplett entfernen
            string cleaned = Regex.Replace(inner, $@"[ \t]*{propertyName}\[\]:[^\r\n]*(\r?\n)?", "");

            // Genau 6 Zeilen sauber einrücken
            string newEntries =
                $"\n\t{propertyName}[]: {values.Rank1}" +
                $"\n\t{propertyName}[]: {values.Rank2}" +
                $"\n\t{propertyName}[]: {values.Rank3}" +
                $"\n\t{propertyName}[]: {values.Rank4}" +
                $"\n\t{propertyName}[]: {values.Rank5}" +
                $"\n\t{propertyName}[]: {values.Rank6}\n";

            // Block inklusive schließender Klammer '}' an der exakten Indexposition ersetzen
            string replacement = header + cleaned.TrimEnd() + newEntries + "}";
            content = content.Substring(0, match.Index) + replacement + content.Substring(match.Index + match.Length);
        }

        private string ExtractBlockValue(string content, string blockId, string paramName, string fallback)
        {
            Match m = new Regex($@"{blockId}[^}}]*?{paramName}:\s*(?<value>[0-9eE.-]+)").Match(content);
            return m.Success ? m.Groups["value"].Value : fallback;
        }

        private void UpdateBlockParameter(ref string content, string blockId, string paramName, string newVal)
        {
            Regex regex = new Regex($@"({blockId}[^}}]*?{paramName}:\s*)[0-9eE.-]+");
            content = regex.Replace(content, $"${{1}}{newVal}", 1);
        }
    }

    public class SkillRankValues
    {
        [DisplayName("Rang 1")]
        public string Rank1 { get; set; }

        [DisplayName("Rang 2")]
        public string Rank2 { get; set; }

        [DisplayName("Rang 3")]
        public string Rank3 { get; set; }

        [DisplayName("Rang 4")]
        public string Rank4 { get; set; }

        [DisplayName("Rang 5")]
        public string Rank5 { get; set; }

        [DisplayName("Rang 6")]
        public string Rank6 { get; set; }

        public SkillRankValues(string r1, string r2, string r3, string r4, string r5, string r6)
        {
            Rank1 = r1; Rank2 = r2; Rank3 = r3;
            Rank4 = r4; Rank5 = r5; Rank6 = r6;
        }

        public SkillRankValues(string uniformValue)
        {
            Rank1 = uniformValue; Rank2 = uniformValue; Rank3 = uniformValue;
            Rank4 = uniformValue; Rank5 = uniformValue; Rank6 = uniformValue;
        }
    }

    public class LongDistanceSkillSettings
    {
        [DisplayName("XP-Bonus")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public SkillRankValues Xp { get; set; } = new SkillRankValues("0.25");

        [DisplayName("Umsatz-Bonus (Revenue)")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public SkillRankValues Revenue { get; set; } = new SkillRankValues("0.05", "0.10", "0.15", "0.20", "0.25", "0.30");

        [DisplayName("Max. Distanz pro Rang (km)")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public SkillRankValues Distances { get; set; } = new SkillRankValues("400.0", "650.0", "1000.0", "1600.0", "2500.0", "4000.0");
    }

    public class ValuableSkillSettings
    {
        [DisplayName("XP-Bonus")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public SkillRankValues Xp { get; set; } = new SkillRankValues("0.18");

        [DisplayName("Umsatz-Bonus (Revenue)")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public SkillRankValues Revenue { get; set; } = new SkillRankValues("0.05", "0.10", "0.15", "0.20", "0.25", "0.30");
    }

    public class FragileSkillSettings
    {
        [DisplayName("XP-Bonus")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public SkillRankValues Xp { get; set; } = new SkillRankValues("0.22");

        [DisplayName("Umsatz-Bonus (Revenue)")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public SkillRankValues Revenue { get; set; } = new SkillRankValues("0.05", "0.10", "0.15", "0.20", "0.25", "0.30");
    }

    public class UrgentSkillSettings
    {
        [DisplayName("XP-Bonus")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public SkillRankValues Xp { get; set; } = new SkillRankValues("0.30");

        [DisplayName("Umsatz-Bonus (Revenue)")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public SkillRankValues Revenue { get; set; } = new SkillRankValues("0.05", "0.10", "0.15", "0.20", "0.25", "0.30");

        [DisplayName("Wichtige Jobs: XP-Koeffizient")]
        [Description("Standard: 0.66")]
        public string ImportantJobXpBonusCoef { get; set; } = "0.66";

        [DisplayName("Wichtige Jobs: Umsatz-Koeffizient")]
        [Description("Standard: 0.6")]
        public string ImportantJobRevenueBonusCoef { get; set; } = "0.6";
    }

    public class EcoDrivingSkillSettings
    {
        [DisplayName("Spritersparnis-Faktor")]
        [Description("Reduktion des Treibstoffverbrauchs je Rang (Standard: 0.10 bis 0.35).")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public SkillRankValues Saved { get; set; } = new SkillRankValues("0.10", "0.15", "0.20", "0.25", "0.30", "0.35");
    }

    public class AdrSkillSettings
    {
        [DisplayName("XP-Bonus")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public SkillRankValues Xp { get; set; } = new SkillRankValues("0.21");
    }
}