using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

public class CharacterFileDataList
{
    public List<CharacterFileData> characterDataList;

    public CharacterFileDataList()
    {
        characterDataList = new List<CharacterFileData>();
    }
}

public class CharacterFileData : IXmlSerializable
{
    [XmlAttribute("CharacterFileName")]
    string characterFileName;

    [XmlAttribute("CharacterObjectSubtype")]
    string characterObjSubtype;

    public CharacterFileData()
    {
        characterFileName = "";
        characterObjSubtype = "INVALID";
    }

    public CharacterFileData(string fileName, string characterObjSubtype)
    {
        characterFileName = fileName;
        this.characterObjSubtype = characterObjSubtype;
    }

    //Getters
    public string GetCharacterFileName()
    {
        return characterFileName;
    }

    public string GetCharacterSubtype()
    {
        return characterObjSubtype;
    }

    //Setters
    public void SetCharacterFileData(string characterFileName, string characterObjSubtype)
    {
        this.characterFileName = characterFileName;
        this.characterObjSubtype = characterObjSubtype;
    }

    public XmlSchema GetSchema()
    {
        return null;
    }

    public void ReadXml(XmlReader reader)
    {
        reader.ReadStartElement("CharacterFileData");

        reader.ReadStartElement("CharacterFileName");
        characterFileName = reader.ReadString();
        reader.ReadEndElement();

        reader.ReadStartElement("CharacterObjectSubtype");
        characterObjSubtype = reader.ReadString();
        reader.ReadEndElement();

        reader.ReadEndElement();
    }

    public void WriteXml(XmlWriter writer)
    {
        writer.WriteStartElement("CharacterFileName");
        writer.WriteString(characterFileName.ToString());
        writer.WriteEndElement();

        writer.WriteStartElement("CharacterObjectSubtype");
        writer.WriteString(characterObjSubtype.ToString());
        writer.WriteEndElement();
    }
}