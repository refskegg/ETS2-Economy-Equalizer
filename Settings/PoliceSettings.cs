using System.ComponentModel;
using System.Text.RegularExpressions;

namespace ETS2_ModTool
{
    public class PoliceSettings
    {
        [Category("Polizei (police_data.sii)")]
        [DisplayName("1. Allgemeine Faktoren & Progression")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public PoliceGeneralSettings General { get; set; } = new PoliceGeneralSettings();

        [Category("Polizei (police_data.sii)")]
        [DisplayName("2. Strafbeträge (€)")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public PoliceFineAmountSettings FineAmounts { get; set; } = new PoliceFineAmountSettings();

        [Category("Polizei (police_data.sii)")]
        [DisplayName("3. Prüfverzögerung (Sekunden)")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public PoliceCheckDelaySettings CheckDelays { get; set; } = new PoliceCheckDelaySettings();

        [Category("Polizei (police_data.sii)")]
        [DisplayName("4. Prüfverzögerung Polizei vor Ort (Sekunden)")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public PolicePoliceCheckDelaySettings PoliceCheckDelays { get; set; } = new PolicePoliceCheckDelaySettings();

        [Category("Polizei (police_data.sii)")]
        [DisplayName("5. Verstoß-Wahrscheinlichkeit (0.0 - 1.0)")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public PoliceProbabilitySettings Probabilities { get; set; } = new PoliceProbabilitySettings();

        [Category("Polizei (police_data.sii)")]
        [DisplayName("6. Wahrscheinlichkeit bei Polizei vor Ort (0.0 - 1.0)")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public PolicePoliceProbabilitySettings PoliceProbabilities { get; set; } = new PolicePoliceProbabilitySettings();

        public void Import(string content)
        {
            // 1. Allgemeines & Progression[cite: 2]
            General.FineFactorBase = ExtractValue(content, "fine_factor_base", General.FineFactorBase);
            General.FineFactorStep = ExtractValue(content, "fine_factor_step", General.FineFactorStep);
            General.FineFactorLimit = ExtractValue(content, "fine_factor_limit", General.FineFactorLimit);
            General.FineAmountRounding = ExtractValue(content, "fine_amount_rounding", General.FineAmountRounding);
            General.FineOverspeedStep = ExtractValue(content, "fine_overspeed_step", General.FineOverspeedStep);
            General.FineOverspeedStepMultiplier = ExtractValue(content, "fine_overspeed_step_multiplier", General.FineOverspeedStepMultiplier);
            General.FineOverspeedMultiplierLimit = ExtractValue(content, "fine_overspeed_multiplier_limit", General.FineOverspeedMultiplierLimit);
            General.TimerDecreaseRate = ExtractValue(content, "timer_decrease_rate", General.TimerDecreaseRate);
            General.PoliceNearbyFineRate = ExtractValue(content, "police_nearby_fine_rate", General.PoliceNearbyFineRate);
            General.PoliceNearbyOffenceTimer = ExtractValue(content, "police_nearby_offence_timer", General.PoliceNearbyOffenceTimer);

            // 2. Strafbeträge[cite: 2]
            FineAmounts.CarCrash = ExtractArrayValue(content, "fine_amounts", 0, FineAmounts.CarCrash);
            FineAmounts.AvoidSleeping = ExtractArrayValue(content, "fine_amounts", 1, FineAmounts.AvoidSleeping);
            FineAmounts.WrongWay = ExtractArrayValue(content, "fine_amounts", 2, FineAmounts.WrongWay);
            FineAmounts.SpeedingCamera = ExtractArrayValue(content, "fine_amounts", 3, FineAmounts.SpeedingCamera);
            FineAmounts.NoLightsNight = ExtractArrayValue(content, "fine_amounts", 4, FineAmounts.NoLightsNight);
            FineAmounts.RedLights = ExtractArrayValue(content, "fine_amounts", 5, FineAmounts.RedLights);
            FineAmounts.Speeding = ExtractArrayValue(content, "fine_amounts", 6, FineAmounts.Speeding);
            FineAmounts.AvoidWeighing = ExtractArrayValue(content, "fine_amounts", 7, FineAmounts.AvoidWeighing);
            FineAmounts.IllegalTrailer = ExtractArrayValue(content, "fine_amounts", 8, FineAmounts.IllegalTrailer);
            FineAmounts.AvoidInspection = ExtractArrayValue(content, "fine_amounts", 9, FineAmounts.AvoidInspection);
            FineAmounts.IllegalBorderCrossing = ExtractArrayValue(content, "fine_amounts", 10, FineAmounts.IllegalBorderCrossing);
            FineAmounts.HardShoulderViolation = ExtractArrayValue(content, "fine_amounts", 11, FineAmounts.HardShoulderViolation);
            FineAmounts.DamagedVehicleUsage = ExtractArrayValue(content, "fine_amounts", 12, FineAmounts.DamagedVehicleUsage);
            FineAmounts.IllegalTrailerWeightStation = ExtractArrayValue(content, "fine_amounts", 13, FineAmounts.IllegalTrailerWeightStation);

            // 3. Prüfverzögerung[cite: 2]
            CheckDelays.CarCrash = ExtractArrayValue(content, "offence_check_delay", 0, CheckDelays.CarCrash);
            CheckDelays.AvoidSleeping = ExtractArrayValue(content, "offence_check_delay", 1, CheckDelays.AvoidSleeping);
            CheckDelays.WrongWay = ExtractArrayValue(content, "offence_check_delay", 2, CheckDelays.WrongWay);
            CheckDelays.SpeedingCamera = ExtractArrayValue(content, "offence_check_delay", 3, CheckDelays.SpeedingCamera);
            CheckDelays.NoLightsNight = ExtractArrayValue(content, "offence_check_delay", 4, CheckDelays.NoLightsNight);
            CheckDelays.RedLights = ExtractArrayValue(content, "offence_check_delay", 5, CheckDelays.RedLights);
            CheckDelays.Speeding = ExtractArrayValue(content, "offence_check_delay", 6, CheckDelays.Speeding);
            CheckDelays.AvoidWeighing = ExtractArrayValue(content, "offence_check_delay", 7, CheckDelays.AvoidWeighing);
            CheckDelays.IllegalTrailer = ExtractArrayValue(content, "offence_check_delay", 8, CheckDelays.IllegalTrailer);

            // 4. Prüfverzögerung Polizei vor Ort[cite: 2]
            PoliceCheckDelays.CarCrash = ExtractArrayValue(content, "offence_police_check_delay", 0, PoliceCheckDelays.CarCrash);
            PoliceCheckDelays.AvoidSleeping = ExtractArrayValue(content, "offence_police_check_delay", 1, PoliceCheckDelays.AvoidSleeping);
            PoliceCheckDelays.WrongWay = ExtractArrayValue(content, "offence_police_check_delay", 2, PoliceCheckDelays.WrongWay);
            PoliceCheckDelays.SpeedingCamera = ExtractArrayValue(content, "offence_police_check_delay", 3, PoliceCheckDelays.SpeedingCamera);
            PoliceCheckDelays.NoLightsNight = ExtractArrayValue(content, "offence_police_check_delay", 4, PoliceCheckDelays.NoLightsNight);
            PoliceCheckDelays.RedLights = ExtractArrayValue(content, "offence_police_check_delay", 5, PoliceCheckDelays.RedLights);
            PoliceCheckDelays.Speeding = ExtractArrayValue(content, "offence_police_check_delay", 6, PoliceCheckDelays.Speeding);
            PoliceCheckDelays.AvoidWeighing = ExtractArrayValue(content, "offence_police_check_delay", 7, PoliceCheckDelays.AvoidWeighing);
            PoliceCheckDelays.IllegalTrailer = ExtractArrayValue(content, "offence_police_check_delay", 8, PoliceCheckDelays.IllegalTrailer);

            // 5. Wahrscheinlichkeiten[cite: 2]
            Probabilities.CarCrash = ExtractArrayValue(content, "offence_probabilty", 0, Probabilities.CarCrash);
            Probabilities.AvoidSleeping = ExtractArrayValue(content, "offence_probabilty", 1, Probabilities.AvoidSleeping);
            Probabilities.WrongWay = ExtractArrayValue(content, "offence_probabilty", 2, Probabilities.WrongWay);
            Probabilities.SpeedingCamera = ExtractArrayValue(content, "offence_probabilty", 3, Probabilities.SpeedingCamera);
            Probabilities.NoLightsNight = ExtractArrayValue(content, "offence_probabilty", 4, Probabilities.NoLightsNight);
            Probabilities.RedLights = ExtractArrayValue(content, "offence_probabilty", 5, Probabilities.RedLights);
            Probabilities.Speeding = ExtractArrayValue(content, "offence_probabilty", 6, Probabilities.Speeding);
            Probabilities.AvoidWeighing = ExtractArrayValue(content, "offence_probabilty", 7, Probabilities.AvoidWeighing);
            Probabilities.IllegalTrailer = ExtractArrayValue(content, "offence_probabilty", 8, Probabilities.IllegalTrailer);
            Probabilities.AvoidInspection = ExtractArrayValue(content, "offence_probabilty", 9, Probabilities.AvoidInspection);
            Probabilities.IllegalBorderCrossing = ExtractArrayValue(content, "offence_probabilty", 10, Probabilities.IllegalBorderCrossing);
            Probabilities.HardShoulderViolation = ExtractArrayValue(content, "offence_probabilty", 11, Probabilities.HardShoulderViolation);
            Probabilities.DamagedVehicleUsage = ExtractArrayValue(content, "offence_probabilty", 12, Probabilities.DamagedVehicleUsage);
            Probabilities.IllegalTrailerWeightStation = ExtractArrayValue(content, "offence_probabilty", 13, Probabilities.IllegalTrailerWeightStation);

            // 6. Wahrscheinlichkeiten Polizei vor Ort[cite: 2]
            PoliceProbabilities.CarCrash = ExtractArrayValue(content, "offence_police_probabilty", 0, PoliceProbabilities.CarCrash);
            PoliceProbabilities.AvoidSleeping = ExtractArrayValue(content, "offence_police_probabilty", 1, PoliceProbabilities.AvoidSleeping);
            PoliceProbabilities.WrongWay = ExtractArrayValue(content, "offence_police_probabilty", 2, PoliceProbabilities.WrongWay);
            PoliceProbabilities.SpeedingCamera = ExtractArrayValue(content, "offence_police_probabilty", 3, PoliceProbabilities.SpeedingCamera);
            PoliceProbabilities.NoLightsNight = ExtractArrayValue(content, "offence_police_probabilty", 4, PoliceProbabilities.NoLightsNight);
            PoliceProbabilities.RedLights = ExtractArrayValue(content, "offence_police_probabilty", 5, PoliceProbabilities.RedLights);
            PoliceProbabilities.Speeding = ExtractArrayValue(content, "offence_police_probabilty", 6, PoliceProbabilities.Speeding);
            PoliceProbabilities.AvoidWeighing = ExtractArrayValue(content, "offence_police_probabilty", 7, PoliceProbabilities.AvoidWeighing);
            PoliceProbabilities.IllegalTrailer = ExtractArrayValue(content, "offence_police_probabilty", 8, PoliceProbabilities.IllegalTrailer);
            PoliceProbabilities.AvoidInspection = ExtractArrayValue(content, "offence_police_probabilty", 9, PoliceProbabilities.AvoidInspection);
            PoliceProbabilities.IllegalBorderCrossing = ExtractArrayValue(content, "offence_police_probabilty", 10, PoliceProbabilities.IllegalBorderCrossing);
            PoliceProbabilities.HardShoulderViolation = ExtractArrayValue(content, "offence_police_probabilty", 11, PoliceProbabilities.HardShoulderViolation);
            PoliceProbabilities.DamagedVehicleUsage = ExtractArrayValue(content, "offence_police_probabilty", 12, PoliceProbabilities.DamagedVehicleUsage);
            PoliceProbabilities.IllegalTrailerWeightStation = ExtractArrayValue(content, "offence_police_probabilty", 13, PoliceProbabilities.IllegalTrailerWeightStation);
        }

        public void Export(ref string content)
        {
            // 1. Allgemeines & Progression schreiben[cite: 2]
            UpdateParameter(ref content, "fine_factor_base", General.FineFactorBase);
            UpdateParameter(ref content, "fine_factor_step", General.FineFactorStep);
            UpdateParameter(ref content, "fine_factor_limit", General.FineFactorLimit);
            UpdateParameter(ref content, "fine_amount_rounding", General.FineAmountRounding);
            UpdateParameter(ref content, "fine_overspeed_step", General.FineOverspeedStep);
            UpdateParameter(ref content, "fine_overspeed_step_multiplier", General.FineOverspeedStepMultiplier);
            UpdateParameter(ref content, "fine_overspeed_multiplier_limit", General.FineOverspeedMultiplierLimit);
            UpdateParameter(ref content, "timer_decrease_rate", General.TimerDecreaseRate);
            UpdateParameter(ref content, "police_nearby_fine_rate", General.PoliceNearbyFineRate);
            UpdateParameter(ref content, "police_nearby_offence_timer", General.PoliceNearbyOffenceTimer);

            // 2. Strafbeträge schreiben[cite: 2]
            UpdateArrayParameter(ref content, "fine_amounts", 0, FineAmounts.CarCrash);
            UpdateArrayParameter(ref content, "fine_amounts", 1, FineAmounts.AvoidSleeping);
            UpdateArrayParameter(ref content, "fine_amounts", 2, FineAmounts.WrongWay);
            UpdateArrayParameter(ref content, "fine_amounts", 3, FineAmounts.SpeedingCamera);
            UpdateArrayParameter(ref content, "fine_amounts", 4, FineAmounts.NoLightsNight);
            UpdateArrayParameter(ref content, "fine_amounts", 5, FineAmounts.RedLights);
            UpdateArrayParameter(ref content, "fine_amounts", 6, FineAmounts.Speeding);
            UpdateArrayParameter(ref content, "fine_amounts", 7, FineAmounts.AvoidWeighing);
            UpdateArrayParameter(ref content, "fine_amounts", 8, FineAmounts.IllegalTrailer);
            UpdateArrayParameter(ref content, "fine_amounts", 9, FineAmounts.AvoidInspection);
            UpdateArrayParameter(ref content, "fine_amounts", 10, FineAmounts.IllegalBorderCrossing);
            UpdateArrayParameter(ref content, "fine_amounts", 11, FineAmounts.HardShoulderViolation);
            UpdateArrayParameter(ref content, "fine_amounts", 12, FineAmounts.DamagedVehicleUsage);
            UpdateArrayParameter(ref content, "fine_amounts", 13, FineAmounts.IllegalTrailerWeightStation);

            // 3. Prüfverzögerung schreiben[cite: 2]
            UpdateArrayParameter(ref content, "offence_check_delay", 0, CheckDelays.CarCrash);
            UpdateArrayParameter(ref content, "offence_check_delay", 1, CheckDelays.AvoidSleeping);
            UpdateArrayParameter(ref content, "offence_check_delay", 2, CheckDelays.WrongWay);
            UpdateArrayParameter(ref content, "offence_check_delay", 3, CheckDelays.SpeedingCamera);
            UpdateArrayParameter(ref content, "offence_check_delay", 4, CheckDelays.NoLightsNight);
            UpdateArrayParameter(ref content, "offence_check_delay", 5, CheckDelays.RedLights);
            UpdateArrayParameter(ref content, "offence_check_delay", 6, CheckDelays.Speeding);
            UpdateArrayParameter(ref content, "offence_check_delay", 7, CheckDelays.AvoidWeighing);
            UpdateArrayParameter(ref content, "offence_check_delay", 8, CheckDelays.IllegalTrailer);

            // 4. Prüfverzögerung Polizei vor Ort schreiben[cite: 2]
            UpdateArrayParameter(ref content, "offence_police_check_delay", 0, PoliceCheckDelays.CarCrash);
            UpdateArrayParameter(ref content, "offence_police_check_delay", 1, PoliceCheckDelays.AvoidSleeping);
            UpdateArrayParameter(ref content, "offence_police_check_delay", 2, PoliceCheckDelays.WrongWay);
            UpdateArrayParameter(ref content, "offence_police_check_delay", 3, PoliceCheckDelays.SpeedingCamera);
            UpdateArrayParameter(ref content, "offence_police_check_delay", 4, PoliceCheckDelays.NoLightsNight);
            UpdateArrayParameter(ref content, "offence_police_check_delay", 5, PoliceCheckDelays.RedLights);
            UpdateArrayParameter(ref content, "offence_police_check_delay", 6, PoliceCheckDelays.Speeding);
            UpdateArrayParameter(ref content, "offence_police_check_delay", 7, PoliceCheckDelays.AvoidWeighing);
            UpdateArrayParameter(ref content, "offence_police_check_delay", 8, PoliceCheckDelays.IllegalTrailer);

            // 5. Wahrscheinlichkeiten schreiben[cite: 2]
            UpdateArrayParameter(ref content, "offence_probabilty", 0, Probabilities.CarCrash);
            UpdateArrayParameter(ref content, "offence_probabilty", 1, Probabilities.AvoidSleeping);
            UpdateArrayParameter(ref content, "offence_probabilty", 2, Probabilities.WrongWay);
            UpdateArrayParameter(ref content, "offence_probabilty", 3, Probabilities.SpeedingCamera);
            UpdateArrayParameter(ref content, "offence_probabilty", 4, Probabilities.NoLightsNight);
            UpdateArrayParameter(ref content, "offence_probabilty", 5, Probabilities.RedLights);
            UpdateArrayParameter(ref content, "offence_probabilty", 6, Probabilities.Speeding);
            UpdateArrayParameter(ref content, "offence_probabilty", 7, Probabilities.AvoidWeighing);
            UpdateArrayParameter(ref content, "offence_probabilty", 8, Probabilities.IllegalTrailer);
            UpdateArrayParameter(ref content, "offence_probabilty", 9, Probabilities.AvoidInspection);
            UpdateArrayParameter(ref content, "offence_probabilty", 10, Probabilities.IllegalBorderCrossing);
            UpdateArrayParameter(ref content, "offence_probabilty", 11, Probabilities.HardShoulderViolation);
            UpdateArrayParameter(ref content, "offence_probabilty", 12, Probabilities.DamagedVehicleUsage);
            UpdateArrayParameter(ref content, "offence_probabilty", 13, Probabilities.IllegalTrailerWeightStation);

            // 6. Wahrscheinlichkeiten Polizei vor Ort schreiben[cite: 2]
            UpdateArrayParameter(ref content, "offence_police_probabilty", 0, PoliceProbabilities.CarCrash);
            UpdateArrayParameter(ref content, "offence_police_probabilty", 1, PoliceProbabilities.AvoidSleeping);
            UpdateArrayParameter(ref content, "offence_police_probabilty", 2, PoliceProbabilities.WrongWay);
            UpdateArrayParameter(ref content, "offence_police_probabilty", 3, PoliceProbabilities.SpeedingCamera);
            UpdateArrayParameter(ref content, "offence_police_probabilty", 4, PoliceProbabilities.NoLightsNight);
            UpdateArrayParameter(ref content, "offence_police_probabilty", 5, PoliceProbabilities.RedLights);
            UpdateArrayParameter(ref content, "offence_police_probabilty", 6, PoliceProbabilities.Speeding);
            UpdateArrayParameter(ref content, "offence_police_probabilty", 7, PoliceProbabilities.AvoidWeighing);
            UpdateArrayParameter(ref content, "offence_police_probabilty", 8, PoliceProbabilities.IllegalTrailer);
            UpdateArrayParameter(ref content, "offence_police_probabilty", 9, PoliceProbabilities.AvoidInspection);
            UpdateArrayParameter(ref content, "offence_police_probabilty", 10, PoliceProbabilities.IllegalBorderCrossing);
            UpdateArrayParameter(ref content, "offence_police_probabilty", 11, PoliceProbabilities.HardShoulderViolation);
            UpdateArrayParameter(ref content, "offence_police_probabilty", 12, PoliceProbabilities.DamagedVehicleUsage);
            UpdateArrayParameter(ref content, "offence_police_probabilty", 13, PoliceProbabilities.IllegalTrailerWeightStation);
        }

        private string ExtractValue(string content, string paramName, string fallback)
        {
            Match m = new Regex($@"(?<!\[\d+\])\b{paramName}:\s*(?<value>[0-9eE.-]+)").Match(content);
            return m.Success ? m.Groups["value"].Value : fallback;
        }

        private void UpdateParameter(ref string content, string paramName, string val)
        {
            Regex r = new Regex($@"(\b{paramName}:\s*)[0-9eE.-]+");
            content = r.Replace(content, $"${{1}}{val}");
        }

        private string ExtractArrayValue(string content, string arrayName, int index, string fallback)
        {
            Match m = new Regex($@"{arrayName}\[{index}\]:\s*(?<value>[0-9eE.-]+)").Match(content);
            return m.Success ? m.Groups["value"].Value : fallback;
        }

        private void UpdateArrayParameter(ref string content, string arrayName, int index, string val)
        {
            Regex r = new Regex($@"({arrayName}\[{index}\]:\s*)[0-9eE.-]+");
            content = r.Replace(content, $"${{1}}{val}");
        }
    }

    public class PoliceGeneralSettings
    {
        [DisplayName("Strafen Basis-Faktor")]
        [Description("Start-Multiplikator für levelbasierte Strafen (Standard: 0.2)[cite: 2].")]
        public string FineFactorBase { get; set; } = "0.2";

        [DisplayName("Strafen Faktor-Schritt je Level")]
        [Description("Erhöhung des Faktors pro Spielerstufe (Standard: 0.08)[cite: 2].")]
        public string FineFactorStep { get; set; } = "0.08";

        [DisplayName("Strafen Faktor-Limit")]
        [Description("Maximaler Multiplikator für levelbasierte Strafen (Standard: 1.0)[cite: 2].")]
        public string FineFactorLimit { get; set; } = "1.0";

        [DisplayName("Rundung der Strafbeträge")]
        [Description("Rundet Strafen auf Vielfache auf (Standard: 20)[cite: 2].")]
        public string FineAmountRounding { get; set; } = "20";

        [DisplayName("Geschwindigkeits-Schritt")]
        [Description("Schrittweite für Geschwindigkeitsverstöße (Standard: 2.777778)[cite: 2].")]
        public string FineOverspeedStep { get; set; } = "2.777778";

        [DisplayName("Geschwindigkeits Multiplikator")]
        [Description("Progressiver Multiplikator je Schritt (Standard: 1.877)[cite: 2].")]
        public string FineOverspeedStepMultiplier { get; set; } = "1.877";

        [DisplayName("Geschwindigkeits Multiplikator-Limit")]
        [Description("Maximale Straferhöhung bei Raserei (Standard: 12.5)[cite: 2].")]
        public string FineOverspeedMultiplierLimit { get; set; } = "12.5";

        [DisplayName("Timer-Abbaurate (Sekunden)")]
        [Description("Reale Sekunden, um den Timer um 1 Sekunde abzubauen (Standard: 120 = 2 Min)[cite: 2].")]
        public string TimerDecreaseRate { get; set; } = "120";

        [DisplayName("Strafmultiplikator bei Polizei in Nähe")]
        [Description("Multiplikator, wenn Streifenwagen in der Nähe ist (Standard: 2)[cite: 2].")]
        public string PoliceNearbyFineRate { get; set; } = "2";

        [DisplayName("Polizei-Intervall vor Ort (Sekunden)")]
        [Description("Wie oft die anwesende Streife strafen darf (Standard: 15.0)[cite: 2].")]
        public string PoliceNearbyOffenceTimer { get; set; } = "15.0";
    }

    public class PoliceFineAmountSettings
    {
        [DisplayName("Unfall [0]")]
        [Description("Standard: 400 €[cite: 2]")]
        public string CarCrash { get; set; } = "400";

        [DisplayName("Müdigkeitsverstoß / Ruhezeit [1]")]
        [Description("Standard: 500 €[cite: 2]")]
        public string AvoidSleeping { get; set; } = "500";

        [DisplayName("Falsche Richtung / Geisterfahrer [2]")]
        [Description("Standard: 100 €[cite: 2]")]
        public string WrongWay { get; set; } = "100";

        [DisplayName("Blitzer / Geschwindigkeitskamera [3]")]
        [Description("Standard: 200 €[cite: 2]")]
        public string SpeedingCamera { get; set; } = "200";

        [DisplayName("Fahren ohne Licht bei Nacht [4]")]
        [Description("Standard: 150 €[cite: 2]")]
        public string NoLightsNight { get; set; } = "150";

        [DisplayName("Rote Ampel [5]")]
        [Description("Standard: 350 €[cite: 2]")]
        public string RedLights { get; set; } = "350";

        [DisplayName("Geschwindigkeitsübertretung allgemein [6]")]
        [Description("Standard: 200 €[cite: 2]")]
        public string Speeding { get; set; } = "200";

        [DisplayName("Wiegestation umgangen [7]")]
        [Description("Standard: 700 €[cite: 2]")]
        public string AvoidWeighing { get; set; } = "700";

        [DisplayName("Illegaler Anhänger [8]")]
        [Description("Standard: 1500 €[cite: 2]")]
        public string IllegalTrailer { get; set; } = "1500";

        [DisplayName("Inspektion umgangen [9]")]
        [Description("Standard: 900 €[cite: 2]")]
        public string AvoidInspection { get; set; } = "900";

        [DisplayName("Illegaler Grenzübergang [10]")]
        [Description("Standard: 2000 €[cite: 2]")]
        public string IllegalBorderCrossing { get; set; } = "2000";

        [DisplayName("Standstreifen blockiert / genutzt [11]")]
        [Description("Standard: 150 €[cite: 2]")]
        public string HardShoulderViolation { get; set; } = "150";

        [DisplayName("Fahrt mit beschädigtem Fahrzeug [12]")]
        [Description("Standard: 500 €[cite: 2]")]
        public string DamagedVehicleUsage { get; set; } = "500";

        [DisplayName("Illegaler Anhänger an Wiegestation [13]")]
        [Description("Standard: 1500 €[cite: 2]")]
        public string IllegalTrailerWeightStation { get; set; } = "1500";
    }

    public class PoliceCheckDelaySettings
    {
        [DisplayName("Unfall [0]")]
        [Description("Verzögerung in Sekunden (Standard: 0)[cite: 2].")]
        public string CarCrash { get; set; } = "0";

        [DisplayName("Müdigkeitsverstoß [1]")]
        [Description("Verzögerung in Sekunden (Standard: 60)[cite: 2].")]
        public string AvoidSleeping { get; set; } = "60";

        [DisplayName("Falsche Richtung [2]")]
        [Description("Verzögerung in Sekunden (Standard: 15)[cite: 2].")]
        public string WrongWay { get; set; } = "15";

        [DisplayName("Blitzer [3]")]
        [Description("Verzögerung in Sekunden (Standard: 0)[cite: 2].")]
        public string SpeedingCamera { get; set; } = "0";

        [DisplayName("Ohne Licht bei Nacht [4]")]
        [Description("Verzögerung in Sekunden (Standard: 70)[cite: 2].")]
        public string NoLightsNight { get; set; } = "70";

        [DisplayName("Rote Ampel [5]")]
        [Description("Verzögerung in Sekunden (Standard: 0)[cite: 2].")]
        public string RedLights { get; set; } = "0";

        [DisplayName("Geschwindigkeit [6]")]
        [Description("Verzögerung in Sekunden (Standard: 60)[cite: 2].")]
        public string Speeding { get; set; } = "60";

        [DisplayName("Wiegestation umgehen [7]")]
        [Description("Verzögerung in Sekunden (Standard: 0)[cite: 2].")]
        public string AvoidWeighing { get; set; } = "0";

        [DisplayName("Illegaler Anhänger [8]")]
        [Description("Verzögerung in Sekunden (Standard: 600)[cite: 2].")]
        public string IllegalTrailer { get; set; } = "600";
    }

    public class PolicePoliceCheckDelaySettings
    {
        [DisplayName("Unfall vor Streife [0]")]
        [Description("Standard: 0 Sekunden[cite: 2]")]
        public string CarCrash { get; set; } = "0";

        [DisplayName("Müdigkeitsverstoß vor Streife [1]")]
        [Description("Standard: 30 Sekunden[cite: 2]")]
        public string AvoidSleeping { get; set; } = "30";

        [DisplayName("Falsche Richtung vor Streife [2]")]
        [Description("Standard: 5 Sekunden[cite: 2]")]
        public string WrongWay { get; set; } = "5";

        [DisplayName("Blitzer vor Streife [3]")]
        [Description("Standard: 0 Sekunden[cite: 2]")]
        public string SpeedingCamera { get; set; } = "0";

        [DisplayName("Ohne Licht vor Streife [4]")]
        [Description("Standard: 7 Sekunden[cite: 2]")]
        public string NoLightsNight { get; set; } = "7";

        [DisplayName("Rote Ampel vor Streife [5]")]
        [Description("Standard: 0 Sekunden[cite: 2]")]
        public string RedLights { get; set; } = "0";

        [DisplayName("Geschwindigkeit vor Streife [6]")]
        [Description("Standard: 3 Sekunden[cite: 2]")]
        public string Speeding { get; set; } = "3";

        [DisplayName("Wiegestation vor Streife [7]")]
        [Description("Standard: 0 Sekunden[cite: 2]")]
        public string AvoidWeighing { get; set; } = "0";

        [DisplayName("Illegaler Anhänger vor Streife [8]")]
        [Description("Standard: 300 Sekunden[cite: 2]")]
        public string IllegalTrailer { get; set; } = "300";
    }

    public class PoliceProbabilitySettings
    {
        [DisplayName("Unfall [0]")]
        [Description("Wahrscheinlichkeit (Standard: 0.7)[cite: 2]")]
        public string CarCrash { get; set; } = "0.7";

        [DisplayName("Müdigkeitsverstoß [1]")]
        [Description("Wahrscheinlichkeit (Standard: 0.3)[cite: 2]")]
        public string AvoidSleeping { get; set; } = "0.3";

        [DisplayName("Falsche Richtung [2]")]
        [Description("Wahrscheinlichkeit (Standard: 0.6)[cite: 2]")]
        public string WrongWay { get; set; } = "0.6";

        [DisplayName("Blitzer [3]")]
        [Description("Wahrscheinlichkeit (Standard: 1.0)[cite: 2]")]
        public string SpeedingCamera { get; set; } = "1.0";

        [DisplayName("Ohne Licht [4]")]
        [Description("Wahrscheinlichkeit (Standard: 0.3)[cite: 2]")]
        public string NoLightsNight { get; set; } = "0.3";

        [DisplayName("Rote Ampel [5]")]
        [Description("Wahrscheinlichkeit (Standard: 0.7)[cite: 2]")]
        public string RedLights { get; set; } = "0.7";

        [DisplayName("Geschwindigkeit [6]")]
        [Description("Wahrscheinlichkeit (Standard: 0.0)[cite: 2]")]
        public string Speeding { get; set; } = "0.0";

        [DisplayName("Wiegestation umgehen [7]")]
        [Description("Wahrscheinlichkeit (Standard: 1.0)[cite: 2]")]
        public string AvoidWeighing { get; set; } = "1.0";

        [DisplayName("Illegaler Anhänger [8]")]
        [Description("Wahrscheinlichkeit (Standard: 0.0)[cite: 2]")]
        public string IllegalTrailer { get; set; } = "0.0";

        [DisplayName("Inspektion umgehen [9]")]
        [Description("Wahrscheinlichkeit (Standard: 1.0)[cite: 2]")]
        public string AvoidInspection { get; set; } = "1.0";

        [DisplayName("Illegaler Grenzübertritt [10]")]
        [Description("Wahrscheinlichkeit (Standard: 1.0)[cite: 2]")]
        public string IllegalBorderCrossing { get; set; } = "1.0";

        [DisplayName("Standstreifen [11]")]
        [Description("Wahrscheinlichkeit (Standard: 1.0)[cite: 2]")]
        public string HardShoulderViolation { get; set; } = "1.0";

        [DisplayName("Fahrzeug beschädigt [12]")]
        [Description("Wahrscheinlichkeit (Standard: 1.0)[cite: 2]")]
        public string DamagedVehicleUsage { get; set; } = "1.0";

        [DisplayName("Illegaler Anhänger Wiegestation [13]")]
        [Description("Wahrscheinlichkeit (Standard: 1.0)[cite: 2]")]
        public string IllegalTrailerWeightStation { get; set; } = "1.0";
    }

    public class PolicePoliceProbabilitySettings
    {
        [DisplayName("Unfall vor Streife [0]")]
        [Description("Wahrscheinlichkeit (Standard: 1.0)[cite: 2]")]
        public string CarCrash { get; set; } = "1.0";

        [DisplayName("Müdigkeitsverstoß vor Streife [1]")]
        [Description("Wahrscheinlichkeit (Standard: 0.3)[cite: 2]")]
        public string AvoidSleeping { get; set; } = "0.3";

        [DisplayName("Falsche Richtung vor Streife [2]")]
        [Description("Wahrscheinlichkeit (Standard: 0.7)[cite: 2]")]
        public string WrongWay { get; set; } = "0.7";

        [DisplayName("Blitzer vor Streife [3]")]
        [Description("Wahrscheinlichkeit (Standard: 1.0)[cite: 2]")]
        public string SpeedingCamera { get; set; } = "1.0";

        [DisplayName("Ohne Licht vor Streife [4]")]
        [Description("Wahrscheinlichkeit (Standard: 0.8)[cite: 2]")]
        public string NoLightsNight { get; set; } = "0.8";

        [DisplayName("Rote Ampel vor Streife [5]")]
        [Description("Wahrscheinlichkeit (Standard: 1.0)[cite: 2]")]
        public string RedLights { get; set; } = "1.0";

        [DisplayName("Geschwindigkeit vor Streife [6]")]
        [Description("Wahrscheinlichkeit (Standard: 0.9)[cite: 2]")]
        public string Speeding { get; set; } = "0.9";

        [DisplayName("Wiegestation vor Streife [7]")]
        [Description("Wahrscheinlichkeit (Standard: 1.0)[cite: 2]")]
        public string AvoidWeighing { get; set; } = "1.0";

        [DisplayName("Illegaler Anhänger vor Streife [8]")]
        [Description("Wahrscheinlichkeit (Standard: 0.3)[cite: 2]")]
        public string IllegalTrailer { get; set; } = "0.3";

        [DisplayName("Inspektion vor Streife [9]")]
        [Description("Wahrscheinlichkeit (Standard: 1.0)[cite: 2]")]
        public string AvoidInspection { get; set; } = "1.0";

        [DisplayName("Grenzübertritt vor Streife [10]")]
        [Description("Wahrscheinlichkeit (Standard: 1.0)[cite: 2]")]
        public string IllegalBorderCrossing { get; set; } = "1.0";

        [DisplayName("Standstreifen vor Streife [11]")]
        [Description("Wahrscheinlichkeit (Standard: 1.0)[cite: 2]")]
        public string HardShoulderViolation { get; set; } = "1.0";

        [DisplayName("Beschädigtes Fahrzeug vor Streife [12]")]
        [Description("Wahrscheinlichkeit (Standard: 1.0)[cite: 2]")]
        public string DamagedVehicleUsage { get; set; } = "1.0";

        [DisplayName("Illegaler Anhänger Wiegestation vor Streife [13]")]
        [Description("Wahrscheinlichkeit (Standard: 1.0)[cite: 2]")]
        public string IllegalTrailerWeightStation { get; set; } = "1.0";
    }
}