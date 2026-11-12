using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using Microsoft.Win32;

namespace ETS2_ModTool
{
    public partial class MainWindow : Window
    {
        private ModSettings currentSettings;
        private readonly ModManager modManager;
        private string activeCategoryKey = "ALL";
        private bool isInitializing = true;
        private bool isUpdatingPresetCombo = false;
        private bool expandAllInfos = false;

        public MainWindow()
        {
            InitializeComponent();

            modManager = new ModManager();
            currentSettings = new ModSettings();

            ApplyVersionInfo();
            isInitializing = false;
            ApplyLanguageToStaticUI();
            RebuildDashboard();
        }

        private void ApplyVersionInfo()
        {
            string rawVersion = Assembly.GetExecutingAssembly()
                .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion
                ?? Assembly.GetExecutingAssembly().GetName().Version?.ToString(3)
                ?? "1.0.0";

            int plusIdx = rawVersion.IndexOf('+');
            if (plusIdx > 0)
                rawVersion = rawVersion.Substring(0, plusIdx);

            Title = $"ETS2 Economy Equalizer v{rawVersion}";
            if (txtVersionBadge != null) txtVersionBadge.Text = $"v{rawVersion}";
            if (txtVersionFooter != null) txtVersionFooter.Text = $"Version {rawVersion}";
        }

        // --- Language Switcher (DE / EN) ---
        private void btnLanguage_Click(object sender, RoutedEventArgs e)
        {
            LocalizationManager.CurrentLanguage = LocalizationManager.CurrentLanguage == AppLanguage.DE
                ? AppLanguage.EN
                : AppLanguage.DE;

            ApplyLanguageToStaticUI();
            RebuildDashboard();
        }

        private void ApplyLanguageToStaticUI()
        {
            bool isEn = LocalizationManager.CurrentLanguage == AppLanguage.EN;

            btnLanguage.Content = isEn ? "🇬🇧 EN" : "🇩🇪 DE";

            // Sidebar Texts
            lblQuickPreset.Text = LocalizationManager.T("SCHNELL-PRESET", "QUICK PRESET");
            btnResetDefault.Content = LocalizationManager.T("↺ Standard", "↺ Default");
            btnResetDefault.ToolTip = LocalizationManager.T(
                "Alle Werte sofort auf Standard (Vanilla) zurücksetzen",
                "Reset all values back to default (Vanilla) immediately");

            cbiPresetVanilla.Content = LocalizationManager.T("⚙️ Standard (Vanilla)", "⚙️ Default (Vanilla)");
            cbiPresetBalanced.Content = LocalizationManager.T("⚖️ Realismus Light (Ausgewogen)", "⚖️ Realism Lite (Balanced)");
            cbiPresetHardcore.Content = LocalizationManager.T("💀 Hardcore Real Economy", "💀 Hardcore Real Economy");
            cbiPresetUsedCareer.Content = LocalizationManager.T("🚛 Gebrauchtwagen-Karriere", "🚛 Used Truck Career");
            cbiPresetRelaxed.Content = LocalizationManager.T("🛡️ Entspannt (Kein Schaden/Polizei)", "🛡️ Relaxed (No Damage/Police)");
            cbiPresetArcade.Content = LocalizationManager.T("⚡ Schnelles Geld & XP (Arcade)", "⚡ Fast Money & XP (Arcade)");
            UpdatePresetDescription();

            lblCategoriesHeader.Text = LocalizationManager.T("KATEGORIEN", "CATEGORIES");
            navAll.Content = LocalizationManager.T("🌐  Alle Kategorien", "🌐  All Categories");
            navBank.Content = LocalizationManager.T("🏦  1. Bank & Kredite", "🏦  1. Bank & Loans");
            navDamage.Content = LocalizationManager.T("💥  2. Schaden & Verschleiß", "💥  2. Damage & Wear");
            navEconomy.Content = LocalizationManager.T("📈  3. Wirtschaft & Fracht", "📈  3. Economy & Cargo");
            navPolice.Content = LocalizationManager.T("🚓  4. Polizei & Bußgelder", "🚓  4. Police & Fines");
            navService.Content = LocalizationManager.T("🔧  5. Werkstatt & Service", "🔧  5. Service & Restoration");
            navSkills.Content = LocalizationManager.T("⭐  6. Fähigkeiten (Skills)", "⭐  6. Driver Skills");
            navEvents.Content = LocalizationManager.T("🔓  7. Events & Freischaltung", "🔓  7. Events & Unlocks");
            navUsed.Content = LocalizationManager.T("🚛  8. Gebrauchtfahrzeuge", "🚛  8. Used Truck Market");

            // Header & Search Bar
            UpdateHeaderTexts(activeCategoryKey);
            UpdateToggleAllInfosButtonText();

            txtSearch.ToolTip = LocalizationManager.T(
                "Tippe zum Filtern nach Namen oder Beschreibung...",
                "Type to filter by parameter name or description...");
            txtSearchPlaceholder.Text = LocalizationManager.T(
                "Parameter suchen (z. B. Kredit, Blitzer, XP)...",
                "Search parameters (e.g. Loan, Speed, XP)...");

            // Bottom Action Bar
            lblModName.Text = LocalizationManager.T("Mod-Name:", "Mod Name:");
            lblAuthor.Text = LocalizationManager.T("Autor:", "Author:");

            if (txtModName.Text == "Mein Custom Mod" && isEn)
                txtModName.Text = "My Custom Mod";
            else if (txtModName.Text == "My Custom Mod" && !isEn)
                txtModName.Text = "Mein Custom Mod";

            btnImport.Content = LocalizationManager.T("📥 .scs / .zip importieren", "📥 Import .scs / .zip");
            btnImport.ToolTip = LocalizationManager.T(
                "Bestehende Mod-Datei (.scs oder .zip) einlesen",
                "Import an existing mod archive (.scs or .zip)");

            btnExportToModFolder.Content = LocalizationManager.T("📁 Direkt in ETS2 Mod-Ordner", "📁 Direct to ETS2 Mod Folder");
            btnExportToModFolder.ToolTip = LocalizationManager.T(
                "Erstellt die .scs Datei direkt unter Dokumente\\Euro Truck Simulator 2\\mod",
                "Exports the .scs file directly into Documents\\Euro Truck Simulator 2\\mod");

            btnExport.Content = LocalizationManager.T("🚀 Mod (.scs) exportieren", "🚀 Export Mod (.scs)");
            btnExport.ToolTip = LocalizationManager.T(
                "Mod-Paket an einem beliebigen Ort speichern",
                "Save the .scs mod package to any location");

            SetStatus(LocalizationManager.T("Bereit.", "Ready."));
        }

