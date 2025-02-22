using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

public class AttributeData : IXmlSerializable
{
    //TODO List!
    //Shift MoveSpeed, AnimationSpeed, Health, and other attributes from CharacterData.

    [XmlAttribute("Health")]
    uint characterHealth;

    [XmlAttribute("Mode")]
    CharacterMode characterMode;

    [XmlAttribute("IsUnique")]
    bool isCharacterUnique;

    [XmlAttribute("AnimationSpeed")]
    float characterAnimationSpeed;

    [XmlAttribute("MoveSpeed")]
    float characterMoveSpeed;

    public XmlSchema GetSchema()
    {
        throw new System.NotImplementedException();
    }

    public void ReadXml(XmlReader reader)
    {
        throw new System.NotImplementedException();
    }

    public void WriteXml(XmlWriter writer)
    {
        throw new System.NotImplementedException();
    }
}
