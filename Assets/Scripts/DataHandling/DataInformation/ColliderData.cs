using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public enum ColliderType
{
    BOX = 0,
    CIRCLE,
    POLYGON,
    CAPSULE,
    NONE = -1,
}

public class ColliderData : IXmlSerializable
{
    [XmlAttribute("ColliderType")]
    protected ColliderType colliderType;

    [XmlAttribute("IsTrigger")]
    bool isTrigger;

    [XmlAttribute("Offset")]
    Vector2 offset;

    [XmlAttribute("IsEnabled")]
    bool isEnabled;

    public ColliderData()
    {
        this.colliderType = ColliderType.NONE;
        isTrigger = false;
        offset = Vector2.zero;
        this.isEnabled = false;
    }
    
    public ColliderData(ColliderType colliderType, bool isTrigger, Vector2 offset, bool isEnabled)
    {
        this.colliderType = colliderType;
        this.isTrigger = isTrigger;
        this.offset.x = offset.x;
        this.offset.y = offset.y;
        this.isEnabled = isEnabled;
    }
    
    public ColliderData(Collider2D collider)
    {
        if (collider == null)
        {
            this.colliderType = ColliderType.NONE;
            isTrigger = false;
            offset = Vector2.zero;
            this.isEnabled = false;
            return;
        }

        colliderType = ColliderType.NONE;
        this.isTrigger = collider.isTrigger;
        this.offset.x = collider.offset.x;
        this.offset.y = collider.offset.y;
        this.isEnabled = collider.enabled;
    }

    //Setters
    public void SetColliderType(ColliderType colliderType)
    {
        this.colliderType = colliderType;
    }

    public void SetData(ColliderType colliderType, bool isTrigger, Vector2 offset, bool isEnabled)
    {
        this.colliderType = colliderType;
        this.isTrigger = isTrigger;
        this.offset.x = offset.x;
        this.offset.y = offset.y;
        this.isEnabled = isEnabled;
    }

    public void SetData(Collider2D collider)
    {
        if (collider == null)
        {
            this.colliderType = ColliderType.NONE;
            isTrigger = false;
            offset = Vector2.zero;
            this.isEnabled = false;
            return;
        }

        this.isTrigger = collider.isTrigger;
        this.offset.x = collider.offset.x;
        this.offset.y = collider.offset.y;
        this.isEnabled = collider.enabled;
    }

    //Getters
    public ColliderType GetColliderType()
    {
        return colliderType;
    }

    public bool GetIsTrigger()
    {
        return isTrigger;
    }

    public Vector2 GetOffset()
    {
        return offset;
    }

    public bool GetIsEnabled()
    {
        return isEnabled;
    }

    public XmlSchema GetSchema()
    {
        return (null);
    }

    public virtual void ReadXml(XmlReader reader)
    {
        reader.ReadStartElement("IsTrigger");
        isTrigger = StringParserWrapper.GetBool(reader.ReadString());
        reader.ReadEndElement();

        reader.ReadStartElement("OffsetX");
        offset.x = StringParserWrapper.GetFloat(reader.ReadString());
        reader.ReadEndElement();

        reader.ReadStartElement("OffsetY");
        offset.y = StringParserWrapper.GetFloat(reader.ReadString());
        reader.ReadEndElement();

        reader.ReadStartElement("IsEnabled");
        isEnabled = StringParserWrapper.GetBool(reader.ReadString());
        reader.ReadEndElement();
    }

    public virtual void WriteXml(XmlWriter writer)
    {
        writer.WriteStartElement("ColliderType");
        writer.WriteString(colliderType.ToString());
        writer.WriteEndElement();

        writer.WriteStartElement("IsTrigger");
        writer.WriteString(isTrigger.ToString());
        writer.WriteEndElement();

        writer.WriteStartElement("OffsetX");
        writer.WriteString(offset.x.ToString());
        writer.WriteEndElement();

        writer.WriteStartElement("OffsetY");
        writer.WriteString(offset.y.ToString());
        writer.WriteEndElement();

        writer.WriteStartElement("IsEnabled");
        writer.WriteString(isEnabled.ToString());
        writer.WriteEndElement();
    }

    protected void SetColliderDataToColliderObject(ref GameObject colliderObject)
    {
        Collider2D collider;
        if (!colliderObject.TryGetComponent<Collider2D>(out collider))
        {
            return;
        }
        collider.offset = GetOffset();
        collider.isTrigger = GetIsTrigger();
        collider.enabled = GetIsEnabled();
    }
}

public class BoxColliderData : ColliderData
{
    [XmlAttribute("BoxColliderSize")]
    Vector2 boxColliderSize;

    public BoxColliderData() : base()
    {
        colliderType = ColliderType.BOX;
        this.boxColliderSize = Vector2.zero;
    }

    public BoxColliderData(Vector2 colliderSize) : base()
    {
        colliderType = ColliderType.BOX;
        this.boxColliderSize.x = colliderSize.x;
        this.boxColliderSize.y = colliderSize.y;
    }
    
