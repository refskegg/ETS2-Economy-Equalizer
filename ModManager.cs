using System;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace ETS2_ModTool
{
    public class ModManager
    {
        public ModSettings ImportMod(string filePath, string tempFolder)
        {
            ModSettings settings = new ModSettings();
            
            if (Directory.Exists(tempFolder)) Directory.Delete(tempFolder, true);
            Directory.CreateDirectory(tempFolder);
            ZipFile.ExtractToDirectory(filePath, tempFolder);

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
            string usedPath = Path.Combine(tempFolder, "def", "used_vehicle_assortment_config.sii");
            if (File.Exists(usedPath)) settings.UsedVehicles.Import(File.ReadAllText(usedPath));

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
            ExportEmbeddedFile(defDir, "used_vehicle_assortment_config.sii", settings.UsedVehicles.Export);

            // Als unkomprimiertes .scs verpacken
            ZipFile.CreateFromDirectory(workDir, targetScs, CompressionLevel.NoCompression, false);
            Directory.Delete(workDir, true);
        }

        private void ExportEmbeddedFile(string defDir, string fileName, ActionRef<string> exportAction)
        {
            string content = ReadResourceText($"Templates.{fileName}");
            if (!string.IsNullOrEmpty(content))
            {
                exportAction(ref content);

                // Komma-Korrektur
                content = Regex.Replace(content, @"(?<=\d),(?=\d)", ".");

                File.WriteAllText(Path.Combine(defDir, fileName), content, Encoding.UTF8);
            }
        }

        private void CreateManifest(string rootDir, ModMetadata meta)
        {
            // --- Universell lesbare Beschreibung mit refskegg-Credits ---
            string formattedDescription =
        $@"=========================================
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

            File.WriteAllText(Path.Combine(rootDir, "mod_description.txt"), formattedDescription, Encoding.UTF8);

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
            File.WriteAllText(Path.Combine(rootDir, "manifest.sii"), manifestContent, Encoding.UTF8);

            // mod_icon.jpg aus Ressourcen kopieren
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream? stream = assembly.GetManifestResourceStream("ETS2_ModTool.Templates.mod_icon.jpg"))
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

            return JsonSerializer.Deserialize<ModSettings>(json) ?? new ModSettings();
        }

        // --- JSON Externe Presets (Laden / Speichern auf Festplatte) ---
        public void SavePreset(ModSettings settings, string filePath)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(settings, options);
            File.WriteAllText(filePath, json, Encoding.UTF8);
        }

        public ModSettings LoadPreset(string filePath)
        {
            string json = File.ReadAllText(filePath, Encoding.UTF8);
            return JsonSerializer.Deserialize<ModSettings>(json) ?? new ModSettings();
        }

        // --- Hilfsfunktion: Textdatei aus interner Ressource auslesen ---
        private string ReadResourceText(string relativeResourcePath)
        {
            var assembly = Assembly.GetExecutingAssembly();
            string resourceName = $"ETS2_ModTool.{relativeResourcePath}";

            using (Stream? stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null) return string.Empty;
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public delegate void ActionRef<T>(ref T item);
    }
}