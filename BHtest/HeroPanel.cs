using System;
using System.Collections.Generic;
using System.Text;


public class HeroPanel
{
    public string heroInfoCsv;

    public HeroPanel(string info)
    {
        heroInfoCsv = info;
    }

    public Character GetHero()
    {

        Character hero;
        String[] values;

        values = heroInfoCsv.Split(',');

        hero = new Character()
        {
            power = Convert.ToInt32(values[0]),
            stamina = Convert.ToInt32(values[1]),
            agility = Convert.ToInt32(values[2]),
            critChance = Convert.ToSingle(values[3]),
            critDamage = Convert.ToSingle(values[4]),
            dsChance = Convert.ToSingle(values[5]),
            empowerChance = Convert.ToSingle(values[6]),
            blockChance = Convert.ToSingle(values[7]),
            evadeChance = Convert.ToSingle(values[8]),
            deflectChance = Convert.ToSingle(values[9]),
            absorbChance = Convert.ToSingle(values[10]),
            damageReduction = Convert.ToSingle(values[11]),
            powerRunes = Convert.ToSingle(values[12]),
            staminaRunes = Convert.ToSingle(values[13]),
            agilityRunes = Convert.ToSingle(values[14]),
            metaRune = GetMetaRuneFromString(values[15]),
            weapon = GetWeaponFromString(values[16]),
            petName = GetPetFromString(values[17]),
            PetLevel = GetPetLevelFromString(values[18]),
            petProcType = GetProcTypeFromString(values[19]),
            setArray = new Set[]
            {
                new Set(GetSetBonusFromString(values[20]), Convert.ToInt32(values[21])),
                new Set(GetSetBonusFromString(values[22]), Convert.ToInt32(values[23])),
                new Set(GetSetBonusFromString(values[24]), Convert.ToInt32(values[25]))
            },
            mythicArray = new MythicBonus[]
            {
                GetMythicBonusFromString(values[26]),
                GetMythicBonusFromString(values[27]),
                GetMythicBonusFromString(values[28]),
                GetMythicBonusFromString(values[29]),
                GetMythicBonusFromString(values[30]),
                GetMythicBonusFromString(values[31]),
            },
            gateKeeperBonus = Convert.ToBoolean(values[32]),
            _isHero = true
        };
        return hero;
    }
    public static SetBonus GetSetBonusFromString(String s)
    {
        SetBonus setBonus = SetBonus.None;
        switch (s)
        {
            case "AresBonus":
                setBonus = SetBonus.AresBonus;
                break;
            case "DivinityBonus":
                setBonus = SetBonus.DivinityBonus;
                break;
            case "MaruBonus":
                setBonus = SetBonus.MaruBonus;
                break;
            case "NWBonus":
                setBonus = SetBonus.NWBonus;
                break;

            //Trials
            case "UnityBonus":
                setBonus = SetBonus.UnityBonus;
                break;
            case "TrugdorBonus":
                setBonus = SetBonus.TrugdorBonus;
                break;
            case "BushidoBonus":
                setBonus = SetBonus.BushidoBonus;
                break;
            case "TaldBonus":
                setBonus = SetBonus.TaldBonus;
                break;
            case "ConducBonus":
                setBonus = SetBonus.ConducBonus;
                break;
            case "LuminaryBonus":
                setBonus = SetBonus.LuminaryBonus;
                break;

            //WB orlag
            case "Lunarbonus":
                setBonus = SetBonus.Lunarbonus;
                break;
            case "JynxBonus":
                setBonus = SetBonus.JynxBonus;
                break;
            case "OblitBonus":
                setBonus = SetBonus.OblitBonus;
                break;
            case "AgonyBonus":
                setBonus = SetBonus.AgonyBonus;
                break;

            //WB nether
            case "IllustriousBonus":
                setBonus = SetBonus.IllustriousBonus;
                break;
            case "TatersBonus":
                setBonus = SetBonus.TatersBonus;
                break;
            case "InfernoBonus":
                setBonus = SetBonus.InfernoBonus;
                break;
        }
        return setBonus;
    }

    public static MythicBonus GetMythicBonusFromString(String s)
    {
        MythicBonus mythicBonus = MythicBonus.None;

        switch (s)
        {

            case "Pewpew":
                mythicBonus = MythicBonus.Pewpew;
                break;
            case "Hysteria_not_Implemented":
                mythicBonus = MythicBonus.Hysteria_not_Implemented;
                break;
            case "Bub":
                mythicBonus = MythicBonus.Bub;
                break;
            case "Supersition":
                mythicBonus = MythicBonus.Supersition;
                break;
            case "NightVisage":
                mythicBonus = MythicBonus.NightVisage;
                break;
            case "Consumption":
                mythicBonus = MythicBonus.Consumption;
                break;
            case "Decay":
                mythicBonus = MythicBonus.Decay;
                break;
            case "Necrosis":
                mythicBonus = MythicBonus.Necrosis;
                break;
            case "Cometfell":
                mythicBonus = MythicBonus.Cometfell;
                break;
            case "Nebuleye_Not_Implemented":
                mythicBonus = MythicBonus.Nebuleye_Not_Implemented;
                break;
            case "HoodOfMenace":
                mythicBonus = MythicBonus.HoodOfMenace;
                break;
            case "CryptTunic":
                mythicBonus = MythicBonus.CryptTunic;
                break;
            case "FishNBarrel":
                mythicBonus = MythicBonus.FishNBarrel;
                break;
            case "EngulfintArtifact":
                mythicBonus = MythicBonus.EngulfintArtifact;
                break;
            case "Nemesis":
                mythicBonus = MythicBonus.Nemesis;
                break;
            case "Bedlam":
                mythicBonus = MythicBonus.Bedlam;
                break;
        }

        return mythicBonus;
    }