    public BoxColliderData(BoxCollider2D boxCollider) : base(boxCollider)
    {
        colliderType = ColliderType.BOX;
        this.boxColliderSize.x = boxCollider.size.x;
        this.boxColliderSize.y = boxCollider.size.y;
    }

    //Getters
    public Vector2 GetColliderSize()
    {
        return boxColliderSize;
    }

    //Setters
    public void SetColliderSize(Vector2 value)
    {
        this.boxColliderSize = value;
    }
    
    public void SetColliderSize(BoxCollider2D boxCollider)
    {
        this.boxColliderSize = boxCollider.size;
    }

    public override void ReadXml(XmlReader reader)
    {
        base.ReadXml(reader);
        
        reader.ReadStartElement("BoxCollider");

        reader.ReadStartElement("BoxColliderSizeX");
        boxColliderSize.x = StringParserWrapper.GetFloat(reader.ReadString());
        reader.ReadEndElement();

        reader.ReadStartElement("BoxColliderSizeY");
        boxColliderSize.y = StringParserWrapper.GetFloat(reader.ReadString());
        reader.ReadEndElement();

        reader.ReadEndElement();
    }

    public override void WriteXml(XmlWriter writer)
    {
        base.WriteXml(writer);

        writer.WriteStartElement("BoxCollider");

        writer.WriteStartElement("BoxColliderSizeX");
        writer.WriteString(boxColliderSize.x.ToString());
        writer.WriteEndElement();

        writer.WriteStartElement("BoxColliderSizeY");
        writer.WriteString(boxColliderSize.y.ToString());
        writer.WriteEndElement();

        writer.WriteEndElement();
    }

    public void SetBoxColliderDataToBoxCollider(ref GameObject boxColliderGameObject)
    {
        BoxCollider2D collider;
        if (!boxColliderGameObject.TryGetComponent<BoxCollider2D>(out collider))
        {
            return;
        }
        base.SetColliderDataToColliderObject(ref boxColliderGameObject);
        collider.size = GetColliderSize();
    }
}

public class CircleColliderData : ColliderData
{
    [XmlAttribute("CircleRadiusSize")]
    float circleRadiusSize;

    public CircleColliderData() : base()
    {
        colliderType = ColliderType.CIRCLE;
        this.circleRadiusSize = 0f;
    }

    public CircleColliderData(float radiusSize) : base()
    {
        colliderType = ColliderType.CIRCLE;
        this.circleRadiusSize = radiusSize;
    }
    
    public CircleColliderData(CircleCollider2D circleCollider) : base(circleCollider)
    {
        colliderType = ColliderType.CIRCLE;
        this.circleRadiusSize = circleCollider.radius;
    }

    //Getters
    public float GetColliderRadius()
    {
        return circleRadiusSize;
    }

    //Setters
    public void SetRadius(float value)
    {
        this.circleRadiusSize = value;
    }

    public override void ReadXml(XmlReader reader)
    {
        base.ReadXml(reader);

        reader.ReadStartElement("CircleCollider");

        reader.ReadStartElement("CircleRadiusSize");
        circleRadiusSize = StringParserWrapper.GetFloat(reader.ReadString());
        reader.ReadEndElement();

        reader.ReadEndElement();
    }

    public override void WriteXml(XmlWriter writer)
    {
        base.WriteXml(writer);

        writer.WriteStartElement("CircleCollider");

        writer.WriteStartElement("CircleRadiusSize");
        writer.WriteString(circleRadiusSize.ToString());
        writer.WriteEndElement();

        writer.WriteEndElement();
    }

    public void SetCircleColliderDataToCircleCollider(ref GameObject circleColliderGameObject)
    {
        CircleCollider2D collider;
        if (!circleColliderGameObject.TryGetComponent<CircleCollider2D>(out collider))
        {
            return;
        }
        base.SetColliderDataToColliderObject(ref circleColliderGameObject);
        collider.radius = GetColliderRadius();
    }
}

public class PolygonColliderData : ColliderData
{
    [XmlAttribute("PolygonColliderPointsList")]
    List<Vector2> polygonColliderPointsList;

    public PolygonColliderData() : base()
    {
        colliderType = ColliderType.POLYGON;
        this.polygonColliderPointsList = new List<Vector2>();
    }

    public PolygonColliderData(List<Vector2> polygonColliderPointsList) : base()
    {
        colliderType = ColliderType.POLYGON;
        this.polygonColliderPointsList = polygonColliderPointsList;
    }
    
    public PolygonColliderData(PolygonCollider2D polygonCollider) : base(polygonCollider)
    {
        colliderType = ColliderType.POLYGON;
        this.polygonColliderPointsList = polygonCollider.points.ToList<Vector2>();
    }

    //Getters
    public List<Vector2> GetPolygonColliderPointsList()
    {
        return polygonColliderPointsList;
    }

    //Setters
    public void SetPolygonColliderPointsList(List<Vector2> polygonColliderPointsList)
    {
        this.polygonColliderPointsList = polygonColliderPointsList;
    }
    
    public void SetPolygonColliderPointsList(PolygonCollider2D polygonCollider)
    {
        base.SetData(polygonCollider);
        this.polygonColliderPointsList = polygonCollider.points.ToList<Vector2>();
    }

