﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace WinFormsApp1
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            SetupDefaults();
        }

        public void SetupDefaults()
        {
            GameTypeComboBox.SelectedIndex = 0;
            CastleDamageModeComboBox.SelectedIndex = 0;
            SiegeWeaponHealthComboBox.SelectedIndex = 2;
            PlayerDamageModeComboBox.SelectedIndex = 0;
            CastleHeartDamageModeComboBox.SelectedIndex = 0;
            PvPProtectionModeComboBox.SelectedIndex = 2;
            DeathContainerPermissionComboBox.SelectedIndex = 0;
            RelicSpawnTypeComboBox.SelectedIndex = 0;
            TimeZoneComboBox.SelectedIndex = 0;            
        }

        private void SaveSettings()
        {
            bool CanLootEnemyContainersBool = true;
            bool BloodBoundEquipmentBool = true;
            bool TeleportBoundItemsBool = true;
            bool AllowGlobalChatBool = true;
            bool AllWaypointsUnlockedBool = false;
            bool FreeCastleClaimBool = false;
            bool FreeCastleDestroyBool = false;
            bool InactivityKillEnabledBool = true;
            bool DisableDisconnectedDeadEnabledBool = true;
            bool AnnounceSiegeWeaponSpawnBool = true;
            bool ShowSiegeWeaponMapIconBool = true;

            if (CanLootEnemyContainersRadioFalse.Checked)
            {
                CanLootEnemyContainersBool = false;
            }
            if (BloodBoundEquipmentRadioFalse.Checked)
            {
                BloodBoundEquipmentBool = false;
            }
            if (TeleportBoundItemsRadioFalse.Checked)
            {
                TeleportBoundItemsBool = false;
            }
            if (AllowGlobalChatRadioFalse.Checked)
            {
                AllowGlobalChatBool = false;
            }
            if (AllWaypointsUnlockedRadioTrue.Checked)
            {
                AllWaypointsUnlockedBool = true;
            }
            if (FreeCastleClaimRadioTrue.Checked)
            {
                FreeCastleClaimBool = true;
            }
            if (FreeCastleDestroyRadioTrue.Checked)
            {
                FreeCastleDestroyBool = true;
            }
            if (InactivityKillEnabledRadioFalse.Checked)
            {
                InactivityKillEnabledBool = false;
            }
            if (DisableDisconnectedDeadEnabledRadioFalse.Checked)
            {
                DisableDisconnectedDeadEnabledBool = false;
            }
            if (AnnounceSiegeWeaponSpawnRadioFalse.Checked)
            {
                AnnounceSiegeWeaponSpawnBool = false;
            }
            if (ShowSiegeWeaponMapIconRadioFalse.Checked)
            {
                ShowSiegeWeaponMapIconBool = false;
            }

            RootSettings main = new RootSettings()
            {
                GameModeType = GameTypeComboBox.SelectedIndex,
                CastleDamageMode = CastleDamageModeComboBox.SelectedIndex,
                SiegeWeaponHealth = SiegeWeaponHealthComboBox.SelectedIndex,
                PlayerDamageMode = PlayerDamageModeComboBox.SelectedIndex,
                CastleHeartDamageMode = CastleHeartDamageModeComboBox.SelectedIndex,
                PvPProtectionMode = PvPProtectionModeComboBox.SelectedIndex,
                DeathContainerPermission = DeathContainerPermissionComboBox.SelectedIndex,
                RelicSpawnType = RelicSpawnTypeComboBox.SelectedIndex,
                CanLootEnemyContainers = CanLootEnemyContainersBool,
                BloodBoundEquipment = BloodBoundEquipmentBool,
                TeleportBoundItems = TeleportBoundItemsBool,
                AllowGlobalChat = AllowGlobalChatBool,
                AllWaypointsUnlocked = AllWaypointsUnlockedBool,
                FreeCastleClaim = FreeCastleClaimBool,
                FreeCastleDestroy = FreeCastleDestroyBool,
                InactivityKillEnabled = InactivityKillEnabledBool,
                InactivityKillTimeMin = decimal.ToInt32(InactivityKillTimeMinNumber.Value),
                InactivityKillTimeMax = decimal.ToInt32(InactivityKillTimeMaxNumber.Value),
                InactivityKillSafeTimeAddition = decimal.ToInt32(InactivityKillSafeTimeAdditionNumber.Value),
                InactivityKillTimerMaxItemLevel = decimal.ToInt32(InactivityKillTimerMaxItemLevelNumber.Value),
                DisableDisconnectedDeadEnabled = DisableDisconnectedDeadEnabledBool,
                DisableDisconnectedDeadTimer = decimal.ToInt32(DisableDisconnectedDeadEnabledNumber.Value),
                InventoryStacksModifier = decimal.ToDouble(InventoryStacksModifierNumber.Value),
                DropTableModifier_General = decimal.ToDouble(DropTableModifier_GeneralNumber.Value),
                DropTableModifier_Missions = decimal.ToDouble(DropTableModifier_MissionsNumber.Value),
                MaterialYieldModifier_Global = decimal.ToDouble(MaterialYieldModifier_GlobalNumber.Value),
                BloodEssenceYieldModifier = decimal.ToDouble(BloodEssenceYieldModifierNumber.Value),
                JournalVBloodSourceUnitMaxDistance = decimal.ToDouble(JournalVBloodSourceUnitMaxDistanceNumber.Value),
                PvPVampireRespawnModifier = decimal.ToDouble(PvPVampireRespawnModifierNumber.Value),
                CastleMinimumDistanceInFloors = decimal.ToInt32(CastleMinimumDistanceInFloorsNumber.Value),
                ClanSize = decimal.ToInt32(ClanSizeNumber.Value),
                BloodDrainModifier = decimal.ToDouble(BloodDrainModifierNumber.Value),
                DurabilityDrainModifier = decimal.ToDouble(DurabilityDrainModifierNumber.Value),
                GarlicAreaStrengthModifier = decimal.ToDouble(GarlicAreaStrengthModifierNumber.Value),
                HolyAreaStrengthModifier = decimal.ToDouble(HolyAreaStrengthModifierNumber.Value),
                SilverStrengthModifier = decimal.ToDouble(SilverStrengthModifierNumber.Value),
                SunDamageModifier = decimal.ToDouble(SunDamageModifierNumber.Value),
                CastleDecayRateModifier = decimal.ToDouble(CastleDecayRateModifierNumber.Value),
                CastleBloodEssenceDrainModifier = decimal.ToDouble(CastleBloodEssenceDrainModifierNumber.Value),
                CastleSiegeTimer = decimal.ToDouble(CastleSiegeTimerNumber.Value),
                CastleUnderAttackTimer = decimal.ToDouble(CastleUnderAttackTimerNumber.Value),
                AnnounceSiegeWeaponSpawn = AnnounceSiegeWeaponSpawnBool,
                ShowSiegeWeaponMapIcon = ShowSiegeWeaponMapIconBool,
                BuildCostModifier = decimal.ToDouble(BuildCostModifierNumber.Value),
                RecipeCostModifier = decimal.ToDouble(RecipeCostModifierNumber.Value),
                CraftRateModifier = decimal.ToDouble(CraftRateModifierNumber.Value),
                ResearchCostModifier = decimal.ToDouble(ResearchCostModifierNumber.Value),
                RefinementCostModifier = decimal.ToDouble(RefinementCostModifierNumber.Value),
                RefinementRateModifier = decimal.ToDouble(RefinementRateModifierNumber.Value),
                ResearchTimeModifier = decimal.ToDouble(ResearchTimeModifierNumber.Value),
                DismantleResourceModifier = decimal.ToDouble(DismantleResourceModifierNumber.Value),
                ServantConvertRateModifier = decimal.ToDouble(ServantConvertRateModifierNumber.Value),
                RepairCostModifier = decimal.ToDouble(RepairCostModifierNumber.Value),
                Death_DurabilityFactorLoss = decimal.ToDouble(Death_DurabilityFactorLossNumber.Value),
                Death_DurabilityLossFactorAsResources = decimal.ToDouble(Death_DurabilityLossFactorAsResourcesNumber.Value),
                StarterEquipmentId = 0,
                StarterResourcesId = 0,
                VBloodUnitSettings = new List<object>(),
                UnlockedAchievements = new List<object>(),
                UnlockedResearchs = new List<object>(),
                GameTimeModifiers = new GameTimeModifiers()
                {
                    DayDurationInSeconds = decimal.ToDouble(DayDurationInSecondsNumber.Value),
                    DayStartHour = decimal.ToInt16(DayStartHourNumber.Value),
                    DayStartMinute = decimal.ToInt16(DayStartMinuteNumber.Value),
                    DayEndHour = decimal.ToInt16(DayEndHourNumber.Value),
                    DayEndMinute = decimal.ToInt16(DayEndMinuteNumber.Value),
                    BloodMoonFrequency_Min = decimal.ToInt16(BloodMoonFrequency_MinNumber.Value),
                    BloodMoonFrequency_Max = decimal.ToInt16(BloodMoonFrequency_MaxNumber.Value),
                    BloodMoonBuff = decimal.ToDouble(BloodMoonBuffNumber.Value),
                },
                VampireStatModifiers = new VampireStatModifiers()
                {
                    MaxHealthModifier = decimal.ToDouble(VampireMaxHealthModifierNumber.Value),
                    MaxEnergyModifier = decimal.ToDouble(VampireMaxEnergyModifierNumber.Value),
                    PhysicalPowerModifier = decimal.ToDouble(VampirePhysicalPowerModifierNumber.Value),
                    SpellPowerModifier = decimal.ToDouble(VampireSpellPowerModifierNumber.Value),
                    ResourcePowerModifier = decimal.ToDouble(VampireResourcePowerModifierNumber.Value),
                    SiegePowerModifier = decimal.ToDouble(VampireSiegePowerModifierNumber.Value),
                    DamageReceivedModifier = decimal.ToDouble(VampireDamageReceivedModifierNumber.Value),
                    ReviveCancelDelay = decimal.ToDouble(VampireReviveCancelDelayNumber.Value)
                },
                UnitStatModifiers_Global = new UnitStatModifiersGlobal()
                {
                    MaxHealthModifier = decimal.ToDouble(MaxHealthModifier_GlobalNumber.Value),
                    PowerModifier = decimal.ToDouble(PowerModifier_GlobalNumber.Value)
                },
                UnitStatModifiers_VBlood = new UnitStatModifiersVBlood()
                {
                    MaxHealthModifier = decimal.ToDouble(MaxHealthModifier_VBloodNumber.Value),
                    PowerModifier = decimal.ToDouble(PowerModifier_VBloodNumber.Value)
                },
                EquipmentStatModifiers_Global = new EquipmentStatModifiersGlobal()
                {
                    MaxEnergyModifier = decimal.ToDouble(MaxEnergyModifier_EquipmentNumber.Value),
                    MaxHealthModifier = decimal.ToDouble(MaxHealthModifier_EquipmentNumber.Value),
                    ResourceYieldModifier = decimal.ToDouble(ResourceYieldModifier_EquipmentNumber.Value),
                    PhysicalPowerModifier = decimal.ToDouble(PhysicalPowerModifier_EquipmentNumber.Value),
                    SpellPowerModifier = decimal.ToDouble(SpellPowerModifier_EquipmentNumber.Value),
                    SiegePowerModifier = decimal.ToDouble(SiegePowerModifier_EquipmentNumber.Value),
                    MovementSpeedModifier = decimal.ToDouble(MovementSpeedModifier_EquipmentNumber.Value)
                },
                CastleStatModifiers_Global = new CastleStatModifiersGlobal()
                {
                    TickPeriod = decimal.ToDouble(TickPeriodNumber.Value),
                    DamageResistance = decimal.ToDouble(DamageResistanceNumber.Value),
                    SafetyBoxLimit = decimal.ToInt16(SafetyBoxLimitNumber.Value),
                    TombLimit = decimal.ToInt16(TombLimitNumber.Value),
                    VerminNestLimit = decimal.ToInt16(VerminNestLimitNumber.Value),
                    PylonPenalties = new PylonPenalties()
                    {
                        Range1 = new EmptyJSON(),
                        Range2 = new EmptyJSON(),
                        Range3 = new EmptyJSON(),
                        Range4 = new EmptyJSON(),
                        Range5 = new EmptyJSON()
                    },
                    FloorPenalties = new FloorPenalties()
                    {
                        Range1 = new EmptyJSON(),
                        Range2 = new EmptyJSON(),
                        Range3 = new EmptyJSON(),
                        Range4 = new EmptyJSON(),
                        Range5 = new EmptyJSON()
                    },
                    HeartLimits = new HeartLimits()
                    {
                        Level1 = new Level1()
                        {
                            Level = 1,
                            FloorLimit = decimal.ToInt16(FloorLimit1Number.Value),
                            ServantLimit = decimal.ToInt16(ServantLimit1Number.Value)
                        },
                        Level2 = new Level2()
                        {
                            Level = 2,
                            FloorLimit = decimal.ToInt16(FloorLimit2Number.Value),
                            ServantLimit = decimal.ToInt16(ServantLimit2Number.Value)
                        },
                        Level3 = new Level3()
                        {
                            Level = 3,
                            FloorLimit = decimal.ToInt16(FloorLimit3Number.Value),
                            ServantLimit = decimal.ToInt16(ServantLimit3Number.Value)
                        },
                        Level4 = new Level4()
                        {
                            Level = 4,
                            FloorLimit = decimal.ToInt16(FloorLimit4Number.Value),
                            ServantLimit = decimal.ToInt16(ServantLimit4Number.Value)
                        }
                    },
                    CastleLimit = decimal.ToInt16(CastleLimitNumber.Value)
                },
                PlayerInteractionSettings = new PlayerInteractionSettings()
                {
                    TimeZone = TimeZoneComboBox.SelectedIndex,
                    VSPlayerWeekdayTime = new VSPlayerWeekdayTime()
                    {
                        StartHour = decimal.ToInt16(StartHourNumber_VSPlayerWeekday.Value),
                        StartMinute = decimal.ToInt16(StartMinuteNumber_VSPlayerWeekday.Value),
                        EndHour = decimal.ToInt16(EndHourNumber_VSPlayerWeekday.Value),
                        EndMinute = decimal.ToInt16(EndMinuteNumber_VSPlayerWeekday.Value)
                    },
                    VSPlayerWeekendTime = new VSPlayerWeekendTime()
                    {
                        StartHour = decimal.ToInt16(StartHourNumber_VSPlayerWeekend.Value),
                        StartMinute = decimal.ToInt16(StartMinuteNumber_VSPlayerWeekend.Value),
                        EndHour = decimal.ToInt16(EndHourNumber_VSPlayerWeekend.Value),
                        EndMinute = decimal.ToInt16(EndMinuteNumber_VSPlayerWeekend.Value)
                    },
                    VSCastleWeekdayTime = new VSCastleWeekdayTime()
                    {
                        StartHour = decimal.ToInt16(StartHourNumber_VSCastleWeekday.Value),
                        StartMinute = decimal.ToInt16(StartMinuteNumber_VSCastleWeekday.Value),
                        EndHour = decimal.ToInt16(EndHourNumber_VSCastleWeekday.Value),
                        EndMinute = decimal.ToInt16(EndMinuteNumber_VSCastleWeekday.Value)
                    },
                    VSCastleWeekendTime = new VSCastleWeekendTime()
                    {
                        StartHour = decimal.ToInt16(StartHourNumber_VSCastleWeekend.Value),
                        StartMinute = decimal.ToInt16(StartMinuteNumber_VSCastleWeekend.Value),
                        EndHour = decimal.ToInt16(EndHourNumber_VSCastleWeekend.Value),
                        EndMinute = decimal.ToInt16(EndMinuteNumber_VSCastleWeekend.Value)
                    }
                }

            };
        string SettingsJSON = JsonConvert.SerializeObject(main, Formatting.Indented);
        SaveSettingsDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "Low" + "\\Stunlock Studios\\VRising";
        if (SaveSettingsDialog.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(SaveSettingsDialog.FileName))
                {
                    File.Copy(SaveSettingsDialog.FileName, SaveSettingsDialog.FileName + ".bak", true);
                }
                File.WriteAllText(SaveSettingsDialog.FileName, SettingsJSON);
                MessageBox.Show("File successfully saved to: \n" + SaveSettingsDialog.FileName);
            }

        }

        private void LoadSettings()
        {
            if(LoadSettingsDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader reader = new StreamReader(LoadSettingsDialog.FileName))
                {
                    string LoadedJSON = reader.ReadToEnd();
                    RootSettings LoadedSettings = JsonConvert.DeserializeObject<RootSettings>(LoadedJSON);
                    GameTypeComboBox.SelectedIndex = Convert.ToInt16(LoadedSettings.GameModeType);
                    CastleDamageModeComboBox.SelectedIndex = Convert.ToInt16(LoadedSettings.CastleDamageMode);
                    SiegeWeaponHealthComboBox.SelectedIndex = Convert.ToInt16(LoadedSettings.SiegeWeaponHealth);
                    PlayerDamageModeComboBox.SelectedIndex = Convert.ToInt16(LoadedSettings.PlayerDamageMode);
                    CastleHeartDamageModeComboBox.SelectedIndex = Convert.ToInt16(LoadedSettings.CastleHeartDamageMode);
                    PvPProtectionModeComboBox.SelectedIndex = Convert.ToInt16(LoadedSettings.PvPProtectionMode);
                    DeathContainerPermissionComboBox.SelectedIndex = Convert.ToInt16(LoadedSettings.DeathContainerPermission);
                    RelicSpawnTypeComboBox.SelectedIndex = Convert.ToInt16(LoadedSettings.RelicSpawnType);
                    if (LoadedSettings.CanLootEnemyContainers == true)
                    {
                        CanLootEnemyContainersRadioTrue.Checked = true;
                    }
                    if (LoadedSettings.BloodBoundEquipment == true)
                    {
                        BloodBoundEquipmentRadioTrue.Checked = true;
                    }
                    if (LoadedSettings.TeleportBoundItems == true)
                    {
                        TeleportBoundItemsRadioTrue.Checked = true;
                    }
                    if (LoadedSettings.AllowGlobalChat == true)
                    {
                        AllowGlobalChatRadioTrue.Checked = true;
                    }
                    if (LoadedSettings.AllWaypointsUnlocked == true)
                    {
                        AllWaypointsUnlockedRadioTrue.Checked = true;
                    }
                    if (LoadedSettings.FreeCastleClaim == true)
                    {
                        FreeCastleClaimRadioTrue.Checked = true;
                    }
                    if (LoadedSettings.FreeCastleDestroy == true)
                    {
                        FreeCastleClaimRadioFalse.Checked = true;
                    }
                    if (LoadedSettings.InactivityKillEnabled == true)
                    {
                        InactivityKillEnabledRadioTrue.Checked = true;
                    }
                    InactivityKillTimeMinNumber.Value = Convert.ToInt32(LoadedSettings.InactivityKillTimeMin);
                    InactivityKillTimeMaxNumber.Value = Convert.ToInt32(LoadedSettings.InactivityKillTimeMax);
                    InactivityKillSafeTimeAdditionNumber.Value = Convert.ToInt32(LoadedSettings.InactivityKillSafeTimeAddition);
                    InactivityKillTimerMaxItemLevelNumber.Value = Convert.ToInt16(LoadedSettings.InactivityKillTimerMaxItemLevel);
                    if (LoadedSettings.DisableDisconnectedDeadEnabled == true)
                    {
                        DisableDisconnectedDeadEnabledRadioTrue.Checked = true;
                    }
                    DisableDisconnectedDeadEnabledNumber.Value = Convert.ToInt16(LoadedSettings.DisableDisconnectedDeadTimer);
                    InventoryStacksModifierNumber.Value = Convert.ToDecimal(LoadedSettings.InventoryStacksModifier);
                    DropTableModifier_GeneralNumber.Value = Convert.ToDecimal(LoadedSettings.DropTableModifier_General);
                    DropTableModifier_MissionsNumber.Value = Convert.ToDecimal(LoadedSettings.DropTableModifier_Missions);
                    MaterialYieldModifier_GlobalNumber.Value = Convert.ToDecimal(LoadedSettings.MaterialYieldModifier_Global);
                    BloodEssenceYieldModifierNumber.Value = Convert.ToDecimal(LoadedSettings.BloodEssenceYieldModifier);
                    JournalVBloodSourceUnitMaxDistanceNumber.Value = Convert.ToDecimal(LoadedSettings.JournalVBloodSourceUnitMaxDistance);
                    PvPVampireRespawnModifierNumber.Value = Convert.ToDecimal(LoadedSettings.PvPVampireRespawnModifier);
                    CastleMinimumDistanceInFloorsNumber.Value = Convert.ToInt16(LoadedSettings.CastleMinimumDistanceInFloors);
                    ClanSizeNumber.Value = Convert.ToInt16(LoadedSettings.ClanSize);
                    BloodDrainModifierNumber.Value = Convert.ToDecimal(LoadedSettings.BloodDrainModifier);
                    DurabilityDrainModifierNumber.Value = Convert.ToDecimal(LoadedSettings.DurabilityDrainModifier);
                    GarlicAreaStrengthModifierNumber.Value = Convert.ToDecimal(LoadedSettings.GarlicAreaStrengthModifier);
                    HolyAreaStrengthModifierNumber.Value = Convert.ToDecimal(LoadedSettings.HolyAreaStrengthModifier);
                    SilverStrengthModifierNumber.Value = Convert.ToDecimal(LoadedSettings.SilverStrengthModifier);
                    SunDamageModifierNumber.Value = Convert.ToDecimal(LoadedSettings.SunDamageModifier);
                    CastleDecayRateModifierNumber.Value = Convert.ToDecimal(LoadedSettings.CastleDecayRateModifier);
                    CastleBloodEssenceDrainModifierNumber.Value = Convert.ToDecimal(LoadedSettings.CastleBloodEssenceDrainModifier);
                    CastleSiegeTimerNumber.Value = Convert.ToDecimal(LoadedSettings.CastleSiegeTimer);
                    CastleUnderAttackTimerNumber.Value = Convert.ToDecimal(LoadedSettings.CastleUnderAttackTimer);
                    if (LoadedSettings.AnnounceSiegeWeaponSpawn == true)
                    {
                        AnnounceSiegeWeaponSpawnRadioTrue.Checked = true;
                    }
                    if (LoadedSettings.ShowSiegeWeaponMapIcon == true)
                    {
                        ShowSiegeWeaponMapIconRadioTrue.Checked = true;
                    }
                    BuildCostModifierNumber.Value = Convert.ToDecimal(LoadedSettings.BuildCostModifier);
                    RecipeCostModifierNumber.Value = Convert.ToDecimal(LoadedSettings.RecipeCostModifier);
                    CraftRateModifierNumber.Value = Convert.ToDecimal(LoadedSettings.CraftRateModifier);
                    ResearchCostModifierNumber.Value = Convert.ToDecimal(LoadedSettings.ResearchCostModifier);
                    RefinementCostModifierNumber.Value = Convert.ToDecimal(LoadedSettings.RefinementCostModifier);
                    RefinementRateModifierNumber.Value = Convert.ToDecimal(LoadedSettings.RefinementRateModifier);
                    ResearchTimeModifierNumber.Value = Convert.ToDecimal(LoadedSettings.ResearchTimeModifier);
                    DismantleResourceModifierNumber.Value = Convert.ToDecimal(LoadedSettings.DismantleResourceModifier);
                    ServantConvertRateModifierNumber.Value = Convert.ToDecimal(LoadedSettings.ServantConvertRateModifier);
                    RepairCostModifierNumber.Value = Convert.ToDecimal(LoadedSettings.RepairCostModifier);
                    Death_DurabilityFactorLossNumber.Value = Convert.ToDecimal(LoadedSettings.Death_DurabilityFactorLoss);
                    Death_DurabilityLossFactorAsResourcesNumber.Value = Convert.ToDecimal(LoadedSettings.Death_DurabilityLossFactorAsResources);
                    DayDurationInSecondsNumber.Value = Convert.ToDecimal(LoadedSettings.GameTimeModifiers.DayDurationInSeconds);
                    DayStartHourNumber.Value = Convert.ToDecimal(LoadedSettings.GameTimeModifiers.DayStartHour);
                    DayStartMinuteNumber.Value = Convert.ToDecimal(LoadedSettings.GameTimeModifiers.DayStartMinute);
                    DayEndHourNumber.Value = Convert.ToDecimal(LoadedSettings.GameTimeModifiers.DayEndHour);
                    DayEndMinuteNumber.Value = Convert.ToDecimal(LoadedSettings.GameTimeModifiers.DayEndMinute);
                    BloodMoonFrequency_MinNumber.Value = Convert.ToDecimal(LoadedSettings.GameTimeModifiers.BloodMoonFrequency_Min);
                    BloodMoonFrequency_MaxNumber.Value = Convert.ToDecimal(LoadedSettings.GameTimeModifiers.BloodMoonFrequency_Max);
                    BloodMoonBuffNumber.Value = Convert.ToDecimal(LoadedSettings.GameTimeModifiers.BloodMoonBuff);
                    VampireMaxHealthModifierNumber.Value = Convert.ToDecimal(LoadedSettings.VampireStatModifiers.MaxHealthModifier);
                    VampireMaxEnergyModifierNumber.Value = Convert.ToDecimal(LoadedSettings.VampireStatModifiers.MaxEnergyModifier);
                    VampirePhysicalPowerModifierNumber.Value = Convert.ToDecimal(LoadedSettings.VampireStatModifiers.PhysicalPowerModifier);
                    VampireSpellPowerModifierNumber.Value = Convert.ToDecimal(LoadedSettings.VampireStatModifiers.SpellPowerModifier);
                    VampireResourcePowerModifierNumber.Value = Convert.ToDecimal(LoadedSettings.VampireStatModifiers.ResourcePowerModifier);
                    VampireSiegePowerModifierNumber.Value = Convert.ToDecimal(LoadedSettings.VampireStatModifiers.SiegePowerModifier);
                    VampireDamageReceivedModifierNumber.Value = Convert.ToDecimal(LoadedSettings.VampireStatModifiers.DamageReceivedModifier);
                    VampireReviveCancelDelayNumber.Value = Convert.ToDecimal(LoadedSettings.VampireStatModifiers.ReviveCancelDelay);
                    MaxHealthModifier_GlobalNumber.Value = Convert.ToDecimal(LoadedSettings.UnitStatModifiers_Global.MaxHealthModifier);
                    PowerModifier_GlobalNumber.Value = Convert.ToDecimal(LoadedSettings.UnitStatModifiers_Global.PowerModifier);
                    MaxHealthModifier_VBloodNumber.Value = Convert.ToDecimal(LoadedSettings.UnitStatModifiers_VBlood.MaxHealthModifier);
                    PowerModifier_VBloodNumber.Value = Convert.ToDecimal(LoadedSettings.UnitStatModifiers_VBlood.PowerModifier);
                    MaxEnergyModifier_EquipmentNumber.Value = Convert.ToDecimal(LoadedSettings.EquipmentStatModifiers_Global.MaxEnergyModifier);
                    MaxHealthModifier_EquipmentNumber.Value = Convert.ToDecimal(LoadedSettings.EquipmentStatModifiers_Global.MaxHealthModifier);
                    ResourceYieldModifier_EquipmentNumber.Value = Convert.ToDecimal(LoadedSettings.EquipmentStatModifiers_Global.ResourceYieldModifier);
                    PhysicalPowerModifier_EquipmentNumber.Value = Convert.ToDecimal(LoadedSettings.EquipmentStatModifiers_Global.PhysicalPowerModifier);
                    SpellPowerModifier_EquipmentNumber.Value = Convert.ToDecimal(LoadedSettings.EquipmentStatModifiers_Global.SpellPowerModifier);
                    SiegePowerModifier_EquipmentNumber.Value = Convert.ToDecimal(LoadedSettings.EquipmentStatModifiers_Global.SiegePowerModifier);
                    MovementSpeedModifier_EquipmentNumber.Value = Convert.ToDecimal(LoadedSettings.EquipmentStatModifiers_Global.MovementSpeedModifier);
                    TickPeriodNumber.Value = Convert.ToDecimal(LoadedSettings.CastleStatModifiers_Global.TickPeriod);
                    DamageResistanceNumber.Value = Convert.ToDecimal(LoadedSettings.CastleStatModifiers_Global.DamageResistance);
                    SafetyBoxLimitNumber.Value = Convert.ToInt16(LoadedSettings.CastleStatModifiers_Global.SafetyBoxLimit);
                    TombLimitNumber.Value = Convert.ToInt16(LoadedSettings.CastleStatModifiers_Global.TombLimit);
                    VerminNestLimitNumber.Value = Convert.ToInt16(LoadedSettings.CastleStatModifiers_Global.VerminNestLimit);
                    FloorLimit1Number.Value = Convert.ToInt16(LoadedSettings.CastleStatModifiers_Global.HeartLimits.Level1.FloorLimit);
                    ServantLimit1Number.Value = Convert.ToInt16(LoadedSettings.CastleStatModifiers_Global.HeartLimits.Level1.ServantLimit);
                    FloorLimit2Number.Value = Convert.ToInt16(LoadedSettings.CastleStatModifiers_Global.HeartLimits.Level2.FloorLimit);
                    ServantLimit2Number.Value = Convert.ToInt16(LoadedSettings.CastleStatModifiers_Global.HeartLimits.Level2.ServantLimit);
                    FloorLimit3Number.Value = Convert.ToInt16(LoadedSettings.CastleStatModifiers_Global.HeartLimits.Level3.FloorLimit);
                    ServantLimit3Number.Value = Convert.ToInt16(LoadedSettings.CastleStatModifiers_Global.HeartLimits.Level3.ServantLimit);
                    FloorLimit4Number.Value = Convert.ToInt16(LoadedSettings.CastleStatModifiers_Global.HeartLimits.Level4.FloorLimit);
                    ServantLimit4Number.Value = Convert.ToInt16(LoadedSettings.CastleStatModifiers_Global.HeartLimits.Level4.ServantLimit);
                    TimeZoneComboBox.SelectedIndex = Convert.ToInt16(LoadedSettings.PlayerInteractionSettings.TimeZone);
                    StartHourNumber_VSPlayerWeekday.Value = Convert.ToInt16(LoadedSettings.PlayerInteractionSettings.VSPlayerWeekdayTime.StartHour);
                    StartMinuteNumber_VSPlayerWeekday.Value = Convert.ToInt16(LoadedSettings.PlayerInteractionSettings.VSPlayerWeekdayTime.StartMinute);
                    EndHourNumber_VSPlayerWeekday.Value = Convert.ToInt16(LoadedSettings.PlayerInteractionSettings.VSPlayerWeekdayTime.EndHour);
                    EndMinuteNumber_VSPlayerWeekday.Value = Convert.ToInt16(LoadedSettings.PlayerInteractionSettings.VSPlayerWeekdayTime.EndMinute);
                    StartHourNumber_VSPlayerWeekend.Value = Convert.ToInt16(LoadedSettings.PlayerInteractionSettings.VSPlayerWeekendTime.StartHour);
                    StartMinuteNumber_VSPlayerWeekend.Value = Convert.ToInt16(LoadedSettings.PlayerInteractionSettings.VSPlayerWeekendTime.StartMinute);
                    EndHourNumber_VSPlayerWeekend.Value = Convert.ToInt16(LoadedSettings.PlayerInteractionSettings.VSPlayerWeekendTime.EndHour);
                    EndMinuteNumber_VSPlayerWeekend.Value = Convert.ToInt16(LoadedSettings.PlayerInteractionSettings.VSPlayerWeekendTime.EndMinute);
                    StartHourNumber_VSCastleWeekday.Value = Convert.ToInt16(LoadedSettings.PlayerInteractionSettings.VSCastleWeekdayTime.StartHour);
                    StartMinuteNumber_VSCastleWeekday.Value = Convert.ToInt16(LoadedSettings.PlayerInteractionSettings.VSCastleWeekdayTime.StartMinute);
                    EndHourNumber_VSCastleWeekday.Value = Convert.ToInt16(LoadedSettings.PlayerInteractionSettings.VSCastleWeekdayTime.EndHour);
                    EndMinuteNumber_VSCastleWeekday.Value = Convert.ToInt16(LoadedSettings.PlayerInteractionSettings.VSCastleWeekdayTime.EndMinute);
                    StartHourNumber_VSCastleWeekend.Value = Convert.ToInt16(LoadedSettings.PlayerInteractionSettings.VSCastleWeekendTime.StartHour);
                    StartMinuteNumber_VSCastleWeekend.Value = Convert.ToInt16(LoadedSettings.PlayerInteractionSettings.VSCastleWeekendTime.StartMinute);
                    EndHourNumber_VSCastleWeekend.Value = Convert.ToInt16(LoadedSettings.PlayerInteractionSettings.VSCastleWeekendTime.EndHour);
                    EndMinuteNumber_VSCastleWeekend.Value = Convert.ToInt16(LoadedSettings.PlayerInteractionSettings.VSCastleWeekendTime.EndMinute);
                }                
            }
        }


        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to quit?\nAll unsaved data will be lost.", "Confirmation", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                System.Windows.Forms.Application.Exit();
            }
        }

        private void InactivityKillEnabledRadioTrue_CheckedChanged(object sender, EventArgs e)
        {
            if (InactivityKillEnabledRadioTrue.Checked)
            {
                InactivityKillTimeMinNumber.ReadOnly = false;
                InactivityKillTimeMinNumber.Increment = 1;
            }
        }

        private void InactivityKillEnabledRadioFalse_CheckedChanged(object sender, EventArgs e)
        {
            if (InactivityKillEnabledRadioFalse.Checked)
            {
                InactivityKillTimeMinNumber.ReadOnly = true;
                InactivityKillTimeMinNumber.Increment = 0;
            }
        }

        private void DisableDisconnectedDeadEnabledRadioTrue_CheckedChanged(object sender, EventArgs e)
        {
            if (DisableDisconnectedDeadEnabledRadioTrue.Checked)
            {
                DisableDisconnectedDeadEnabledNumber.ReadOnly = false;
                DisableDisconnectedDeadEnabledNumber.Increment = 1;
            }
        }

        private void DisableDisconnectedDeadEnabledRadioFalse_CheckedChanged(object sender, EventArgs e)
        {
            if (DisableDisconnectedDeadEnabledRadioFalse.Checked)
            {
                DisableDisconnectedDeadEnabledNumber.ReadOnly = true;
                DisableDisconnectedDeadEnabledNumber.Increment = 0;
            }
        }

        private void saveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void loadFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadSettings();
        }
    }
}