    public static Character.MetaRune GetMetaRuneFromString(String s)
    {
        Character.MetaRune metaRune;

        switch (s)
        {
            case "Redirect":
                metaRune = Character.MetaRune.Redirect;
                break;
            case "spRegen":
                metaRune = Character.MetaRune.spRegen;
                break;
            default:
                metaRune = Character.MetaRune.None;
                break;
        }

        return metaRune;
    }

    public static PetType GetPetFromString(String s)
    {
        PetType pet;

        switch (s)
        {
            case "Nelson":
                pet = PetType.Nelson;
                break;
            case "Gemmi":
                pet = PetType.Gemmi;
                break;
            case "Boogie":
                pet = PetType.Boogie;
                break;
            case "Nemo":
                pet = PetType.Nemo;
                break;
            case "Crem":
                pet = PetType.Crem;
                break;
            case "Boiguh":
                pet = PetType.Boiguh;
                break;
            case "Nerder":
                pet = PetType.Nerder;
                break;
            case "Quimby":
                pet = PetType.Quimby;
                break;
            case "Snut":
                pet = PetType.Snut;
                break;
            case "Wuvboi":
                pet = PetType.Wuvboi;
                break;
            case "Buvboi":
                pet = PetType.Buvboi;
                break;
            case "Skulldemort":
                pet = PetType.Skulldemort;
                break;
            case "Toebert":
                pet = PetType.Toebert;
                break;
            case "Urgoff":
                pet = PetType.Urgoff;
                break;
            case "Roogamenz":
                pet = PetType.Roogamenz;
                break;
            case "Fuvboi":
                pet = PetType.Fuvboi;
                break;
            case "Karlorr":
                pet = PetType.Karlorr;
                break;
            case "Pumkwim":
                pet = PetType.Pumkwim;
                break;
            case "EpicBoogie":
                pet = PetType.EpicBoogie;
                break;
            case "EpicNemo":
                pet = PetType.EpicNemo;
                break;
            case "EpicNerder":
                pet = PetType.EpicNerder;
                break;
            case "EpicPumkwim":
                pet = PetType.EpicPumkwim;
                break;
            case "EpicCrem":
                pet = PetType.EpicCrem;
                break;
            case "EpicSnut":
                pet = PetType.EpicSnut;
                break;
            case "Pritza":
                pet = PetType.Pritza;
                break;
            case "Sparklez":
                pet = PetType.Sparklez;
                break;
            case "Dug":
                pet = PetType.Dug;
                break;
            case "Gumgum":
                pet = PetType.Gumgum;
                break;
            case "Phony":
                pet = PetType.Phony;
                break;
            case "Waldo":
                pet = PetType.Waldo;
                break;
            case "Beanz":
                pet = PetType.Beanz;
                break;
            case "Rutledge":
                pet = PetType.Rutledge;
                break;
            case "Log":
                pet = PetType.Log;
                break;
            case "Melvin":
                pet = PetType.Melvin;
                break;
            case "Bryan":
                pet = PetType.Bryan;
                break;
            case "Nacl":
                pet = PetType.Nacl;
                break;
            case "Mewwo":
                pet = PetType.Mewwo;
                break;
            case "Gusty":
                pet = PetType.Gusty;
                break;
            case "Dot":
                pet = PetType.Dot;
                break;



            default:
                pet = PetType.None;
                break;
        }

        return pet;
    }

    public static PetProcType GetProcTypeFromString(String s)
    {
        PetProcType petProcType = PetProcType.AllType;
        switch (s)
        {
            case "PerHit":
                petProcType = PetProcType.PerHit;
                break;
            case "GetHit":
                petProcType = PetProcType.GetHit;
                break;
            case "PerTurn":
                petProcType = PetProcType.PerTurn;
                break;
            case "AllType":
                petProcType = PetProcType.AllType;
                break;
        }
        return petProcType;
    }

    public static int GetPetLevelFromString(String s)
    {
        return Int32.Parse(s);
    }

    public static Character.Weapon GetWeaponFromString(String s)
    {
        Character.Weapon weapon;

        switch (s)
        {
            case "Axe":
                weapon = Character.Weapon.Axe;
                break;
            case "Bow":
                weapon = Character.Weapon.Bow;
                break;
            case "Spear":
                weapon = Character.Weapon.Spear;
                break;
            case "Staff":
                weapon = Character.Weapon.Staff;
                break;
            case "Sword":
                weapon = Character.Weapon.Sword;
                break;
            case "Laser":
                weapon = Character.Weapon.Laser;
                break;
            case "DemonStaff":
                weapon = Character.Weapon.DemonStaff;
                break;
            case "Harvester":
                weapon = Character.Weapon.Harvester;
                break;
            case "ShieldStaff":
                weapon = Character.Weapon.ShieldStaff;
                break;
            case "YakBlade":
                weapon = Character.Weapon.YakBlade;
                break;
            default:
                weapon = Character.Weapon.None;
                break;
        }
        return weapon;
    }

}