    public override void ReadXml(XmlReader reader)
    {
        base.ReadXml(reader);

        reader.ReadStartElement("PolygonCollider");

        reader.ReadStartElement("PolygonColliderPointsList");

        polygonColliderPointsList.Clear();

        reader.ReadStartElement("PolygonPointsCount");
        int polygonPointsCount = StringParserWrapper.GetInt(reader.ReadString());
        reader.ReadEndElement();

        for (int pointsIndex = 0; pointsIndex < polygonPointsCount; pointsIndex++)
        {
            reader.ReadStartElement("PolygonPointX");
            float pointX = StringParserWrapper.GetFloat(reader.ReadString());
            reader.ReadEndElement();

            reader.ReadStartElement("PolygonPointY");
            float pointY = StringParserWrapper.GetFloat(reader.ReadString());
            reader.ReadEndElement();

            polygonColliderPointsList.Add(new Vector2(pointX, pointY));
        }

        reader.ReadEndElement();

        reader.ReadEndElement();
    }

    public override void WriteXml(XmlWriter writer)
    {
        base.WriteXml(writer);

        writer.WriteStartElement("PolygonCollider");

        writer.WriteStartElement("PolygonColliderPointsList");

        writer.WriteStartElement("PolygonPointsCount");
        writer.WriteString(polygonColliderPointsList.Count.ToString());
        writer.WriteEndElement();

        for (int pointsIndex = 0; pointsIndex < polygonColliderPointsList.Count; ++pointsIndex)
        {
            writer.WriteStartElement("PolygonPointX");
            writer.WriteString(polygonColliderPointsList[pointsIndex].x.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement("PolygonPointY");
            writer.WriteString(polygonColliderPointsList[pointsIndex].y.ToString());
            writer.WriteEndElement();
        }

        writer.WriteEndElement();

        writer.WriteEndElement();
    }

    public void SetPolygonColliderDataToPolygonCollider(ref GameObject polygonColliderGameObject)
    {
        PolygonCollider2D collider;
        if (!polygonColliderGameObject.TryGetComponent<PolygonCollider2D>(out collider))
        {
            return;
        }
        base.SetColliderDataToColliderObject(ref polygonColliderGameObject);
        collider.points = GetPolygonColliderPointsList().ToArray();
    }
}

public class CapsuleColliderData : ColliderData
{
    [XmlAttribute("CapsuleColliderSize")]
    Vector2 capsuleColliderSize;

    public CapsuleColliderData() : base()
    {
        colliderType = ColliderType.CAPSULE;
        this.capsuleColliderSize = Vector2.zero;
    }

    public CapsuleColliderData(Vector2 colliderSize)
    {
        colliderType = ColliderType.CAPSULE;
        this.capsuleColliderSize.x = colliderSize.x;
        this.capsuleColliderSize.y = colliderSize.y;
    }

    public CapsuleColliderData(CapsuleCollider2D capsuleCollider) : base(capsuleCollider)
    {
        colliderType = ColliderType.CAPSULE;
        this.capsuleColliderSize.x = capsuleCollider.size.x;
        this.capsuleColliderSize.y = capsuleCollider.size.y;
    }

    //Getters
    public Vector2 GetColliderSize()
    {
        return capsuleColliderSize;
    }

    //Setters
    public void SetColliderSize(Vector2 value)
    {
        this.capsuleColliderSize = value;
    }

    public void SetColliderSize(CapsuleCollider2D capsuleCollider)
    {
        this.capsuleColliderSize.x = capsuleCollider.size.x;
        this.capsuleColliderSize.y = capsuleCollider.size.y;
    }

    public override void ReadXml(XmlReader reader)
    {
        base.ReadXml(reader);
        
        reader.ReadStartElement("CapsuleCollider");

        reader.ReadStartElement("CapsuleColliderSizeX");
        capsuleColliderSize.x = StringParserWrapper.GetFloat(reader.ReadString());
        reader.ReadEndElement();

        reader.ReadStartElement("CapsuleColliderSizeY");
        capsuleColliderSize.y = StringParserWrapper.GetFloat(reader.ReadString());
        reader.ReadEndElement();

        reader.ReadEndElement();
    }

    public override void WriteXml(XmlWriter writer)
    {
        base.WriteXml(writer);

        writer.WriteStartElement("CapsuleCollider");

        writer.WriteStartAttribute("CapsuleColliderSizeX");
        writer.WriteString(capsuleColliderSize.x.ToString());
        writer.WriteEndElement();

        writer.WriteStartAttribute("CapsuleColliderSizeY");
        writer.WriteString(capsuleColliderSize.y.ToString());
        writer.WriteEndElement();

        writer.WriteEndElement();
    }

    public void SetCapsuleColliderDataToCapsuleCollider(ref GameObject capsuleColliderGameObject)
    {
        CapsuleCollider2D collider;
        if (!capsuleColliderGameObject.TryGetComponent<CapsuleCollider2D>(out collider))
        {
            return;
        }
        base.SetColliderDataToColliderObject(ref capsuleColliderGameObject);
        collider.size = GetColliderSize();
    }
}