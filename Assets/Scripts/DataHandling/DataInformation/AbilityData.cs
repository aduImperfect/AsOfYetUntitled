using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

public class AbilityData : IXmlSerializable
{
    [XmlAttribute("CharacterCanAttack")]
    bool characterCanAttack;

    [XmlAttribute("CharacterInvincible")]
    bool characterInvincible;

    public AbilityData() 
    {
        characterCanAttack = false;
        characterInvincible = false;
    }

    public AbilityData(bool characterCanAttack, bool characterInvincible)
    {
        this.characterCanAttack = characterCanAttack;
        this.characterInvincible = characterInvincible;
    }

    //Getters
    public bool GetCharacterCanAttack()
    {
        return characterCanAttack;
    }

    public bool GetCharacterInvincible()
    {
        return characterInvincible;
    }

    //Setters
    public void SetCharacterCanAttack(bool value)
    {
        characterCanAttack = value;
    }

    public void SetCharacterInvincible(bool value)
    {
        characterInvincible = value;
    }

    public XmlSchema GetSchema()
    {
        return null;
    }

    public void ReadXml(XmlReader reader)
    {
        reader.ReadStartElement("CharacterCanAttack");
        characterCanAttack = StringParserWrapper.GetBool(reader.ReadString());
        reader.ReadEndElement();
        
        reader.ReadStartElement("CharacterInvincible");
        characterInvincible = StringParserWrapper.GetBool(reader.ReadString());
        reader.ReadEndElement();
    }

    public void WriteXml(XmlWriter writer)
    {
        writer.WriteStartElement("CharacterCanAttack");
        writer.WriteString(characterCanAttack.ToString());
        writer.WriteEndElement();
        
        writer.WriteStartElement("CharacterInvincible");
        writer.WriteString(characterInvincible.ToString());
        writer.WriteEndElement();
    }
}