        private void UpdateToggleAllInfosButtonText()
        {
            if (btnToggleAllInfos == null) return;
            btnToggleAllInfos.Content = expandAllInfos
                ? LocalizationManager.T("ⓘ Alle Infos einklappen", "ⓘ Collapse All Info")
                : LocalizationManager.T("ⓘ Alle Infos ausklappen", "ⓘ Expand All Info");
            btnToggleAllInfos.ToolTip = LocalizationManager.T(
                "Blendet alle detaillierten Parameter-Erklärungen gleichzeitig ein oder aus",
                "Shows or hides all detailed parameter explanations at once");
        }

        // --- Navigation & Search ---
        private void NavButton_Checked(object sender, RoutedEventArgs e)
        {
            if (isInitializing) return;
            if (sender is RadioButton rb && rb.Tag is string key)
            {
                activeCategoryKey = key;
                UpdateHeaderTexts(key);
                RebuildDashboard();
                settingsScrollViewer?.ScrollToTop();
            }
        }

        private void UpdateHeaderTexts(string key)
        {
            if (txtCategoryTitle == null || txtCategorySubtitle == null) return;

            switch (key)
            {
                case "ALL":
                    txtCategoryTitle.Text = LocalizationManager.T("Alle Kategorien", "All Categories");
                    txtCategorySubtitle.Text = LocalizationManager.T(
                        "Übersicht sämtlicher Wirtschaftsparameter in einem Dashboard.",
                        "Overview of all economy parameters in a single dashboard.");
                    break;
                case "Bank":
                    txtCategoryTitle.Text = LocalizationManager.T("1. Bank & Kredite", "1. Bank & Loans");
                    txtCategorySubtitle.Text = LocalizationManager.T(
                        "Kreditbeträge, Zinssätze, Laufzeiten und Dispokredit-Regeln (bank_data.sii).",
                        "Loan amounts, interest rates, durations and overdraft rules (bank_data.sii).");
                    break;
                case "Damage":
                    txtCategoryTitle.Text = LocalizationManager.T("2. Schaden & Verschleiß", "2. Damage & Wear");
                    txtCategorySubtitle.Text = LocalizationManager.T(
                        "Unfallschäden, Schadensverteilung und Abnutzung pro Kilometer (damage_data.sii).",
                        "Crash damage, component damage distribution and wear per kilometer (damage_data.sii).");
                    break;
                case "Economy":
                    txtCategoryTitle.Text = LocalizationManager.T("3. Wirtschaft & Fracht", "3. Economy & Cargo");
                    txtCategorySubtitle.Text = LocalizationManager.T(
                        "Frachtvergütung, Garagenpreise, Ruhezeiten, KI-Fahrer und XP-Raten (economy_data.sii).",
                        "Freight revenue, garage prices, rest periods, AI drivers and XP rates (economy_data.sii).");
                    break;
                case "Police":
                    txtCategoryTitle.Text = LocalizationManager.T("4. Polizei & Bußgelder", "4. Police & Fines");
                    txtCategorySubtitle.Text = LocalizationManager.T(
                        "Strafbeträge, Blitzer, Kontrollverzögerungen und Wahrscheinlichkeiten (police_data.sii).",
                        "Fine amounts, speed cameras, check delays and offence probabilities (police_data.sii).");
                    break;
                case "Service":
                    txtCategoryTitle.Text = LocalizationManager.T("5. Werkstatt & Service", "5. Service & Restoration");
                    txtCategorySubtitle.Text = LocalizationManager.T(
                        "Kosten für LKW- und Anhänger-Restaurierung (service_data.sii).",
                        "Costs for complete truck and trailer restoration (service_data.sii).");
                    break;
                case "Skills":
                    txtCategoryTitle.Text = LocalizationManager.T("6. Fähigkeiten (Skills)", "6. Driver Skills");
                    txtCategorySubtitle.Text = LocalizationManager.T(
                        "XP-Boni, Umsatzsteigerungen und Distanzen je Fähigkeit und Rang (skill_data.sii).",
                        "XP bonuses, revenue increases and distances per skill and rank (skill_data.sii).");
                    break;
                case "SpecialEvents":
                    txtCategoryTitle.Text = LocalizationManager.T("7. Events & Freischaltungen", "7. Events & Unlocks");
                    txtCategorySubtitle.Text = LocalizationManager.T(
                        "Bedingungen für erste Kredite, Händler, Einladungen und Anhängerkauf (special_event_data.sii).",
                        "Conditions for initial loans, dealers, invitations and trailer purchase (special_event_data.sii).");
                    break;
                case "UsedVehicles":
                    txtCategoryTitle.Text = LocalizationManager.T("8. Gebrauchtfahrzeuge", "8. Used Truck Market");
                    txtCategorySubtitle.Text = LocalizationManager.T(
                        "Angebotsgenerierung, Kilometerstände, Verschleiß und Preisfaktoren (used_vehicle_config.sii).",
                        "Offer generation, mileage limits, wear and price factors (used_vehicle_config.sii).");
                    break;
            }
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (isInitializing) return;

            bool hasText = !string.IsNullOrWhiteSpace(txtSearch.Text);
            txtSearchPlaceholder.Visibility = hasText ? Visibility.Collapsed : Visibility.Visible;
            btnClearSearch.Visibility = hasText ? Visibility.Visible : Visibility.Collapsed;

            RebuildDashboard();
        }

