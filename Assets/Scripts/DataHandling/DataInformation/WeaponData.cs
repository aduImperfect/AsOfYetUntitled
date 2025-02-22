using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;
using UnityEngine;

public class WeaponData : IXmlSerializable
{
    [XmlAttribute("WeaponName")]
    string weaponName;

    [XmlAttribute("WeaponType")]
    WeaponType weaponType;

    [XmlAttribute("WeaponTraits")]
    List<WeaponTraitType> weaponTraits;

    [XmlAttribute("WeaponDamage")]
    DamageType damageType;

    [XmlAttribute("WeaponSpecialDamages")]
    List<SpecialDamageType> specialDamages;

    [XmlAttribute("WeaponAttackTimings")]
    List<AttackTimingType> attackTimings;

    [XmlAttribute("WeaponContactFrames")]
    List<int> pointOfContactFrames;

    [XmlAttribute("WeaponDurability")]
    uint durabilityPercentage;

    [XmlAttribute("Sprite")]
    Sprite sprite;

    public WeaponData()
    {
        weaponName = "NULL";
        weaponType = WeaponType.WEAPON_INVALID;
        weaponTraits = new List<WeaponTraitType>(0);
        damageType = DamageType.DAMAGE_INVALID;
        specialDamages = new List<SpecialDamageType>(0);
        attackTimings = new List<AttackTimingType>(0);
        pointOfContactFrames = new List<int>(0);
        durabilityPercentage = 0;
        sprite = null;
    }

    public WeaponData(string name, WeaponType weaponType, WeaponTraitType weaponTrait, DamageType damageType, SpecialDamageType specialDamageType, AttackTimingType attackTiming, int contactFrame, uint durabilityVal, Sprite spriteVal)
    {
        weaponName = name;
        this.weaponType = weaponType;
        weaponTraits = new List<WeaponTraitType> { weaponTrait };
        this.damageType = damageType;
        specialDamages = new List<SpecialDamageType> { specialDamageType };
        attackTimings = new List<AttackTimingType> { attackTiming };
        pointOfContactFrames = new List<int> { contactFrame };
        durabilityPercentage = durabilityVal;
        sprite = spriteVal;
    }

    // Getters
    public string GetWeaponName()
    {
        return weaponName;
    }

    public WeaponType GetWeaponType()
    {
        return weaponType;
    }

    public List<WeaponTraitType> GetWeaponTraits()
    {
        return weaponTraits;
    }

    public DamageType GetDamageType()
    {
        return damageType;
    }

    public List<SpecialDamageType> GetSpecialDamageTypes()
    {
        return specialDamages;
    }

    public List<AttackTimingType> GetAttackTimings()
    {
        return attackTimings;
    }

    public List<int> GetPointOfContactFrames()
    {
        return pointOfContactFrames;
    }

    public uint GetWeaponDurability()
    {
        return durabilityPercentage;
    }

    public Sprite GetWeaponSpriteInfo()
    {
        return sprite;
    }

    // Setters
    public void SetWeaponName(string name)
    {
        weaponName = name;
    }

    public void SetWeaponType(WeaponType wpnType)
    {
        weaponType = wpnType;
    }

    public void AddWeaponTrait(WeaponTraitType weaponTrait)
    {
        if (weaponTraits == null)
        {
            weaponTraits = new List<WeaponTraitType>();
        }

        weaponTraits.Add(weaponTrait);
    }

    public void SetDamageType(DamageType dmgType)
    {
        damageType = dmgType;
    }

    public void AddSpecialDamageType(SpecialDamageType specialDamage)
    {
        if (specialDamages == null)
        {
            specialDamages = new List<SpecialDamageType>();
        }

        specialDamages.Add(specialDamage);
    }

    public void AddAttackTiming(AttackTimingType attackTimingType)
    {
        if (attackTimings == null)
        {
            attackTimings = new List<AttackTimingType>();
        }

        attackTimings.Add(attackTimingType);
    }

    public void AddPointOfContactFrames(int contactFrame)
    {
        if (pointOfContactFrames == null)
        {
            pointOfContactFrames = new List<int>();
        }

        pointOfContactFrames.Add(contactFrame);
    }

    public void SetWeaponDurability(uint durabilityVal)
    {
        durabilityPercentage = durabilityVal;
    }

    public void SetWeaponSpriteInfo(Sprite spr)
    {
        sprite = spr;
    }

