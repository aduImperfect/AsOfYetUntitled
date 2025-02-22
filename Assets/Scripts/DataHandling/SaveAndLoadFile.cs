using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices.ComTypes;

public static class SaveAndLoadFile
{
    /// <summary>
    /// Serializes an object.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="serializableObject"></param>
    /// <param name="fileName"></param>
    public static void SerializeObject<T>(T serializableObject, string fileName)
    {
        if (serializableObject == null) { return; }

        try
        {
            XmlDocument xmlDocument = new XmlDocument();
            XmlSerializer serializer = new XmlSerializer(serializableObject.GetType());
            using (MemoryStream stream = new MemoryStream())
            {
                serializer.Serialize(stream, serializableObject);
                stream.Position = 0;
                xmlDocument.Load(stream);
                xmlDocument.Save(fileName);
            }
        }
        catch (Exception ex)
        {
            //Log exception here
            UnityEngine.Debug.LogError(ex);
        }
    }

    /// <summary>
    /// De Serializes an xml file into an object list
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static T DeSerializeObject<T>(string fileName)
    {
        T objectOut = default(T);

        if (string.IsNullOrEmpty(fileName)) { return objectOut; }

        try
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(fileName);
            string xmlString = xmlDocument.OuterXml;

            using (StringReader read = new StringReader(xmlString))
            {
                Type outType = typeof(T);

                XmlSerializer serializer = new XmlSerializer(outType);
                using (XmlReader reader = new XmlTextReader(read))
                {
                    objectOut = (T)serializer.Deserialize(reader);
                }
            }
        }
        catch (Exception ex)
        {
            //Log exception here
            UnityEngine.Debug.LogError(ex);
        }

        return objectOut;
    }

    public static Task<T> DeSerializeObjectAsync<T>(string fileName)
    {
        T objectOut = default(T);

        if (string.IsNullOrEmpty(fileName)) { return Task.FromResult(objectOut); }

        try
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(fileName);
            string xmlString = xmlDocument.OuterXml;

            using (StringReader read = new StringReader(xmlString))
            {
                Type outType = typeof(T);

                XmlSerializer serializer = new XmlSerializer(outType);
                using (XmlReader reader = new XmlTextReader(read))
                {
                    objectOut = (T)serializer.Deserialize(reader);
                }
            }
        }
        catch (Exception)
        {
            //Log exception here
        }

        return Task.FromResult(objectOut);

    }
}