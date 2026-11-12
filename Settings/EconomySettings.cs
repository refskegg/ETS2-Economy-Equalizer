using System.ComponentModel;
using System.Text.RegularExpressions;

namespace ETS2_ModTool
{
    public class EconomySettings
    {
        [Category("Wirtschaft (economy_data.sii)")]
        [DisplayName("1. Garagen & LKW-Kauf")]
        [Description("Preise für Garagen, Tankrabatte, Wiederverkaufswerte und Online-Händler-Freischaltung.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public EconomyGarageSettings Garage { get; set; } = new EconomyGarageSettings();

        [Category("Wirtschaft (economy_data.sii)")]
        [DisplayName("2. Fracht, Einnahmen & Strafen")]
        [Description("Die wichtigsten Parameter für deine Bezahlung pro Auftrag und Abzüge bei Frachtschäden.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public EconomyRevenueSettings Revenue { get; set; } = new EconomyRevenueSettings();

        [Category("Wirtschaft (economy_data.sii)")]
        [DisplayName("3. Zeitfenster & Lieferfristen")]
        [Description("Zeitpuffer für Standard-, wichtige und dringende Lieferungen sowie deren Vergütungsboni.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public EconomyDeliveryWindowSettings DeliveryWindows { get; set; } = new EconomyDeliveryWindowSettings();

        [Category("Wirtschaft (economy_data.sii)")]
        [DisplayName("4. Zeit, Ermüdung & Frachtverfügbarkeit")]
        [Description("Lenk- und Ruhezeiten (Müdigkeitssuche), Zeitsimulation und Lebensdauer von Frachtangeboten.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public EconomySimulationSettings Simulation { get; set; } = new EconomySimulationSettings();

        [Category("Wirtschaft (economy_data.sii)")]
        [DisplayName("5. Notdienst (Abschleppen, Sprit, Elektro)")]
        [Description("Kosten und Zeitstrafen für den F7-Abschleppdienst, Notbetankung und E-LKW-Notladung.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public EconomyServiceSettings Service { get; set; } = new EconomyServiceSettings();

        [Category("Wirtschaft (economy_data.sii)")]
        [DisplayName("6. KI-Fahrer Verhalten & Finanzen")]
        [Description("Gehalt, Einnahmen, Leerfahrten-Wahrscheinlichkeit und Kündigungsverhalten deiner angestellten Fahrer.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public EconomyDriverSettings Driver { get; set; } = new EconomyDriverSettings();

        [Category("Wirtschaft (economy_data.sii)")]
        [DisplayName("7. Erfahrungspunkte (XP) & Park-Boni")]
        [Description("XP-Vergabe pro Kilometer, beim Erkunden von Straßen und beim manuellen Einparken.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public EconomyXPSettings XP { get; set; } = new EconomyXPSettings();

        public void Import(string content)
        {
            // 1. Garages & Trucks
            Garage.TruckRefund = ExtractValue(content, "truck_refund", Garage.TruckRefund);
            Garage.PriceSmallGarage = ExtractValue(content, "price_small_garage", Garage.PriceSmallGarage);
            Garage.PriceGarageUpgrade = ExtractValue(content, "price_garage_upgrade", Garage.PriceGarageUpgrade);
            Garage.GarageProdPlanTiny = ExtractValue(content, "garage_prod_plan_tiny", Garage.GarageProdPlanTiny);
            Garage.GarageProdPlanSmall = ExtractValue(content, "garage_prod_plan_small", Garage.GarageProdPlanSmall);
            Garage.GarageProdPlanLarge = ExtractValue(content, "garage_prod_plan_large", Garage.GarageProdPlanLarge);
            Garage.FuelDiscountInGarage = ExtractValue(content, "fuel_discount_in_garage", Garage.FuelDiscountInGarage);
            Garage.TruckCountForOnline = ExtractValue(content, "truck_count_for_online", Garage.TruckCountForOnline);

            // 2. Cargo & Revenue
            Revenue.RevenuePerKmBase = ExtractValue(content, "revenue_per_km_base", Revenue.RevenuePerKmBase);
            Revenue.FixedRevenue = ExtractValue(content, "fixed_revenue", Revenue.FixedRevenue);
            Revenue.RevenueCoefPerKm = ExtractValue(content, "revenue_coef_per_km", Revenue.RevenueCoefPerKm);
            Revenue.CargoMarketRevenueCoefPerKm = ExtractValue(content, "cargo_market_revenue_coef_per_km", Revenue.CargoMarketRevenueCoefPerKm);
            Revenue.RewardBonusLevel = ExtractValue(content, "reward_bonus_level", Revenue.RewardBonusLevel);
            Revenue.AbandonedJobFine = ExtractValue(content, "abandoned_job_fine", Revenue.AbandonedJobFine);
            Revenue.CargoDamageCost = ExtractValue(content, "cargo_damage_cost", Revenue.CargoDamageCost);
            Revenue.CargoDamageCostFactor = ExtractValue(content, "cargo_damage_cost_factor", Revenue.CargoDamageCostFactor);

            // 3. Delivery Windows & Deadlines
            DeliveryWindows.LateDeliveryMaxOvertime0 = ExtractArrayValue(content, "late_delivery_max_overtime", 0, DeliveryWindows.LateDeliveryMaxOvertime0);
            DeliveryWindows.LateDeliveryMaxOvertime1 = ExtractArrayValue(content, "late_delivery_max_overtime", 1, DeliveryWindows.LateDeliveryMaxOvertime1);
            DeliveryWindows.LateDeliveryMaxOvertime2 = ExtractArrayValue(content, "late_delivery_max_overtime", 2, DeliveryWindows.LateDeliveryMaxOvertime2);
            DeliveryWindows.DeliveryWindow0 = ExtractArrayValue(content, "delivery_window", 0, DeliveryWindows.DeliveryWindow0);
            DeliveryWindows.DeliveryWindow1 = ExtractArrayValue(content, "delivery_window", 1, DeliveryWindows.DeliveryWindow1);
            DeliveryWindows.DeliveryWindow2 = ExtractArrayValue(content, "delivery_window", 2, DeliveryWindows.DeliveryWindow2);
            DeliveryWindows.DeliveryWindowCoef0 = ExtractArrayValue(content, "delivery_window_coef", 0, DeliveryWindows.DeliveryWindowCoef0);
            DeliveryWindows.DeliveryWindowCoef1 = ExtractArrayValue(content, "delivery_window_coef", 1, DeliveryWindows.DeliveryWindowCoef1);
            DeliveryWindows.DeliveryWindowCoef2 = ExtractArrayValue(content, "delivery_window_coef", 2, DeliveryWindows.DeliveryWindowCoef2);

            // 4. Time, Fatigue & Jobs
            Simulation.SimulationAvgSpeed = ExtractValue(content, "simulation_avg_speed", Simulation.SimulationAvgSpeed);
            Simulation.MaximumDrivingTime = ExtractValue(content, "maximum_driving_time", Simulation.MaximumDrivingTime);
            Simulation.SleepingTime = ExtractValue(content, "sleeping_time", Simulation.SleepingTime);
            Simulation.HurryUpTimer = ExtractValue(content, "hurry_up_timer", Simulation.HurryUpTimer);
            Simulation.NoCargoProb = ExtractValue(content, "no_cargo_prob", Simulation.NoCargoProb);
            Simulation.CargoValidityMin = ExtractValue(content, "cargo_validity_min", Simulation.CargoValidityMin);
            Simulation.CargoValidityMax = ExtractValue(content, "cargo_validity_max", Simulation.CargoValidityMax);

            // 5. Emergency Services & Refueling
            Service.TowPriceBase = ExtractValue(content, "tow_price_base", Service.TowPriceBase);
            Service.TowPriceFactor = ExtractValue(content, "tow_price_factor", Service.TowPriceFactor);
            Service.TowTimeBase = ExtractValue(content, "tow_time_base", Service.TowTimeBase);
            Service.TowTimeFactor = ExtractValue(content, "tow_time_factor", Service.TowTimeFactor);
            Service.TowFuelRatio = ExtractValue(content, "tow_fuel_ratio", Service.TowFuelRatio);
            Service.RefuelFuel = ExtractValue(content, "refuel_fuel", Service.RefuelFuel);
            Service.RefuelPriceBase = ExtractValue(content, "refuel_price_base", Service.RefuelPriceBase);
            Service.RefuelPriceFactor = ExtractValue(content, "refuel_price_factor", Service.RefuelPriceFactor);
            Service.RefuelTimeBase = ExtractValue(content, "refuel_time_base", Service.RefuelTimeBase);
            Service.RechargeEnergy = ExtractValue(content, "recharge_energy", Service.RechargeEnergy);
            Service.RechargePriceBase = ExtractValue(content, "recharge_price_base", Service.RechargePriceBase);
            Service.RechargePrice = ExtractValue(content, "recharge_price", Service.RechargePrice);
            Service.RechargeTimeBase = ExtractValue(content, "recharge_time_base", Service.RechargeTimeBase);

            // 6. AI Drivers
            Driver.MinimalDriverSalary = ExtractValue(content, "minimal_driver_salary", Driver.MinimalDriverSalary);
            Driver.DriverRevenueCoefPerKm = ExtractValue(content, "driver_revenue_coef_per_km", Driver.DriverRevenueCoefPerKm);
            Driver.DriverCargoMarketRevenueCoefPerKm = ExtractValue(content, "driver_cargo_market_revenue_coef_per_km", Driver.DriverCargoMarketRevenueCoefPerKm);
            Driver.DriverHireCost = ExtractValue(content, "driver_hire_cost", Driver.DriverHireCost);
            Driver.DriverMaxCargoDamage = ExtractValue(content, "driver_max_cargo_damage", Driver.DriverMaxCargoDamage);
            Driver.FreeDriverLevelCap = ExtractValue(content, "free_driver_level_cap", Driver.FreeDriverLevelCap);
            Driver.DriverOfferScrapProb = ExtractValue(content, "driver_offer_scrap_prob", Driver.DriverOfferScrapProb);
            Driver.DriverQuitwarnTime = ExtractValue(content, "driver_quitwarn_time", Driver.DriverQuitwarnTime);
            Driver.DriverQuitTime = ExtractValue(content, "driver_quit_time", Driver.DriverQuitTime);
            Driver.DriverNoReturnJobProb = ExtractValue(content, "driver_no_return_job_prob", Driver.DriverNoReturnJobProb);
            Driver.DriverSkilledJobProb = ExtractValue(content, "driver_skilled_job_prob", Driver.DriverSkilledJobProb);

            // 7. Experience Points (XP)
            XP.FreeRoamReportLimit = ExtractValue(content, "free_roam_report_limit", XP.FreeRoamReportLimit);
            XP.ExpCargoDelivery = ExtractValue(content, "exp_cargo_delivery", XP.ExpCargoDelivery);
            XP.ExpFreeRoam = ExtractValue(content, "exp_free_roam", XP.ExpFreeRoam);
            XP.ExpRoadDiscovery = ExtractValue(content, "exp_road_discovery", XP.ExpRoadDiscovery);
            XP.ExpParkBonus = ExtractValue(content, "exp_park_bonus", XP.ExpParkBonus);
            XP.ExpParkBonusMedium = ExtractValue(content, "exp_park_bonus_medium", XP.ExpParkBonusMedium);
            XP.ExpParkBonusHard = ExtractValue(content, "exp_park_bonus_hard", XP.ExpParkBonusHard);
            XP.ExpParkDoubleBonus = ExtractValue(content, "exp_park_double_bonus", XP.ExpParkDoubleBonus);
            XP.ExpParkDoubleBonusMedium = ExtractValue(content, "exp_park_double_bonus_medium", XP.ExpParkDoubleBonusMedium);
            XP.ExpParkDoubleBonusHard = ExtractValue(content, "exp_park_double_bonus_hard", XP.ExpParkDoubleBonusHard);
            XP.ExpParkLoadBonus = ExtractValue(content, "exp_park_load_bonus", XP.ExpParkLoadBonus);
            XP.ExpDamagedCargo = ExtractValue(content, "exp_damaged_cargo", XP.ExpDamagedCargo);
            XP.ExpDamagedCargoFactor = ExtractValue(content, "exp_damaged_cargo_factor", XP.ExpDamagedCargoFactor);
        }

        public void Export(ref string content)
        {
            // 1. Garages & Trucks
            UpdateParameter(ref content, "truck_refund", Garage.TruckRefund);
            UpdateParameter(ref content, "price_small_garage", Garage.PriceSmallGarage);
            UpdateParameter(ref content, "price_garage_upgrade", Garage.PriceGarageUpgrade);
            UpdateParameter(ref content, "garage_prod_plan_tiny", Garage.GarageProdPlanTiny);
            UpdateParameter(ref content, "garage_prod_plan_small", Garage.GarageProdPlanSmall);
            UpdateParameter(ref content, "garage_prod_plan_large", Garage.GarageProdPlanLarge);
            UpdateParameter(ref content, "fuel_discount_in_garage", Garage.FuelDiscountInGarage);
            UpdateParameter(ref content, "truck_count_for_online", Garage.TruckCountForOnline);

            // 2. Cargo & Revenue
            UpdateParameter(ref content, "revenue_per_km_base", Revenue.RevenuePerKmBase);
            UpdateParameter(ref content, "fixed_revenue", Revenue.FixedRevenue);
            UpdateParameter(ref content, "revenue_coef_per_km", Revenue.RevenueCoefPerKm);
            UpdateParameter(ref content, "cargo_market_revenue_coef_per_km", Revenue.CargoMarketRevenueCoefPerKm);
            UpdateParameter(ref content, "reward_bonus_level", Revenue.RewardBonusLevel);
            UpdateParameter(ref content, "abandoned_job_fine", Revenue.AbandonedJobFine);
            UpdateParameter(ref content, "cargo_damage_cost", Revenue.CargoDamageCost);
            UpdateParameter(ref content, "cargo_damage_cost_factor", Revenue.CargoDamageCostFactor);

            // 3. Delivery Windows & Deadlines
            UpdateArrayParameter(ref content, "late_delivery_max_overtime", 0, DeliveryWindows.LateDeliveryMaxOvertime0);
            UpdateArrayParameter(ref content, "late_delivery_max_overtime", 1, DeliveryWindows.LateDeliveryMaxOvertime1);
            UpdateArrayParameter(ref content, "late_delivery_max_overtime", 2, DeliveryWindows.LateDeliveryMaxOvertime2);
            UpdateArrayParameter(ref content, "delivery_window", 0, DeliveryWindows.DeliveryWindow0);
            UpdateArrayParameter(ref content, "delivery_window", 1, DeliveryWindows.DeliveryWindow1);
            UpdateArrayParameter(ref content, "delivery_window", 2, DeliveryWindows.DeliveryWindow2);
            UpdateArrayParameter(ref content, "delivery_window_coef", 0, DeliveryWindows.DeliveryWindowCoef0);
            UpdateArrayParameter(ref content, "delivery_window_coef", 1, DeliveryWindows.DeliveryWindowCoef1);
            UpdateArrayParameter(ref content, "delivery_window_coef", 2, DeliveryWindows.DeliveryWindowCoef2);

            // 4. Time, Fatigue & Jobs
            UpdateParameter(ref content, "simulation_avg_speed", Simulation.SimulationAvgSpeed);
            UpdateParameter(ref content, "maximum_driving_time", Simulation.MaximumDrivingTime);
            UpdateParameter(ref content, "sleeping_time", Simulation.SleepingTime);
            UpdateParameter(ref content, "hurry_up_timer", Simulation.HurryUpTimer);
            UpdateParameter(ref content, "no_cargo_prob", Simulation.NoCargoProb);
            UpdateParameter(ref content, "cargo_validity_min", Simulation.CargoValidityMin);
            UpdateParameter(ref content, "cargo_validity_max", Simulation.CargoValidityMax);

            // 5. Emergency Services & Refueling
            UpdateParameter(ref content, "tow_price_base", Service.TowPriceBase);
            UpdateParameter(ref content, "tow_price_factor", Service.TowPriceFactor);
            UpdateParameter(ref content, "tow_time_base", Service.TowTimeBase);
            UpdateParameter(ref content, "tow_time_factor", Service.TowTimeFactor);
            UpdateParameter(ref content, "tow_fuel_ratio", Service.TowFuelRatio);
            UpdateParameter(ref content, "refuel_fuel", Service.RefuelFuel);
            UpdateParameter(ref content, "refuel_price_base", Service.RefuelPriceBase);
            UpdateParameter(ref content, "refuel_price_factor", Service.RefuelPriceFactor);
            UpdateParameter(ref content, "refuel_time_base", Service.RefuelTimeBase);
            UpdateParameter(ref content, "recharge_energy", Service.RechargeEnergy);
            UpdateParameter(ref content, "recharge_price_base", Service.RechargePriceBase);
            UpdateParameter(ref content, "recharge_price", Service.RechargePrice);
            UpdateParameter(ref content, "recharge_time_base", Service.RechargeTimeBase);

            // 6. AI Drivers
            UpdateParameter(ref content, "minimal_driver_salary", Driver.MinimalDriverSalary);
            UpdateParameter(ref content, "driver_revenue_coef_per_km", Driver.DriverRevenueCoefPerKm);
            UpdateParameter(ref content, "driver_cargo_market_revenue_coef_per_km", Driver.DriverCargoMarketRevenueCoefPerKm);
            UpdateParameter(ref content, "driver_hire_cost", Driver.DriverHireCost);
            UpdateParameter(ref content, "driver_max_cargo_damage", Driver.DriverMaxCargoDamage);
            UpdateParameter(ref content, "free_driver_level_cap", Driver.FreeDriverLevelCap);
            UpdateParameter(ref content, "driver_offer_scrap_prob", Driver.DriverOfferScrapProb);
            UpdateParameter(ref content, "driver_quitwarn_time", Driver.DriverQuitwarnTime);
            UpdateParameter(ref content, "driver_quit_time", Driver.DriverQuitTime);
            UpdateParameter(ref content, "driver_no_return_job_prob", Driver.DriverNoReturnJobProb);
            UpdateParameter(ref content, "driver_skilled_job_prob", Driver.DriverSkilledJobProb);

            // 7. Experience Points (XP)
            UpdateParameter(ref content, "free_roam_report_limit", XP.FreeRoamReportLimit);
            UpdateParameter(ref content, "exp_cargo_delivery", XP.ExpCargoDelivery);
            UpdateParameter(ref content, "exp_free_roam", XP.ExpFreeRoam);
            UpdateParameter(ref content, "exp_road_discovery", XP.ExpRoadDiscovery);
            UpdateParameter(ref content, "exp_park_bonus", XP.ExpParkBonus);
            UpdateParameter(ref content, "exp_park_bonus_medium", XP.ExpParkBonusMedium);
            UpdateParameter(ref content, "exp_park_bonus_hard", XP.ExpParkBonusHard);
            UpdateParameter(ref content, "exp_park_double_bonus", XP.ExpParkDoubleBonus);
            UpdateParameter(ref content, "exp_park_double_bonus_medium", XP.ExpParkDoubleBonusMedium);
            UpdateParameter(ref content, "exp_park_double_bonus_hard", XP.ExpParkDoubleBonusHard);
            UpdateParameter(ref content, "exp_park_load_bonus", XP.ExpParkLoadBonus);
            UpdateParameter(ref content, "exp_damaged_cargo", XP.ExpDamagedCargo);
            UpdateParameter(ref content, "exp_damaged_cargo_factor", XP.ExpDamagedCargoFactor);
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

    public class EconomyGarageSettings
    {
        [DisplayName("LKW Rückerstattung")]
        [Description("Standard: 0.6 (60%) | Anteil des Neupreises, den du beim Verkauf eines alten LKWs oder Bauteils zurückbekommst.\n• Beispiel: 0.35 = 35% Restwert (realistischer), 0.85 = 85% Restwert.")]
        public string TruckRefund { get; set; } = "0.6";

        [DisplayName("Preis kleine Garage (€)")]
        [Description("Standard: 180.000 € | Kaufpreis einer neuen kleinen Garage (mit 3 Stellplätzen) in einer Stadt.")]
        public string PriceSmallGarage { get; set; } = "180000";

        [DisplayName("Preis Garagen-Upgrade (€)")]
        [Description("Standard: 100.000 € | Kosten für den Ausbau einer Garage von 3 auf 5 Stellplätze (inkl. eigener Hoftankstelle).")]
        public string PriceGarageUpgrade { get; set; } = "100000";

        [DisplayName("Produktionsplan: Winzig (€)")]
        [Description("Standard: 20.000 € | Internes Wochen-Umsatzziel für die Start-Garage (rein optische Statistik im Garagen-Manager).")]
        public string GarageProdPlanTiny { get; set; } = "20000";

        [DisplayName("Produktionsplan: Klein (€)")]
        [Description("Standard: 150.000 € | Internes Wochen-Umsatzziel für eine kleine Garage mit 3 Stellplätzen.")]
        public string GarageProdPlanSmall { get; set; } = "150000";

        [DisplayName("Produktionsplan: Groß (€)")]
        [Description("Standard: 300.000 € | Internes Wochen-Umsatzziel für eine große Garage mit 5 Stellplätzen.")]
        public string GarageProdPlanLarge { get; set; } = "300000";

        [DisplayName("Tankrabatt in eigener Garage")]
        [Description("Standard: 0.15 (15%) | Preisnachlass auf Diesel gegenüber normalen Tankstellen, wenn du an deiner eigenen großen Garage tankst.")]
        public string FuelDiscountInGarage { get; set; } = "0.15";

        [DisplayName("LKW-Anzahl für Onlinekauf")]
        [Description("Standard: 5 LKWs | Anzahl eigener LKWs, die du besitzen musst, bevor du beim Händler 'Online kaufen' nutzen darfst.\n• Tipp: Auf '0' oder '1' setzen, um sofort ab Spielbeginn online LKWs bestellen zu können.")]
        public string TruckCountForOnline { get; set; } = "5";
    }

    public class EconomyRevenueSettings
    {
        [DisplayName("Basisvergütung pro km (€)")]
        [Description("Standard: 15 € | Der wichtigste Wert der gesamten Spielwirtschaft! Grundbetrag in Euro je gefahrenem Auftrags-Kilometer.\n• Berechnung: Wird noch mit dem jeweiligen Fracht-Faktor und deinen Skill-Boni multipliziert.\n• Richtwerte: 1.5 bis 4.0 € = echtes Speditions-Feeling (Hardcore) | 6.0 bis 10.0 € = ausgewogen | 15 € = Vanilla.")]
        public string RevenuePerKmBase { get; set; } = "15";

        [DisplayName("Fester Fracht-Grundbetrag (€)")]
        [Description("Standard: 600 € | Einmalige Grundpauschale in Euro, die auf jeden Auftrag (unabhängig von der Strecke) oben draufgezahlt wird.\n• Tipp: Für realistische Wirtschaften auf 150 bis 300 € senken.")]
        public string FixedRevenue { get; set; } = "600";

        [DisplayName("Frachtmarkt Koeffizient")]
        [Description("Standard: 0.9 (90%) | Multiplikator für Aufträge im normalen 'Frachtmarkt' (eigene Zugmaschine, gestellter fremder Auflieger).\n• Hinweis: Bei 'Schnellen Aufträgen' (gestellter LKW) zahlt das Spiel automatisch noch etwas weniger.")]
        public string RevenueCoefPerKm { get; set; } = "0.9";

        [DisplayName("Frachtmarkt (Eigener Trailer) Koeffizient")]
        [Description("Standard: 1.0 (100%) | Multiplikator für den 'Cargo-Markt' (wenn du sowohl eigenen LKW als auch eigenen Anhänger nutzt).")]
        public string CargoMarketRevenueCoefPerKm { get; set; } = "1.0";

        [DisplayName("Bonus-Multiplikator pro Spielerlevel")]
        [Description("Standard: 0.015 (+1,5% je Level) | Zusätzlicher Einnahmen-Bonus für jedes erreichte Spielerlevel.\n• Beispiel: Auf Level 20 verdienst du bei 0.015 automatisch 30% mehr pro Auftrag.")]
        public string RewardBonusLevel { get; set; } = "0.015";

        [DisplayName("Strafe bei Auftragsabbruch (€)")]
        [Description("Standard: 12.000 € | Vertragsstrafe, wenn du einen laufenden Frachtauftrag im Menü abbrichst.")]
        public string AbandonedJobFine { get; set; } = "12000";

        [DisplayName("Frachtschaden: Fester Abzug (€ je 1%)")]
        [Description("Standard: 5.0 € | Fester Euro-Abzug pro 1% Schaden an der abgelieferten Fracht.")]
        public string CargoDamageCost { get; set; } = "5.0";

        [DisplayName("Frachtschaden: Prozentualer Abzug (je 1%)")]
        [Description("Standard: 0.04 (4%) | Prozentualer Abzug vom gesamten Auftragslohn pro 1% Frachtschaden.\n• Beispiel: Bei 0.04 und 10% Frachtschaden verlierst du 40% deiner Bezahlung (ab 25% Schaden gibt es 0 €).")]
        public string CargoDamageCostFactor { get; set; } = "0.04";
    }

    public class EconomyDeliveryWindowSettings
    {
        [DisplayName("Max. Verspätung: Standard-Fracht (Minuten)")]
        [Description("Standard: 2880 Min (48 Std) | Wie lange du das Lieferfenster bei normalen Aufträgen überziehen darfst, bevor es den maximalen Abzug gibt.")]
        public string LateDeliveryMaxOvertime0 { get; set; } = "2880";

        [DisplayName("Max. Verspätung: Wichtige Fracht (Minuten)")]
        [Description("Standard: 720 Min (12 Std) | Maximale Überziehungszeit bei wichtigen Lieferungen (einzelner Pfeil).")]
        public string LateDeliveryMaxOvertime1 { get; set; } = "720";

        [DisplayName("Max. Verspätung: Dringende Fracht (Minuten)")]
        [Description("Standard: 240 Min (4 Std) | Maximale Überziehungszeit bei dringenden Eillieferungen (Doppelpfeil).")]
        public string LateDeliveryMaxOvertime2 { get; set; } = "240";

        [DisplayName("Zeitfenster-Breite: Leicht (Minuten)")]
        [Description("Standard: 400 Min | Größe des Lieferzeitfensters (Ankunftszeitraum von–bis) für normale Standard-Aufträge.")]
        public string DeliveryWindow0 { get; set; } = "400";

        [DisplayName("Zeitfenster-Breite: Mittel (Minuten)")]
        [Description("Standard: 250 Min | Größe des Lieferzeitfensters für wichtige Aufträge.")]
        public string DeliveryWindow1 { get; set; } = "250";

        [DisplayName("Zeitfenster-Breite: Hart (Minuten)")]
        [Description("Standard: 90 Min | Größe des Lieferzeitfensters für dringende Just-in-Time-Lieferungen.")]
        public string DeliveryWindow2 { get; set; } = "90";

        [DisplayName("Vergütungs-Koeffizient: Leicht")]
        [Description("Standard: 1.0 (100%) | Einnahmen-Multiplikator für normale Aufträge ohne Zeitdruck.")]
        public string DeliveryWindowCoef0 { get; set; } = "1.0";

        [DisplayName("Vergütungs-Koeffizient: Mittel")]
        [Description("Standard: 1.15 (+15%) | Einnahmen-Multiplikator für wichtige Lieferungen.")]
        public string DeliveryWindowCoef1 { get; set; } = "1.15";

        [DisplayName("Vergütungs-Koeffizient: Hart")]
        [Description("Standard: 1.4 (+40%) | Einnahmen-Multiplikator für dringende Eillieferungen.")]
        public string DeliveryWindowCoef2 { get; set; } = "1.4";
    }

    public class EconomySimulationSettings
    {
        [DisplayName("Simulierte Durchschnittsgeschwindigkeit (km/h)")]
        [Description("Standard: 62.0 km/h | Damit berechnet das Spiel im Voraus, wie viel Zeit du für einen Auftrag bekommst.\n• Wichtig: Wenn du den Wert senkst (z. B. auf 50 km/h), bekommst du für alle Aufträge deutlich entspanntere Lieferfristen!")]
        public string SimulationAvgSpeed { get; set; } = "62.0";

        [DisplayName("Maximale Lenkzeit (Minuten)")]
        [Description("Standard: 660 Min (11 Std) | Wie viele Spielminuten du am Stück fahren darfst, bevor der Fahrer müde wird und gähnt.\n• Umrechnung: 540 = 9 Std (realistischer EU-Tagessatz) | 660 = 11 Std | 900 = 15 Std.")]
        public string MaximumDrivingTime { get; set; } = "660";

        [DisplayName("Schlafenszeit / Ruhepause (Minuten)")]
        [Description("Standard: 540 Min (9 Std) | Wie viele Spielminuten beim Schlafen auf einem Rastplatz vergehen.\n• Umrechnung: 480 = 8 Std | 540 = 9 Std | 660 = 11 Std (reguläre EU-Ruhezeit).")]
        public string SleepingTime { get; set; } = "540";

        [DisplayName("Beeilungsmusik Timer (Minuten)")]
        [Description("Standard: 114 Min | Ab wie vielen verbleibenden Spielminuten vor Ablauf der Lieferfrist die dramatische 'Beeilungs-Musik' startet (0 = Musik komplett aus!).")]
        public string HurryUpTimer { get; set; } = "114";

        [DisplayName("Leerplatz-Wahrscheinlichkeit bei Firmen")]
        [Description("Standard: 0.1 (10%) | Wahrscheinlichkeit, dass ein Fracht-Slot bei einer Firma im Auftragsmenü leer bleibt.")]
        public string NoCargoProb { get; set; } = "0.1";

        [DisplayName("Auftragsgültigkeit Min (Minuten)")]
        [Description("Standard: 180 Min (3 Std) | Kürzeste Zeitspanne, wie lange ein generierter Auftrag im Frachtmarkt verfügbar bleibt.")]
        public string CargoValidityMin { get; set; } = "180";

        [DisplayName("Auftragsgültigkeit Max (Minuten)")]
        [Description("Standard: 1800 Min (30 Std) | Längste Zeitspanne, wie lange ein generierter Auftrag im Frachtmarkt verfügbar bleibt.")]
        public string CargoValidityMax { get; set; } = "1800";
    }

    public class EconomyServiceSettings
    {
        [DisplayName("Abschleppen: Basispreis (€)")]
        [Description("Standard: 150 € | Feste Grundgebühr, wenn du dich per F7-Notdienst zur nächsten Werkstatt abschleppen lässt.")]
        public string TowPriceBase { get; set; } = "150";

        [DisplayName("Abschleppen: Preis pro Meter (€)")]
        [Description("Standard: 0.4 € | Zusatzkosten pro Meter Luftlinie bis zur Werkstatt (0.4 € pro Meter = 400 € pro Kilometer).")]
        public string TowPriceFactor { get; set; } = "0.4";

        [DisplayName("Abschleppen: Basiszeit (Sekunden)")]
        [Description("Standard: 1800 Sek (30 Min) | Feste Spielzeit in Sekunden, die beim Abschleppen mindestens vergeht.")]
        public string TowTimeBase { get; set; } = "1800";

        [DisplayName("Abschleppen: Zeit pro Meter (Sekunden)")]
        [Description("Standard: 3 Sek | Zusätzliche Spielzeit in Sekunden je Meter Entfernung zur Zielwerkstatt.")]
        public string TowTimeFactor { get; set; } = "3";

        [DisplayName("Tankfüllung nach Abschleppen")]
        [Description("Standard: 0.0 (0%) | Falls dein Tank leer war: Auf wie viel Prozent der Tank beim Abschleppen automatisch aufgefüllt wird (z. B. 0.15 = 15%).")]
        public string TowFuelRatio { get; set; } = "0.0";

        [DisplayName("Notbetankung: Spritmenge (Liter)")]
        [Description("Standard: 50 Liter | Wie viele Liter Diesel der F7-Pannendienst bei einer Notbetankung einfüllt.")]
        public string RefuelFuel { get; set; } = "50";

        [DisplayName("Notbetankung: Grundgebühr (€)")]
        [Description("Standard: 150 € | Feste Anfahrtspauschale für den F7-Tanknotdienst (zusätzlich zum Spritpreis).")]
        public string RefuelPriceBase { get; set; } = "150";

        [DisplayName("Notbetankung: Spritpreis-Multiplikator")]
        [Description("Standard: 3 (300%) | Wie viel teurer der Diesel beim Pannendienst im Vergleich zur normalen Tankstelle ist.")]
        public string RefuelPriceFactor { get; set; } = "3";

        [DisplayName("Notbetankung: Zeitverlust (Sekunden)")]
        [Description("Standard: 1800 Sek (30 Min) | Wie viel Spielzeit (in Sekunden) bei einer Notbetankung vergeht.")]
        public string RefuelTimeBase { get; set; } = "1800";

        [DisplayName("E-Truck Notladung: Energiemenge (kWh)")]
        [Description("Standard: 100 kWh | Geladene Strommenge in kWh beim F7-Notdienst für Elektro-LKWs.")]
        public string RechargeEnergy { get; set; } = "100";

        [DisplayName("E-Truck Notladung: Grundgebühr (€)")]
        [Description("Standard: 150 € | Feste Anfahrtspauschale für den mobilen Lade-Notdienst.")]
        public string RechargePriceBase { get; set; } = "150";

        [DisplayName("E-Truck Notladung: Preis pro kWh (€)")]
        [Description("Standard: 2 € | Arbeitspreis in Euro je geladener Kilowattstunde (kWh) Notstrom.")]
        public string RechargePrice { get; set; } = "2";

        [DisplayName("E-Truck Notladung: Ladezeit (Sekunden)")]
        [Description("Standard: 7200 Sek (2 Std) | Spielzeit in Sekunden, die der mobile Notladevorgang dauert.")]
        public string RechargeTimeBase { get; set; } = "7200";
    }

    public class EconomyDriverSettings
    {
        [DisplayName("KI-Fahrer: Mindestgehalt (€)")]
        [Description("Standard: 350 € | Grundgehalt für deine angestellten Fahrer pro Auftrag/Tag.")]
        public string MinimalDriverSalary { get; set; } = "350";

        [DisplayName("KI-Fahrer: Einnahmenkoeffizient (Frachtmarkt)")]
        [Description("Standard: 0.67 (67%) | Bestimmt, wie viel Umsatz deine KI-Fahrer im Vergleich zur Basisvergütung einfahren.\n• Tipp: Wenn KI-Fahrer zu schnell Millionen scheffeln, senke diesen Wert auf z. B. 0.35 oder 0.45.")]
        public string DriverRevenueCoefPerKm { get; set; } = "0.67";

        [DisplayName("KI-Fahrer: Einnahmenkoeffizient (Eigener Trailer)")]
        [Description("Standard: 0.70 (70%) | Umsatzfaktor für KI-Fahrer, wenn sie einen eigenen Anhänger deiner Firma nutzen.")]
        public string DriverCargoMarketRevenueCoefPerKm { get; set; } = "0.70";

        [DisplayName("KI-Fahrer: Vermittlungsgebühr (€)")]
        [Description("Standard: 1500 € | Einmalige Gebühr an die Arbeitsagentur beim Einstellen eines neuen Fahrers.")]
        public string DriverHireCost { get; set; } = "1500";

        [DisplayName("KI-Fahrer: Max. Frachtschaden (%)")]
        [Description("Standard: 6.0% | Maximaler Schaden, den ein angestellter Fahrer an einer Ladung verursachen kann.")]
        public string DriverMaxCargoDamage { get; set; } = "6.0";

        [DisplayName("KI-Fahrer: Max. Level in Agentur")]
        [Description("Standard: 30 | Höchstes Erfahrungslevel, mit dem neue Fahrer bei der Arbeitsagentur erscheinen können.")]
        public string FreeDriverLevelCap { get; set; } = "30";

        [DisplayName("Arbeitsmarkt-Fluktuation")]
        [Description("Standard: 0.5 (50%) | Wahrscheinlichkeit, dass ein nicht eingestelltes Fahrerangebot beim nächsten Update durch einen neuen Kandidaten ersetzt wird.")]
        public string DriverOfferScrapProb { get; set; } = "0.5";

        [DisplayName("Kündigungswarnung ohne LKW (Tage)")]
        [Description("Standard: 3 Tage | Nach wie vielen Tagen ohne zugewiesenen LKW sich ein neuer Fahrer per E-Mail beschwert.")]
        public string DriverQuitwarnTime { get; set; } = "3";

        [DisplayName("Kündigung ohne LKW (Tage)")]
        [Description("Standard: 5 Tage | Nach wie vielen Tagen ohne eigenen LKW der angestellte Fahrer deine Firma wieder verlässt.")]
        public string DriverQuitTime { get; set; } = "5";

        [DisplayName("Leerfahrt-Wahrscheinlichkeit (Rückfahrt)")]
        [Description("Standard: 0.1 (10%) | Risiko, dass ein KI-Fahrer am Zielort keine Rückfracht findet und auf deine Kosten leer zur Garage zurückfährt.\n• Realismus-Tipp: Auf 0.20 (20%) oder 0.25 (25%) erhöhen für realistischere Speditions-Margen.")]
        public string DriverNoReturnJobProb { get; set; } = "0.1";

        [DisplayName("Bevorzugung von Spezial-Aufträgen")]
        [Description("Standard: 0.8 (80%) | Wahrscheinlichkeit, dass KI-Fahrer gezielt lukrative Aufträge passend zu ihren erlernten Fähigkeiten (z. B. ADR/Gefahrgut) auswählen.")]
        public string DriverSkilledJobProb { get; set; } = "0.8";
    }

    public class EconomyXPSettings
    {
        [DisplayName("Freie Fahrt: XP-Meldeschwelle (km)")]
        [Description("Standard: 100 km | Nach wie vielen Kilometern Fahrt ohne Auftrag die gesammelten Freie-Fahrt-XP gutgeschrieben werden.")]
        public string FreeRoamReportLimit { get; set; } = "100";

        [DisplayName("XP pro Auftrags-Kilometer")]
        [Description("Standard: 1.0 XP/km | Erfahrungspunkte je gefahrenem Kilometer während einer Frachtlieferung.\n• Beispiel: Eine 500-km-Tour bringt bei 1.0 exakt 500 Basis-XP (plus Skill- und Parkboni).")]
        public string ExpCargoDelivery { get; set; } = "1.0";

        [DisplayName("XP pro Freie-Fahrt-Kilometer")]
        [Description("Standard: 0.5 XP/km | Erfahrungspunkte je Kilometer, wenn du ohne aktiven Auftrag unterwegs bist.")]
        public string ExpFreeRoam { get; set; } = "0.5";

        [DisplayName("XP Bonus: Neue Straße entdeckt")]
        [Description("Standard: 0.8 XP/km | Zusätzlicher XP-Bonus pro Kilometer auf Straßenabschnitten, die du zum ersten Mal befährst.")]
        public string ExpRoadDiscovery { get; set; } = "0.8";

        [DisplayName("Parkbonus: Leicht (XP)")]
        [Description("Standard: 15 XP | Fester XP-Bonus für das einfache, gerade Vorwärts-Abstellen des Aufliegers.")]
        public string ExpParkBonus { get; set; } = "15";

        [DisplayName("Parkbonus: Mittel (XP)")]
        [Description("Standard: 40 XP | Fester XP-Bonus für das mittelschwere Einparken ('Sicher gehen').")]
        public string ExpParkBonusMedium { get; set; } = "40";

        [DisplayName("Parkbonus: Schwer / Profi (XP)")]
        [Description("Standard: 90 XP | Fester XP-Bonus für das anspruchsvolle Rückwärts-Einparken ('Wo soll ich ihn abstellen?').")]
        public string ExpParkBonusHard { get; set; } = "90";

        [DisplayName("Doppelauflieger Parkbonus: Leicht (XP)")]
        [Description("Standard: 20 XP | Einpark-Bonus beim Fahren von Doppelaufliegern / HCT (Leicht).")]
        public string ExpParkDoubleBonus { get; set; } = "20";

        [DisplayName("Doppelauflieger Parkbonus: Mittel (XP)")]
        [Description("Standard: 60 XP | Einpark-Bonus beim Fahren von Doppelaufliegern / HCT (Mittel).")]
        public string ExpParkDoubleBonusMedium { get; set; } = "60";

        [DisplayName("Doppelauflieger Parkbonus: Schwer (XP)")]
        [Description("Standard: 170 XP | Einpark-Bonus beim rangierenden Rückwärtsparken von Doppelaufliegern (Schwer).")]
        public string ExpParkDoubleBonusHard { get; set; } = "170";

        [DisplayName("Beladungs-Parkbonus (XP)")]
        [Description("Standard: 15 XP | Zusätzliche XP, wenn du mit eigenem Anhänger beim Beladen manuell an die Rampe rangierst.")]
        public string ExpParkLoadBonus { get; set; } = "15";

        [DisplayName("XP-Abzug bei Frachtschaden (je 1%)")]
        [Description("Standard: 5 XP | Fester Punkteabzug pro 1% Schaden an der gelieferten Fracht.")]
        public string ExpDamagedCargo { get; set; } = "5";

        [DisplayName("XP-Abzug Faktor bei Frachtschaden (je 1%)")]
        [Description("Standard: 0.04 (4%) | Prozentualer XP-Abzug von den Auftrags-XP je 1% Frachtschaden.")]
        public string ExpDamagedCargoFactor { get; set; } = "0.04";
    }
}
