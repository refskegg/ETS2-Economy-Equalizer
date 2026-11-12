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
        [Description("Schaltet längere Routen frei und gewährt XP- sowie Umsatzboni für weite Strecken.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public LongDistanceSkillSettings LongDistance { get; set; } = new LongDistanceSkillSettings();

        [Category("Fähigkeiten (skill_data.sii)")]
        [DisplayName("2. Wertvolle Fracht (High Value)")]
        [Description("Schaltet hochwertige Fracht frei und erhöht XP sowie Bezahlung für wertvolle Güter.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public ValuableSkillSettings Valuable { get; set; } = new ValuableSkillSettings();

        [Category("Fähigkeiten (skill_data.sii)")]
        [DisplayName("3. Zerbrechliche Fracht (Fragile)")]
        [Description("Schaltet empfindliche Ladung frei und erhöht XP sowie Bezahlung für zerbrechliche Fracht.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public FragileSkillSettings Fragile { get; set; } = new FragileSkillSettings();

        [Category("Fähigkeiten (skill_data.sii)")]
        [DisplayName("4. Eillieferung (Just In Time)")]
        [Description("Schaltet wichtige und dringende Lieferungen frei und steigert deren Vergütung.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public UrgentSkillSettings Urgent { get; set; } = new UrgentSkillSettings();

        [Category("Fähigkeiten (skill_data.sii)")]
        [DisplayName("5. Sparsame Fahrweise (Eco Driving)")]
        [Description("Senkt den Treibstoffverbrauch von dir und deinen angestellten KI-Fahrern je Rang.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public EcoDrivingSkillSettings EcoDriving { get; set; } = new EcoDrivingSkillSettings();

        [Category("Fähigkeiten (skill_data.sii)")]
        [DisplayName("6. Gefahrgut (ADR)")]
        [Description("XP-Bonus für das Abliefern von Gefahrgut-Frachten der 6 ADR-Klassen.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public AdrSkillSettings ADR { get; set; } = new AdrSkillSettings();

        // --- ETS2 1.62+ Bus / Coaches DLC Skills (3 ranks each) ---
        [Category("Fähigkeiten (skill_data.sii)")]
        [DisplayName("🚌 7. Bus-DLC: Lange Schicht (Long Duty)")]
        [Description("Schaltet Buslinien mit mehr Haltestellen frei und erhöht XP sowie Ticketeinnahmen (3 Ränge).")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public BusLongDutySkillSettings BusLongDuty { get; set; } = new BusLongDutySkillSettings();

        [Category("Fähigkeiten (skill_data.sii)")]
        [DisplayName("🚌 8. Bus-DLC: Straffer Fahrplan (Tight Schedule)")]
        [Description("Schaltet anspruchsvollere Busfahrpläne mit kürzeren Fahrzeiten und höherem XP-Bonus frei (3 Ränge).")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public BusTightScheduleSkillSettings BusTightSchedule { get; set; } = new BusTightScheduleSkillSettings();

        [Category("Fähigkeiten (skill_data.sii)")]
        [DisplayName("🚌 9. Bus-DLC: Fahrgast-Toleranz (Satisfaction Decrease)")]
        [Description("Verringert den Zufriedenheitsverlust der Fahrgäste bei Fahrfehlern und erhöht den Ticketumsatz (3 Ränge).")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public BusSatisfactionDecreaseSkillSettings BusSatisfactionDecrease { get; set; } = new BusSatisfactionDecreaseSkillSettings();

        [Category("Fähigkeiten (skill_data.sii)")]
        [DisplayName("🚌 10. Bus-DLC: Sanfte Fahrweise (Low Acceleration)")]
        [Description("Senkt die Empfindlichkeit der Fahrgäste gegenüber starkem Bremsen/Beschleunigen und gewährt XP- & Umsatzboni (3 Ränge).")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public BusLowAccelerationSkillSettings BusLowAcceleration { get; set; } = new BusLowAccelerationSkillSettings();

        [Category("Fähigkeiten (skill_data.sii)")]
        [DisplayName("🚌 11. Bus-DLC: Fahrgast-Begeisterung (Satisfaction Increase)")]
        [Description("Verstärkt den positiven Zufriedenheitsgewinn der Passagiere während einer angenehmen Busfahrt (3 Ränge).")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public BusSatisfactionIncreaseSkillSettings BusSatisfactionIncrease { get; set; } = new BusSatisfactionIncreaseSkillSettings();

        [Category("Fähigkeiten (skill_data.sii)")]
        [DisplayName("🚌 12. Bus-DLC: Sparsame Bus-Fahrweise (Bus Eco Driving)")]
        [Description("Senkt den Treibstoffverbrauch beim Fahren von Reisebussen je freigeschaltetem Rang (3 Ränge).")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public BusEcoDrivingSkillSettings BusEcoDriving { get; set; } = new BusEcoDrivingSkillSettings();

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

            // 7–12. Bus Skills (3 Ranks)
            ImportBusRankArray(content, @"\.skills\.long_duty\.xp", "percentage_increase_per_rank", BusLongDuty.Xp);
            ImportBusRankArray(content, @"\.skills\.long_duty\.revenue", "percentage_increase_per_rank", BusLongDuty.Revenue);
            ImportBusRankArray(content, @"\.skills\.long_duty\.distances", "max_stops_per_rank", BusLongDuty.MaxStops);

            ImportBusRankArray(content, @"\.skills\.tight_sched\.xp", "percentage_increase_per_rank", BusTightSchedule.Xp);
            ImportBusRankArray(content, @"\.skills\.tight_sched\.less_time", "percentage_decrease_per_rank", BusTightSchedule.LessTime);

            ImportBusRankArray(content, @"\.skills\.sat_decrease\.revenue", "percentage_increase_per_rank", BusSatisfactionDecrease.Revenue);
            ImportBusRankArray(content, @"\.skills\.sat_decrease\.neg_satis", "percentage_increase_per_rank", BusSatisfactionDecrease.NegativeSatisfactionReduction);

            ImportBusRankArray(content, @"\.skills\.low_accel\.xp", "percentage_increase_per_rank", BusLowAcceleration.Xp);
            ImportBusRankArray(content, @"\.skills\.low_accel\.revenue", "percentage_increase_per_rank", BusLowAcceleration.Revenue);
            ImportBusRankArray(content, @"\.skills\.low_accel\.sensitivity", "percentage_decrease_per_rank", BusLowAcceleration.SensitivityDecrease);

            ImportBusRankArray(content, @"\.skills\.sat_increase\.pos_satis", "percentage_increase_per_rank", BusSatisfactionIncrease.PositiveSatisfactionIncrease);

            ImportBusRankArray(content, @"\.skills\.bus_eco\.saved", "percentage_decrease_per_rank", BusEcoDriving.Saved);
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

            // 7–12. Bus Skills (3 Ranks)
            ExportBusRankArray(ref content, @"\.skills\.long_duty\.xp", "percentage_increase_per_rank", BusLongDuty.Xp);
            ExportBusRankArray(ref content, @"\.skills\.long_duty\.revenue", "percentage_increase_per_rank", BusLongDuty.Revenue);
            ExportBusRankArray(ref content, @"\.skills\.long_duty\.distances", "max_stops_per_rank", BusLongDuty.MaxStops);

            ExportBusRankArray(ref content, @"\.skills\.tight_sched\.xp", "percentage_increase_per_rank", BusTightSchedule.Xp);
            ExportBusRankArray(ref content, @"\.skills\.tight_sched\.less_time", "percentage_decrease_per_rank", BusTightSchedule.LessTime);

            ExportBusRankArray(ref content, @"\.skills\.sat_decrease\.revenue", "percentage_increase_per_rank", BusSatisfactionDecrease.Revenue);
            ExportBusRankArray(ref content, @"\.skills\.sat_decrease\.neg_satis", "percentage_increase_per_rank", BusSatisfactionDecrease.NegativeSatisfactionReduction);

            ExportBusRankArray(ref content, @"\.skills\.low_accel\.xp", "percentage_increase_per_rank", BusLowAcceleration.Xp);
            ExportBusRankArray(ref content, @"\.skills\.low_accel\.revenue", "percentage_increase_per_rank", BusLowAcceleration.Revenue);
            ExportBusRankArray(ref content, @"\.skills\.low_accel\.sensitivity", "percentage_decrease_per_rank", BusLowAcceleration.SensitivityDecrease);

            ExportBusRankArray(ref content, @"\.skills\.sat_increase\.pos_satis", "percentage_increase_per_rank", BusSatisfactionIncrease.PositiveSatisfactionIncrease);

            ExportBusRankArray(ref content, @"\.skills\.bus_eco\.saved", "percentage_decrease_per_rank", BusEcoDriving.Saved);
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

            // Remove existing array lines for this attribute completely
            string cleaned = Regex.Replace(inner, $@"[ \t]*{propertyName}\[\]:[^\r\n]*(\r?\n)?", "");

            // Format and indent all 6 rank lines cleanly
            string newEntries =
                $"\n\t{propertyName}[]: {values.Rank1}" +
                $"\n\t{propertyName}[]: {values.Rank2}" +
                $"\n\t{propertyName}[]: {values.Rank3}" +
                $"\n\t{propertyName}[]: {values.Rank4}" +
                $"\n\t{propertyName}[]: {values.Rank5}" +
                $"\n\t{propertyName}[]: {values.Rank6}\n";

            // Replace block including closing brace '}' at the exact index position
            string replacement = header + cleaned.TrimEnd() + newEntries + "}";
            content = content.Substring(0, match.Index) + replacement + content.Substring(match.Index + match.Length);
        }

        private void ImportBusRankArray(string content, string blockPattern, string propertyName, BusSkillRankValues target)
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
            }
            else if (matches.Count >= 3)
            {
                target.Rank1 = matches[0].Groups["value"].Value;
                target.Rank2 = matches[1].Groups["value"].Value;
                target.Rank3 = matches[2].Groups["value"].Value;
            }
        }

        private void ExportBusRankArray(ref string content, string blockPattern, string propertyName, BusSkillRankValues values)
        {
            Regex blockRegex = new Regex($@"(?<header>{blockPattern}[^}}]*?\{{)(?<inner>[^}}]+)\}}");
            Match match = blockRegex.Match(content);
            if (!match.Success) return;

            string header = match.Groups["header"].Value;
            string inner = match.Groups["inner"].Value;

            string cleaned = Regex.Replace(inner, $@"[ \t]*{propertyName}\[\]:[^\r\n]*(\r?\n)?", "");

            string newEntries =
                $"\n\t{propertyName}[]: {values.Rank1}" +
                $"\n\t{propertyName}[]: {values.Rank2}" +
                $"\n\t{propertyName}[]: {values.Rank3}\n";

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
        public string Rank1 { get; set; } = "0.0";

        [DisplayName("Rang 2")]
        public string Rank2 { get; set; } = "0.0";

        [DisplayName("Rang 3")]
        public string Rank3 { get; set; } = "0.0";

        [DisplayName("Rang 4")]
        public string Rank4 { get; set; } = "0.0";

        [DisplayName("Rang 5")]
        public string Rank5 { get; set; } = "0.0";

        [DisplayName("Rang 6")]
        public string Rank6 { get; set; } = "0.0";

        public SkillRankValues()
        {
        }

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
        [DisplayName("XP-Bonus je Rang")]
        [Description("Standard: 0.25 (+25%) | Zusätzlicher XP-Bonus für Fernfahrten über 250 km auf dem jeweiligen Skill-Rang.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public SkillRankValues Xp { get; set; } = new SkillRankValues("0.25");

        [DisplayName("Umsatz-Bonus (Revenue) je Rang")]
        [Description("Standard: 0.05 bis 0.30 (+5% bis +30%) | Zusätzlicher Einnahmen-Bonus für Fernfahrten auf dem jeweiligen Rang.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public SkillRankValues Revenue { get; set; } = new SkillRankValues("0.05", "0.10", "0.15", "0.20", "0.25", "0.30");

        [DisplayName("Max. Distanz pro Rang (km)")]
        [Description("Standard: 400 bis 4000 km | Maximale Auftragsentfernung im Frachtmarkt, die auf diesem Rang generiert wird (vor Rang 1 liegt das Limit bei 250 km).")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public SkillRankValues Distances { get; set; } = new SkillRankValues("400.0", "650.0", "1000.0", "1600.0", "2500.0", "4000.0");
    }

    public class ValuableSkillSettings
    {
        [DisplayName("XP-Bonus je Rang")]
        [Description("Standard: 0.18 (+18%) | Zusätzlicher XP-Bonus für Lieferungen von wertvoller Fracht.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public SkillRankValues Xp { get; set; } = new SkillRankValues("0.18");

        [DisplayName("Umsatz-Bonus (Revenue) je Rang")]
        [Description("Standard: 0.05 bis 0.30 (+5% bis +30%) | Zusätzlicher Vergütungsbonus für wertvolle Fracht je erreichter Stufe.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public SkillRankValues Revenue { get; set; } = new SkillRankValues("0.05", "0.10", "0.15", "0.20", "0.25", "0.30");
    }

    public class FragileSkillSettings
    {
        [DisplayName("XP-Bonus je Rang")]
        [Description("Standard: 0.22 (+22%) | Zusätzlicher XP-Bonus für Lieferungen von zerbrechlicher Fracht.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public SkillRankValues Xp { get; set; } = new SkillRankValues("0.22");

        [DisplayName("Umsatz-Bonus (Revenue) je Rang")]
        [Description("Standard: 0.05 bis 0.30 (+5% bis +30%) | Zusätzlicher Vergütungsbonus für zerbrechliche Fracht je erreichter Stufe.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public SkillRankValues Revenue { get; set; } = new SkillRankValues("0.05", "0.10", "0.15", "0.20", "0.25", "0.30");
    }

    public class UrgentSkillSettings
    {
        [DisplayName("XP-Bonus je Rang (Dringende Jobs)")]
        [Description("Standard: 0.30 (+30%) | Zusätzlicher XP-Bonus für dringende Eillieferungen (ab Rang 2 verfügbar).")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public SkillRankValues Xp { get; set; } = new SkillRankValues("0.30");

        [DisplayName("Umsatz-Bonus je Rang (Dringende Jobs)")]
        [Description("Standard: 0.05 bis 0.30 (+5% bis +30%) | Zusätzlicher Vergütungsbonus für dringende Eillieferungen.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public SkillRankValues Revenue { get; set; } = new SkillRankValues("0.05", "0.10", "0.15", "0.20", "0.25", "0.30");

        [DisplayName("Wichtige Jobs: XP-Anteil")]
        [Description("Standard: 0.66 (66%) | Welchen Anteil des Eillieferungs-XP-Bonus bereits 'Wichtige Lieferungen' (ab Rang 1) erhalten.")]
        public string ImportantJobXpBonusCoef { get; set; } = "0.66";

        [DisplayName("Wichtige Jobs: Umsatz-Anteil")]
        [Description("Standard: 0.60 (60%) | Welchen Anteil des Eillieferungs-Umsatzbonus bereits 'Wichtige Lieferungen' erhalten.")]
        public string ImportantJobRevenueBonusCoef { get; set; } = "0.6";
    }

    public class EcoDrivingSkillSettings
    {
        [DisplayName("Spritersparnis je Rang")]
        [Description("Standard: 0.10 bis 0.35 (10% bis 35%) | Prozentuale Treibstoffersparnis je Skill-Rang.\n• Hinweis: Wirkt sich stark auf KI-Fahrer und die einfache Verbrauchssimulation aus (für realistischeren Verbrauch z. B. auf 0.02 bis 0.12 senken).")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public SkillRankValues Saved { get; set; } = new SkillRankValues("0.10", "0.15", "0.20", "0.25", "0.30", "0.35");
    }

    public class AdrSkillSettings
    {
        [DisplayName("XP-Bonus je freigeschalteter ADR-Klasse")]
        [Description("Standard: 0.21 (+21%) | Zusätzlicher XP-Bonus beim Transport von Gefahrgut (Klassen: Explosiv, Gase, Brennbare Flüssigkeiten, Feststoffe, Giftig, Ätzend).")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public SkillRankValues Xp { get; set; } = new SkillRankValues("0.21");
    }

    // --- 3-rank value class for the new ETS2 1.62+ Bus skills ---
    public class BusSkillRankValues
    {
        [DisplayName("Rang 1")]
        public string Rank1 { get; set; } = "0.0";

        [DisplayName("Rang 2")]
        public string Rank2 { get; set; } = "0.0";

        [DisplayName("Rang 3")]
        public string Rank3 { get; set; } = "0.0";

        public BusSkillRankValues()
        {
        }

        public BusSkillRankValues(string r1, string r2, string r3)
        {
            Rank1 = r1;
            Rank2 = r2;
            Rank3 = r3;
        }
    }

    public class BusLongDutySkillSettings
    {
        [DisplayName("XP-Bonus je Rang")]
        [Description("Standard: 0.05 bis 0.15 (+5% bis +15%) | Zusätzlicher XP-Bonus für lange Buslinien mit mehreren Zwischenstopps auf Rang 1 bis 3.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public BusSkillRankValues Xp { get; set; } = new BusSkillRankValues("0.05", "0.10", "0.15");

        [DisplayName("Umsatz-Bonus (Revenue) je Rang")]
        [Description("Standard: 0.05 bis 0.15 (+5% bis +15%) | Zusätzlicher Einnahmen-Bonus für lange Buslinien je freigeschaltetem Rang.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public BusSkillRankValues Revenue { get; set; } = new BusSkillRankValues("0.05", "0.10", "0.15");

        [DisplayName("Max. Haltestellen pro Linie je Rang")]
        [Description("Standard: 4, 6, 8 Stopps | Maximale Anzahl an Haltestellen einer Buslinie, die auf dem jeweiligen Rang freigeschaltet wird.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public BusSkillRankValues MaxStops { get; set; } = new BusSkillRankValues("4", "6", "8");
    }

    public class BusTightScheduleSkillSettings
    {
        [DisplayName("XP-Bonus je Rang")]
        [Description("Standard: 0.10, 0.20, 0.40 (+10% bis +40%) | Zusätzlicher XP-Bonus für das Einhalten besonders straffer Busfahrpläne.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public BusSkillRankValues Xp { get; set; } = new BusSkillRankValues("0.1", "0.2", "0.4");

        [DisplayName("Fahrzeit-Verkürzung je Rang")]
        [Description("Standard: 0.05, 0.10, 0.15 (-5% bis -15%) | Um welchen Prozentsatz das vorgegebene Zeitfenster des Fahrplans je Rang knapper bemessen ist.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public BusSkillRankValues LessTime { get; set; } = new BusSkillRankValues("0.05", "0.1", "0.15");
    }

    public class BusSatisfactionDecreaseSkillSettings
    {
        [DisplayName("Umsatz-Bonus (Revenue) je Rang")]
        [Description("Standard: 0.10, 0.20, 0.30 (+10% bis +30%) | Zusätzlicher Einnahmen-Bonus für Busfahrten mit anspruchsvollen Passagieren.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public BusSkillRankValues Revenue { get; set; } = new BusSkillRankValues("0.1", "0.2", "0.3");

        [DisplayName("Reduzierung von Unzufriedenheit je Rang")]
        [Description("Standard: 0.20, 0.40, 0.60 (-20% bis -60%) | Dämpft den Abzug bei der Fahrgastzufriedenheit (z. B. bei Verspätungen oder kleineren Fehlern).")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public BusSkillRankValues NegativeSatisfactionReduction { get; set; } = new BusSkillRankValues("0.2", "0.4", "0.6");
    }

    public class BusLowAccelerationSkillSettings
    {
        [DisplayName("XP-Bonus je Rang")]
        [Description("Standard: 0.05, 0.10, 0.15 (+5% bis +15%) | Zusätzlicher XP-Bonus für besonders sanftes und gleichmäßiges Fahren.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public BusSkillRankValues Xp { get; set; } = new BusSkillRankValues("0.05", "0.10", "0.15");

        [DisplayName("Umsatz-Bonus (Revenue) je Rang")]
        [Description("Standard: 0.05, 0.10, 0.15 (+5% bis +15%) | Zusätzlicher Vergütungsbonus für sanften Fahrstil ohne ruckartiges Bremsen.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public BusSkillRankValues Revenue { get; set; } = new BusSkillRankValues("0.05", "0.10", "0.15");

        [DisplayName("Reduzierte G-Kraft-Empfindlichkeit je Rang")]
        [Description("Standard: 0.10, 0.20, 0.30 (-10% bis -30%) | Verringert, wie stark Passagiere auf starkes Bremsen, Beschleunigen oder enge Kurven reagieren.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public BusSkillRankValues SensitivityDecrease { get; set; } = new BusSkillRankValues("0.1", "0.2", "0.3");
    }

    public class BusSatisfactionIncreaseSkillSettings
    {
        [DisplayName("Steigerung positiver Zufriedenheit je Rang")]
        [Description("Standard: 0.10, 0.20, 0.30 (+10% bis +30%) | Erhöht den positiven Zufriedenheitsgewinn der Fahrgäste während einer angenehmen Fahrt.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public BusSkillRankValues PositiveSatisfactionIncrease { get; set; } = new BusSkillRankValues("0.1", "0.2", "0.3");
    }

    public class BusEcoDrivingSkillSettings
    {
        [DisplayName("Bus-Spritersparnis je Rang")]
        [Description("Standard: 0.10, 0.20, 0.30 (10% bis 30%) | Prozentuale Treibstoffersparnis beim Fahren von Reisebussen auf Rang 1 bis 3.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public BusSkillRankValues Saved { get; set; } = new BusSkillRankValues("0.10", "0.20", "0.30");
    }
}
