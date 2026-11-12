# ETS2 Economy Equalizer (ETS2EE)

🌍 **[🇬🇧 English](#-english-version)** | **[🇩🇪 Deutsch](#-deutsche-version)**

---

## 🇬🇧 English Version

A lightweight, powerful open-source tool for **Euro Truck Simulator 2** to customize the entire in-game economy without manually editing text files, and export your setup directly as a ready-to-play `.scs` mod with a single click.

> ### 🎮 Compatibility & Game Versions
> - **Main Version (`ETS2EE_v1.0.0.exe`):** Built for **ETS2 1.62 and newer** (includes full support for the new Bus / Coaches DLC skills).
> - **Legacy Version:** For **ETS2 ≤ 1.61**, a separate **Legacy Version** is available under [Releases](https://github.com/refskegg/ETS2-Economy-Equalizer/releases), as SCS Software expanded the structure of `skill_data.sii` starting with update 1.62.

---

### 🚚 Features (Version 1.0)

- **All 8 Economy Areas in One Place:**
  1. 🏦 **Bank & Loans (`bank_data.sii`):** Loan amounts, interest rates, durations, overdraft grace periods, and warning emails.
  2. 💥 **Damage & Wear (`damage_data.sii`):** Collision multipliers, component damage distribution, and ongoing wear (both fixable & permanent unfixable wear) per kilometer.
  3. 📈 **Economy & Cargo (`economy_data.sii`):** Base revenue per km, garage prices, fuel discounts, driving & sleeping hours, F7 emergency services, AI driver margins, and XP bonuses.
  4. 🚓 **Police & Fines (`police_data.sii`):** Fine amounts for every traffic offence, progressive speed camera multipliers, check delays, and offence probabilities (with/without a police patrol car nearby).
  5. 🔧 **Service & Restoration (`service_data.sii`):** Base fees and part price factors for complete restoration of trucks and owned trailers.
  6. ⭐ **Driver Skills (`skill_data.sii`):** XP bonuses, revenue increases, and maximum route distances per rank – including all **6 new Bus / Coaches DLC skills** (ETS2 1.62+).
  7. 🔓 **Events & Unlocks (`special_event_data.sii`):** Unlock thresholds for bank loans, truck dealers, and trailer ownership, plus maximum total credit limits.
  8. 🚛 **Used Truck Market (`used_vehicle_config.sii`):** Offer counts, refresh times, mileage limits, pre-existing wear, and price discounts at the used truck dealer.
- **6 Built-in Quick Presets:** Load directly via dropdown and customize further as you like:
  - ⚙️ *Default (Vanilla)* – Unmodified original SCS Software values
  - ⚖️ *Realism Lite (Balanced)* – Fair loans, 9 €/km, 10h EU driving time, reduced passive AI driver profits
  - 💀 *Hardcore Real Economy* – True haulage simulation (2.20 €/km, 0.5 XP/km, 9h drive / 11h rest, tight real-world margins)
  - 🚛 *Used Truck Career* – Large & affordable used truck assortment from Level 0 and cheaper workshop restoration
  - 🛡️ *Relaxed (No Damage/Police)* – Stress-free cruising with 0% vehicle/cargo damage and no police fines
  - ⚡ *Fast Money & XP (Arcade)* – Fast progression (35 €/km, 2.5 XP/km, 0% permanent wear, instant unlocks)
- **Collapsible Parameter Explanations (`ⓘ Details`):** Every single setting includes a clear explanation with its original default value, conversion formulas, and practical tips.
- **Live Search & Dashboard Overview:** Instantly find any parameter via the search bar (e.g., *"Loan"*, *"Speed"*, *"XP"*, *"Bus"*).
- **Mod Import & Direct Export:**
  - Exports a ready-to-use `.scs` mod archive (including `manifest.sii`, bilingual description, and icon) **directly into your `Documents\Euro Truck Simulator 2\mod` folder** if desired.
  - Existing `.scs` or `.zip` economy mods can be imported at any time for further editing.
- **Bilingual UI (German & English):** Switch the entire interface and all parameter explanations between **🇩🇪 DE** and **🇬🇧 EN** at any time with a single click.
- **No Installation Required:** Portable single-file application (`ETS2EE_v1.0.0.exe`) that runs out of the box without an installer or extra runtime dependencies.

---

### 🗺️ Roadmap / Planned

- **ATS Support:** Full support and dedicated profiles for **American Truck Simulator**.

---

### 📥 Download & Getting Started

1. Download the matching `.exe` from the [Releases](https://github.com/refskegg/ETS2-Economy-Equalizer/releases) page:
   - **`ETS2EE_v1.0.0.exe`** for **ETS2 1.62+**
   - **Legacy Version** for **ETS2 1.60 / 1.61**
2. Double-click the file to launch it.
   > **Note on Windows SmartScreen:**  
   > Since this is a private, unsigned open-source tool without a paid code-signing certificate, Windows may show a one-time warning on first launch. Simply click **"More info"** $\rightarrow$ **"Run anyway"**.
3. Pick a preset or customize your desired economy parameters.
4. Click **"📁 Direct to ETS2 Mod Folder"** (or **"🚀 Export Mod (.scs)"**) in the bottom right corner and activate the mod in the ETS2 Mod Manager.

---

### 💡 About the Project & AI Transparency

I created this tool because the vanilla economy in ETS2 often generates too much money too quickly for my taste, and manually editing game files becomes tedious over time.

Existing tools in this space didn't offer enough configuration options for me, which meant I still had to piece my own mods together manually.

The goal of **ETS2EE** is to provide an all-in-one solution: maximum freedom in tweaking parameters, paired with a user interface designed to be as clean and intuitive as possible.

Since I don't have a professional background in C# development, this tool was built with the help of **AI as a coding partner**. The entire source code is openly available here – feel free to take a look or contribute!

---

### 💬 Feedback & Contributions

If you find bugs, have requests for additional parameters, or want to leave feedback:
- Feel free to open a [GitHub Issue](https://github.com/refskegg/ETS2-Economy-Equalizer/issues).
- I am a hobbyist doing this for fun in my spare time.  
  Please bear with me if replies or bug fixes sometimes take a little while.

---
---

## 🇩🇪 Deutsche Version

Ein schlankes, leistungsstarkes Open-Source-Tool für **Euro Truck Simulator 2**, um die gesamte Spielwirtschaft ohne manuelles Bearbeiten von Textdateien individuell anzupassen und mit einem Klick als spielfertige `.scs`-Mod zu exportieren.

> ### 🎮 Kompatibilität & Spielversionen
> - **Hauptversion (`ETS2EE_v1.0.0.exe`):** Für **ETS2 1.62 und neuer** (inklusive Unterstützung für die neuen Bus-/Coaches-DLC-Skills).
> - **Legacy-Version:** Für **ETS2 ≤ 1.61** steht unter den [Releases](https://github.com/refskegg/ETS2-Economy-Equalizer/releases) eine separate **Legacy-Version** bereit, da SCS Software ab Version 1.62 die Struktur der `skill_data.sii` erweitert hat.

---

### 🚚 Features (Version 1.0)

- **Alle 8 Wirtschaftsbereiche an einem Ort:**
  1. 🏦 **Bank & Kredite (`bank_data.sii`):** Kredithöhen, Zinssätze, Laufzeiten, Dispo-Tage und Mahnungen.
  2. 💥 **Schaden & Verschleiß (`damage_data.sii`):** Kollisionsfaktoren, Bauteil-Schadensverteilung und laufender Verschleiß (reparierbar & permanenter Dauerschaden) pro Kilometer.
  3. 📈 **Wirtschaft & Fracht (`economy_data.sii`):** Basisvergütung pro km, Garagenpreise, Tankrabatte, Lenk- und Ruhezeiten, F7-Notdienst, KI-Fahrer-Margen und XP-Boni.
  4. 🚓 **Polizei & Bußgelder (`police_data.sii`):** Strafbeträge für jedes Delikt, Blitzer-Progression, Kontrollverzögerungen und Wahrscheinlichkeiten (mit/ohne Polizeistreife vor Ort).
  5. 🔧 **Werkstatt & Service (`service_data.sii`):** Kosten und Teilefaktoren für die Gesamtwiederherstellung von LKWs und eigenen Anhängern.
  6. ⭐ **Fähigkeiten / Skills (`skill_data.sii`):** XP-Boni, Umsatzsteigerungen und Maximaldistanzen je Rang – inklusive der **6 neuen Bus-/Coaches-DLC-Skills** (ab ETS2 1.62).
  7. 🔓 **Events & Freischaltungen (`special_event_data.sii`):** Wann Bankkredite, LKW-Händler und der eigene Anhängerkauf freigeschaltet werden, sowie das maximale Gesamtkreditlimit.
  8. 🚛 **Gebrauchtfahrzeuge (`used_vehicle_config.sii`):** Angebotsgröße, Erneuerungszeiten, Kilometerstände, Vorschäden und Preisrabatte beim Gebrauchtwagenhändler.
- **6 integrierte Schnell-Presets:** Direkt per Dropdown laden und nach Belieben weiter verfeinern:
  - ⚙️ *Standard (Vanilla)* – Unveränderte Originalwerte von SCS Software
  - ⚖️ *Realismus Light (Ausgewogen)* – Faire Kredite, 9 €/km, 10h Lenkzeit, reduzierter passiver KI-Fahrer-Gewinn
  - 💀 *Hardcore Real Economy* – Echte Speditions-Simulation (2,20 €/km, 0,5 XP/km, 9h Lenk- / 11h Ruhezeit, knappe Margen)
  - 🚛 *Gebrauchtwagen-Karriere* – Großes & günstiges Gebraucht-Angebot ab Level 0 und günstigere Restauration
  - 🛡️ *Entspannt (Kein Schaden/Polizei)* – Stressfreies Cruisen ohne Fahrzeug-/Frachtschaden und ohne Bußgelder
  - ⚡ *Schnelles Geld & XP (Arcade)* – Schneller Aufstieg (35 €/km, 2,5 XP/km, 0 % Dauerschaden, Sofort-Unlocks)
- **Ausklappbare Parameter-Erklärungen (`ⓘ Erklärung`):** Zu jedem einzelnen Wert gibt es eine verständliche Erklärung inklusive Original-Standardwert, Umrechnungsformeln und Praxis-Tipps.
- **Live-Suche & Dashboard-Übersicht:** Finde jeden Parameter sofort per Suchfeld (z. B. *„Kredit“*, *„Blitzer“*, *„XP“*, *„Bus“*).
- **Mod-Import & Direkt-Export:**
  - Exportiert eine fertig gepackte `.scs`-Mod (inkl. `manifest.sii`, zweisprachiger Beschreibung und Icon) auf Wunsch **direkt in deinen `Dokumente\Euro Truck Simulator 2\mod`-Ordner**.
  - Bestehende `.scs`- oder `.zip`-Wirtschaftsmods können jederzeit wieder importiert und weiterbearbeitet werden.
- **Zweisprachig (Deutsch & Englisch):** Komplette Oberfläche und alle Parameter-Erklärungen lassen sich jederzeit per Klick zwischen **🇩🇪 DE** und **🇬🇧 EN** umschalten.
- **Keine Installation nötig:** Portable Single-File-Anwendung (`ETS2EE_v1.0.0.exe`), die ohne Installer oder zusätzliche Laufzeitumgebung sofort startet.

---

### 🗺️ Ausblick / Geplant

- **ATS-Support:** Vollständige Unterstützung und angepasste Profile für den **American Truck Simulator**.

---

### 📥 Download & Erste Schritte

1. Lade dir die passende `.exe` unter den [Releases](https://github.com/refskegg/ETS2-Economy-Equalizer/releases) herunter:
   - **`ETS2EE_v1.0.0.exe`** für **ETS2 1.62+**
   - **Legacy-Version** für **ETS2 1.60 / 1.61**
2. Starte die Datei per Doppelklick.
   > **Hinweis zu Windows SmartScreen:**  
   > Da es sich um ein privates, nicht kostenpflichtig signiertes Open-Source-Tool handelt, warnt Windows beim ersten Start einmalig. Einfach auf **„Weitere Informationen“** $\rightarrow$ **„Trotzdem ausführen“** klicken.
3. Wähle ein Preset oder passe die gewünschten Wirtschafts-Parameter frei an.
4. Klicke unten rechts auf **„📁 Direkt in ETS2 Mod-Ordner“** (oder **„🚀 Mod (.scs) exportieren“**) und aktiviere die Mod im ETS2 Mod-Manager.

---

### 💡 Über das Projekt & KI-Transparenz

Ich habe das Tool entwickelt, weil mir das originale Wirtschaftssystem in ETS2 oft zu schnell zu viel Geld abgeworfen hat und manuelles Bearbeiten der Spieldateien auf Dauer umständlich ist.

Die bereits existierenden Tools in dem Bereich hatten mir jeweils zu wenige Einstellungsmöglichkeiten, sodass ich mir die Mods dennoch selbst zusammenbasteln musste.

Das Ziel des **ETS2EE** ist es, alles aus einer Hand zu haben: maximale Freiheit bei der Parameterwahl, gepaart mit einer so einfach und übersichtlich wie möglich gestalteten Benutzeroberfläche.

Da ich selbst keinen professionellen Hintergrund in der C#-Entwicklung habe, ist dieses Tool mit Unterstützung von **KI als Coding-Partner** entstanden. Der gesamte Quellcode ist hier transparent einsehbar – schaut gerne rein oder helft mit!

---

### 💬 Feedback & Mithilfe

Wenn ihr Fehler findet, Wünsche für Parameter habt oder Feedback dalassen wollt:
- Erstellt einfach ein [GitHub Issue](https://github.com/refskegg/ETS2-Economy-Equalizer/issues).
- Ich bin kompletter Laie und mache das aus Spaß in meiner Freizeit.  
  Bitte verzeiht, wenn ich mal nicht schnell antworten sollte oder Fehlerbehebungen länger auf sich warten lassen.


