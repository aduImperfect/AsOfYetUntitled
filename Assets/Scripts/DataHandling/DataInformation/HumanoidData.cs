using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using UnityEngine;

public class HumanoidData : IXmlSerializable
{
    [XmlAttribute("CharacterHumanType")]
    HumanType humanType;

    [XmlAttribute("CharacterBodyType")]
    BodyType bodyType;

    [XmlAttribute("CharacterBodyColor")]
    Color bodyColor;

    [XmlAttribute("CharacterSkinType")]
    SkinType skinType;

    [XmlAttribute("CharacterSkinColor")]
    Color skinColor;

    [XmlAttribute("CharacterHairType")]
    HairType hairType;

    [XmlAttribute("CharacterHairColor")]
    Color hairColor;

    [XmlAttribute("CharacterPrimaryWeapon")]
    string primaryWeaponName;

    [XmlAttribute("CharacterSecondaryWeapon")]
    string secondaryWeaponName;

    [XmlAttribute("CharacterWeaponInventory")]
    List<WeaponData> weaponInventory;

    public HumanoidData()
    {
        humanType = HumanType.HUMAN_INVALID;
        bodyType = BodyType.BODY_INVALID;
        bodyColor = Color.black;
        skinType = SkinType.SKIN_INVALID;
        skinColor = Color.black;
        hairType = HairType.HAIR_INVALID;
        hairColor = Color.black;
        primaryWeaponName = "NULL";
        secondaryWeaponName = "NULL";
        weaponInventory = new List<WeaponData>(0);
    }

    public HumanoidData(HumanType characterHumanType, BodyType characterBodyType, Color bodyColor, SkinType characterSkinType, Color skinColor, HairType hairType, Color hairColor, string primaryWeapon, string secondaryWeapon, List<WeaponData> weaponInventory)
    {
        this.humanType = characterHumanType;
        this.bodyType = characterBodyType;
        this.bodyColor = bodyColor;
        this.skinType = characterSkinType;
        this.skinColor = skinColor;
        this.hairType = hairType;
        this.hairColor = hairColor;
        this.primaryWeaponName = primaryWeapon;
        if (primaryWeaponName.Equals(""))
        {
            primaryWeaponName = "NULL";
        }
        this.secondaryWeaponName = secondaryWeapon;
        if (secondaryWeaponName.Equals(""))
        {
            secondaryWeaponName = "NULL";
        }
        this.weaponInventory = weaponInventory;
    }

    //Getters
    public HumanType GetHumanType()
    {
        return humanType;
    }

    public BodyType GetBodyType()
    {
        return bodyType;
    }
    
    public Color GetBodyColor()
    {
        return bodyColor;
    }
    
    public SkinType GetSkinType()
    {
        return skinType;
    }
    
    public Color GetSkinColor()
    {
        return skinColor;
    }
    
    public HairType GetHairType()
    {
        return hairType;
    }
    
    public Color GetHairColor()
    {
        return hairColor;
    }

    public string GetPrimaryWeaponName()
    {
        return primaryWeaponName;
    }

    public string GetSecondaryWeaponName()
    {
        return secondaryWeaponName;
    }

    public List<WeaponData> GetWeaponInventory()
    {
        return weaponInventory;
    }

    //Setters
    public void SetHumanType(HumanType humanType)
    {
        this.humanType = humanType; 
    }
    
    public void SetBodyType(BodyType bodyType)
    {
        this.bodyType = bodyType;
    }
    
    public void SetBodyColor(Color bodyColor)
    {
        this.bodyColor = bodyColor;
    }

    public void SetSkinType(SkinType skinType)
    {
        this.skinType = skinType;
    }
    
    public void SetSkinColor(Color skinColor)
    {
        this.skinColor = skinColor;
    }

    public void SetHairType(HairType hairType)
    {
        this.hairType = hairType;
    }

    public void SetHairColor(Color hairColor)
    {
        this.hairColor = hairColor;
    }

    public void SetPrimaryWeapon(string weaponName)
    {
        primaryWeaponName = weaponName;
    }
    
