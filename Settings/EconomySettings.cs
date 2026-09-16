using System.ComponentModel;
using System.Text.RegularExpressions;

namespace ETS2_ModTool
{
    public class EconomySettings
    {
        [Category("Wirtschaft (economy_data.sii)")]
        [DisplayName("1. Garagen & LKW-Kauf")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public EconomyGarageSettings Garage { get; set; } = new EconomyGarageSettings();

        [Category("Wirtschaft (economy_data.sii)")]
        [DisplayName("2. Fracht, Einnahmen & Strafen")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public EconomyRevenueSettings Revenue { get; set; } = new EconomyRevenueSettings();

        [Category("Wirtschaft (economy_data.sii)")]
        [DisplayName("3. Zeitfenster & Lieferfristen")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public EconomyDeliveryWindowSettings DeliveryWindows { get; set; } = new EconomyDeliveryWindowSettings();

        [Category("Wirtschaft (economy_data.sii)")]
        [DisplayName("4. Zeit, Ermüdung & Frachtverfügbarkeit")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public EconomySimulationSettings Simulation { get; set; } = new EconomySimulationSettings();

        [Category("Wirtschaft (economy_data.sii)")]
        [DisplayName("5. Notdienst (Abschleppen, Sprit, Elektro)")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public EconomyServiceSettings Service { get; set; } = new EconomyServiceSettings();

        [Category("Wirtschaft (economy_data.sii)")]
        [DisplayName("6. KI-Fahrer Verhalten & Finanzen")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public EconomyDriverSettings Driver { get; set; } = new EconomyDriverSettings();

        [Category("Wirtschaft (economy_data.sii)")]
        [DisplayName("7. Erfahrungspunkte (XP) & Park-Boni")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public EconomyXPSettings XP { get; set; } = new EconomyXPSettings();

        public void Import(string content)
        {
            // 1. Garagen & LKW[cite: 1]
            Garage.TruckRefund = ExtractValue(content, "truck_refund", Garage.TruckRefund);
            Garage.PriceSmallGarage = ExtractValue(content, "price_small_garage", Garage.PriceSmallGarage);
            Garage.PriceGarageUpgrade = ExtractValue(content, "price_garage_upgrade", Garage.PriceGarageUpgrade);
            Garage.GarageProdPlanTiny = ExtractValue(content, "garage_prod_plan_tiny", Garage.GarageProdPlanTiny);
            Garage.GarageProdPlanSmall = ExtractValue(content, "garage_prod_plan_small", Garage.GarageProdPlanSmall);
            Garage.GarageProdPlanLarge = ExtractValue(content, "garage_prod_plan_large", Garage.GarageProdPlanLarge);
            Garage.FuelDiscountInGarage = ExtractValue(content, "fuel_discount_in_garage", Garage.FuelDiscountInGarage);
            Garage.TruckCountForOnline = ExtractValue(content, "truck_count_for_online", Garage.TruckCountForOnline);

            // 2. Fracht & Einnahmen[cite: 1]
            Revenue.RevenuePerKmBase = ExtractValue(content, "revenue_per_km_base", Revenue.RevenuePerKmBase);
            Revenue.FixedRevenue = ExtractValue(content, "fixed_revenue", Revenue.FixedRevenue);
            Revenue.RevenueCoefPerKm = ExtractValue(content, "revenue_coef_per_km", Revenue.RevenueCoefPerKm);
            Revenue.CargoMarketRevenueCoefPerKm = ExtractValue(content, "cargo_market_revenue_coef_per_km", Revenue.CargoMarketRevenueCoefPerKm);
            Revenue.RewardBonusLevel = ExtractValue(content, "reward_bonus_level", Revenue.RewardBonusLevel);
            Revenue.AbandonedJobFine = ExtractValue(content, "abandoned_job_fine", Revenue.AbandonedJobFine);
            Revenue.CargoDamageCost = ExtractValue(content, "cargo_damage_cost", Revenue.CargoDamageCost);
            Revenue.CargoDamageCostFactor = ExtractValue(content, "cargo_damage_cost_factor", Revenue.CargoDamageCostFactor);

            // 3. Zeitfenster & Lieferfristen[cite: 1]
            DeliveryWindows.LateDeliveryMaxOvertime0 = ExtractArrayValue(content, "late_delivery_max_overtime", 0, DeliveryWindows.LateDeliveryMaxOvertime0);
            DeliveryWindows.LateDeliveryMaxOvertime1 = ExtractArrayValue(content, "late_delivery_max_overtime", 1, DeliveryWindows.LateDeliveryMaxOvertime1);
            DeliveryWindows.LateDeliveryMaxOvertime2 = ExtractArrayValue(content, "late_delivery_max_overtime", 2, DeliveryWindows.LateDeliveryMaxOvertime2);
            DeliveryWindows.DeliveryWindow0 = ExtractArrayValue(content, "delivery_window", 0, DeliveryWindows.DeliveryWindow0);
            DeliveryWindows.DeliveryWindow1 = ExtractArrayValue(content, "delivery_window", 1, DeliveryWindows.DeliveryWindow1);
            DeliveryWindows.DeliveryWindow2 = ExtractArrayValue(content, "delivery_window", 2, DeliveryWindows.DeliveryWindow2);
            DeliveryWindows.DeliveryWindowCoef0 = ExtractArrayValue(content, "delivery_window_coef", 0, DeliveryWindows.DeliveryWindowCoef0);
            DeliveryWindows.DeliveryWindowCoef1 = ExtractArrayValue(content, "delivery_window_coef", 1, DeliveryWindows.DeliveryWindowCoef1);
            DeliveryWindows.DeliveryWindowCoef2 = ExtractArrayValue(content, "delivery_window_coef", 2, DeliveryWindows.DeliveryWindowCoef2);

            // 4. Zeit, Ermüdung & Jobs[cite: 1]
            Simulation.SimulationAvgSpeed = ExtractValue(content, "simulation_avg_speed", Simulation.SimulationAvgSpeed);
            Simulation.MaximumDrivingTime = ExtractValue(content, "maximum_driving_time", Simulation.MaximumDrivingTime);
            Simulation.SleepingTime = ExtractValue(content, "sleeping_time", Simulation.SleepingTime);
            Simulation.HurryUpTimer = ExtractValue(content, "hurry_up_timer", Simulation.HurryUpTimer);
            Simulation.NoCargoProb = ExtractValue(content, "no_cargo_prob", Simulation.NoCargoProb);
            Simulation.CargoValidityMin = ExtractValue(content, "cargo_validity_min", Simulation.CargoValidityMin);
            Simulation.CargoValidityMax = ExtractValue(content, "cargo_validity_max", Simulation.CargoValidityMax);

            // 5. Notdienst & Tanken[cite: 1]
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

            // 6. KI-Fahrer[cite: 1]
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

            // 7. XP[cite: 1]
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
            // 1. Garagen & LKW[cite: 1]
            UpdateParameter(ref content, "truck_refund", Garage.TruckRefund);
            UpdateParameter(ref content, "price_small_garage", Garage.PriceSmallGarage);
            UpdateParameter(ref content, "price_garage_upgrade", Garage.PriceGarageUpgrade);
            UpdateParameter(ref content, "garage_prod_plan_tiny", Garage.GarageProdPlanTiny);
            UpdateParameter(ref content, "garage_prod_plan_small", Garage.GarageProdPlanSmall);
            UpdateParameter(ref content, "garage_prod_plan_large", Garage.GarageProdPlanLarge);
            UpdateParameter(ref content, "fuel_discount_in_garage", Garage.FuelDiscountInGarage);
            UpdateParameter(ref content, "truck_count_for_online", Garage.TruckCountForOnline);

            // 2. Fracht & Einnahmen[cite: 1]
            UpdateParameter(ref content, "revenue_per_km_base", Revenue.RevenuePerKmBase);
            UpdateParameter(ref content, "fixed_revenue", Revenue.FixedRevenue);
            UpdateParameter(ref content, "revenue_coef_per_km", Revenue.RevenueCoefPerKm);
            UpdateParameter(ref content, "cargo_market_revenue_coef_per_km", Revenue.CargoMarketRevenueCoefPerKm);
            UpdateParameter(ref content, "reward_bonus_level", Revenue.RewardBonusLevel);
            UpdateParameter(ref content, "abandoned_job_fine", Revenue.AbandonedJobFine);
            UpdateParameter(ref content, "cargo_damage_cost", Revenue.CargoDamageCost);
            UpdateParameter(ref content, "cargo_damage_cost_factor", Revenue.CargoDamageCostFactor);

            // 3. Zeitfenster & Lieferfristen[cite: 1]
            UpdateArrayParameter(ref content, "late_delivery_max_overtime", 0, DeliveryWindows.LateDeliveryMaxOvertime0);
            UpdateArrayParameter(ref content, "late_delivery_max_overtime", 1, DeliveryWindows.LateDeliveryMaxOvertime1);
            UpdateArrayParameter(ref content, "late_delivery_max_overtime", 2, DeliveryWindows.LateDeliveryMaxOvertime2);
            UpdateArrayParameter(ref content, "delivery_window", 0, DeliveryWindows.DeliveryWindow0);
            UpdateArrayParameter(ref content, "delivery_window", 1, DeliveryWindows.DeliveryWindow1);
            UpdateArrayParameter(ref content, "delivery_window", 2, DeliveryWindows.DeliveryWindow2);
            UpdateArrayParameter(ref content, "delivery_window_coef", 0, DeliveryWindows.DeliveryWindowCoef0);
            UpdateArrayParameter(ref content, "delivery_window_coef", 1, DeliveryWindows.DeliveryWindowCoef1);
            UpdateArrayParameter(ref content, "delivery_window_coef", 2, DeliveryWindows.DeliveryWindowCoef2);

            // 4. Zeit, Ermüdung & Jobs[cite: 1]
            UpdateParameter(ref content, "simulation_avg_speed", Simulation.SimulationAvgSpeed);
            UpdateParameter(ref content, "maximum_driving_time", Simulation.MaximumDrivingTime);
            UpdateParameter(ref content, "sleeping_time", Simulation.SleepingTime);
            UpdateParameter(ref content, "hurry_up_timer", Simulation.HurryUpTimer);
            UpdateParameter(ref content, "no_cargo_prob", Simulation.NoCargoProb);
            UpdateParameter(ref content, "cargo_validity_min", Simulation.CargoValidityMin);
            UpdateParameter(ref content, "cargo_validity_max", Simulation.CargoValidityMax);

            // 5. Notdienst & Tanken[cite: 1]
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

            // 6. KI-Fahrer[cite: 1]
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

            // 7. XP[cite: 1]
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
        [Description("Rückkauf-Anteil beim LKW-Verkauf (Standard: 0.6)[cite: 1].")]
        public string TruckRefund { get; set; } = "0.6";

        [DisplayName("Preis kleine Garage")]
        [Description("Kaufpreis kleine Garage (Standard: 180000)[cite: 1].")]
        public string PriceSmallGarage { get; set; } = "180000";

        [DisplayName("Preis Garagen-Upgrade")]
        [Description("Ausbaukosten für die Garage (Standard: 100000)[cite: 1].")]
        public string PriceGarageUpgrade { get; set; } = "100000";

        [DisplayName("Produktionsplan: Tiny")]
        [Description("Umsatzziel kleinste Garage (Standard: 20000)[cite: 1].")]
        public string GarageProdPlanTiny { get; set; } = "20000";

        [DisplayName("Produktionsplan: Small")]
        [Description("Umsatzziel kleine Garage (Standard: 150000)[cite: 1].")]
        public string GarageProdPlanSmall { get; set; } = "150000";

        [DisplayName("Produktionsplan: Large")]
        [Description("Umsatzziel große Garage (Standard: 300000)[cite: 1].")]
        public string GarageProdPlanLarge { get; set; } = "300000";

        [DisplayName("Tankrabatt in Garage")]
        [Description("Rabatt an der eigenen Tankstelle (Standard: 0.15)[cite: 1].")]
        public string FuelDiscountInGarage { get; set; } = "0.15";

        [DisplayName("LKW-Anzahl für Onlinekauf")]
        [Description("Benötigte eigene Trucks für Onlinekauf (Standard: 5)[cite: 1].")]
        public string TruckCountForOnline { get; set; } = "5";
    }

    public class EconomyRevenueSettings
    {
        [DisplayName("Basisvergütung pro km")]
        [Description("Grundbetrag pro Kilometer (Standard: 15)[cite: 1].")]
        public string RevenuePerKmBase { get; set; } = "15";

        [DisplayName("Fester Fracht-Grundbetrag")]
        [Description("Fixe Pauschale pro Auftrag (Standard: 600)[cite: 1].")]
        public string FixedRevenue { get; set; } = "600";

        [DisplayName("Frachtmarkt Koeffizient")]
        [Description("Einnahmenkoeffizient Frachtmarkt (Standard: 0.9)[cite: 1].")]
        public string RevenueCoefPerKm { get; set; } = "0.9";

        [DisplayName("Frachtmarkt (Cargo) Koeffizient")]
        [Description("Einnahmenkoeffizient Cargo-Markt (Standard: 1.0)[cite: 1].")]
        public string CargoMarketRevenueCoefPerKm { get; set; } = "1.0";

        [DisplayName("Bonus-Multiplikator pro Level")]
        [Description("Vergütungsbonus je Spielerstufe (Standard: 0.015)[cite: 1].")]
        public string RewardBonusLevel { get; set; } = "0.015";

        [DisplayName("Strafe bei Auftragsabbruch")]
        [Description("Vertragsstrafe bei Stornierung (Standard: 12000)[cite: 1].")]
        public string AbandonedJobFine { get; set; } = "12000";

        [DisplayName("Frachtschaden Kosten")]
        [Description("Fester Abzug je 1% Schaden (Standard: 5.0)[cite: 1].")]
        public string CargoDamageCost { get; set; } = "5.0";

        [DisplayName("Frachtschaden Kostenfaktor")]
        [Description("Prozentualer Abzug je 1% Schaden (Standard: 0.04)[cite: 1].")]
        public string CargoDamageCostFactor { get; set; } = "0.04";
    }

    public class EconomyDeliveryWindowSettings
    {
        [DisplayName("Lieferfrist Überziehung: Leicht")]
        [Description("Max. Toleranz in Minuten (Standard: 2880 = 2 Tage)[cite: 1].")]
        public string LateDeliveryMaxOvertime0 { get; set; } = "2880";

        [DisplayName("Lieferfrist Überziehung: Normal")]
        [Description("Max. Toleranz in Minuten (Standard: 720 = 12 Std)[cite: 1].")]
        public string LateDeliveryMaxOvertime1 { get; set; } = "720";

        [DisplayName("Lieferfrist Überziehung: Dringend")]
        [Description("Max. Toleranz in Minuten (Standard: 240 = 4 Std)[cite: 1].")]
        public string LateDeliveryMaxOvertime2 { get; set; } = "240";

        [DisplayName("Zeitfenster: Leicht")]
        [Description("Lieferpuffer in Minuten (Standard: 400)[cite: 1].")]
        public string DeliveryWindow0 { get; set; } = "400";

        [DisplayName("Zeitfenster: Mittel")]
        [Description("Lieferpuffer in Minuten (Standard: 250)[cite: 1].")]
        public string DeliveryWindow1 { get; set; } = "250";

        [DisplayName("Zeitfenster: Hart")]
        [Description("Lieferpuffer in Minuten (Standard: 90)[cite: 1].")]
        public string DeliveryWindow2 { get; set; } = "90";

        [DisplayName("Vergütungs-Koeffizient: Leicht")]
        [Description("Umsatzfaktor Zeitfenster (Standard: 1.0)[cite: 1].")]
        public string DeliveryWindowCoef0 { get; set; } = "1.0";

        [DisplayName("Vergütungs-Koeffizient: Mittel")]
        [Description("Umsatzfaktor Zeitfenster (Standard: 1.15)[cite: 1].")]
        public string DeliveryWindowCoef1 { get; set; } = "1.15";

        [DisplayName("Vergütungs-Koeffizient: Hart")]
        [Description("Umsatzfaktor Zeitfenster (Standard: 1.4)[cite: 1].")]
        public string DeliveryWindowCoef2 { get; set; } = "1.4";
    }

    public class EconomySimulationSettings
    {
        [DisplayName("Simulierte Durchschnittsgeschwindigkeit")]
        [Description("In km/h zur Frachtzeitberechnung (Standard: 62.0)[cite: 1].")]
        public string SimulationAvgSpeed { get; set; } = "62.0";

        [DisplayName("Maximale Lenkzeit (Minuten)")]
        [Description("Maximalzeit bis zur Müdigkeit (Standard: 660 = 11 Std)[cite: 1].")]
        public string MaximumDrivingTime { get; set; } = "660";

        [DisplayName("Schlafenszeit (Minuten)")]
        [Description("Dauer einer Rast (Standard: 540 = 9 Std)[cite: 1].")]
        public string SleepingTime { get; set; } = "540";

        [DisplayName("Beeilungsmusik Timer")]
        [Description("Laufzeit des Dringlichkeits-Sounds im Spiel (Standard: 114)[cite: 1].")]
        public string HurryUpTimer { get; set; } = "114";

        [DisplayName("Leerfracht Wahrscheinlichkeit")]
        [Description("Chance auf leere Auftragsplätze (Standard: 0.1)[cite: 1].")]
        public string NoCargoProb { get; set; } = "0.1";

        [DisplayName("Auftragsgültigkeit Min (Minuten)")]
        [Description("Minimale Fracht-Verweildauer (Standard: 180)[cite: 1].")]
        public string CargoValidityMin { get; set; } = "180";

        [DisplayName("Auftragsgültigkeit Max (Minuten)")]
        [Description("Maximale Fracht-Verweildauer (Standard: 1800)[cite: 1].")]
        public string CargoValidityMax { get; set; } = "1800";
    }

    public class EconomyServiceSettings
    {
        [DisplayName("Abschleppen Basispreis")]
        [Description("Grundpreis Abschleppen (Standard: 150)[cite: 1].")]
        public string TowPriceBase { get; set; } = "150";

        [DisplayName("Abschleppen Entfernungsfaktor")]
        [Description("Kosten pro Meter Luftlinie (Standard: 0.4)[cite: 1].")]
        public string TowPriceFactor { get; set; } = "0.4";

        [DisplayName("Abschleppen Basiszeit")]
        [Description("Feste Zeitstrafe in Sekunden (Standard: 1800)[cite: 1].")]
        public string TowTimeBase { get; set; } = "1800";

        [DisplayName("Abschleppen Zeitfaktor")]
        [Description("Zeit pro Meter Luftlinie (Standard: 3)[cite: 1].")]
        public string TowTimeFactor { get; set; } = "3";

        [DisplayName("Notbetankung Tank-Anteil nach Tow")]
        [Description("Notfallfüllung nach Abschleppen in % (Standard: 0.0)[cite: 1].")]
        public string TowFuelRatio { get; set; } = "0.0";

        [DisplayName("Notbetankung Spritmenge")]
        [Description("Gelieferte Notmenge (Standard: 50)[cite: 1].")]
        public string RefuelFuel { get; set; } = "50";

        [DisplayName("Notbetankung Basispreis")]
        [Description("Grundpreis Notdienst Sprit (Standard: 150)[cite: 1].")]
        public string RefuelPriceBase { get; set; } = "150";

        [DisplayName("Notbetankung Preisfaktor")]
        [Description("Multiplikator des Markt-Spritpreises (Standard: 3)[cite: 1].")]
        public string RefuelPriceFactor { get; set; } = "3";

        [DisplayName("Notbetankung Zeit")]
        [Description("Dauer der Betankung vor Ort (Standard: 1800)[cite: 1].")]
        public string RefuelTimeBase { get; set; } = "1800";

        [DisplayName("E-Truck Notladung Energie")]
        [Description("Notfall-Ladung in kWh (Standard: 100)[cite: 1].")]
        public string RechargeEnergy { get; set; } = "100";

        [DisplayName("E-Truck Notladung Basispreis")]
        [Description("Grundgebühr Notladung (Standard: 150)[cite: 1].")]
        public string RechargePriceBase { get; set; } = "150";

        [DisplayName("E-Truck Strompreis")]
        [Description("Preis pro kWh Notstrom (Standard: 2)[cite: 1].")]
        public string RechargePrice { get; set; } = "2";

        [DisplayName("E-Truck Ladezeit")]
        [Description("Dauer des Ladevorgangs (Standard: 7200)[cite: 1].")]
        public string RechargeTimeBase { get; set; } = "7200";
    }

    public class EconomyDriverSettings
    {
        [DisplayName("KI-Fahrer Mindestgehalt")]
        [Description("Fixe Tagesbasis (Standard: 350)[cite: 1].")]
        public string MinimalDriverSalary { get; set; } = "350";

        [DisplayName("KI-Fahrer Einnahmenkoeffizient")]
        [Description("Umsatzkoeffizient Quick-Job / Standard (Standard: 0.67)[cite: 1].")]
        public string DriverRevenueCoefPerKm { get; set; } = "0.67";

        [DisplayName("KI-Fahrer Cargo-Markt Koeffizient")]
        [Description("Umsatzkoeffizient Frachtmarkt (Standard: 0.70)[cite: 1].")]
        public string DriverCargoMarketRevenueCoefPerKm { get; set; } = "0.70";

        [DisplayName("KI-Fahrer Anwerbegebühr")]
        [Description("Einmalige Vermittlungskosten (Standard: 1500)[cite: 1].")]
        public string DriverHireCost { get; set; } = "1500";

        [DisplayName("KI-Fahrer Max. Frachtschaden")]
        [Description("Schadensdeckelung in % (Standard: 6.0)[cite: 1].")]
        public string DriverMaxCargoDamage { get; set; } = "6.0";

        [DisplayName("Freie Fahrer Level-Cap")]
        [Description("Maximales Level angebotener Fahrer (Standard: 30)[cite: 1].")]
        public string FreeDriverLevelCap { get; set; } = "30";

        [DisplayName("Fahrerangebot Löschwahrscheinlichkeit")]
        [Description("Fluktuation am Arbeitsmarkt (Standard: 0.5)[cite: 1].")]
        public string DriverOfferScrapProb { get; set; } = "0.5";

        [DisplayName("Kündigungswarnung (Tage)")]
        [Description("Tage ohne LKW bis zur Kündigungswarnung (Standard: 3)[cite: 1].")]
        public string DriverQuitwarnTime { get; set; } = "3";

        [DisplayName("Kündigung (Tage)")]
        [Description("Tage bis zur endgültigen Kündigung (Standard: 5)[cite: 1].")]
        public string DriverQuitTime { get; set; } = "5";

        [DisplayName("Leerfahrt Wahrscheinlichkeit")]
        [Description("Chance auf Rückfahrt ohne Ladung (Standard: 0.1)[cite: 1].")]
        public string DriverNoReturnJobProb { get; set; } = "0.1";

        [DisplayName("Qualifizierte Auftrags-Wahrscheinlichkeit")]
        [Description("Chance auf spezielle Aufträge nach Skill (Standard: 0.8)[cite: 1].")]
        public string DriverSkilledJobProb { get; set; } = "0.8";
    }

    public class EconomyXPSettings
    {
        [DisplayName("Freie Fahrt Report Limit")]
        [Description("Obergrenze für freie Fahrten Berichte (Standard: 100)[cite: 1].")]
        public string FreeRoamReportLimit { get; set; } = "100";

        [DisplayName("XP Frachtlieferung")]
        [Description("XP pro km gefahrener Fracht (Standard: 1.0)[cite: 1].")]
        public string ExpCargoDelivery { get; set; } = "1.0";

        [DisplayName("XP Freie Fahrt")]
        [Description("XP pro km ohne Auftrag (Standard: 0.5)[cite: 1].")]
        public string ExpFreeRoam { get; set; } = "0.5";

        [DisplayName("XP Straßen-Entdeckung")]
        [Description("XP pro km neu erkundeter Straßen (Standard: 0.8)[cite: 1].")]
        public string ExpRoadDiscovery { get; set; } = "0.8";

        [DisplayName("Parkbonus: Leicht")]
        [Description("XP für manuelles Parken (Standard: 15)[cite: 1].")]
        public string ExpParkBonus { get; set; } = "15";

        [DisplayName("Parkbonus: Mittel")]
        [Description("XP für manuelles Parken (Standard: 40)[cite: 1].")]
        public string ExpParkBonusMedium { get; set; } = "40";

        [DisplayName("Parkbonus: Schwer")]
        [Description("XP für manuelles Parken (Standard: 90)[cite: 1].")]
        public string ExpParkBonusHard { get; set; } = "90";

        [DisplayName("Doppelauflieger Bonus: Leicht")]
        [Description("XP-Bonus für Doppelauflieger (Standard: 20)[cite: 1].")]
        public string ExpParkDoubleBonus { get; set; } = "20";

        [DisplayName("Doppelauflieger Bonus: Mittel")]
        [Description("XP-Bonus für Doppelauflieger (Standard: 60)[cite: 1].")]
        public string ExpParkDoubleBonusMedium { get; set; } = "60";

        [DisplayName("Doppelauflieger Bonus: Schwer")]
        [Description("XP-Bonus für Doppelauflieger (Standard: 170)[cite: 1].")]
        public string ExpParkDoubleBonusHard { get; set; } = "170";

        [DisplayName("Beladungs-Parkbonus")]
        [Description("XP für manuelles Andocken beim Laden (Standard: 15)[cite: 1].")]
        public string ExpParkLoadBonus { get; set; } = "15";

        [DisplayName("XP-Abzug Frachtschaden")]
        [Description("XP-Verlust pro 1% Schaden (Standard: 5)[cite: 1].")]
        public string ExpDamagedCargo { get; set; } = "5";

        [DisplayName("XP-Abzug Schaden Faktor")]
        [Description("Prozentualer XP-Verlust je 1% Schaden (Standard: 0.04)[cite: 1].")]
        public string ExpDamagedCargoFactor { get; set; } = "0.04";
    }
}