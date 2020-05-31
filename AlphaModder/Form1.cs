using AlphaModder.Constants;
using AlphaModder.Model;
using AlphaModder.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlphaModder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if ( // if the game folder is set to "Not found", or the game directory can't be found, search for the game folder
                String.Equals(Properties.Settings.Default.GameFolder, "Not found")
                || !SystemUtils.verifyGameFolder(Properties.Settings.Default.GameFolder)
                )
                if(!SystemUtils.findGameFolder())
                    MessageBox.Show("No game installation folder was found. Please configure it in the Tools menu or put the Alpha Modder executable in the game installation folder.");
            
            labelGameFolder.Text = Properties.Settings.Default.GameFolder;
            if(String.Equals(labelGameFolder.Text, "Not found"))
                labelGameFolder.ForeColor = Color.Red;

            // set the huge planet size slider text
            labelHugePlanetSize.Text = "Default \n(128 x 64)";

            setMousewheelHandlersDisable();
            refreshPresetsDropdown();
        }

        // set control mousewheel handlers to do nothing so they don't change value when the user
        // tries to scroll down the page
        private void setMousewheelHandlersDisable()
        {
            // trackbars for General tab
            trackBarTerraformingRate.MouseWheel += new MouseEventHandler(delegate (object sender, MouseEventArgs e)
            { ((HandledMouseEventArgs)e).Handled = true; });
            trackBarDroneRiots.MouseWheel += new MouseEventHandler(delegate (object sender, MouseEventArgs e)
            { ((HandledMouseEventArgs)e).Handled = true; });
            trackBarNativeLife.MouseWheel += new MouseEventHandler(delegate (object sender, MouseEventArgs e)
            { ((HandledMouseEventArgs)e).Handled = true; });
            trackBarFungus.MouseWheel += new MouseEventHandler(delegate (object sender, MouseEventArgs e)
            { ((HandledMouseEventArgs)e).Handled = true; });
            trackBarPopulationLimits.MouseWheel += new MouseEventHandler(delegate (object sender, MouseEventArgs e)
            { ((HandledMouseEventArgs)e).Handled = true; });
            trackBarHugePlanetSize.MouseWheel += new MouseEventHandler(delegate (object sender, MouseEventArgs e)
            { ((HandledMouseEventArgs)e).Handled = true; });
            trackBarTechDiscoveryRate.MouseWheel += new MouseEventHandler(delegate (object sender, MouseEventArgs e)
            { ((HandledMouseEventArgs)e).Handled = true; });
            
            // numeric up/downs for reactor power
            numericFissionPlantPower.MouseWheel += new MouseEventHandler(delegate (object sender, MouseEventArgs e)
            { ((HandledMouseEventArgs)e).Handled = true; });
            numericFusionReactorPower.MouseWheel += new MouseEventHandler(delegate (object sender, MouseEventArgs e)
            { ((HandledMouseEventArgs)e).Handled = true; });
            numericQuantumChamberPower.MouseWheel += new MouseEventHandler(delegate (object sender, MouseEventArgs e)
            { ((HandledMouseEventArgs)e).Handled = true; });
            numericSingularityEnginePower.MouseWheel += new MouseEventHandler(delegate (object sender, MouseEventArgs e)
            { ((HandledMouseEventArgs)e).Handled = true; });

            // numeric up/down for number of turns between council meetings
            numTurnsBetweenCouncils.MouseWheel += new MouseEventHandler(delegate (object sender, MouseEventArgs e)
            { ((HandledMouseEventArgs)e).Handled = true; });
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            String applyMessage = "alpha.txt and alphax.txt will be overwritten with the selected settings.";
            DialogResult dialogResult = MessageBox.Show(applyMessage, "Apply Modifications", MessageBoxButtons.OKCancel);
            if (dialogResult == DialogResult.Cancel)
            {
                return;
            }

            AlphaConfiguration alphaConfiguration = buildAlphaConfiguration();

            // generate an alpha.txt file and save it as alpha.txt
            alphaConfiguration.isAlphax = false;
            if(DataUtils.saveAlphaFile(alphaConfiguration, Properties.Settings.Default.AlphaFilePath))
            {
                // play sound for success
                List<String> saveSoundList = new List<String>();
                saveSoundList.Add(Sounds.PRODUCTION_COMPLETE);
                saveSoundList.Add(Sounds.PROJECT_COMPLETE);
                SystemUtils.playRandomSound(saveSoundList);

                MessageBox.Show("Settings applied successfully.");
            } else
            {
                MessageBox.Show("Something went wrong.  The settings have NOT been applied.");
            }

            // genereate an alphax.txt file and save it as alphax.txt
            alphaConfiguration.isAlphax = true;
            if(DataUtils.saveAlphaFile(alphaConfiguration, Properties.Settings.Default.AlphaXFilePath))
            {
                //MessageBox.Show("Settings applied successfully.");
            } else
            {
                MessageBox.Show("AlphaX: Something went wrong.  The settings have NOT been applied.");
            }
        }

        private AlphaConfiguration buildAlphaConfiguration()
        {
            AlphaConfiguration alphaConfiguration = new AlphaConfiguration();

#region DISABLED CHECKBOXES

            //if (checkBoxFastTerraforming.Checked)
            //{
            //    alphaConfiguration.terraformFarmRate
            //    = alphaConfiguration.terraformSoilEnricherRate
            //    = alphaConfiguration.terraformMineRate
            //    = alphaConfiguration.terraformSolarCollectorRate
            //    = alphaConfiguration.terraformForrestRate
            //    = alphaConfiguration.terraformRoadRate 
            //    = alphaConfiguration.terraformMagTubeRate 
            //    = alphaConfiguration.terraformBunkerRate
            //    = alphaConfiguration.terraformAirbaseRate
            //    = alphaConfiguration.terraformSensorArrayRate
            //    = alphaConfiguration.terraformRemoveFungusRate
            //    = alphaConfiguration.terraformPlantFungusRate
            //    = alphaConfiguration.terraformCondenserRate
            //    = alphaConfiguration.terraformEchelonMirrorRate
            //    = alphaConfiguration.terraformBoreholeRate
            //    = alphaConfiguration.terraformAquiferRate
            //    = alphaConfiguration.terraformRaiseLandRate
            //    = alphaConfiguration.terraformLowerLandRate
            //    = alphaConfiguration.terraformLevelRate
            //    = "1";
            //}

            //if (checkBoxEasyDrones.Checked)
            //{
            //    // free rec commons, hologram theatre, paradise garden, punishment sphere
            //    alphaConfiguration.facilityRecCommonsCost
            //    = alphaConfiguration.facilityRecCommonsMaint
            //    = alphaConfiguration.facilityParadiseGardenCost
            //    = alphaConfiguration.facilityParadiseGardenMaint
            //    = alphaConfiguration.facilityHologramTheatreCost
            //    = alphaConfiguration.facilityHologramTheatreMaint
            //    = alphaConfiguration.facilityPunishmentSphereCost
            //    = alphaConfiguration.facilityPunishmentSphereMaint
            //    = "0";
            //    // police special ability prereq none and no cost
            //    alphaConfiguration.abilityNonLethalMethodsPoliceCost = "0";
            //    alphaConfiguration.abilityNonLethalMethodsPolicePrereq = Techs.NONE;
            //}

            //if (checkBoxDisablePopulationLimits.Checked)
            //{
            //    alphaConfiguration.rulePopLimitWithoutHabComplex = "9999";
            //    alphaConfiguration.rulePopLimitWithoutHabDome = "9999";
            //    // TODO disable hab complex and hab dome facilities
            //}

            //if (checkBoxEasyMindworms.Checked)
            //{
            //    // attack / defense ratio
            //    // disbale building native life
            //    // empath and trance special abilities bonus percent
            //    alphaConfiguration.generalEmpathAttackBonus = "500";
            //    alphaConfiguration.generalTranceDefenseBonus = "500";
            //    // empath and trance spec abilities - no prereq, no cost
            //    alphaConfiguration.abilityEmpathSongCost = "0";
            //    alphaConfiguration.abilityTranceCost = "0";
            //    alphaConfiguration.abilityEmpathSongPrereq = Techs.NONE;
            //    alphaConfiguration.abilityTrancePrereq = Techs.NONE;
            //    // disable psi defense
            //    alphaConfiguration.defensePsiDefensePrereq = Techs.DISABLE;
            //}

            //if (checkBoxEasyFungus.Checked)
            //{
            //    // TODO implement easy fungus
            //}

            #endregion

#region General tab sliders

            // TERRAFORMING

            // set all to 1/2 of standard
            if (trackBarTerraformingRate.Value == 1) 
            {
                alphaConfiguration.terraformFarmRate = "2";
                alphaConfiguration.terraformSoilEnricherRate = "4";
                alphaConfiguration.terraformMineRate = "4";
                alphaConfiguration.terraformSolarCollectorRate = "2";
                alphaConfiguration.terraformForrestRate = "2";
                alphaConfiguration.terraformMagTubeRate = "2";
                alphaConfiguration.terraformBunkerRate = "2";
                alphaConfiguration.terraformAirbaseRate = "5";
                alphaConfiguration.terraformSensorArrayRate = "2";
                alphaConfiguration.terraformRemoveFungusRate = "3";
                alphaConfiguration.terraformPlantFungusRate = "3";
                alphaConfiguration.terraformCondenserRate = "6";
                alphaConfiguration.terraformEchelonMirrorRate = "6";
                alphaConfiguration.terraformBoreholeRate = "12";
                alphaConfiguration.terraformAquiferRate = "9";
                alphaConfiguration.terraformRaiseLandRate = "6";
                alphaConfiguration.terraformLowerLandRate = "6";
                alphaConfiguration.terraformLevelRate = "4";
            }

            // set all to 1/4 of standard
            else if (trackBarTerraformingRate.Value == 2) 
            {
                alphaConfiguration.terraformFarmRate = "1";
                alphaConfiguration.terraformSoilEnricherRate = "2";
                alphaConfiguration.terraformMineRate = "2";
                alphaConfiguration.terraformSolarCollectorRate = "1";
                alphaConfiguration.terraformForrestRate = "1";
                alphaConfiguration.terraformMagTubeRate = "1";
                alphaConfiguration.terraformBunkerRate = "1";
                alphaConfiguration.terraformAirbaseRate = "2";
                alphaConfiguration.terraformSensorArrayRate = "1";
                alphaConfiguration.terraformRemoveFungusRate = "1";
                alphaConfiguration.terraformPlantFungusRate = "1";
                alphaConfiguration.terraformCondenserRate = "3";
                alphaConfiguration.terraformEchelonMirrorRate = "3";
                alphaConfiguration.terraformBoreholeRate = "6";
                alphaConfiguration.terraformAquiferRate = "4";
                alphaConfiguration.terraformRaiseLandRate = "3";
                alphaConfiguration.terraformLowerLandRate = "3";
                alphaConfiguration.terraformLevelRate = "2";
            }

            // set all to 1 turn
            else if (trackBarTerraformingRate.Value == 3) 
            {
                alphaConfiguration.terraformFarmRate
                = alphaConfiguration.terraformSoilEnricherRate
                = alphaConfiguration.terraformMineRate
                = alphaConfiguration.terraformSolarCollectorRate
                = alphaConfiguration.terraformForrestRate
                = alphaConfiguration.terraformRoadRate
                = alphaConfiguration.terraformMagTubeRate
                = alphaConfiguration.terraformBunkerRate
                = alphaConfiguration.terraformAirbaseRate
                = alphaConfiguration.terraformSensorArrayRate
                = alphaConfiguration.terraformRemoveFungusRate
                = alphaConfiguration.terraformPlantFungusRate
                = alphaConfiguration.terraformCondenserRate
                = alphaConfiguration.terraformEchelonMirrorRate
                = alphaConfiguration.terraformBoreholeRate
                = alphaConfiguration.terraformAquiferRate
                = alphaConfiguration.terraformRaiseLandRate
                = alphaConfiguration.terraformLowerLandRate
                = alphaConfiguration.terraformLevelRate
                = "1";
            }

            // DRONE RIOTS

            // half cost rec commons, hologram theatre, paradise garden, punishment sphere
            if (trackBarDroneRiots.Value == 1)
            {
                alphaConfiguration.facilityRecCommonsCost = "2";
                alphaConfiguration.facilityParadiseGardenCost = "6";
                alphaConfiguration.facilityParadiseGardenMaint = "2";
                alphaConfiguration.facilityHologramTheatreCost = "3";
                alphaConfiguration.facilityHologramTheatreMaint = "1";
                alphaConfiguration.facilityPunishmentSphereCost = "5";
                alphaConfiguration.facilityPunishmentSphereMaint = "1";
                // police special ability prereq none and no cost
                alphaConfiguration.abilityNonLethalMethodsPolicePrereq = Techs.NONE;
            }

            
            else if (trackBarDroneRiots.Value == 2)
            {
                // free rec commons, hologram theatre, paradise garden, punishment sphere
                alphaConfiguration.facilityRecCommonsCost
                = alphaConfiguration.facilityRecCommonsMaint
                = alphaConfiguration.facilityParadiseGardenCost
                = alphaConfiguration.facilityParadiseGardenMaint
                = alphaConfiguration.facilityHologramTheatreCost
                = alphaConfiguration.facilityHologramTheatreMaint
                = alphaConfiguration.facilityPunishmentSphereCost
                = alphaConfiguration.facilityPunishmentSphereMaint
                = "0";

                // unlock rec commons, hologram theatre, paradise garden, punishment sphere
                alphaConfiguration.facilityRecCommonsPrereq
                = alphaConfiguration.facilityHologramTheatrePrereq
                = alphaConfiguration.facilityParadiseGardenPrereq
                = alphaConfiguration.facilityPunishmentSpherePrereq
                = Techs.NONE;

                // police special ability prereq none and no cost
                alphaConfiguration.abilityNonLethalMethodsPoliceCost = "0";
                alphaConfiguration.abilityNonLethalMethodsPolicePrereq = Techs.NONE;
                // TODO better PSYCH social engineering
            }

            if (trackBarNativeLife.Value == 1)
            {
                // TODO implement native life 1
                // empath and trance special abilities double effectiveness
                // Empath Song and Hypnotic Trance abilities unlocked, cost 1, and double attack/defense bonus. Native life units and psi attack/defense disabled.
                alphaConfiguration.generalEmpathAttackBonus = "100";
                alphaConfiguration.generalTranceDefenseBonus = "100";
                // trance and empath cheaper
                alphaConfiguration.abilityEmpathSongCost = "1";
                alphaConfiguration.abilityTranceCost = "1";
                // trance and empath unlocked
                alphaConfiguration.abilityEmpathSongPrereq = Techs.NONE;
                alphaConfiguration.abilityTrancePrereq = Techs.NONE;
                // disable psi defense and attack
                alphaConfiguration.defensePsiDefensePrereq = Techs.DISABLE;
                // disable psi attack
                alphaConfiguration.weaponPsiAttackPrereq = Techs.DISABLE;

                // disbale building native life
                alphaConfiguration.unitMindWormsPrereq = Techs.DISABLE;
                alphaConfiguration.unitIsleOfTheDeepPrereq = Techs.DISABLE;
                alphaConfiguration.unitLocustsOfChironPrereq = Techs.DISABLE;
            }
            else if (trackBarNativeLife.Value == 2)
            {
                // TODO psi attack / defense ratio
                // empath and trance special abilities x10 effectiveness
                // Empath Song and Hypnotic Trance abilities unlocked, free, and x10 attack/defense bonus. Native life units and psi attack/defense disabled.
                alphaConfiguration.generalEmpathAttackBonus = "500";
                alphaConfiguration.generalTranceDefenseBonus = "500";
                // empath and trance spec abilities - no prereq, no cost
                alphaConfiguration.abilityEmpathSongCost = "0";
                alphaConfiguration.abilityTranceCost = "0";
                alphaConfiguration.abilityEmpathSongPrereq = Techs.NONE;
                alphaConfiguration.abilityTrancePrereq = Techs.NONE;
                // disable psi defense
                alphaConfiguration.defensePsiDefensePrereq = Techs.DISABLE;
                // disable psi attack
                alphaConfiguration.weaponPsiAttackPrereq = Techs.DISABLE;

                // disbale building native life units
                alphaConfiguration.unitMindWormsPrereq = Techs.DISABLE;
                alphaConfiguration.unitIsleOfTheDeepPrereq = Techs.DISABLE;
                alphaConfiguration.unitLocustsOfChironPrereq = Techs.DISABLE;
            }

            if (trackBarFungus.Value == 1)
            {
                // Easy - Double fungus removal speed, Fungicide Tanks ability unlocked, unlock build roads on fungus and ease fungus movement.
                // TODO implement fungus 1
                // remove fungus double speed (3 turns)
                alphaConfiguration.terraformRemoveFungusRate = "3";
                // earlier cheaper fungicide tanks
                alphaConfiguration.abilityFungicideTanksPrereq = Techs.NONE;
                // disable pant fungus
                alphaConfiguration.terraformPlantFungusPrereq = Techs.DISABLE;
                // unlock, ease fungus movement, and build roads in fungus
                alphaConfiguration.ruleTechToEaseFungusMovement = Techs.NONE;
                alphaConfiguration.ruleTechToBuildRoadsInFungus = Techs.NONE;
            }
            else if(trackBarFungus.Value == 2)
            {
                // Very Easy = Remove fungus 1 turn, disable fungicide tanks, unlock build roads on fungus and ease fungus movement.
                // terraforming
                // remove fungus 1 turn
                alphaConfiguration.terraformRemoveFungusRate = "1";
                // disable fungicide tanks so ai doesn't use it
                alphaConfiguration.abilityFungicideTanksPrereq = Techs.DISABLE;
                // unlock tech ease fungus movement
                // unlock tech build roads fungus
                alphaConfiguration.ruleTechToEaseFungusMovement = Techs.NONE;
                alphaConfiguration.ruleTechToBuildRoadsInFungus = Techs.NONE;

                // disable pant fungus
                alphaConfiguration.terraformPlantFungusPrereq = Techs.DISABLE;
            }

            if(trackBarPopulationLimits.Value == 1)
            {
                // Relaxed - Population limit without Hab Complex: 11, without Habitation Dome: 18.  Both are half cost.  Hab Complex unlocked, Habitation Dome prerequisite: Industrial Automation.
                // pop limit without hab complex / dome 11/18
                alphaConfiguration.rulePopLimitWithoutHabComplex = "11";
                alphaConfiguration.rulePopLimitWithoutHabDome = "18";
                // hab dome and hab comlplex half cost/maint
                alphaConfiguration.facilityHabComplexPrereq = Techs.DISABLE;
                alphaConfiguration.facilityHabComplexCost = "4";
                alphaConfiguration.facilityHabComplexMaint = "1";
                alphaConfiguration.facilityHabDomePrereq = "IndAuto";
                alphaConfiguration.facilityHabDomeCost = "8";
                alphaConfiguration.facilityHabDomeMaint = "2";
            }
            else if(trackBarPopulationLimits.Value == 2)
            {
                // Unlimited
                // pop limit without hab dome/hab complex 9999
                alphaConfiguration.rulePopLimitWithoutHabComplex = "9999";
                alphaConfiguration.rulePopLimitWithoutHabDome = "9999";

                //disable hab dome and hab complex
                alphaConfiguration.facilityHabComplexPrereq = Techs.DISABLE;
                alphaConfiguration.facilityHabDomePrereq = Techs.DISABLE;
            }

            if (trackBarHugePlanetSize.Value == 1)
            {
                // x1.5 - 128 x 96
                alphaConfiguration.planetSizeHugeX = "128";
                alphaConfiguration.planetSizeHugeY = "96";
            }
            else if (trackBarHugePlanetSize.Value == 2)
            {
                // x2 - 128 x 128
                alphaConfiguration.planetSizeHugeX =
                alphaConfiguration.planetSizeHugeY = "128";
            }
            else if (trackBarHugePlanetSize.Value == 3)
            {
                // x4 - 256 x 128
                alphaConfiguration.planetSizeHugeX = "256";
                alphaConfiguration.planetSizeHugeY = "128";
            }
            else if (trackBarHugePlanetSize.Value == 4)
            {
                // x8 - 256 x 256
                alphaConfiguration.planetSizeHugeX =
                alphaConfiguration.planetSizeHugeY = "256";
            }

            if(trackBarTechDiscoveryRate.Value == 0)
            {
                // x0
                alphaConfiguration.ruleTechDiscoveryRate = "0";
            }
            else if(trackBarTechDiscoveryRate.Value == 1)
            {
                // x0.1
                alphaConfiguration.ruleTechDiscoveryRate = "10";
            }
            else if(trackBarTechDiscoveryRate.Value == 2)
            {
                // x0.5
                alphaConfiguration.ruleTechDiscoveryRate = "50";
            }
            else if(trackBarTechDiscoveryRate.Value == 3)
            {
                // x0.75
                alphaConfiguration.ruleTechDiscoveryRate = "75";
            }
            // 4 is standard
            else if(trackBarTechDiscoveryRate.Value == 5)
            {
                // x1.25
                alphaConfiguration.ruleTechDiscoveryRate = "125";
            }
            else if(trackBarTechDiscoveryRate.Value == 6)
            {
                // x1.5
                alphaConfiguration.ruleTechDiscoveryRate = "150";
            }
            else if(trackBarTechDiscoveryRate.Value == 7)
            {
                // x2
                alphaConfiguration.ruleTechDiscoveryRate = "200";
            }
            else if(trackBarTechDiscoveryRate.Value == 8)
            {
                // x5
                alphaConfiguration.ruleTechDiscoveryRate = "500";
            }
            else if(trackBarTechDiscoveryRate.Value == 9)
            {
                // x10
                alphaConfiguration.ruleTechDiscoveryRate = "1000";
            }

            #endregion General tab sliders

            #region Misc tab

            if (checkBoxUnlockTerraforming.Checked)
            {
                alphaConfiguration.terraformSoilEnricherPrereq
                = alphaConfiguration.terraformMagTubePrereq
                = alphaConfiguration.terraformBunkerPrereq
                = alphaConfiguration.terraformAirbasePrereq
                = alphaConfiguration.terraformPlantFungusPrereq
                = alphaConfiguration.terraformPlantFungusSeaPrereq
                = alphaConfiguration.terraformCondenserPrereq
                = alphaConfiguration.terraformEchelonMirrorPrereq
                = alphaConfiguration.terraformBoreholePrereq
                = alphaConfiguration.terraformAquiferPrereq
                = alphaConfiguration.terraformRaiseLandPrereq
                = alphaConfiguration.terraformRaiseLandSeaPrereq
                = alphaConfiguration.terraformLowerLandPrereq
                = alphaConfiguration.terraformLowerLandSeaPrereq
                = Constants.Techs.NONE;
            }

            

            if (checkBoxDisableObliterateIsAtrocity.Checked)
            {
                alphaConfiguration.ruleObliterateBaseIsAtrocity = "0";
            }



            if (checkBoxDisableProbeTeams.Checked)
            {
                // disable probe team weapon
                alphaConfiguration.weaponProbeTeamPrereq = Techs.DISABLE;
                // disable hunter-seeker project
                alphaConfiguration.projectHunterSeekerPrereq = Techs.DISABLE;
                // disable probe team unit
                alphaConfiguration.unitProbeTeamPrereq = Techs.DISABLE;
                // TODO add special abilities for alphax
            }

            if (checkBoxEnableHeavyTransport.Checked)
            {
                alphaConfiguration.heavyTransportPrereq = Techs.DOCTRINE_INITIATIVE;
            }

            // turns between council meetings
            alphaConfiguration.minTurnsBetweenCouncils = numTurnsBetweenCouncils.Value.ToString();

            // TODO all reactors available / just singularity available

            // TODO reactor power adjust

            #endregion Misc tab

            #region Facilities tab

            // Rec commons
            if (checkBoxUnlockRecreationCommons.Checked) alphaConfiguration.facilityRecCommonsPrereq = Techs.NONE;
            if (checkBoxDisableRecreationCommons.Checked) alphaConfiguration.facilityRecCommonsPrereq = Techs.DISABLE;
            if (checkBoxFreeRecreationCommons.Checked) alphaConfiguration.facilityRecCommonsCost = alphaConfiguration.facilityRecCommonsMaint = "0";

            // Hologram theatre
            if (checkBoxUnlockHologramTheatre.Checked) alphaConfiguration.facilityHologramTheatrePrereq = Techs.NONE;
            if (checkBoxDisableHologramTheatre.Checked) alphaConfiguration.facilityHologramTheatrePrereq = Techs.DISABLE;
            if (checkBoxFreeHologramTheatre.Checked) alphaConfiguration.facilityHologramTheatreCost = alphaConfiguration.facilityHologramTheatreMaint = "0";

            //paradise garden
            if (checkBoxUnlockParadiseGarden.Checked) alphaConfiguration.facilityParadiseGardenPrereq = Techs.NONE;
            if (checkBoxDisableParadiseGarden.Checked) alphaConfiguration.facilityParadiseGardenPrereq = Techs.DISABLE;
            if (checkBoxFreeParadiseGarden.Checked) alphaConfiguration.facilityParadiseGardenCost = alphaConfiguration.facilityParadiseGardenMaint = "0";

            // punishment sphere
            if (checkBoxUnlockPunishmentSphere.Checked) alphaConfiguration.facilityPunishmentSpherePrereq = Techs.NONE;
            if (checkBoxDisablePunishmentSphere.Checked) alphaConfiguration.facilityPunishmentSpherePrereq = Techs.DISABLE;
            if (checkBoxFreePunishmentSphere.Checked) alphaConfiguration.facilityPunishmentSphereCost = alphaConfiguration.facilityPunishmentSphereMaint = "0";

            // skunkworks
            if (checkBoxUnlockSkunkworks.Checked) alphaConfiguration.facilitySkunkworksPrereq = Techs.NONE;
            if (checkBoxDisableSkunkworks.Checked) alphaConfiguration.facilitySkunkworksPrereq = Techs.DISABLE;
            if (checkBoxFreeSkunkworks.Checked) alphaConfiguration.facilitySkunkworksCost = alphaConfiguration.facilitySkunkworksMaint = "0";

            // hab complex
            if (checkBoxUnlockHabComplex.Checked) alphaConfiguration.facilityHabComplexPrereq = Techs.NONE;
            if (checkBoxDisableHabComplex.Checked) alphaConfiguration.facilityHabComplexPrereq = Techs.DISABLE;
            if (checkBoxFreeHabComplex.Checked) alphaConfiguration.facilityHabComplexCost = alphaConfiguration.facilityHabComplexMaint = "0";

            // hab dome
            if (checkBoxUnlockHabitationDome.Checked) alphaConfiguration.facilityHabDomePrereq = Techs.NONE;
            if (checkBoxDisableHabitationDome.Checked) alphaConfiguration.facilityHabDomePrereq = Techs.DISABLE;
            if (checkBoxFreeHabitationDome.Checked) alphaConfiguration.facilityHabDomeCost = alphaConfiguration.facilityHabDomeMaint = "0";

            // pressure dome
            if (checkBoxUnlockPressureDome.Checked) alphaConfiguration.facilityPressureDomePrereq = Techs.NONE;
            if (checkBoxDisablePressureDome.Checked) alphaConfiguration.facilityPressureDomePrereq = Techs.DISABLE;
            if (checkBoxFreePressureDome.Checked) alphaConfiguration.facilityPressureDomeCost = alphaConfiguration.facilityPressureDomeMaint = "0";

            // psi gate
            if (checkBoxUnlockPsiGate.Checked) alphaConfiguration.facilityPsiGatePrereq = Techs.NONE;
            if (checkBoxDisablePsiGate.Checked) alphaConfiguration.facilityPsiGatePrereq = Techs.DISABLE;
            if (checkBoxFreePsiGate.Checked) alphaConfiguration.facilityPsiGateCost = alphaConfiguration.facilityPsiGateMaint = "0";

            // sky hydroponics lab
            if (checkBoxUnlockSkyHydroponicsLab.Checked) alphaConfiguration.facilitySkyHydroponicsLabPrereq = Techs.NONE;
            if (checkBoxDisableSkyHydroponicsLab.Checked) alphaConfiguration.facilitySkyHydroponicsLabPrereq = Techs.DISABLE;
            if (checkBoxFreeSkyHydroponicsLab.Checked) alphaConfiguration.facilitySkyHydroponicsLabCost = alphaConfiguration.facilitySkyHydroponicsLabMaint = "0";

            // nessus mining station
            if (checkBoxUnlockNessusMiningStation.Checked) alphaConfiguration.facilityNessusMiningStationPrereq = Techs.NONE;
            if (checkBoxDisableNessusMiningStation.Checked) alphaConfiguration.facilityNessusMiningStationPrereq = Techs.DISABLE;
            if (checkBoxFreeNessusMiningStation.Checked) alphaConfiguration.facilityNessusMiningStationCost = alphaConfiguration.facilityNessusMiningStationMaint = "0";

            // orbital power transmitter
            if (checkBoxUnlockOrbitalPowerTransmitter.Checked) alphaConfiguration.facilityOrbitalPowerTransmitterPrereq = Techs.NONE;
            if (checkBoxDisableOrbitalPowerTransmitter.Checked) alphaConfiguration.facilityOrbitalPowerTransmitterPrereq = Techs.DISABLE;
            if (checkBoxFreeOrbitalPowerTransmitter.Checked) alphaConfiguration.facilityOrbitalPowerTransmitterCost = alphaConfiguration.facilityOrbitalPowerTransmitterMaint = "0";

            // orbital defense pod
            if (checkBoxUnlockOrbitalDefensePod.Checked) alphaConfiguration.facilityOrbitalDefensePodPrereq = Techs.NONE;
            if (checkBoxDisableOrbitalDefensePod.Checked) alphaConfiguration.facilityOrbitalDefensePodPrereq = Techs.DISABLE;
            if (checkBoxFreeOrbitalDefensePod.Checked) alphaConfiguration.facilityOrbitalDefensePodCost = alphaConfiguration.facilityOrbitalDefensePodMaint = "0";

            #endregion Facilities tab

            #region Units tab

            // infantry
            if (checkBoxDisableInfantry.Checked) alphaConfiguration.chassisInfantryPrereq = Techs.DISABLE;
            if (checkBoxFreeInfantry.Checked) alphaConfiguration.chassisInfantryCost = "0";

            // speeder unit
            if (checkBoxUnlockSpeeder.Checked) alphaConfiguration.chassisSpeederPrereq = Techs.NONE;
            if (checkBoxDisableSpeeder.Checked) alphaConfiguration.chassisSpeederPrereq = Techs.DISABLE;
            if (checkBoxFreeSpeeder.Checked) alphaConfiguration.chassisSpeederCost = "0";

            // Hovertank
            if (checkBoxUnlockHoverTanks.Checked) alphaConfiguration.chassisHovertankPrereq = Techs.NONE;
            if (checkBoxDisableHovertanks.Checked) alphaConfiguration.chassisHovertankPrereq = Techs.DISABLE;
            if (checkBoxFreeHovertank.Checked) alphaConfiguration.chassisHovertankCost = "0";

            // Foil
            if (checkBoxUnlockFoils.Checked) alphaConfiguration.chassisFoilPrereq = Techs.NONE;
            if (checkBoxDisableFoils.Checked) alphaConfiguration.chassisFoilPrereq = Techs.DISABLE;
            if (checkBoxFreeFoil.Checked) alphaConfiguration.chassisFoilCost = "0";

            // Cruiser
            if (checkBoxUnlockCruisers.Checked) alphaConfiguration.chassisCruiserPrereq = Techs.NONE;
            if (checkBoxDisableCruisers.Checked) alphaConfiguration.chassisCruiserPrereq = Techs.DISABLE;
            if (checkBoxFreeCruiser.Checked) alphaConfiguration.chassisCruiserCost = "0";

            // Needlejet
            if (checkBoxUnlockPlanes.Checked) alphaConfiguration.chassisNeedlejetPrereq = Techs.NONE;
            if (checkBoxDisablePlanes.Checked) alphaConfiguration.chassisNeedlejetPrereq = Techs.DISABLE;
            if (checkBoxFreeNeedlejet.Checked) alphaConfiguration.chassisNeedlejetCost = "0";

            // Copter
            if (checkBoxUnlockHelicopters.Checked) alphaConfiguration.chassisCopterPrereq = Techs.NONE;
            if (checkBoxDisableHelicopters.Checked) alphaConfiguration.chassisCopterPrereq = Techs.DISABLE;
            if (checkBoxFreeCopter.Checked) alphaConfiguration.chassisCopterCost = "0";

            // Gravship
            if (checkBoxUnlockGravships.Checked) alphaConfiguration.chassisGravshipPrereq = Techs.NONE;
            if (checkBoxDisableGravships.Checked) alphaConfiguration.chassisGravshipPrereq = Techs.DISABLE;
            if (checkBoxFreeGravship.Checked) alphaConfiguration.chassisGravshipCost = "0";

            // Conventional Payload missile
            if (checkBoxUnlockConventionalPayload.Checked) alphaConfiguration.chassisMissilePrereq = Techs.NONE;
            if (checkBoxDisableConventionalPayload.Checked) alphaConfiguration.chassisMissilePrereq = Techs.DISABLE;
            if (checkBoxFreeConventionalPayload.Checked) alphaConfiguration.chassisMissileCost = "0";

            // Planet Buster
            if (checkBoxUnlockPlanetBusters.Checked) alphaConfiguration.weaponPlanetBusterPrereq = Techs.NONE;
            if (checkBoxDisablePlanetBusters.Checked) alphaConfiguration.weaponPlanetBusterPrereq = Techs.DISABLE;
            if (checkBoxFreePlanetBuster.Checked) alphaConfiguration.weaponPlanetBusterCost = "0";

            // ColonyPod
            if (checkBoxDisableColonyPods.Checked) alphaConfiguration.weaponColonyModulePrereq = Techs.DISABLE;
            if (checkBoxFreeColonyPod.Checked) alphaConfiguration.weaponColonyModuleCost = "0";

            // Troop transport
            if (checkBoxUnlockTroopTransport.Checked) alphaConfiguration.weaponTransportPrereq = Techs.NONE;
            if (checkBoxDisableTroopTransport.Checked) alphaConfiguration.weaponTransportPrereq = Techs.DISABLE;
            if (checkBoxFreeTroopTransport.Checked) alphaConfiguration.weaponTransportCost = "0";

            // Supply transport
            if (checkBoxUnlockSupplyTransport.Checked) alphaConfiguration.weaponSupplyPrereq = Techs.NONE;
            if (checkBoxDisableSupplyTransport.Checked) alphaConfiguration.weaponSupplyPrereq = Techs.DISABLE;
            if (checkBoxFreeSupplyTransport.Checked) alphaConfiguration.weaponSupplyCost = "0";

            // Psi attack
            if (checkBoxUnlockPsiAttack.Checked) alphaConfiguration.weaponPsiAttackPrereq = Techs.NONE;
            if (checkBoxDisablePsiAttack.Checked) alphaConfiguration.weaponPsiAttackPrereq = Techs.DISABLE;
            if (checkBoxFreePsiAttack.Checked) alphaConfiguration.weaponPsiAttackCost = "0";

            // Psi defense
            if (checkBoxUnlockPsiDefense.Checked) alphaConfiguration.defensePsiDefensePrereq = Techs.NONE;
            if (checkBoxDisablePsiDefense.Checked) alphaConfiguration.defensePsiDefensePrereq = Techs.DISABLE;
            if (checkBoxFreePsiDefense.Checked) alphaConfiguration.defensePsiDefenseCost = "0";

            #endregion Units tab

            #region Abilities tab

            // super former
            if (checkBoxUnlockSuperFormer.Checked) alphaConfiguration.abilitySuperFormerPrereq = Techs.NONE;
            if (checkBoxDisableSuperFormer.Checked) alphaConfiguration.abilitySuperFormerPrereq = Techs.DISABLE;
            if (checkBoxFreeSuperFormer.Checked) alphaConfiguration.abilitySuperFormerCost = "0";

            // fungicide tanks
            if (checkBoxUnlockFungicideTanks.Checked) alphaConfiguration.abilityFungicideTanksPrereq = Techs.NONE;
            if (checkBoxDisableFungicideTanks.Checked) alphaConfiguration.abilityFungicideTanksPrereq = Techs.DISABLE;
            if (checkBoxFreeFungicideTanks.Checked) alphaConfiguration.abilityFungicideTanksCost = "0";

            // drop pods
            if (checkBoxUnlockDropPods.Checked) alphaConfiguration.abilityDropPodsPrereq = Techs.NONE;
            if (checkBoxDisableDropPods.Checked) alphaConfiguration.abilityDropPodsPrereq = Techs.DISABLE;
            if (checkBoxFreeDropPods.Checked) alphaConfiguration.abilityDropPodsCost = "0";

            // deep pressure hull
            if (checkBoxUnlockDeepPressureHull.Checked) alphaConfiguration.abilityDeepPressureHullPrereq = Techs.NONE;
            if (checkBoxDisableDeepPressureHull.Checked) alphaConfiguration.abilityDeepPressureHullPrereq = Techs.DISABLE;
            if (checkBoxFreeDeepPressureHull.Checked) alphaConfiguration.abilityDeepPressureHullCost = "0";

            // carrier deck
            if (checkBoxUnlockCarrierDeck.Checked) alphaConfiguration.abilityCarrierDeckPrereq = Techs.NONE;
            if (checkBoxDisableCarrierDeck.Checked) alphaConfiguration.abilityCarrierDeckPrereq = Techs.DISABLE;
            if (checkBoxFreeCarrierDeck.Checked) alphaConfiguration.abilityCarrierDeckCost = "0";

            // AAA
            if (checkBoxUnlockAAA.Checked) alphaConfiguration.abilityAAATrackingPrereq = Techs.NONE;
            if (checkBoxDisableAAA.Checked) alphaConfiguration.abilityAAATrackingPrereq = Techs.DISABLE;
            if (checkBoxFreeAAA.Checked) alphaConfiguration.abilityAAATrackingCost = "0";

            // empath
            if (checkBoxUnlockEmpathSong.Checked) alphaConfiguration.abilityEmpathSongPrereq = Techs.NONE;
            if (checkBoxDisableEmpathSong.Checked) alphaConfiguration.abilityEmpathSongPrereq = Techs.DISABLE;
            if (checkBoxFreeEmpathSong.Checked) alphaConfiguration.abilityEmpathSongCost = "0";

            // trance
            if (checkBoxUnlockHypnoticTrance.Checked) alphaConfiguration.abilityTrancePrereq = Techs.NONE;
            if (checkBoxDisableHypnoticTrance.Checked) alphaConfiguration.abilityTrancePrereq = Techs.DISABLE;
            if (checkBoxFreeHypnoticTrance.Checked) alphaConfiguration.abilityTranceCost = "0";

            // clean
            if (checkBoxUnlockCleanReactor.Checked) alphaConfiguration.abilityCleanReactorPrereq = Techs.NONE;
            if (checkBoxDisableCleanReactor.Checked) alphaConfiguration.abilityCleanReactorPrereq = Techs.DISABLE;
            if (checkBoxFreeCleanReactor.Checked) alphaConfiguration.abilityCleanReactorCost = "0";

            // nerve gas pods
            if (checkBoxUnlockNerveGasPods.Checked) alphaConfiguration.abilityNerveGasPodsPrereq = Techs.NONE;
            if (checkBoxDisableNerveGasPods.Checked) alphaConfiguration.abilityNerveGasPodsPrereq = Techs.DISABLE;
            if (checkBoxFreeNerveGasPods.Checked) alphaConfiguration.abilityNerveGasPodsCost = "0";

            // repair bay
            if (checkBoxUnlockRepairBay.Checked) alphaConfiguration.abilityRepairBayPrereq = Techs.NONE;
            if (checkBoxDisableRepairBay.Checked) alphaConfiguration.abilityRepairBayPrereq = Techs.DISABLE;
            if (checkBoxFreeRepairBay.Checked) alphaConfiguration.abilityRepairBayCost = "0";

            // police / non lethal methods
            if (checkBoxUnlockPolice.Checked) alphaConfiguration.abilityNonLethalMethodsPolicePrereq = Techs.NONE;
            if (checkBoxDisablePolice.Checked) alphaConfiguration.abilityNonLethalMethodsPolicePrereq = Techs.DISABLE;
            if (checkBoxFreePolice.Checked) alphaConfiguration.abilityNonLethalMethodsPoliceCost = "0";

            #endregion Abilities tab

            #region reactors tab

            // fission lab
            if (checkBoxDisableFissionPlant.Checked) alphaConfiguration.reactorFissionPrereq = Techs.DISABLE;
            alphaConfiguration.reactorFissionPower = numericFissionPlantPower.Value.ToString();

            // fusion reactor
            if (checkBoxUnlockFusionReactor.Checked) alphaConfiguration.reactorFusionPrereq = Techs.NONE;
            if (checkBoxDisableFusionReactor.Checked) alphaConfiguration.reactorFusionPrereq = Techs.DISABLE;
            alphaConfiguration.reactorFusionPower = numericFusionReactorPower.Value.ToString();

            // quantum chamber
            if (checkBoxUnlockQuantumChamber.Checked) alphaConfiguration.reactorQuantumPrereq = Techs.NONE;
            if (checkBoxDisableQuantumChamber.Checked) alphaConfiguration.reactorQuantumPrereq = Techs.DISABLE;
            alphaConfiguration.reactorQuantumPower = numericQuantumChamberPower.Value.ToString();


            // singularity engine
            if (checkBoxUnlockSingularityEngine.Checked) alphaConfiguration.reactorSingularityPrereq = Techs.NONE;
            if (checkBoxDisableSingularityEngine.Checked) alphaConfiguration.reactorSingularityPrereq = Techs.DISABLE;
            alphaConfiguration.reactorSingularityPower = numericSingularityEnginePower.Value.ToString();

            #endregion

            return alphaConfiguration;
        }

        private void SaveAlphatxtAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AlphaConfiguration alphaConfiguration = buildAlphaConfiguration();
            // show dialog for where to save alpha.txt
            alphaConfiguration.isAlphax = false;
            DataUtils.saveAlphaFile(alphaConfiguration);
        }

        private void SaveAlphaxtxtAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AlphaConfiguration alphaConfiguration = buildAlphaConfiguration();
            // show dialog for where to save alphax.txt
            alphaConfiguration.isAlphax = true;
            DataUtils.saveAlphaFile(alphaConfiguration);
        }

        private void HighResolutionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setResolutionDialog(true);
        }

        private void LowResolutionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setResolutionDialog(false);
        }

        private void setResolutionDialog(bool highRes)
        {
            String setResMessage;
            if (highRes)
                setResMessage = "This will set DirectDraw=0 in 'Alpha Centauri.ini', causing the game to run in desktop resolution.  If your game frequently crashes after changing this, try setting it back either by using this tool to set low resolution, or set DirectDraw=1 in 'Alpha Centauri.ini'.";
            else
                setResMessage = "This will set DirectDraw=1 in 'Alpha Centauri.ini', causing the game to run in default resolution (640 x 480).";
            

            DialogResult dialogResult = MessageBox.Show(setResMessage, "Set Game Resolution", MessageBoxButtons.OKCancel);
            if (dialogResult == DialogResult.OK)
            {
                DataUtils.setResolution(highRes); // set the resolution - true = high res, false = low res
            }
        }

        private void TrackBarTerraformingRate_Scroll(object sender, EventArgs e)
        {
            if(trackBarTerraformingRate.Value == 0)
                labelTerraformingSpeed.Text = "Standard";
            else if(trackBarTerraformingRate.Value == 1)
                labelTerraformingSpeed.Text = "x2";
            else if (trackBarTerraformingRate.Value == 2)
                labelTerraformingSpeed.Text = "x4";
            else if(trackBarTerraformingRate.Value == 3)
                labelTerraformingSpeed.Text = "Instant - all terraforming takes 1 turn";
        }

        private void TrackBarDroneRiots_Scroll(object sender, EventArgs e)
        {
            if (trackBarDroneRiots.Value == 0)
                labelDroneRiots.Text = "Standard";
            else if (trackBarDroneRiots.Value == 1)
                labelDroneRiots.Text = "Easy - Half cost and maintenance for Rec Commons, Hologram Theatre, Paradise Gardens, and Punishment Sphere. Police ability unlocked.";
            else if (trackBarDroneRiots.Value == 2)
                labelDroneRiots.Text = "Very Easy - Rec Commons, Hologram Theatre, Paradise Gardens, Punishment Sphere, and Police ability unlocked and no cost.";
        }
        
        private void TrackBarNativeLife_Scroll(object sender, EventArgs e)
        {
            if (trackBarNativeLife.Value == 0)
                labelNativeLife.Text = "Standard";
            else if (trackBarNativeLife.Value == 1)
                labelNativeLife.Text = "Easy - Empath Song and Hypnotic Trance abilities unlocked, cost 1, and double attack/defense bonus. Native life units and psi attack/defense disabled..";
            else if (trackBarNativeLife.Value == 2)
                labelNativeLife.Text = "Very Easy - Empath Song and Hypnotic Trance abilities unlocked, free, and x10 attack/defense bonus. Native life units and psi attack/defense disabled.";
        }

        private void TrackBarFungus_Scroll(object sender, EventArgs e)
        {
            if (trackBarFungus.Value == 0)
                labelFungus.Text = "Standard";
            else if (trackBarFungus.Value == 1)
                labelFungus.Text = "Easy - Double fungus removal speed, Fungicide Tanks ability unlocked, unlock build roads on fungus and ease fungus movement.";
            else if (trackBarFungus.Value == 2)
                labelFungus.Text = "Very Easy = Remove fungus 1 turn, disable fungicide tanks, unlock build roads on fungus and ease fungus movement.";
        }

        private void TrackBarPopulationLimits_Scroll(object sender, EventArgs e)
        {
            if (trackBarPopulationLimits.Value == 0)
                labelPopulationLimits.Text = "Standard";
            else if (trackBarPopulationLimits.Value == 1)
                labelPopulationLimits.Text = "Relaxed - Population limit without Hab Complex: 11, without Habitation Dome: 18.  Both are half cost.  Hab Complex unlocked, Habitation Dome prerequisite: Industrial Automation."; //TODO update this text
            else if (trackBarPopulationLimits.Value == 2)
                labelPopulationLimits.Text = "Unlimited - Hab Complex and Habitation Dome disabled."; //TODO update this text
        }

        private void TrackBarHugePlanetSize_Scroll(object sender, EventArgs e)
        {
            if(trackBarHugePlanetSize.Value == 0)
                labelHugePlanetSize.Text = "Default \n(128 x 64)"; // 8,192
            else if(trackBarHugePlanetSize.Value == 1)
                labelHugePlanetSize.Text = "x1.5 \n(128 x 96)"; // 12,288
            else if (trackBarHugePlanetSize.Value == 2)
                labelHugePlanetSize.Text = "x2 \n(128 x 128)"; // 16,384
            else if (trackBarHugePlanetSize.Value == 3)
                labelHugePlanetSize.Text = "x4 \n(256 x 128)"; // 32,768
            else if (trackBarHugePlanetSize.Value == 4)
                labelHugePlanetSize.Text = "x8 \n(256 x 256)"; // 65,536
        }

        private void OpenGameFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SystemUtils.openGameFolderInExplorer();
        }

        private void SearchForGameInstallationFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SystemUtils.findGameFolder())
                MessageBox.Show("Game folder found: \n" + Properties.Settings.Default.GameFolder
                    + "\n\nYou can change it in the Tools menu.");
            labelGameFolder.Text = Properties.Settings.Default.GameFolder;
            labelGameFolder.ForeColor = Color.Black;
        }

        private void SetGameInstallationFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SystemUtils.setGameFolderDialog();
            labelGameFolder.Text = Properties.Settings.Default.GameFolder;
            if(String.Equals(labelGameFolder.Text, "Not found"))
                labelGameFolder.ForeColor = Color.Red;
            else
                labelGameFolder.ForeColor = Color.Black;
        }

        private void HurryCostCalculatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormHurryCalculator formHurryCalculator = new FormHurryCalculator();
            formHurryCalculator.Show();
        }

        private void TrackBarTechDiscoveryRate_Scroll(object sender, EventArgs e)
        {
            if(trackBarTechDiscoveryRate.Value == 0)
                labelTechDiscoveryRate.Text = "x0";
            else if(trackBarTechDiscoveryRate.Value == 1)
                labelTechDiscoveryRate.Text = "x0.1";
            else if(trackBarTechDiscoveryRate.Value == 2)
                labelTechDiscoveryRate.Text = "x0.5";
            else if(trackBarTechDiscoveryRate.Value == 3)
                labelTechDiscoveryRate.Text = "x0.75";
            else if(trackBarTechDiscoveryRate.Value == 4)
                labelTechDiscoveryRate.Text = "Standard";
            else if(trackBarTechDiscoveryRate.Value == 5)
                labelTechDiscoveryRate.Text = "x1.25";
            else if(trackBarTechDiscoveryRate.Value == 6)
                labelTechDiscoveryRate.Text = "x1.5";
            else if(trackBarTechDiscoveryRate.Value == 7)
                labelTechDiscoveryRate.Text = "x2";
            else if(trackBarTechDiscoveryRate.Value == 8)
                labelTechDiscoveryRate.Text = "x5";
            else if(trackBarTechDiscoveryRate.Value == 9)
                labelTechDiscoveryRate.Text = "x10";
        }

        private void Form1_ResizeBegin(object sender, EventArgs e)
        {
            this.SuspendLayout();
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            this.ResumeLayout(true);
        }

        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // open the settings/options screen
            OptionsScreen optionsScreen = new OptionsScreen();
            optionsScreen.Show();
        }

        // disable probe teams sound effect
        private void CheckBoxDisableProbeTeams_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
                SystemUtils.playSound(Sounds.PROBE_TEAM_COMPROMISED);
        }

        // when the app is closed, save the settings
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        // iterate thru controls for each tab.  depending on the control type, 
        // save the control's name and value to the json string
        private String buildPresetJson()
        {
            StringBuilder presetJsonBuilder = new StringBuilder();
            
            // append { to start the json file
            presetJsonBuilder.Append(@"{
""presetName"" : ""todo - set preset name""");

            // iterate thru each tab and it's controls
            foreach(TabPage tabPage in this.tabControl.Controls)
            {
                foreach(Control control in tabPage.Controls)
                {
                    if((control is TrackBar) || (control is CheckBox) || (control is NumericUpDown)){
                        presetJsonBuilder.Append(@",
""" + control.Name);
                        presetJsonBuilder.Append("\": \"" + getControlStateString(control) + "\"");
                    }
                }
            }


            // append } to end the json file
            presetJsonBuilder.Append(@",
""appVersion"" : ""todo - set app version""
}");
            return presetJsonBuilder.ToString();
        }

        // provide a control, get it's state as a string for use in json
        private string getControlStateString(Control control)
        {
            if (control is TrackBar)
            {
                return ((TrackBar)control).Value.ToString();
            }
            else if (control is CheckBox)
            {
                // first letter for bools is capitalized, json wants all lowercase
                return ((CheckBox)control).Checked.ToString();
            }
            else if (control is NumericUpDown)
            {
                return ((NumericUpDown)control).Value.ToString();
            }
            return "";
        }

        private void loadPresetToControls(String presetName)
        {
            String jsonString = DataUtils.getPresetAsJsonString(presetName);
            JObject jsonObject = JObject.Parse(jsonString);

            // iterate thru each tab and it's controls
            foreach(TabPage tabPage in this.tabControl.Controls)
            {
                foreach(Control control in tabPage.Controls)
                {
                    // if the control is one we need, look up its value in the json
                    // using the control's name as the key.  Set the value to the control
                    if((control is TrackBar) || (control is CheckBox) || (control is NumericUpDown)){
                        String controlState = (String)jsonObject[control.Name];
                        setControlState(control, controlState);
                    }
                }
            }
        }

        // parse the String value from the json to the appropriate type for the control
        // and set it to the control
        private void setControlState(Control control, String state)
        {
            if (control is TrackBar)
            {
                ((TrackBar)control).Value = Int32.Parse(state);
            }
            else if (control is CheckBox)
            {
                ((CheckBox)control).Checked = Boolean.Parse(state);
            }
            else if (control is NumericUpDown)
            {
                ((NumericUpDown)control).Value = Int32.Parse(state);
            }
        }

        private void ButtonSavePreset_Click(object sender, EventArgs e)
        {
            DataUtils.savePresetJsonFile(buildPresetJson(), comboBoxPresets.Text);
            refreshPresetsDropdown();
        }

        private void ButtonLoadPreset_Click(object sender, EventArgs e)
        {
            loadPresetToControls(comboBoxPresets.Text);
            refreshPresetsDropdown();
        }

        private void refreshPresetsDropdown()
        {
            comboBoxPresets.Items.Clear();
            List<String> presetsList = DataUtils.getPresetsList();
            foreach (String presetName in presetsList)
            {
                comboBoxPresets.Items.Add(presetName);
            }
        }
    }
}
