﻿using AlphaModder.Constants;
using AlphaModder.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlphaModder.Utils
{
    class DataUtils
    {

        // save file to the location specified in the dialog.
        public static void saveAlphaFile(AlphaConfiguration alphaConfiguration)
        {
            // TODO improve this code
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Title = "Choose where to save your alpha file";

                if (saveFileDialog.ShowDialog().Equals(DialogResult.OK))
                {
                    File.WriteAllText(saveFileDialog.FileName, getAlphaFileString(alphaConfiguration));
                }
            }
        }

        // save the file directly to the specified path.  No dialog or confirmation.
        public static bool saveAlphaFile(AlphaConfiguration alphaConfiguration, String path)
        {
            try
            {
                File.WriteAllText(path, getAlphaFileString(alphaConfiguration));
                return true;
            } catch(Exception e)
            {
                MessageBox.Show("There was an error saving the file: \n"
                    + path + "\n\n" 
                    + e.StackTrace);
            }
            return false;
        }

        // sets the game resolution by rewriting the Alpha Centauri.ini file
        // with DirectDraw=0 for hi res and DirectDraw=1 for lo res
        public static void setResolution(bool highRes)
        {
            // read in the Alpha Centauri.ini contents
            List<string> ini = new List<string>(File.ReadAllLines(Properties.Settings.Default.AlphaCentauriIniPath));
            StringBuilder iniStringBuilder = new StringBuilder();

            // for each line in the contents, add it to the new ini file, but not if it's a DirectDraw setting
            foreach(string line in ini)
            {
                if (!line.StartsWith("DirectDraw"))
                {
                    iniStringBuilder.Append(line + "\n");
                }
            }

            // add the DirectDraw setting
            if (highRes)
                iniStringBuilder.Append(AlphaModderConstants.DIRECTDRAW_0_HIGH_RES_LINE);
            else
                iniStringBuilder.Append(AlphaModderConstants.DIRECTDRAW_1_LOW_RES_LINE);

            // save the file
            File.WriteAllText(Properties.Settings.Default.AlphaCentauriIniPath, iniStringBuilder.ToString());

        }

        public static String getGameInstallationDirectoryStr()
        {
            return Properties.Settings.Default.GameFolder;
        }

        public static String getPresetsDirectoryStr()
        {
            return Properties.Settings.Default.GameFolder + AlphaModderConstants.PRESETS_FOLDER_RELATIVE_PATH;
        }

        public static bool checkPresetExists(String presetName)
        {
            return File.Exists(getPresetAbsolutePath(presetName));
        }

        public static String getPresetAbsolutePath(String presetName)
        {
            return getPresetsDirectoryStr() + presetName + ".json";
        }

        // save preset json file
        // todo - refactor this
        public static bool savePresetJsonFile(String jsonString, String presetName)
        {
            if (checkPresetExists(presetName))
            {
                // if the user clicks Cancel or Close, do nothing.
                if(!DialogUtils.messageBoxOkCancel("Preset \"" + presetName + "\" already exists. Overwrite?"))
                {
                    return false;
                }
            }

            String directory = getPresetsDirectoryStr();
            Directory.CreateDirectory(directory);
            File.WriteAllText(getPresetAbsolutePath(presetName), jsonString);
            
            return true;
        }

        public static string getPresetAsJsonString(String presetName)
        {
            if (!checkPresetExists(presetName))
            {
                DialogUtils.messageBox("Unable to load preset: \n\n" + getPresetAbsolutePath(presetName) + "\n\nThe file could not be found.");
                return "{ }"; // return an empty json object string
            }
            return File.ReadAllText(getPresetAbsolutePath(presetName));
        }

        public static List<string> getPresetsList()
        {
            String presetsFolder = getPresetsDirectoryStr();
            Directory.CreateDirectory(presetsFolder);

            DirectoryInfo directoryInfo = new DirectoryInfo(presetsFolder);
            FileInfo[] presetFiles = directoryInfo.GetFiles("*.json");

            List<String> presetsList = new List<String>();
            foreach(FileInfo file in presetFiles)
            {
                String fileName = file.Name;
                String presetName = fileName.Substring(0, fileName.Length - 5);
                presetsList.Add(presetName);
            }
            presetsList.Sort();
            return presetsList;
        }

        private static String getAlphaFileString(AlphaConfiguration alphaConfiguration)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append(
@";
; This file was generated with Alpha Modder version " + AlphaModderConstants.VERSION + @"
;
; Sid Meier's ALPHA CENTAURI
;
; Alpha Centauri User-Configurable Rules
;
; Copyright (c) 1997, 1998 by Firaxis Games, Inc.
;
; ALPHA CENTAURI reads the rules of the game from this file at
; startup. Feel free, at your own risk, to experiment with editing
; this file. We recommend you make a backup copy of the original.
;
; If you are building a scenario and need custom rules, copy this
; file into a subdirectory with your scenario file before editing.
; Files in the same subdirectory as a scenario file automatically
; take precedence over files in the main game directory.
;

;
; FUNDAMENTAL RULES
;   You will find that many of the items on this list have been
;   finely balanced: small changes can have drastic effects on
;   gameplay.
;
#RULES
3,       ; Movement rate along roads
2,       ; Nutrient intake requirement for citizens
3,2      ; Numerator & Denominator for artillery fire damage
2,       ; Max artillery range (larger will break multiplayer)
8,       ; Max airdrop range w/o orbital insertion
10,      ; Nutrient cost multiplier
10,      ; Minerals cost multiplier
" + alphaConfiguration.getRuleTechDiscoveryRateLineStr() + @"
1,       ; Limits mineral increase for mine without road in square
-1,      ; Nutrient effect in mine square (0 or -1)
5,       ; Minimum base size to support specialists
" + alphaConfiguration.getRuleDronesInducedByGenejackFactoryLineStr() + @"
" + alphaConfiguration.getPopLimitWithoutHabComplexLineStr() + @"
" + alphaConfiguration.getPopLimitWithoutHabDomeLineStr() + @"
" + alphaConfiguration.getRulePrototypeCostPctLineStr() + @"
3,2,     ; Psi combat offense-to-defense ratio (LAND unit defending)
1,1,     ; Psi combat offense-to-defense ratio (SEA unit defending)
1,1,     ; Psi combat offense-to-defense ratio (AIR unit defending)
" + alphaConfiguration.getStartingEnergyLineStr() + @"
25,      ; Combat % -> intrinsic base defense
0,       ; Combat % -> attacking along road
0,       ; Combat % -> for attacking from higher elevation
0,       ; Combat penalty % -> attacking from lower elevation
25,      ; Combat % -> Mobile unit in open ground
0,       ; Combat % -> Defend vs. mobile in rough
25,      ; Combat % -> Infantry vs. Base
50,      ; Combat penalty % -> attack after airdrop
25,      ; Combat % -> Fanatic attack bonus
50,      ; Combat % -> Land based guns vs. ship artillery bonus
25,      ; Combat % -> Artillery bonus per level of altitude
" + alphaConfiguration.getGeneralTranceDefenseBonusLineStr() + @"
" + alphaConfiguration.getGeneralEmpathAttackBonusLineStr() + @"
50,      ; Combat penalty % -> Air superiority unit vs. ground unit
100,     ; Combat % -> Air superiority unit vs. air unit
50,      ; Combat penalty % -> Non-combat unit defending vs. combat unit
50,      ; Combat % -> Comm Jammer unit defending vs. mobile unit
100,     ; Combat % -> Bonus vs. ships caught in port
" + alphaConfiguration.getRuleAAABonusPctLineStr() + @"
25,      ; Combat % -> Defend in range of friendly Sensor
10,      ; Combat % -> Psi attack bonus/penalty per +PLANET
" + alphaConfiguration.getRuleRetoolPenaltyPctLineStr() + @"
" + alphaConfiguration.getRuleRetoolStrictnessLineStr() + @"
10,      ; Retool exemption (first X minerals not affected by penalty)
" + alphaConfiguration.getMinTurnsBetweenCouncilsLineStr() + @"
5,       ; Minerals for harvesting forest
8,       ; Territory: max distance from base
20,      ; Turns to corner Global Energy Market
CentPsi, ; Technology to improve fungus squares
" + alphaConfiguration.getRuleTechToEaseFungusMovementLineStr() + @"
" + alphaConfiguration.getRuleTechToBuildRoadsInFungusLineStr() + @"
Neural,  ; Technology to allow 2 special abilities for a unit
Gene,    ; Technology to allow 3 nutrients in a square
EcoEng,  ; Technology to allow 3 minerals in a square
EnvEcon, ; Technology to allow 3 energy in a square
" + alphaConfiguration.getRuleTechToAllowOrbitalInsertionLineStr() + @"
EcoEng2, ; Technology for +1 mining platform bonus
PlaEcon, ; Technology for economic victory
" + alphaConfiguration.getRuleProbeTeamsCanStealTechLineStr() + @"
" + alphaConfiguration.getRuleHumansCanAlwaysContactEachOtherLineStr() + @"
1,       ; If non-zero, humans can always contact each other in hotseat/email games
50,      ; Maximum % damage inflicted by arty versus units in base/bunker
99,      ; Maximum % damage inflicted by arty versus units in open
100,     ; Maximum % damage inflicted by arty versus units at sea
" + alphaConfiguration.getRuleGlobalWarmingFrequencyLineStr() + @"
" + alphaConfiguration.getRuleStartingYearLineStr() + @"
" + alphaConfiguration.getRuleEndingYearEasyLineStr() + @"
" + alphaConfiguration.getRuleEndingYearHardLineStr() + @"
" + alphaConfiguration.getObliterateBaseIsAtrocityLineStr());
            if (alphaConfiguration.isAlphax)
            {
                stringBuilder.Append(@"
10       ; Size of base for subspace generator
6        ; Number of subspace generators needed");
            }

            stringBuilder.Append(@"


;
; TERRAFORMING IMPROVEMENTS
;
; Name, Preq, Sea Name, Sea Preq, Rate, Order, Letter, Keystroke
;
; Rate      = # of turns to build
; Order     = Description of unit orders
; Letter    = Order letter
; Keystroke = Keystroke description (changing text does not change
;             the actual key mappings in the game)
;
#TERRAIN
" + alphaConfiguration.getTerraformFarmLineStr() + @"
" + alphaConfiguration.getTerraformSoilEnricherLineStr() + @"
" + alphaConfiguration.getTerraformMineLineStr() + @"
" + alphaConfiguration.getTerraformSolarCollectorLineStr() + @"
" + alphaConfiguration.getTerraformForestLineStr() + @"
" + alphaConfiguration.getTerraformRoadLineStr() + @"
" + alphaConfiguration.getTerraformMagTubeLineStr() + @"
" + alphaConfiguration.getTerraformBunkerLineStr() + @"
" + alphaConfiguration.getTerraformAirbaseLineStr() + @"
" + alphaConfiguration.getTerraformSensorArrayLineStr() + @"
" + alphaConfiguration.getTerraformRemoveFungusLineStr() + @"
" + alphaConfiguration.getTerraformPlantFungusLineStr() + @"
" + alphaConfiguration.getTerraformCondenserLineStr() + @"
" + alphaConfiguration.getTerraformEchelonMirrorLineStr() + @"
" + alphaConfiguration.getTerraformBoreholeLineStr() + @"
" + alphaConfiguration.getTerraformAquiferLineStr() + @"
" + alphaConfiguration.getTerraformRaiseLandLineStr() + @"
" + alphaConfiguration.getTerraformLowerLandLineStr() + @"
" + alphaConfiguration.getTerraformLevelLineStr() + @"
Monolith,         Disable, Monolith,         Disable,  8,  Place Monolith,  ?, ?, (this is here for map editor)


;
; RESOURCE INFORMATION
;
; Resource production (nutrient, minerals, energy, <unused>) for
; special squares. In normal squares, these values are determined
; by the temperature, rainfall, rockiness, etc. of the square.
;
; ""Bonus Square"" value for particular category is added to other
; production in a square.
;
; ""Improved Land"" means farm, mine, solar
; ""Improved Sea""  means desal, platform, harness
;
#RESOURCEINFO
Ocean Square,     1, 0, 0, 0,
Base Square,      2, 1, 1, 0,
Bonus Square,     2, 2, 2, 0, * Mineral +1 if mine present
Forest Square,    1, 2, 1, 0,
Recycling Tanks,  1, 1, 1, 0,
Improved Land,    1, *, *, 0, ""*"" columns are ignored entirely
Improved Sea,     2, 1, 3, 0, * Mineral +1 with Advanced Ecological Engineering
Monolith,         2, 2, 2, 0,
Borehole Square,  0, 6, 6, 0,

;
; WORLDBUILDER
;   These parameters control the finer points of the world builder,
;   along with the player's ""custom planet"" selections. Values are
;   automatically scaled based on the size of the world.
;
#WORLDBUILDER
384, ; Land base        (Seeded land size of a standard world)
256, ; Land modifier    (additional land from LAND selection: x0, x1, x2)
12,  ; Continent base   (Base size of a land mass seed)
24,  ; Continent modif. (Increased size from LAND selection: x0, x1, x2)
1,   ; Hills base       (Base # of extra hills)
2,   ; Hills modifier   (additional hills from TIDAL selection: x0, x1, x2)
4,   ; Plateau base     (Basic plateau size)
8,   ; Plateau modifier (Plateau modifier based on LAND selection: x0, x1, x2)
8,   ; Rivers base      (Basic # of rivers)
12,  ; Rivers rain mod. (Additional rivers based on RAIN selection)
14,  ; Solar Energy     (Latitude DIVISOR for temperature based on HEAT) Smaller # increases effect of HEAT selection
14,  ; Thermal band     (Latitude DIVISOR for thermal banding)  Smaller # widens hot bands
8,   ; Thermal deviance (Latitude DIVISOR for thermal deviance) Smaller # increases randomness
8,   ; Global Warming   (Latitude DIVISOR for global warming)   Smaller # increases effect of warming
5,   ; Sea Level Rises  (Magnitude of sea level changes from ice cap melting/freezing)
5,   ; Cloudmass peaks  (Size of cloud mass trapped by peaks)
3,   ; Cloudmass hills  (Size of cloud mass trapped by hills)
1,   ; Rainfall coeff.  (Multiplier for rainfall belts)
15,  ; Deep water       (Encourages fractal to grow deep water)
10,  ; Shelf            (Encourages fractal to grow shelf)
15,  ; Plains           (Encourages highland plains)
10,  ; Beach            (Encourages wider beaches)
10,  ; Hills            (Encourages hills x TIDAL selection)
25,  ; Peaks            (Encourages peaks)
1,   ; Fungus           (Fungus coefficient based on LIFE selection)
3,6,12,18,24 ; Ratio    (Continent size ratios)
36   ; Islands          (Higher # increases island count)

#WORLDSIZE
5
Tiny planet|(early conflict), 24, 48
Small planet,                 32, 64
Standard planet,              40, 80
Large planet,                 44, 90
" + alphaConfiguration.getPlanetSizeHugeLineStr() + @"

;
; TIME CONTROLS (Multiplayer)
;
; Name, Turn, Base, Unit, Event, Extra, Refresh, Accum
;
; Turn    = Minimum time per turn        (sec)
; Base    = Minimum time per base        (sec)
; Unit    = Minimum time per active unit (sec)
; Event   = Minimum time per event       (sec)
; Extra   = Extra time bonuses           (sec)
; Refresh = Player bonus refreshes       (# turns)
; Accum   = Max bonuses accumulated      (# bonuses at a time)
;
#TIMECONTROLS
None,      0, 0, 0, 0,  0,   0,  0,
Tight,    15, 2, 2, 4,  15, 10,  1,
Standard, 20, 3, 3, 8,  20, 10,  2,
Moderate, 30, 4, 4, 12, 30, 10,  3,
Loose,    45, 5, 5, 16, 45, 10,  3,
Custom,   20, 3, 3, 8,  20, 10,  2,

;
; TECHNOLOGY TREE
;
; Name, id, ai-mil, ai-tech, ai-infra, ai-colonize, preq(1), preq(2), flags
;
; Name     = Name of technology
;
; id       = 3 letter id code; this code is used when assigning
;            the tech as a prerequisite.
;
; power    = military value
; tech     = advance-of-knowledge value
; wealth   = infrastructure value
; growth   = colonization value
;
; preq(n)  = Prerequisite technology
;            a) 3 character id code of tech
;            b) ""nil"" to allow w/o prerequisite
;            c) ""no"" to disallow entirely from game
;
; flags    = Special tech flags
;            000000001 = ""Secrets"": first discoverer gains free tech
;            000000010 = Improves Probe Team success rate
;            000000100 = Increases commerce income
;            000001000 = Reveals map
;            000010000 = Allows gene warfare atrocity
;            000100000 = Increases intrinsic defense against gene warfare
;            001000000 = Increases ENERGY production in fungus
;            010000000 = Increases MINERALS production in fungus
;            100000000 = Increases NUTRIENT production in fungus
;
#TECHNOLOGY
Biogenetics,                Biogen,  0, 3, 2, 2, None,    None,    000100000
Industrial Base,            Indust,  2, 1, 3, 0, None,    None,    000000000
Information Networks,       InfNet,  0, 3, 2, 1, None,    None,    000000000
Applied Physics,            Physic,  4, 2, 1, 0, None,    None,    000000000
Social Psych,               Psych,   0, 1, 3, 2, None,    None,    000000000
Doctrine: Mobility,         Mobile,  2, 0, 0, 3, None,    None,    000000000
Centauri Ecology,           Ecology, 0, 1, 2, 3, None,    None,    100000000
Superconductor,             Super,   4, 2, 0, 0, OptComp, Indust,  000000000
Nonlinear Mathematics,      Chaos,   4, 3, 0, 0, Physic,  InfNet,  000000000
Applied Relativity,         E=Mc2,   1, 3, 2, 0, Super,   Subat,   000000000
Fusion Power,               Fusion,  3, 4, 3, 1, Algor,   Super,   000000000
Silksteel Alloys,           Alloys,  3, 0, 4, 2, Subat,   IndAuto, 000000000
Advanced Subatomic Theory,  Subat,   2, 3, 2, 0, Chemist, Poly,    000000000
High Energy Chemistry,      Chemist, 3, 1, 2, 0, Indust,  Physic,  000000000
Frictionless Surfaces,      Surface, 1, 3, 2, 0, Unified, IndRob,  000000000
Nanometallurgy,             Metal,   1, 1, 0, 3, ProbMec, DocInit, 000000000
Superstring Theory,         String,  3, 2, 0, 1, Chaos,   Cyber,   000000000");

            if (alphaConfiguration.isAlphax)
            {
                stringBuilder.Append(@"
Advanced Military Algorithms,MilAlg, 3, 0, 1, 2, AdapDoc, OptComp, 000000000");
            } else
            {
                stringBuilder.Append(@"
Advanced Military Algorithms,MilAlg,  3, 0, 1, 2, DocFlex, OptComp, 000000000");
            }

            stringBuilder.Append(@"
Monopole Magnets,           Magnets, 1, 1, 5, 0, String,  Alloys,  000000000
Matter Compression,         MatComp, 3, 1, 2, 0, Metal,   NanoMin, 000000000
Unified Field Theory,       Unified, 4, 3, 0, 1, Magnets, E=Mc2,   000000000
Graviton Theory,            Gravity, 3, 1, 0, 3, QuanMac, MindMac, 000000000
Polymorphic Software,       Poly,    2, 3, 1, 0, Indust,  InfNet,  000000010
Applied Gravitonics,        AGrav,   3, 1, 0, 4, Gravity, DigSent, 000000000
deleted,                    delete,  3, 2, 0, 2, Disable, Disable, 000000000
Quantum Power,              Quantum, 3, 4, 3, 0, Surface, PlaEcon, 000000000
Singularity Mechanics,      SingMec, 1, 4, 3, 0, Create,  HAL9000, 000000000
Controlled Singularity,     ConSing, 4, 1, 2, 0, SingMec, AGrav,   000000000
Temporal Mechanics,         TempMec, 0, 1, 3, 2, Eudaim,  Matter,  001000000
Probability Mechanics,      ProbMec, 1, 1, 3, 2, DocSec,  Algor,   000000000
Pre-Sentient Algorithms,    Algor,   2, 4, 3, 2, MilAlg,  Cyber,   000000010
Super Tensile Solids,       Solids,  1, 0, 5, 2, MatComp, Space,   000000000
Planetary Networks,         PlaNets, 0, 4, 3, 1, InfNet,  None,    000000000
Digital Sentience,          DigSent, 0, 4, 3, 2, IndRob,  MindMac, 000000010");

            if (alphaConfiguration.isAlphax)
            {
                stringBuilder.Append(@"
Self-Aware Machines,        HAL9000, 0, 4, 3, 3, NewMiss, DigSent, 000000010");
            }
            else
            {
                stringBuilder.Append(@"
Self-Aware Machines,        HAL9000, 0, 4, 3, 3, Space,   DigSent, 000000010");
            }

            stringBuilder.Append(@"
Doctrine: Initiative,       DocInit, 2, 0, 0, 4, DocFlex, IndAuto, 000000000
Doctrine: Flexibility,      DocFlex, 2, 0, 1, 4, Mobile,  None,    000000000
Intellectual Integrity,     Integ,   0, 1, 3, 4, EthCalc, DocLoy,  000000000
Synthetic Fossil Fuels,     Fossil,  1, 0, 2, 4, Chemist, Gene,    000000000
Doctrine: Air Power,        DocAir,  3, 0, 3, 4, Fossil,  DocFlex, 000000000
Photon/Wave Mechanics,      DocSec,  3, 2, 2, 0, E=Mc2,   Alloys,  000000000
Mind/Machine Interface,     MindMac, 4, 0, 2, 2, DocAir,  Neural,  000000010
Nanominiaturization,        NanoMin, 1, 0, 4, 3, Magnets, SupLube, 000000000
Doctrine: Loyalty,          DocLoy,  3, 0, 2, 2, Mobile,  Psych,   000000000
Ethical Calculus,           EthCalc, 0, 1, 3, 3, Psych,   None,    000000000
Industrial Economics,       IndEcon, 0, 0, 5, 2, Indust,  None,    000000100
Industrial Automation,      IndAuto, 0, 1, 4, 3, IndEcon, PlaNets, 000000100
Centauri Meditation,        CentMed, 0, 0, 2, 4, EcoEng,  CentEmp, 001000000
Secrets of the Human Brain, Brain,   1, 5, 0, 3, Psych,   Biogen,  000000001
Gene Splicing,              Gene,    0, 2, 4, 3, Biogen,  EthCalc, 000100000
Bio-Engineering,            BioEng,  0, 2, 3, 2, Gene,    Neural,  000100000
Biomachinery,               BioMac,  3, 1, 4, 1, MindMac, Viral,   000100000
Neural Grafting,            Neural,  3, 1, 1, 1, Brain,   IndAuto, 000000000
Cyberethics,                Cyber,   1, 3, 4, 0, PlaNets, Integ,   000000000
Eudaimonia,                 Eudaim,  0, 0, 3, 4, SentEco, WillPow, 000000000
The Will to Power,          WillPow, 0, 3, 1, 4, HomoSup, CentPsi, 000000000");

            if (alphaConfiguration.isAlphax)
            {
                stringBuilder.Append(@"
Threshold of Transcendence, Thresh,  0, 1, 3, 4, SecMani, TempMec, 010000000");
            }
            else
            {
                stringBuilder.Append(@"
Threshold of Transcendence, Thresh,  0, 1, 3, 4, Create,  TempMec, 010000000");
            }

            stringBuilder.Append(@"
Matter Transmission,        Matter,  1, 0, 3, 2, NanEdit, AlphCen, 010000000
Centauri Empathy,           CentEmp, 0, 1, 0, 6, Brain,   Ecology, 000000000
Environmental Economics,    EnvEcon, 0, 0, 4, 3, IndEcon, EcoEng,  000000100
Ecological Engineering,     EcoEng,  0, 0, 3, 4, Ecology, Gene,    000000000");

            if (alphaConfiguration.isAlphax)
            {
                stringBuilder.Append(@"
Planetary Economics,        PlaEcon, 0, 0, 4, 3, AdapEco, Integ,   000000100");
            }
            else
            {
                stringBuilder.Append(@"
Planetary Economics,        PlaEcon, 0, 0, 4, 3, EnvEcon, Integ,   000000100");
            }

            stringBuilder.Append(@"
Adv. Ecological Engineering,EcoEng2, 0, 0, 4, 2, Fusion,  EnvEcon, 000000000
Centauri Psi,               CentPsi, 0, 1, 1, 6, CentGen, EcoEng2, 100000000
Secrets of Alpha Centauri,  AlphCen, 0, 4, 0, 2, CentPsi, SentEco, 001001001
Secrets of Creation,        Create,  1, 4, 1, 0, Unified, WillPow, 000000001
Advanced Spaceflight,       Space,   2, 4, 2, 3, Orbital, SupLube, 000000000
Homo Superior,              HomoSup, 3, 2, 1, 4, BioMac,  DocInit, 000000000
Organic Superlubricant,     SupLube, 3, 1, 2, 0, Fusion,  Fossil,  000000000
Quantum Machinery,          QuanMac, 3, 1, 4, 0, Quantum, Metal,   000000000
deleted,                    deleted, 0, 0, 5, 0, Disable, Disable, 000000000
Matter Editation,           NanEdit, 1, 2, 3, 1, HAL9000, Solids,  000100000
Optical Computers,          OptComp, 2, 4, 1, 0, Physic,  Poly,    000000000
Industrial Nanorobotics,    IndRob,  3, 1, 8, 1, NanoMin, IndAuto, 000000100
Centauri Genetics,          CentGen, 0, 2, 0, 5, CentMed, Viral,   010000000
Sentient Econometrics,      SentEco, 1, 1, 3, 4, PlaEcon, DigSent, 000000100
Retroviral Engineering,     Viral,   4, 2, 0, 2, BioEng,  MilAlg,  000110000
Orbital Spaceflight,        Orbital, 0, 4, 3, 3, DocAir,  Algor,   000000000");

            if (alphaConfiguration.isAlphax)
            {
                stringBuilder.Append(@"
Progenitor Psych,           PrPsych, 0, 2, 3, 4, None,    None,    000000000
Field Modulation,           FldMod,  4, 2, 0, 2, PrPsych, Ecology, 000000000
Adaptive Doctrine,          AdapDoc, 5, 2, 0, 0, Poly,    DocFlex, 000000000
Adaptive Economics,         AdapEco, 0, 1, 5, 2, PrPsych, IndEcon, 000000000
Bioadaptive Resonance,      Bioadap, 5, 2, 0, 0, FldMod,  CentEmp, 000000000
Sentient Resonance,         SentRes, 5, 2, 0, 0, Bioadap, CentPsi, 000000000
Secrets of the Manifolds,   SecMani, 0, 5, 4, 2, SentRes, AlphCen, 000000001
N-Space Compression,        NewMiss, 4, 0, 2, 0, Orbital, BioMac,  000000000
String Resonance,           BFG9000, 8, 0, 0, 0, SecMani, ConSing, 000000000
User Technology,            User,    0, 2, 0, 0, Disable, Disable, 000000000");
            }
            else
            {
                stringBuilder.Append(@"
User Technology 0,          User0,   0, 2, 0, 0, Disable, Disable, 000000000
User Technology 1,          User1,   0, 2, 0, 0, Disable, Disable, 000000000
User Technology 2,          User2,   0, 2, 0, 0, Disable, Disable, 000000000
User Technology 3,          User3,   0, 2, 0, 0, Disable, Disable, 000000000
User Technology 4,          User4,   0, 2, 0, 0, Disable, Disable, 000000000
User Technology 5,          User5,   0, 2, 0, 0, Disable, Disable, 000000000
User Technology 6,          User6,   0, 2, 0, 0, Disable, Disable, 000000000
User Technology 7,          User7,   0, 2, 0, 0, Disable, Disable, 000000000
User Technology 8,          User8,   0, 2, 0, 0, Disable, Disable, 000000000
User Technology 9,          User9,   0, 2, 0, 0, Disable, Disable, 000000000");
            }

            stringBuilder.Append(@"
Transcendent Thought,       TranT,   0, 2, 0, 0, Thresh,  ConSing, 000000000

;
; Chassis
;
; Names..., Speed, Triad, Range, Cargo, Cost, Preq, ...Large names
;
; Names...= First two names used for offensive units.
;           Second two names used for defensive units.
;           M1 = Masculine Singular (For foreign translations)
;           M2 = Masculine Plural
;           (etc. Use F for female, N for neuter)
;
; Speed   = # of moves
; Triad   = Movement (0=Land 1=Sea 2=Air)
; Range   = Range in turns from base (air units only)
; Missile = Chassis is a ""missile"" (destroyed after attacking)
; Cargo   = # units transported (multiply by reactor rating)
; Cost    = Cost factor of chassis type (normally equal to speed)
; Preq    = Technology required (see TECHNOLOGY)
;
#CHASSIS
" + alphaConfiguration.getChassisInfantryLineStr() + @"
" + alphaConfiguration.getChassisSpeederLineStr() + @"
" + alphaConfiguration.getChassisHovertankLineStr() + @"
" + alphaConfiguration.getChassisFoilLineStr() + @"
" + alphaConfiguration.getChassisCruiserLineStr() + @"
" + alphaConfiguration.getChassisNeedlejetLineStr());

            if (alphaConfiguration.isAlphax)
            {
                stringBuilder.Append(@"
" + alphaConfiguration.getChassisCopterLineStr());
            }
            else
            {
                stringBuilder.Append(@"
" + alphaConfiguration.getChassisCopterLineStr());
            }

            stringBuilder.Append(@"
" + alphaConfiguration.getChassisGravshipLineStr() + @"
" + alphaConfiguration.getChassisMissileLineStr() + @"

;
; Reactors
;
; Name, power, preq
;
#REACTORS
" + alphaConfiguration.getReactorFissionLineStr() + @"
" + alphaConfiguration.getReactorFusionLineStr() + @"
" + alphaConfiguration.getReactorQuantumLineStr() + @"
" + alphaConfiguration.getReactorSingularityLineStr() + @"

;
; Weapons & non-combat packages
;
; Name, Name2, Offense, Mode, Cost, Preq
;
; Name    = Full name
; Name2   = Short name
; Offense = Attack rating (-1 = Psi Offense)
; Mode    = Offense mode (or noncombat package type)
;           Combat modes: 0=Projectile, 1=Energy, 2 = Missile
;           Noncombat: 7=Transport 8=Colonist
;                      9=Terraformer 10=Convoy 11=InfoWar
;                      12=Artifact
; Cost   = Cost factor of weapon or package
; Icon   = Special unit icon (used only if chassis is Infantry)
; Preq   = Technology prerequisite (see TECHNOLOGY)
;
#WEAPONS
Hand Weapons,         Gun,            1, 0, 1, -1, None,
Laser,                Laser,          2, 0, 2, -1, Physic,
Particle Impactor,    Impact,         4, 0, 4, -1, Chaos,
Gatling Laser,        Gatling,        5, 1, 5, -1, Super,
Missile Launcher,     Missile,        6, 2, 6, -1, Fossil,
Chaos Gun,            Chaos,          8, 0, 8, -1, String,
Fusion Laser,         Fusion,        10, 1,10, -1, SupLube,
Tachyon Bolt,         Tachyon,       12, 1,12, -1, Unified,
Plasma Shard,         Shard,         13, 2,13, -1, Space,
Quantum Laser,        Quantum,       16, 1,16, -1, QuanMac,
Graviton Gun,         Graviton,      20, 0,20, -1, AGrav,
Singularity Laser,    Singularity,   24, 1,24, -1, ConSing,");

            if (alphaConfiguration.isAlphax)
            {
                stringBuilder.Append(@"
Resonance Laser,      R-Laser,        6, 1, 8, -1, Bioadap,
Resonance Bolt,       R-Bolt,        12, 1,16, -1, SentRes,
String Disruptor,     String,        30, 1,40, -1, BFG9000,");
            }

            stringBuilder.Append(@"
" + alphaConfiguration.getWeaponPsiAttackLineStr() + @"
" + alphaConfiguration.getWeaponPlanetBusterLineStr() + @"
" + alphaConfiguration.getWeaponColonyModuleLineStr() + @"
" + alphaConfiguration.getWeaponTerraformingUnitLineStr() + @"
" + alphaConfiguration.getWeaponTransportLineStr() + @"
" + alphaConfiguration.getWeaponSupplyLineStr() + @"
" + alphaConfiguration.getWeaponProbeTeamLineStr() + @"
Alien Artifact,       Artifact,       0,12,36, -1, Disable,
" + alphaConfiguration.getWeaponConventionalPayloadLineStr());

            if (alphaConfiguration.isAlphax)
            {
                stringBuilder.Append(@"
Tectonic Payload,     Tectonic,       0,13,24, -1, NewMiss
Fungal Payload,       Fungal,         0,14,24, -1, NewMiss");
            }

            stringBuilder.Append(@"

;
; Armor
;
; Name, Name2, Armor, Mode, Cost, Preq
;
; Name    = Full Name
; Name2   = Short Name
; Armor   = Armor rating (-1 = Psi)
; Mode    = Armor mode (0=Projectile, 1=Energy, 2=Binary)
; Cost    = Cost factor of armor (should usually equal Armor)
; Preq    = Technology prerequisite (see TECHNOLOGY)
;
#DEFENSES
No Armor,            Scout,       1, 0, 1, None,
Synthmetal Armor,    Synthmetal,  2, 0, 2, Indust,
Plasma Steel Armor,  Plasma,      3, 2, 3, Chemist,
Silksteel Armor,     Silksteel,   4, 1, 4, Alloys,
Photon Wall,         Photon,      5, 1, 5, DocSec,
Probability Sheath,  Probability, 6, 2, 6, ProbMec,
Neutronium Armor,    Neutronium,  8, 1, 8, MatComp,
Antimatter Plate,    Antimatter, 10, 2,10, NanEdit,
Stasis Generator,    Stasis,     12, 2,12, TempMec,
" + alphaConfiguration.getDefensePsiDefenseLineStr());

            if (alphaConfiguration.isAlphax)
            {
                stringBuilder.Append(@"
Pulse 3 Armor,       3-Pulse,     3, 1, 5, AdapDoc,
Resonance 3 Armor,   3-Res,       3, 1, 5, FldMod,
Pulse 8 Armor,       8-Pulse,     8, 1, 11, Solids,
Resonance 8 Armor,   8-Res,       8, 1, 11, SentRes,");
            }

            stringBuilder.Append(@"

;
; Special Unit Abilities
;
; Name, Cost, Preq, Abbrev, Desc
;
; Name   = Name of ability
; Cost   = Cost factor of ability
;          1+ = Straight Cost; 25% increase per unit of cost
;           0 = None
;          -1 = Increases w/ ratio of weapon to armor: 0, 1, or 2.
;               Rounded DOWN. Never higher than 2.
;               Examples: For a W1,A2 unit, cost is 0
;                         For a W3,A2 unit, cost is 1 (3/2 rounded down)
;                         For a W6,A3 unit, cost is 2
;          -2 = Increases w/ weapon value
;          -3 = Increases w/ armor value
;          -4 = Increases w/ speed value
;          -5 = Increases w/ weapon+armor value
;          -6 = Increases w/ weapon+speed value
;          -7 = Increases w/ armor+speed value
; Preq   = Technology prerequisite (see TECHNOLOGY)
; Abbrev = Abbreviation for use in unit names
; Desc   = Description of ability (keep it brief!)
;
; Flags  =");

            if (alphaConfiguration.isAlphax)
            {
                stringBuilder.Append(@"
;          000000000001 = Allowed for Land units
;          000000000010 = Allowed for Sea units
;          000000000100 = Allowed for Air units
;          000000001000 = Allowed for Combat units
;          000000010000 = Allowed for Terraformer units
;          000000100000 = Allowed for Noncombat units (non-terraformer)
;          000001000000 = Not allowed for probe teams
;          000010000000 = Not allowed for psi units
;          000100000000 = Transport units only
;          001000000000 = Not allowed for fast-moving units
;          010000000000 = Cost increased for land units
;          100000000000 = Only allowed on probe teams
;
#ABILITIES
" + alphaConfiguration.getAbilitySuperFormerLineStr() + @"
Deep Radar,             0, MilAlg,   ,          010000111111, Sees 2 spaces
Cloaking Device,        1, Surface,  Cloaked,   000001111001, Invisible; Ignores ZOCs
Amphibious Pods,        1, DocInit,  Amphibious,000000001001, Attacks from ship
" + alphaConfiguration.getAbilityDropPodsLineStr() + @"
Air Superiority,        1, DocAir,   SAM,       000000001111, Attacks air units
" + alphaConfiguration.getAbilityDeepPressureHullLineStr() + @"
" + alphaConfiguration.getAbilityCarrierDeckLineStr() + @"
" + alphaConfiguration.getAbilityAAATrackingLineStr() + @"
Comm Jammer,           -1, Subat,    ECM,       000010111001, +50% vs. fast units
Antigrav Struts,        1, Gravity,  Grav,      000000111001, +1 movement rate (or +Reactor*2 for Air)
" + alphaConfiguration.getAbilityEmpathLineStr() + @"
Polymorphic Encryption, 1, Algor,    Secure,    000000111111, x2 cost to subvert
" + alphaConfiguration.getAbilityFungicideTanksLineStr() + @"
High Morale,            1, Integ,    Trained,   000000001111, Gains morale upgrade
Heavy Artillery,       -7, Poly,     Artillery, 000010001001, Bombards
" + alphaConfiguration.getAbilityCleanReactorLineStr() + @"
Blink Displacer,        1, Matter,   Blink,     000000001111, Bypass base defenses
" + alphaConfiguration.getAbilityTranceLineStr() + @"
" + alphaConfiguration.getHeavyTransportSpecAbilityLineStr() + @"
" + alphaConfiguration.getAbilityNerveGasPodsLineStr() + @"
" + alphaConfiguration.getAbilityRepairBayLineStr() + @"
" + alphaConfiguration.getAbilityNonLethalMethodsPoliceLineStr() + @"
Slow Unit,              0, Disable,  Slow,      000000111111, -1 moves
Soporific Gas Pods,     1, Bioadap,  Gas,       000001001101, -2 Enemy morale vs. non-native
Dissociative Wave,      2, CentPsi,  Wave,      000000111111, Fizzles special abilities
Marine Detachment,      1, AdapDoc,  Marine,    000001001010, Capture enemy ships
Fuel Nanocells,         1, MatComp,  Nanocell,  000000111100, Increased air range
Algorithmic Enhancement,1, NanoMin,  Enhanced,  100000111111, Halves probe team failure
");
            }
            else
            {
                stringBuilder.Append(@"
;          00000000001 = Allowed for Land units
;          00000000010 = Allowed for Sea units
;          00000000100 = Allowed for Air units
;          00000001000 = Allowed for Combat units
;          00000010000 = Allowed for Terraformer units
;          00000100000 = Allowed for Noncombat units (non-terraformer)
;          00001000000 = Not allowed for probe teams
;          00010000000 = Not allowed for psi units
;          00100000000 = Transport units only
;          01000000000 = Not allowed for fast-moving units
;          10000000000 = Cost increased for land units
;
#ABILITIES
" + alphaConfiguration.getAbilitySuperFormerLineStr() + @"
Deep Radar,             0, MilAlg,   ,          10000111111, Sees 2 spaces
Cloaking Device,        1, Surface,  Cloaked,   00001111001, Invisible; Ignores ZOCs
Amphibious Pods,        1, DocInit,  Amphibious,00000001001, Attacks from ship
" + alphaConfiguration.getAbilityDropPodsLineStr() + @"
Air Superiority,        1, DocAir,   SAM,       00000001111, Attacks air units
" + alphaConfiguration.getAbilityDeepPressureHullLineStr() + @"
" + alphaConfiguration.getAbilityCarrierDeckLineStr() + @"
" + alphaConfiguration.getAbilityAAATrackingLineStr() + @"
Comm Jammer,           -1, Subat,    ECM,       00010111001, +50% vs. fast units
Antigrav Struts,        1, Gravity,  Grav,      00000111001, +1 movement rate (or +Reactor*2 for Air)
" + alphaConfiguration.getAbilityEmpathLineStr() + @"
Polymorphic Encryption, 1, Algor,    Secure,    00000111111, x2 cost to subvert
" + alphaConfiguration.getAbilityFungicideTanksLineStr() + @"
High Morale,            1, Integ,    Trained,   00000001111, Gains morale upgrade
Heavy Artillery,       -7, Poly,     Artillery, 00010001001, Bombards
" + alphaConfiguration.getAbilityCleanReactorLineStr() + @"
Blink Displacer,        1, Matter,   Blink,     00000001111, Bypass base defenses
" + alphaConfiguration.getAbilityTranceLineStr() + @"
" + alphaConfiguration.getHeavyTransportSpecAbilityLineStr() + @"
" + alphaConfiguration.getAbilityNerveGasPodsLineStr() + @"
" + alphaConfiguration.getAbilityRepairBayLineStr() + @"
" + alphaConfiguration.getAbilityNonLethalMethodsPoliceLineStr() + @"
Slow Unit,              0, Disable,  Slow,      00000111111, -1 moves");
            }

            stringBuilder.Append(@"

;
; Morale levels
;
; Names of morale levels. Default morale level is ""Green"". ""Very Green""
; morale is obtained only with a net -1 morale modifier.
;
#MORALE
Very Green,  Hatchling
Green,       Larval Mass
Disciplined, Pre-Boil
Hardened,    Boil
Veteran,     Mature Boil
Commando,    Great Boil
Elite,       Demon Boil

;
; Combat modes
;   Projectile weapons receive a bonus against Energy Armor.
;   Energy weapons receive a bonus against Projectile Armor.
;   No weapon receives a bonus against Binary Armor.
;   Missile weapons never receive a bonus.
;
#DEFENSEMODES
Projectile, Proj-,   Proj., P
Energy,     Energy-, Ener., E
Binary,     Binary-, Bin.,  B

#OFFENSEMODES
Projectile, Proj-,    Proj., P
Energy,     Energy-,  Ener., E
Missile,    Missile-, Miss., M

;
; Basic Units - Units predesigned for all players
;
; Name, Chassis, Weapon, Armor, Plan, Cost, Carry, Preq, Icon, Abil
;
; Name    = Name of unit
; Chassis = Chassis type   (index to CHASSIS list, above)
; Weapon  = Weapon/package (index to WEAPONS list, above)
; Armor   = Armor          (index to ARMOR   list, above)
;
; Plan    = Unit ""plan"" for AI purposes:");

            if (alphaConfiguration.isAlphax)
                stringBuilder.Append(@"
;          -1  = Auto Calculate");
            else
                stringBuilder.Append(@"
;           -1 = Auto Calculate");

            stringBuilder.Append(@"
;           0  = Offensive
;           1  = Combat
;           2  = Defensive
;           3  = Reconnaisance
;           4  = Air Superiority
;           5  = Planet Buster
;           6  = Naval Superiority
;           7  = Naval Transport
;           8  = Colonization
;           9  = Terraforming
;           10 = Supply Convoy
;           11 = Info Warfare
;           12 = Alien Artifact
; Cost    = Cost in minerals (0 = Autocalculate)
; Carry   = Carrying capacity (0 = Autocalculate)
; Preq    = Technology prerequisite (see TECHNOLOGY, above)
; Icon    = Special icon, if any
; Abil    = Special ability flags
;
#UNITS");

            if (alphaConfiguration.isAlphax)
            {
                stringBuilder.Append(@"
23
Colony Pod,             Infantry, Colony Pod,   Scout,      8, 0, 0, None,    -1, 00000000000000000000000000
Formers,                Infantry, Formers,      Scout,      9, 0, 0, Ecology, -1, 00000000000000000000000000
Scout Patrol,           Infantry, Gun,          Scout,      3, 0, 0, None,    -1, 00000000000000000000000000
Transport Foil,         Foil,     Transport,    Scout,      7, 0, 0, DocFlex, -1, 00000000000000000000000000
*Sea Formers,           Foil,     Formers,      Scout,      9, 0, 0, Disable, -1, 00000000000000000000000000
Supply Crawler,         Infantry, Supply,       Scout,     10, 0, 0, IndAuto, -1, 00000000000000000000000000
" + alphaConfiguration.getUnitProbeTeamLineString() + @"
Alien Artifact,         Infantry, Artifact,     Scout,     12,10, 0, Disable,  2, 00000000000000000000000000
" + alphaConfiguration.getUnitMindWormsLineStr() + @"
" + alphaConfiguration.getUnitIsleOfTheDeepLineStr() + @"
" + alphaConfiguration.getUnitLocustsOfChironLineStr() + @"
Unity Rover,            Speeder,  Gun,          Scout,      3, 0, 0, Disable, -1, 00000000000000000000000000
Unity Scout Chopper,    Copter,   Gun,          Scout,      4, 0, 0, Disable, -1, 00000000000000000000100000
Unity Foil,             Foil,     Transport,    Scout,      7, 0, 0, Disable, -1, 00100000000000000000000000
Sealurk,                Foil,     Psi,          Psi,        6, 6, 0, CentPsi,  4, 00000000000000000001000000
Spore Launcher,         Infantry, Psi,          Psi,        0, 5, 0, Bioadap,  5, 00000000001000000000000000
Battle Ogre MK1,        Infantry, R-Laser,      3-Res,      0,10, 0, Disable,  6, 00010000000000000000000000
Battle Ogre MK2,        Infantry, R-Bolt,       8-Res,      0,15, 0, Disable,  6, 10010000000000000000000000
Battle Ogre MK3,        Speeder,  String,       Stasis,     0,20, 0, Disable,  6, 10010000000000000000000000
Fungal Tower,           Infantry, Psi,          Psi,        3, 0, 0, Disable,  1, 00000000000000000000000000
Unity Mining Laser,     Infantry, Laser,        Scout,      0, 0, 0, Disable, -1, 00000000000000000000000000
Sea Escape Pod,         Foil,     Colony Pod,   Scout,      8, 0, 0, Disable, -1, 00000000000000000000000000
Unity Gunship,          Foil,     Gun,          Scout,     -1, 0, 0, Disable, -1, 00000000000000000000000000
");
            }
            else
            {
                stringBuilder.Append(@"
14
Colony Pod,             Infantry, Colony Pod,   Scout,      8, 0, 0, None,    -1, 000000000000000000000000
Formers,                Infantry, Formers,      Scout,      9, 0, 0, Ecology, -1, 000000000000000000000000
Scout Patrol,           Infantry, Gun,          Scout,      3, 0, 0, None,    -1, 000000000000000000000000
Transport Foil,         Foil,     Transport,    Scout,      7, 0, 0, DocFlex, -1, 000000000000000000000000
*Sea Formers,           Foil,     Formers,      Scout,      9, 0, 0, Disable, -1, 000000000000000000000000
Supply Crawler,         Infantry, Supply,       Scout,     10, 0, 0, IndAuto, -1, 000000000000000000000000
" + alphaConfiguration.getUnitProbeTeamLineString() + @"
Alien Artifact,         Infantry, Artifact,     Scout,     12,10, 0, Disable,  2, 000000000000000000000000
" + alphaConfiguration.getUnitMindWormsLineStr() + @"
" + alphaConfiguration.getUnitIsleOfTheDeepLineStr() + @"
" + alphaConfiguration.getUnitLocustsOfChironLineStr() + @"
Unity Rover,            Speeder,  Gun,          Scout,      3, 0, 0, Disable, -1, 000000000000000000000000
Unity Scout Chopper,    'Copter,  Gun,          Scout,      4, 0, 0, Disable, -1, 000000000000000000100000
Unity Foil,             Foil,     Transport,    Scout,      7, 0, 0, Disable, -1, 100000000000000000000000
");
            }

            stringBuilder.Append(@"
;
; Base Facilities
;
; Name, Cost, Maint, Preq, Free, Effect");
            if (alphaConfiguration.isAlphax)
                stringBuilder.Append(", (for SPs only) ai-fight, ai-mil, ai-tech, ai-infra, ai-colonize");

            stringBuilder.Append(@"
;
; Name  = Name of facility type
; Cost  = Construction cost in minerals (x Minerals multiplier in RULES)
; Maint = Maintenance cost in energy per turn
; Preq  = Technology prerequisite (see TECHNOLOGY)
; Free  = No longer supported.
; Effect= Brief description of effect");

            if (alphaConfiguration.isAlphax)
                stringBuilder.Append(@"
; ai-fight=Corresponds with AI 'aggressiveness' setting of -1, 0, or 1
; ai-mil= military value
; ai-tech= advance-of-knowledge value
; ai-infra= infrastructure value
; ai-colonize= colonization value");

            stringBuilder.Append(@"
;
;
#FACILITIES
Headquarters,                  5, 0, None,    Disable,  Efficiency
Children's Creche,             5, 1, EthCalc, Disable,  Growth/Effic/Morale
Recycling Tanks,               4, 0, Biogen,  EcoEng2,  Bonus Resources
Perimeter Defense,             5, 0, DocLoy,  Disable,  Defense +100%
Tachyon Field,                12, 2, ProbMec, Disable,  All Defense +100%
" + alphaConfiguration.getFacilityRecCommonsLineStr() + @"
Energy Bank,                   8, 1, IndEcon, QuanMac,  Economy Bonus
Network Node,                  8, 1, InfNet,  HAL9000,  Labs Bonus
Biology Lab,                   6, 1, CentEmp, Disable,  Research and PSI
" + alphaConfiguration.getFacilitySkunkworksLineStr() + @"
" + alphaConfiguration.getFacilityHologramTheatreLineStr() + @"
" + alphaConfiguration.getFacilityParadiseGardenLineStr() + @"
Tree Farm,                    12, 3, EnvEcon, Disable,  Econ/Psych/Forest
Hybrid Forest,                24, 4, PlaEcon, Disable,  Econ/Psych/Forest
Fusion Lab,                   12, 3, Fusion,  Disable,  Econ and Labs Bonus
Quantum Lab,                  24, 4, Quantum, Disable,  Econ and Labs Bonus
Research Hospital,            12, 3, Gene,    Disable,  Labs and Psych Bonus
Nanohospital,                 24, 4, HomoSup, Disable,  Labs and Psych Bonus
Robotic Assembly Plant,       20, 4, IndRob,  Disable,  Minerals Bonus
Nanoreplicator,               32, 6, NanEdit, Disable,  Minerals Bonus
Quantum Converter,            20, 5, QuanMac, Disable,  Minerals Bonus
Genejack Factory,             10, 2, Viral,   Disable,  Minerals; More Drones
" + alphaConfiguration.getFacilityPunishmentSphereLineStr() + @"
" + alphaConfiguration.getFacilityHabComplexLineStr() + @"
" + alphaConfiguration.getFacilityHabDomeLineStr() + @"
" + alphaConfiguration.getFacilityPressureDomeLineStr() + @"
Command Center,                4, 1, Mobile,  Disable,  +2 Morale:Land
Naval Yard,                    8, 2, DocInit, Disable,  +2 Morale:Sea, Sea Def +100%
Aerospace Complex,             8, 2, DocAir,  Disable,  +2 Morale:Air, Air Def +100%
Bioenhancement Center,        10, 2, Neural,  Disable,  +2 Morale:ALL
Centauri Preserve,            10, 2, CentMed, Disable,  Ecology Bonus
Temple of Planet,             20, 3, AlphCen, Disable,  Ecology Bonus
" + alphaConfiguration.getFacilityPsiGateLineStr());

            if (alphaConfiguration.isAlphax)
                stringBuilder.Append(@"
Covert Ops Center,            10, 2, Algor,   Disable,  Stronger Probe Teams
Brood Pit,                     8, 2, CentGen, Disable,  Police/Morale
Aquafarm,                      8, 1, PrPsych, Disable,  Sea Nutrients
Subsea Trunkline,             12, 4, PlaEcon, Disable,  Sea Minerals
Thermocline Transducer,        8, 0, AdapEco, Disable,  Sea Energy
Flechette Defense System,     12, 2, NewMiss, Disable,  Missile Defense
Subspace Generator,           60, 5, SingMec, Disable,  Alien Victory
Geosynchronous Survey Pod,    16, 4, NewMiss, Disable,  Increase sight\sensor bonus
Empty Facility 42,            10, 2, Disable, Disable,  Do Not Build Me
Empty Facility 43,            10, 2, Disable, Disable,  Do Not Build Me
Empty Facility 44,            10, 2, Disable, Disable,  Do Not Build Me
Empty Facility 45,            10, 2, Disable, Disable,  Do Not Build Me
Empty Facility 46,            10, 2, Disable, Disable,  Do Not Build Me
Empty Facility 47,            10, 2, Disable, Disable,  Do Not Build Me
Empty Facility 48,            10, 2, Disable, Disable,  Do Not Build Me
Empty Facility 49,            10, 2, Disable, Disable,  Do Not Build Me
Empty Facility 50,            10, 2, Disable, Disable,  Do Not Build Me
Empty Facility 51,            10, 2, Disable, Disable,  Do Not Build Me
Empty Facility 52,            10, 2, Disable, Disable,  Do Not Build Me
Empty Facility 53,            10, 2, Disable, Disable,  Do Not Build Me
Empty Facility 54,            10, 2, Disable, Disable,  Do Not Build Me
Empty Facility 55,            10, 2, Disable, Disable,  Do Not Build Me
Empty Facility 56,            10, 2, Disable, Disable,  Do Not Build Me
Empty Facility 57,            10, 2, Disable, Disable,  Do Not Build Me
Empty Facility 58,            10, 2, Disable, Disable,  Do Not Build Me
Empty Facility 59,            10, 2, Disable, Disable,  Do Not Build Me
Empty Facility 60,            10, 2, Disable, Disable,  Do Not Build Me
Empty Facility 61,            10, 2, Disable, Disable,  Do Not Build Me
Empty Facility 62,            10, 2, Disable, Disable,  Do Not Build Me
Empty Facility 63,            10, 2, Disable, Disable,  Do Not Build Me
Empty Facility 64,            10, 2, Disable, Disable,  Do Not Build Me");

            stringBuilder.Append(@"
" + alphaConfiguration.getFacilitySkyHydroponicsLabLineStr() + @"
" + alphaConfiguration.getFacilityNessusMiningStationLineStr() + @"
" + alphaConfiguration.getFacilityOrbitalPowerTransmitterLineStr() + @"
" + alphaConfiguration.getFacilityOrbitalDefensePodLineStr() + @"
Stockpile Energy,              0, 0, None,    Disable,  Minerals to Energy
The Human Genome Project,     20, 0, Biogen,  Disable,  +1 Talent Each Base,            -1, 0, 0, 1, 1,
The Command Nexus,            20, 0, DocLoy,  Disable,  Command Center Each Base,        1, 2, 0,-1, 0,
The Weather Paradigm,         20, 0, Ecology, Disable,  Terraform Rate +50%,             0, 0, 0, 2, 1,
The Merchant Exchange,        20, 0, Indust,  Disable,  +1 Energy Each Square Here,      0, 0, 1, 2, 0,
The Empath Guild,             20, 0, CentEmp, Disable,  Commlink For Every Faction,     -2, 0, 0, 0, 0,
The Citizens' Defense Force,  30, 0, Integ,   Disable,  Perimeter Defense Each Base,     0, 1, 0, 0, 0,
The Virtual World,            30, 0, PlaNets, Disable,  Network Nodes Help Drones,       0, 0, 2, 0, 0,
The Planetary Transit System, 30, 0, IndAuto, Disable,  New Bases Begin At Size 3,       0, 0, 0, 1, 2,
The Xenoempathy Dome,         30, 0, CentMed, Disable,  Fungus Movement Bonus,           0,-1, 0, 0, 2,
The Neural Amplifier,         30, 0, Neural,  Disable,  Psi Defense +50%,                0, 0, 0, 0, 0,
The Maritime Control Center,  30, 0, DocInit, Disable,  Naval Movement +2; Naval Bases,  1, 1, 0, 0, 0,
The Planetary Datalinks,      30, 0, Cyber,   Disable,  Any Tech Known To 3 Others,      0, 0, 1, 0, 0,
The Supercollider,            30, 0, E=Mc2,   Disable,  Labs +100% At This Base,         0, 0, 2, 0, 0,
The Ascetic Virtues,          30, 0, PlaEcon, Disable,  Pop. Limit Relaxed; +1 POLICE,   0, 0, 0, 1, 2,
The Longevity Vaccine,        30, 0, BioEng,  Disable,  Fewer Drones or More Profits,    0, 0, 0, 2, 1,
" + alphaConfiguration.getProjectHunterSeekerLineStr() + @"
The Pholus Mutagen,           40, 0, CentGen, Disable,  Ecology Bonus; Lifecycle Bonus,  0, 0, 0, 1, 1,
The Cyborg Factory,           40, 0, MindMac, Disable,  Bioenh. Center Every Base,       1, 1, 0, 0, 0,
The Theory of Everything,     40, 0, Unified, Disable,  Labs +100% At This Base,         0, 0, 2, 0, 0,
The Dream Twister,            40, 0, WillPow, Disable,  Psi Attack +50%,                 0, 1, 0, 0, 1,
The Universal Translator,     40, 0, HomoSup, Disable,  Two Free Techs,                  0, 1, 2, 0, 0,
The Network Backbone,         40, 0, DigSent, Disable,  +1 Lab Per Commerce/Net Node,    0, 0, 1, 0, 0,
The Nano Factory,             40, 0, IndRob,  Disable,  Repair Units; Low Upgrade Costs, 2, 1, 0, 0, 0,
The Living Refinery,          40, 0, Space,   Disable,  +2 SUPPORT (social),             0, 1, 0, 1, 0,
The Cloning Vats,             50, 0, BioMac,  Disable,  Population Boom At All Bases,    0, 0, 0, 1, 2,
The Self-Aware Colony,        50, 0, HAL9000, Disable,  Maintenance Halved; Extra Police,0, 1, 0, 2, 0,
Clinical Immortality,         50, 0, NanEdit, Disable,  Extra Talent Every Base,         0, 0, 1, 1, 0,
The Space Elevator,           50, 0, Solids,  Disable,  Energy +100%/Orbital Cost Halved,0, 0, 2, 2, 0,
The Singularity Inductor,     60, 0, ConSing, Disable,  Quantum Converter Every Base,    0, 1, 1, 1, 0,
The Bulk Matter Transmitter,  60, 0, Matter,  Disable,  +2 Minerals Every Base,          0, 1, 1, 1, 0,
The Telepathic Matrix,        60, 0, Eudaim,  Disable,  No More Drone Riots; +2 PROBE,   0, 1, 0, 0, 1,
The Voice of Planet,          60, 0, Thresh,  Disable,  Begins Ascent To Transcendence,  0,-2, 2, 2, 1,");

            if (alphaConfiguration.isAlphax)
            {
                stringBuilder.Append(@"
The Ascent to Transcendence, 200, 0, Thresh,  Disable,  End of Singular Sentience Era,   0, 0, 2, 2, 2,
The Manifold Harmonics,       50, 0, SecMani, Disable,  Bonus Resources In Fungus,      -1, 0, 1, 2, 2,
The Nethack Terminus,         40, 0, HAL9000, Disable,  Stronger Probe Teams,            0, 1, 2, 1, 1,
The Cloudbase Academy,        30, 0, MindMac, Disable,  Faster/Stronger Air Units,       1, 1, 0, 0, 0,
The Planetary Energy Grid,    30, 0, AdapEco, Disable,  Energy Bank At Every Base,      -1, 0, 1, 2, 1,
Empty Secret Project 38,      20, 0, Disable, Disable,  End of Real Secret Projects,     0, 0, 2, 2, 2,
Empty Secret Project 39,      20, 0, Disable, Disable,  End of Real Secret Projects,     0, 0, 2, 2, 2,
Empty Secret Project 40,      20, 0, Disable, Disable,  End of Real Secret Projects,     0, 0, 2, 2, 2,
Empty Secret Project 41,      20, 0, Disable, Disable,  End of Real Secret Projects,     0, 0, 2, 2, 2,
Empty Secret Project 42,      20, 0, Disable, Disable,  End of Real Secret Projects,     0, 0, 2, 2, 2,
Empty Secret Project 43,      20, 0, Disable, Disable,  End of Real Secret Projects,     0, 0, 2, 2, 2,
Empty Secret Project 44,      20, 0, Disable, Disable,  End of Real Secret Projects,     0, 0, 2, 2, 2,
Empty Secret Project 45,      20, 0, Disable, Disable,  End of Real Secret Projects,     0, 0, 2, 2, 2,
Empty Secret Project 46,      20, 0, Disable, Disable,  End of Real Secret Projects,     0, 0, 2, 2, 2,
Empty Secret Project 47,      20, 0, Disable, Disable,  End of Real Secret Projects,     0, 0, 2, 2, 2,
Empty Secret Project 48,      20, 0, Disable, Disable,  End of Real Secret Projects,     0, 0, 2, 2, 2,
Empty Secret Project 49,      20, 0, Disable, Disable,  End of Real Secret Projects,     0, 0, 2, 2, 2,
Empty Secret Project 50,      20, 0, Disable, Disable,  End of Real Secret Projects,     0, 0, 2, 2, 2,
Empty Secret Project 51,      20, 0, Disable, Disable,  End of Real Secret Projects,     0, 0, 2, 2, 2,
Empty Secret Project 52,      20, 0, Disable, Disable,  End of Real Secret Projects,     0, 0, 2, 2, 2,
Empty Secret Project 53,      20, 0, Disable, Disable,  End of Real Secret Projects,     0, 0, 2, 2, 2,
Empty Secret Project 54,      20, 0, Disable, Disable,  End of Real Secret Projects,     0, 0, 2, 2, 2,
Empty Secret Project 55,      20, 0, Disable, Disable,  End of Real Secret Projects,     0, 0, 2, 2, 2,
Empty Secret Project 56,      20, 0, Disable, Disable,  End of Real Secret Projects,     0, 0, 2, 2, 2,
Empty Secret Project 57,      20, 0, Disable, Disable,  End of Real Secret Projects,     0, 0, 2, 2, 2,
Empty Secret Project 58,      20, 0, Disable, Disable,  End of Real Secret Projects,     0, 0, 2, 2, 2,
Empty Secret Project 59,      20, 0, Disable, Disable,  End of Real Secret Projects,     0, 0, 2, 2, 2,
Empty Secret Project 60,      20, 0, Disable, Disable,  End of Real Secret Projects,     0, 0, 2, 2, 2,
Empty Secret Project 61,      20, 0, Disable, Disable,  End of Real Secret Projects,     0, 0, 2, 2, 2,
Empty Secret Project 62,      20, 0, Disable, Disable,  End of Real Secret Projects,     0, 0, 2, 2, 2,
Empty Secret Project 63,      20, 0, Disable, Disable,  End of Real Secret Projects,     0, 0, 2, 2, 2,
Empty Secret Project 64,      20, 0, Disable, Disable,  End of Real Secret Projects,     0, 0, 2, 2, 2,
");
            } 
                else
            {
                stringBuilder.Append(@"
The Ascent to Transcendence, 200, 0, Thresh,  Disable,  End of Human Era,                0, 0, 2, 2, 2,
");
            }

            stringBuilder.Append(@"
;
; Actions - Names for unit activities
;
#ORDERS
No Orders,                -
Sentry/Board Transport,   L
Hold,                     H
Convoy,                   O
Move to,                  G
Move,                     >
Explore,                  /
Road to,                  r
Tube to,                  t

#COMPASS
Northeast
East
Southeast
South
Southwest
West
Northwest
North

#PLANS
Assault,       Assault,
Combat,        Combat,
Defense,       Defense,
Explore,       Explore/Defense,
Air Power,     Air Superiority,
Planet Buster, Planet Buster,
Naval,         Naval Power,
Transport,     Sea Transport,
Colonize,      Expansion/New Bases,
Terraform,     Planting/Terraforming,
Convoy,        Resource Convoy,
Covert Ops,    Covert Operations,
Artifact,      Alien Artifact,");

            if (alphaConfiguration.isAlphax)
                stringBuilder.Append(@"
Tectonic,      Tectonic Payload
Fungal,        Fungal Payload");

            stringBuilder.Append(@"

#TRIAD
Land
Sea
Air

;
; Resources & Energy names
;
#RESOURCES
Nutrient, Nutrients,
Mineral,  Minerals,
Energy,   Energy,
Psi,      Psi,

#ENERGY
Econ,  Economy,
Psych, Psych/Soc,
Labs,  Laboratories,

;
; Citizens - Ethics and Specialties
;
; Singular, Plural, preq, obsolete, ops, psych, research
;
; preq     = Technology prerequisite
; obsolete = Technology rendering specialty obsolete
; ops      = Bonus to operations/reserves
; psych    = Bonus to psych/soc
; research = Bonus to research
;

#CITIZENS
Technician,       Technicians,         None,    Fusion,  3, 0, 0, 0000000
Doctor,           Doctors,             None,    CentMed, 0, 2, 0, 0000000
Librarian,        Librarians,          PlaNets, MindMac, 0, 0, 3, 0000000
Engineer,         Engineers,           Fusion,  Disable, 3, 0, 2, 0000000
Empath,           Empathi,             CentMed, AlphCen, 2, 2, 0, 0000000
Thinker,          Thinkers,            MindMac, AlphCen, 0, 1, 3, 0000000
Transcend,        Transcendi,          AlphCen, Disable, 2, 2, 4, 0000000
Drone,            Drones,
Worker,           Workers,
Talent,           Talents,

#SOCIO
ECONOMY, EFFIC, SUPPORT, TALENT, MORALE, POLICE, GROWTH, PLANET, PROBE, INDUSTRY, RESEARCH
ECONOMY, EFFIC, SUPPORT, TALENT, MORALE, POLICE, GROWTH, PLANET, PROBE, INDUSTRY, RESEARCH
Politics, Economics, Values, Future Society
Frontier,        None,
Police State,    DocLoy,  ++POLICE, ++SUPPORT, --EFFIC
Democratic,      EthCalc, ++EFFIC,  ++GROWTH,  --SUPPORT
Fundamentalist,  Brain,   +MORALE,  ++PROBE,   --RESEARCH
Simple,          None,
Free Market,     IndEcon, ++ECONOMY, ---PLANET, -----POLICE
Planned,         PlaNets, ++GROWTH,  +INDUSTRY,  --EFFIC
Green,           CentEmp, ++PLANET,  ++EFFIC,    --GROWTH
Survival,        None,
Power,           MilAlg,  ++MORALE,   ++SUPPORT, --INDUSTRY
Knowledge,       Cyber,   ++RESEARCH, +EFFIC,    --PROBE
Wealth,          IndAuto, +INDUSTRY,  +ECONOMY,  --MORALE
None,            None,
Cybernetic,      DigSent, ++EFFIC,  ++PLANET,  ++RESEARCH, ---POLICE
Eudaimonic,      Eudaim,  ++GROWTH, ++ECONOMY, ++INDUSTRY, --MORALE
Thought Control, WillPow, ++POLICE, ++MORALE,  ++PROBE,   ---SUPPORT


#SOCECONOMY
-3, -2 energy each base
-2, -1 energy each base
-1, -1 energy at HQ base
 0, Standard energy rates
 1, +1 energy each base
 2, +1 energy each square!
 3, +1 energy each square; +1 commerce rating!!
 4, +1 energy/sq; +2 energy/base; +2 commerce!!!
 5, +1 energy/sq; +4 energy/base; +3 commerce!!!!

#SOCEFFIC
-4, ECONOMIC PARALYSIS
-3, Murderous inefficiency
-2, Appalling inefficiency
-1, Gross inefficiency
 0, High inefficiency
 1, Reasonable efficiency
 2, Commendable efficiency
 3, Exemplary efficiency!
 4, PARADIGM ECONOMY!!

#SOCSUPPORT
-4, Each unit costs 2 to support; no free minerals for new base.
-3, Each unit costs 1 to support; no free minerals for new base.
-2, Support 1 unit free per base; no free minerals for new base.
-1, Support 1 unit free per base
 0, Support 2 units free per base
 1, Support 3 units free per base
 2, Support 4 units free per base!
 3, Support 4 units OR up to base size for free!!

#SOCTALENT
-1, Extra drones at each base
 0,
 1, Extra talents at each base!

#SOCMORALE
-4, -3 Morale; + modifiers halved
-3, -2 Morale; + modifiers halved
-2, -1 Morale; + modifiers halved
-1, -1 Morale
 0, Normal Morale
 1, +1 Morale
 2, +1 Morale (+2 on defense)
 3, +2 Morale! (+3 on defense)
 4, +3 Morale!!

#SOCPOLICE
-5, Two extra drones for each military unit away from territory
-4, Extra drone for each military unit away from territory
-3, Extra drone if more than one military unit away from territory
-2, Cannot use military units as police. No nerve stapling.
-1, One police unit allowed. No nerve stapling.
 0, Can use one military unit as police
 1, Can use up to 2 military units as police
 2, Can use up to 3 military units as police!
 3, 3 units as police. Police effect doubled!!

#SOCGROWTH
-3, NEAR-ZERO POPULATION GROWTH
-2, -20% growth rate
-1, -10% growth rate
 0, Normal growth rate
 1, +10% growth rate
 2, +20% growth rate
 3, +30% growth rate
 4, +40% growth rate
 5, +50% growth rate!
 6, POPULATION BOOM!!

#SOCPLANET
-3, Wanton ecological disruption; -3 Fungus production
-2, Rampant ecological disruption; -2 Fungus production
-1, Increased ecological disruption; -1 Fungus production
 0, Normal ecological tension
 1, Ecological safeguards!; Can capture native life forms
 2, Ecological harmony!; Increased chance of native life form capture!
 3, Ecological wisdom!!; Maximum chance of native life form capture!!

#SOCPROBE
-2, -50% cost of enemy probe team actions; enemy success rate increased
-1, -25% cost of enemy probe team actions; enemy success rate increased
 0, Normal security measures
 1, +1 probe team morale; +50% cost of enemy probe team actions
 2, +2 probe team morale; Doubles cost of enemy probe team actions!");

            if (alphaConfiguration.isAlphax)
                stringBuilder.Append(@"
 3, +3 probe team morale; Bases and units cannot be subverted by standard Probe Teams!!
");
            else stringBuilder.Append(@"
 3, +3 probe team morale; Bases and units cannot be subverted!!
");

            stringBuilder.Append(@"
#SOCINDUSTRY
-3, Mineral costs increased by 30%
-2, Mineral costs increased by 20%
-1, Mineral costs increased by 10%
 0, Normal production rate
 1, Mineral costs decreased by 10%
 2, Mineral costs decreased by 20%
 3, Mineral costs decreased by 30%!
 4, Mineral costs decreased by 40%!!
 5, Mineral costs decreased by 50%!!!

#SOCRESEARCH
-5, Labs research slowed by 50%
-4, Labs research slowed by 40%
-3, Labs research slowed by 30%
-2, Labs research slowed by 20%
-1, Labs research slowed by 10%
 0, Normal research rate
 1, Labs research speeded by 10%
 2, Labs research speeded by 20%
 3, Labs research speeded by 30%
 4, Labs research speeded by 40%
 5, Labs research speeded by 50%

;
; Difficulty - Names for difficulty levels
;
#DIFF
Citizen,
Specialist,
Talent,
Librarian,
Thinker,
Transcend,

##### IMPORTANT NOTE TO TRANSLATORS
# For this list of adjectives, please translate to all genders & plurality:
Alien
Gaian
Hive
University
Morganic
Spartan
Believing
Peacekeeper

;
; Factions -- Names & personalities
;
; formal, desc, noun, masc/fem, sing/plural, name, gender, ai-fight, ai-power, ai-tech, ai-wealth, ai-growth
;    title, characteristic, adjective
;    SPECIAL RULES, parameters, ...
;    SOCIAL PRIORITY, setting, result
;
; formal      = Formal name of colony/group
; desc        = Description of basic ideology
; noun        = Plural noun
; masc/fem    = Is faction's noun masculine or feminine
; sing/plural = Is faction's noun singular (Labyrinth) or plural (Gaians)
; name        = Name of default leader
; gender      = M or F (gender of leader)
; ai-fight    = -1,0,1 (willingness to use force to achieve goals)
; ai-power    = 1 or 0 (interest in power)
; ai-tech     = 1 or 0 (interest in knowledge)
; ai-wealth   = 1 or 0 (interest in wealth)
; ai-growth   = 1 or 0 (interest in population growth)
;
; title          = leader's title
; characteristic = leader's descriptive adjective
;
; adjective      = adjectival form of faction name (e.g. Gaian)
;                  TRANSLATOR NOTE: for FRENCH versions, this should
;                  give all four forms (ms:fs:mp:fp):
;                  Gaian:Gaiane:Gaians:Gaianes (or whatever it would be)
;                  for GERMAN it should have all six forms.
;
; SPECIAL RULES = rule, parameter
;
;   TECH        = Free technology at start. Parameter
;                 is either a tech id (e.g. ""Cen"") to
;                 indicate a specific technology, or a
;                 number (e.g. 2) to indicate a number
;                 of player-selected technologies.
;   MORALE      = Morale modifier (if 0, indicates an
;                 exemption from negative modifiers from
;                 other sources).
;   PSI         = Percentage combat bonus for PSI Combat.
;   FACILITY    = Every new base gets this free facility.
;                 Param indicates facility (e.g. ""4"" is
;                 a Perimeter Defense) from the facilities
;                 list. Do NOT attempt to give satellites
;                 and secret projects this way.
;   RESEARCH    = Free research points per base per turn.
;   DRONE       = Extra drone at base (per ""param""
;                 citizens, rounded down)
;   TALENT      = Extra talent at base (per ""param""
;                 citizens, rounded up)
;   ENERGY      = Free energy reserves at start
;   INTEREST    = Energy reserves interest.
;                 Non-zero = constant percentage per turn (including negative)
;                 Zero     = +1/base each turn
;   COMMERCE    = Increased commerce rate
;   POPULATION  = # to be added to population limit of
;                 each base for purposes of Habitation
;                 domes, etc.
;   HURRY       = Percentage change in costs of ""Hurry""
;                 button on construction (e.g. 125 means
;                 125% of normal cost, so 100 costs 125).
;   UNIT        = Extra free unit at start; param is
;                 index from units list (e.g. 0 equals
;                 Colonists, 1 Terraformers, 2 Scout
;                 Crawler)
;   TECHCOST    = Modifier % for tech research rate.
;                 (e.g. 125 means each discovery costs
;                 125% the usual number of research
;                 points).
;   SHARETECH   = Gain any technology known to # other players
;   TERRAFORM   = Halves terraform raise/lower cost
;   SOCIAL      = Gives a modifier in the named social effect category
;                 (""SOCIAL, +EFFIC"" raises the EFFIC rating by 1;
;                  ""SOCIAL, --POLICE"" lowers POLICE rating by 2);
;   ROBUST      = Halves the intensity of minus effects in the named
;                 social area (""ROBUST, EFFIC"" halves minus efficiency
;                 effects in social model).
;   IMMUNITY    = Immunity from minus effects in the named social
;                 area. (""IMMUNITY, ENERGY"" prevents minus energy
;                 effects in social model).
;   IMPUNITY    = Impunity :-) from minus effects from a particular
;                 social setting. ""IMPUNITY, Police State"" prevents
;                 all - effects from ""Police State"" setting.
;   PENALTY     = Opposite of impunity: doubles the negative effects
;                 of a particular setting.
;   FUNGNUTRIENT= Modifier to NUTRIENT produced in fungus squares
;   FUNGMINERALS= Modifier to MINERALS produced in fungus squares
;   FUNGENERGY  = Modifier to ENERGY produced in fungus squares
;   COMMFREQ    = Gets an extra comm frequency (another faction to
;                 talk to) at beginning of game. (Parameter is ignored)
;   MINDCONTROL = Vehicles and bases immune to mind control
;   FANATIC     = +25% bonus on attack
;   VOTES       = Multiplier for governor votes
;   FREEPROTO   = Prototype cost reduced to zero for this faction");

            if (alphaConfiguration.isAlphax)
                stringBuilder.Append(@"
;   AQUATIC     = This is a sea-based faction
;   ALIEN       = Non-human faction
;   FREEFAC     = Free facility when appropriate tech is discovered
;   REVOLT      = Additional % chance that other revolting bases will defect to this faction
;   NODRONE     = Number of drones per base made content
;   WORMPOLICE  = Mindworms do double police duty
;   FREEABIL    = All appropriate units have this ability when requisite tech is discovered
;   PROBECOST   = Percentage of normal cost for probe-team actions (default is 100)
;   DEFENSE     = Percent modifier for defensive combat (default is 100)
;   OFFENSE     = Percent modifier for offensive combat (default is 100)
;   TECHSHARE   = When used with SHARETECH, requires one to be spying on the other
;                 factions (by probe, Empath Guild, Governor, or Pact). Parameter is ignored.
;   TECHSTEAL   = Faction steal a tech when it captures a base. Parameter is ignored.");

            stringBuilder.Append(@"
;
; SOCIAL PRIORITY:
;   Drawn from the political table above (see #SOCIO), these entries
;   must exactly match the SOCIO entries in spelling. These determine
;   the leader's social priorities.
;

##
##SAMPLE SENTENCES FOR TRANSLATORS
##
##Gaia's Stepdaughters, The Green, Gaians, F, 2, Deirdre, F, -1, 0, 0, 0, 1,
##  TECH, Ecology, SOCIAL, -MORALE, SOCIAL, -POLICE, SOCIAL, +EFFIC, PSI, 25, FUNGNUTRIENT, 1
##  Environment, Pro-Ecology, PLANET
##  Liberties, Democratic, EFFIC
##  Gaian, Gaian,
##  Lindly, Scott, Lindly's Rest
##  Lady, beautiful, tree-crazy, green, eco-daft,
##  Nature Loony
##  [Together we shall both] preserve Planet's native life [and ...]
##  [My intent is] to guard, understand, and preserve Planet's native life
##  [She is bent on] stamping out all legitimate human development of this planet
##  [You seem bent on] stamping out all legitimate human development of this planet
##  [I shall not stand here while you] cripple any and all efforts toward human progress on this world
##  [She spends her time] dancing naked through the trees
##  [You spend your time] dancing naked through the trees
##  [I have been accused of] spouting tree-crazy prattle
##  [Your] tree-crazy prattle [does not impress me]
##  [Think how this could benefit your] environmental initiatives
##  [I trust your] pagan rituals [are proceeding to your satisfaction]
##  [It is customary to remit me a small] ecology tax [...]
##  [...for the services my forces provide in] preserving and cataloguing Planet's native life
##  [I shall instruct] my Environmental Police [to see that no such...]
##  [You are in contravention of] the Planetary Ecology Code
##
##  END OF SAMPLE SENTENCES FOR TRANSLATORS
##

; Bonus Names: These keys must match the strings used in faction definitions
#BONUSNAMES
TECH, MORALE, PSI, FACILITY, RESEARCH, DRONE, TALENT, ENERGY,
INTEREST, COMMERCE, POPULATION, HURRY, UNIT, TECHCOST, SHARETECH, TERRAFORM,
SOCIAL, ROBUST, IMMUNITY, IMPUNITY, PENALTY, FUNGNUTRIENT, FUNGMINERALS, FUNGENERGY,");

            if (alphaConfiguration.isAlphax)
                stringBuilder.Append(@"
COMMFREQ, MINDCONTROL, FANATIC, VOTES, FREEPROTO, AQUATIC, ALIEN, FREEFAC,
REVOLT, NODRONE, WORMPOLICE, FREEABIL, PROBECOST, DEFENSE, OFFENSE, TECHSHARE,
TECHSTEAL
");
            else stringBuilder.Append(@"
COMMFREQ, MINDCONTROL, FANATIC, VOTES, FREEPROTO");

            stringBuilder.Append(@"

;
; First item is file name, second item is search key.
; These should always be the same except for debugging purposes.
;
#FACTIONS
GAIANS,   GAIANS
HIVE,     HIVE
UNIV,     UNIV
MORGAN,   MORGAN
SPARTANS, SPARTANS
BELIEVE,  BELIEVE
PEACE,    PEACE
");

            if (alphaConfiguration.isAlphax)
                stringBuilder.Append(@"
;
; First item is file name, second item is search key.
; These should always be the same except for debugging purposes.
;
#NEWFACTIONS
CYBORG,   CYBORG
PIRATES,  PIRATES
DRONE,    DRONE
ANGELS,   ANGELS
FUNGBOY,  FUNGBOY
CARETAKE, CARETAKE
USURPER,  USURPER

;
; These are factions you want included in the startup list.
; These may also be chosen when a random faction is selected.
; First item is file name, second item is search key.
; These should always be the same except for debugging purposes.
;
#CUSTOMFACTIONS
");

            stringBuilder.Append(@"
#MANDATE
Explore, EXPLORE
Discover, DISCOVER
Build, BUILD
Conquer, CONQUER

#MOOD
Magnanimous
Solicitous
Cooperative
Noncommittal
Ambivalent
Obstinate
Quarrelsome
Belligerent
Seething

#REPUTE
Noble
Faithful
Scrupulous
Dependable
Ruthless
Treacherous
Wicked
Infamous

#MIGHT
Unsurpassed, magnificent
Potent,      mighty
Formidable,  formidable
Sufficient,  respectable
Wanting,     small
Feeble,      feeble little
Pathetic,    pathetic little

#PROPOSALS
Elect Planetary Governor,          None,    New Governor Appointed
Unite Behind Me As Supreme Leader, MindMac, Diplomatic Victory; Game Ends
Salvage Unity Fusion Core,         Orbital, +500 Energy Credits Every Faction
Global Trade Pact,                 PlaEcon, Commerce Rates Doubled
Repeal Global Trade Pact,          PlaEcon, Commerce Rates Halved
Launch Solar Shade,                Space,   Global Cooling; Sea Levels Drop
Increase Solar Shade,              Space,   Global Cooling; Sea Levels Drop
Melt Polar Caps,                   EcoEng2, Global Warming; Sea Levels Rise
Repeal U.N. Charter,               MilAlg,  Atrocity Prohibitions Lifted
Reinstate U.N. Charter,            MilAlg,  Atrocity Prohibitions Return
...,                               Disable, ...



#NATURAL
Garland Crater, Crater
Mount Planet, Volcano
Monsoon Jungle, Jungle
Uranium Flats, Uranium
New Sargasso, Sargasso
The Ruins, Ruins
Great Dunes, Dunes
Freshwater Sea, Fresh
Sunny Mesa, Mesa
Nessus Canyon, Canyon
Geothermal Shallows, Geothermal
Pholus Ridge, Ridge
Borehole Cluster, Borehole
Manifold Nexus, Nexus");

            if (alphaConfiguration.isAlphax)
                stringBuilder.Append(@"
U.N.S. Unity Wreckage, Unity
Fossil Field Ridge, Fossil");
            else
                stringBuilder.Append(@"
");

            stringBuilder.Append(@"


# ; This line must remain at end of file

");


            return stringBuilder.ToString();
        }
    }
}
