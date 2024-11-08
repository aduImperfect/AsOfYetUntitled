using System;
using System.Linq;
using UnityEngine;

public static class StringParserWrapper
{
    public static int GetInt(string strValue)
    {
        int intVal;
        Int32.TryParse(strValue, out intVal);
        return intVal;
    }

    public static PathfindingTerrainType GetEnumPathfindingTerrainType(string strValue)
    {
        PathfindingTerrainType enumVal;
        Enum.TryParse(strValue, out enumVal);
        return enumVal;
    }

    public static Vector3 GetVector3(string strValue)
    {
        float[] newCoord = strValue.Split(new string[] { ", ", "(", ")" }, StringSplitOptions.RemoveEmptyEntries).Select(x => float.Parse(x)).ToArray();

        Vector3 newVec = Vector3.zero;
        newVec.x = newCoord[0];
        newVec.y = newCoord[1];
        newVec.z = newCoord[2];

        return newVec;
    }

    public static Quaternion GetQuaternion(string strValue)
    {
        float[] newCoord = strValue.Split(new string[] { ", ", "(", ")" }, StringSplitOptions.RemoveEmptyEntries).Select(x => float.Parse(x)).ToArray();

        Quaternion newQuat = Quaternion.identity;
        newQuat.x = newCoord[0];
        newQuat.y = newCoord[1];
        newQuat.z = newCoord[2];
        newQuat.w = newCoord[3];
        return newQuat;
    }
}