    public void SetSecondaryWeapon(string weaponName)
    {
        secondaryWeaponName = weaponName;
    }

    public void SetWeaponInventory(List<WeaponData> weapons)
    {
        weaponInventory = weapons;
    }

    public XmlSchema GetSchema()
    {
        return null;
    }

    public void ReadXml(XmlReader reader)
    {
        reader.ReadStartElement("HumanType");
        humanType = StringParserWrapper.GetEnumHumanType(reader.ReadString());
        reader.ReadEndElement();
        
        reader.ReadStartElement("BodyType");
        bodyType = StringParserWrapper.GetEnumBodyType(reader.ReadString());
        reader.ReadEndElement();

        reader.ReadStartElement("BodyColor");
        bodyColor = StringParserWrapper.GetColor(reader.ReadString());
        reader.ReadEndElement();

        reader.ReadStartElement("SkinType");
        skinType = StringParserWrapper.GetEnumSkinType(reader.ReadString());
        reader.ReadEndElement();

        reader.ReadStartElement("SkinColor");
        skinColor = StringParserWrapper.GetColor(reader.ReadString());
        reader.ReadEndElement();

        reader.ReadStartElement("HairType");
        hairType = StringParserWrapper.GetEnumHairType(reader.ReadString());
        reader.ReadEndElement();

        reader.ReadStartElement("HairColor");
        hairColor = StringParserWrapper.GetColor(reader.ReadString());
        reader.ReadEndElement();

        reader.ReadStartElement("PrimaryWeaponName");
        primaryWeaponName = reader.ReadString();
        reader.ReadEndElement();

        reader.ReadStartElement("SecondaryWeaponName");
        secondaryWeaponName = reader.ReadString();
        reader.ReadEndElement();

        reader.ReadStartElement("WeaponInventoryList");
        {
            reader.ReadStartElement("WeaponInventoryCount");
            int count = StringParserWrapper.GetInt(reader.ReadString());
            reader.ReadEndElement();

            weaponInventory = new List<WeaponData>();

            for(int i = 0; i < count; ++i)
            {
                WeaponData weaponData = new WeaponData();

                weaponData.ReadXml(reader);

                weaponInventory.Add(weaponData);
            }
        }
        reader.ReadEndElement();
    }

    public void WriteXml(XmlWriter writer)
    {
        writer.WriteStartElement("HumanType");
        writer.WriteString(humanType.ToString());
        writer.WriteEndElement();

        writer.WriteStartElement("BodyType");
        writer.WriteString(bodyType.ToString());
        writer.WriteEndElement();

        writer.WriteStartElement("BodyColor");
        writer.WriteString(bodyColor.ToString());
        writer.WriteEndElement();

        writer.WriteStartElement("SkinType");
        writer.WriteString(skinType.ToString());
        writer.WriteEndElement();

        writer.WriteStartElement("SkinColor");
        writer.WriteString(skinColor.ToString());
        writer.WriteEndElement();

        writer.WriteStartElement("HairType");
        writer.WriteString(hairType.ToString());
        writer.WriteEndElement();

        writer.WriteStartElement("HairColor");
        writer.WriteString(hairColor.ToString());
        writer.WriteEndElement();

        writer.WriteStartElement("PrimaryWeaponName");
        writer.WriteString(primaryWeaponName.ToString());
        writer.WriteEndElement();
        
        writer.WriteStartElement("SecondaryWeaponName");
        writer.WriteString(secondaryWeaponName.ToString());
        writer.WriteEndElement();

        writer.WriteStartElement("WeaponInventoryList");
        {
            writer.WriteStartElement("WeaponInventoryCount");
            writer.WriteString(weaponInventory.Count.ToString());
            writer.WriteEndElement();
            
            foreach (WeaponData weaponItem in weaponInventory)
            {
                writer.WriteStartElement("WeaponData");
                weaponItem.WriteXml(writer);
                writer.WriteEndElement();
            }
        }
        writer.WriteEndElement();
    }
}
