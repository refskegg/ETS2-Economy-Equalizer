using System.ComponentModel;
using System.Text.RegularExpressions;

namespace ETS2_ModTool
{
    public class ServiceSettings
    {
        [Category("Werkstatt (service_data.sii)")]
        [DisplayName("1. LKW Restaurierung")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public ServiceTruckSettings Truck { get; set; } = new ServiceTruckSettings();

        [Category("Werkstatt (service_data.sii)")]
        [DisplayName("2. Anhänger Restaurierung")]
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
        [DisplayName("LKW Grundpreis Restaurierung")]
        [Description("Fester Basispreis für die Gesamtwiederherstellung des LKWs (Standard: 0)[cite: 3].")]
        public string RestoreFixedPrice { get; set; } = "0";

        [DisplayName("LKW Zubehör Grundpreis")]
        [Description("Fester Aufpreis pro Zubehörteil bei Restaurierung (Standard: 1000)[cite: 3].")]
        public string RestoreAccessoryFixedPrice { get; set; } = "1000";

        [DisplayName("LKW Zubehör Preisfaktor")]
        [Description("Multiplikator für Zubehör-Wiederherstellung (Standard: 1.1)[cite: 3].")]
        public string RestoreAccessoryPriceCoef { get; set; } = "1.1";
    }

    public class ServiceTrailerSettings
    {
        [DisplayName("Anhänger Grundpreis Restaurierung")]
        [Description("Fester Basispreis für die Gesamtwiederherstellung des Anhängers (Standard: 0)[cite: 3].")]
        public string RestoreFixedPrice { get; set; } = "0";

        [DisplayName("Anhänger Zubehör Grundpreis")]
        [Description("Fester Aufpreis pro Zubehörteil bei Restaurierung (Standard: 1000)[cite: 3].")]
        public string RestoreAccessoryFixedPrice { get; set; } = "1000";

        [DisplayName("Anhänger Zubehör Preisfaktor")]
        [Description("Multiplikator für Zubehör-Wiederherstellung (Standard: 1.1)[cite: 3].")]
        public string RestoreAccessoryPriceCoef { get; set; } = "1.1";
    }
}