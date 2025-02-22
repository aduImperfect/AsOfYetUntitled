using System.Xml.Schema;
using System.Xml.Serialization;
using System.Xml;

public class LevelSwitcherData : IXmlSerializable
{
    [XmlAttribute("LevelName")]
    private string levelName;

    public LevelSwitcherData()
    {
        levelName = "NULL";
    }

    public LevelSwitcherData(string name)
    {
        levelName = name;
    }

    public string GetLevelName()
    {
        return levelName;
    }

    public void SetLevelName(string name)
    {
        levelName = name;
    }

    public void SetData(string name)
    {
        levelName = name;
    }

    public XmlSchema GetSchema()
    {
        return (null);
    }

    public void ReadXml(XmlReader reader)
    {
        reader.ReadStartElement("LevelSwitcherData");

        reader.ReadStartElement("LevelName");
        levelName = reader.ReadString();
        reader.ReadEndElement();

        reader.ReadEndElement();
    }

    public void WriteXml(XmlWriter writer)
    {
        writer.WriteStartElement("LevelName");
        writer.WriteString(levelName.ToString());
        writer.WriteEndElement();
    }
}