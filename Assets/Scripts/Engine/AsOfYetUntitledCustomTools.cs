using UnityEditor;
using UnityEngine;

public class AsOfYetUntitledCustomTools : MonoBehaviour
{
    //[MenuItem()]
    //CUSTOM MENU ITEMS HERE
    //IF U WANT TO LOOK FOR OTHER MENU ITEMS

    #region LevelObjectsCreator Tools

    [MenuItem("AsOfYetUntitledCustomTools/LevelObjectsCreator/ShiftSelectedLevelObjectsToDifferentArea")]
    public static void ShiftSelectedLevelObjectsToDifferentArea()
    {
        GameObject levelObjectsCreatorObject = GameObject.Find("LevelObjectsCreator");
        LevelObjectsCreator levelObjectsCreator = levelObjectsCreatorObject.GetComponent<LevelObjectsCreator>();

        levelObjectsCreator.ShiftSelectedLevelObjectsToArea();
    }

    #endregion
}