    public void SetWeaponData(string name, WeaponType weaponType, WeaponTraitType weaponTrait, DamageType damageType, SpecialDamageType specialDamageType, AttackTimingType attackTiming, int contactFrame, uint durabilityVal, Sprite spriteVal)
    {
        weaponName = name;
        this.weaponType = weaponType;
        AddWeaponTrait(weaponTrait);
        this.damageType = damageType;
        AddSpecialDamageType(specialDamageType);
        AddAttackTiming(attackTiming);
        AddPointOfContactFrames(contactFrame);
        durabilityPercentage = durabilityVal;
        sprite = spriteVal;
    }

    public void SetWeaponData(WeaponData newData)
    {
        weaponName = newData.weaponName;
        weaponType = newData.weaponType;

        foreach (WeaponTraitType traitType in newData.GetWeaponTraits())
        {
            AddWeaponTrait(traitType);
        }

        damageType = newData.damageType;

        foreach (SpecialDamageType splDmgType in newData.GetSpecialDamageTypes())
        {
            AddSpecialDamageType(splDmgType);
        }

        foreach (AttackTimingType atkTmgType in newData.GetAttackTimings())
        {
            AddAttackTiming(atkTmgType);
        }

        foreach (int contactFrame in newData.GetPointOfContactFrames())
        {
            AddPointOfContactFrames(contactFrame);
        }
        
        durabilityPercentage = newData.durabilityPercentage;
        sprite = newData.sprite;
    }

    public XmlSchema GetSchema()
    {
        return (null);
    }

    public void ReadXml(XmlReader reader)
    {
        reader.ReadStartElement("WeaponData");
        {
            reader.ReadStartElement("WeaponName");
            weaponName = reader.ReadString();
            reader.ReadEndElement();

            reader.ReadStartElement("WeaponType");
            weaponType = StringParserWrapper.GetEnumWeaponType(reader.ReadString());
            reader.ReadEndElement();

            //*
            //Weapon Traits Data
            reader.ReadStartElement("WeaponTraitsList");

            {
                reader.ReadStartElement("WeaponTraitsCount");
                int weaponTraitsCount = StringParserWrapper.GetInt(reader.ReadString());
                reader.ReadEndElement();

                if (weaponTraits != null)
                {
                    weaponTraits.Clear();
                }
                else
                {
                    weaponTraits = new List<WeaponTraitType>();
                }

                for (int traitsIndex = 0; traitsIndex < weaponTraitsCount; ++traitsIndex)
                {
                    reader.ReadStartElement("WeaponTraitType");
                    WeaponTraitType traitType = StringParserWrapper.GetEnumWeaponTraitType(reader.ReadString());
                    reader.ReadEndElement();

                    weaponTraits.Add(traitType);
                }
            }

            reader.ReadEndElement();
            //*/

            reader.ReadStartElement("WeaponDamageType");
            damageType = StringParserWrapper.GetEnumWeaponDamageType(reader.ReadString());
            reader.ReadEndElement();

            //*
            //Weapon Special Damage Data
            reader.ReadStartElement("WeaponSpecialDamagesList");

            {
                reader.ReadStartElement("WeaponSpecialDamagesCount");
                int weaponSpecialDamagesCount = StringParserWrapper.GetInt(reader.ReadString());
                reader.ReadEndElement();

                if (specialDamages != null)
                {
                    specialDamages.Clear();
                }
                else
                {
                    specialDamages = new List<SpecialDamageType>();
                }

                for (int spDamagesIndex = 0; spDamagesIndex < weaponSpecialDamagesCount; ++spDamagesIndex)
                {
                    reader.ReadStartElement("WeaponSpecialDamageType");
                    SpecialDamageType spDamageType = StringParserWrapper.GetEnumWeaponSpecialDamageType(reader.ReadString());
                    reader.ReadEndElement();

                    specialDamages.Add(spDamageType);
                }
            }

            reader.ReadEndElement();
            //*/

            //*
            //Weapon Attack Timings Data
            reader.ReadStartElement("WeaponAttackTimingsList");

            {
                reader.ReadStartElement("WeaponAttackTimingsCount");
                int weaponAttackTimingsCount = StringParserWrapper.GetInt(reader.ReadString());
                reader.ReadEndElement();

                if (attackTimings != null)
                {
                    attackTimings.Clear();
                }
                else
                {
                    attackTimings = new List<AttackTimingType>();
                }

                for (int attackTimingIndex = 0; attackTimingIndex < weaponAttackTimingsCount; ++attackTimingIndex)
                {
                    reader.ReadStartElement("WeaponAttackTimingType");
                    AttackTimingType atkTimingType = StringParserWrapper.GetEnumWeaponAttackTimingType(reader.ReadString());
                    reader.ReadEndElement();

                    attackTimings.Add(atkTimingType);
                }
            }

            reader.ReadEndElement();
            //*/

            //*
            //Weapon Point Of Contact Frames Data
            reader.ReadStartElement("WeaponContactFramesList");

            {
                reader.ReadStartElement("WeaponContactFramesCount");
                int weaponContactFramesCount = StringParserWrapper.GetInt(reader.ReadString());
                reader.ReadEndElement();

                if (pointOfContactFrames != null)
                {
                    pointOfContactFrames.Clear();
                }
                else
                {
                    pointOfContactFrames = new List<int>();
                }

                for (int contactFrameIndex = 0; contactFrameIndex < weaponContactFramesCount; ++contactFrameIndex)
                {
                    reader.ReadStartElement("WeaponContactFrameType");
                    int contactFrame = StringParserWrapper.GetInt(reader.ReadString());
                    reader.ReadEndElement();

                    pointOfContactFrames.Add(contactFrame);
                }
            }

            reader.ReadEndElement();
            //*/

            reader.ReadStartElement("WeaponDurability");
            durabilityPercentage = StringParserWrapper.GetUInt(reader.ReadString());
            reader.ReadEndElement();

            reader.ReadStartElement("WeaponSpriteInfo");
            sprite = StringParserWrapper.GetSpriteFromString(reader.ReadString());
            reader.ReadEndElement();
        }
        reader.ReadEndElement();
    }

