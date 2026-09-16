using System.ComponentModel;
using System.Text.RegularExpressions;

namespace ETS2_ModTool
{
    public class UsedVehicleSettings
    {
        [Category("Gebrauchtfahrzeuge (used_vehicle_assortment_config)")]
        [DisplayName("1. Generation & Angebot")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public UsedVehicleGenerationSettings Generation { get; set; } = new UsedVehicleGenerationSettings();

        [Category("Gebrauchtfahrzeuge (used_vehicle_assortment_config)")]
        [DisplayName("2. Kilometerstand (Odometer)")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public UsedVehicleOdometerSettings Odometer { get; set; } = new UsedVehicleOdometerSettings();

        [Category("Gebrauchtfahrzeuge (used_vehicle_assortment_config)")]
        [DisplayName("3. Reparierbarer Verschleiß")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public UsedVehicleWearSettings Wear { get; set; } = new UsedVehicleWearSettings();

        [Category("Gebrauchtfahrzeuge (used_vehicle_assortment_config)")]
        [DisplayName("4. Unreparierbarer Verschleiß")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public UsedVehicleUnfixableWearSettings UnfixableWear { get; set; } = new UsedVehicleUnfixableWearSettings();

        [Category("Gebrauchtfahrzeuge (used_vehicle_assortment_config)")]
        [DisplayName("5. Preisfaktoren & Spanne")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public UsedVehiclePriceSettings Price { get; set; } = new UsedVehiclePriceSettings();

        public void Import(string content)
        {
            // 1. Generation & Angebot
            Generation.TruckGenerationGameTimeMin = ExtractValue(content, "truck_generation_game_time_min", Generation.TruckGenerationGameTimeMin);
            Generation.TruckGenerationGameTimeMax = ExtractValue(content, "truck_generation_game_time_max", Generation.TruckGenerationGameTimeMax);
            Generation.TruckExpirationGameTimeMin = ExtractValue(content, "truck_expiration_game_time_min", Generation.TruckExpirationGameTimeMin);
            Generation.TruckExpirationGameTimeMax = ExtractValue(content, "truck_expiration_game_time_max", Generation.TruckExpirationGameTimeMax);
            Generation.TruckCountMin = ExtractValue(content, "truck_count_min", Generation.TruckCountMin);
            Generation.TruckCountMax = ExtractValue(content, "truck_count_max", Generation.TruckCountMax);

            // 2. Kilometerstand[cite: 6]
            Odometer.TruckOdometerLimitValues = ExtractArrayValue(content, "truck_odometer_limit_values", Odometer.TruckOdometerLimitValues);
            Odometer.TruckOdometerLimitLevels = ExtractArrayValue(content, "truck_odometer_limit_levels", Odometer.TruckOdometerLimitLevels);
            Odometer.TruckOdometerRangeCoef = ExtractValue(content, "truck_odometer_range_coef", Odometer.TruckOdometerRangeCoef);
            Odometer.DealerTruckOdometerMin = ExtractValue(content, "dealer_truck_odometer_min", Odometer.DealerTruckOdometerMin);
            Odometer.DealerTruckOdometerMax = ExtractValue(content, "dealer_truck_odometer_max", Odometer.DealerTruckOdometerMax);

            // 3. Reparierbarer Verschleiß[cite: 6]
            Wear.ChassisMin = ExtractValue(content, "truck_chassis_wear_min", Wear.ChassisMin);
            Wear.ChassisMax = ExtractValue(content, "truck_chassis_wear_max", Wear.ChassisMax);
            Wear.WheelsMin = ExtractValue(content, "truck_wheels_wear_min", Wear.WheelsMin);
            Wear.WheelsMax = ExtractValue(content, "truck_wheels_wear_max", Wear.WheelsMax);
            Wear.EngineMin = ExtractValue(content, "truck_engine_wear_min", Wear.EngineMin);
            Wear.EngineMax = ExtractValue(content, "truck_engine_wear_max", Wear.EngineMax);
            Wear.TransmissionMin = ExtractValue(content, "truck_transmission_wear_min", Wear.TransmissionMin);
            Wear.TransmissionMax = ExtractValue(content, "truck_transmission_wear_max", Wear.TransmissionMax);
            Wear.CabinMin = ExtractValue(content, "truck_cabin_wear_min", Wear.CabinMin);
            Wear.CabinMax = ExtractValue(content, "truck_cabin_wear_max", Wear.CabinMax);

            // 4. Unreparierbarer Verschleiß[cite: 6]
            UnfixableWear.LimitValues = ExtractArrayValue(content, "truck_wear_unfixable_limit_values", UnfixableWear.LimitValues);
            UnfixableWear.LimitLevels = ExtractArrayValue(content, "truck_wear_unfixable_limit_levels", UnfixableWear.LimitLevels);
            UnfixableWear.ChassisMin = ExtractValue(content, "truck_chassis_wear_unfixable_min", UnfixableWear.ChassisMin);
            UnfixableWear.ChassisMax = ExtractValue(content, "truck_chassis_wear_unfixable_max", UnfixableWear.ChassisMax);
            UnfixableWear.WheelsMin = ExtractValue(content, "truck_wheels_wear_unfixable_min", UnfixableWear.WheelsMin);
            UnfixableWear.WheelsMax = ExtractValue(content, "truck_wheels_wear_unfixable_max", UnfixableWear.WheelsMax);
            UnfixableWear.EngineMin = ExtractValue(content, "truck_engine_wear_unfixable_min", UnfixableWear.EngineMin);
            UnfixableWear.EngineMax = ExtractValue(content, "truck_engine_wear_unfixable_max", UnfixableWear.EngineMax);
            UnfixableWear.TransmissionMin = ExtractValue(content, "truck_transmission_wear_unfixable_min", UnfixableWear.TransmissionMin);
            UnfixableWear.TransmissionMax = ExtractValue(content, "truck_transmission_wear_unfixable_max", UnfixableWear.TransmissionMax);
            UnfixableWear.CabinMin = ExtractValue(content, "truck_cabin_wear_unfixable_min", UnfixableWear.CabinMin);
            UnfixableWear.CabinMax = ExtractValue(content, "truck_cabin_wear_unfixable_max", UnfixableWear.CabinMax);

            // 5. Preisfaktoren[cite: 6]
            Price.FactorIntegrityWear = ExtractValue(content, "truck_price_factor_integrity_wear", Price.FactorIntegrityWear);
            Price.FactorWearUnfixable = ExtractValue(content, "truck_price_factor_wear_unfixable", Price.FactorWearUnfixable);
            Price.FactorWear = ExtractValue(content, "truck_price_factor_wear", Price.FactorWear);
            Price.MinAccessoryPriceCoef = ExtractValue(content, "truck_price_minimum_accessory_price_coef", Price.MinAccessoryPriceCoef);
            Price.PriceRangeCoef = ExtractValue(content, "truck_price_range_coef", Price.PriceRangeCoef);
        }

        public void Export(ref string content)
        {
            // 1. Generation & Angebot schreiben[cite: 6]
            UpdateParameter(ref content, "truck_generation_game_time_min", Generation.TruckGenerationGameTimeMin);
            UpdateParameter(ref content, "truck_generation_game_time_max", Generation.TruckGenerationGameTimeMax);
            UpdateParameter(ref content, "truck_expiration_game_time_min", Generation.TruckExpirationGameTimeMin);
            UpdateParameter(ref content, "truck_expiration_game_time_max", Generation.TruckExpirationGameTimeMax);
            UpdateParameter(ref content, "truck_count_min", Generation.TruckCountMin);
            UpdateParameter(ref content, "truck_count_max", Generation.TruckCountMax);

            // 2. Kilometerstand schreiben[cite: 6]
            UpdateArrayParameter(ref content, "truck_odometer_limit_values", Odometer.TruckOdometerLimitValues);
            UpdateArrayParameter(ref content, "truck_odometer_limit_levels", Odometer.TruckOdometerLimitLevels);
            UpdateParameter(ref content, "truck_odometer_range_coef", Odometer.TruckOdometerRangeCoef);
            UpdateParameter(ref content, "dealer_truck_odometer_min", Odometer.DealerTruckOdometerMin);
            UpdateParameter(ref content, "dealer_truck_odometer_max", Odometer.DealerTruckOdometerMax);

            // 3. Reparierbarer Verschleiß schreiben[cite: 6]
            UpdateParameter(ref content, "truck_chassis_wear_min", Wear.ChassisMin);
            UpdateParameter(ref content, "truck_chassis_wear_max", Wear.ChassisMax);
            UpdateParameter(ref content, "truck_wheels_wear_min", Wear.WheelsMin);
            UpdateParameter(ref content, "truck_wheels_wear_max", Wear.WheelsMax);
            UpdateParameter(ref content, "truck_engine_wear_min", Wear.EngineMin);
            UpdateParameter(ref content, "truck_engine_wear_max", Wear.EngineMax);
            UpdateParameter(ref content, "truck_transmission_wear_min", Wear.TransmissionMin);
            UpdateParameter(ref content, "truck_transmission_wear_max", Wear.TransmissionMax);
            UpdateParameter(ref content, "truck_cabin_wear_min", Wear.CabinMin);
            UpdateParameter(ref content, "truck_cabin_wear_max", Wear.CabinMax);

            // 4. Unreparierbarer Verschleiß schreiben[cite: 6]
            UpdateArrayParameter(ref content, "truck_wear_unfixable_limit_values", UnfixableWear.LimitValues);
            UpdateArrayParameter(ref content, "truck_wear_unfixable_limit_levels", UnfixableWear.LimitLevels);
            UpdateParameter(ref content, "truck_chassis_wear_unfixable_min", UnfixableWear.ChassisMin);
            UpdateParameter(ref content, "truck_chassis_wear_unfixable_max", UnfixableWear.ChassisMax);
            UpdateParameter(ref content, "truck_wheels_wear_unfixable_min", UnfixableWear.WheelsMin);
            UpdateParameter(ref content, "truck_wheels_wear_unfixable_max", UnfixableWear.WheelsMax);
            UpdateParameter(ref content, "truck_engine_wear_unfixable_min", UnfixableWear.EngineMin);
            UpdateParameter(ref content, "truck_engine_wear_unfixable_max", UnfixableWear.EngineMax);
            UpdateParameter(ref content, "truck_transmission_wear_unfixable_min", UnfixableWear.TransmissionMin);
            UpdateParameter(ref content, "truck_transmission_wear_unfixable_max", UnfixableWear.TransmissionMax);
            UpdateParameter(ref content, "truck_cabin_wear_unfixable_min", UnfixableWear.CabinMin);
            UpdateParameter(ref content, "truck_cabin_wear_unfixable_max", UnfixableWear.CabinMax);

            // 5. Preisfaktoren schreiben[cite: 6]
            UpdateParameter(ref content, "truck_price_factor_integrity_wear", Price.FactorIntegrityWear);
            UpdateParameter(ref content, "truck_price_factor_wear_unfixable", Price.FactorWearUnfixable);
            UpdateParameter(ref content, "truck_price_factor_wear", Price.FactorWear);
            UpdateParameter(ref content, "truck_price_minimum_accessory_price_coef", Price.MinAccessoryPriceCoef);
            UpdateParameter(ref content, "truck_price_range_coef", Price.PriceRangeCoef);
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

        private string ExtractArrayValue(string content, string arrayName, string fallback)
        {
            Match m = new Regex($@"{arrayName}\[\]:\s*(?<value>[0-9eE.-]+)").Match(content);
            return m.Success ? m.Groups["value"].Value : fallback;
        }

        private void UpdateArrayParameter(ref string content, string arrayName, string val)
        {
            Regex r = new Regex($@"({arrayName}\[\]:\s*)[0-9eE.-]+");
            content = r.Replace(content, $"${{1}}{val}", 1);
        }
    }

    public class UsedVehicleGenerationSettings
    {
        [DisplayName("Generierungszeit Min (Minuten)")]
        [Description("Mindestzeit bis neue Angebote generiert werden (Standard: 1440 = 1 Tag)[cite: 6].")]
        public string TruckGenerationGameTimeMin { get; set; } = "1440";

        [DisplayName("Generierungszeit Max (Minuten)")]
        [Description("Maximalzeit bis neue Angebote generiert werden (Standard: 2160 = 1.5 Tage)[cite: 6].")]
        public string TruckGenerationGameTimeMax { get; set; } = "2160";

        [DisplayName("Ablaufzeit Min (Minuten)")]
        [Description("Mindestdauer bis ein Angebot verfällt (Standard: 2880 = 2 Tage)[cite: 6].")]
        public string TruckExpirationGameTimeMin { get; set; } = "2880";

        [DisplayName("Ablaufzeit Max (Minuten)")]
        [Description("Maximaldauer bis ein Angebot verfällt (Standard: 4320 = 3 Tage)[cite: 6].")]
        public string TruckExpirationGameTimeMax { get; set; } = "4320";

        [DisplayName("Fahrzeuganzahl Min")]
        [Description("Minimale Anzahl verfügbarer Gebrauchtfahrzeuge (Standard: 12)[cite: 6].")]
        public string TruckCountMin { get; set; } = "12";

        [DisplayName("Fahrzeuganzahl Max")]
        [Description("Maximale Anzahl verfügbarer Gebrauchtfahrzeuge (Standard: 18)[cite: 6].")]
        public string TruckCountMax { get; set; } = "18";
    }

    public class UsedVehicleOdometerSettings
    {
        [DisplayName("Kilometer Limit-Wert")]
        [Description("Obergrenze des Kilometerzählers für die Stufe (Standard: 100000)[cite: 6].")]
        public string TruckOdometerLimitValues { get; set; } = "100000";

        [DisplayName("Kilometer Limit-Level")]
        [Description("Benötigtes Spielerlevel für das Odometer-Limit (Standard: 3)[cite: 6].")]
        public string TruckOdometerLimitLevels { get; set; } = "3";

        [DisplayName("Kilometer Schwankungskoeffizient")]
        [Description("Streuungsbereich der Kilometerstände (Standard: 0.9)[cite: 6].")]
        public string TruckOdometerRangeCoef { get; set; } = "0.9";

        [DisplayName("Händler Min. Kilometer")]
        [Description("Minimaler Kilometerstand beim Händler (Standard: 1000)[cite: 6].")]
        public string DealerTruckOdometerMin { get; set; } = "1000";

        [DisplayName("Händler Max. Kilometer")]
        [Description("Maximaler Kilometerstand beim Händler (Standard: 100000)[cite: 6].")]
        public string DealerTruckOdometerMax { get; set; } = "100000";
    }

    public class UsedVehicleWearSettings
    {
        [DisplayName("Chassis Verschleiß: Min")]
        [Description("Standard: 0.0[cite: 6]")]
        public string ChassisMin { get; set; } = "0.0";

        [DisplayName("Chassis Verschleiß: Max")]
        [Description("Standard: 0.0[cite: 6]")]
        public string ChassisMax { get; set; } = "0.0";

        [DisplayName("Räder Verschleiß: Min")]
        [Description("Standard: 0.0[cite: 6]")]
        public string WheelsMin { get; set; } = "0.0";

        [DisplayName("Räder Verschleiß: Max")]
        [Description("Standard: 0.0[cite: 6]")]
        public string WheelsMax { get; set; } = "0.0";

        [DisplayName("Motor Verschleiß: Min")]
        [Description("Standard: 0.0[cite: 6]")]
        public string EngineMin { get; set; } = "0.0";

        [DisplayName("Motor Verschleiß: Max")]
        [Description("Standard: 0.0[cite: 6]")]
        public string EngineMax { get; set; } = "0.0";

        [DisplayName("Getriebe Verschleiß: Min")]
        [Description("Standard: 0.0[cite: 6]")]
        public string TransmissionMin { get; set; } = "0.0";

        [DisplayName("Getriebe Verschleiß: Max")]
        [Description("Standard: 0.0[cite: 6]")]
        public string TransmissionMax { get; set; } = "0.0";

        [DisplayName("Kabine Verschleiß: Min")]
        [Description("Standard: 0.0[cite: 6]")]
        public string CabinMin { get; set; } = "0.0";

        [DisplayName("Kabine Verschleiß: Max")]
        [Description("Standard: 0.0[cite: 6]")]
        public string CabinMax { get; set; } = "0.0";
    }

    public class UsedVehicleUnfixableWearSettings
    {
        [DisplayName("Unreparierbar Limit-Wert")]
        [Description("Grenzwert des Dauerschadens (Standard: 0.15)[cite: 6].")]
        public string LimitValues { get; set; } = "0.15";

        [DisplayName("Unreparierbar Limit-Level")]
        [Description("Spielerlevel für den Dauerschaden-Grenzwert (Standard: 3)[cite: 6].")]
        public string LimitLevels { get; set; } = "3";

        [DisplayName("Chassis Dauerschaden: Min")]
        [Description("Standard: 0.0[cite: 6]")]
        public string ChassisMin { get; set; } = "0.0";

        [DisplayName("Chassis Dauerschaden: Max")]
        [Description("Standard: 0.4[cite: 6]")]
        public string ChassisMax { get; set; } = "0.4";

        [DisplayName("Räder Dauerschaden: Min")]
        [Description("Standard: 0.0[cite: 6]")]
        public string WheelsMin { get; set; } = "0.0";

        [DisplayName("Räder Dauerschaden: Max")]
        [Description("Standard: 0.4[cite: 6]")]
        public string WheelsMax { get; set; } = "0.4";

        [DisplayName("Motor Dauerschaden: Min")]
        [Description("Standard: 0.0[cite: 6]")]
        public string EngineMin { get; set; } = "0.0";

        [DisplayName("Motor Dauerschaden: Max")]
        [Description("Standard: 0.4[cite: 6]")]
        public string EngineMax { get; set; } = "0.4";

        [DisplayName("Getriebe Dauerschaden: Min")]
        [Description("Standard: 0.0[cite: 6]")]
        public string TransmissionMin { get; set; } = "0.0";

        [DisplayName("Getriebe Dauerschaden: Max")]
        [Description("Standard: 0.4[cite: 6]")]
        public string TransmissionMax { get; set; } = "0.4";

        [DisplayName("Kabine Dauerschaden: Min")]
        [Description("Standard: 0.0[cite: 6]")]
        public string CabinMin { get; set; } = "0.0";

        [DisplayName("Kabine Dauerschaden: Max")]
        [Description("Standard: 0.4[cite: 6]")]
        public string CabinMax { get; set; } = "0.4";
    }

    public class UsedVehiclePriceSettings
    {
        [DisplayName("Preisfaktor: Gesamtzustand")]
        [Description("Preiseinfluss der allgemeinen Fahrzeugabnutzung (Standard: 3.0)[cite: 6].")]
        public string FactorIntegrityWear { get; set; } = "3.0";

        [DisplayName("Preisfaktor: Dauerschaden")]
        [Description("Preiseinfluss unreparierbarer Schäden (Standard: 2.0)[cite: 6].")]
        public string FactorWearUnfixable { get; set; } = "2.0";

        [DisplayName("Preisfaktor: Normaler Verschleiß")]
        [Description("Preiseinfluss reparierbarer Schäden (Standard: 1.0)[cite: 6].")]
        public string FactorWear { get; set; } = "1.0";

        [DisplayName("Zubehör Mindestpreis-Koeffizient")]
        [Description("Mindestanteil des Zubehörpreises beim Verkauf (Standard: 0.1)[cite: 6].")]
        public string MinAccessoryPriceCoef { get; set; } = "0.1";

        [DisplayName("Preis-Streukoeffizient")]
        [Description("Preisvarianz der Angebote (Standard: 0.2)[cite: 6].")]
        public string PriceRangeCoef { get; set; } = "0.2";
    }
}