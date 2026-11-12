using System.ComponentModel;
using System.Text.RegularExpressions;

namespace ETS2_ModTool
{
    public class PoliceSettings
    {
        [Category("Polizei (police_data.sii)")]
        [DisplayName("1. Allgemeine Faktoren & Progression")]
        [Description("Level-Skalierung der Strafen, progressive Blitzer-Berechnung und Streifenwagen-Multiplikatoren.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public PoliceGeneralSettings General { get; set; } = new PoliceGeneralSettings();

        [Category("Polizei (police_data.sii)")]
        [DisplayName("2. Strafbeträge (€)")]
        [Description("Basis-Bußgelder in Euro für jedes einzelne Verkehrsdelikt.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public PoliceFineAmountSettings FineAmounts { get; set; } = new PoliceFineAmountSettings();

        [Category("Polizei (police_data.sii)")]
        [DisplayName("3. Prüfverzögerung / Toleranzzeit (Sekunden)")]
        [Description("Wie viele Sekunden ein Verstoß andauern muss, bevor eine automatische Strafe ausgelöst wird.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public PoliceCheckDelaySettings CheckDelays { get; set; } = new PoliceCheckDelaySettings();

        [Category("Polizei (police_data.sii)")]
        [DisplayName("4. Prüfverzögerung bei Polizei vor Ort (Sekunden)")]
        [Description("Verkürzte Toleranzzeit in Sekunden, wenn sich ein Streifenwagen im Verkehr direkt neben dir befindet.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public PolicePoliceCheckDelaySettings PoliceCheckDelays { get; set; } = new PolicePoliceCheckDelaySettings();

        [Category("Polizei (police_data.sii)")]
        [DisplayName("5. Verstoß-Wahrscheinlichkeit ohne Streife (0.0 – 1.0)")]
        [Description("Chance (0.0 = 0%, 1.0 = 100%), dass ein Verstoß auch ganz ohne Polizeiauto in der Nähe bestraft wird.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public PoliceProbabilitySettings Probabilities { get; set; } = new PoliceProbabilitySettings();

        [Category("Polizei (police_data.sii)")]
        [DisplayName("6. Wahrscheinlichkeit bei Polizei vor Ort (0.0 – 1.0)")]
        [Description("Chance (0.0 = 0%, 1.0 = 100%), dass ein Streifenwagen im Verkehr deinen Verstoß bemerkt und ahndet.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public PolicePoliceProbabilitySettings PoliceProbabilities { get; set; } = new PolicePoliceProbabilitySettings();

        public void Import(string content)
        {
            // 1. General & Progression
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

            // 2. Fine Amounts
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

            // 3. Check Delays
            CheckDelays.CarCrash = ExtractArrayValue(content, "offence_check_delay", 0, CheckDelays.CarCrash);
            CheckDelays.AvoidSleeping = ExtractArrayValue(content, "offence_check_delay", 1, CheckDelays.AvoidSleeping);
            CheckDelays.WrongWay = ExtractArrayValue(content, "offence_check_delay", 2, CheckDelays.WrongWay);
            CheckDelays.SpeedingCamera = ExtractArrayValue(content, "offence_check_delay", 3, CheckDelays.SpeedingCamera);
            CheckDelays.NoLightsNight = ExtractArrayValue(content, "offence_check_delay", 4, CheckDelays.NoLightsNight);
            CheckDelays.RedLights = ExtractArrayValue(content, "offence_check_delay", 5, CheckDelays.RedLights);
            CheckDelays.Speeding = ExtractArrayValue(content, "offence_check_delay", 6, CheckDelays.Speeding);
            CheckDelays.AvoidWeighing = ExtractArrayValue(content, "offence_check_delay", 7, CheckDelays.AvoidWeighing);
            CheckDelays.IllegalTrailer = ExtractArrayValue(content, "offence_check_delay", 8, CheckDelays.IllegalTrailer);

            // 4. Check Delays with Police Nearby
            PoliceCheckDelays.CarCrash = ExtractArrayValue(content, "offence_police_check_delay", 0, PoliceCheckDelays.CarCrash);
            PoliceCheckDelays.AvoidSleeping = ExtractArrayValue(content, "offence_police_check_delay", 1, PoliceCheckDelays.AvoidSleeping);
            PoliceCheckDelays.WrongWay = ExtractArrayValue(content, "offence_police_check_delay", 2, PoliceCheckDelays.WrongWay);
            PoliceCheckDelays.SpeedingCamera = ExtractArrayValue(content, "offence_police_check_delay", 3, PoliceCheckDelays.SpeedingCamera);
            PoliceCheckDelays.NoLightsNight = ExtractArrayValue(content, "offence_police_check_delay", 4, PoliceCheckDelays.NoLightsNight);
            PoliceCheckDelays.RedLights = ExtractArrayValue(content, "offence_police_check_delay", 5, PoliceCheckDelays.RedLights);
            PoliceCheckDelays.Speeding = ExtractArrayValue(content, "offence_police_check_delay", 6, PoliceCheckDelays.Speeding);
            PoliceCheckDelays.AvoidWeighing = ExtractArrayValue(content, "offence_police_check_delay", 7, PoliceCheckDelays.AvoidWeighing);
            PoliceCheckDelays.IllegalTrailer = ExtractArrayValue(content, "offence_police_check_delay", 8, PoliceCheckDelays.IllegalTrailer);

            // 5. Probabilities
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

            // 6. Probabilities with Police Nearby
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
            // 1. Write general & progression
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

            // 2. Write fine amounts
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

            // 3. Write check delays
            UpdateArrayParameter(ref content, "offence_check_delay", 0, CheckDelays.CarCrash);
            UpdateArrayParameter(ref content, "offence_check_delay", 1, CheckDelays.AvoidSleeping);
            UpdateArrayParameter(ref content, "offence_check_delay", 2, CheckDelays.WrongWay);
            UpdateArrayParameter(ref content, "offence_check_delay", 3, CheckDelays.SpeedingCamera);
            UpdateArrayParameter(ref content, "offence_check_delay", 4, CheckDelays.NoLightsNight);
            UpdateArrayParameter(ref content, "offence_check_delay", 5, CheckDelays.RedLights);
            UpdateArrayParameter(ref content, "offence_check_delay", 6, CheckDelays.Speeding);
            UpdateArrayParameter(ref content, "offence_check_delay", 7, CheckDelays.AvoidWeighing);
            UpdateArrayParameter(ref content, "offence_check_delay", 8, CheckDelays.IllegalTrailer);

            // 4. Write check delays with police nearby
            UpdateArrayParameter(ref content, "offence_police_check_delay", 0, PoliceCheckDelays.CarCrash);
            UpdateArrayParameter(ref content, "offence_police_check_delay", 1, PoliceCheckDelays.AvoidSleeping);
            UpdateArrayParameter(ref content, "offence_police_check_delay", 2, PoliceCheckDelays.WrongWay);
            UpdateArrayParameter(ref content, "offence_police_check_delay", 3, PoliceCheckDelays.SpeedingCamera);
            UpdateArrayParameter(ref content, "offence_police_check_delay", 4, PoliceCheckDelays.NoLightsNight);
            UpdateArrayParameter(ref content, "offence_police_check_delay", 5, PoliceCheckDelays.RedLights);
            UpdateArrayParameter(ref content, "offence_police_check_delay", 6, PoliceCheckDelays.Speeding);
            UpdateArrayParameter(ref content, "offence_police_check_delay", 7, PoliceCheckDelays.AvoidWeighing);
            UpdateArrayParameter(ref content, "offence_police_check_delay", 8, PoliceCheckDelays.IllegalTrailer);

            // 5. Write probabilities
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
            UpdateArrayParameter(ref content, "offence_probabilty", 13, FineAmounts.IllegalTrailerWeightStation != null ? Probabilities.IllegalTrailerWeightStation : "1.0");

            // 6. Write probabilities with police nearby
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
        [DisplayName("Strafen Basis-Faktor (Level 0)")]
        [Description("Standard: 0.2 (20%) | Auf Spielerlevel 0 zahlst du nur diesen Bruchteil der eigentlichen Bußgelder.\n• Tipp: Setze Basis-Faktor = 1.0, Schritt = 0.0 und Limit = 1.0, wenn Bußgelder ab Tag 1 immer exakt den vollen Euro-Betrag kosten sollen.")]
        public string FineFactorBase { get; set; } = "0.2";

        [DisplayName("Strafen Faktor-Anstieg je Level")]
        [Description("Standard: 0.08 (+8% je Level) | Um wie viel die Bußgelder mit jedem erreichten Spielerlevel teurer werden.")]
        public string FineFactorStep { get; set; } = "0.08";

        [DisplayName("Strafen Faktor-Limit (Maximum)")]
        [Description("Standard: 1.0 (100%) | Höchster Level-Multiplikator für Bußgelder (bei Standardwerten ab Level 10 erreicht).")]
        public string FineFactorLimit { get; set; } = "1.0";

        [DisplayName("Rundung der Strafbeträge (€)")]
        [Description("Standard: 20 € | Berechnete Bußgelder werden immer auf ein glattes Vielfaches dieses Wertes gerundet (z. B. 10 € oder 20 €).")]
        public string FineAmountRounding { get; set; } = "20";

        [DisplayName("Geschwindigkeits-Stufe (in m/s)")]
        [Description("Standard: 2.777778 m/s (= 10 km/h) | Abstand der Geschwindigkeitsstufen, ab denen das Bußgeld progressiv ansteigt.\n• Umrechnung: ETS2 rechnet intern in Metern pro Sekunde (m/s × 3,6 = km/h). 2.777778 m/s entsprechen genau 10 km/h.")]
        public string FineOverspeedStep { get; set; } = "2.777778";

        [DisplayName("Geschwindigkeits-Multiplikator je Stufe")]
        [Description("Standard: 1.877 | Mit jeder überschrittenen 10-km/h-Stufe wird das Blitzer-Bußgeld mit diesem Faktor potenziert.\n• Beispiel: Wer 30 km/h statt 10 km/h zu schnell fährt, zahlt dadurch deutlich mehr.")]
        public string FineOverspeedStepMultiplier { get; set; } = "1.877";

        [DisplayName("Geschwindigkeits-Multiplikator Limit")]
        [Description("Standard: 12.5 | Obergrenze für den progressiven Aufschlag bei extremen Geschwindigkeitsüberschreitungen.")]
        public string FineOverspeedMultiplierLimit { get; set; } = "12.5";

        [DisplayName("Verstoß-Timer Abbaurate (Sekunden)")]
        [Description("Standard: 120 Sek | Wie schnell der interne Zähler nach einem abgebrochenen Kurz-Verstoß (z. B. kurz im Gegenverkehr überholt) wieder auf 0 sinkt.")]
        public string TimerDecreaseRate { get; set; } = "120";

        [DisplayName("Aufschlag bei Polizei in Sichtweite")]
        [Description("Standard: 2 (Doppelte Strafe!) | Multiplikator auf das Bußgeld, wenn du ein Vergehen direkt vor den Augen eines Streifenwagens begehst (1 = kein Aufschlag).")]
        public string PoliceNearbyFineRate { get; set; } = "2";

        [DisplayName("Polizeikontroll-Abklingzeit (Sekunden)")]
        [Description("Standard: 15.0 Sek | Mindestabstand in Sekunden zwischen zwei Bußgeldern durch denselben Streifenwagen.")]
        public string PoliceNearbyOffenceTimer { get; set; } = "15.0";
    }

    public class PoliceFineAmountSettings
    {
        [DisplayName("Unfall mit KI-Fahrzeug")]
        [Description("Standard: 400 € | Basis-Bußgeld bei einer verschuldeten Kollision mit einem anderen Verkehrsteilnehmer.")]
        public string CarCrash { get; set; } = "400";

        [DisplayName("Lenkzeitüberschreitung / Müdigkeit")]
        [Description("Standard: 500 € | Basis-Bußgeld beim Ignorieren der vorgeschriebenen Ruhepause.")]
        public string AvoidSleeping { get; set; } = "500";

        [DisplayName("Fahren in Gegenrichtung (Geisterfahrer)")]
        [Description("Standard: 100 € | Basis-Bußgeld für das Befahren der Gegenfahrbahn.")]
        public string WrongWay { get; set; } = "100";

        [DisplayName("Stationärer Blitzer (Radarkamera)")]
        [Description("Standard: 200 € | Basis-Bußgeld am festen Blitzer (wird je nach Tempo-Überschreitung noch mit dem Geschwindigkeits-Multiplikator erhöht).")]
        public string SpeedingCamera { get; set; } = "200";

        [DisplayName("Fahren ohne Abblendlicht (Nacht/Regen)")]
        [Description("Standard: 150 € | Basis-Bußgeld, wenn du nachts oder im Tunnel ohne eingeschaltetes Licht fährst.")]
        public string NoLightsNight { get; set; } = "150";

        [DisplayName("Rote Ampel überfahren")]
        [Description("Standard: 350 € | Basis-Bußgeld beim Überfahren einer roten Ampel.")]
        public string RedLights { get; set; } = "350";

        [DisplayName("Geschwindigkeitsübertretung (Polizeistreife)")]
        [Description("Standard: 200 € | Basis-Bußgeld, wenn dich ein Streifenwagen beim Zu-schnell-Fahren erwischt.")]
        public string Speeding { get; set; } = "200";

        [DisplayName("Wiegestation ignoriert")]
        [Description("Standard: 700 € | Basis-Bußgeld, wenn du trotz Aufforderung an einer offenen Wiegekontrolle vorbeifährst.")]
        public string AvoidWeighing { get; set; } = "700";

        [DisplayName("Illegaler Anhänger im Land")]
        [Description("Standard: 1500 € | Basis-Bußgeld beim Ziehen eines im jeweiligen Land verbotenen Anhängertyps (z. B. HCT außerhalb Skandinaviens).")]
        public string IllegalTrailer { get; set; } = "1500";

        [DisplayName("Grenzkontrolle / Inspektion umgangen")]
        [Description("Standard: 900 € | Basis-Bußgeld beim Auslassen einer verpflichtenden Zoll-/Röntgenkontrolle an Grenzen.")]
        public string AvoidInspection { get; set; } = "900";

        [DisplayName("Illegaler Grenzübergang")]
        [Description("Standard: 2000 € | Basis-Bußgeld beim Durchfahren einer gesperrten Grenzspur ohne Abfertigung.")]
        public string IllegalBorderCrossing { get; set; } = "2000";

        [DisplayName("Befahren des Standstreifens")]
        [Description("Standard: 150 € | Basis-Bußgeld für das Überholen oder Fahren auf dem Pannenstreifen der Autobahn.")]
        public string HardShoulderViolation { get; set; } = "150";

        [DisplayName("Fahrt mit schwer beschädigtem LKW")]
        [Description("Standard: 500 € | Basis-Bußgeld bei Polizeikontrollen, wenn dein Fahrzeug erhebliche Schäden aufweist.")]
        public string DamagedVehicleUsage { get; set; } = "500";

        [DisplayName("Illegaler Anhänger an Wiegestation")]
        [Description("Standard: 1500 € | Basis-Bußgeld, wenn an einer Wiegestation ein unzulässiger Auflieger festgestellt wird.")]
        public string IllegalTrailerWeightStation { get; set; } = "1500";
    }

    public class PoliceCheckDelaySettings
    {
        [DisplayName("Unfall")]
        [Description("Standard: 0 Sek | Verzögerung bis zur Strafe (0 = wird sofort beim Aufprall geprüft).")]
        public string CarCrash { get; set; } = "0";

        [DisplayName("Müdigkeitsverstoß")]
        [Description("Standard: 60 Sek | Wie viele reale Sekunden du mit leerem Ruhezeit-Balken weiterfahren kannst, bevor die nächste Strafe geprüft wird.")]
        public string AvoidSleeping { get; set; } = "60";

        [DisplayName("Falsche Richtung")]
        [Description("Standard: 15 Sek | Wie viele Sekunden du auf der Gegenfahrbahn fahren darfst (z. B. zum Überholen), bevor eine Strafe droht.")]
        public string WrongWay { get; set; } = "15";

        [DisplayName("Blitzer")]
        [Description("Standard: 0 Sek | Blitzer lösen unmittelbar bei Durchfahrt aus.")]
        public string SpeedingCamera { get; set; } = "0";

        [DisplayName("Ohne Licht bei Nacht")]
        [Description("Standard: 70 Sek | Nach wie vielen Sekunden Dunkelheit ohne Licht eine Strafe ausgelöst wird.")]
        public string NoLightsNight { get; set; } = "70";

        [DisplayName("Rote Ampel")]
        [Description("Standard: 0 Sek | Wird sofort beim Überfahren der Haltelinie geprüft.")]
        public string RedLights { get; set; } = "0";

        [DisplayName("Geschwindigkeit")]
        [Description("Standard: 60 Sek | Zeitintervall in Sekunden für automatische Tempo-Prüfungen ohne Streife.")]
        public string Speeding { get; set; } = "60";

        [DisplayName("Wiegestation umgehen")]
        [Description("Standard: 0 Sek | Wird sofort beim Vorbeifahren an der Station ausgelöst.")]
        public string AvoidWeighing { get; set; } = "0";

        [DisplayName("Illegaler Anhänger")]
        [Description("Standard: 600 Sek (10 Min) | Zeitintervall zwischen zwei Prüfungen auf illegale Auflieger.")]
        public string IllegalTrailer { get; set; } = "600";
    }

    public class PolicePoliceCheckDelaySettings
    {
        [DisplayName("Unfall vor Streife")]
        [Description("Standard: 0 Sek | Sofortige Ahndung, wenn eine Polizeistreife den Unfall sieht.")]
        public string CarCrash { get; set; } = "0";

        [DisplayName("Müdigkeitsverstoß vor Streife")]
        [Description("Standard: 30 Sek | Prüfverzögerung in Sekunden, wenn du übermüdet neben einem Streifenwagen fährst.")]
        public string AvoidSleeping { get; set; } = "30";

        [DisplayName("Falsche Richtung vor Streife")]
        [Description("Standard: 5 Sek | Wie viele Sekunden du vor einer Polizeistreife im Gegenverkehr sein darfst.")]
        public string WrongWay { get; set; } = "5";

        [DisplayName("Blitzer vor Streife")]
        [Description("Standard: 0 Sek | Unmittelbare Auslösung.")]
        public string SpeedingCamera { get; set; } = "0";

        [DisplayName("Ohne Licht vor Streife")]
        [Description("Standard: 7 Sek | Nach 7 Sekunden Fahrt ohne Licht neben einer Streife gibt es ein Bußgeld.")]
        public string NoLightsNight { get; set; } = "7";

        [DisplayName("Rote Ampel vor Streife")]
        [Description("Standard: 0 Sek | Sofortige Strafe beim Rotlichtverstoß vor einem Streifenwagen.")]
        public string RedLights { get; set; } = "0";

        [DisplayName("Geschwindigkeit vor Streife")]
        [Description("Standard: 3 Sek | Wie viele Sekunden du neben einem Polizeiauto zu schnell fahren darfst, bevor es blitzt (Tipp: 6–8 Sek verhindert unfaire Strafen direkt nach Ortsschildern!).")]
        public string Speeding { get; set; } = "3";

        [DisplayName("Wiegestation vor Streife")]
        [Description("Standard: 0 Sek | Sofortige Strafe.")]
        public string AvoidWeighing { get; set; } = "0";

        [DisplayName("Illegaler Anhänger vor Streife")]
        [Description("Standard: 300 Sek (5 Min) | Verzögerung bei Fahrt mit unzulässigem Trailer neben einer Streife.")]
        public string IllegalTrailer { get; set; } = "300";
    }

    public class PoliceProbabilitySettings
    {
        [DisplayName("Unfall")]
        [Description("Standard: 0.7 (70%) | Wahrscheinlichkeit einer Strafe bei einem Unfall ohne Polizei in der Nähe.\n• Realismus-Tipp: Setze diesen Wert (und Ampel/Gegenverkehr/Licht) auf '0.0', damit du nur noch bestraft wirst, wenn wirklich eine Polizeistreife oder ein Blitzer in der Nähe ist!")]
        public string CarCrash { get; set; } = "0.7";

        [DisplayName("Müdigkeitsverstoß")]
        [Description("Standard: 0.3 (30%) | Wahrscheinlichkeit einer automatischen Strafe (digitaler Fahrtenschreiber) bei Lenkzeitüberschreitung.")]
        public string AvoidSleeping { get; set; } = "0.3";

        [DisplayName("Falsche Richtung")]
        [Description("Standard: 0.6 (60%) | Wahrscheinlichkeit einer automatischen Strafe im Gegenverkehr ohne Polizei vor Ort (0.0 = nur bei Polizei).")]
        public string WrongWay { get; set; } = "0.6";

        [DisplayName("Blitzer")]
        [Description("Standard: 1.0 (100%) | Wahrscheinlichkeit, dass ein aktiver Blitzerkasten tatsächlich auslöst (z. B. 0.85 = 15% der Blitzer sind Attrappen).")]
        public string SpeedingCamera { get; set; } = "1.0";

        [DisplayName("Ohne Licht")]
        [Description("Standard: 0.3 (30%) | Wahrscheinlichkeit einer Strafe für Fahren ohne Licht ohne Polizei in der Nähe (0.0 = nur bei Polizei).")]
        public string NoLightsNight { get; set; } = "0.3";

        [DisplayName("Rote Ampel")]
        [Description("Standard: 0.7 (70%) | Wahrscheinlichkeit einer Strafe an roten Ampeln ohne Polizei vor Ort (0.0 = keine unsichtbaren Ampelblitzer).")]
        public string RedLights { get; set; } = "0.7";

        [DisplayName("Geschwindigkeit (ohne Blitzer/Streife)")]
        [Description("Standard: 0.0 (0%) | Sollte auf 0.0 bleiben, damit man nicht auf freier Strecke ohne Blitzer und ohne Polizei geblitzt wird.")]
        public string Speeding { get; set; } = "0.0";

        [DisplayName("Wiegestation umgehen")]
        [Description("Standard: 1.0 (100%) | Wahrscheinlichkeit einer Strafe beim Ignorieren einer Wiegestation.")]
        public string AvoidWeighing { get; set; } = "1.0";

        [DisplayName("Illegaler Anhänger")]
        [Description("Standard: 0.0 (0%) | Wahrscheinlichkeit einer automatischen Strafe ohne Polizei/Wiegestation.")]
        public string IllegalTrailer { get; set; } = "0.0";

        [DisplayName("Inspektion umgehen")]
        [Description("Standard: 1.0 (100%) | Wahrscheinlichkeit einer Strafe beim Umgehen einer Grenzkontrolle.")]
        public string AvoidInspection { get; set; } = "1.0";

        [DisplayName("Illegaler Grenzübertritt")]
        [Description("Standard: 1.0 (100%) | Wahrscheinlichkeit einer Strafe an Grenzen.")]
        public string IllegalBorderCrossing { get; set; } = "1.0";

        [DisplayName("Standstreifen")]
        [Description("Standard: 1.0 (100%) | Wahrscheinlichkeit einer Strafe auf dem Pannenstreifen ohne Polizei vor Ort (0.0 = nur bei Polizei).")]
        public string HardShoulderViolation { get; set; } = "1.0";

        [DisplayName("Fahrzeug beschädigt")]
        [Description("Standard: 1.0 (100%) | Wahrscheinlichkeit einer Strafe bei stark beschädigtem LKW an Kontrollstellen.")]
        public string DamagedVehicleUsage { get; set; } = "1.0";

        [DisplayName("Illegaler Anhänger Wiegestation")]
        [Description("Standard: 1.0 (100%) | Wahrscheinlichkeit, dass ein illegaler Auflieger auf der Waage auffällt.")]
        public string IllegalTrailerWeightStation { get; set; } = "1.0";
    }

    public class PolicePoliceProbabilitySettings
    {
        [DisplayName("Unfall vor Streife")]
        [Description("Standard: 1.0 (100%) | Wahrscheinlichkeit einer Strafe, wenn ein Streifenwagen deinen Unfall direkt sieht.")]
        public string CarCrash { get; set; } = "1.0";

        [DisplayName("Müdigkeitsverstoß vor Streife")]
        [Description("Standard: 0.3 (30%) | Wahrscheinlichkeit, dass eine vorbeifahrende Streife deine Lenkzeit kontrolliert.")]
        public string AvoidSleeping { get; set; } = "0.3";

        [DisplayName("Falsche Richtung vor Streife")]
        [Description("Standard: 0.7 (70%) | Wahrscheinlichkeit einer Strafe bei Geisterfahrt vor einer Streife.")]
        public string WrongWay { get; set; } = "0.7";

        [DisplayName("Blitzer vor Streife")]
        [Description("Standard: 1.0 (100%) | Auslösewahrscheinlichkeit am Blitzer.")]
        public string SpeedingCamera { get; set; } = "1.0";

        [DisplayName("Ohne Licht vor Streife")]
        [Description("Standard: 0.8 (80%) | Wahrscheinlichkeit, dass eine Streife nachts dein fehlendes Licht ahndet.")]
        public string NoLightsNight { get; set; } = "0.8";

        [DisplayName("Rote Ampel vor Streife")]
        [Description("Standard: 1.0 (100%) | Wahrscheinlichkeit einer Strafe, wenn du vor einem Streifenwagen über Rot fährst.")]
        public string RedLights { get; set; } = "1.0";

        [DisplayName("Geschwindigkeit vor Streife")]
        [Description("Standard: 0.9 (90%) | Wahrscheinlichkeit, dass dich ein Streifenwagen im Verkehr bei überhöhtem Tempo blitzt.")]
        public string Speeding { get; set; } = "0.9";

        [DisplayName("Wiegestation vor Streife")]
        [Description("Standard: 1.0 (100%) | Ahndungswahrscheinlichkeit an der Waage.")]
        public string AvoidWeighing { get; set; } = "1.0";

        [DisplayName("Illegaler Anhänger vor Streife")]
        [Description("Standard: 0.3 (30%) | Wahrscheinlichkeit, dass einer Polizeistreife dein unzulässiger Auflieger auffällt.")]
        public string IllegalTrailer { get; set; } = "0.3";

        [DisplayName("Inspektion vor Streife")]
        [Description("Standard: 1.0 (100%) | Ahndungswahrscheinlichkeit bei umgangener Inspektion.")]
        public string AvoidInspection { get; set; } = "1.0";

        [DisplayName("Grenzübertritt vor Streife")]
        [Description("Standard: 1.0 (100%) | Ahndungswahrscheinlichkeit an der Grenze.")]
        public string IllegalBorderCrossing { get; set; } = "1.0";

        [DisplayName("Standstreifen vor Streife")]
        [Description("Standard: 1.0 (100%) | Wahrscheinlichkeit einer Strafe, wenn du vor der Polizei den Standstreifen nutzt.")]
        public string HardShoulderViolation { get; set; } = "1.0";

        [DisplayName("Beschädigtes Fahrzeug vor Streife")]
        [Description("Standard: 1.0 (100%) | Wahrscheinlichkeit, dass eine Streife deinen schwer beschädigten LKW beanstandet.")]
        public string DamagedVehicleUsage { get; set; } = "1.0";

        [DisplayName("Illegaler Anhänger Wiegestation vor Streife")]
        [Description("Standard: 1.0 (100%) | Ahndungswahrscheinlichkeit auf der Waage.")]
        public string IllegalTrailerWeightStation { get; set; } = "1.0";
    }
}
