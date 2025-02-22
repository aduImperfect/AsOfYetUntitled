using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SpriteData : IXmlSerializable
{
    [XmlAttribute("Name")]
    string spriteName;

    [XmlAttribute("Color")]
    Color spriteColor;

    [XmlAttribute("FlipX")]
    bool spriteFlipX;

    [XmlAttribute("FlipY")]
    bool spriteFlipY;

    [XmlAttribute("MatName")]
    string spriteMatName;

    [XmlAttribute("IsEnabled")]
    bool spriteIsEnabled;

    [XmlAttribute("SortingLayerName")]
    string spriteSortingLayerName;

    [XmlAttribute("SortingLayerID")]
    int spriteSortingLayerID;

    [XmlAttribute("SortingOrder")]
    int spriteSortingOrder;

    [XmlAttribute("RenderingLayerMask")]
    uint spriteRenderingLayerMask;

    public SpriteData()
    {
        spriteName = "NULL";
        spriteColor = Color.white;
        spriteFlipX = false;
        spriteFlipY = false;
        spriteMatName = "NULL";
        spriteIsEnabled = false;
        spriteSortingLayerName = "NULL";
        spriteSortingLayerID = 0;
        spriteSortingOrder = 0;
        spriteRenderingLayerMask = 0;
    }

    public SpriteData(SpriteRenderer spriteRenderer)
    {
        if (spriteRenderer == null)
        {
            spriteName = "NULL";
            spriteColor = Color.white;
            spriteFlipX = false;
            spriteFlipY = false;
            spriteMatName = "NULL";
            spriteIsEnabled = false;
            spriteSortingLayerName = "NULL";
            spriteSortingLayerID = 0;
            spriteSortingOrder = 0;
            spriteRenderingLayerMask = 0;
            return;
        }
        spriteName = spriteRenderer.sprite.name;
        spriteColor = spriteRenderer.color;
        spriteFlipX = spriteRenderer.flipX;
        spriteFlipY = spriteRenderer.flipY;
        spriteMatName = spriteRenderer.sharedMaterial.name;
        spriteIsEnabled = spriteRenderer.enabled;
        spriteSortingLayerName = spriteRenderer.sortingLayerName;
        spriteSortingLayerID = spriteRenderer.sortingLayerID;
        spriteSortingOrder = spriteRenderer.sortingOrder;
        spriteRenderingLayerMask = spriteRenderer.renderingLayerMask;
    }

    // Getters

    public string GetSpriteName()
    {
        return spriteName;
    }

    public Color GetSpriteColor()
    {
        return spriteColor;
    }

    public bool GetSpriteFlipX()
    {
        return spriteFlipX;
    }

    public bool GetSpriteFlipY()
    {
        return spriteFlipY;
    }

    public string GetSpriteMaterialName()
    {
        return spriteMatName;
    }

    public bool GetSpriteIsEnabled()
    {
        return spriteIsEnabled;
    }

    public string GetSpriteSortingLayerName()
    {
        return spriteSortingLayerName;
    }

    public int GetSpriteSortingLayerID()
    {
        return spriteSortingLayerID;
    }

    public int GetSpriteSortingOrder()
    {
        return spriteSortingOrder;
    }

    public uint GetSpriteRenderingLayerMask()
    {
        return spriteRenderingLayerMask;
    }



    // Setters

    public void SetSpriteName(string name)
    {
        spriteName = name;
    }

    public void SetSpriteColor(Color color)
    {
        spriteColor = color;
    }

    public void SetSpriteFlipX(bool set)
    {
        spriteFlipX = set;
    }

    public void SetSpriteFlipY(bool set)
    {
        spriteFlipY = set;
    }

    public void SetSpriteMaterialName(string name)
    {
        spriteMatName = name;
    }

    public void SetSpriteIsEnabled(bool set)
    {
        spriteIsEnabled = set;
    }

    public void SetSpriteSortingLayerName(string name)
    {
        spriteSortingLayerName = name;
    }

    public void SetSpriteSortingLayerID(int id)
    {
        spriteSortingLayerID = id;
    }

    public void SetSpriteSortingOrder(int order)
    {
        spriteSortingOrder = order;
    }

    public void SetSpriteRenderingLayerMask(uint layerMask)
    {
        spriteRenderingLayerMask = layerMask;
    }

    public void SetData(SpriteRenderer spriteRenderer)
    {
        spriteName = spriteRenderer.sprite.name;
        spriteColor = spriteRenderer.color;
        spriteFlipX = spriteRenderer.flipX;
        spriteFlipY = spriteRenderer.flipY;
        spriteMatName = spriteRenderer.sharedMaterial.name;
        spriteIsEnabled = spriteRenderer.enabled;
        spriteSortingLayerName = spriteRenderer.sortingLayerName;
        spriteSortingLayerID = spriteRenderer.sortingLayerID;
        spriteSortingOrder = spriteRenderer.sortingOrder;
        spriteRenderingLayerMask = spriteRenderer.renderingLayerMask;
    }

    public XmlSchema GetSchema()
    {
        return (null);
    }

    public void ReadXml(XmlReader reader)
    {
        reader.ReadStartElement("SpriteData");

        reader.ReadStartElement("SpriteName");
        spriteName = reader.ReadString();
        reader.ReadEndElement();

        reader.ReadStartElement("SpriteColor");
        spriteColor = StringParserWrapper.GetColor(reader.ReadString());
        reader.ReadEndElement();

        reader.ReadStartElement("SpriteFlipX");
        spriteFlipX = StringParserWrapper.GetBool(reader.ReadString());
        reader.ReadEndElement();

        reader.ReadStartElement("SpriteFlipY");
        spriteFlipY = StringParserWrapper.GetBool(reader.ReadString());
        reader.ReadEndElement();

        reader.ReadStartElement("SpriteMaterialName");
        spriteMatName = reader.ReadString();
        reader.ReadEndElement();

        reader.ReadStartElement("SpriteIsEnabled");
        spriteIsEnabled = StringParserWrapper.GetBool(reader.ReadString());
        reader.ReadEndElement();

        reader.ReadStartElement("SpriteSortingLayerName");
        spriteSortingLayerName = reader.ReadString();
        reader.ReadEndElement();

        reader.ReadStartElement("SpriteSortingLayerID");
        spriteSortingLayerID = StringParserWrapper.GetInt(reader.ReadString());
        reader.ReadEndElement();

        reader.ReadStartElement("SpriteSortingOrder");
        spriteSortingOrder = StringParserWrapper.GetInt(reader.ReadString());
        reader.ReadEndElement();

        reader.ReadStartElement("SpriteRenderingLayerMask");
        spriteRenderingLayerMask = StringParserWrapper.GetUInt(reader.ReadString());
        reader.ReadEndElement();

        reader.ReadEndElement();
    }

    public void WriteXml(XmlWriter writer)
    {
        writer.WriteStartElement("SpriteName");
        writer.WriteString(spriteName.ToString());
        writer.WriteEndElement();

        writer.WriteStartElement("SpriteColor");
        writer.WriteString(spriteColor.ToString());
        writer.WriteEndElement();

        writer.WriteStartElement("SpriteFlipX");
        writer.WriteString(spriteFlipX.ToString());
        writer.WriteEndElement();

        writer.WriteStartElement("SpriteFlipY");
        writer.WriteString(spriteFlipY.ToString());
        writer.WriteEndElement();

        writer.WriteStartElement("SpriteMaterialName");
        writer.WriteString(spriteMatName.ToString());
        writer.WriteEndElement();

        writer.WriteStartElement("SpriteIsEnabled");
        writer.WriteString(spriteIsEnabled.ToString());
        writer.WriteEndElement();

        writer.WriteStartElement("SpriteSortingLayerName");
        writer.WriteString(spriteSortingLayerName.ToString());
        writer.WriteEndElement();

        writer.WriteStartElement("SpriteSortingLayerID");
        writer.WriteString(spriteSortingLayerID.ToString());
        writer.WriteEndElement();

        writer.WriteStartElement("SpriteSortingOrder");
        writer.WriteString(spriteSortingOrder.ToString());
        writer.WriteEndElement();

        writer.WriteStartElement("SpriteRenderingLayerMask");
        writer.WriteString(spriteRenderingLayerMask.ToString());
        writer.WriteEndElement();
    }
}
