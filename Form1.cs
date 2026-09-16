using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ETS2_ModTool
{
    public partial class Form1 : Form
    {
        private ModSettings currentSettings;
        private ModManager modManager;

        public Form1()
        {
            InitializeComponent();

            // Icon aus der eigenen .exe extrahieren und als Fenster-Icon setzen
            try
            {
                string? exePath = Process.GetCurrentProcess().MainModule?.FileName;
                if (!string.IsNullOrEmpty(exePath))
                {
                    Icon = Icon.ExtractAssociatedIcon(exePath);
                }
            }
            catch
            {
                // Fallback, falls beim Auslesen des Icons etwas schiefgeht
            }

            modManager = new ModManager();
            currentSettings = new ModSettings();

            // PropertyGrid initial anbinden
            propertyGrid1.SelectedObject = currentSettings;
        }

        // --- Dropdown Preset-Auswahl ---
        private void cmbPresets_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbPresets.SelectedIndex)
            {
                case 0: // Standard
                    currentSettings = new ModSettings();
                    break;

                case 1: // Hardcore Realismus (sucht nach Presets/Realism.json in der .exe)
                    currentSettings = modManager.LoadEmbeddedPreset("Realism.json");
                    break;

                case 2: // Schnelles Geld (sucht nach Presets/Arcade.json in der .exe)
                    currentSettings = modManager.LoadEmbeddedPreset("Arcade.json");
                    break;
            }

            propertyGrid1.SelectedObject = currentSettings;
            propertyGrid1.Refresh();
        }

        // --- Preset laden (von Festplatte) ---
        private void btnLoadPreset_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "JSON Preset (*.json)|*.json";
                ofd.Title = "Mod-Preset laden";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        currentSettings = modManager.LoadPreset(ofd.FileName);
                        
                        propertyGrid1.SelectedObject = currentSettings;
                        propertyGrid1.Refresh();

                        MessageBox.Show("Preset erfolgreich geladen!", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Fehler beim Laden: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // --- Preset speichern (auf Festplatte) ---
        private void btnSavePreset_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "JSON Preset (*.json)|*.json";
                sfd.Title = "Mod-Preset speichern";
                sfd.FileName = "Mein_Mod_Preset.json";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        modManager.SavePreset(currentSettings, sfd.FileName);
                        MessageBox.Show("Preset erfolgreich gespeichert!", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Fehler beim Speichern: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // --- Importieren einer .scs-Datei ---
        private void btnImport_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "SCS Mod-Dateien (*.scs;*.zip)|*.scs;*.zip";
                ofd.Title = "Mod-Datei zum Bearbeiten auswählen";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string tempFolder = Path.Combine(Path.GetTempPath(), "ETS2_Import_" + Guid.NewGuid().ToString());
                        currentSettings = modManager.ImportMod(ofd.FileName, tempFolder);
                        
                        propertyGrid1.SelectedObject = currentSettings;
                        propertyGrid1.Refresh();

                        MessageBox.Show("Mod-Werte erfolgreich eingelesen!", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Fehler beim Importieren: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // --- Exportieren einer .scs-Datei ---
        private void btnExport_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "SCS Mod (*.scs)|*.scs";
                sfd.Title = "Mod-Paket exportieren";

                // Dateiname dynamisch aus der Textbox ableiten
                string defaultName = string.IsNullOrWhiteSpace(txtModName.Text) 
                    ? "MeinCustomMod" 
                    : txtModName.Text.Trim().Replace(" ", "_");

                sfd.FileName = $"{defaultName}.scs";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string workDir = Path.Combine(Path.GetTempPath(), "ETS2_Export_" + Guid.NewGuid().ToString());
                        
                        // Dynamische Zuweisung von Mod-Name und Autor
                        string finalModName = string.IsNullOrWhiteSpace(txtModName.Text) 
                            ? "ETS2 Economy Equalizer Mod" 
                            : txtModName.Text.Trim();

                        string finalAuthor = string.IsNullOrWhiteSpace(txtAuthor.Text) 
                            ? "Custom Modder" 
                            : txtAuthor.Text.Trim();

                        ModMetadata meta = new ModMetadata
                        {
                            PackageName = finalModName,
                            Author = finalAuthor,
                            Version = "1.0",
                            Category = "economy",
                            Description = $"Erstellt mit dem ETS2 Economy Equalizer ({finalModName})."
                        };

                        modManager.ExportMod(currentSettings, workDir, sfd.FileName, meta);

                        MessageBox.Show("Mod-Archiv (.scs) erfolgreich erstellt!", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Fehler beim Exportieren: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}