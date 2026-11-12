using System.ComponentModel;
using System.Text.RegularExpressions;

namespace ETS2_ModTool
{
    public class ServiceSettings
    {
        [Category("Werkstatt (service_data.sii)")]
        [DisplayName("1. LKW Gesamtwiederherstellung")]
        [Description("Aufpreise für die komplette Generalüberholung eines abgenutzten LKWs (entfernt unreparierbaren Verschleiß).")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public ServiceTruckSettings Truck { get; set; } = new ServiceTruckSettings();

        [Category("Werkstatt (service_data.sii)")]
        [DisplayName("2. Anhänger Gesamtwiederherstellung")]
        [Description("Aufpreise für die komplette Generalüberholung eines eigenen Anhängers in der Werkstatt.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public ServiceTrailerSettings Trailer { get; set; } = new ServiceTrailerSettings();

        public void Import(string content)
        {
            Truck.RestoreFixedPrice = ExtractValue(content, "truck_restore_fixed_price", Truck.RestoreFixedPrice);
            Truck.RestoreAccessoryFixedPrice = ExtractValue(content, "truck_restore_accessory_fixed_price", Truck.RestoreAccessoryFixedPrice);
            Truck.RestoreAccessoryPriceCoef = ExtractValue(content, "truck_restore_accessory_price_coef", Truck.RestoreAccessoryPriceCoef);

            Trailer.RestoreFixedPrice = ExtractValue(content, "trailer_restore_fixed_price", Trailer.RestoreFixedPrice);
            Trailer.RestoreAccessoryFixedPrice = ExtractValue(content, "trailer_restore_accessory_fixed_price", Trailer.RestoreAccessoryFixedPrice);
            Trailer.RestoreAccessoryPriceCoef = ExtractValue(content, "trailer_restore_accessory_price_coef", Trailer.RestoreAccessoryPriceCoef);
        }

        public void Export(ref string content)
        {
            UpdateParameter(ref content, "truck_restore_fixed_price", Truck.RestoreFixedPrice);
            UpdateParameter(ref content, "truck_restore_accessory_fixed_price", Truck.RestoreAccessoryFixedPrice);
            UpdateParameter(ref content, "truck_restore_accessory_price_coef", Truck.RestoreAccessoryPriceCoef);

            UpdateParameter(ref content, "trailer_restore_fixed_price", Trailer.RestoreFixedPrice);
            UpdateParameter(ref content, "trailer_restore_accessory_fixed_price", Trailer.RestoreAccessoryFixedPrice);
            UpdateParameter(ref content, "trailer_restore_accessory_price_coef", Trailer.RestoreAccessoryPriceCoef);
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
    }

    public class ServiceTruckSettings
    {
        [DisplayName("LKW Grundpreis Restaurierung (€)")]
        [Description("Standard: 0 € | Einmalige Grundpauschale in Euro für die 'Gesamtwiederherstellung' des LKWs in der Werkstatt.")]
        public string RestoreFixedPrice { get; set; } = "0";

        [DisplayName("LKW Pauschale pro Bauteil (€)")]
        [Description("Standard: 1000 € | Fester Arbeitslohn-Aufschlag je betroffenem Hauptbauteil (Motor, Getriebe, Chassis, Kabine, Räder) bei einer Gesamtwiederherstellung.")]
        public string RestoreAccessoryFixedPrice { get; set; } = "1000";

        [DisplayName("LKW Bauteil-Preisfaktor")]
        [Description("Standard: 1.1 (110%) | Multiplikator auf den Neupreis der restaurierten Bauteile (1.1 = Restaurierung kostet 110% des Teilewertes, 0.5 = 50%).")]
        public string RestoreAccessoryPriceCoef { get; set; } = "1.1";
    }

    public class ServiceTrailerSettings
    {
        [DisplayName("Anhänger Grundpreis Restaurierung (€)")]
        [Description("Standard: 0 € | Einmalige Grundpauschale in Euro für die 'Gesamtwiederherstellung' eines eigenen Anhängers.")]
        public string RestoreFixedPrice { get; set; } = "0";

        [DisplayName("Anhänger Pauschale pro Bauteil (€)")]
        [Description("Standard: 1000 € | Fester Arbeitslohn-Aufschlag je betroffenem Anhänger-Bauteil bei einer Gesamtwiederherstellung.")]
        public string RestoreAccessoryFixedPrice { get; set; } = "1000";

        [DisplayName("Anhänger Bauteil-Preisfaktor")]
        [Description("Standard: 1.1 (110%) | Multiplikator auf den Teilewert bei der Anhänger-Restaurierung.")]
        public string RestoreAccessoryPriceCoef { get; set; } = "1.1";
    }
}
