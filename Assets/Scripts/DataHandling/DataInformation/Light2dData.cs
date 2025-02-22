using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using LightType = UnityEngine.Rendering.Universal.Light2D.LightType;

public class Light2dData : IXmlSerializable
{
    [XmlAttribute("LightType")]
    LightType lightType;

    [XmlAttribute("LightColor")]
    Color lightColor;

    [XmlAttribute("LightIntensity")]
    float lightIntensity;

    [XmlAttribute("LightFalloff")]
    float lightFalloff;

    [XmlAttribute("LightFalloffStrength")]
    float lightFalloffStrength;

    [XmlAttribute("LightTargetSortingLayers")]
    int[] lightTargetSortingLayers;
    
    [XmlAttribute("LightPoints")]
    Vector3[] lightPoints;
    
    [XmlAttribute("LightShadowEnabled")]
    bool lightShadowEnabled;
    
    [XmlAttribute("LightShadowIntensity")]
    float lightShadowIntensity;

    public Light2dData()
    {
        //parametric light type is deprecated hence used as invalid
        lightType = LightType.Parametric;
        lightColor = Color.black;
        lightIntensity = -1;
        lightFalloff = -1;
        lightFalloffStrength = -1;
        lightTargetSortingLayers = new int[0];
        lightPoints = new Vector3[0];
        lightShadowEnabled = false;
        lightShadowIntensity = -1;
    }

    public Light2dData(Light2D light)
    {
        lightType = light.lightType;
        lightColor = light.color;
        lightIntensity = light.intensity;
        lightFalloff = light.falloffIntensity;
        lightFalloffStrength = light.shapeLightFalloffSize;
        lightTargetSortingLayers = light.m_ApplyToSortingLayers;
        lightPoints = light.shapePath;
        lightShadowEnabled = light.shadowsEnabled;
        lightShadowIntensity = light.shadowIntensity;
    }

    public void SetData(Light2D light)
    {
        lightType = light.lightType;
        lightColor = light.color;
        lightIntensity = light.intensity;
        lightFalloff = light.falloffIntensity;
        lightFalloffStrength = light.shapeLightFalloffSize;
        lightTargetSortingLayers = light.m_ApplyToSortingLayers;
        lightPoints = light.shapePath;
        lightShadowEnabled = light.shadowsEnabled;
        lightShadowIntensity = light.shadowIntensity;
    }

    public void SetDataToLight2D(ref Light2D light)
    {
        if (GetLightType().ToString().Equals("NULL"))
        {
            light.lightType = LightType.Parametric;
            light.color = Color.black;
            light.intensity = -1;
            light.falloffIntensity = -1;
            light.shapeLightFalloffSize = -1;
            light.m_ApplyToSortingLayers = new int[0];
            light.SetShapePath(new Vector3[0]);
            return;
            //light.lightType = LightType.Freeform;
        }
        else
        {
            light.lightType = GetLightType();
        }

        light.color = GetLightColor();
        light.intensity = GetLightIntensity();
        light.falloffIntensity = GetLightFalloff();
        light.shapeLightFalloffSize = GetLightFalloffStrength();
        light.m_ApplyToSortingLayers = GetLightTargetSortingLayers();
        light.SetShapePath(GetLightPoints());
        light.shadowsEnabled = GetLightShadowEnabled();
        light.shadowIntensity = GetLightShadowIntensity();
    }

    public LightType GetLightType() => lightType;
    public Color GetLightColor() => lightColor;
    public float GetLightIntensity() => lightIntensity;
    public float GetLightFalloff() => lightFalloff;
    public float GetLightFalloffStrength() => lightFalloffStrength;
    public int[] GetLightTargetSortingLayers() => lightTargetSortingLayers;
    public Vector3[] GetLightPoints() => lightPoints;
    public bool GetLightShadowEnabled() => lightShadowEnabled;
    public float GetLightShadowIntensity() => lightShadowIntensity;


    private void SetLightType(LightType lightType) 
    { this.lightType = lightType; }
    private void SetLightColor(Color color) 
    { this.lightColor = color; }
    private void SetLightIntensity(float intensity) 
    { this.lightIntensity = intensity; }
    private void SetLightFalloff(float fallOff) 
    { this.lightFalloff = fallOff; }
    private void SetLightFalloffStrength(float fallOffStrength) 
    { this.lightFalloffStrength = fallOffStrength; }
    private void SetLightTargetSortingLayers(int[] lightSortingLayers) 
    { this.lightTargetSortingLayers = lightSortingLayers; }
    private void SetLightPoints(Vector3[] lightPoints)
    {
        this.lightPoints = lightPoints;
    }
    private void SetLightShadowEnabled(bool shadowEnabled)
    {
        this.lightShadowEnabled = shadowEnabled;
    }
    private void SetLightShadowIntensity(float intensity)
    {
        this.lightShadowIntensity = intensity;
    }

    public XmlSchema GetSchema() => null;

