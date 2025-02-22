using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using LightType = UnityEngine.Rendering.Universal.Light2D.LightType;

public static class StringParserWrapper
{
    public static int GetInt(string strValue)
    {
        int intVal;
        Int32.TryParse(strValue, out intVal);
        return intVal;
    }
    public static uint GetUInt(string strValue)
    {
        uint uintVal;
        UInt32.TryParse(strValue, out uintVal);
        return uintVal;
    }

    public static float GetFloat(string strValue)
    {
        float floatVal;
        float.TryParse(strValue, out floatVal);
        return floatVal;
    }

    public static double GetDouble(string strValue)
    {
        double doubleVal;
        double.TryParse(strValue, out doubleVal);
        return doubleVal;
    }

    public static ColliderType GetEnumColliderType(string strValue)
    {
        ColliderType enumVal;
        Enum.TryParse(strValue, out enumVal);
        return enumVal;
    }

    public static LightType GetEnumLightType(string strValue)
    {
        LightType enumVal;
        Enum.TryParse(strValue, out enumVal);
        return enumVal;
    }
    
    public static CharacterMode GetEnumCharacterMode(string strValue)
    {
        CharacterMode enumVal;
        Enum.TryParse(strValue, out enumVal);
        return enumVal;
    }

    public static HumanType GetEnumHumanType(string strValue)
    {
        HumanType enumVal;
        Enum.TryParse(strValue, out enumVal);
        return enumVal;
    }
    
    public static BodyType GetEnumBodyType(string strValue)
    {
        BodyType enumVal;
        Enum.TryParse(strValue, out enumVal);
        return enumVal;
    }

    public static SkinType GetEnumSkinType(string strValue)
    {
        SkinType enumVal;
        Enum.TryParse(strValue, out enumVal);
        return enumVal;
    }
    
    public static HairType GetEnumHairType(string strValue)
    {
        HairType enumVal;
        Enum.TryParse(strValue, out enumVal);
        return enumVal;
    }
    
    public static InteractingSubstate GetEnumInteractingSubstate(string strValue)
    {
        InteractingSubstate enumVal;
        Enum.TryParse(strValue, out enumVal);
        return enumVal;
    }
    
    public static DashingSubstate GetEnumDashingSubstate(string strValue)
    {
        DashingSubstate enumVal;
        Enum.TryParse(strValue, out enumVal);
        return enumVal;
    }
    
    public static ThrowingSubstate GetEnumThrowingSubstate(string strValue)
    {
        ThrowingSubstate enumVal;
        Enum.TryParse(strValue, out enumVal);
        return enumVal;
    }
    
    public static AttackingSubstate GetEnumAttackingSubstate(string strValue)
    {
        AttackingSubstate enumVal;
        Enum.TryParse(strValue, out enumVal);
        return enumVal;
    }
    
    public static CinematicSubstate GetEnumCinematicSubstate(string strValue)
    {
        CinematicSubstate enumVal;
        Enum.TryParse(strValue, out enumVal);
        return enumVal;
    }

    public static WeaponType GetEnumWeaponType(string strValue)
    {
        WeaponType enumVal;
        Enum.TryParse(strValue, out enumVal);
        return enumVal;
    }

    public static WeaponTraitType GetEnumWeaponTraitType(string strValue)
    {
        WeaponTraitType enumVal;
        Enum.TryParse(strValue, out enumVal);
        return enumVal;
    }

    public static DamageType GetEnumWeaponDamageType(string strValue)
    {
        DamageType enumVal;
        Enum.TryParse(strValue, out enumVal);
        return enumVal;
    }

    public static SpecialDamageType GetEnumWeaponSpecialDamageType(string strValue)
    {
        SpecialDamageType enumVal;
        Enum.TryParse(strValue, out enumVal);
        return enumVal;
    }

    public static AttackTimingType GetEnumWeaponAttackTimingType(string strValue)
    {
        AttackTimingType enumVal;
        Enum.TryParse(strValue, out enumVal);
        return enumVal;
    }

    public static PrimaryState GetEnumPrimaryState(string strValue)
    {
        PrimaryState enumVal;
        Enum.TryParse(strValue, out enumVal);
        return enumVal;
    }

    public static SecondaryState GetEnumSecondaryState(string strValue)
    {
        SecondaryState enumVal;
        Enum.TryParse(strValue, out enumVal);
        return enumVal;
    }

    public static TEnum GetEnumValue<TEnum>(string strValue) where TEnum : struct
    {
        if (Enum.TryParse<TEnum>(strValue, false, out TEnum result))
        {
            return result;
        }
        else
        {
            throw new Exception("Enum not defined!!");
        }
    }

    public static bool GetBool(string strValue)
    {
        bool value;
        Boolean.TryParse(strValue, out value);
        return value;
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
    
    public static Vector2 GetVector2(string strValue)
    {
        float[] newCoord = strValue.Split(new string[] { ", ", "(", ")" }, StringSplitOptions.RemoveEmptyEntries).Select(x => float.Parse(x)).ToArray();

        Vector2 newVec = Vector3.zero;
        newVec.x = newCoord[0];
        newVec.y = newCoord[1];

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

    public static Color GetColor(string strValue)
    {
        string[] rgba = strValue.Substring(5, strValue.Length - 6).Split(", ");
        Color newColor = new Color(float.Parse(rgba[0]), float.Parse(rgba[1]), float.Parse(rgba[2]), float.Parse(rgba[3]));
        return newColor;
    }

    public static string GetObjectTypeFromSubType(string type)
    {
        string[] objSubTypeStrSplitter = type.Split(new string[] { "_" }, StringSplitOptions.RemoveEmptyEntries);
        return objSubTypeStrSplitter[0];
    }

    public static Sprite GetSpriteFromString(string strValue)
    {
        Sprite spriteVal = null;
        string[] assetPaths = AssetDatabase.FindAssets(strValue); // Search for assets with the given name.

        foreach (string path in assetPaths)
        {
            // Get the full path to the asset
            string assetPath = AssetDatabase.GUIDToAssetPath(path);

            // Check if the asset is a Sprite
            if (AssetDatabase.GetMainAssetTypeAtPath(assetPath) == typeof(Sprite))
            {
                spriteVal = AssetDatabase.LoadAssetAtPath<Sprite>(assetPath); // Return the Sprite object
                break;
            }
        }

        return spriteVal;
    }

    public static PathfindingTerrainType GetEnumPathfindingTerrainType(string strValue)
    {
        PathfindingTerrainType enumVal;
        Enum.TryParse(strValue, out enumVal);
        return enumVal;
    }
}