        private void btnClearSearch_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Text = string.Empty;
            txtSearch.Focus();
        }

        private void btnToggleAllInfos_Click(object sender, RoutedEventArgs e)
        {
            expandAllInfos = !expandAllInfos;
            UpdateToggleAllInfosButtonText();
            RebuildDashboard();
        }

        // --- Dynamic Dashboard Generator (from C# classes & LocalizationManager) ---
        private void RebuildDashboard()
        {
            if (cardsContainer == null || currentSettings == null) return;
            cardsContainer.Children.Clear();

            string searchFilter = txtSearch?.Text?.Trim() ?? string.Empty;
            int visibleFieldsCount = 0;

            PropertyInfo[] topCategories = typeof(ModSettings).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo topProp in topCategories)
            {
                if (activeCategoryKey != "ALL" && string.IsNullOrEmpty(searchFilter) && topProp.Name != activeCategoryKey)
                    continue;

                object? topSectionObj = topProp.GetValue(currentSettings);
                if (topSectionObj == null) continue;

                string topDisplayName = LocalizationManager.GetDisplayName(topProp);
                string topDesc = LocalizationManager.GetDescription(topProp);

                StackPanel sectionPanel = new StackPanel { Margin = new Thickness(0, 0, 0, 16) };

                if (activeCategoryKey == "ALL" || !string.IsNullOrEmpty(searchFilter))
                {
                    Border sectionHeader = new Border
                    {
                        Margin = new Thickness(0, 8, 0, 12),
                        Padding = new Thickness(12, 8, 12, 8),
                        Background = new SolidColorBrush(Color.FromRgb(22, 25, 34)),
                        BorderBrush = new SolidColorBrush(Color.FromRgb(245, 158, 11)),
                        BorderThickness = new Thickness(3, 0, 0, 0),
                        CornerRadius = new CornerRadius(4)
                    };

                    StackPanel headerTextStack = new StackPanel();
                    headerTextStack.Children.Add(new TextBlock
                    {
                        Text = topDisplayName,
                        FontSize = 16,
                        FontWeight = FontWeights.Bold,
                        Foreground = Brushes.White
                    });

                    if (!string.IsNullOrWhiteSpace(topDesc))
                    {
                        headerTextStack.Children.Add(new TextBlock
                        {
                            Text = topDesc,
                            FontSize = 12,
                            Foreground = new SolidColorBrush(Color.FromRgb(156, 163, 175)),
                            Margin = new Thickness(0, 2, 0, 0)
                        });
                    }

                    sectionHeader.Child = headerTextStack;
                    sectionPanel.Children.Add(sectionHeader);
                }

                int sectionMatches = BuildCardsForObject(topSectionObj, sectionPanel, searchFilter);
                if (sectionMatches > 0)
                {
                    visibleFieldsCount += sectionMatches;
                    cardsContainer.Children.Add(sectionPanel);
                }
            }

            if (visibleFieldsCount == 0)
            {
                Border emptyCard = CreateCardBorder();
                emptyCard.Padding = new Thickness(24);
                TextBlock emptyText = new TextBlock
                {
                    Text = LocalizationManager.T(
                        $"Keine Parameter für „{searchFilter}“ gefunden.",
                        $"No parameters found matching \"{searchFilter}\"."),
                    FontSize = 14,
                    Foreground = new SolidColorBrush(Color.FromRgb(156, 163, 175)),
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                emptyCard.Child = emptyText;
                cardsContainer.Children.Add(emptyCard);
            }
        }