    public void ReadXml(XmlReader reader)
    {
        reader.ReadStartElement("Light2dData");

        reader.ReadStartElement("LightType");
        SetLightType(StringParserWrapper.GetEnumLightType(reader.ReadString()));
        reader.ReadEndElement();

        reader.ReadStartElement("LightColor");
        SetLightColor(StringParserWrapper.GetColor(reader.ReadString()));
        reader.ReadEndElement();

        reader.ReadStartElement("LightIntensity");
        SetLightIntensity(StringParserWrapper.GetFloat(reader.ReadString()));
        reader.ReadEndElement();

        reader.ReadStartElement("LightFalloff");
        SetLightFalloff(StringParserWrapper.GetFloat(reader.ReadString()));
        reader.ReadEndElement();

        reader.ReadStartElement("LightFalloffStrength");
        SetLightFalloffStrength(StringParserWrapper.GetFloat(reader.ReadString()));
        reader.ReadEndElement();

        reader.ReadStartElement("LightPointsList");
        {
            reader.ReadStartElement("LightPointsCount");
            int lightPointsCount = StringParserWrapper.GetInt(reader.ReadString());
            reader.ReadEndElement();

            Vector3[] lightPPs = new Vector3[lightPointsCount];

            for(int i = 0; i < lightPointsCount; ++i)
            {
                reader.ReadStartElement("LightPositionX");
                lightPPs[i].x = StringParserWrapper.GetFloat(reader.ReadString());
                reader.ReadEndElement();
                
                reader.ReadStartElement("LightPositionY");
                lightPPs[i].y = StringParserWrapper.GetFloat(reader.ReadString());
                reader.ReadEndElement();
                
                reader.ReadStartElement("LightPositionZ");
                lightPPs[i].z = StringParserWrapper.GetFloat(reader.ReadString());
                reader.ReadEndElement();
            }
            SetLightPoints(lightPPs);
        }
        reader.ReadEndElement();

        reader.ReadStartElement("LightlayersList");
        {
            reader.ReadStartElement("LightLayersCount");
            int lightLayersCount = StringParserWrapper.GetInt(reader.ReadString());
            reader.ReadEndElement();

            int[] lightLayersPP = new int[lightLayersCount];
            for (int i = 0; i < lightLayersCount; ++i)
            {
                reader.ReadStartElement("LightLayer");
                lightLayersPP[i] = StringParserWrapper.GetInt(reader.ReadString());
                reader.ReadEndElement();
            }
            SetLightTargetSortingLayers(lightLayersPP);
        }
        reader.ReadEndElement();

        reader.ReadStartElement("LightShadowEnabled");
        SetLightShadowEnabled(StringParserWrapper.GetBool(reader.ReadString()));
        reader.ReadEndElement();
        
        reader.ReadStartElement("LightShadowIntensity");
        SetLightShadowIntensity(StringParserWrapper.GetFloat(reader.ReadString()));
        reader.ReadEndElement();

        reader.ReadEndElement();
    }

    public void WriteXml(XmlWriter writer)
    {
        writer.WriteStartElement("LightType");
        string lightTypeStr = lightType.ToString();
        if (lightType == LightType.Parametric)
        {
            lightTypeStr = "NULL";
        }
        writer.WriteString(lightTypeStr);
        writer.WriteEndElement();

        writer.WriteStartElement("LightColor");
        writer.WriteString(lightColor.ToString());
        writer.WriteEndElement();

        writer.WriteStartElement("LightIntensity");
        writer.WriteString(lightIntensity.ToString());
        writer.WriteEndElement();

        writer.WriteStartElement("LightFalloff");
        writer.WriteString(lightFalloff.ToString());
        writer.WriteEndElement();
        
        writer.WriteStartElement("LightFalloffStrength");
        writer.WriteString(lightFalloffStrength.ToString());
        writer.WriteEndElement();

        writer.WriteStartElement("LightPointsList");
        {
            writer.WriteStartElement("LightPointsCount");
            writer.WriteString(lightPoints.Length.ToString());
            writer.WriteEndElement();

            for (int i = 0; i < lightPoints.Length; ++i)
            {
                writer.WriteStartElement("LightPositionX");
                writer.WriteString(lightPoints[i].x.ToString());
                writer.WriteEndElement();
                
                writer.WriteStartElement("LightPositionY");
                writer.WriteString(lightPoints[i].y.ToString());
                writer.WriteEndElement();
                
                writer.WriteStartElement("LightPositionZ");
                writer.WriteString(lightPoints[i].z.ToString());
                writer.WriteEndElement();
            }
        }
        writer.WriteEndElement();

        writer.WriteStartElement("LightlayersList");
        {
            writer.WriteStartElement("LightLayersCount");
            writer.WriteString(lightTargetSortingLayers.Length.ToString());
            writer.WriteEndElement();

            for (int i = 0; i < lightTargetSortingLayers.Length; ++i)
            {
                writer.WriteStartElement("LightLayer");
                writer.WriteString(lightTargetSortingLayers[i].ToString());
                writer.WriteEndElement();
            }
        }
        writer.WriteEndElement();

        writer.WriteStartElement("LightShadowEnabled");
        writer.WriteString(lightShadowEnabled.ToString());
        writer.WriteEndElement();
        
        writer.WriteStartElement("LightShadowIntensity");
        writer.WriteString(lightShadowIntensity.ToString());
        writer.WriteEndElement();
    }
}
