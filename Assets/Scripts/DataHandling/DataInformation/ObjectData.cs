using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ObjectData : IXmlSerializable
{
    //TODO List!
    //Shift layer from CharacterData to ObjectData....

    [XmlAttribute("Name")]
    string name;

    [XmlAttribute("Parent")]
    string parentName;

    [XmlAttribute("Position")]
    Vector3 position;

    [XmlAttribute("Rotation")]
    Quaternion rotation;

    [XmlAttribute("Scale")]
    Vector3 scale;

    [XmlAttribute("Type")]
    string type;

    [XmlAttribute("SubType")]
    string subType;

    [XmlAttribute("IsActive")]
    bool isActive;

    [XmlAttribute("Sprite")]
    SpriteData spriteData;

    [XmlAttribute("Light")]
    Light2dData lightData;

    [XmlAttribute("Collider")]
    ColliderData[] collidersData;

    [XmlAttribute("LevelSwitcher")]
    LevelSwitcherData levelSwitcherData;

    [XmlAttribute("Layer")]
    int layer;

    public ObjectData()
    {
        name = "NULL";
        parentName = "NULL";
        position = Vector3.zero;
        rotation = Quaternion.identity;
        position = Vector3.zero;
        type = "INVALID";
        subType = "SUBTYPE_INVALID";
        isActive = false;
        spriteData = new SpriteData();
        collidersData = new ColliderData[0];
        lightData = new Light2dData();
        levelSwitcherData = new LevelSwitcherData();
        layer = -1;
    }
    
    public ObjectData(string subtype)
    {
        name = "NULL";
        parentName = "NULL";
        position = Vector3.zero;
        rotation = Quaternion.identity;
        position = Vector3.zero;
        type = StringParserWrapper.GetObjectTypeFromSubType(subtype);
        subType = subtype;
        isActive = false;
        spriteData = new SpriteData();
        collidersData = new ColliderData[0];
        lightData = new Light2dData();
        levelSwitcherData = new LevelSwitcherData();
        layer = -1;
    }

    public ObjectData(GameObject obj)
    {
        name = obj.name;
        parentName = obj.transform.parent.name;

        position.x = obj.transform.position.x;
        position.y = obj.transform.position.y;
        position.z = obj.transform.position.z;

        rotation.x = obj.transform.rotation.x;
        rotation.y = obj.transform.rotation.y;
        rotation.z = obj.transform.rotation.z;
        rotation.w = obj.transform.rotation.w;

        scale.x = obj.transform.localScale.x;
        scale.y = obj.transform.localScale.y;
        scale.z = obj.transform.localScale.z;

        type = "INVALID";
        isActive = obj.activeInHierarchy;

        SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteData = new SpriteData(spriteRenderer);
        }
        else
        {
            spriteData = new SpriteData();
        }

        //*
        Collider2D[] colliders;
        colliders = obj.GetComponents<Collider2D>();
        collidersData = new ColliderData[colliders.Length];

        if (colliders != null)
        {
            for (int colliderIndex = 0; colliderIndex < colliders.Length; ++colliderIndex)
            {
                if (colliders[colliderIndex] is BoxCollider2D)
                {
                    collidersData[colliderIndex] =
                            new BoxColliderData(colliders[colliderIndex].GetComponent<BoxCollider2D>());
                }
                else if (colliders[colliderIndex] is CircleCollider2D)
                {
                    collidersData[colliderIndex] =
                            new CircleColliderData(colliders[colliderIndex].GetComponent<CircleCollider2D>());
                }
                else if (colliders[colliderIndex] is PolygonCollider2D)
                {
                    collidersData[colliderIndex] =
                            new PolygonColliderData(colliders[colliderIndex].GetComponent<PolygonCollider2D>());
                }
                else if (colliders[colliderIndex] is CapsuleCollider2D)
                {
                    collidersData[colliderIndex] =
                            new CapsuleColliderData(colliders[colliderIndex].GetComponent<CapsuleCollider2D>());
                }
                else
                {
                    collidersData[colliderIndex] = new ColliderData(colliders[colliderIndex]);
                }

                collidersData[colliderIndex] = new ColliderData(colliders[colliderIndex]);
            }
        }
        //*/

        Light2D light2D = obj.GetComponent<Light2D>();

        if (light2D != null)
        {
            lightData = new Light2dData(light2D);
        }
        else
        {
            lightData = new Light2dData();
        }

        LevelSwitcher lvlSwitcher = obj.GetComponent<LevelSwitcher>();
        if (lvlSwitcher != null)
        {
            levelSwitcherData = new LevelSwitcherData(lvlSwitcher.newLevelName);
        }
        else
        {
            levelSwitcherData = new LevelSwitcherData();
        }

        layer = obj.layer;
    }

    public ObjectData(GameObject obj, string objSubtype)
    {
        name = obj.name;
        parentName = obj.transform.parent.name;

        position.x = obj.transform.position.x;
        position.y = obj.transform.position.y;
        position.z = obj.transform.position.z;

        rotation.x = obj.transform.rotation.x;
        rotation.y = obj.transform.rotation.y;
        rotation.z = obj.transform.rotation.z;
        rotation.w = obj.transform.rotation.w;

        scale.x = obj.transform.localScale.x;
        scale.y = obj.transform.localScale.y;
        scale.z = obj.transform.localScale.z;

        type = StringParserWrapper.GetObjectTypeFromSubType(objSubtype);
        subType = objSubtype;
        isActive = obj.activeInHierarchy;

        SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteData = new SpriteData(spriteRenderer);
        }
        else
        {
            spriteData = new SpriteData();
        }

        //*
        Collider2D[] colliders = obj.GetComponents<Collider2D>();
        collidersData = new ColliderData[colliders.Length];

        if (colliders != null)
        {
            for (int colliderIndex = 0; colliderIndex < colliders.Length; ++colliderIndex)
            {
                if (colliders[colliderIndex] is BoxCollider2D)
                {
                    collidersData[colliderIndex] =
                            new BoxColliderData(colliders[colliderIndex] as BoxCollider2D);
                }
                else if (colliders[colliderIndex] is CircleCollider2D)
                {
                    collidersData[colliderIndex] =
                            new CircleColliderData(colliders[colliderIndex] as CircleCollider2D);
                }
                else if (colliders[colliderIndex] is PolygonCollider2D)
                {
                    collidersData[colliderIndex] =
                            new PolygonColliderData(colliders[colliderIndex] as PolygonCollider2D);
                }
                else if (colliders[colliderIndex] is CapsuleCollider2D)
                {
                    collidersData[colliderIndex] =
                            new CapsuleColliderData(colliders[colliderIndex] as CapsuleCollider2D);
                }
                else
                {
                    collidersData[colliderIndex] = new ColliderData(colliders[colliderIndex]);
                }
            }
        }

        Light2D light2D = obj.GetComponent<Light2D>();

        if (light2D != null)
        {
            lightData = new Light2dData(light2D);
        }
        else
        {
            lightData = new Light2dData();
        }
        //*/

        LevelSwitcher lvlSwitcher = obj.GetComponent<LevelSwitcher>();
        if (lvlSwitcher != null)
        {
            levelSwitcherData = new LevelSwitcherData(lvlSwitcher.newLevelName);
        }
        else
        {
            levelSwitcherData = new LevelSwitcherData();
        }

        layer = obj.layer;
    }

    //Getters
    public string GetName()
    {
        return name;
    }

    public string GetParentName()
    {
        return parentName;
    }

    public Vector3 GetPosition()
    {
        return position;
    }

    public Quaternion GetOrientation()
    {
        return rotation;
    }

    public Vector3 GetScale()
    {
        return scale;
    }

    //public ObjectType GetObjectType()
    //{
    //    return type;
    //}

    public string GetObjectType()
    {
        return type;
    }

    //public ObjectSubtype GetObjectSubtype()
    //{
    //    return subType;
    //}

    public string GetObjectSubtype()
    {
        return subType;
    }

    public bool GetIsActive()
    {
        return isActive;
    }

    public SpriteData GetSpriteData()
    {
        return spriteData;
    }

    public ColliderData[] GetColliderData()
    {
        return collidersData;
    }

    public Light2dData GetLight2DData()
    {
        return lightData;
    }

    public LevelSwitcherData GetLevelSwitcherData()
    {
        return levelSwitcherData;
    }

    public int GetLayer()
    {
        return layer;
    }

    //Setters
    public void SetName(string newName)
    {
        name = newName;
    }
    
    public void SetParentName(string newName)
    {
        parentName = newName;
    }

    public void SetPosition(Vector3 newPosition)
    {
        position = newPosition;
    }

    public void SetOrientation(Quaternion newRotation)
    {
        rotation = newRotation;
    }

    public void SetScale(Vector3 newScale)
    {
        scale = newScale;
    }

    public void SetObjectType(string newType)
    {
        type = newType;
    }

    public void SetObjectSubtype(string newSubtype)
    {
        subType = newSubtype;
    }

    public void SetIsActive(bool active)
    {
        isActive = active;
    }

    public void SetSpriteData(SpriteData data)
    {
        spriteData = data;
    }
    
    public void SetColliderData(Collider2D[] colliders)
    {
        collidersData = new ColliderData[colliders.Length];
        if (colliders != null)
        {
            for (int colliderIndex = 0; colliderIndex < colliders.Length; ++colliderIndex)
            {
                collidersData[colliderIndex] = new ColliderData(colliders[colliderIndex]);
            }
        }
    }

    public void SetLight2DData(Light2dData data)
    {
        lightData = data;
    }

    public void SetLevelSwitcherData(LevelSwitcherData data)
    {
        levelSwitcherData = data;
    }

    public void SetLayer(int objLayer)
    {
        layer = objLayer;
    }

    public void SetData(GameObject obj, string objSubtype)
    {
        name = obj.name;
        parentName = obj.transform.parent.name;

        position.x = obj.transform.position.x;
        position.y = obj.transform.position.y;
        position.z = obj.transform.position.z;

        rotation.x = obj.transform.rotation.x;
        rotation.y = obj.transform.rotation.y;
        rotation.z = obj.transform.rotation.z;
        rotation.w = obj.transform.rotation.w;

        scale.x = obj.transform.localScale.x;
        scale.y = obj.transform.localScale.y;
        scale.z = obj.transform.localScale.z;

        type = StringParserWrapper.GetObjectTypeFromSubType(objSubtype);
        subType = objSubtype;
        isActive = obj.activeInHierarchy;

        SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteData = new SpriteData(spriteRenderer);
        }
        else
        {
            spriteData = new SpriteData();
        }

        //*
        Collider2D[] colliders;
        colliders = obj.GetComponentsInChildren<Collider2D>();
        collidersData = new ColliderData[colliders.Length];

        if (colliders != null)
        {
            for (int colliderIndex = 0; colliderIndex < colliders.Length; ++colliderIndex)
            {
                if (colliders[colliderIndex] is BoxCollider2D)
                {
                    collidersData[colliderIndex] =
                            new BoxColliderData(colliders[colliderIndex].GetComponent<BoxCollider2D>());
                }
                else if (colliders[colliderIndex] is CircleCollider2D)
                {
                    collidersData[colliderIndex] =
                            new CircleColliderData(colliders[colliderIndex].GetComponent<CircleCollider2D>());
                }
                else if (colliders[colliderIndex] is PolygonCollider2D)
                {
                    collidersData[colliderIndex] =
                            new PolygonColliderData(colliders[colliderIndex].GetComponent<PolygonCollider2D>());
                }
                else if (colliders[colliderIndex] is CapsuleCollider2D)
                {
                    collidersData[colliderIndex] =
                            new CapsuleColliderData(colliders[colliderIndex].GetComponent<CapsuleCollider2D>());
                }
                else
                {
                    collidersData[colliderIndex] = new ColliderData(colliders[colliderIndex]);
                }
            }
        }

        Light2D light2D = obj.GetComponent<Light2D>();

        if (light2D != null)
        {
            lightData.SetData(light2D);
        }
        else
        {
            lightData = new Light2dData();
        }

        //*/

        LevelSwitcher lvlSwitcher = obj.GetComponent<LevelSwitcher>();
        if (lvlSwitcher != null)
        {
            levelSwitcherData = new LevelSwitcherData(lvlSwitcher.newLevelName);
        }
        else
        {
            levelSwitcherData = new LevelSwitcherData();
        }

        layer = obj.layer;
    }

    public XmlSchema GetSchema()
    {
        return (null);
    }

    public void ReadXml(XmlReader reader)
    {
        reader.ReadStartElement("ObjectData");

        {
            reader.ReadStartElement("ObjectName");
            name = reader.ReadString();
            reader.ReadEndElement();

            reader.ReadStartElement("ObjectParentName");
            parentName = reader.ReadString();
            reader.ReadEndElement();

            reader.ReadStartElement("ObjectPositionX");
            position.x = StringParserWrapper.GetFloat(reader.ReadString());
            reader.ReadEndElement();

            reader.ReadStartElement("ObjectPositionY");
            position.y = StringParserWrapper.GetFloat(reader.ReadString());
            reader.ReadEndElement();

            reader.ReadStartElement("ObjectPositionZ");
            position.z = StringParserWrapper.GetFloat(reader.ReadString());
            reader.ReadEndElement();

            reader.ReadStartElement("ObjectRotationX");
            rotation.x = StringParserWrapper.GetFloat(reader.ReadString());
            reader.ReadEndElement();

            reader.ReadStartElement("ObjectRotationY");
            rotation.y = StringParserWrapper.GetFloat(reader.ReadString());
            reader.ReadEndElement();

            reader.ReadStartElement("ObjectRotationZ");
            rotation.z = StringParserWrapper.GetFloat(reader.ReadString());
            reader.ReadEndElement();

            reader.ReadStartElement("ObjectRotationW");
            rotation.w = StringParserWrapper.GetFloat(reader.ReadString());
            reader.ReadEndElement();

            reader.ReadStartElement("ObjectScaleX");
            scale.x = StringParserWrapper.GetFloat(reader.ReadString());
            reader.ReadEndElement();

            reader.ReadStartElement("ObjectScaleY");
            scale.y = StringParserWrapper.GetFloat(reader.ReadString());
            reader.ReadEndElement();

            reader.ReadStartElement("ObjectScaleZ");
            scale.z = StringParserWrapper.GetFloat(reader.ReadString());
            reader.ReadEndElement();

            reader.ReadStartElement("ObjectType");
            type = reader.ReadString();
            reader.ReadEndElement();

            reader.ReadStartElement("ObjectSubtype");
            subType = reader.ReadString();
            reader.ReadEndElement();

            reader.ReadStartElement("ObjectIsActive");
            isActive = StringParserWrapper.GetBool(reader.ReadString());
            reader.ReadEndElement();

            // Sprite Data
            spriteData.ReadXml(reader);

            //*
            //Collider Data
            reader.ReadStartElement("CollidersList");

            {
                reader.ReadStartElement("CollidersCount");
                int colliderCount = StringParserWrapper.GetInt(reader.ReadString());
                reader.ReadEndElement();

                collidersData = new ColliderData[colliderCount];

                for (int colliderIndex = 0; colliderIndex < colliderCount; ++colliderIndex)
                {
                    reader.ReadStartElement("ColliderData");

                    reader.ReadStartElement("ColliderType");
                    ColliderType colliderType = StringParserWrapper.GetEnumColliderType(reader.ReadString());
                    reader.ReadEndElement();

                    switch (colliderType)
                    {
                        case ColliderType.BOX:
                            collidersData[colliderIndex] = new BoxColliderData();
                            break;
                        case ColliderType.CIRCLE:
                            collidersData[colliderIndex] = new CircleColliderData();
                            break;
                        case ColliderType.POLYGON:
                            collidersData[colliderIndex] = new PolygonColliderData();
                            break;
                        case ColliderType.CAPSULE:
                            collidersData[colliderIndex] = new CapsuleColliderData();
                            break;
                        default:
                            break;
                    }
                    collidersData[colliderIndex].SetColliderType(colliderType);
                    collidersData[colliderIndex].ReadXml(reader);

                    reader.ReadEndElement();
                }
            }

            reader.ReadEndElement();
            //*/

            //Light2D Data 

            lightData.ReadXml(reader);

            // Level Switcher Data
            levelSwitcherData.ReadXml(reader);

            reader.ReadStartElement("ObjectLayer");
            layer = StringParserWrapper.GetInt(reader.ReadString());
            reader.ReadEndElement();
        }

        reader.ReadEndElement();
    }

    public void WriteXml(XmlWriter writer)
    {
        writer.WriteStartElement("ObjectName");
        writer.WriteString(name.ToString());
        writer.WriteEndElement();
        
        writer.WriteStartElement("ObjectParentName");
        writer.WriteString(parentName.ToString());
        writer.WriteEndElement();

        writer.WriteStartElement("ObjectPositionX");
        writer.WriteString(position.x.ToString());
        writer.WriteEndElement();

        writer.WriteStartElement("ObjectPositionY");
        writer.WriteString(position.y.ToString());
        writer.WriteEndElement();

        writer.WriteStartElement("ObjectPositionZ");
        writer.WriteString(position.z.ToString());
        writer.WriteEndElement();

        writer.WriteStartElement("ObjectRotationX");
        writer.WriteString(rotation.x.ToString());
        writer.WriteEndElement();

        writer.WriteStartElement("ObjectRotationY");
        writer.WriteString(rotation.y.ToString());
        writer.WriteEndElement();

        writer.WriteStartElement("ObjectRotationZ");
        writer.WriteString(rotation.z.ToString());
        writer.WriteEndElement();

        writer.WriteStartElement("ObjectRotationW");
        writer.WriteString(rotation.w.ToString());
        writer.WriteEndElement();

        writer.WriteStartElement("ObjectScaleX");
        writer.WriteString(scale.x.ToString());
        writer.WriteEndElement();

        writer.WriteStartElement("ObjectScaleY");
        writer.WriteString(scale.y.ToString());
        writer.WriteEndElement();

        writer.WriteStartElement("ObjectScaleZ");
        writer.WriteString(scale.z.ToString());
        writer.WriteEndElement();

        writer.WriteStartElement("ObjectType");
        writer.WriteString(type.ToString());
        writer.WriteEndElement();

        writer.WriteStartElement("ObjectSubtype");
        writer.WriteString(subType.ToString());
        writer.WriteEndElement();

        writer.WriteStartElement("ObjectIsActive");
        writer.WriteString(isActive.ToString());
        writer.WriteEndElement();

        writer.WriteStartElement("SpriteData");
        spriteData.WriteXml(writer);
        writer.WriteEndElement();

        //*
        writer.WriteStartElement("CollidersList");

        {
            writer.WriteStartElement("CollidersCount");
            writer.WriteString(collidersData.Length.ToString());
            writer.WriteEndElement();

            for (int colliderIndex = 0; colliderIndex < collidersData.Length; ++colliderIndex)
            {
                writer.WriteStartElement("ColliderData");
                collidersData[colliderIndex].WriteXml(writer);
                writer.WriteEndElement();
            }
        }

        writer.WriteEndElement();
        //*/

        writer.WriteStartElement("Light2dData");
        lightData.WriteXml(writer);
        writer.WriteEndElement();

        //*/

        writer.WriteStartElement("LevelSwitcherData");
        levelSwitcherData.WriteXml(writer);
        writer.WriteEndElement();

        writer.WriteStartElement("ObjectLayer");
        writer.WriteString(layer.ToString());
        writer.WriteEndElement();
    }
}