        private int BuildCardsForObject(object sectionObj, Panel parentContainer, string searchFilter)
        {
            int totalMatched = 0;
            PropertyInfo[] subGroups = sectionObj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo groupProp in subGroups)
            {
                object? groupValue = groupProp.GetValue(sectionObj);
                if (groupValue == null) continue;

                string groupTitle = LocalizationManager.GetDisplayName(groupProp);
                string groupDesc = LocalizationManager.GetDescription(groupProp);

                Border card = CreateCardBorder();
                StackPanel cardContent = new StackPanel();

                // Card Header
                Border headerBorder = new Border
                {
                    Background = new SolidColorBrush(Color.FromRgb(34, 39, 54)),
                    CornerRadius = new CornerRadius(8, 8, 0, 0),
                    Padding = new Thickness(18, 12, 18, 12),
                    BorderBrush = new SolidColorBrush(Color.FromRgb(42, 48, 60)),
                    BorderThickness = new Thickness(0, 0, 0, 1)
                };

                StackPanel headerStack = new StackPanel();
                headerStack.Children.Add(new TextBlock
                {
                    Text = groupTitle,
                    FontSize = 14.5,
                    FontWeight = FontWeights.SemiBold,
                    Foreground = new SolidColorBrush(Color.FromRgb(245, 158, 11))
                });

                if (!string.IsNullOrWhiteSpace(groupDesc))
                {
                    headerStack.Children.Add(new TextBlock
                    {
                        Text = groupDesc,
                        FontSize = 11.5,
                        Foreground = new SolidColorBrush(Color.FromRgb(156, 163, 175)),
                        Margin = new Thickness(0, 2, 0, 0)
                    });
                }

                headerBorder.Child = headerStack;
                cardContent.Children.Add(headerBorder);

                // Card Body
                StackPanel rowsPanel = new StackPanel { Margin = new Thickness(18, 8, 18, 12) };
                int cardMatches = PopulatePropertyRows(groupValue, rowsPanel, groupTitle, searchFilter, "");

                if (cardMatches > 0)
                {
                    cardContent.Children.Add(rowsPanel);
                    card.Child = cardContent;
                    parentContainer.Children.Add(card);
                    totalMatched += cardMatches;
                }
            }

