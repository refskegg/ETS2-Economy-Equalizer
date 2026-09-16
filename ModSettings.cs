using System.ComponentModel;

namespace ETS2_ModTool
{
    public class ModSettings
    {
        [Category("1. Bank")]
        [DisplayName("Bank-Einstellungen")]
        [Description("Kredite, Überziehung und Dispo-Zeiten.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public BankSettings Bank { get; set; } = new BankSettings();

        [Category("2. Schaden")]
        [DisplayName("Schadens-Einstellungen")]
        [Description("Kollisionsschäden, Verteilung und Verschleiß pro km.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public DamageSettings Damage { get; set; } = new DamageSettings();

        [Category("3. Wirtschaft")]
        [DisplayName("Wirtschafts-Einstellungen")]
        [Description("Garagen, Frachteinnahmen, Ruhezeiten und KI-Fahrer.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public EconomySettings Economy { get; set; } = new EconomySettings();

        [Category("4. Polizei")]
        [DisplayName("Polizei-Einstellungen")]
        [Description("Strafbeträge, Blitzer, Verstoß-Wahrscheinlichkeiten und Kontrollen.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public PoliceSettings Police { get; set; } = new PoliceSettings();

        [Category("5. Werkstatt")]
        [DisplayName("Werkstatt-Einstellungen")]
        [Description("Kosten für LKW- und Trailer-Wiederherstellung.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public ServiceSettings Service { get; set; } = new ServiceSettings();

        [Category("6. Fähigkeiten")]
        [DisplayName("Fähigkeiten-Einstellungen")]
        [Description("XP-, Umsatzboni und Distanzen für Fahrer-Fähigkeiten pro Rang.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public SkillSettings Skills { get; set; } = new SkillSettings();

        [Category("7. Events & Freischaltungen")]
        [DisplayName("Spezial-Events")]
        [Description("Bedingungen für Kredite, Händler und Anhängerkauf.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public SpecialEventSettings SpecialEvents { get; set; } = new SpecialEventSettings();

        [Category("8. Gebrauchtfahrzeuge")]
        [DisplayName("Gebrauchtwagenmarkt")]
        [Description("Generierungszeiten, Kilometerstände, Dauerschäden und Preisfaktoren.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public UsedVehicleSettings UsedVehicles { get; set; } = new UsedVehicleSettings();
    }
}