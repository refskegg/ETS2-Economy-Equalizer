using System.ComponentModel;
using System.Text.RegularExpressions;

namespace ETS2_ModTool
{
    public class DamageSettings
    {
        [Category("Schaden (damage_data.sii)")]
        [DisplayName("1. Kollision & Aufprallfaktoren")]
        [Description("Steuert, wie stark LKW und Anhänger bei Unfällen und Kollisionen beschädigt werden.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public DamageCollisionSettings Collision { get; set; } = new DamageCollisionSettings();

        [Category("Schaden (damage_data.sii)")]
        [DisplayName("2. LKW: Schadensverteilung & Dauerschaden")]
        [Description("Legt fest, wie stark einzelne LKW-Bauteile bei einem Unfall leiden und wie viel davon unreparierbar bleibt.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public DamageTruckDistSettings TruckDist { get; set; } = new DamageTruckDistSettings();

        [Category("Schaden (damage_data.sii)")]
        [DisplayName("3. Anhänger: Schadensverteilung & Fracht")]
        [Description("Verteilung von Unfallschäden auf Anhänger-Bauteile und die geladene Fracht.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public DamageTrailerDistSettings TrailerDist { get; set; } = new DamageTrailerDistSettings();

        [Category("Schaden (damage_data.sii)")]
        [DisplayName("4. LKW: Verschleiß pro Kilometer")]
        [Description("Laufende Abnutzung (reparierbar & unreparierbar) je gefahrenem Kilometer.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public DamageTruckWearSettings TruckWear { get; set; } = new DamageTruckWearSettings();

        [Category("Schaden (damage_data.sii)")]
        [DisplayName("5. Anhänger: Verschleiß pro Kilometer")]
        [Description("Laufende Abnutzung des eigenen Anhängers je gefahrenem Kilometer.")]
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
        [Description("Standard: 0.0007 | Grundmultiplikator für Unfallschäden am LKW.\n• Funktion: Wird mit der Aufprallenergie multipliziert. Höhere Werte (z. B. 0.0014) verdoppeln den Unfallschaden.\n• Tipp: Setze '0.0' für unzerstörbare LKWs bei Unfällen.")]
        public string TruckDamageCoef { get; set; } = "0.0007";

        [DisplayName("Anhänger Schaden Koeffizient")]
        [Description("Standard: 0.0007 | Grundmultiplikator für Unfallschäden am gezogenen Anhänger.\n• Funktion: Steuert, wie empfindlich der Trailer auf Kollisionen mit Leitplanken, Autos oder Hindernissen reagiert.")]
        public string TrailerDamageCoef { get; set; } = "0.0007";

        [DisplayName("Falsch angehängter Anhänger Schaden")]
        [Description("Standard: 0.00002 | Laufender Schaden beim Ziehen eines nicht richtig verriegelten Aufliegers.")]
        public string DraggedTrailerDamageCoef { get; set; } = "0.00002";

        [DisplayName("Schaden von LKW auf Anhänger")]
        [Description("Standard: 0.2 (20%) | Anteil des Aufprallschadens, der bei einem Frontalunfall der Zugmaschine zusätzlich auf den Anhänger übertragen wird.")]
        public string TruckToTrailerDmg { get; set; } = "0.2";

        [DisplayName("Seiten-Schaden Faktor")]
        [Description("Standard: 6.0 | Schadens-Multiplikator, wenn der LKW oder Anhänger seitlich getroffen wird oder umkippt.")]
        public string SideDamageFactor { get; set; } = "6.0";

        [DisplayName("Dach-Schaden Faktor")]
        [Description("Standard: 11.0 | Schadens-Multiplikator bei einem Überschlag auf das Kabinendach.")]
        public string RoofDamageFactor { get; set; } = "11.0";
    }

    public class DamageTruckDistSettings
    {
        [DisplayName("Kabine: Schadensverhältnis")]
        [Description("Standard: 0.8 (80%) | Bestimmt, wie stark die Fahrerkabine bei einem Unfall im Verhältnis zum Gesamtschaden beschädigt wird.")]
        public string CabinDamageRatio { get; set; } = "0.8";

        [DisplayName("Fahrgestell: Schadensverhältnis")]
        [Description("Standard: 1.0 (100%) | Bestimmt, wie stark das Chassis bei einem Unfall beschädigt wird.")]
        public string ChassisDamageRatio { get; set; } = "1.0";

        [DisplayName("Motor: Schadensverhältnis")]
        [Description("Standard: 0.5 (50%) | Bestimmt, wie stark der Motor bei einem Unfall leidet (hoher Motorschaden führt im Spiel zu Motoraussetzern!).")]
        public string EngineDamageRatio { get; set; } = "0.5";

        [DisplayName("Getriebe: Schadensverhältnis")]
        [Description("Standard: 0.3 (30%) | Bestimmt, wie stark das Getriebe bei einem Unfall beschädigt wird (führt bei hohem Schaden zu krachenden Gängen).")]
        public string TransmissionDamageRatio { get; set; } = "0.3";

        [DisplayName("Räder: Schadensverhältnis")]
        [Description("Standard: 0.15 (15%) | Bestimmt, wie stark die Reifen bei einem Unfall beschädigt werden.")]
        public string WheelDamageRatio { get; set; } = "0.15";

        [DisplayName("Kabine: Unreparierbar-Anteil")]
        [Description("Standard: 0.04 (4%) | Anteil des Unfallschadens an der Kabine, der sofort als permanenter Verschleiß zurückbleibt.\n• Hinweis: Unreparierbarer Verschleiß lässt sich nur durch eine teure 'Gesamtwiederherstellung' oder Teiletausch beheben. (0.0 = deaktiviert).")]
        public string CabinUnfixable { get; set; } = "0.04";

        [DisplayName("Fahrgestell: Unreparierbar-Anteil")]
        [Description("Standard: 0.05 (5%) | Anteil des Unfallschadens am Fahrgestell, der als permanenter Verschleiß zurückbleibt (0.0 = kein Dauerschaden).")]
        public string ChassisUnfixable { get; set; } = "0.05";

        [DisplayName("Motor: Unreparierbar-Anteil")]
        [Description("Standard: 0.02 (2%) | Anteil des Unfallschadens am Motor, der als permanenter Verschleiß zurückbleibt (0.0 = kein Dauerschaden).")]
        public string EngineUnfixable { get; set; } = "0.02";

        [DisplayName("Getriebe: Unreparierbar-Anteil")]
        [Description("Standard: 0.01 (1%) | Anteil des Unfallschadens am Getriebe, der als permanenter Verschleiß zurückbleibt (0.0 = kein Dauerschaden).")]
        public string TransmissionUnfixable { get; set; } = "0.01";

        [DisplayName("Räder: Unreparierbar-Anteil")]
        [Description("Standard: 0.01 (1%) | Anteil des Unfallschadens an den Rädern, der als permanenter Verschleiß zurückbleibt (0.0 = kein Dauerschaden).")]
        public string WheelUnfixable { get; set; } = "0.01";
    }

    public class DamageTrailerDistSettings
    {
        [DisplayName("Aufbau: Schadensverhältnis")]
        [Description("Standard: 0.8 (80%) | Wie stark der Koffer-/Planenaufbau des Anhängers bei einer Kollision beschädigt wird.")]
        public string TrailerBodyDamageRatio { get; set; } = "0.8";

        [DisplayName("Fahrgestell: Schadensverhältnis")]
        [Description("Standard: 1.0 (100%) | Wie stark das Anhänger-Fahrgestell bei einer Kollision beschädigt wird.")]
        public string TrailerChassisDamageRatio { get; set; } = "1.0";

        [DisplayName("Räder: Schadensverhältnis")]
        [Description("Standard: 0.15 (15%) | Wie stark die Anhänger-Reifen bei einer Kollision leiden.")]
        public string TrailerWheelDamageRatio { get; set; } = "0.15";

        [DisplayName("Fracht: Schadensverhältnis")]
        [Description("Standard: 1.0 (100%) | Wie stark die geladene Fracht bei einem Unfall am Anhänger beschädigt wird.\n• Tipp: Ein niedrigerer Wert (z. B. 0.5) halbiert den Frachtschaden bei kleinen Remplern.")]
        public string CargoDamageRatio { get; set; } = "1.0";

        [DisplayName("Aufbau: Unreparierbar-Anteil")]
        [Description("Standard: 0.04 (4%) | Permanenter Verschleißanteil am Anhängeraufbau nach einem Unfall (0.0 = kein Dauerschaden).")]
        public string TrailerBodyUnfixable { get; set; } = "0.04";

        [DisplayName("Fahrgestell: Unreparierbar-Anteil")]
        [Description("Standard: 0.05 (5%) | Permanenter Verschleißanteil am Anhänger-Chassis nach einem Unfall (0.0 = kein Dauerschaden).")]
        public string TrailerChassisUnfixable { get; set; } = "0.05";

        [DisplayName("Räder: Unreparierbar-Anteil")]
        [Description("Standard: 0.01 (1%) | Permanenter Verschleißanteil an den Anhängerrädern nach einem Unfall (0.0 = kein Dauerschaden).")]
        public string TrailerWheelUnfixable { get; set; } = "0.01";
    }

    public class DamageTruckWearSettings
    {
        [DisplayName("Verschleiß: Kabine")]
        [Description("Standard: 0.0 | Normaler Verschleiß der Kabine pro gefahrenem Kilometer.\n• Umrechnung: '1e-6' (0.000001) entspricht 0,1% Verschleiß auf 1.000 km.")]
        public string CabinWear { get; set; } = "0.0";

        [DisplayName("Verschleiß: Fahrgestell")]
        [Description("Standard: 0.0 | Normaler Verschleiß des Fahrgestells pro gefahrenem Kilometer.\n• Umrechnung: '1e-6' (0.000001) entspricht 0,1% Verschleiß auf 1.000 km.")]
        public string ChassisWear { get; set; } = "0.0";

        [DisplayName("Verschleiß: Motor")]
        [Description("Standard: 2e-6 (0.000002) | Normaler, reparierbarer Motorverschleiß pro Kilometer.\n• Praxis-Wert: '2e-6' bedeutet 0,2% Verschleiß je 1.000 km (bzw. 2% nach 10.000 km).")]
        public string EngineWear { get; set; } = "2e-6";

        [DisplayName("Verschleiß: Getriebe")]
        [Description("Standard: 2e-6 (0.000002) | Normaler, reparierbarer Getriebeverschleiß pro Kilometer.\n• Praxis-Wert: '2e-6' bedeutet 0,2% Verschleiß je 1.000 km.")]
        public string TransmissionWear { get; set; } = "2e-6";

        [DisplayName("Verschleiß: Räder")]
        [Description("Standard: 2e-6 (0.000002) | Normaler Reifenabrieb pro gefahrenem Kilometer.\n• Praxis-Wert: '2e-6' = 0,2% je 1.000 km. Für schnelleren Reifenverschleiß z. B. '2e-5' (= 2% je 1.000 km) wählen.")]
        public string WheelWear { get; set; } = "2e-6";

        [DisplayName("Unreparierbar: Kabine")]
        [Description("Standard: 0.0 | Permanenter Alterungsverschleiß der Kabine pro Kilometer (nur durch Komplettrestaurierung behebbar).")]
        public string CabinWearUnfixable { get; set; } = "0.0";

        [DisplayName("Unreparierbar: Fahrgestell")]
        [Description("Standard: 0.0 | Permanenter Alterungsverschleiß des Fahrgestells pro Kilometer.")]
        public string ChassisWearUnfixable { get; set; } = "0.0";

        [DisplayName("Unreparierbar: Motor")]
        [Description("Standard: 2e-7 (0.0000002) | Permanenter Motorverschleiß pro Kilometer.\n• Praxis-Wert: '2e-7' entspricht 0,2% Dauerschaden nach 10.000 km (oder '0.0' zum Deaktivieren).")]
        public string EngineWearUnfixable { get; set; } = "2e-7";

        [DisplayName("Unreparierbar: Getriebe")]
        [Description("Standard: 2e-7 (0.0000002) | Permanenter Getriebeverschleiß pro Kilometer.\n• Praxis-Wert: '2e-7' entspricht 0,2% Dauerschaden nach 10.000 km (oder '0.0' zum Deaktivieren).")]
        public string TransmissionWearUnfixable { get; set; } = "2e-7";

        [DisplayName("Unreparierbar: Räder")]
        [Description("Standard: 2e-5 (0.00002) | Permanenter Reifenverschleiß pro Kilometer.\n• Praxis-Wert: '2e-5' bedeutet 2% permanenter Reifenabrieb pro 1.000 km (nach 50.000 km sind die Reifen komplett abgefahren).")]
        public string WheelWearUnfixable { get; set; } = "2e-5";
    }

    public class DamageTrailerWearSettings
    {
        [DisplayName("Verschleiß: Aufbau")]
        [Description("Standard: 0.0 | Reparierbarer Verschleiß des Anhängeraufbaus pro Kilometer.")]
        public string TrailerBodyWear { get; set; } = "0.0";

        [DisplayName("Verschleiß: Fahrgestell")]
        [Description("Standard: 0.0 | Reparierbarer Verschleiß des Anhänger-Fahrgestells pro Kilometer.")]
        public string TrailerChassisWear { get; set; } = "0.0";

        [DisplayName("Verschleiß: Räder")]
        [Description("Standard: 2e-6 (0.000002) | Normaler Reifenabrieb am eigenen Anhänger pro Kilometer (= 0,2% auf 1.000 km).")]
        public string TrailerWheelWear { get; set; } = "2e-6";

        [DisplayName("Unreparierbar: Aufbau")]
        [Description("Standard: 0.0 | Permanenter Alterungsverschleiß am Anhängeraufbau pro Kilometer.")]
        public string TrailerBodyWearUnfixable { get; set; } = "0.0";

        [DisplayName("Unreparierbar: Fahrgestell")]
        [Description("Standard: 0.0 | Permanenter Alterungsverschleiß am Anhänger-Fahrgestell pro Kilometer.")]
        public string TrailerChassisWearUnfixable { get; set; } = "0.0";

        [DisplayName("Unreparierbar: Räder")]
        [Description("Standard: 2e-5 (0.00002) | Permanenter Reifenabrieb am Anhänger pro Kilometer (= 2% je 1.000 km).")]
        public string TrailerWheelWearUnfixable { get; set; } = "2e-5";
    }
}
