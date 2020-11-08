using AlphaModder.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaModder.Model
{
    class AlphaConfiguration
    {
        public bool isAlphax { get; set; } = false;

        public String configJson { get; set; } = ";";
        public String planetSizeHugeX { get; set; } = "128";
        public String planetSizeHugeY { get; set; } = "64";

        #region RULES

        public String ruleTechDiscoveryRate { get; set; } = "100";// { get; set; } TODO fix this?
        public String ruleDronesInducedByGenejackFactory { get; set; } = "1";

        public String startingEnergy { get; set; } = "10";
        public String minTurnsBetweenCouncils { get; set; } = "20";
        public String rulePrototypeExtraCostPctLand { get; set; } = "50";
        public String rulePrototypeExtraCostPctSea { get; set; } = "50";
        public String rulePrototypeExtraCostPctAir { get; set; } = "50";
        public String ruleRetoolPenaltyPct { get; set; } = "50";
        public String rulePopLimitWithoutHabComplex { get; set; } = "7";
        public String rulePopLimitWithoutHabDome { get; set; } = "14";
        public String ruleRetoolStrictness { get; set; } = AlphaModderConstants.RETOOL_STRICTNESS_2_FREE_IF_PROJECT;
        public String ruleObliterateBaseIsAtrocity { get; set; } = "1";
        public String ruleProbeTeamsCanStealTech { get; set; } = "1";

        public String ruleStartingYear { get; set; } = "2100";
        public String ruleEndingYearEasy { get; set; } = "2600";
        public String ruleEndingYearHard { get; set; } = "2500";

        #endregion

        
        public String heavyTransportPrereq { get; set; } = Constants.Techs.DISABLE;

        #region Terraforming fields

        public String terraformFarmRate { get; set; } = "4";
        public String terraformSoilEnricherPrereq { get; set; } = "EcoEng2";
        public String terraformSoilEnricherRate { get; set; } = "8";
        public String terraformMineRate { get; set; } = "8";
        public String terraformSolarCollectorRate { get; set; } = "4";
        public String terraformForrestRate { get; set; } = "4";
        public String terraformRoadRate { get; set; } = "1";
        public String terraformMagTubePrereq { get; set; } = "Magnets";
        public String terraformMagTubeRate { get; set; } = "3";
        public String terraformBunkerPrereq { get; set; } = "MilAlg";
        public String terraformBunkerRate { get; set; } = "5";
        public String terraformAirbasePrereq { get; set; } = "DocAir";
        public String terraformAirbaseRate { get; set; } = "10";
        public String terraformSensorArrayRate { get; set; } = "4";
        public String terraformRemoveFungusRate { get; set; } = "6";
        public String terraformPlantFungusPrereq { get; set; } = "EcoEng";
        public String terraformPlantFungusSeaPrereq { get; set; } = "EcoEng";
        public String terraformPlantFungusRate { get; set; } = "6";
        public String terraformCondenserPrereq { get; set; } = "EcoEng";
        public String terraformCondenserRate { get; set; } = "12";
        public String terraformEchelonMirrorPrereq { get; set; } = "EcoEng";
        public String terraformEchelonMirrorRate { get; set; } = "12";
        public String terraformBoreholePrereq { get; set; } = "EcoEng";
        public String terraformBoreholeRate { get; set; } = "24";
        public String terraformAquiferPrereq { get; set; } = "EcoEng";
        public String terraformAquiferRate { get; set; } = "18";
        public String terraformRaiseLandPrereq { get; set; } = "EnvEcon";
        public String terraformRaiseLandSeaPrereq { get; set; } = "EnvEcon";
        public String terraformRaiseLandRate { get; set; } = "12";
        public String terraformLowerLandPrereq { get; set; } = "EnvEcon";
        public String terraformLowerLandSeaPrereq { get; set; } = "EnvEcon";
        public String terraformLowerLandRate { get; set; } = "12";
        public String terraformLevelRate { get; set; } = "8";

        #endregion

        #region Drones fields

        public String facilityRecCommonsPrereq { get; set; } = "Psych";
        public String facilityRecCommonsCost { get; set; } = "4";
        public String facilityRecCommonsMaint { get; set; } = "1";
        public String facilityHologramTheatrePrereq { get; set; } = "PlaNets";
        public String facilityHologramTheatreCost { get; set; } = "6";
        public String facilityHologramTheatreMaint { get; set; } = "3";
        public String facilityParadiseGardenPrereq { get; set; } = "SentEco";
        public String facilityParadiseGardenCost { get; set; } = "12";
        public String facilityParadiseGardenMaint { get; set; } = "4";
        public String facilityPunishmentSpherePrereq { get; set; } = "MilAlg";
        public String facilityPunishmentSphereCost { get; set; } = "10";
        public String facilityPunishmentSphereMaint { get; set; } = "2";
        public String abilityNonLethalMethodsPoliceCost { get; set; } = "1";
        public String abilityNonLethalMethodsPolicePrereq { get; set; } = "Integ";

        public String unitProbeTeamPrereq { get; set; } = "PlaNets";



        #endregion

        #region Fungus fields

        // tech to improve fungus squares
        // tech to ease fungus movement
        // tech to build roads in fungus
        // faster terraforming
        // cost / prereq for terraforming abilities

        #endregion

        #region Mindworms fields

        public String generalTranceDefenseBonus { get; set; } = "50";
        public String generalEmpathAttackBonus { get; set; } = "50";

        public String abilityEmpathSongCost { get; set; } = "2";
        public String abilityEmpathSongPrereq { get; set; } = "CentEmp";

        public String abilityTranceCost { get; set; } = "-1";
        public String abilityTrancePrereq { get; set; } = "Brain";

        public String unitMindWormsPrereq { get; set; } = "CentEmp";
        public String unitIsleOfTheDeepPrereq { get; set; } = "CentMed";
        public String unitLocustsOfChironPrereq { get; set; } = "CentGen";


        #endregion


        #region Probe teams fields

        public String weaponProbeTeamPrereq { get; set; } = "PlaNets";
        public String projectHunterSeekerPrereq { get; set; } = "Algor";

        #endregion

        public String getPlanetSizeHugeLineStr()
        {
            return "Huge planet|(late conflict),  " + planetSizeHugeY + ", " + planetSizeHugeX;
        }

        #region RULES LINE STRINGS

        public String getRuleTechDiscoveryRateLineStr()
        {
            return ruleTechDiscoveryRate + ",     ; Technology discovery rate as a percentage of standard";
        }

        public String getRuleDronesInducedByGenejackFactoryLineStr()
        {
            return ruleDronesInducedByGenejackFactory + ",       ; Drones induced by Genejack factory";
        }

        public String getRuleRetoolPenaltyPctLineStr()
        {
            return ruleRetoolPenaltyPct + ",      ; Retool percent penalty for production change";
        }

        public String getRuleRetoolStrictnessLineStr()
        {
            return ruleRetoolStrictness + ",       ; Retool strictness (0 = Always Free, 1 = Free in Category, 2 = Free if Project, 3 = Never Free)";
        }

        public String getRuleProbeTeamsCanStealTechLineStr()
        {
            return ruleProbeTeamsCanStealTech + ",       ; If non-zero, probe teams can steal technologies";
        }

        public String getRuleStartingYearLineStr()
        {
            return ruleStartingYear + "     ; Normal starting year";
        }

        public String getRuleEndingYearEasyLineStr()
        {
            return ruleEndingYearEasy + "     ; Normal ending year for lowest 3 difficulty levels";
        }

        public String getRuleEndingYearHardLineStr()
        {
            return ruleEndingYearHard + "     ; Normal ending year for highest 3 difficulty levels";
        }

        #endregion
        public String getStartingEnergyLineStr()
        {
            return startingEnergy + ",      ; Players' starting energy reserves";
        }

        public String getRulePrototypeCostPctLineStr()
        {
            return rulePrototypeExtraCostPctLand + ",      ; Extra percentage cost of prototype LAND unit\n"
                + rulePrototypeExtraCostPctSea + ",      ; Extra percentage cost of prototype SEA unit\n"
                + rulePrototypeExtraCostPctAir + ",      ; Extra percentage cost of prototype AIR unit";
        }

        public String getHeavyTransportSpecAbilityLineStr()
        {
            if (isAlphax)
                return "Heavy Transport,        1, " + heavyTransportPrereq + ",  Heavy,     000100100111, +50% transport capacity";
            else
                return "Heavy Transport,        1, " + heavyTransportPrereq + ",  Heavy,     00100100111, +50% transport capacity";
        }

        public String getMinTurnsBetweenCouncilsLineStr()
        {
            return minTurnsBetweenCouncils + ",      ; Minimum # of turns between councils";
        }

        public String getObliterateBaseIsAtrocityLineStr()
        {
            return ruleObliterateBaseIsAtrocity + "        ; If non-zero, obliterating a base counts as an atrocity";
        }

        public String getPopLimitWithoutHabComplexLineStr()
        {
            return rulePopLimitWithoutHabComplex + ",       ; Population limit w/o hab complex";
        }
        public String getPopLimitWithoutHabDomeLineStr()
        {
            return rulePopLimitWithoutHabDome + ",      ; Population limit w/o hab dome";
        }

        #region Terraforming Line Strings

        public String getTerraformFarmLineStr()
        {
            return "Farm,             None,    Kelp Farm,        None,     " + terraformFarmRate + ",  Cultivate $STR0, f, F";
        }

        public String getTerraformSoilEnricherLineStr()
        {
            return "Soil Enricher,    " + terraformSoilEnricherPrereq + ", Soil Enricher,    Disable,  " + terraformSoilEnricherRate + ",  Construct $STR0, f, F";
        }

        public String getTerraformMineLineStr()
        {
            return "Mine,             None,    Mining Platform,  None,     " + terraformMineRate + ",  Construct $STR0, M, M";
        }

        public String getTerraformSolarCollectorLineStr()
        {
            return "Solar Collector,  None,    Tidal Harness,    None,     " + terraformSolarCollectorRate + ",  Construct $STR0, S, S";
        }

        public String getTerraformForestLineStr()
        {
            return "Forest,           None,    ...,              Disable,  " + terraformForrestRate + ",  Plant $STR0,     F, Shift+F";
        }

        public String getTerraformRoadLineStr()
        {
            return "Road,             None,    Road,             Disable,  " + terraformRoadRate + ",  Build $STR0,     R, R";
        }

        public String getTerraformMagTubeLineStr()
        {
            return "Mag Tube,         " + terraformMagTubePrereq + ", Mag Tube,         Disable,  " + terraformMagTubeRate + ",  Build $STR0,     R, R";
        }

        public String getTerraformBunkerLineStr()
        {
            return "Bunker,           " + terraformBunkerPrereq + ",  Bunker,           Disable,  " + terraformBunkerRate + ",  Construct $STR0, K, K";
        }

        public String getTerraformAirbaseLineStr()
        {
            return "Airbase,          " + terraformAirbasePrereq + ",  Airbase,          Disable,  " + terraformAirbaseRate + ", Construct $STR0, ., .";
        }

        public String getTerraformSensorArrayLineStr()
        {
            return "Sensor Array,     None,    Sensor Array,     Disable,  " + terraformSensorArrayRate + ",  Construct $STR0, O, O";
        }

        public String getTerraformRemoveFungusLineStr()
        {
            return "Fungus,           None,    Sea Fungus,       None,     " + terraformRemoveFungusRate + ",  Remove $STR0,    F, F";
        }

        public String getTerraformPlantFungusLineStr()
        {
            return "Fungus,           " + terraformPlantFungusPrereq + ",  Sea Fungus,       " + terraformPlantFungusSeaPrereq + ",   " + terraformPlantFungusRate + ",  Plant $STR0,     F, Ctrl+F";
        }

        public String getTerraformCondenserLineStr()
        {
            return "Condenser,        " + terraformCondenserPrereq + ",  Condenser,        Disable,  " + terraformCondenserRate + ", Construct $STR0, N, N";
        }

        public String getTerraformEchelonMirrorLineStr()
        {
            return "Echelon Mirror,   " + terraformEchelonMirrorPrereq + ",  Echelon Mirror,   Disable,  " + terraformEchelonMirrorRate + ", Construct $STR0, E, Shift+E";
        }

        public String getTerraformBoreholeLineStr()
        {
            return "Thermal Borehole, " + terraformBoreholePrereq + ",  Thermal Borehole, Disable,  " + terraformBoreholeRate + ", Construct $STR0, B, Shift+B";
        }

        public String getTerraformAquiferLineStr()
        {
            return "Aquifer,          " + terraformAquiferPrereq + ",  Aquifer,          Disable,  " + terraformAquiferRate + ", Drill to $STR0,  Q, Q";
        }

        public String getTerraformRaiseLandLineStr()
        {
            return "Raise Land,       " + terraformRaiseLandPrereq + ", Raise Sea Floor,  " + terraformRaiseLandSeaPrereq + ",  " + terraformRaiseLandRate + ", Terraform UP,    ], ]]";
        }

        public String getTerraformLowerLandLineStr()
        {
            return "Lower Land,       " + terraformLowerLandPrereq + ", Lower Sea Floor,  " + terraformLowerLandSeaPrereq + ",  " + terraformLowerLandRate + ", Terraform DOWN,  [, [[";
        }

        public String getTerraformLevelLineStr()
        {
            return "Level Terrain,    None,    Level Terrain,    Disable,  " + terraformLevelRate + ",  Terraform LEVEL, _, _";
        }

        #endregion

        #region Drones Line Strings

        public String getFacilityRecCommonsLineStr()
        {
            return "Recreation Commons,            " + facilityRecCommonsCost + ", " + facilityRecCommonsMaint + ", " + facilityRecCommonsPrereq + ",   SentEco,  Fewer Drones";
        }

        public String getFacilityHologramTheatreLineStr()
        {
            return "Hologram Theatre,              " + facilityHologramTheatreCost + ", " + facilityHologramTheatreMaint + ", " + facilityHologramTheatrePrereq + ", Disable,  Psych and Fewer Drones";
        }

        public String getFacilityParadiseGardenLineStr()
        {
            return "Paradise Garden,              " + facilityParadiseGardenCost + ", " + facilityParadiseGardenMaint + ", " + facilityParadiseGardenPrereq + ", Disable,  +2 Talents";
        }

        public String getFacilityPunishmentSphereLineStr()
        {
            return "Punishment Sphere,            " + facilityPunishmentSphereCost + ", " + facilityPunishmentSphereMaint + ", " + facilityPunishmentSpherePrereq + ",  Disable,  No Drones/-50% Tech";
        }

        public String getAbilityNonLethalMethodsPoliceLineStr()
        {
            if (isAlphax)
                return "Non-Lethal Methods,     " + abilityNonLethalMethodsPoliceCost + ", " + abilityNonLethalMethodsPolicePrereq + ",    Police,    000000001001, x2 Police powers";
            else
                return "Non-Lethal Methods,     " + abilityNonLethalMethodsPoliceCost + ", " + abilityNonLethalMethodsPolicePrereq + ",    Police,    00000001001, x2 Police powers";
        }


        #endregion

        #region Fungus Line Strings

        #endregion

        #region Mindworms Line Strings

        public String getGeneralTranceDefenseBonusLineStr() {
            return generalTranceDefenseBonus + ",      ; Combat % -> Trance bonus defending vs. psi";
        }

        public String getGeneralEmpathAttackBonusLineStr()
        {
            return generalEmpathAttackBonus + ",      ; Combat % -> Empath Song bonus attacking vs. psi";
        }

        public String getAbilityEmpathLineStr()
        {
            if(isAlphax)
                return "Empath Song,            " + abilityEmpathSongCost + ", " + abilityEmpathSongPrereq + ",  Empath,    000010001111, +" + generalEmpathAttackBonus + "% attack vs. Psi"; // alphax
            else
                return "Empath Song,            " + abilityEmpathSongCost + ", " + abilityEmpathSongPrereq + ",  Empath,    00010001111, +" + generalEmpathAttackBonus + "% attack vs. Psi"; // alpha
        }

        public String getAbilityTranceLineStr()
        {
            if (isAlphax)
                return "Hypnotic Trance,       " + abilityTranceCost + ", " + abilityTrancePrereq + ",    Trance,    000010111111, +" + generalTranceDefenseBonus + "% defense vs. Psi"; // alphax
            else
                return "Hypnotic Trance,       " + abilityTranceCost + ", " + abilityTrancePrereq + ",    Trance,    00010111111, +" + generalTranceDefenseBonus + "% defense vs. PSI"; // alpha
        }

        public String getUnitMindWormsLineStr()
        {
            if (isAlphax)
                return "Mind Worms,             Infantry, Psi,          Psi,        1, 5, 0, " + unitMindWormsPrereq + ",  3, 00000000000000000000000000";
            else
                return "Mind Worms,             Infantry, Psi,          Psi,        1, 5, 0, " + unitMindWormsPrereq + ",  3, 000000000000000000000000";
        }
        
        public String getUnitIsleOfTheDeepLineStr()
        {
            if (isAlphax)
                return "Isle of the Deep,       Foil,     Psi,          Psi,        7, 8, 4, " + unitIsleOfTheDeepPrereq + ", -1, 00000000000000000000000000";
            else
                return "Isle of the Deep,       Foil,     Psi,          Psi,        7, 8, 4, " + unitIsleOfTheDeepPrereq + ", -1, 000000000000000000000000";
        }

        public String getUnitLocustsOfChironLineStr()
        {
            if (isAlphax)
                return "Locusts of Chiron,      Gravship, Psi,          Psi,        4,10, 0, " + unitLocustsOfChironPrereq + ", -1, 00000000000000000000100000";
            else
                return "Locusts of Chiron,      Gravship, Psi,          Psi,        4,10, 0, " + unitLocustsOfChironPrereq + ", -1, 000000000000000000100000";
        }

        #endregion

        #region Probe teams Line Strings
        public String getWeaponProbeTeamLineStr()
        {
            return "Probe Team,           Probe Team,     0,11, 4, -1, " + weaponProbeTeamPrereq + ",";
        }

        public String getProjectHunterSeekerLineStr()
        {
            return "The Hunter-Seeker Algorithm,  30, 0, " + projectHunterSeekerPrereq + ",   Disable,  Immunity to Probe Teams,         0, 0, 2, 0, 0,";
        }

        public String getUnitProbeTeamLineString()
        {
            if (isAlphax)
                return "Probe Team,             Speeder,  Probe Team,   Scout,     11, 0, 0, " + unitProbeTeamPrereq + ", -1, 00000000000000000000000000";
            else
                return "Probe Team,             Speeder,  Probe Team,   Scout,     11, 0, 0, " + unitProbeTeamPrereq + ", -1, 000000000000000000000000";
        }

        #endregion

        public String facilityHabComplexCost { get; set; } = "8";
        public String facilityHabComplexMaint { get; set; } = "2";
        public String facilityHabComplexPrereq { get; set; } = "IndAuto";
        public String getFacilityHabComplexLineStr()
        {
            return "Hab Complex,                   " + facilityHabComplexCost + ", " + facilityHabComplexMaint + ", " + facilityHabComplexPrereq + ", Disable,  Increase Population Limit";
        }

        public String facilityHabDomeCost { get; set; } = "16";
        public String facilityHabDomeMaint { get; set; } = "4";
        public String facilityHabDomePrereq { get; set; } = "Solids";
        public String getFacilityHabDomeLineStr()
        {
            return "Habitation Dome,              " + facilityHabDomeCost + ", " + facilityHabDomeMaint + ", " + facilityHabDomePrereq + ",  Disable,  Increase Population Limit";
        }

        public String facilitySkunkworksCost { get; set; } = "6";
        public String facilitySkunkworksMaint { get; set; } = "1";
        public String facilitySkunkworksPrereq { get; set; } = "Subat";
        public String getFacilitySkunkworksLineStr()
        {
            return "Skunkworks,                    " + facilitySkunkworksCost + ", " + facilitySkunkworksMaint + ", " + facilitySkunkworksPrereq + ",   Disable,  Prototypes Free";
        }

        public String facilityPressureDomeCost { get; set; } = "8";
        public String facilityPressureDomeMaint { get; set; } = "0";
        public String facilityPressureDomePrereq { get; set; } = "DocFlex";
        public String getFacilityPressureDomeLineStr()
        {
            return "Pressure Dome,                 " + facilityPressureDomeCost + ", " + facilityPressureDomeMaint + ", " + facilityPressureDomePrereq + ", Disable,  Submersion/Resources";
        }

        public String facilityPsiGateCost { get; set; } = "10";
        public String facilityPsiGateMaint { get; set; } = "2";
        public String facilityPsiGatePrereq { get; set; } = "Matter";
        public String getFacilityPsiGateLineStr()
        {
            return "Psi Gate,                     " + facilityPsiGateCost + ", " + facilityPsiGateMaint + ", " + facilityPsiGatePrereq + ",  Disable,  Teleport";
        }

        public String facilitySkyHydroponicsLabCost { get; set; } = "12";
        public String facilitySkyHydroponicsLabMaint { get; set; } = "0";
        public String facilitySkyHydroponicsLabPrereq { get; set; } = "Orbital";
        public String getFacilitySkyHydroponicsLabLineStr()
        {
            return "Sky Hydroponics Lab,          " + facilitySkyHydroponicsLabCost + ", " + facilitySkyHydroponicsLabMaint + ", " + facilitySkyHydroponicsLabPrereq + ", Disable,  +1 Nutrient ALL BASES";
        }

        public String facilityNessusMiningStationCost { get; set; } = "12";
        public String facilityNessusMiningStationMaint { get; set; } = "0";
        public String facilityNessusMiningStationPrereq { get; set; } = "HAL9000";
        public String getFacilityNessusMiningStationLineStr()
        {
            return "Nessus Mining Station,        " + facilityNessusMiningStationCost + ", " + facilityNessusMiningStationMaint + ", " + facilityNessusMiningStationPrereq + ", Disable,  +1 Minerals ALL BASES";
        }

        public String facilityOrbitalPowerTransmitterCost { get; set; } = "12";
        public String facilityOrbitalPowerTransmitterMaint { get; set; } = "0";
        public String facilityOrbitalPowerTransmitterPrereq { get; set; } = "Space";
        public String getFacilityOrbitalPowerTransmitterLineStr()
        {
            return "Orbital Power Transmitter,    " + facilityOrbitalPowerTransmitterCost + ", " + facilityOrbitalPowerTransmitterMaint + ", " + facilityOrbitalPowerTransmitterPrereq + ",   Disable,  +1 Energy ALL BASES";
        }

        public String facilityOrbitalDefensePodCost { get; set; } = "12";
        public String facilityOrbitalDefensePodMaint { get; set; } = "0";
        public String facilityOrbitalDefensePodPrereq { get; set; } = "HAL9000";
        public String getFacilityOrbitalDefensePodLineStr()
        {
            return "Orbital Defense Pod,          " + facilityOrbitalDefensePodCost + ", " + facilityOrbitalDefensePodMaint + ", " + facilityOrbitalDefensePodPrereq + ", Disable,  Missile Defense";
        }

        public String ruleAAABonusPct { get; set; } = "100";
        public String getRuleAAABonusPctLineStr()
        {
            return ruleAAABonusPct + ",     ; Combat % -> AAA bonus vs. air units";
        }

        public String ruleTechToEaseFungusMovement { get; set; } = "CentPsi";
        public String getRuleTechToEaseFungusMovementLineStr()
        {
            return ruleTechToEaseFungusMovement + ", ; Technology to ease fungus movement";
        }

        public String ruleTechToBuildRoadsInFungus { get; set; } = "CentEmp";
        public String getRuleTechToBuildRoadsInFungusLineStr()
        {
            return ruleTechToBuildRoadsInFungus + ", ; Technology to build roads in fungus";
        }

        public String ruleTechToAllowOrbitalInsertion { get; set; } = "Gravity";
        public String getRuleTechToAllowOrbitalInsertionLineStr() // todo when orbital insertion is unlocked disable space elevator project
        {
            return ruleTechToAllowOrbitalInsertion + ", ; Technology to allow orbital insertion w/o Space Elevator";
        }

        public String ruleHumansCanAlwaysContactEachOther { get; set; } = "1";
        public String getRuleHumansCanAlwaysContactEachOtherLineStr()
        {
            return ruleHumansCanAlwaysContactEachOther + ",       ; If non-zero, humans can always contact each other in net games";
        }

        public String ruleGlobalWarmingFrequencyNumerator { get; set; } = "1";
        public String ruleGlobalWarmingFrequencyDenominator { get; set; } = "1";
        public String getRuleGlobalWarmingFrequencyLineStr()
        {
            return ruleGlobalWarmingFrequencyNumerator + ", " + ruleGlobalWarmingFrequencyDenominator + @"     ; Numerator/Denominator for frequency of global warming (1,2 would be ""half"" normal warming).";
        }

        public String chassisInfantrySpeed { get; set; } = "1";
        public String chassisInfantryTriad { get; set; } = "0";
        public String chassisInfantryRange { get; set; } = "0";
        public String chassisInfantryCargo { get; set; } = "1";
        public String chassisInfantryCost { get; set; } = "1";
        public String chassisInfantryPrereq { get; set; } = Techs.NONE;
        public String getChassisInfantryLineStr()
        {
            return "Infantry,M1,  Squad,M1,      Sentinels,M2,   Garrison,M1,  "+ chassisInfantrySpeed + ", "+ chassisInfantryTriad + ", "+ chassisInfantryRange + ", 0, "+ chassisInfantryCargo + ", "+ chassisInfantryCost + ", "+ chassisInfantryPrereq + ",     Shock Troops,M2,   Elite Guard,M1,";
        }

        public String chassisSpeederSpeed { get; set; } = "2";
        public String chassisSpeederTriad { get; set; } = "0";
        public String chassisSpeederRange { get; set; } = "0";
        public String chassisSpeederCargo { get; set; } = "1";
        public String chassisSpeederCost { get; set; } = "2";
        public String chassisSpeederPrereq { get; set; } = "Mobile";
        public String getChassisSpeederLineStr()
        {
            return "Speeder,M1,   Rover,M1,      Defensive,M1,   Skirmisher,M1," + chassisSpeederSpeed + ", " + chassisSpeederTriad + ", " + chassisSpeederRange + ", 0, " + chassisSpeederCargo + ", " + chassisSpeederCost + ", " + chassisSpeederPrereq + ",   Dragon,M1,         Enforcer,M1,";
        }

        public String chassisHovertankSpeed { get; set; } = "3";
        public String chassisHovertankTriad { get; set; } = "0";
        public String chassisHovertankRange { get; set; } = "0";
        public String chassisHovertankCargo { get; set; } = "1";
        public String chassisHovertankCost { get; set; } = "3";
        public String chassisHovertankPrereq { get; set; } = "NanoMin";
        public String getChassisHovertankLineStr()
        {
            return "Hovertank,M1, Tank,M1,       Skimmer,M1,     Evasive,M1,   " + chassisHovertankSpeed + ", " + chassisHovertankTriad + ", " + chassisHovertankRange + ", 0, " + chassisHovertankCargo + ", " + chassisHovertankCost + ", " + chassisHovertankPrereq + ",  Behemoth,M1,       Guardian,M1,";
        }

        public String chassisFoilSpeed { get; set; } = "4";
        public String chassisFoilTriad { get; set; } = "1";
        public String chassisFoilRange { get; set; } = "0";
        public String chassisFoilCargo { get; set; } = "2";
        public String chassisFoilCost { get; set; } = "4";
        public String chassisFoilPrereq { get; set; } = "DocFlex";
        public String getChassisFoilLineStr()
        {
            return "Foil,M1,      Skimship,M1,   Hoverboat,M1,   Coastal,M1,   " + chassisFoilSpeed + ", " + chassisFoilTriad + ", " + chassisFoilRange + ", 0, " + chassisFoilCargo + ", " + chassisFoilCost + ", " + chassisFoilPrereq + ",  Megafoil,M1,       Superfoil,M1,";
        }

        public String chassisCruiserSpeed { get; set; } = "6";
        public String chassisCruiserTriad { get; set; } = "1";
        public String chassisCruiserRange { get; set; } = "0";
        public String chassisCruiserCargo { get; set; } = "4";
        public String chassisCruiserCost { get; set; } = "6";
        public String chassisCruiserPrereq { get; set; } = "DocInit";
        public String getChassisCruiserLineStr()
        {
            return "Cruiser,M1,   Destroyer,M1,  Cutter,M1,      Gunboat,M1,   " + chassisCruiserSpeed + ", " + chassisCruiserTriad + ", " + chassisCruiserRange + ", 0, " + chassisCruiserCargo + ", " + chassisCruiserCost + ", " + chassisCruiserPrereq + ",  Battleship,M1,     Monitor,M1,";
        }

        public String chassisNeedlejetSpeed { get; set; } = "8";
        public String chassisNeedlejetTriad { get; set; } = "2";
        public String chassisNeedlejetRange { get; set; } = "2";
        public String chassisNeedlejetCargo { get; set; } = "1";
        public String chassisNeedlejetCost { get; set; } = "8";
        public String chassisNeedlejetPrereq { get; set; } = "DocAir";
        public String getChassisNeedlejetLineStr()
        {
            return "Needlejet,M1, Penetrator,M1, Interceptor,M1, Tactical,M1,  " + chassisNeedlejetSpeed + ", " + chassisNeedlejetTriad + ", " + chassisNeedlejetRange + ", 0, " + chassisNeedlejetCargo + ", " + chassisNeedlejetCost + ", " + chassisNeedlejetPrereq + ",   Thunderbolt,M1,    Sovereign,M1,";
        }

        public String chassisCopterSpeed { get; set; } = "8";
        public String chassisCopterTriad { get; set; } = "2";
        public String chassisCopterRange { get; set; } = "1";
        public String chassisCopterCargo { get; set; } = "1";
        public String chassisCopterCost { get; set; } = "8";
        public String chassisCopterPrereq { get; set; } = "MindMac";
        public String getChassisCopterLineStr()
        {
            if (isAlphax)
                return "Copter,M1,    Chopper,M1,    Rotor,M1,       Lifter,M1,    " + chassisCopterSpeed + ", " + chassisCopterTriad + ", " + chassisCopterRange + ", 0, " + chassisCopterCargo + ", " + chassisCopterCost + ", " + chassisCopterPrereq + ",  Gunship,M1,        Warbird,M1,";
            else
                return "'Copter,M1,   Chopper,M1,    Rotor,M1,       Lifter,M1,    " + chassisCopterSpeed + ", " + chassisCopterTriad + ", " + chassisCopterRange + ", 0, " + chassisCopterCargo + ", " + chassisCopterCost + ", " + chassisCopterPrereq + ",  Gunship,M1,        Warbird,M1,";
        }


        public String chassisGravshipSpeed { get; set; } = "8";
        public String chassisGravshipTriad { get; set; } = "2";
        public String chassisGravshipRange { get; set; } = "0";
        public String chassisGravshipCargo { get; set; } = "1";
        public String chassisGravshipCost { get; set; } = "8";
        public String chassisGravshipPrereq { get; set; } = "Gravity";
        public String getChassisGravshipLineStr()
        {
            return "Gravship,M1,  Skybase,M1,    Antigrav,M1,    Skyfort,M1,   " + chassisGravshipSpeed + ", " + chassisGravshipTriad + ", " + chassisGravshipRange + ", 0, " + chassisGravshipCargo + ", " + chassisGravshipCost + ", " + chassisGravshipPrereq + ",  Deathsphere,M1,    Doomwall,M1,";
        }

        public String chassisMissileSpeed { get; set; } = "12";
        public String chassisMissileTriad { get; set; } = "2";
        public String chassisMissileRange { get; set; } = "1";
        public String chassisMissileCargo { get; set; } = "0";
        public String chassisMissileCost { get; set; } = "12";
        public String chassisMissilePrereq { get; set; } = "Orbital";
        public String getChassisMissileLineStr()
        {
            return "Missile,M1,   Missile,M1,    Missile,M1,     Missile,M1,  " + chassisMissileSpeed + ", " + chassisMissileTriad + ", " + chassisMissileRange + ", 1, " + chassisMissileCargo + "," + chassisMissileCost + ", " + chassisMissilePrereq + ",  Missile,M1,        Missile,M1,";
        }

        public String reactorFissionPower { get; set; } = "1";
        public String reactorFissionPrereq { get; set; } = Techs.NONE;
        public String getReactorFissionLineStr()
        {
            return "Fission Plant,        Fission,     " + reactorFissionPower + ", " + reactorFissionPrereq + ",";
        }

        public String reactorFusionPower { get; set; } = "2";
        public String reactorFusionPrereq { get; set; } = "Fusion";
        public String getReactorFusionLineStr()
        {
            return "Fusion Reactor,       Fusion,      " + reactorFusionPower + ", " + reactorFusionPrereq + ",";
        }

        public String reactorQuantumPower { get; set; } = "3";
        public String reactorQuantumPrereq { get; set; } = "Quantum";
        public String getReactorQuantumLineStr()
        {
            return "Quantum Chamber,      Quantum,     " + reactorQuantumPower + ", " + reactorQuantumPrereq + ",";
        }

        public String reactorSingularityPower { get; set; } = "4";
        public String reactorSingularityPrereq { get; set; } = "SingMec";
        public String getReactorSingularityLineStr()
        {
            return "Singularity Engine,   Singularity, " + reactorSingularityPower + ", " + reactorSingularityPrereq + ",";
        }

        public String weaponPsiAttackPower { get; set; } = "-1";
        public String weaponPsiAttackCost { get; set; } = "10";
        public String weaponPsiAttackPrereq { get; set; } = "CentPsi";
        public String getWeaponPsiAttackLineStr()
        {
            return "Psi Attack,           Psi,           " + weaponPsiAttackPower + ", 2," + weaponPsiAttackCost + ", -1, " + weaponPsiAttackPrereq + ",";
        }

        public String weaponPlanetBusterPower { get; set; } = "99";
        public String weaponPlanetBusterCost { get; set; } = "32";
        public String weaponPlanetBusterPrereq { get; set; } = "Orbital";
        public String getWeaponPlanetBusterLineStr()
        {
            return "Planet Buster,        Planet Buster, " + weaponPlanetBusterPower + ", 0," + weaponPlanetBusterCost + ", -1, " + weaponPlanetBusterPrereq + ",";
        }

        public String weaponColonyModulePower { get; set; } = "0";
        public String weaponColonyModuleCost { get; set; } = "10";
        public String weaponColonyModulePrereq { get; set; } = Techs.NONE;
        public String getWeaponColonyModuleLineStr()
        {
            return "Colony Module,        Colony Pod,     " + weaponColonyModulePower + ", 8," + weaponColonyModuleCost + ", -1, " + weaponColonyModulePrereq + ",     ; Noncombat packages";
        }

        public String weaponTerraformingUnitPower { get; set; } = "0";
        public String weaponTerraformingUnitCost { get; set; } = "6";
        public String weaponTerraformingUnitPrereq { get; set; } = "Ecology";
        public String getWeaponTerraformingUnitLineStr()
        {
            return "Terraforming Unit,    Formers,        " + weaponTerraformingUnitPower + ", 9, " + weaponTerraformingUnitCost + ", -1, " + weaponTerraformingUnitPrereq + ",";
        }

        public String weaponTransportPower { get; set; } = "0";
        public String weaponTransportCost { get; set; } = "4";
        public String weaponTransportPrereq { get; set; } = "DocFlex";
        public String getWeaponTransportLineStr()
        {
            return "Troop Transport,      Transport,      " + weaponTransportPower + ", 7, " + weaponTransportCost + ", -1, " + weaponTransportPrereq + ",";
        }

        public String weaponSupplyPower { get; set; } = "0";
        public String weaponSupplyCost { get; set; } = "8";
        public String weaponSupplyPrereq { get; set; } = "IndAuto";
        public String getWeaponSupplyLineStr()
        {
            return "Supply Transport,     Supply,         " + weaponSupplyPower + ",10, " + weaponSupplyCost + ", -1, " + weaponSupplyPrereq + ",";
        }

        public String weaponConventionalPayloadPower { get; set; } = "12";
        public String weaponConventionalPayloadCost { get; set; } = "12";
        public String weaponConventionalPayloadPrereq { get; set; } = "Orbital";
        public String getWeaponConventionalPayloadLineStr()
        {
            return "Conventional Payload, Conventional,  " + weaponConventionalPayloadPower + ", 0," + weaponConventionalPayloadCost + ", -1, " + weaponConventionalPayloadPrereq + ",";
        }

        public String defensePsiDefensePower { get; set; } = "-1";
        public String defensePsiDefenseCost { get; set; } = "6";
        public String defensePsiDefensePrereq { get; set; } = "Eudaim";
        public String getDefensePsiDefenseLineStr()
        {
            return "Psi Defense,         Psi,        " + defensePsiDefensePower + ", 2, " + defensePsiDefenseCost + ", " + defensePsiDefensePrereq + ",";
        }

        public String abilitySuperFormerCost { get; set; } = "1";
        public String abilitySuperFormerPrereq { get; set; } = "EcoEng2";
        public String getAbilitySuperFormerLineStr()
        {
            if (isAlphax)
                return "Super Former,           " + abilitySuperFormerCost + ", " + abilitySuperFormerPrereq + ",  Super,     000000010111, Terraform rate doubled";
            else
                return "Super Former,           " + abilitySuperFormerCost + ", " + abilitySuperFormerPrereq + ",  Super,     00000010111, Terraform rate doubled";
        }
        
        public String abilityDropPodsCost { get; set; } = "2";
        public String abilityDropPodsPrereq { get; set; } = "MindMac";
        public String getAbilityDropPodsLineStr()
        {
            if (isAlphax)
                return "Drop Pods,              " + abilityDropPodsCost + ", " + abilityDropPodsPrereq + ",  Drop,      000000111001, Makes air drops";
            else
                return "Drop Pods,              " + abilityDropPodsCost + ", " + abilityDropPodsPrereq + ",  Drop,      00000111001, Makes air drops";
        }
        
        public String abilityDeepPressureHullCost { get; set; } = "1";
        public String abilityDeepPressureHullPrereq { get; set; } = "Metal";
        public String getAbilityDeepPressureHullLineStr()
        {
            if (isAlphax)
                return "Deep Pressure Hull,     " + abilityDeepPressureHullCost + ", " + abilityDeepPressureHullPrereq + ",    Sub,       000000111010, Operates underwater";
            else
                return "Deep Pressure Hull,     " + abilityDeepPressureHullCost + ", " + abilityDeepPressureHullPrereq + ",    Sub,       00000111010, Operates underwater";
        }
        
        public String abilityCarrierDeckCost { get; set; } = "1";
        public String abilityCarrierDeckPrereq { get; set; } = "Metal";
        public String getAbilityCarrierDeckLineStr()
        {
            if (isAlphax)
                return "Carrier Deck,           " + abilityCarrierDeckCost + ", " + abilityCarrierDeckPrereq + ",    Carrier,   000101101010, Mobile Airbase";
            else
                return "Carrier Deck,           " + abilityCarrierDeckCost + ", " + abilityCarrierDeckPrereq + ",    Carrier,   00101101010, Mobile Airbase";
        }
        
        public String abilityAAATrackingCost { get; set; } = "1";
        public String abilityAAATrackingPrereq { get; set; } = "MilAlg";
        public String getAbilityAAATrackingLineStr()
        {
            if (isAlphax)
                return "AAA Tracking,           " + abilityAAATrackingCost + ", " + abilityAAATrackingPrereq + ",   AAA,       000010001011, x2 vs. air attacks";
            else
                return "AAA Tracking,           " + abilityAAATrackingCost + ", " + abilityAAATrackingPrereq + ",   AAA,       00010001011, x2 vs. air attacks";
        }
        
        public String abilityFungicideTanksCost { get; set; } = "1";
        public String abilityFungicideTanksPrereq { get; set; } = "Fossil";
        public String getAbilityFungicideTanksLineStr()
        {
            if (isAlphax)
                return "Fungicide Tanks,        " + abilityFungicideTanksCost + ", " + abilityFungicideTanksPrereq + ",   Fungicidal,000000010111, Clear fungus at double speed";
            else
                return "Fungicide Tanks,        " + abilityFungicideTanksCost + ", " + abilityFungicideTanksPrereq + ",   Fungicidal,00000010111, Clear fungus at double speed";
        }
        
        public String abilityCleanReactorCost { get; set; } = "2";
        public String abilityCleanReactorPrereq { get; set; } = "BioEng";
        public String getAbilityCleanReactorLineStr()
        {
            if (isAlphax)
                return "Clean Reactor,          " + abilityCleanReactorCost + ", " + abilityCleanReactorPrereq + ",   Clean,     000000111111, Requires no support";
            else
                return "Clean Reactor,          " + abilityCleanReactorCost + ", " + abilityCleanReactorPrereq + ",   Clean,     00000111111, Requires no support";
        }
        
        public String abilityNerveGasPodsCost { get; set; } = "1";
        public String abilityNerveGasPodsPrereq { get; set; } = "Chemist";
        public String getAbilityNerveGasPodsLineStr()
        {
            if (isAlphax)
                return "Nerve Gas Pods,         " + abilityNerveGasPodsCost + ", " + abilityNerveGasPodsPrereq + ",  X,         000011001101, Can +50% offense (Atrocity)";
            else
                return "Nerve Gas Pods,         " + abilityNerveGasPodsCost + ", " + abilityNerveGasPodsPrereq + ",  X,         00011001101, Can +50% offense (Atrocity)";
        }
        
        public String abilityRepairBayCost { get; set; } = "1";
        public String abilityRepairBayPrereq { get; set; } = "Metal";
        public String getAbilityRepairBayLineStr()
        {
            if (isAlphax)
                return "Repair Bay,             " + abilityRepairBayCost + ", " + abilityRepairBayPrereq + ",    Repair,    000100100111, Repairs ground units on board";
            else
                return "Repair Bay,             " + abilityRepairBayCost + ", " + abilityRepairBayPrereq + ",    Repair,    00100100111, Repairs ground units on board";
        }
    }
}
