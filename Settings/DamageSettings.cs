using System.ComponentModel;
using System.Text.RegularExpressions;

namespace ETS2_ModTool
{
    public class DamageSettings
    {
        [Category("Schaden (damage_data.sii)")]
        [DisplayName("Kollision & Allgemeine Faktoren")]
        [Description("Grundregeln für Unfälle und Aufprallschäden.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public DamageCollisionSettings Collision { get; set; } = new DamageCollisionSettings();

        [Category("Schaden (damage_data.sii)")]
        [DisplayName("LKW: Schadensverteilung & Totalschaden")]
        [Description("Wie sich Schäden auf LKW-Teile verteilen und welche Anteile unreparierbar sind.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public DamageTruckDistSettings TruckDist { get; set; } = new DamageTruckDistSettings();

        [Category("Schaden (damage_data.sii)")]
        [DisplayName("Anhänger: Schadensverteilung & Totalschaden")]
        [Description("Verteilung und unreparierbare Anteile für Anhänger und Fracht.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public DamageTrailerDistSettings TrailerDist { get; set; } = new DamageTrailerDistSettings();

        [Category("Schaden (damage_data.sii)")]
        [DisplayName("LKW: Verschleiß pro km")]
        [Description("Normaler Verschleiß und unreparierbarer Abrieb pro Kilometer für den LKW.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public DamageTruckWearSettings TruckWear { get; set; } = new DamageTruckWearSettings();

        [Category("Schaden (damage_data.sii)")]
        [DisplayName("Anhänger: Verschleiß pro km")]
        [Description("Verschleißwerte für den Anhänger.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public DamageTrailerWearSettings TrailerWear { get; set; } = new DamageTrailerWearSettings();

        public void Import(string content)
        {
            // Kollision
            Collision.TruckDamageCoef = ExtractValue(content, "truck_damage_coef", Collision.TruckDamageCoef);
            Collision.TrailerDamageCoef = ExtractValue(content, "trailer_damage_coef", Collision.TrailerDamageCoef);
            Collision.DraggedTrailerDamageCoef = ExtractValue(content, "dragged_trailer_damage_coef", Collision.DraggedTrailerDamageCoef);
            Collision.TruckToTrailerDmg = ExtractValue(content, "truck_to_trailer_dmg", Collision.TruckToTrailerDmg);
            Collision.SideDamageFactor = ExtractValue(content, "side_damage_factor", Collision.SideDamageFactor);
            Collision.RoofDamageFactor = ExtractValue(content, "roof_damage_factor", Collision.RoofDamageFactor);

            // LKW Verteilung
            TruckDist.CabinDamageRatio = ExtractValue(content, "cabin_damage_ratio", TruckDist.CabinDamageRatio);
            TruckDist.ChassisDamageRatio = ExtractValue(content, "chassis_damage_ratio", TruckDist.ChassisDamageRatio);
            TruckDist.EngineDamageRatio = ExtractValue(content, "engine_damage_ratio", TruckDist.EngineDamageRatio);
            TruckDist.TransmissionDamageRatio = ExtractValue(content, "transmission_damage_ratio", TruckDist.TransmissionDamageRatio);
            TruckDist.WheelDamageRatio = ExtractValue(content, "wheel_damage_ratio", TruckDist.WheelDamageRatio);

            TruckDist.CabinUnfixable = ExtractValue(content, "cabin_unfixable_damage_ratio", TruckDist.CabinUnfixable);
            TruckDist.ChassisUnfixable = ExtractValue(content, "chassis_unfixable_damage_ratio", TruckDist.ChassisUnfixable);
            TruckDist.EngineUnfixable = ExtractValue(content, "engine_unfixable_damage_ratio", TruckDist.EngineUnfixable);
            TruckDist.TransmissionUnfixable = ExtractValue(content, "transmission_unfixable_damage_ratio", TruckDist.TransmissionUnfixable);
            TruckDist.WheelUnfixable = ExtractValue(content, "wheel_unfixable_damage_ratio", TruckDist.WheelUnfixable);

            // Anhänger Verteilung
            TrailerDist.TrailerBodyDamageRatio = ExtractValue(content, "trailer_body_damage_ratio", TrailerDist.TrailerBodyDamageRatio);
            TrailerDist.TrailerChassisDamageRatio = ExtractValue(content, "trailer_chassis_damage_ratio", TrailerDist.TrailerChassisDamageRatio);
            TrailerDist.TrailerWheelDamageRatio = ExtractValue(content, "trailer_wheel_damage_ratio", TrailerDist.TrailerWheelDamageRatio);
            TrailerDist.CargoDamageRatio = ExtractValue(content, "cargo_damage_ratio", TrailerDist.CargoDamageRatio);

            TrailerDist.TrailerBodyUnfixable = ExtractValue(content, "trailer_body_unfixable_damage_ratio", TrailerDist.TrailerBodyUnfixable);
            TrailerDist.TrailerChassisUnfixable = ExtractValue(content, "trailer_chassis_unfixable_damage_ratio", TrailerDist.TrailerChassisUnfixable);
            TrailerDist.TrailerWheelUnfixable = ExtractValue(content, "trailer_wheel_unfixable_damage_ratio", TrailerDist.TrailerWheelUnfixable);

            // LKW Verschleiß
            TruckWear.CabinWear = ExtractValue(content, "cabin_wear", TruckWear.CabinWear);
            TruckWear.ChassisWear = ExtractValue(content, "chassis_wear", TruckWear.ChassisWear);
            TruckWear.EngineWear = ExtractValue(content, "engine_wear", TruckWear.EngineWear);
            TruckWear.TransmissionWear = ExtractValue(content, "transmission_wear", TruckWear.TransmissionWear);
            TruckWear.WheelWear = ExtractValue(content, "wheel_wear", TruckWear.WheelWear);

            TruckWear.CabinWearUnfixable = ExtractValue(content, "cabin_wear_unfixable", TruckWear.CabinWearUnfixable);
            TruckWear.ChassisWearUnfixable = ExtractValue(content, "chassis_wear_unfixable", TruckWear.ChassisWearUnfixable);
            TruckWear.EngineWearUnfixable = ExtractValue(content, "engine_wear_unfixable", TruckWear.EngineWearUnfixable);
            TruckWear.TransmissionWearUnfixable = ExtractValue(content, "transmission_wear_unfixable", TruckWear.TransmissionWearUnfixable);
            TruckWear.WheelWearUnfixable = ExtractValue(content, "wheel_wear_unfixable", TruckWear.WheelWearUnfixable);

            // Anhänger Verschleiß
            TrailerWear.TrailerBodyWear = ExtractValue(content, "trailer_body_wear", TrailerWear.TrailerBodyWear);
            TrailerWear.TrailerChassisWear = ExtractValue(content, "trailer_chassis_wear", TrailerWear.TrailerChassisWear);
            TrailerWear.TrailerWheelWear = ExtractValue(content, "trailer_wheel_wear", TrailerWear.TrailerWheelWear);

            TrailerWear.TrailerBodyWearUnfixable = ExtractValue(content, "trailer_body_wear_unfixable", TrailerWear.TrailerBodyWearUnfixable);
            TrailerWear.TrailerChassisWearUnfixable = ExtractValue(content, "trailer_chassis_wear_unfixable", TrailerWear.TrailerChassisWearUnfixable);
            TrailerWear.TrailerWheelWearUnfixable = ExtractValue(content, "trailer_wheel_wear_unfixable", TrailerWear.TrailerWheelWearUnfixable);
        }

        public void Export(ref string content)
        {
            // Kollision schreiben
            UpdateParameter(ref content, "truck_damage_coef", Collision.TruckDamageCoef);
            UpdateParameter(ref content, "trailer_damage_coef", Collision.TrailerDamageCoef);
            UpdateParameter(ref content, "dragged_trailer_damage_coef", Collision.DraggedTrailerDamageCoef);
            UpdateParameter(ref content, "truck_to_trailer_dmg", Collision.TruckToTrailerDmg);
            UpdateParameter(ref content, "side_damage_factor", Collision.SideDamageFactor);
            UpdateParameter(ref content, "roof_damage_factor", Collision.RoofDamageFactor);

            // LKW Verteilung schreiben
            UpdateParameter(ref content, "cabin_damage_ratio", TruckDist.CabinDamageRatio);
            UpdateParameter(ref content, "chassis_damage_ratio", TruckDist.ChassisDamageRatio);
            UpdateParameter(ref content, "engine_damage_ratio", TruckDist.EngineDamageRatio);
            UpdateParameter(ref content, "transmission_damage_ratio", TruckDist.TransmissionDamageRatio);
            UpdateParameter(ref content, "wheel_damage_ratio", TruckDist.WheelDamageRatio);

            UpdateParameter(ref content, "cabin_unfixable_damage_ratio", TruckDist.CabinUnfixable);
            UpdateParameter(ref content, "chassis_unfixable_damage_ratio", TruckDist.ChassisUnfixable);
            UpdateParameter(ref content, "engine_unfixable_damage_ratio", TruckDist.EngineUnfixable);
            UpdateParameter(ref content, "transmission_unfixable_damage_ratio", TruckDist.TransmissionUnfixable);
            UpdateParameter(ref content, "wheel_unfixable_damage_ratio", TruckDist.WheelUnfixable);

            // Anhänger Verteilung schreiben
            UpdateParameter(ref content, "trailer_body_damage_ratio", TrailerDist.TrailerBodyDamageRatio);
            UpdateParameter(ref content, "trailer_chassis_damage_ratio", TrailerDist.TrailerChassisDamageRatio);
            UpdateParameter(ref content, "trailer_wheel_damage_ratio", TrailerDist.TrailerWheelDamageRatio);
            UpdateParameter(ref content, "cargo_damage_ratio", TrailerDist.CargoDamageRatio);

            UpdateParameter(ref content, "trailer_body_unfixable_damage_ratio", TrailerDist.TrailerBodyUnfixable);
            UpdateParameter(ref content, "trailer_chassis_unfixable_damage_ratio", TrailerDist.TrailerChassisUnfixable);
            UpdateParameter(ref content, "trailer_wheel_unfixable_damage_ratio", TrailerDist.TrailerWheelUnfixable);

            // LKW Verschleiß schreiben
            UpdateParameter(ref content, "cabin_wear", TruckWear.CabinWear);
            UpdateParameter(ref content, "chassis_wear", TruckWear.ChassisWear);
            UpdateParameter(ref content, "engine_wear", TruckWear.EngineWear);
            UpdateParameter(ref content, "transmission_wear", TruckWear.TransmissionWear);
            UpdateParameter(ref content, "wheel_wear", TruckWear.WheelWear);

            UpdateParameter(ref content, "cabin_wear_unfixable", TruckWear.CabinWearUnfixable);
            UpdateParameter(ref content, "chassis_wear_unfixable", TruckWear.ChassisWearUnfixable);
            UpdateParameter(ref content, "engine_wear_unfixable", TruckWear.EngineWearUnfixable);
            UpdateParameter(ref content, "transmission_wear_unfixable", TruckWear.TransmissionWearUnfixable);
            UpdateParameter(ref content, "wheel_wear_unfixable", TruckWear.WheelWearUnfixable);

            // Anhänger Verschleiß schreiben
            UpdateParameter(ref content, "trailer_body_wear", TrailerWear.TrailerBodyWear);
            UpdateParameter(ref content, "trailer_chassis_wear", TrailerWear.TrailerChassisWear);
            UpdateParameter(ref content, "trailer_wheel_wear", TrailerWear.TrailerWheelWear);

            UpdateParameter(ref content, "trailer_body_wear_unfixable", TrailerWear.TrailerBodyWearUnfixable);
            UpdateParameter(ref content, "trailer_chassis_wear_unfixable", TrailerWear.TrailerChassisWearUnfixable);
            UpdateParameter(ref content, "trailer_wheel_wear_unfixable", TrailerWear.TrailerWheelWearUnfixable);
        }

        private string ExtractValue(string content, string parameterName, string fallback)
        {
            Match match = new Regex($@"{parameterName}:\s*(?<value>[0-9eE.-]+)").Match(content);
            return match.Success ? match.Groups["value"].Value : fallback;
        }

        private void UpdateParameter(ref string content, string parameterName, string newValue)
        {
            Regex regex = new Regex($@"({parameterName}:\s*)[0-9eE.-]+");
            content = regex.Replace(content, $"${{1}}{newValue}");
        }
    }

    public class DamageCollisionSettings
    {
        [DisplayName("LKW Schaden Koeffizient")]
        [Description("Schaden pro Treffer am LKW (Standard: 0.0007).")]
        public string TruckDamageCoef { get; set; } = "0.0007";

        [DisplayName("Anhänger Schaden Koeffizient")]
        [Description("Schaden pro Treffer am Anhänger (Standard: 0.0007).")]
        public string TrailerDamageCoef { get; set; } = "0.0007";

        [DisplayName("Falsch angehängter Anhänger Schaden")]
        [Description("Schadenskoeffizient beim Ziehen eines nicht korrekt gekoppelten Anhängers (Standard: 0.00002).")]
        public string DraggedTrailerDamageCoef { get; set; } = "0.00002";

        [DisplayName("Schaden von LKW auf Anhänger")]
        [Description("Übertragener Schaden zwischen LKW und Anhänger (Standard: 0.2).")]
        public string TruckToTrailerDmg { get; set; } = "0.2";

        [DisplayName("Seiten-Schaden Faktor")]
        [Description("Erhöhter Faktor für Treffer an den Seiten (Standard: 6.0).")]
        public string SideDamageFactor { get; set; } = "6.0";

        [DisplayName("Dach-Schaden Faktor")]
        [Description("Erhöhter Faktor für Treffer am Dach (Standard: 11.0).")]
        public string RoofDamageFactor { get; set; } = "11.0";
    }

    public class DamageTruckDistSettings
    {
        [DisplayName("Kabine: Schadensverhältnis")]
        [Description("Standard: 0.8")]
        public string CabinDamageRatio { get; set; } = "0.8";

        [DisplayName("Fahrgestell: Schadensverhältnis")]
        [Description("Standard: 1.0")]
        public string ChassisDamageRatio { get; set; } = "1.0";

        [DisplayName("Motor: Schadensverhältnis")]
        [Description("Standard: 0.5")]
        public string EngineDamageRatio { get; set; } = "0.5";

        [DisplayName("Getriebe: Schadensverhältnis")]
        [Description("Standard: 0.3")]
        public string TransmissionDamageRatio { get; set; } = "0.3";

        [DisplayName("Räder: Schadensverhältnis")]
        [Description("Standard: 0.15")]
        public string WheelDamageRatio { get; set; } = "0.15";

        [DisplayName("Kabine: Unreparierbar-Anteil")]
        [Description("Standard: 0.04")]
        public string CabinUnfixable { get; set; } = "0.04";

        [DisplayName("Fahrgestell: Unreparierbar-Anteil")]
        [Description("Standard: 0.05")]
        public string ChassisUnfixable { get; set; } = "0.05";

        [DisplayName("Motor: Unreparierbar-Anteil")]
        [Description("Standard: 0.02")]
        public string EngineUnfixable { get; set; } = "0.02";

        [DisplayName("Getriebe: Unreparierbar-Anteil")]
        [Description("Standard: 0.01")]
        public string TransmissionUnfixable { get; set; } = "0.01";

        [DisplayName("Räder: Unreparierbar-Anteil")]
        [Description("Standard: 0.01")]
        public string WheelUnfixable { get; set; } = "0.01";
    }

    public class DamageTrailerDistSettings
    {
        [DisplayName("Aufbau: Schadensverhältnis")]
        [Description("Standard: 0.8")]
        public string TrailerBodyDamageRatio { get; set; } = "0.8";

        [DisplayName("Fahrgestell: Schadensverhältnis")]
        [Description("Standard: 1.0")]
        public string TrailerChassisDamageRatio { get; set; } = "1.0";

        [DisplayName("Räder: Schadensverhältnis")]
        [Description("Standard: 0.15")]
        public string TrailerWheelDamageRatio { get; set; } = "0.15";

        [DisplayName("Fracht: Schadensverhältnis")]
        [Description("Standard: 1.0")]
        public string CargoDamageRatio { get; set; } = "1.0";

        [DisplayName("Aufbau: Unreparierbar-Anteil")]
        [Description("Standard: 0.04")]
        public string TrailerBodyUnfixable { get; set; } = "0.04";

        [DisplayName("Fahrgestell: Unreparierbar-Anteil")]
        [Description("Standard: 0.05")]
        public string TrailerChassisUnfixable { get; set; } = "0.05";

        [DisplayName("Räder: Unreparierbar-Anteil")]
        [Description("Standard: 0.01")]
        public string TrailerWheelUnfixable { get; set; } = "0.01";
    }

    public class DamageTruckWearSettings
    {
        [DisplayName("Verschleiß: Kabine")]
        [Description("Verschleiß pro km. (Hinweis: '2e-6' entspricht 0.000002).")]
        public string CabinWear { get; set; } = "0.0";

        [DisplayName("Verschleiß: Fahrgestell")]
        [Description("Verschleiß pro km.")]
        public string ChassisWear { get; set; } = "0.0";

        [DisplayName("Verschleiß: Motor")]
        [Description("Motorverschleiß pro km (Standard: 2e-6 bzw. 0.000002).")]
        public string EngineWear { get; set; } = "2e-6";

        [DisplayName("Verschleiß: Getriebe")]
        [Description("Getriebeverschleiß pro km (Standard: 2e-6 bzw. 0.000002).")]
        public string TransmissionWear { get; set; } = "2e-6";

        [DisplayName("Verschleiß: Räder")]
        [Description("Reifenverschleiß pro km (Standard: 2e-6 bzw. 0.000002).")]
        public string WheelWear { get; set; } = "2e-6";

        [DisplayName("Unreparierbar: Kabine")]
        [Description("Anteil des Dauerschadens, der nicht repariert werden kann.")]
        public string CabinWearUnfixable { get; set; } = "0.0";

        [DisplayName("Unreparierbar: Fahrgestell")]
        [Description("Anteil des unreparierbaren Verschleißes.")]
        public string ChassisWearUnfixable { get; set; } = "0.0";

        [DisplayName("Unreparierbar: Motor")]
        [Description("Unreparierbarer Motorverschleiß (Standard: 2e-7 bzw. 0.0000002).")]
        public string EngineWearUnfixable { get; set; } = "2e-7";

        [DisplayName("Unreparierbar: Getriebe")]
        [Description("Unreparierbarer Getriebeverschleiß (Standard: 2e-7 bzw. 0.0000002).")]
        public string TransmissionWearUnfixable { get; set; } = "2e-7";

        [DisplayName("Unreparierbar: Räder")]
        [Description("Unreparierbarer Reifenverschleiß (Standard: 2e-5 bzw. 0.00002).")]
        public string WheelWearUnfixable { get; set; } = "2e-5";
    }

    public class DamageTrailerWearSettings
    {
        [DisplayName("Verschleiß: Aufbau")]
        [Description("Verschleiß des Anhängeraufbaus pro gefahrenem Kilometer.")]
        public string TrailerBodyWear { get; set; } = "0.0";

        [DisplayName("Verschleiß: Fahrgestell")]
        [Description("Verschleiß des Anhänger-Fahrgestells pro Kilometer.")]
        public string TrailerChassisWear { get; set; } = "0.0";

        [DisplayName("Verschleiß: Räder")]
        [Description("Reifenverschleiß des Anhängers pro Kilometer (Standard: 2e-6).")]
        public string TrailerWheelWear { get; set; } = "2e-6";

        [DisplayName("Unreparierbar: Aufbau")]
        [Description("Unreparierbarer Dauerschaden am Aufbau.")]
        public string TrailerBodyWearUnfixable { get; set; } = "0.0";

        [DisplayName("Unreparierbar: Fahrgestell")]
        [Description("Unreparierbarer Dauerschaden am Anhänger-Fahrgestell.")]
        public string TrailerChassisWearUnfixable { get; set; } = "0.0";

        [DisplayName("Unreparierbar: Räder")]
        [Description("Unreparierbarer Reifenverschleiß beim Anhänger (Standard: 2e-5).")]
        public string TrailerWheelWearUnfixable { get; set; } = "2e-5";
    }
}