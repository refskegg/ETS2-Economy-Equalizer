using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace ETS2_ModTool
{
    public class ModManager
    {
        public ModSettings ImportMod(string filePath, string tempFolder, out ModMetadata? importedMeta)
        {
            ModSettings settings = new ModSettings();
            importedMeta = null;
            
            if (Directory.Exists(tempFolder)) Directory.Delete(tempFolder, true);
            Directory.CreateDirectory(tempFolder);
            ZipFile.ExtractToDirectory(filePath, tempFolder);

            // Manifest auslesen (Mod-Name & Autor wiederherstellen)
            string manifestPath = Path.Combine(tempFolder, "manifest.sii");
            if (File.Exists(manifestPath))
            {
                string manifestText = File.ReadAllText(manifestPath);
                importedMeta = new ModMetadata();

                Match nameMatch = Regex.Match(manifestText, @"display_name:\s*""(?<val>[^""]+)""");
                if (nameMatch.Success) importedMeta.PackageName = nameMatch.Groups["val"].Value;

                Match authorMatch = Regex.Match(manifestText, @"author:\s*""(?<val>[^""]+)""");
                if (authorMatch.Success)
                {
                    string rawAuthor = authorMatch.Groups["val"].Value;
                    importedMeta.Author = rawAuthor.Replace(" (Tool by refskegg)", "").Trim();
                }
            }

            // 1. Bank
            string bankPath = Path.Combine(tempFolder, "def", "bank_data.sii");
            if (File.Exists(bankPath)) settings.Bank.Import(File.ReadAllText(bankPath));

            // 2. Schaden
            string damagePath = Path.Combine(tempFolder, "def", "damage_data.sii");
            if (File.Exists(damagePath)) settings.Damage.Import(File.ReadAllText(damagePath));

            // 3. Wirtschaft
            string economyPath = Path.Combine(tempFolder, "def", "economy_data.sii");
            if (File.Exists(economyPath)) settings.Economy.Import(File.ReadAllText(economyPath));

            // 4. Polizei
            string policePath = Path.Combine(tempFolder, "def", "police_data.sii");
            if (File.Exists(policePath)) settings.Police.Import(File.ReadAllText(policePath));

            // 5. Werkstatt
            string servicePath = Path.Combine(tempFolder, "def", "service_data.sii");
            if (File.Exists(servicePath)) settings.Service.Import(File.ReadAllText(servicePath));

            // 6. Fähigkeiten
            string skillPath = Path.Combine(tempFolder, "def", "skill_data.sii");
            if (File.Exists(skillPath)) settings.Skills.Import(File.ReadAllText(skillPath));

            // 7. Spezial-Events
            string sePath = Path.Combine(tempFolder, "def", "special_event_data.sii");
            if (File.Exists(sePath)) settings.SpecialEvents.Import(File.ReadAllText(sePath));

            // 8. Gebrauchtfahrzeuge
            string usedPath = Path.Combine(tempFolder, "def", "used_vehicle_config.sii");
            string usedAltPath = Path.Combine(tempFolder, "def", "used_vehicle_assortment_config.sii");
            if (File.Exists(usedPath)) settings.UsedVehicles.Import(File.ReadAllText(usedPath));
            else if (File.Exists(usedAltPath)) settings.UsedVehicles.Import(File.ReadAllText(usedAltPath));

            Directory.Delete(tempFolder, true);
            return settings;
        }

        public void ExportMod(ModSettings settings, string workDir, string targetScs, ModMetadata? meta = null)
        {
            if (meta == null) meta = new ModMetadata();

            if (Directory.Exists(workDir)) Directory.Delete(workDir, true);
            if (File.Exists(targetScs)) File.Delete(targetScs);

            string defDir = Path.Combine(workDir, "def");
            Directory.CreateDirectory(defDir);

            // --- 1. Manifest & Beschreibung anlegen ---
            CreateManifest(workDir, meta);

            // --- 2. .sii-Dateien aus Embedded Templates verarbeiten ---
            ExportEmbeddedFile(defDir, "bank_data.sii", settings.Bank.Export);
            ExportEmbeddedFile(defDir, "damage_data.sii", settings.Damage.Export);
            ExportEmbeddedFile(defDir, "economy_data.sii", settings.Economy.Export);
            ExportEmbeddedFile(defDir, "police_data.sii", settings.Police.Export);
            ExportEmbeddedFile(defDir, "service_data.sii", settings.Service.Export);
            ExportEmbeddedFile(defDir, "skill_data.sii", settings.Skills.Export);
            ExportEmbeddedFile(defDir, "special_event_data.sii", settings.SpecialEvents.Export);
            ExportEmbeddedFile(defDir, "used_vehicle_config.sii", settings.UsedVehicles.Export);

            // Als unkomprimiertes .scs verpacken
            ZipFile.CreateFromDirectory(workDir, targetScs, CompressionLevel.NoCompression, false);
            Directory.Delete(workDir, true);
        }

        private static readonly Encoding Utf8NoBom = new UTF8Encoding(false);

        private void ExportEmbeddedFile(string defDir, string fileName, ActionRef<string> exportAction)
        {
            string content = ReadResourceText($"Templates.{fileName}");
            if (!string.IsNullOrEmpty(content))
            {
                exportAction(ref content);

                // Komma-Korrektur
                content = Regex.Replace(content, @"(?<=\d),(?=\d)", ".");

                File.WriteAllText(Path.Combine(defDir, fileName), content, Utf8NoBom);
            }
        }

        private void CreateManifest(string rootDir, ModMetadata meta)
        {
            // --- Universell lesbare Beschreibung (DE / EN je nach aktiver Sprache) ---
            string formattedDescription = LocalizationManager.CurrentLanguage == AppLanguage.EN
                ? $@"=========================================
        {meta.PackageName}
        Configured by: {meta.Author}
        Tool: ETS2 Economy Equalizer by refskegg
        =========================================

        [ OVERVIEW ]
        This mod package customizes the internal game economy for a
        balanced and personalized driving experience.
        Generated with ETS2 Economy Equalizer by refskegg.

        [ CUSTOMIZED AREAS ]
        • Loans & Bank Interest: Adjusted limits, durations and rates
        • Freight Revenue: Scaled payouts and distance bonuses
        • Operating Costs: Fuel discounts, service & towing rates
        • Police & Fines: Modified penalties, delays and probabilities
        • Driver Skills: Custom XP rates, payouts and unlocks

        -----------------------------------------
        Powered by ETS2 Economy Equalizer
        Developed by refskegg
        ========================================="
                : $@"=========================================
        {meta.PackageName}
        Konfiguriert von: {meta.Author}
        Tool: ETS2 Economy Equalizer by refskegg
        =========================================

        [ ÜBERSICHT ]
        Dieses Mod-Paket passt die interne Spielwirtschaft für ein 
        ausgewogenes und individuelles Spielerlebnis an.
        Generiert mit dem ETS2 Economy Equalizer by refskegg.

        [ ANGEPASSTE BEREICHE ]
        • Kredite & Bankzinsen: Angepasste Limits, Laufzeiten und Raten
        • Frachtvergütungen: Skalierte Einnahmen und Entfernungsboni
        • Betriebskosten: Treibstoffkosten, Werkstatt- und Abschlepptarife
        • Polizei & Bußgelder: Modifizierte Strafen und Toleranzen
        • Fähigkeiten: Individuelle XP-Raten und Freischaltungen

        -----------------------------------------
        Powered by ETS2 Economy Equalizer
        Developed by refskegg
        =========================================";

            File.WriteAllText(Path.Combine(rootDir, "mod_description.txt"), formattedDescription, Utf8NoBom);

            string manifestContent =
$@"SiiNunit
{{
mod_package : .package_name
{{
    package_version: ""{meta.Version}""
    author: ""{meta.Author} (Tool by refskegg)""
    category[]: ""{meta.Category}""
    icon: ""mod_icon.jpg""
    description_file: ""mod_description.txt""
    display_name: ""{meta.PackageName}""
}}
}}";
            File.WriteAllText(Path.Combine(rootDir, "manifest.sii"), manifestContent, Utf8NoBom);

            // mod_icon.jpg aus Ressourcen kopieren
            using (Stream? stream = OpenEmbeddedResourceStream("Templates.mod_icon.jpg"))
            {
                if (stream != null)
                {
                    using (FileStream fileStream = File.Create(Path.Combine(rootDir, "mod_icon.jpg")))
                    {
                        stream.CopyTo(fileStream);
                    }
                }
            }
        }

        // --- JSON Embedded Preset laden ---
        public ModSettings LoadEmbeddedPreset(string presetFileName)
        {
            string json = ReadResourceText($"Presets.{presetFileName}");
            if (string.IsNullOrWhiteSpace(json))
                return new ModSettings();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            };

            return JsonSerializer.Deserialize<ModSettings>(json, options) ?? new ModSettings();
        }

        // --- Hilfsfunktion: Textdatei aus interner Ressource auslesen ---
        private string ReadResourceText(string relativeResourcePath)
        {
            using (Stream? stream = OpenEmbeddedResourceStream(relativeResourcePath))
            {
                if (stream == null) return string.Empty;
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        private static Stream? OpenEmbeddedResourceStream(string relativeResourcePath)
        {
            var assembly = Assembly.GetExecutingAssembly();

            Stream? stream = assembly.GetManifestResourceStream($"ETS2_ModTool.{relativeResourcePath}")
                          ?? assembly.GetManifestResourceStream($"ETS2EE.{relativeResourcePath}");

            if (stream != null)
                return stream;

            string? matchedName = assembly.GetManifestResourceNames()
                .FirstOrDefault(n => n.EndsWith("." + relativeResourcePath, StringComparison.OrdinalIgnoreCase)
                                  || n.Equals(relativeResourcePath, StringComparison.OrdinalIgnoreCase));

            return matchedName != null ? assembly.GetManifestResourceStream(matchedName) : null;
        }

        public delegate void ActionRef<T>(ref T item);
    }
}