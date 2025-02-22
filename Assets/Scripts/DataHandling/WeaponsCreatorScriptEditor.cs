using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WeaponsCreator))]
public class WeaponsCreatorScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        WeaponsCreator myTarget = (WeaponsCreator)target;

        GUILayout.Space(20);

        if (GUILayout.Button("Clear Weapons Storage"))
        {
            myTarget.ClearWeapons();
        }

        if (GUILayout.Button("Add Weapon"))
        {
            myTarget.AddWeapon();
        }

        if (GUILayout.Button("Remove Weapon"))
        {
            myTarget.RemoveWeapon();
        }

        GUILayout.Space(20);

        if (GUILayout.Button("Add Additional Trait To Current Weapon"))
        {
            myTarget.AddAdditionalTraitToCurrentWeapon();
        }

        if (GUILayout.Button("Add Additional Special Damage To Current Weapon"))
        {
            myTarget.AddAdditionalSpecialDamageToCurrentWeapon();
        }

        if (GUILayout.Button("Add Additional Attack Timing To Current Weapon"))
        {
            myTarget.AddAdditionalAttackTimingToCurrentWeapon();
        }

        if (GUILayout.Button("Add Additional Contact Frame To Current Weapon"))
        {
            myTarget.AddAdditionalContactFrameToCurrentWeapon();
        }

        if (GUILayout.Button("Set New Durability To Current Weapon"))
        {
            myTarget.SetNewDurabilityToCurrentWeapon();
        }

        if (GUILayout.Button("Set New Sprite To Current Weapon"))
        {
            myTarget.SetNewSpriteToCurrentWeapon();
        }

        GUILayout.Space(20);

        GUILayout.Label("ANY WEAPON SAVE DOES A COPY SAVE IN _WEAPONS DUMP FOLDER (DATE & TIME WARE)");
        if (GUILayout.Button("Save Weapon"))
        {
            myTarget.SaveWeapon();
        }

        if (GUILayout.Button("Save All Weapons"))
        {
            myTarget.SaveAllWeapons();
        }

        GUILayout.Space(20);

        if (GUILayout.Button("Load Weapon"))
        {
            myTarget.LoadWeapon();
        }

        if (GUILayout.Button("Load All Weapons"))
        {
            myTarget.LoadAllWeapons();
        }
    }
}