            return totalMatched;
        }

        private int PopulatePropertyRows(object targetObj, StackPanel rowsPanel, string groupTitle, string searchFilter, string prefixLabel, string parentDesc = "")
        {
            int count = 0;
            PropertyInfo[] props = targetObj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            if (targetObj is SkillRankValues || targetObj is BusSkillRankValues)
            {
                string fullTitle = string.IsNullOrEmpty(prefixLabel) ? groupTitle : prefixLabel;
                if (!MatchesFilter(fullTitle, parentDesc, searchFilter))
                    return 0;

                rowsPanel.Children.Add(CreateSkillRankRow(targetObj, fullTitle, parentDesc));
                return 1;
            }

            foreach (PropertyInfo prop in props)
            {
                if (!prop.CanRead) continue;

                string propDisplay = LocalizationManager.GetDisplayName(prop);
                string propDesc = LocalizationManager.GetDescription(prop);
                string fullLabel = string.IsNullOrEmpty(prefixLabel) ? propDisplay : $"{prefixLabel} • {propDisplay}";

                if (prop.PropertyType.IsClass && prop.PropertyType != typeof(string))
                {
                    object? nestedObj = prop.GetValue(targetObj);
                    if (nestedObj != null)
                    {
                        count += PopulatePropertyRows(nestedObj, rowsPanel, groupTitle, searchFilter, fullLabel, propDesc);
                    }
                    continue;
                }

                if (!prop.CanWrite) continue;

                if (!MatchesFilter(fullLabel, propDesc, searchFilter) && !MatchesFilter(groupTitle, "", searchFilter))
                    continue;

                rowsPanel.Children.Add(CreatePropertyInputRow(targetObj, prop, fullLabel, propDesc));
                count++;
            }

            return count;
        }

        private static (string badgeText, string detailText) SplitDescription(string rawDescription)
        {
            if (string.IsNullOrWhiteSpace(rawDescription))
                return (string.Empty, string.Empty);

            int pipeIndex = rawDescription.IndexOf('|');
            if (pipeIndex > 0)
            {
                string badge = rawDescription.Substring(0, pipeIndex).Trim();
                string details = rawDescription.Substring(pipeIndex + 1).Trim();
                return (badge, details);
            }

            return (string.Empty, rawDescription.Trim());
        }

        private static (Button infoBtn, Border infoPanel) CreateCollapsibleInfoBlock(string badgeText, string detailText, bool initialExpanded)
        {
            string fullInfoText = string.IsNullOrEmpty(badgeText)
                ? detailText
                : $"{badgeText}\n{detailText}";

            string collapsedLabel = LocalizationManager.T("ⓘ Erklärung ▼", "ⓘ Details ▼");
            string expandedLabel = LocalizationManager.T("ⓘ Erklärung ▲", "ⓘ Details ▲");

            Border infoPanel = new Border
            {
                Background = new SolidColorBrush(Color.FromRgb(19, 23, 34)),
                BorderBrush = new SolidColorBrush(Color.FromRgb(245, 158, 11)),
                BorderThickness = new Thickness(2, 0, 0, 0),
                CornerRadius = new CornerRadius(4),
                Padding = new Thickness(12, 8, 12, 8),
                Margin = new Thickness(0, 6, 0, 2),
                Visibility = initialExpanded ? Visibility.Visible : Visibility.Collapsed
            };

            TextBlock detailBlock = new TextBlock
            {
                Text = fullInfoText,
                FontSize = 12,
                LineHeight = 18,
                Foreground = new SolidColorBrush(Color.FromRgb(209, 213, 219)),
                TextWrapping = TextWrapping.Wrap
            };
            infoPanel.Child = detailBlock;

            Button infoBtn = new Button
            {
                Content = initialExpanded ? expandedLabel : collapsedLabel,
                FontSize = 11,
                FontWeight = FontWeights.SemiBold,
                Padding = new Thickness(7, 2, 7, 2),
                Margin = new Thickness(8, 0, 0, 0),
                VerticalAlignment = VerticalAlignment.Center,
                Cursor = System.Windows.Input.Cursors.Hand,
                Background = new SolidColorBrush(initialExpanded ? Color.FromRgb(55, 48, 35) : Color.FromRgb(34, 40, 54)),
                Foreground = new SolidColorBrush(initialExpanded ? Color.FromRgb(251, 191, 36) : Color.FromRgb(156, 163, 175)),
                BorderBrush = new SolidColorBrush(initialExpanded ? Color.FromRgb(217, 119, 6) : Color.FromRgb(55, 65, 81)),
                BorderThickness = new Thickness(1)
            };

            FrameworkElementFactory borderFactory = new FrameworkElementFactory(typeof(Border));
            borderFactory.SetValue(Border.CornerRadiusProperty, new CornerRadius(4));
            borderFactory.SetBinding(Border.BackgroundProperty, new System.Windows.Data.Binding("Background") { RelativeSource = System.Windows.Data.RelativeSource.TemplatedParent });
            borderFactory.SetBinding(Border.BorderBrushProperty, new System.Windows.Data.Binding("BorderBrush") { RelativeSource = System.Windows.Data.RelativeSource.TemplatedParent });
            borderFactory.SetBinding(Border.BorderThicknessProperty, new System.Windows.Data.Binding("BorderThickness") { RelativeSource = System.Windows.Data.RelativeSource.TemplatedParent });
            borderFactory.SetBinding(Border.PaddingProperty, new System.Windows.Data.Binding("Padding") { RelativeSource = System.Windows.Data.RelativeSource.TemplatedParent });

            FrameworkElementFactory presenterFactory = new FrameworkElementFactory(typeof(ContentPresenter));
            presenterFactory.SetValue(HorizontalAlignmentProperty, HorizontalAlignment.Center);
            presenterFactory.SetValue(VerticalAlignmentProperty, VerticalAlignment.Center);
            borderFactory.AppendChild(presenterFactory);

            ControlTemplate chipTemplate = new ControlTemplate(typeof(Button)) { VisualTree = borderFactory };
            infoBtn.Template = chipTemplate;

            infoBtn.Click += (s, e) =>
            {
                bool isNowVisible = infoPanel.Visibility != Visibility.Visible;
                infoPanel.Visibility = isNowVisible ? Visibility.Visible : Visibility.Collapsed;
                infoBtn.Content = isNowVisible ? expandedLabel : collapsedLabel;
                infoBtn.Background = new SolidColorBrush(isNowVisible ? Color.FromRgb(55, 48, 35) : Color.FromRgb(34, 40, 54));
                infoBtn.Foreground = new SolidColorBrush(isNowVisible ? Color.FromRgb(251, 191, 36) : Color.FromRgb(156, 163, 175));
                infoBtn.BorderBrush = new SolidColorBrush(isNowVisible ? Color.FromRgb(217, 119, 6) : Color.FromRgb(55, 65, 81));
            };

            return (infoBtn, infoPanel);
        }

        private UIElement CreateSkillRankRow(object rankValues, string label, string description)
        {
            Border rowBorder = new Border
            {
                BorderBrush = new SolidColorBrush(Color.FromRgb(42, 48, 60)),
                BorderThickness = new Thickness(0, 0, 0, 1),
                Padding = new Thickness(0, 10, 0, 10)
            };

            StackPanel container = new StackPanel();

            var (badgeText, detailText) = SplitDescription(description);

            WrapPanel titleRow = new WrapPanel { VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(0, 0, 0, 8) };
            titleRow.Children.Add(new TextBlock
            {
                Text = label,
                FontSize = 13,
                FontWeight = FontWeights.Medium,
                Foreground = new SolidColorBrush(Color.FromRgb(243, 244, 246)),
                VerticalAlignment = VerticalAlignment.Center
            });

            if (!string.IsNullOrEmpty(badgeText))
            {
                titleRow.Children.Add(new TextBlock
                {
                    Text = $"  ({badgeText})",
                    FontSize = 11.5,
                    Foreground = new SolidColorBrush(Color.FromRgb(156, 163, 175)),
                    VerticalAlignment = VerticalAlignment.Center
                });
            }

            Border? collapsiblePanel = null;
            if (!string.IsNullOrEmpty(detailText))
            {
                var (infoBtn, infoPanel) = CreateCollapsibleInfoBlock(badgeText, detailText, expandAllInfos);
                titleRow.Children.Add(infoBtn);
                collapsiblePanel = infoPanel;
            }

            container.Children.Add(titleRow);
            if (collapsiblePanel != null)
            {
                collapsiblePanel.Margin = new Thickness(0, 0, 0, 8);
                container.Children.Add(collapsiblePanel);
            }

            PropertyInfo[] rankProps = rankValues.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            UniformGrid rankGrid = new UniformGrid { Columns = rankProps.Length };

            for (int i = 0; i < rankProps.Length; i++)
            {
                PropertyInfo rp = rankProps[i];
                StackPanel cell = new StackPanel { Margin = new Thickness(i == 0 ? 0 : 4, 0, i == rankProps.Length - 1 ? 0 : 4, 0) };

                cell.Children.Add(new TextBlock
                {
                    Text = LocalizationManager.GetDisplayName(rp),
                    FontSize = 11,
                    Foreground = new SolidColorBrush(Color.FromRgb(156, 163, 175)),
                    Margin = new Thickness(2, 0, 0, 4)
                });

                TextBox tb = new TextBox
                {
                    Style = (Style)FindResource("ModernTextBoxStyle"),
                    Text = Convert.ToString(rp.GetValue(rankValues), CultureInfo.InvariantCulture) ?? "",
                    Height = 32
                };

                tb.TextChanged += (s, e) =>
                {
                    TryApplyPropertyValue(rankValues, rp, tb.Text, tb);
                };

                cell.Children.Add(tb);
                rankGrid.Children.Add(cell);
            }

            container.Children.Add(rankGrid);
            rowBorder.Child = container;
            return rowBorder;
        }

        private UIElement CreatePropertyInputRow(object targetObj, PropertyInfo prop, string label, string description)
        {
            Border rowBorder = new Border
            {
                BorderBrush = new SolidColorBrush(Color.FromRgb(38, 44, 56)),
                BorderThickness = new Thickness(0, 0, 0, 1),
                Padding = new Thickness(0, 9, 0, 9)
            };

            StackPanel outerStack = new StackPanel();

            Grid grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(200) });

            var (badgeText, detailText) = SplitDescription(description);

            WrapPanel titleRow = new WrapPanel { VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(0, 0, 16, 0) };
            titleRow.Children.Add(new TextBlock
            {
                Text = label,
                FontSize = 13,
                FontWeight = FontWeights.Medium,
                Foreground = new SolidColorBrush(Color.FromRgb(243, 244, 246)),
                VerticalAlignment = VerticalAlignment.Center
            });

            if (!string.IsNullOrEmpty(badgeText))
            {
                titleRow.Children.Add(new TextBlock
                {
                    Text = $"   {badgeText}",
                    FontSize = 11.5,
                    Foreground = new SolidColorBrush(Color.FromRgb(140, 148, 164)),
                    VerticalAlignment = VerticalAlignment.Center
                });
            }

            Border? collapsiblePanel = null;
            if (!string.IsNullOrEmpty(detailText))
            {
                var (infoBtn, infoPanel) = CreateCollapsibleInfoBlock(badgeText, detailText, expandAllInfos);
                titleRow.Children.Add(infoBtn);
                collapsiblePanel = infoPanel;
            }

            Grid.SetColumn(titleRow, 0);
            grid.Children.Add(titleRow);

            object? rawVal = prop.GetValue(targetObj);
            string initialText = rawVal is IFormattable formattable
                ? formattable.ToString(null, CultureInfo.InvariantCulture)
                : (rawVal?.ToString() ?? "");

            TextBox inputBox = new TextBox
            {
                Style = (Style)FindResource("ModernTextBoxStyle"),
                Text = initialText,
                Height = 32,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Right
            };

            inputBox.TextChanged += (s, e) =>
            {
                TryApplyPropertyValue(targetObj, prop, inputBox.Text, inputBox);
            };

            Grid.SetColumn(inputBox, 1);
            grid.Children.Add(inputBox);

            outerStack.Children.Add(grid);
            if (collapsiblePanel != null)
            {
                outerStack.Children.Add(collapsiblePanel);
            }

            rowBorder.Child = outerStack;
            return rowBorder;
        }

        private void TryApplyPropertyValue(object targetObj, PropertyInfo prop, string textValue, TextBox inputBox)
        {
            try
            {
                string normalized = textValue.Trim().Replace(',', '.');
                if (prop.PropertyType == typeof(string))
                {
                    prop.SetValue(targetObj, normalized);
                    inputBox.Tag = null;
                }
                else if (prop.PropertyType == typeof(double))
                {
                    if (double.TryParse(normalized, NumberStyles.Any, CultureInfo.InvariantCulture, out double dVal))
                    {
                        prop.SetValue(targetObj, dVal);
                        inputBox.Foreground = new SolidColorBrush(Color.FromRgb(243, 244, 246));
                    }
                    else
                    {
                        inputBox.Foreground = new SolidColorBrush(Color.FromRgb(248, 113, 113));
                    }
                }
                else if (prop.PropertyType == typeof(int))
                {
                    if (int.TryParse(normalized, NumberStyles.Integer, CultureInfo.InvariantCulture, out int iVal))
                    {
                        prop.SetValue(targetObj, iVal);
                        inputBox.Foreground = new SolidColorBrush(Color.FromRgb(243, 244, 246));
                    }
                    else
                    {
                        inputBox.Foreground = new SolidColorBrush(Color.FromRgb(248, 113, 113));
                    }
                }
            }
            catch
            {
                // Ignore transient typing errors
            }
        }

        private static Border CreateCardBorder()
        {
            return new Border
            {
                Background = new SolidColorBrush(Color.FromRgb(28, 32, 43)),
                BorderBrush = new SolidColorBrush(Color.FromRgb(42, 48, 60)),
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(8),
                Margin = new Thickness(0, 0, 0, 14)
            };
        }

        private static bool MatchesFilter(string title, string description, string filter)
        {
            if (string.IsNullOrWhiteSpace(filter)) return true;
            return title.Contains(filter, StringComparison.OrdinalIgnoreCase)
                || description.Contains(filter, StringComparison.OrdinalIgnoreCase);
        }

        // --- Presets & File Actions ---
        private void cmbPresets_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isInitializing || isUpdatingPresetCombo || modManager == null) return;
            if (cmbPresets.SelectedIndex < 0) return;

            switch (cmbPresets.SelectedIndex)
            {
                case 0:
                    currentSettings = new ModSettings();
                    SetStatus(LocalizationManager.T("Standard-Preset (Vanilla) wiederhergestellt.", "Default preset (Vanilla) restored."));
                    break;
                case 1:
                    currentSettings = modManager.LoadEmbeddedPreset("Balanced.json");
                    SetStatus(LocalizationManager.T("Preset 'Realismus Light (Ausgewogen)' geladen.", "Preset 'Realism Lite (Balanced)' loaded."));
                    break;
                case 2:
                    currentSettings = modManager.LoadEmbeddedPreset("Hardcore.json");
                    SetStatus(LocalizationManager.T("Preset 'Hardcore Real Economy' geladen.", "Preset 'Hardcore Real Economy' loaded."));
                    break;
                case 3:
                    currentSettings = modManager.LoadEmbeddedPreset("UsedTruckCareer.json");
                    SetStatus(LocalizationManager.T("Preset 'Gebrauchtwagen-Karriere' geladen.", "Preset 'Used Truck Career' loaded."));
                    break;
                case 4:
                    currentSettings = modManager.LoadEmbeddedPreset("Relaxed.json");
                    SetStatus(LocalizationManager.T("Preset 'Entspannt (Kein Schaden/Polizei)' geladen.", "Preset 'Relaxed (No Damage/Police)' loaded."));
                    break;
                case 5:
                    currentSettings = modManager.LoadEmbeddedPreset("Arcade.json");
                    SetStatus(LocalizationManager.T("Preset 'Schnelles Geld & XP (Arcade)' geladen.", "Preset 'Fast Money & XP (Arcade)' loaded."));
                    break;
            }

            UpdatePresetDescription();
            RebuildDashboard();
        }

        private void UpdatePresetDescription()
        {
            if (txtPresetDescription == null || cmbPresets == null) return;

            txtPresetDescription.Text = cmbPresets.SelectedIndex switch
            {
                0 => LocalizationManager.T(
                    "Unveränderte Originalwerte von SCS Software (15 €/km Basis, 1,0 XP/km).",
                    "Unmodified original SCS Software values (15 €/km base, 1.0 XP/km)."),
                1 => LocalizationManager.T(
                    "Ausgewogene Wirtschaft (9 €/km, 10h EU-Lenkzeit, faire Kredite, halbierter KI-Fahrer-Gewinn).",
                    "Balanced economy (9 €/km, 10h EU driving time, fair loans, reduced passive AI driver profit)."),
                2 => LocalizationManager.T(
                    "Echte Speditions-Simulation (2,20 €/km, 0,5 XP/km, 9h Lenk- / 11h Ruhezeit, knappe Margen).",
                    "True haulage simulation (2.20 €/km, 0.5 XP/km, 9h drive / 11h rest, tight real-world margins)."),
                3 => LocalizationManager.T(
                    "Fokus auf Gebraucht-LKWs: Großes & günstiges Angebot ab Level 0, günstigere Restauration.",
                    "Focus on used trucks: Large & cheap assortment from Level 0, cheaper workshop restoration."),
                4 => LocalizationManager.T(
                    "Stressfreies Fahren: 0 % Fahrzeug-/Frachtschaden, keine Bußgelder, 48h Lenkzeit & 18 €/km.",
                    "Stress-free cruising: 0% vehicle/cargo damage, no police fines, 48h driving limit & 18 €/km."),
                5 => LocalizationManager.T(
                    "Schneller Aufstieg: 35 €/km, 2,5 XP/km, 0 % permanenter Verschleiß, günstige Kredite & Sofort-Unlocks.",
                    "Fast progression: 35 €/km, 2.5 XP/km, 0% unfixable wear, cheap loans & instant unlocks."),
                _ => LocalizationManager.T(
                    "Benutzerdefinierte Werte aus importierter Mod-Datei aktiv.",
                    "Custom values from imported mod file active.")
            };
        }

        private void ClearPresetSelection()
        {
            if (cmbPresets == null) return;
            isUpdatingPresetCombo = true;
            cmbPresets.SelectedIndex = -1;
            isUpdatingPresetCombo = false;
            UpdatePresetDescription();
        }

        private void btnResetDefault_Click(object sender, RoutedEventArgs e)
        {
            isUpdatingPresetCombo = true;
            cmbPresets.SelectedIndex = 0;
            isUpdatingPresetCombo = false;

            currentSettings = new ModSettings();
            UpdatePresetDescription();
            RebuildDashboard();
            SetStatus(LocalizationManager.T(
                "Alle Werte auf Standard (Vanilla) zurückgesetzt.",
                "All values reset to default (Vanilla)."));
        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = LocalizationManager.T("SCS Mod-Dateien (*.scs;*.zip)|*.scs;*.zip", "SCS Mod Files (*.scs;*.zip)|*.scs;*.zip"),
                Title = LocalizationManager.T("Mod-Datei zum Bearbeiten auswählen", "Select Mod File to Edit")
            };

            if (ofd.ShowDialog() == true)
            {
                try
                {
                    string tempFolder = Path.Combine(Path.GetTempPath(), "ETS2_Import_" + Guid.NewGuid());
                    currentSettings = modManager.ImportMod(ofd.FileName, tempFolder, out ModMetadata? importedMeta);

                    if (importedMeta != null)
                    {
                        if (!string.IsNullOrWhiteSpace(importedMeta.PackageName))
                            txtModName.Text = importedMeta.PackageName;
                        if (!string.IsNullOrWhiteSpace(importedMeta.Author))
                            txtAuthor.Text = importedMeta.Author;
                    }

                    ClearPresetSelection();
                    RebuildDashboard();
                    SetStatus(LocalizationManager.T($"Mod importiert: {Path.GetFileName(ofd.FileName)}", $"Mod imported: {Path.GetFileName(ofd.FileName)}"));
                    MessageBox.Show(
                        LocalizationManager.T("Mod-Werte erfolgreich eingelesen!", "Mod values imported successfully!"),
                        LocalizationManager.T("Erfolg", "Success"),
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        LocalizationManager.T($"Fehler beim Importieren: {ex.Message}", $"Error importing mod: {ex.Message}"),
                        LocalizationManager.T("Fehler", "Error"),
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            string defaultName = GetSanitizedModFileName();

            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "SCS Mod (*.scs)|*.scs",
                Title = LocalizationManager.T("Mod-Paket exportieren", "Export Mod Package"),
                FileName = $"{defaultName}.scs"
            };

            if (sfd.ShowDialog() == true)
            {
                ExportToFile(sfd.FileName);
            }
        }

        private void btnExportToModFolder_Click(object sender, RoutedEventArgs e)
        {
            string docsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string ets2ModDir = Path.Combine(docsPath, "Euro Truck Simulator 2", "mod");

            if (!Directory.Exists(ets2ModDir))
            {
                var result = MessageBox.Show(
                    LocalizationManager.T(
                        $"Der ETS2 Mod-Ordner wurde unter:\n{ets2ModDir}\nnicht gefunden.\n\nSoll der Ordner jetzt erstellt werden?",
                        $"The ETS2 mod folder was not found at:\n{ets2ModDir}\n\nWould you like to create it now?"),
                    LocalizationManager.T("ETS2 Mod-Ordner", "ETS2 Mod Folder"),
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    Directory.CreateDirectory(ets2ModDir);
                }
                else
                {
                    return;
                }
            }

            string targetFile = Path.Combine(ets2ModDir, $"{GetSanitizedModFileName()}.scs");
            ExportToFile(targetFile);
        }

        private void ExportToFile(string targetFilePath)
        {
            try
            {
                string workDir = Path.Combine(Path.GetTempPath(), "ETS2_Export_" + Guid.NewGuid());

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
                    Description = LocalizationManager.T(
                        $"Erstellt mit dem ETS2 Economy Equalizer ({finalModName}).",
                        $"Created with ETS2 Economy Equalizer ({finalModName}).")
                };

                modManager.ExportMod(currentSettings, workDir, targetFilePath, meta);
                SetStatus(LocalizationManager.T($"Erfolgreich exportiert nach: {targetFilePath}", $"Successfully exported to: {targetFilePath}"));
                MessageBox.Show(
                    LocalizationManager.T(
                        $"Mod-Archiv (.scs) erfolgreich erstellt!\n\nSpeicherort:\n{targetFilePath}",
                        $"Mod archive (.scs) created successfully!\n\nLocation:\n{targetFilePath}"),
                    LocalizationManager.T("Export erfolgreich", "Export Successful"),
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    LocalizationManager.T($"Fehler beim Exportieren: {ex.Message}", $"Error exporting mod: {ex.Message}"),
                    LocalizationManager.T("Fehler", "Error"),
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private string GetSanitizedModFileName()
        {
            string rawName = string.IsNullOrWhiteSpace(txtModName.Text)
                ? "MyCustomMod"
                : txtModName.Text.Trim().Replace(" ", "_");

            foreach (char c in Path.GetInvalidFileNameChars())
            {
                rawName = rawName.Replace(c.ToString(), "");
            }

            return string.IsNullOrWhiteSpace(rawName) ? "MyCustomMod" : rawName;
        }

        private void SetStatus(string text)
        {
            if (txtStatus != null)
            {
                txtStatus.Text = text;
            }
        }
    }
}
