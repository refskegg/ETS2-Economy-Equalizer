using System.ComponentModel;
using System.Text.RegularExpressions;

namespace ETS2_ModTool
{
    public class UsedVehicleSettings
    {
        [Category("Gebrauchtfahrzeuge (used_vehicle_assortment_config)")]
        [DisplayName("1. Angebots-Generierung & Anzahl")]
        [Description("Steuert, wie oft neue gebrauchte LKWs beim Händler erscheinen und wie viele gleichzeitig verfügbar sind.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public UsedVehicleGenerationSettings Generation { get; set; } = new UsedVehicleGenerationSettings();

        [Category("Gebrauchtfahrzeuge (used_vehicle_assortment_config)")]
        [DisplayName("2. Kilometerstand (Odometer)")]
        [Description("Regeln für die Laufleistung (Kilometerzähler) der angebotenen Gebrauchtfahrzeuge.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public UsedVehicleOdometerSettings Odometer { get; set; } = new UsedVehicleOdometerSettings();

        [Category("Gebrauchtfahrzeuge (used_vehicle_assortment_config)")]
        [DisplayName("3. Reparierbarer Verschleiß beim Kauf")]
        [Description("Normaler Verschleiß (0.0 bis 1.0), den gebrauchte LKWs haben und der sich in der Werkstatt günstig reparieren lässt.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public UsedVehicleWearSettings Wear { get; set; } = new UsedVehicleWearSettings();

        [Category("Gebrauchtfahrzeuge (used_vehicle_assortment_config)")]
        [DisplayName("4. Unreparierbarer Dauerschaden beim Kauf")]
        [Description("Permanenter Verschleiß (0.0 bis 1.0) an Gebrauchtwagen, der nur durch Teiletausch oder Komplettrestaurierung verschwindet.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public UsedVehicleUnfixableWearSettings UnfixableWear { get; set; } = new UsedVehicleUnfixableWearSettings();

        [Category("Gebrauchtfahrzeuge (used_vehicle_assortment_config)")]
        [DisplayName("5. Preisfaktoren & Rabatte")]
        [Description("Wie stark Kilometerstand und Schäden den Kaufpreis eines gebrauchten LKWs drücken.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public UsedVehiclePriceSettings Price { get; set; } = new UsedVehiclePriceSettings();

        public void Import(string content)
        {
            // 1. Generation & Offers
            Generation.TruckGenerationGameTimeMin = ExtractValue(content, "truck_generation_game_time_min", Generation.TruckGenerationGameTimeMin);
            Generation.TruckGenerationGameTimeMax = ExtractValue(content, "truck_generation_game_time_max", Generation.TruckGenerationGameTimeMax);
            Generation.TruckExpirationGameTimeMin = ExtractValue(content, "truck_expiration_game_time_min", Generation.TruckExpirationGameTimeMin);
            Generation.TruckExpirationGameTimeMax = ExtractValue(content, "truck_expiration_game_time_max", Generation.TruckExpirationGameTimeMax);
            Generation.TruckCountMin = ExtractValue(content, "truck_count_min", Generation.TruckCountMin);
            Generation.TruckCountMax = ExtractValue(content, "truck_count_max", Generation.TruckCountMax);

            // 2. Odometer (Mileage)
            Odometer.TruckOdometerLimitValues = ExtractArrayValue(content, "truck_odometer_limit_values", Odometer.TruckOdometerLimitValues);
            Odometer.TruckOdometerLimitLevels = ExtractArrayValue(content, "truck_odometer_limit_levels", Odometer.TruckOdometerLimitLevels);
            Odometer.TruckOdometerRangeCoef = ExtractValue(content, "truck_odometer_range_coef", Odometer.TruckOdometerRangeCoef);
            Odometer.DealerTruckOdometerMin = ExtractValue(content, "dealer_truck_odometer_min", Odometer.DealerTruckOdometerMin);
            Odometer.DealerTruckOdometerMax = ExtractValue(content, "dealer_truck_odometer_max", Odometer.DealerTruckOdometerMax);

            // 3. Fixable Wear
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

            // 4. Unfixable Wear
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

            // 5. Price Factors
            Price.FactorIntegrityWear = ExtractValue(content, "truck_price_factor_integrity_wear", Price.FactorIntegrityWear);
            Price.FactorWearUnfixable = ExtractValue(content, "truck_price_factor_wear_unfixable", Price.FactorWearUnfixable);
            Price.FactorWear = ExtractValue(content, "truck_price_factor_wear", Price.FactorWear);
            Price.MinAccessoryPriceCoef = ExtractValue(content, "truck_price_minimum_accessory_price_coef", Price.MinAccessoryPriceCoef);
            Price.PriceRangeCoef = ExtractValue(content, "truck_price_range_coef", Price.PriceRangeCoef);
        }

        public void Export(ref string content)
        {
            // 1. Write generation & offers
            UpdateParameter(ref content, "truck_generation_game_time_min", Generation.TruckGenerationGameTimeMin);
            UpdateParameter(ref content, "truck_generation_game_time_max", Generation.TruckGenerationGameTimeMax);
            UpdateParameter(ref content, "truck_expiration_game_time_min", Generation.TruckExpirationGameTimeMin);
            UpdateParameter(ref content, "truck_expiration_game_time_max", Generation.TruckExpirationGameTimeMax);
            UpdateParameter(ref content, "truck_count_min", Generation.TruckCountMin);
            UpdateParameter(ref content, "truck_count_max", Generation.TruckCountMax);

            // 2. Write odometer (mileage)
            UpdateArrayParameter(ref content, "truck_odometer_limit_values", Odometer.TruckOdometerLimitValues);
            UpdateArrayParameter(ref content, "truck_odometer_limit_levels", Odometer.TruckOdometerLimitLevels);
            UpdateParameter(ref content, "truck_odometer_range_coef", Odometer.TruckOdometerRangeCoef);
            UpdateParameter(ref content, "dealer_truck_odometer_min", Odometer.DealerTruckOdometerMin);
            UpdateParameter(ref content, "dealer_truck_odometer_max", Odometer.DealerTruckOdometerMax);

            // 3. Write fixable wear
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

            // 4. Write unfixable wear
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

            // 5. Write price factors
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
        [Description("Standard: 1440 Min (24 Std) | Frühester Zeitpunkt in Spielminuten, nach dem neue Gebrauchtwagen-Angebote generiert werden.")]
        public string TruckGenerationGameTimeMin { get; set; } = "1440";

        [DisplayName("Generierungszeit Max (Minuten)")]
        [Description("Standard: 2160 Min (36 Std) | Spätester Zeitpunkt in Spielminuten, bis neue Gebrauchtwagen-Angebote erscheinen.")]
        public string TruckGenerationGameTimeMax { get; set; } = "2160";

        [DisplayName("Ablaufzeit Min (Minuten)")]
        [Description("Standard: 2880 Min (2 Tage) | Mindestdauer in Spielminuten, die ein Gebrauchtwagen beim Händler im Angebot bleibt.")]
        public string TruckExpirationGameTimeMin { get; set; } = "2880";

        [DisplayName("Ablaufzeit Max (Minuten)")]
        [Description("Standard: 4320 Min (3 Tage) | Maximaldauer in Spielminuten, bevor ein Angebot wieder vom Markt verschwindet.")]
        public string TruckExpirationGameTimeMax { get; set; } = "4320";

        [DisplayName("Fahrzeuganzahl Min")]
        [Description("Standard: 12 LKWs | Minimale Anzahl gleichzeitig angebotener Gebrauchtfahrzeuge beim Händler.")]
        public string TruckCountMin { get; set; } = "12";

        [DisplayName("Fahrzeuganzahl Max")]
        [Description("Standard: 18 LKWs | Maximale Anzahl gleichzeitig angebotener Gebrauchtfahrzeuge (Tipp: z. B. auf 25–30 erhöhen für größere Auswahl!).")]
        public string TruckCountMax { get; set; } = "18";
    }

    public class UsedVehicleOdometerSettings
    {
        [DisplayName("Kilometer-Limit für Anfänger (km)")]
        [Description("Standard: 100.000 km | Maximale Laufleistung der angebotenen Gebrauchtwagen für Spieler unterhalb des Limit-Levels.")]
        public string TruckOdometerLimitValues { get; set; } = "100000";

        [DisplayName("Spielerlevel für Kilometer-Limit")]
        [Description("Standard: Level 3 | Bis zu diesem Spielerlevel gilt die oben genannte Kilometer-Schonfrist.")]
        public string TruckOdometerLimitLevels { get; set; } = "3";

        [DisplayName("Kilometer-Streufaktor")]
        [Description("Standard: 0.9 (90%) | Bestimmt, wie breit die Kilometerstände der Angebote streuen.")]
        public string TruckOdometerRangeCoef { get; set; } = "0.9";

        [DisplayName("Händler Min. Kilometerstand (km)")]
        [Description("Standard: 1.000 km | Niedrigster möglicher Tachostand eines Gebrauchtwagens (quasi Jahres-/Vorführwagen).")]
        public string DealerTruckOdometerMin { get; set; } = "1000";

        [DisplayName("Händler Max. Kilometerstand (km)")]
        [Description("Standard: 100.000 km | Bezugswert für die Obergrenze der Kilometerberechnung beim Gebrauchtwagenhändler.")]
        public string DealerTruckOdometerMax { get; set; } = "100000";
    }

    public class UsedVehicleWearSettings
    {
        [DisplayName("Chassis Verschleiß: Min")]
        [Description("Standard: 0.0 (0%) | Minimaler normaler (günstig reparierbarer) Fahrgestell-Verschleiß beim Kauf.")]
        public string ChassisMin { get; set; } = "0.0";

        [DisplayName("Chassis Verschleiß: Max")]
        [Description("Standard: 0.0 (0%) | Maximaler normaler Fahrgestell-Verschleiß beim Kauf (z. B. 0.2 = bis zu 20%).")]
        public string ChassisMax { get; set; } = "0.0";

        [DisplayName("Räder Verschleiß: Min")]
        [Description("Standard: 0.0 (0%) | Minimaler normaler Reifenverschleiß beim Kauf.")]
        public string WheelsMin { get; set; } = "0.0";

        [DisplayName("Räder Verschleiß: Max")]
        [Description("Standard: 0.0 (0%) | Maximaler normaler Reifenverschleiß beim Kauf.")]
        public string WheelsMax { get; set; } = "0.0";

        [DisplayName("Motor Verschleiß: Min")]
        [Description("Standard: 0.0 (0%) | Minimaler normaler Motorverschleiß beim Kauf.")]
        public string EngineMin { get; set; } = "0.0";

        [DisplayName("Motor Verschleiß: Max")]
        [Description("Standard: 0.0 (0%) | Maximaler normaler Motorverschleiß beim Kauf.")]
        public string EngineMax { get; set; } = "0.0";

        [DisplayName("Getriebe Verschleiß: Min")]
        [Description("Standard: 0.0 (0%) | Minimaler normaler Getriebeverschleiß beim Kauf.")]
        public string TransmissionMin { get; set; } = "0.0";

        [DisplayName("Getriebe Verschleiß: Max")]
        [Description("Standard: 0.0 (0%) | Maximaler normaler Getriebeverschleiß beim Kauf.")]
        public string TransmissionMax { get; set; } = "0.0";

        [DisplayName("Kabine Verschleiß: Min")]
        [Description("Standard: 0.0 (0%) | Minimaler normaler Kabinenverschleiß beim Kauf.")]
        public string CabinMin { get; set; } = "0.0";

        [DisplayName("Kabine Verschleiß: Max")]
        [Description("Standard: 0.0 (0%) | Maximaler normaler Kabinenverschleiß beim Kauf.")]
        public string CabinMax { get; set; } = "0.0";
    }

    public class UsedVehicleUnfixableWearSettings
    {
        [DisplayName("Dauerschaden-Limit für Anfänger")]
        [Description("Standard: 0.15 (15%) | Maximaler permanenter Verschleiß für Spieler unterhalb des Limit-Levels.")]
        public string LimitValues { get; set; } = "0.15";

        [DisplayName("Spielerlevel für Dauerschaden-Limit")]
        [Description("Standard: Level 3 | Ab diesem Level können auch stärker abgenutzte Gebrauchtwagen (bis zum Max-Wert unten) erscheinen.")]
        public string LimitLevels { get; set; } = "3";

        [DisplayName("Chassis Dauerschaden: Min")]
        [Description("Standard: 0.0 (0%) | Minimaler permanenter Verschleiß am Fahrgestell.")]
        public string ChassisMin { get; set; } = "0.0";

        [DisplayName("Chassis Dauerschaden: Max")]
        [Description("Standard: 0.4 (40%) | Maximaler permanenter Verschleiß am Fahrgestell (0.4 = bis zu 40% Dauerschaden).")]
        public string ChassisMax { get; set; } = "0.4";

        [DisplayName("Räder Dauerschaden: Min")]
        [Description("Standard: 0.0 (0%) | Minimaler permanenter Reifenverschleiß.")]
        public string WheelsMin { get; set; } = "0.0";

        [DisplayName("Räder Dauerschaden: Max")]
        [Description("Standard: 0.4 (40%) | Maximaler permanenter Reifenverschleiß.")]
        public string WheelsMax { get; set; } = "0.4";

        [DisplayName("Motor Dauerschaden: Min")]
        [Description("Standard: 0.0 (0%) | Minimaler permanenter Motorverschleiß.")]
        public string EngineMin { get; set; } = "0.0";

        [DisplayName("Motor Dauerschaden: Max")]
        [Description("Standard: 0.4 (40%) | Maximaler permanenter Motorverschleiß.")]
        public string EngineMax { get; set; } = "0.4";

        [DisplayName("Getriebe Dauerschaden: Min")]
        [Description("Standard: 0.0 (0%) | Minimaler permanenter Getriebeverschleiß.")]
        public string TransmissionMin { get; set; } = "0.0";

        [DisplayName("Getriebe Dauerschaden: Max")]
        [Description("Standard: 0.4 (40%) | Maximaler permanenter Getriebeverschleiß.")]
        public string TransmissionMax { get; set; } = "0.4";

        [DisplayName("Kabine Dauerschaden: Min")]
        [Description("Standard: 0.0 (0%) | Minimaler permanenter Kabinenverschleiß.")]
        public string CabinMin { get; set; } = "0.0";

        [DisplayName("Kabine Dauerschaden: Max")]
        [Description("Standard: 0.4 (40%) | Maximaler permanenter Kabinenverschleiß.")]
        public string CabinMax { get; set; } = "0.4";
    }

    public class UsedVehiclePriceSettings
    {
        [DisplayName("Preisfaktor: Gesamtzustand / Laufleistung")]
        [Description("Standard: 3.0 | Bestimmt, wie stark eine hohe Kilometerleistung den Preis des Gebrauchtwagens senkt (höherer Wert = günstigere Preise bei vielen Kilometern).")]
        public string FactorIntegrityWear { get; set; } = "3.0";

        [DisplayName("Preisfaktor: Permanenter Dauerschaden")]
        [Description("Standard: 2.0 | Bestimmt, wie stark unreparierbare Schäden den Kaufpreis rabattieren.")]
        public string FactorWearUnfixable { get; set; } = "2.0";

        [DisplayName("Preisfaktor: Normaler Verschleiß")]
        [Description("Standard: 1.0 | Bestimmt, wie stark reparierbare Schäden den Kaufpreis senken.")]
        public string FactorWear { get; set; } = "1.0";

        [DisplayName("Mindestpreis-Faktor für Bauteile")]
        [Description("Standard: 0.1 (10%) | Selbst ein völlig heruntergewirtschafteter LKW kostet mindestens diesen Bruchteil (10%) seines Neupreises.")]
        public string MinAccessoryPriceCoef { get; set; } = "0.1";

        [DisplayName("Zufällige Preis-Schwankung")]
        [Description("Standard: 0.2 (±20%) | Zufällige Preisstreuung nach oben/unten, damit identische Gebrauchtwagen unterschiedlich gute Schnäppchen sein können.")]
        public string PriceRangeCoef { get; set; } = "0.2";
    }
}
