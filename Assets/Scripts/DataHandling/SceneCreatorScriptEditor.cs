using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.UIElements;

[CustomEditor(typeof(SceneCreator))]
public class SceneCreatorScriptEditor : Editor
{
    private int counter = 0;

    public void DrawSceneCreatorImage(string imageName)
    {
        string spriteImgFullPath = Path.Combine(FilePaths.GetFullSceneCreatorImagesPath(), imageName);

        if (spriteImgFullPath == null)
        {
            return;
        }

        GUIInspectorWriterHelper.DrawImage(spriteImgFullPath);
    }

    public override void OnInspectorGUI()
    {
        counter = 0;

        GUIInspectorWriterHelper.WriteTitle("WELCOME TO THE SCENE CREATOR SYSTEM");

        GUIInspectorWriterHelper.DrawDivider();

        GUIInspectorWriterHelper.WriteSectionHeader("PREFAB LOCATION");
        GUIInspectorWriterHelper.WriteParagraph("This prefab object is available under:" + GUIInspectorWriterHelper.InsertNewLine() + FilePaths.GetFullPrefabSceneObjectsPath());
        DrawSceneCreatorImage((++counter).ToString());

        GUIInspectorWriterHelper.DrawDivider();

        GUIInspectorWriterHelper.WriteSectionHeader("CREATOR CATEGORIES");
        GUIInspectorWriterHelper.WriteParagraph("There are currently 3 types of creators (child objects) available:" + GUIInspectorWriterHelper.InsertNewLine() + "1. CharacterObjectsCreator" + 
            GUIInspectorWriterHelper.InsertNewLine() + "2. LevelObjectsCreator" + 
            GUIInspectorWriterHelper.InsertNewLine() + "3. WeaponsCreator");

        GUIInspectorWriterHelper.DrawDivider();

        GUIInspectorWriterHelper.WriteSectionHeader("OBJECT TYPES/ SUBTYPES");
        GUIInspectorWriterHelper.WriteParagraph("Character and Level Objects Creators utilize a common classification" + GUIInspectorWriterHelper.InsertNewLine() + "for their objects: ObjectTypes which are available as prefabs under:" + GUIInspectorWriterHelper.InsertNewLine() + FilePaths.GetFullPrefabObjectTypesPath() + "\\LEVELS");
        DrawSceneCreatorImage((++counter).ToString());

        GUIInspectorWriterHelper.DrawDivider();

        GUIInspectorWriterHelper.WriteSectionHeader("CREATING/REMOVING OBJECT TYPES/SUBTYPES PREFABS");
        GUIInspectorWriterHelper.WriteParagraph("If you would like to create new assets for usage in the scene please" + GUIInspectorWriterHelper.InsertNewLine() + "make sure to do the following steps:");

        GUIInspectorWriterHelper.InsertSpacer();

        GUIInspectorWriterHelper.WriteParagraph("1. Find the Level Generator and scroll to the field named \"Text Field To ADD/REMOVE Level Objects\"");
        
        GUIInspectorWriterHelper.WriteParagraph("2. Type the ObjectType/SubType you want to CREATE/REMOVE");
        DrawSceneCreatorImage((++counter).ToString());
        
        GUIInspectorWriterHelper.WriteParagraph("3. Select the DESIRED OPTION (ADD/REMOVE) to ADD Or Remove the Level Object");

        GUIInspectorWriterHelper.DrawDivider();

        GUIInspectorWriterHelper.WriteSectionHeader("LEVEL OBJECTS");
        GUIInspectorWriterHelper.WriteParagraph("Out of the various ObjectTypes available for us there are " + "presently " + LevelObjectTypesContainer.GetLevelObjectTypesCount() + " Level Object Categories to use." + 
            GUIInspectorWriterHelper.InsertNewLine() + "Among those categories there are a total of " + LevelObjectTypesContainer.GetTotalLevelObjectSubtypesCount() + " Level Object ObjectSubtypes.");

        GUIInspectorWriterHelper.InsertSpacer();

        GUIInspectorWriterHelper.DrawDivider();
    }
}