    public void WriteXml(XmlWriter writer)
    {
        writer.WriteStartElement("WeaponName");
        writer.WriteString(weaponName.ToString());
        writer.WriteEndElement();

        writer.WriteStartElement("WeaponType");
        writer.WriteString(weaponType.ToString());
        writer.WriteEndElement();

        //*
        writer.WriteStartElement("WeaponTraitsList");

        {
            writer.WriteStartElement("WeaponTraitsCount");
            writer.WriteString(weaponTraits.Count.ToString());
            writer.WriteEndElement();

            for (int traitIndex = 0; traitIndex < weaponTraits.Count; ++traitIndex)
            {
                writer.WriteStartElement("WeaponTraitType");
                writer.WriteString(weaponTraits[traitIndex].ToString());
                writer.WriteEndElement();
            }
        }

        writer.WriteEndElement();
        //*/

        writer.WriteStartElement("WeaponDamageType");
        writer.WriteString(damageType.ToString());
        writer.WriteEndElement();

        //*
        writer.WriteStartElement("WeaponSpecialDamagesList");

        {
            writer.WriteStartElement("WeaponSpecialDamagesCount");
            writer.WriteString(specialDamages.Count.ToString());
            writer.WriteEndElement();

            for (int spDamageIndex = 0; spDamageIndex < specialDamages.Count; ++spDamageIndex)
            {
                writer.WriteStartElement("WeaponSpecialDamageType");
                writer.WriteString(specialDamages[spDamageIndex].ToString());
                writer.WriteEndElement();
            }
        }

        writer.WriteEndElement();
        //*/

        //*
        writer.WriteStartElement("WeaponAttackTimingsList");

        {
            writer.WriteStartElement("WeaponAttackTimingsCount");
            writer.WriteString(attackTimings.Count.ToString());
            writer.WriteEndElement();

            for (int atkTimingIndex = 0; atkTimingIndex < attackTimings.Count; ++atkTimingIndex)
            {
                writer.WriteStartElement("WeaponAttackTimingType");
                writer.WriteString(attackTimings[atkTimingIndex].ToString());
                writer.WriteEndElement();
            }
        }

        writer.WriteEndElement();
        //*/

        //*
        writer.WriteStartElement("WeaponContactFramesList");

        {
            writer.WriteStartElement("WeaponContactFramesCount");
            writer.WriteString(pointOfContactFrames.Count.ToString());
            writer.WriteEndElement();

            for (int contactFrameIndex = 0; contactFrameIndex < pointOfContactFrames.Count; ++contactFrameIndex)
            {
                writer.WriteStartElement("WeaponContactFrameType");
                writer.WriteString(pointOfContactFrames[contactFrameIndex].ToString());
                writer.WriteEndElement();
            }
        }

        writer.WriteEndElement();
        //*/

        writer.WriteStartElement("WeaponDurability");
        writer.WriteString(durabilityPercentage.ToString());
        writer.WriteEndElement();

        writer.WriteStartElement("WeaponSpriteInfo");

        if (sprite == null)
        {
            writer.WriteString("NULL");
        }
        else
        {
            writer.WriteString(sprite.name);
        }
        writer.WriteEndElement();
    }
}
