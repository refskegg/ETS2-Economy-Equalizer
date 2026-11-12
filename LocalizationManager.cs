using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace ETS2_ModTool
{
    public enum AppLanguage
    {
        DE,
        EN
    }

    public static class LocalizationManager
    {
        public static AppLanguage CurrentLanguage { get; set; } = AppLanguage.DE;

        public static string T(string deText, string enText)
        {
            return CurrentLanguage == AppLanguage.EN ? enText : deText;
        }

        public static string GetDisplayName(PropertyInfo prop)
        {
            if (CurrentLanguage == AppLanguage.EN)
            {
                string key = $"{prop.DeclaringType?.Name}.{prop.Name}";
                if (EnProperties.TryGetValue(key, out var entry) && !string.IsNullOrWhiteSpace(entry.Name))
                {
                    return entry.Name;
                }
            }

            var attr = prop.GetCustomAttribute<DisplayNameAttribute>();
            return attr != null && !string.IsNullOrWhiteSpace(attr.DisplayName) ? attr.DisplayName : prop.Name;
        }

        public static string GetDescription(PropertyInfo prop)
        {
            if (CurrentLanguage == AppLanguage.EN)
            {
                string key = $"{prop.DeclaringType?.Name}.{prop.Name}";
                if (EnProperties.TryGetValue(key, out var entry) && !string.IsNullOrWhiteSpace(entry.Desc))
                {
                    return entry.Desc;
                }
            }

            var attr = prop.GetCustomAttribute<DescriptionAttribute>();
            return attr != null ? attr.Description : string.Empty;
        }

        // Complete English translations for all ModSettings & Settings classes
        private static readonly Dictionary<string, (string Name, string Desc)> EnProperties = new(StringComparer.OrdinalIgnoreCase)
        {
            // --- ModSettings (Top-Level Categories) ---
            ["ModSettings.Bank"] = ("1. Bank & Loans", "Loans, interest rates, overdraft duration and repayment rules."),
            ["ModSettings.Damage"] = ("2. Damage & Wear", "Collision damage, part distribution and wear per kilometer."),
            ["ModSettings.Economy"] = ("3. Economy & Cargo", "Garages, freight revenue, rest times, AI drivers and XP."),
            ["ModSettings.Police"] = ("4. Police & Fines", "Fine amounts, speed cameras, check delays and offence probabilities."),
            ["ModSettings.Service"] = ("5. Service & Restoration", "Costs for complete truck and trailer restoration."),
            ["ModSettings.Skills"] = ("6. Driver Skills", "XP bonuses, revenue increases and distances per skill rank."),
            ["ModSettings.SpecialEvents"] = ("7. Events & Unlocks", "Conditions for initial loans, dealers, invitations and trailer purchase."),
            ["ModSettings.UsedVehicles"] = ("8. Used Truck Market", "Offer generation times, mileage, permanent wear and price factors."),

            // --- 1. BankSettings ---
            ["BankSettings.General"] = ("Bank General", "Rules for account overdraft, warning emails and cooldown after loan repayment."),
            ["BankSettings.Loan1"] = ("Loan 1 (Micro Loan)", "Smallest instant loan for repairs or early cash shortages."),
            ["BankSettings.Loan2"] = ("Loan 2 (Medium Loan)", "Medium loan, ideal for your first own trailer or a used truck."),
            ["BankSettings.Loan3"] = ("Loan 3 (Large Loan)", "Large loan for purchasing a brand-new truck or upgrading a garage."),
            ["BankSettings.Loan4"] = ("Loan 4 (Mega Loan)", "Maximum loan for buying new garages and multiple trucks including drivers."),

            ["BankGeneralSettings.MinAfterLoanRepay"] = ("Cooldown After Loan Repayment", "Default: 0 | Waiting time after full repayment before the same loan can be taken again.\n• Function: Prevents immediate refinancing if set > 0.\n• Recommendation: Keep at 0 for unrestricted loan access."),
            ["BankGeneralSettings.OverdraftDuration"] = ("Overdraft Duration (Days)", "Default: 5 Days | Number of in-game days your account is allowed to stay negative.\n• Function: If your balance remains negative after these days, the bank automatically repossesses trucks or trailers.\n• Tip: Higher values (e.g. 10) give more breathing room in hardcore economy mods."),
            ["BankGeneralSettings.OverdraftWarnDay"] = ("Overdraft Warning (Day)", "Default: 2 Days | In-game day in the red on which the bank sends an urgent warning email.\n• Note: Should always be lower than 'Overdraft Duration'."),

            ["LoanSettings.Amount"] = ("Loan Amount (€)", "Default: 10,000 to 400,000 € | The amount transferred to your account immediately when taking this loan.\n• Note: Also check the total credit limit under '7. Events & Unlocks' so large loans aren't blocked by the cap."),
            ["LoanSettings.InterestRate"] = ("Interest Rate (%)", "Default: 12% to 23% | Annual nominal interest rate in percent (e.g. 15 for 15%).\n• Function: Together with duration, determines the daily installment (deducted every day at 12:00 PM).\n• Realism Tip: Real commercial truck financing is usually between 5% and 9%."),
            ["LoanSettings.Duration"] = ("Duration (Weeks)", "Default: 5 or 10 Weeks | Repayment period in in-game weeks (1 week = 7 in-game days).\n• Example: '5' means 35 daily installments. A longer duration lowers the daily payment but increases total interest paid."),

            // --- 2. DamageSettings ---
            ["DamageSettings.Collision"] = ("1. Collision & Impact Factors", "Controls how severely trucks and trailers are damaged during accidents and collisions."),
            ["DamageSettings.TruckDist"] = ("2. Truck: Damage Distribution & Permanent Wear", "Defines how strongly individual truck parts suffer in a crash and how much remains unfixable."),
            ["DamageSettings.TrailerDist"] = ("3. Trailer: Damage Distribution & Cargo", "Distribution of crash damage across trailer components and loaded cargo."),
            ["DamageSettings.TruckWear"] = ("4. Truck: Wear per Kilometer", "Ongoing wear and tear (fixable & unfixable) per kilometer driven."),
            ["DamageSettings.TrailerWear"] = ("5. Trailer: Wear per Kilometer", "Ongoing wear and tear of your own trailer per kilometer driven."),

            ["DamageCollisionSettings.TruckDamageCoef"] = ("Truck Damage Coefficient", "Default: 0.0007 | Base multiplier for crash damage to the truck.\n• Function: Multiplied by impact kinetic energy. Higher values (e.g. 0.0014) double crash damage.\n• Tip: Set to '0.0' for indestructible trucks in accidents."),
            ["DamageCollisionSettings.TrailerDamageCoef"] = ("Trailer Damage Coefficient", "Default: 0.0007 | Base multiplier for crash damage to the towed trailer.\n• Function: Controls how sensitive the trailer is to collisions with guardrails, cars or obstacles."),
            ["DamageCollisionSettings.DraggedTrailerDamageCoef"] = ("Dragged Trailer Damage", "Default: 0.00002 | Continuous damage when dragging an improperly coupled trailer."),
            ["DamageCollisionSettings.TruckToTrailerDmg"] = ("Truck-to-Trailer Damage Transfer", "Default: 0.2 (20%) | Share of impact damage transferred from the tractor unit to the trailer during a frontal crash."),
            ["DamageCollisionSettings.SideDamageFactor"] = ("Side Impact Damage Factor", "Default: 6.0 | Damage multiplier when the truck or trailer is hit on the sides or tips over."),
            ["DamageCollisionSettings.RoofDamageFactor"] = ("Roof Impact Damage Factor", "Default: 11.0 | Damage multiplier during a rollover onto the cabin roof."),

            ["DamageTruckDistSettings.CabinDamageRatio"] = ("Cabin: Damage Ratio", "Default: 0.8 (80%) | Determines how strongly the cabin is damaged in an accident relative to total impact."),
            ["DamageTruckDistSettings.ChassisDamageRatio"] = ("Chassis: Damage Ratio", "Default: 1.0 (100%) | Determines how strongly the chassis is damaged in an accident."),
            ["DamageTruckDistSettings.EngineDamageRatio"] = ("Engine: Damage Ratio", "Default: 0.5 (50%) | Determines how strongly the engine suffers in a crash (high engine damage causes engine malfunctions!)."),
            ["DamageTruckDistSettings.TransmissionDamageRatio"] = ("Transmission: Damage Ratio", "Default: 0.3 (30%) | Determines how strongly the gearbox is damaged in a crash (causes grinding gears when high)."),
            ["DamageTruckDistSettings.WheelDamageRatio"] = ("Wheels: Damage Ratio", "Default: 0.15 (15%) | Determines how strongly the tires are damaged in an accident."),
            ["DamageTruckDistSettings.CabinUnfixable"] = ("Cabin: Unfixable Ratio", "Default: 0.04 (4%) | Share of crash damage to the cabin that immediately remains as permanent wear.\n• Note: Unfixable wear can only be removed via an expensive 'Complete Restoration' or part replacement (0.0 = disabled)."),
            ["DamageTruckDistSettings.ChassisUnfixable"] = ("Chassis: Unfixable Ratio", "Default: 0.05 (5%) | Share of crash damage to the chassis that remains as permanent wear (0.0 = no permanent wear)."),
            ["DamageTruckDistSettings.EngineUnfixable"] = ("Engine: Unfixable Ratio", "Default: 0.02 (2%) | Share of crash damage to the engine that remains as permanent wear (0.0 = no permanent wear)."),
            ["DamageTruckDistSettings.TransmissionUnfixable"] = ("Transmission: Unfixable Ratio", "Default: 0.01 (1%) | Share of crash damage to the transmission that remains as permanent wear (0.0 = no permanent wear)."),
            ["DamageTruckDistSettings.WheelUnfixable"] = ("Wheels: Unfixable Ratio", "Default: 0.01 (1%) | Share of crash damage to the wheels that remains as permanent wear (0.0 = no permanent wear)."),

            ["DamageTrailerDistSettings.TrailerBodyDamageRatio"] = ("Body: Damage Ratio", "Default: 0.8 (80%) | How strongly the trailer box/curtain body is damaged in a collision."),
            ["DamageTrailerDistSettings.TrailerChassisDamageRatio"] = ("Chassis: Damage Ratio", "Default: 1.0 (100%) | How strongly the trailer chassis is damaged in a collision."),
            ["DamageTrailerDistSettings.TrailerWheelDamageRatio"] = ("Wheels: Damage Ratio", "Default: 0.15 (15%) | How strongly the trailer tires suffer in a collision."),
            ["DamageTrailerDistSettings.CargoDamageRatio"] = ("Cargo: Damage Ratio", "Default: 1.0 (100%) | How strongly the loaded cargo is damaged when the trailer hits something.\n• Tip: A lower value (e.g. 0.5) halves cargo damage from minor bumps."),
            ["DamageTrailerDistSettings.TrailerBodyUnfixable"] = ("Body: Unfixable Ratio", "Default: 0.04 (4%) | Permanent wear share on the trailer body after an accident (0.0 = no permanent wear)."),
            ["DamageTrailerDistSettings.TrailerChassisUnfixable"] = ("Chassis: Unfixable Ratio", "Default: 0.05 (5%) | Permanent wear share on the trailer chassis after an accident (0.0 = no permanent wear)."),
            ["DamageTrailerDistSettings.TrailerWheelUnfixable"] = ("Wheels: Unfixable Ratio", "Default: 0.01 (1%) | Permanent wear share on the trailer wheels after an accident (0.0 = no permanent wear)."),

            ["DamageTruckWearSettings.CabinWear"] = ("Wear: Cabin", "Default: 0.0 | Normal fixable wear of the cabin per kilometer driven.\n• Conversion: '1e-6' (0.000001) equals 0.1% wear per 1,000 km."),
            ["DamageTruckWearSettings.ChassisWear"] = ("Wear: Chassis", "Default: 0.0 | Normal fixable wear of the chassis per kilometer driven.\n• Conversion: '1e-6' (0.000001) equals 0.1% wear per 1,000 km."),
            ["DamageTruckWearSettings.EngineWear"] = ("Wear: Engine", "Default: 2e-6 (0.000002) | Normal fixable engine wear per kilometer.\n• In Practice: '2e-6' means 0.2% wear per 1,000 km (or 2% after 10,000 km)."),
            ["DamageTruckWearSettings.TransmissionWear"] = ("Wear: Transmission", "Default: 2e-6 (0.000002) | Normal fixable transmission wear per kilometer.\n• In Practice: '2e-6' means 0.2% wear per 1,000 km."),
            ["DamageTruckWearSettings.WheelWear"] = ("Wear: Wheels", "Default: 2e-6 (0.000002) | Normal tire wear per kilometer driven.\n• In Practice: '2e-6' = 0.2% per 1,000 km. For faster tire wear choose e.g. '2e-5' (= 2% per 1,000 km)."),
            ["DamageTruckWearSettings.CabinWearUnfixable"] = ("Unfixable: Cabin", "Default: 0.0 | Permanent aging wear of the cabin per kilometer (only removable via complete restoration)."),
            ["DamageTruckWearSettings.ChassisWearUnfixable"] = ("Unfixable: Chassis", "Default: 0.0 | Permanent aging wear of the chassis per kilometer."),
            ["DamageTruckWearSettings.EngineWearUnfixable"] = ("Unfixable: Engine", "Default: 2e-7 (0.0000002) | Permanent engine wear per kilometer.\n• In Practice: '2e-7' equals 0.2% permanent wear after 10,000 km (or '0.0' to disable)."),
            ["DamageTruckWearSettings.TransmissionWearUnfixable"] = ("Unfixable: Transmission", "Default: 2e-7 (0.0000002) | Permanent transmission wear per kilometer.\n• In Practice: '2e-7' equals 0.2% permanent wear after 10,000 km (or '0.0' to disable)."),
            ["DamageTruckWearSettings.WheelWearUnfixable"] = ("Unfixable: Wheels", "Default: 2e-5 (0.00002) | Permanent tire wear per kilometer.\n• In Practice: '2e-5' means 2% permanent tire wear per 1,000 km (tires are completely worn out after 50,000 km)."),

            ["DamageTrailerWearSettings.TrailerBodyWear"] = ("Wear: Body", "Default: 0.0 | Fixable wear of the trailer body per kilometer."),
            ["DamageTrailerWearSettings.TrailerChassisWear"] = ("Wear: Chassis", "Default: 0.0 | Fixable wear of the trailer chassis per kilometer."),
            ["DamageTrailerWearSettings.TrailerWheelWear"] = ("Wear: Wheels", "Default: 2e-6 (0.000002) | Normal tire wear on your own trailer per kilometer (= 0.2% per 1,000 km)."),
            ["DamageTrailerWearSettings.TrailerBodyWearUnfixable"] = ("Unfixable: Body", "Default: 0.0 | Permanent aging wear on the trailer body per kilometer."),
            ["DamageTrailerWearSettings.TrailerChassisWearUnfixable"] = ("Unfixable: Chassis", "Default: 0.0 | Permanent aging wear on the trailer chassis per kilometer."),
            ["DamageTrailerWearSettings.TrailerWheelWearUnfixable"] = ("Unfixable: Wheels", "Default: 2e-5 (0.00002) | Permanent tire wear on the trailer per kilometer (= 2% per 1,000 km)."),

            // --- 3. EconomySettings ---
            ["EconomySettings.Garage"] = ("1. Garages & Truck Purchase", "Garage prices, fuel discounts, truck refund ratios and online dealer unlock requirements."),
            ["EconomySettings.Revenue"] = ("2. Cargo, Revenue & Penalties", "The most important parameters for your payout per job and deductions for cargo damage."),
            ["EconomySettings.DeliveryWindows"] = ("3. Delivery Windows & Deadlines", "Time buffers for standard, important and urgent deliveries as well as their revenue bonuses."),
            ["EconomySettings.Simulation"] = ("4. Time, Fatigue & Cargo Validity", "Driving and sleeping hours (fatigue simulation), average speed and lifespan of job offers."),
            ["EconomySettings.Service"] = ("5. Emergency Services (Tow, Fuel, EV)", "Costs and time penalties for F7 towing, emergency refueling and electric truck charging."),
            ["EconomySettings.Driver"] = ("6. AI Drivers Behavior & Finances", "Salary, revenue coefficients, empty return trip probability and quitting behavior of hired drivers."),
            ["EconomySettings.XP"] = ("7. Experience Points (XP) & Parking Bonuses", "XP rewards per kilometer, when discovering roads and when manually parking trailers."),

            ["EconomyGarageSettings.TruckRefund"] = ("Truck Trade-In Refund", "Default: 0.6 (60%) | Share of the original price you get back when selling an old truck or part.\n• Example: 0.35 = 35% resale value (more realistic), 0.85 = 85% resale value."),
            ["EconomyGarageSettings.PriceSmallGarage"] = ("Small Garage Price (€)", "Default: 180,000 € | Purchase price of a new small garage (3 slots) in a city."),
            ["EconomyGarageSettings.PriceGarageUpgrade"] = ("Garage Upgrade Price (€)", "Default: 100,000 € | Cost to upgrade a garage from 3 to 5 slots (includes your own yard fuel station)."),
            ["EconomyGarageSettings.GarageProdPlanTiny"] = ("Production Plan: Tiny (€)", "Default: 20,000 € | Internal weekly revenue target for the starter garage (purely visual stat in Garage Manager)."),
            ["EconomyGarageSettings.GarageProdPlanSmall"] = ("Production Plan: Small (€)", "Default: 150,000 € | Internal weekly revenue target for a small 3-slot garage."),
            ["EconomyGarageSettings.GarageProdPlanLarge"] = ("Production Plan: Large (€)", "Default: 300,000 € | Internal weekly revenue target for a large 5-slot garage."),
            ["EconomyGarageSettings.FuelDiscountInGarage"] = ("Garage Fuel Discount", "Default: 0.15 (15%) | Price discount on diesel compared to regular gas stations when refueling at your own large garage."),
            ["EconomyGarageSettings.TruckCountForOnline"] = ("Trucks Needed for Online Purchase", "Default: 5 Trucks | Number of owned trucks required before you can use 'Buy Online' at truck dealers.\n• Tip: Set to '0' or '1' to unlock online truck ordering right from the start."),

            ["EconomyRevenueSettings.RevenuePerKmBase"] = ("Base Revenue per km (€)", "Default: 15 € | The single most important parameter of the entire game economy! Base amount in Euro per job kilometer.\n• Calculation: Multiplied by cargo factor and your skill bonuses.\n• Guide: 1.5 to 4.0 € = true hardcore haulage | 6.0 to 10.0 € = balanced | 15 € = Vanilla."),
            ["EconomyRevenueSettings.FixedRevenue"] = ("Fixed Base Job Revenue (€)", "Default: 600 € | Flat bonus in Euro added to every job regardless of distance.\n• Tip: Lower to 150–300 € for realistic economies."),
            ["EconomyRevenueSettings.RevenueCoefPerKm"] = ("Freight Market Coefficient", "Default: 0.9 (90%) | Multiplier for jobs in the standard 'Freight Market' (own truck, company-provided trailer).\n• Note: 'Quick Jobs' (provided truck) automatically pay slightly less."),
            ["EconomyRevenueSettings.CargoMarketRevenueCoefPerKm"] = ("Cargo Market (Own Trailer) Coefficient", "Default: 1.0 (100%) | Multiplier for the 'Cargo Market' (using both your own truck and your own trailer)."),
            ["EconomyRevenueSettings.RewardBonusLevel"] = ("Bonus Multiplier per Player Level", "Default: 0.015 (+1.5% per level) | Additional revenue bonus for each player level reached.\n• Example: At level 20 with 0.015, you automatically earn +30% more per job."),
            ["EconomyRevenueSettings.AbandonedJobFine"] = ("Abandoned Job Penalty (€)", "Default: 12,000 € | Contract penalty fee when you cancel an active freight delivery."),
            ["EconomyRevenueSettings.CargoDamageCost"] = ("Cargo Damage: Fixed Deduction (€ per 1%)", "Default: 5.0 € | Fixed Euro deduction per 1% damage on the delivered cargo."),
            ["EconomyRevenueSettings.CargoDamageCostFactor"] = ("Cargo Damage: Percentage Deduction (per 1%)", "Default: 0.04 (4%) | Percentage deduction from total job payout per 1% cargo damage.\n• Example: At 0.04 and 10% cargo damage, you lose 40% of your payout (at 25% damage payout hits 0 €)."),

            ["EconomyDeliveryWindowSettings.LateDeliveryMaxOvertime0"] = ("Max Overtime: Standard Cargo (Minutes)", "Default: 2880 Min (48 hrs) | How long you can exceed the delivery deadline on normal jobs before hitting maximum penalty."),
            ["EconomyDeliveryWindowSettings.LateDeliveryMaxOvertime1"] = ("Max Overtime: Important Cargo (Minutes)", "Default: 720 Min (12 hrs) | Maximum overtime allowed on important deliveries (single arrow)."),
            ["EconomyDeliveryWindowSettings.LateDeliveryMaxOvertime2"] = ("Max Overtime: Urgent Cargo (Minutes)", "Default: 240 Min (4 hrs) | Maximum overtime allowed on urgent Just-in-Time deliveries (double arrow)."),
            ["EconomyDeliveryWindowSettings.DeliveryWindow0"] = ("Delivery Window Width: Easy (Minutes)", "Default: 400 Min | Size of the arrival time window (from–to) for standard jobs."),
            ["EconomyDeliveryWindowSettings.DeliveryWindow1"] = ("Delivery Window Width: Medium (Minutes)", "Default: 250 Min | Size of the arrival time window for important jobs."),
            ["EconomyDeliveryWindowSettings.DeliveryWindow2"] = ("Delivery Window Width: Hard (Minutes)", "Default: 90 Min | Size of the arrival time window for urgent Just-in-Time deliveries."),
            ["EconomyDeliveryWindowSettings.DeliveryWindowCoef0"] = ("Revenue Coefficient: Easy", "Default: 1.0 (100%) | Revenue multiplier for standard jobs without time pressure."),
            ["EconomyDeliveryWindowSettings.DeliveryWindowCoef1"] = ("Revenue Coefficient: Medium", "Default: 1.15 (+15%) | Revenue multiplier for important deliveries."),
            ["EconomyDeliveryWindowSettings.DeliveryWindowCoef2"] = ("Revenue Coefficient: Hard", "Default: 1.4 (+40%) | Revenue multiplier for urgent Just-in-Time deliveries."),

            ["EconomySimulationSettings.SimulationAvgSpeed"] = ("Simulated Average Speed (km/h)", "Default: 62.0 km/h | Used by the game to calculate how much time you get for a delivery.\n• Pro Tip: Lowering this value (e.g. to 50 km/h) gives you much more relaxed delivery deadlines on all jobs!"),
            ["EconomySimulationSettings.MaximumDrivingTime"] = ("Maximum Driving Time (Minutes)", "Default: 660 Min (11 hrs) | How many in-game minutes you can drive before getting tired and yawning.\n• Conversion: 540 = 9 hrs (realistic EU daily limit) | 660 = 11 hrs | 900 = 15 hrs."),
            ["EconomySimulationSettings.SleepingTime"] = ("Sleeping / Rest Duration (Minutes)", "Default: 540 Min (9 hrs) | How many in-game minutes pass when resting at a parking area.\n• Conversion: 480 = 8 hrs | 540 = 9 hrs | 660 = 11 hrs (regular EU rest period)."),
            ["EconomySimulationSettings.HurryUpTimer"] = ("Hurry-Up Music Timer (Minutes)", "Default: 114 Min | Remaining minutes before deadline when the dramatic 'hurry-up music' starts playing (0 = disable music completely!)."),
            ["EconomySimulationSettings.NoCargoProb"] = ("Empty Cargo Slot Probability", "Default: 0.1 (10%) | Probability that a cargo slot at a company remains empty."),
            ["EconomySimulationSettings.CargoValidityMin"] = ("Offer Validity Min (Minutes)", "Default: 180 Min (3 hrs) | Shortest time a generated job offer stays available in the freight market."),
            ["EconomySimulationSettings.CargoValidityMax"] = ("Offer Validity Max (Minutes)", "Default: 1800 Min (30 hrs) | Longest time a generated job offer stays available in the freight market."),

            ["EconomyServiceSettings.TowPriceBase"] = ("Towing: Base Price (€)", "Default: 150 € | Flat base fee when using F7 emergency towing to the nearest service station."),
            ["EconomyServiceSettings.TowPriceFactor"] = ("Towing: Price per Meter (€)", "Default: 0.4 € | Additional cost per meter of straight-line distance to the service station (0.4 €/m = 400 €/km)."),
            ["EconomyServiceSettings.TowTimeBase"] = ("Towing: Base Time (Seconds)", "Default: 1800 Sec (30 min) | Minimum in-game time in seconds that passes when being towed."),
            ["EconomyServiceSettings.TowTimeFactor"] = ("Towing: Time per Meter (Seconds)", "Default: 3 Sec | Additional in-game time in seconds per meter distance to the service station."),
            ["EconomyServiceSettings.TowFuelRatio"] = ("Fuel Ratio After Towing", "Default: 0.0 (0%) | If your tank was empty: percentage to which your tank is refilled during towing (e.g. 0.15 = 15%)."),
            ["EconomyServiceSettings.RefuelFuel"] = ("Emergency Refuel: Amount (Liters)", "Default: 50 Liters | How many liters of diesel the F7 roadside assistance delivers."),
            ["EconomyServiceSettings.RefuelPriceBase"] = ("Emergency Refuel: Base Fee (€)", "Default: 150 € | Flat call-out fee for F7 emergency refueling (on top of fuel cost)."),
            ["EconomyServiceSettings.RefuelPriceFactor"] = ("Emergency Refuel: Fuel Price Multiplier", "Default: 3 (300%) | How much more expensive emergency diesel is compared to regular gas stations."),
            ["EconomyServiceSettings.RefuelTimeBase"] = ("Emergency Refuel: Time Penalty (Seconds)", "Default: 1800 Sec (30 min) | How much in-game time (in seconds) passes during roadside refueling."),
            ["EconomyServiceSettings.RechargeEnergy"] = ("EV Emergency Charge: Energy (kWh)", "Default: 100 kWh | Amount of electricity in kWh delivered by F7 roadside service for electric trucks."),
            ["EconomyServiceSettings.RechargePriceBase"] = ("EV Emergency Charge: Base Fee (€)", "Default: 150 € | Flat call-out fee for mobile EV charging."),
            ["EconomyServiceSettings.RechargePrice"] = ("EV Emergency Charge: Price per kWh (€)", "Default: 2 € | Price in Euro per kilowatt-hour (kWh) of emergency power."),
            ["EconomyServiceSettings.RechargeTimeBase"] = ("EV Emergency Charge: Charging Time (Seconds)", "Default: 7200 Sec (2 hrs) | In-game time in seconds required for mobile emergency charging."),

            ["EconomyDriverSettings.MinimalDriverSalary"] = ("AI Driver: Minimum Salary (€)", "Default: 350 € | Base salary for your hired drivers per job/day."),
            ["EconomyDriverSettings.DriverRevenueCoefPerKm"] = ("AI Driver: Revenue Coefficient (Freight Market)", "Default: 0.67 (67%) | Determines how much revenue your AI drivers generate compared to base rate.\n• Tip: If AI drivers make millions too quickly, lower this to e.g. 0.35 or 0.45."),
            ["EconomyDriverSettings.DriverCargoMarketRevenueCoefPerKm"] = ("AI Driver: Revenue Coefficient (Own Trailer)", "Default: 0.70 (70%) | Revenue factor for AI drivers when using a company-owned trailer."),
            ["EconomyDriverSettings.DriverHireCost"] = ("AI Driver: Recruitment Fee (€)", "Default: 1500 € | One-time agency fee when hiring a new driver."),
            ["EconomyDriverSettings.DriverMaxCargoDamage"] = ("AI Driver: Max Cargo Damage (%)", "Default: 6.0% | Maximum damage a hired driver can cause to a load."),
            ["EconomyDriverSettings.FreeDriverLevelCap"] = ("AI Driver: Max Level in Agency", "Default: 30 | Highest experience level at which new drivers can appear in the recruitment agency."),
            ["EconomyDriverSettings.DriverOfferScrapProb"] = ("Job Market Turnover Rate", "Default: 0.5 (50%) | Probability that an unhired driver offer is replaced by a new candidate on the next refresh."),
            ["EconomyDriverSettings.DriverQuitwarnTime"] = ("Quit Warning Without Truck (Days)", "Default: 3 Days | After how many days without an assigned truck a new driver sends a complaint email."),
            ["EconomyDriverSettings.DriverQuitTime"] = ("Quitting Without Truck (Days)", "Default: 5 Days | After how many days without a truck the hired driver leaves your company."),
            ["EconomyDriverSettings.DriverNoReturnJobProb"] = ("Empty Return Trip Probability", "Default: 0.1 (10%) | Risk that an AI driver finds no return cargo at the destination and drives back empty at your expense.\n• Realism Tip: Increase to 0.20 (20%) or 0.25 (25%) for realistic haulage profit margins."),
            ["EconomyDriverSettings.DriverSkilledJobProb"] = ("Preference for Specialized Jobs", "Default: 0.8 (80%) | Probability that AI drivers specifically pick lucrative jobs matching their learned skills (e.g. ADR/Hazmat)."),

            ["EconomyXPSettings.FreeRoamReportLimit"] = ("Free Roam: XP Report Threshold (km)", "Default: 100 km | After how many kilometers of driving without a job the accumulated free-roam XP is awarded."),
            ["EconomyXPSettings.ExpCargoDelivery"] = ("XP per Job Kilometer", "Default: 1.0 XP/km | Experience points awarded per kilometer driven during a cargo delivery.\n• Example: A 500 km trip grants 500 base XP at 1.0 (plus skill and parking bonuses)."),
            ["EconomyXPSettings.ExpFreeRoam"] = ("XP per Free-Roam Kilometer", "Default: 0.5 XP/km | Experience points per kilometer when driving without an active job."),
            ["EconomyXPSettings.ExpRoadDiscovery"] = ("XP Bonus: New Road Discovered", "Default: 0.8 XP/km | Additional XP bonus per kilometer on road segments driven for the very first time."),
            ["EconomyXPSettings.ExpParkBonus"] = ("Parking Bonus: Easy (XP)", "Default: 15 XP | Flat XP bonus for simple straight-ahead trailer parking."),
            ["EconomyXPSettings.ExpParkBonusMedium"] = ("Parking Bonus: Medium (XP)", "Default: 40 XP | Flat XP bonus for medium-difficulty parking ('Play it safe')."),
            ["EconomyXPSettings.ExpParkBonusHard"] = ("Parking Bonus: Hard / Pro (XP)", "Default: 90 XP | Flat XP bonus for challenging reverse parking ('Where do you want it?')."),
            ["EconomyXPSettings.ExpParkDoubleBonus"] = ("Double Trailer Parking: Easy (XP)", "Default: 20 XP | Parking bonus when hauling double trailers / HCT (Easy)."),
            ["EconomyXPSettings.ExpParkDoubleBonusMedium"] = ("Double Trailer Parking: Medium (XP)", "Default: 60 XP | Parking bonus when hauling double trailers / HCT (Medium)."),
            ["EconomyXPSettings.ExpParkDoubleBonusHard"] = ("Double Trailer Parking: Hard (XP)", "Default: 170 XP | Parking bonus for reverse maneuvering of double trailers (Hard)."),
            ["EconomyXPSettings.ExpParkLoadBonus"] = ("Loading Bay Parking Bonus (XP)", "Default: 15 XP | Extra XP when manually backing your own trailer into the loading dock."),
            ["EconomyXPSettings.ExpDamagedCargo"] = ("XP Penalty for Cargo Damage (per 1%)", "Default: 5 XP | Fixed XP deduction per 1% damage on delivered cargo."),
            ["EconomyXPSettings.ExpDamagedCargoFactor"] = ("XP Penalty Factor for Cargo Damage (per 1%)", "Default: 0.04 (4%) | Percentage XP deduction from total job XP per 1% cargo damage."),

            // --- 4. PoliceSettings ---
            ["PoliceSettings.General"] = ("1. General Factors & Progression", "Level scaling of fines, progressive speed camera calculation and police car multipliers."),
            ["PoliceSettings.FineAmounts"] = ("2. Fine Amounts (€)", "Base fine amounts in Euro for each individual traffic offence."),
            ["PoliceSettings.CheckDelays"] = ("3. Check Delay / Grace Period (Seconds)", "How many seconds a violation must persist before an automatic fine is triggered."),
            ["PoliceSettings.PoliceCheckDelays"] = ("4. Check Delay with Police Nearby (Seconds)", "Shortened grace period in seconds when a police patrol car is right next to you in traffic."),
            ["PoliceSettings.Probabilities"] = ("5. Offence Probability Without Police (0.0 – 1.0)", "Chance (0.0 = 0%, 1.0 = 100%) that a violation is fined even with no police car nearby."),
            ["PoliceSettings.PoliceProbabilities"] = ("6. Probability with Police Nearby (0.0 – 1.0)", "Chance (0.0 = 0%, 1.0 = 100%) that a nearby patrol car in traffic notices and fines your violation."),

            ["PoliceGeneralSettings.FineFactorBase"] = ("Fine Base Factor (Level 0)", "Default: 0.2 (20%) | At player level 0 you only pay this fraction of the base fines.\n• Tip: Set Base = 1.0, Step = 0.0 and Limit = 1.0 if fines should always cost the exact full Euro amount from day 1."),
            ["PoliceGeneralSettings.FineFactorStep"] = ("Fine Factor Increase per Level", "Default: 0.08 (+8% per level) | By how much fines increase with each player level gained."),
            ["PoliceGeneralSettings.FineFactorLimit"] = ("Fine Factor Limit (Maximum)", "Default: 1.0 (100%) | Highest level multiplier for fines (reached at level 10 with default values)."),
            ["PoliceGeneralSettings.FineAmountRounding"] = ("Fine Amount Rounding (€)", "Default: 20 € | Calculated fines are always rounded to a clean multiple of this value (e.g. 10 € or 20 €)."),
            ["PoliceGeneralSettings.FineOverspeedStep"] = ("Overspeed Step (in m/s)", "Default: 2.777778 m/s (= 10 km/h) | Speed step interval at which speeding fines increase progressively.\n• Conversion: ETS2 calculates internally in meters per second (m/s × 3.6 = km/h). 2.777778 m/s equals exactly 10 km/h."),
            ["PoliceGeneralSettings.FineOverspeedStepMultiplier"] = ("Overspeed Multiplier per Step", "Default: 1.877 | With each 10 km/h step exceeded, the speeding fine is raised by this power.\n• Example: Driving 30 km/h over the limit costs significantly more than 10 km/h over."),
            ["PoliceGeneralSettings.FineOverspeedMultiplierLimit"] = ("Overspeed Multiplier Limit", "Default: 12.5 | Maximum cap for progressive surcharge during extreme speeding."),
            ["PoliceGeneralSettings.TimerDecreaseRate"] = ("Violation Timer Decay Rate (Seconds)", "Default: 120 Sec | How quickly the internal counter resets to 0 after a brief aborted violation (e.g. briefly overtaking in oncoming lane)."),
            ["PoliceGeneralSettings.PoliceNearbyFineRate"] = ("Surcharge with Police in Sight", "Default: 2 (Double Fine!) | Multiplier applied to the fine when committing an offence right in front of a patrol car (1 = no surcharge)."),
            ["PoliceGeneralSettings.PoliceNearbyOffenceTimer"] = ("Police Check Cooldown (Seconds)", "Default: 15.0 Sec | Minimum time in seconds between two fines issued by the same patrol car."),

            ["PoliceFineAmountSettings.CarCrash"] = ("Crash with AI Vehicle", "Default: 400 € | Base fine for causing a collision with another traffic vehicle."),
            ["PoliceFineAmountSettings.AvoidSleeping"] = ("Driving Time Violation / Fatigue", "Default: 500 € | Base fine for ignoring mandatory rest periods."),
            ["PoliceFineAmountSettings.WrongWay"] = ("Wrong-Way Driving", "Default: 100 € | Base fine for driving in the oncoming lane."),
            ["PoliceFineAmountSettings.SpeedingCamera"] = ("Stationary Speed Camera", "Default: 200 € | Base fine at fixed radar cameras (scaled further by the overspeed multiplier depending on speed)."),
            ["PoliceFineAmountSettings.NoLightsNight"] = ("Driving Without Lights (Night/Rain)", "Default: 150 € | Base fine when driving at night or in tunnels without headlights turned on."),
            ["PoliceFineAmountSettings.RedLights"] = ("Running a Red Light", "Default: 350 € | Base fine for crossing a red traffic light."),
            ["PoliceFineAmountSettings.Speeding"] = ("Speeding (Police Patrol)", "Default: 200 € | Base fine when caught speeding by a moving police car."),
            ["PoliceFineAmountSettings.AvoidWeighing"] = ("Skipping Weigh Station", "Default: 700 € | Base fine for bypassing an active weigh station despite being called in."),
            ["PoliceFineAmountSettings.IllegalTrailer"] = ("Illegal Trailer in Country", "Default: 1500 € | Base fine for pulling a trailer type restricted in the current country (e.g. HCT outside Scandinavia)."),
            ["PoliceFineAmountSettings.AvoidInspection"] = ("Skipping Border Inspection", "Default: 900 € | Base fine for bypassing a mandatory customs/X-ray inspection at borders."),
            ["PoliceFineAmountSettings.IllegalBorderCrossing"] = ("Illegal Border Crossing", "Default: 2000 € | Base fine for driving through a closed border lane without clearance."),
            ["PoliceFineAmountSettings.HardShoulderViolation"] = ("Driving on Hard Shoulder", "Default: 150 € | Base fine for overtaking or driving on the highway emergency lane."),
            ["PoliceFineAmountSettings.DamagedVehicleUsage"] = ("Driving a Heavily Damaged Truck", "Default: 500 € | Base fine during police checks if your vehicle has severe damage."),
            ["PoliceFineAmountSettings.IllegalTrailerWeightStation"] = ("Illegal Trailer at Weigh Station", "Default: 1500 € | Base fine when an unauthorized trailer is detected on the scales."),

            ["PoliceCheckDelaySettings.CarCrash"] = ("Crash", "Default: 0 Sec | Delay before fine (0 = checked immediately upon impact)."),
            ["PoliceCheckDelaySettings.AvoidSleeping"] = ("Fatigue Violation", "Default: 60 Sec | How many real seconds you can keep driving with an empty rest bar before the next fine check."),
            ["PoliceCheckDelaySettings.WrongWay"] = ("Wrong Way", "Default: 15 Sec | How many seconds you can drive in the oncoming lane (e.g. while overtaking) before risking a fine."),
            ["PoliceCheckDelaySettings.SpeedingCamera"] = ("Speed Camera", "Default: 0 Sec | Speed cameras trigger immediately when passing."),
            ["PoliceCheckDelaySettings.NoLightsNight"] = ("No Lights at Night", "Default: 70 Sec | After how many seconds of darkness without lights a fine is triggered."),
            ["PoliceCheckDelaySettings.RedLights"] = ("Red Light", "Default: 0 Sec | Checked immediately when crossing the stop line."),
            ["PoliceCheckDelaySettings.Speeding"] = ("Speeding", "Default: 60 Sec | Time interval in seconds for automatic speed checks without a patrol car."),
            ["PoliceCheckDelaySettings.AvoidWeighing"] = ("Skipping Weigh Station", "Default: 0 Sec | Triggered immediately when driving past the station."),
            ["PoliceCheckDelaySettings.IllegalTrailer"] = ("Illegal Trailer", "Default: 600 Sec (10 min) | Time interval between two checks for illegal trailers."),

            ["PolicePoliceCheckDelaySettings.CarCrash"] = ("Crash Near Police", "Default: 0 Sec | Immediate fine if a police patrol witnesses the crash."),
            ["PolicePoliceCheckDelaySettings.AvoidSleeping"] = ("Fatigue Near Police", "Default: 30 Sec | Check delay in seconds when driving fatigued next to a patrol car."),
            ["PolicePoliceCheckDelaySettings.WrongWay"] = ("Wrong Way Near Police", "Default: 5 Sec | How many seconds you can be in the oncoming lane in front of a police car."),
            ["PolicePoliceCheckDelaySettings.SpeedingCamera"] = ("Speed Camera Near Police", "Default: 0 Sec | Immediate trigger."),
            ["PolicePoliceCheckDelaySettings.NoLightsNight"] = ("No Lights Near Police", "Default: 7 Sec | Driving 7 seconds without lights next to a patrol car triggers a fine."),
            ["PolicePoliceCheckDelaySettings.RedLights"] = ("Red Light Near Police", "Default: 0 Sec | Immediate fine when running a red light in front of a patrol car."),
            ["PolicePoliceCheckDelaySettings.Speeding"] = ("Speeding Near Police", "Default: 3 Sec | How many seconds you can exceed the speed limit next to a police car before getting fined (Tip: 6–8 sec prevents unfair fines right after city limit signs!)."),
            ["PolicePoliceCheckDelaySettings.AvoidWeighing"] = ("Weigh Station Near Police", "Default: 0 Sec | Immediate fine."),
            ["PolicePoliceCheckDelaySettings.IllegalTrailer"] = ("Illegal Trailer Near Police", "Default: 300 Sec (5 min) | Delay when pulling an unauthorized trailer next to a patrol car."),

            ["PoliceProbabilitySettings.CarCrash"] = ("Crash", "Default: 0.7 (70%) | Probability of a fine in an accident without police nearby.\n• Realism Tip: Set this (and Red Light/Wrong Way/Lights) to '0.0' so you are only fined when an actual police car or camera is nearby!"),
            ["PoliceProbabilitySettings.AvoidSleeping"] = ("Fatigue Violation", "Default: 0.3 (30%) | Probability of an automatic fine (digital tachograph) when exceeding driving time."),
            ["PoliceProbabilitySettings.WrongWay"] = ("Wrong Way", "Default: 0.6 (60%) | Probability of an automatic fine in oncoming traffic without police nearby (0.0 = police only)."),
            ["PoliceProbabilitySettings.SpeedingCamera"] = ("Speed Camera", "Default: 1.0 (100%) | Probability that an active speed camera actually flashes (e.g. 0.85 = 15% of cameras are dummy boxes)."),
            ["PoliceProbabilitySettings.NoLightsNight"] = ("No Lights", "Default: 0.3 (30%) | Probability of a fine for driving without lights when no police is nearby (0.0 = police only)."),
            ["PoliceProbabilitySettings.RedLights"] = ("Red Light", "Default: 0.7 (70%) | Probability of a fine at red lights without police on site (0.0 = no invisible red-light cameras)."),
            ["PoliceProbabilitySettings.Speeding"] = ("Speeding (Without Camera/Police)", "Default: 0.0 (0%) | Should stay at 0.0 so you don't get fined out of nowhere on open roads without cameras or police."),
            ["PoliceProbabilitySettings.AvoidWeighing"] = ("Skipping Weigh Station", "Default: 1.0 (100%) | Probability of a fine when ignoring a weigh station."),
            ["PoliceProbabilitySettings.IllegalTrailer"] = ("Illegal Trailer", "Default: 0.0 (0%) | Probability of an automatic fine without police/weigh station."),
            ["PoliceProbabilitySettings.AvoidInspection"] = ("Skipping Inspection", "Default: 1.0 (100%) | Probability of a fine when bypassing a border inspection."),
            ["PoliceProbabilitySettings.IllegalBorderCrossing"] = ("Illegal Border Crossing", "Default: 1.0 (100%) | Probability of a fine at borders."),
            ["PoliceProbabilitySettings.HardShoulderViolation"] = ("Hard Shoulder", "Default: 1.0 (100%) | Probability of a fine on the emergency lane without police nearby (0.0 = police only)."),
            ["PoliceProbabilitySettings.DamagedVehicleUsage"] = ("Damaged Vehicle", "Default: 1.0 (100%) | Probability of a fine for heavily damaged trucks at checkpoints."),
            ["PoliceProbabilitySettings.IllegalTrailerWeightStation"] = ("Illegal Trailer at Weigh Station", "Default: 1.0 (100%) | Probability that an illegal trailer is caught on the scales."),

            ["PolicePoliceProbabilitySettings.CarCrash"] = ("Crash Near Police", "Default: 1.0 (100%) | Probability of a fine when a patrol car directly witnesses your accident."),
            ["PolicePoliceProbabilitySettings.AvoidSleeping"] = ("Fatigue Near Police", "Default: 0.3 (30%) | Probability that a passing patrol car checks your driving hours."),
            ["PolicePoliceProbabilitySettings.WrongWay"] = ("Wrong Way Near Police", "Default: 0.7 (70%) | Probability of a fine for wrong-way driving in front of a patrol car."),
            ["PolicePoliceProbabilitySettings.SpeedingCamera"] = ("Speed Camera Near Police", "Default: 1.0 (100%) | Trigger probability at speed cameras."),
            ["PolicePoliceProbabilitySettings.NoLightsNight"] = ("No Lights Near Police", "Default: 0.8 (80%) | Probability that a patrol car fines you for missing headlights at night."),
            ["PolicePoliceProbabilitySettings.RedLights"] = ("Red Light Near Police", "Default: 1.0 (100%) | Probability of a fine when running a red light in front of a patrol car."),
            ["PolicePoliceProbabilitySettings.Speeding"] = ("Speeding Near Police", "Default: 0.9 (90%) | Probability that a patrol car in traffic fines you for speeding."),
            ["PolicePoliceProbabilitySettings.AvoidWeighing"] = ("Weigh Station Near Police", "Default: 1.0 (100%) | Enforcement probability at weigh stations."),
            ["PolicePoliceProbabilitySettings.IllegalTrailer"] = ("Illegal Trailer Near Police", "Default: 0.3 (30%) | Probability that a police patrol notices your unauthorized trailer."),
            ["PolicePoliceProbabilitySettings.AvoidInspection"] = ("Inspection Near Police", "Default: 1.0 (100%) | Enforcement probability when skipping inspection."),
            ["PolicePoliceProbabilitySettings.IllegalBorderCrossing"] = ("Border Crossing Near Police", "Default: 1.0 (100%) | Enforcement probability at borders."),
            ["PolicePoliceProbabilitySettings.HardShoulderViolation"] = ("Hard Shoulder Near Police", "Default: 1.0 (100%) | Probability of a fine when using the emergency lane in front of police."),
            ["PolicePoliceProbabilitySettings.DamagedVehicleUsage"] = ("Damaged Vehicle Near Police", "Default: 1.0 (100%) | Probability that a patrol car fines your heavily damaged truck."),
            ["PolicePoliceProbabilitySettings.IllegalTrailerWeightStation"] = ("Illegal Trailer at Weigh Station Near Police", "Default: 1.0 (100%) | Enforcement probability on the scales."),

            // --- 5. ServiceSettings ---
            ["ServiceSettings.Truck"] = ("1. Truck Complete Restoration", "Surcharges for a full overhaul of a worn-out truck (removes permanent unfixable wear)."),
            ["ServiceSettings.Trailer"] = ("2. Trailer Complete Restoration", "Surcharges for a full overhaul of an owned trailer at the service shop."),

            ["ServiceTruckSettings.RestoreFixedPrice"] = ("Truck Base Restoration Price (€)", "Default: 0 € | One-time flat fee in Euro for 'Complete Restoration' of the truck at the service station."),
            ["ServiceTruckSettings.RestoreAccessoryFixedPrice"] = ("Truck Fee per Component (€)", "Default: 1000 € | Fixed labor surcharge per affected main component (engine, transmission, chassis, cabin, wheels) during restoration."),
            ["ServiceTruckSettings.RestoreAccessoryPriceCoef"] = ("Truck Component Price Factor", "Default: 1.1 (110%) | Multiplier on the new price of restored parts (1.1 = restoration costs 110% of part value, 0.5 = 50%)."),

            ["ServiceTrailerSettings.RestoreFixedPrice"] = ("Trailer Base Restoration Price (€)", "Default: 0 € | One-time flat fee in Euro for 'Complete Restoration' of an owned trailer."),
            ["ServiceTrailerSettings.RestoreAccessoryFixedPrice"] = ("Trailer Fee per Component (€)", "Default: 1000 € | Fixed labor surcharge per affected trailer component during restoration."),
            ["ServiceTrailerSettings.RestoreAccessoryPriceCoef"] = ("Trailer Component Price Factor", "Default: 1.1 (110%) | Multiplier on the part value during trailer restoration."),

            // --- 6. SkillSettings ---
            ["SkillSettings.LongDistance"] = ("1. Long Distance", "Unlocks longer routes and grants XP and revenue bonuses for long-haul trips."),
            ["SkillSettings.Valuable"] = ("2. High Value Cargo", "Unlocks high-value cargo and increases XP and payout for valuable goods."),
            ["SkillSettings.Fragile"] = ("3. Fragile Cargo", "Unlocks delicate cargo and increases XP and payout for fragile goods."),
            ["SkillSettings.Urgent"] = ("4. Just-In-Time Delivery", "Unlocks important and urgent deliveries and boosts their payout."),
            ["SkillSettings.EcoDriving"] = ("5. Eco-Driving", "Reduces fuel consumption for you and your hired AI drivers per rank."),
            ["SkillSettings.ADR"] = ("6. Hazardous Materials (ADR)", "XP bonus for delivering hazardous cargo across the 6 ADR classes."),
            ["SkillSettings.BusLongDuty"] = ("🚌 7. Bus DLC: Long Duty", "Unlocks bus lines with more stops and increases XP and ticket revenue (3 ranks)."),
            ["SkillSettings.BusTightSchedule"] = ("🚌 8. Bus DLC: Tight Schedule", "Unlocks demanding bus schedules with tighter timetables and higher XP bonuses (3 ranks)."),
            ["SkillSettings.BusSatisfactionDecrease"] = ("🚌 9. Bus DLC: Passenger Tolerance (Satisfaction Decrease)", "Reduces passenger satisfaction penalties during minor driving mistakes and boosts ticket revenue (3 ranks)."),
            ["SkillSettings.BusLowAcceleration"] = ("🚌 10. Bus DLC: Smooth Driving (Low Acceleration)", "Lowers passenger sensitivity to harsh braking/acceleration and grants XP & revenue bonuses (3 ranks)."),
            ["SkillSettings.BusSatisfactionIncrease"] = ("🚌 11. Bus DLC: Passenger Delight (Satisfaction Increase)", "Boosts positive passenger satisfaction gained during a smooth and pleasant bus ride (3 ranks)."),
            ["SkillSettings.BusEcoDriving"] = ("🚌 12. Bus DLC: Bus Eco-Driving", "Reduces fuel consumption when driving coaches/buses per unlocked rank (3 ranks)."),

            ["SkillRankValues.Rank1"] = ("Rank 1", ""),
            ["SkillRankValues.Rank2"] = ("Rank 2", ""),
            ["SkillRankValues.Rank3"] = ("Rank 3", ""),
            ["SkillRankValues.Rank4"] = ("Rank 4", ""),
            ["SkillRankValues.Rank5"] = ("Rank 5", ""),
            ["SkillRankValues.Rank6"] = ("Rank 6", ""),

            ["BusSkillRankValues.Rank1"] = ("Rank 1", ""),
            ["BusSkillRankValues.Rank2"] = ("Rank 2", ""),
            ["BusSkillRankValues.Rank3"] = ("Rank 3", ""),

            ["LongDistanceSkillSettings.Xp"] = ("XP Bonus per Rank", "Default: 0.25 (+25%) | Additional XP bonus for long-distance deliveries over 250 km at the respective skill rank."),
            ["LongDistanceSkillSettings.Revenue"] = ("Revenue Bonus per Rank", "Default: 0.05 to 0.30 (+5% to +30%) | Additional revenue bonus for long-distance deliveries at the respective rank."),
            ["LongDistanceSkillSettings.Distances"] = ("Max Distance per Rank (km)", "Default: 400 to 4000 km | Maximum job distance generated in the freight market at this rank (before Rank 1 the limit is 250 km)."),

            ["ValuableSkillSettings.Xp"] = ("XP Bonus per Rank", "Default: 0.18 (+18%) | Additional XP bonus for delivering high-value cargo."),
            ["ValuableSkillSettings.Revenue"] = ("Revenue Bonus per Rank", "Default: 0.05 to 0.30 (+5% to +30%) | Additional payout bonus for high-value cargo per rank reached."),

            ["FragileSkillSettings.Xp"] = ("XP Bonus per Rank", "Default: 0.22 (+22%) | Additional XP bonus for delivering fragile cargo."),
            ["FragileSkillSettings.Revenue"] = ("Revenue Bonus per Rank", "Default: 0.05 to 0.30 (+5% to +30%) | Additional payout bonus for fragile cargo per rank reached."),

            ["UrgentSkillSettings.Xp"] = ("XP Bonus per Rank (Urgent Jobs)", "Default: 0.30 (+30%) | Additional XP bonus for urgent Just-In-Time deliveries (available from Rank 2)."),
            ["UrgentSkillSettings.Revenue"] = ("Revenue Bonus per Rank (Urgent Jobs)", "Default: 0.05 to 0.30 (+5% to +30%) | Additional payout bonus for urgent Just-In-Time deliveries."),
            ["UrgentSkillSettings.ImportantJobXpBonusCoef"] = ("Important Jobs: XP Share", "Default: 0.66 (66%) | Share of the urgent XP bonus already granted to 'Important Deliveries' (available from Rank 1)."),
            ["UrgentSkillSettings.ImportantJobRevenueBonusCoef"] = ("Important Jobs: Revenue Share", "Default: 0.60 (60%) | Share of the urgent revenue bonus already granted to 'Important Deliveries'."),

            ["EcoDrivingSkillSettings.Saved"] = ("Fuel Savings per Rank", "Default: 0.10 to 0.35 (10% to 35%) | Percentage fuel reduction per skill rank.\n• Note: Strongly affects AI drivers and simple fuel simulation (lower to e.g. 0.02–0.12 for realistic consumption)."),

            ["AdrSkillSettings.Xp"] = ("XP Bonus per Unlocked ADR Class", "Default: 0.21 (+21%) | Additional XP bonus when hauling hazardous materials (Classes: Explosives, Gases, Flammable Liquids, Flammable Solids, Toxic, Corrosive)."),

            ["BusLongDutySkillSettings.Xp"] = ("XP Bonus per Rank", "Default: 0.05 to 0.15 (+5% to +15%) | Additional XP bonus for long bus routes with multiple stops on Ranks 1 to 3."),
            ["BusLongDutySkillSettings.Revenue"] = ("Revenue Bonus per Rank", "Default: 0.05 to 0.15 (+5% to +15%) | Additional revenue bonus for long bus lines per unlocked rank."),
            ["BusLongDutySkillSettings.MaxStops"] = ("Max Stops per Route per Rank", "Default: 4, 6, 8 Stops | Maximum number of stops on a bus route unlocked at the respective skill rank."),

            ["BusTightScheduleSkillSettings.Xp"] = ("XP Bonus per Rank", "Default: 0.10, 0.20, 0.40 (+10% to +40%) | Additional XP bonus for completing bus routes with tight timetables."),
            ["BusTightScheduleSkillSettings.LessTime"] = ("Schedule Time Reduction per Rank", "Default: 0.05, 0.10, 0.15 (-5% to -15%) | By what percentage the scheduled driving time window is shortened per rank."),

            ["BusSatisfactionDecreaseSkillSettings.Revenue"] = ("Revenue Bonus per Rank", "Default: 0.10, 0.20, 0.30 (+10% to +30%) | Additional payout bonus for bus trips with demanding passengers."),
            ["BusSatisfactionDecreaseSkillSettings.NegativeSatisfactionReduction"] = ("Dissatisfaction Reduction per Rank", "Default: 0.20, 0.40, 0.60 (-20% to -60%) | Dampens passenger satisfaction loss caused by delays or minor driving mistakes."),

            ["BusLowAccelerationSkillSettings.Xp"] = ("XP Bonus per Rank", "Default: 0.05, 0.10, 0.15 (+5% to +15%) | Additional XP bonus for smooth, comfortable coach driving."),
            ["BusLowAccelerationSkillSettings.Revenue"] = ("Revenue Bonus per Rank", "Default: 0.05, 0.10, 0.15 (+5% to +15%) | Additional payout bonus for smooth braking and acceleration."),
            ["BusLowAccelerationSkillSettings.SensitivityDecrease"] = ("G-Force Sensitivity Reduction per Rank", "Default: 0.10, 0.20, 0.30 (-10% to -30%) | Reduces how strongly passengers react to hard braking, acceleration or sharp cornering."),

            ["BusSatisfactionIncreaseSkillSettings.PositiveSatisfactionIncrease"] = ("Positive Satisfaction Boost per Rank", "Default: 0.10, 0.20, 0.30 (+10% to +30%) | Increases positive passenger satisfaction gained during a pleasant bus trip."),

            ["BusEcoDrivingSkillSettings.Saved"] = ("Bus Fuel Savings per Rank", "Default: 0.10, 0.20, 0.30 (10% to 30%) | Percentage fuel reduction when driving coaches/buses on Ranks 1 to 3."),

            // --- 7. SpecialEventSettings ---
            ["SpecialEventSettings.Unlocks"] = ("1. Unlocks & Game Start", "When banks, truck dealers and trailer ownership are unlocked for a new profile."),
            ["SpecialEventSettings.Credit"] = ("2. Total Credit Limit & Tiers", "Maximum total debt allowed by the bank depending on your player level."),

            ["SpecialEventUnlocksSettings.JobsBeforeFirstLoan"] = ("Jobs Until 1st Loan Unlock", "Default: 2 Jobs | How many quick jobs you must complete in a new profile before the bank sends the first loan email."),
            ["SpecialEventUnlocksSettings.JobsBeforeCompanyInvitation"] = ("Jobs Until Company Invitation", "Default: 2 Jobs | After how many trips other companies email you invitations to keep driving for them."),
            ["SpecialEventUnlocksSettings.FirstDealerUnlockDelay"] = ("1st Truck Dealer Delay (Seconds)", "Default: 120 Sec (2 min) | Waiting time after starting a profile until the email unlocking your preferred brand's truck dealer arrives."),
            ["SpecialEventUnlocksSettings.FirstJobPreferredRadius"] = ("Preferred Radius for 1st Job (km)", "Default: 0 (unlimited) | Maximum distance for the very first introductory job of a new profile."),
            ["SpecialEventUnlocksSettings.TrailersUnlockLevel"] = ("Trailer Dealer Unlock Level", "Default: 5 | Player level at which the trailer dealer unlocks so you can buy your own trailers (1 = unlocked immediately)."),

            ["SpecialEventCreditSettings.LevelTier1"] = ("Tier 1: Required Player Level", "Default: 0 | Player level for the initial credit limit (0 = applies immediately from the first loan offer)."),
            ["SpecialEventCreditSettings.LimitTier1"] = ("Tier 1: Maximum Credit Limit (€)", "Default: 100,000 € | Maximum sum of all active loans combined at Tier 1.\n• Important: If you increase Loan 1/2 under '1. Bank', make sure to raise this limit too!"),
            ["SpecialEventCreditSettings.LevelTier2"] = ("Tier 2: Required Player Level", "Default: 3 | Player level at which the bank raises your maximum credit line to Tier 2."),
            ["SpecialEventCreditSettings.LimitTier2"] = ("Tier 2: Maximum Credit Limit (€)", "Default: 500,000 € | Maximum sum of all active loans combined at Tier 2.\n• Important: If you set loans above 500,000 € under '1. Bank', you must raise this limit as well (e.g. to 1000000), otherwise those loans will be greyed out in-game!"),

            // --- 8. UsedVehicleSettings ---
            ["UsedVehicleSettings.Generation"] = ("1. Offer Generation & Count", "Controls how often new used trucks appear at the dealer and how many are available simultaneously."),
            ["UsedVehicleSettings.Odometer"] = ("2. Mileage (Odometer)", "Rules for the odometer reading (kilometers driven) of offered used trucks."),
            ["UsedVehicleSettings.Wear"] = ("3. Fixable Wear at Purchase", "Normal wear (0.0 to 1.0) on used trucks that can be cheaply repaired at the service shop."),
            ["UsedVehicleSettings.UnfixableWear"] = ("4. Unfixable Permanent Wear at Purchase", "Permanent wear (0.0 to 1.0) on used trucks that can only be removed via part replacement or full restoration."),
            ["UsedVehicleSettings.Price"] = ("5. Price Factors & Discounts", "How strongly mileage and wear reduce the purchase price of a used truck."),

            ["UsedVehicleGenerationSettings.TruckGenerationGameTimeMin"] = ("Generation Time Min (Minutes)", "Default: 1440 Min (24 hrs) | Earliest in-game time in minutes after which new used truck offers are generated."),
            ["UsedVehicleGenerationSettings.TruckGenerationGameTimeMax"] = ("Generation Time Max (Minutes)", "Default: 2160 Min (36 hrs) | Latest in-game time in minutes until new used truck offers appear."),
            ["UsedVehicleGenerationSettings.TruckExpirationGameTimeMin"] = ("Expiration Time Min (Minutes)", "Default: 2880 Min (2 days) | Minimum duration in in-game minutes a used truck stays on offer."),
            ["UsedVehicleGenerationSettings.TruckExpirationGameTimeMax"] = ("Expiration Time Max (Minutes)", "Default: 4320 Min (3 days) | Maximum duration in in-game minutes before an offer disappears from the market."),
            ["UsedVehicleGenerationSettings.TruckCountMin"] = ("Vehicle Count Min", "Default: 12 Trucks | Minimum number of used trucks offered simultaneously at the dealer."),
            ["UsedVehicleGenerationSettings.TruckCountMax"] = ("Vehicle Count Max", "Default: 18 Trucks | Maximum number of used trucks offered simultaneously (Tip: increase to e.g. 25–30 for a larger selection!)."),

            ["UsedVehicleOdometerSettings.TruckOdometerLimitValues"] = ("Mileage Limit for Beginners (km)", "Default: 100,000 km | Maximum mileage of offered used trucks for players below the limit level."),
            ["UsedVehicleOdometerSettings.TruckOdometerLimitLevels"] = ("Player Level for Mileage Limit", "Default: Level 3 | Up to this player level, the beginner mileage cap above applies."),
            ["UsedVehicleOdometerSettings.TruckOdometerRangeCoef"] = ("Mileage Spread Factor", "Default: 0.9 (90%) | Determines how widely the odometer readings of offers vary."),
            ["UsedVehicleOdometerSettings.DealerTruckOdometerMin"] = ("Dealer Min Mileage (km)", "Default: 1,000 km | Lowest possible odometer reading of a used truck (near-new demo truck)."),
            ["UsedVehicleOdometerSettings.DealerTruckOdometerMax"] = ("Dealer Max Mileage (km)", "Default: 100,000 km | Reference upper bound for mileage calculation at the used truck dealer."),

            ["UsedVehicleWearSettings.ChassisMin"] = ("Chassis Wear: Min", "Default: 0.0 (0%) | Minimum normal (cheaply fixable) chassis wear at purchase."),
            ["UsedVehicleWearSettings.ChassisMax"] = ("Chassis Wear: Max", "Default: 0.0 (0%) | Maximum normal chassis wear at purchase (e.g. 0.2 = up to 20%)."),
            ["UsedVehicleWearSettings.WheelsMin"] = ("Wheels Wear: Min", "Default: 0.0 (0%) | Minimum normal tire wear at purchase."),
            ["UsedVehicleWearSettings.WheelsMax"] = ("Wheels Wear: Max", "Default: 0.0 (0%) | Maximum normal tire wear at purchase."),
            ["UsedVehicleWearSettings.EngineMin"] = ("Engine Wear: Min", "Default: 0.0 (0%) | Minimum normal engine wear at purchase."),
            ["UsedVehicleWearSettings.EngineMax"] = ("Engine Wear: Max", "Default: 0.0 (0%) | Maximum normal engine wear at purchase."),
            ["UsedVehicleWearSettings.TransmissionMin"] = ("Transmission Wear: Min", "Default: 0.0 (0%) | Minimum normal transmission wear at purchase."),
            ["UsedVehicleWearSettings.TransmissionMax"] = ("Transmission Wear: Max", "Default: 0.0 (0%) | Maximum normal transmission wear at purchase."),
            ["UsedVehicleWearSettings.CabinMin"] = ("Cabin Wear: Min", "Default: 0.0 (0%) | Minimum normal cabin wear at purchase."),
            ["UsedVehicleWearSettings.CabinMax"] = ("Cabin Wear: Max", "Default: 0.0 (0%) | Maximum normal cabin wear at purchase."),

            ["UsedVehicleUnfixableWearSettings.LimitValues"] = ("Permanent Wear Limit for Beginners", "Default: 0.15 (15%) | Maximum permanent wear for players below the limit level."),
            ["UsedVehicleUnfixableWearSettings.LimitLevels"] = ("Player Level for Permanent Wear Limit", "Default: Level 3 | From this level onwards, more heavily worn used trucks (up to the Max values below) can appear."),
            ["UsedVehicleUnfixableWearSettings.ChassisMin"] = ("Chassis Permanent Wear: Min", "Default: 0.0 (0%) | Minimum permanent wear on the chassis."),
            ["UsedVehicleUnfixableWearSettings.ChassisMax"] = ("Chassis Permanent Wear: Max", "Default: 0.4 (40%) | Maximum permanent wear on the chassis (0.4 = up to 40% permanent wear)."),
            ["UsedVehicleUnfixableWearSettings.WheelsMin"] = ("Wheels Permanent Wear: Min", "Default: 0.0 (0%) | Minimum permanent tire wear."),
            ["UsedVehicleUnfixableWearSettings.WheelsMax"] = ("Wheels Permanent Wear: Max", "Default: 0.4 (40%) | Maximum permanent tire wear."),
            ["UsedVehicleUnfixableWearSettings.EngineMin"] = ("Engine Permanent Wear: Min", "Default: 0.0 (0%) | Minimum permanent engine wear."),
            ["UsedVehicleUnfixableWearSettings.EngineMax"] = ("Engine Permanent Wear: Max", "Default: 0.4 (40%) | Maximum permanent engine wear."),
            ["UsedVehicleUnfixableWearSettings.TransmissionMin"] = ("Transmission Permanent Wear: Min", "Default: 0.0 (0%) | Minimum permanent transmission wear."),
            ["UsedVehicleUnfixableWearSettings.TransmissionMax"] = ("Transmission Permanent Wear: Max", "Default: 0.4 (40%) | Maximum permanent transmission wear."),
            ["UsedVehicleUnfixableWearSettings.CabinMin"] = ("Cabin Permanent Wear: Min", "Default: 0.0 (0%) | Minimum permanent cabin wear."),
            ["UsedVehicleUnfixableWearSettings.CabinMax"] = ("Cabin Permanent Wear: Max", "Default: 0.4 (40%) | Maximum permanent cabin wear."),

            ["UsedVehiclePriceSettings.FactorIntegrityWear"] = ("Price Factor: Overall Condition / Mileage", "Default: 3.0 | Determines how strongly high mileage lowers the price of a used truck (higher value = cheaper prices at high mileage)."),
            ["UsedVehiclePriceSettings.FactorWearUnfixable"] = ("Price Factor: Permanent Wear", "Default: 2.0 | Determines how strongly unfixable damage discounts the purchase price."),
            ["UsedVehiclePriceSettings.FactorWear"] = ("Price Factor: Normal Wear", "Default: 1.0 | Determines how strongly fixable damage lowers the purchase price."),
            ["UsedVehiclePriceSettings.MinAccessoryPriceCoef"] = ("Minimum Component Price Factor", "Default: 0.1 (10%) | Even a completely worn-out truck costs at least this fraction (10%) of its new price."),
            ["UsedVehiclePriceSettings.PriceRangeCoef"] = ("Random Price Variance", "Default: 0.2 (±20%) | Random price fluctuation up/down so identical used trucks can be different bargains.")
        };
    }
}
