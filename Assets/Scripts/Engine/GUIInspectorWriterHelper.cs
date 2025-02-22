using System.IO;
using UnityEngine;

public static class GUIInspectorWriterHelper
{
    public static void WriteTitle(string titleString)
    {
        GUILayout.Label(titleString);
    }

    public static void WriteSectionHeader(string sectionString)
    {
        GUILayout.Label(sectionString);
    }

    public static void WriteParagraph(string paraString)
    {
        GUILayout.Label(paraString);
    }

    public static void DrawDivider()
    {
        GUILayout.Label("-------------------------------------------------------------------");
    }

    public static void InsertSpacer(uint spacerInt = 10)
    {
        GUILayout.Space(spacerInt);
    }

    public static string InsertNewLine()
    {
        return "\r\n";
    }

    public static void DrawImage(string fullPath)
    {
        Sprite spr = Resources.Load<Sprite>(fullPath);

        if (spr == null)
        {
            return;
        }

        Texture imageTex = spr.texture;

        if (imageTex == null)
        {
            return;
        }

        GUILayout.Label(imageTex);
    }
}
