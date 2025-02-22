using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WeaponsCreator : MonoBehaviour
{
    [SerializeField] public string weaponName;
    [SerializeField] public WeaponType weaponType;
    [SerializeField] public WeaponTraitType weaponTrait;
    [SerializeField] public DamageType damageType;
    [SerializeField] public SpecialDamageType specialDamage;
    [SerializeField] public AttackTimingType attackTiming;
    [SerializeField] public int contactFrame;
    [SerializeField] public uint durabilityPercentage;
    [SerializeField] public Sprite spriteRef;

    [SerializeField] public int allWeaponsCount;

    [SerializeField] public List<string> weaponsStorageNames;

    [SerializeField] public List<WeaponData> allWeapons;
    [SerializeField] public WeaponData currentWeapon;

    public WeaponData GetCurrentWeapon()
    {
        return currentWeapon;
    }

    public List<WeaponData> GetAllWeapons()
    {
        return allWeapons;
    }

    public void ClearWeapons()
    {
        if (allWeapons == null)
        {
            weaponsStorageNames.Clear();
            return;
        }

        allWeapons.Clear();
        weaponsStorageNames.Clear();
        allWeaponsCount = allWeapons.Count;
    }

    public void AddWeapon()
    {
        if (allWeapons == null)
        {
            allWeapons = new List<WeaponData>();
        }
        allWeaponsCount = allWeapons.Count;

        for (int i = 0; i < allWeapons.Count; ++i)
        {
            if (allWeapons[i].GetWeaponName().Equals(weaponName))
            {
                return;
            }
        }

        WeaponData weaponData = new WeaponData();

        weaponData.SetWeaponName(weaponName);
        weaponData.SetWeaponType(weaponType);
        weaponData.AddWeaponTrait(weaponTrait);
        weaponData.SetDamageType(damageType);
        weaponData.AddSpecialDamageType(specialDamage);
        weaponData.AddAttackTiming(attackTiming);
        weaponData.AddPointOfContactFrames(contactFrame);
        weaponData.SetWeaponDurability(durabilityPercentage);

        allWeapons.Add(weaponData);
        allWeaponsCount = allWeapons.Count;
        currentWeapon = weaponData;
    }

    public void RemoveWeapon()
    {
        if (allWeapons == null)
        {
            return;
        }

        for (int i = 0; i < allWeapons.Count; ++i)
        {
            if (allWeapons[i].GetWeaponName().Equals(weaponName))
            {
                allWeapons.RemoveAt(i);
                break;
            }
        }
        allWeaponsCount = allWeapons.Count;
    }

    public void AddAdditionalTraitToCurrentWeapon()
    {
        if (allWeapons == null)
        {
            return;
        }

        for (int i = 0; i < allWeapons.Count; ++i)
        {
            if (allWeapons[i].GetWeaponName().Equals(weaponName))
            {
                allWeapons[i].AddWeaponTrait(weaponTrait);
                break;
            }
        }
        allWeaponsCount = allWeapons.Count;
    }

    public void AddAdditionalSpecialDamageToCurrentWeapon()
    {
        if (allWeapons == null)
        {
            return;
        }

        for (int i = 0; i < allWeapons.Count; ++i)
        {
            if (allWeapons[i].GetWeaponName().Equals(weaponName))
            {
                allWeapons[i].AddSpecialDamageType(specialDamage);
                break;
            }
        }
        allWeaponsCount = allWeapons.Count;
    }

    public void AddAdditionalAttackTimingToCurrentWeapon()
    {
        if (allWeapons == null)
        {
            return;
        }

        for (int i = 0; i < allWeapons.Count; ++i)
        {
            if (allWeapons[i].GetWeaponName().Equals(weaponName))
            {
                allWeapons[i].AddAttackTiming(attackTiming);
                break;
            }
        }
        allWeaponsCount = allWeapons.Count;
    }

    public void AddAdditionalContactFrameToCurrentWeapon()
    {
        if (allWeapons == null)
        {
            return;
        }

        for (int i = 0; i < allWeapons.Count; ++i)
        {
            if (allWeapons[i].GetWeaponName().Equals(weaponName))
            {
                allWeapons[i].AddPointOfContactFrames(contactFrame);
                break;
            }
        }
        allWeaponsCount = allWeapons.Count;
    }

    public void SetNewDurabilityToCurrentWeapon()
    {
        if (allWeapons == null)
        {
            return;
        }

        for (int i = 0; i < allWeapons.Count; ++i)
        {
            if (allWeapons[i].GetWeaponName().Equals(weaponName))
            {
                allWeapons[i].SetWeaponDurability(durabilityPercentage);
                break;
            }
        }
        allWeaponsCount = allWeapons.Count;
    }

    public void SetNewSpriteToCurrentWeapon()
    {
        if (allWeapons == null)
        {
            return;
        }

        for (int i = 0; i < allWeapons.Count; ++i)
        {
            if (allWeapons[i].GetWeaponName().Equals(weaponName))
            {
                allWeapons[i].SetWeaponSpriteInfo(spriteRef);
                break;
            }
        }
        allWeaponsCount = allWeapons.Count;
    }

    public void SaveWeapon()
    {
        if (allWeapons == null)
        {
            allWeapons = new List<WeaponData>();
        }
        allWeaponsCount = allWeapons.Count;

        string dirPath = FilePaths.GetFullWeaponsPath();
        Directory.CreateDirectory(dirPath);

        for (int i = 0; i < allWeapons.Count; ++i)
        {
            if (weaponName.Equals(allWeapons[i].GetWeaponName()))
            {
                string fullWeaponPath = Path.Combine(dirPath, weaponName);
                SaveAndLoadFile.SerializeObject<WeaponData>(allWeapons[i], fullWeaponPath);

                //Dump Saves!!
                string dirDumpPath = FilePaths.GetFullWeaponDumpsPath();
                string dumpFileName = weaponName + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");

                Directory.CreateDirectory(dirDumpPath);

                string fullDumpPath = Path.Combine(dirDumpPath, dumpFileName);
                SaveAndLoadFile.SerializeObject<WeaponData>(allWeapons[i], fullDumpPath);
                break;
            }
        }
    }

    public void SaveAllWeapons()
    {
        if (allWeapons == null)
        {
            allWeapons = new List<WeaponData>();
        }
        allWeaponsCount = allWeapons.Count;

        string dirPath = FilePaths.GetFullWeaponsPath();
        Directory.CreateDirectory(dirPath);

        for (int i = 0; i < allWeapons.Count; ++i)
        {
            string weaponFileName = allWeapons[i].GetWeaponName();
            string fullWeaponPath = Path.Combine(dirPath, weaponFileName);
            SaveAndLoadFile.SerializeObject<WeaponData>(allWeapons[i], fullWeaponPath);

            //Dump Saves!!
            string dirDumpPath = FilePaths.GetFullWeaponDumpsPath();
            string dumpFileName = weaponName + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");

            Directory.CreateDirectory(dirDumpPath);

            string fullDumpPath = Path.Combine(dirDumpPath, dumpFileName);
            SaveAndLoadFile.SerializeObject<WeaponData>(allWeapons[i], fullDumpPath);
        }
    }

    public void LoadWeapon()
    {
        if (allWeapons == null)
        {
            allWeapons = new List<WeaponData>();
        }
        allWeaponsCount = allWeapons.Count;


        string dirPath = "Weapons";
        Directory.CreateDirectory(dirPath);

        string[] allFiles = Directory.GetFiles(dirPath);

        for (int i = 0; i < allFiles.Length; ++i)
        {
            if (allFiles[i].Equals(weaponName))
            {
                currentWeapon = SaveAndLoadFile.DeSerializeObject<WeaponData>(allFiles[i]);
                allWeapons.Add(currentWeapon);
                break;
            }
        }
        allWeaponsCount = allWeapons.Count;
    }

    public WeaponData GetWeaponByName(string wpnName)
    {
        if (allWeapons == null)
        {
            LoadAllWeapons();
        }

        for (int i = 0; i < allWeapons.Count; ++i)
        {
            if (allWeapons[i].GetWeaponName().Equals(wpnName))
            {
                return allWeapons[i];
            }
        }

        return null;
    }

    public void LoadAllWeapons()
    {
        if (allWeapons == null)
        {
            allWeapons = new List<WeaponData>();
        }

        allWeapons.Clear();

        string dirPath = FilePaths.GetFullWeaponsPath();
        Directory.CreateDirectory(dirPath);
        
        string[] allFiles = Directory.GetFiles(dirPath);

        for (int i = 0; i < allFiles.Length; ++i)
        {
            allWeapons.Add(SaveAndLoadFile.DeSerializeObject<WeaponData>(allFiles[i]));
        }
        allWeaponsCount = allWeapons.Count;

        if (weaponsStorageNames == null)
        {
            weaponsStorageNames = new List<string>();
        }

        weaponsStorageNames.Clear();

        foreach (WeaponData weaponData in allWeapons)
        {
            weaponsStorageNames.Add(weaponData.GetWeaponName());
        }
    }
}
