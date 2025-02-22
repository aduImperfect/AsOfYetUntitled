using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(IndividualCharacterManager))]
public class IndividualCharacterManagerScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        IndividualCharacterManager myTarget = (IndividualCharacterManager)target;

        if (GUILayout.Button("Save Character"))
        {
            myTarget.SaveCharacter();
        }

        if (GUILayout.Button("Update Character From File"))
        {
            myTarget.UpdateCharacterFromFile();

        }

        if (GUILayout.Button("Parse States"))
        {
            myTarget.RequestStateParse();
        }

        GUILayout.Label("PRIMARY STATE CHANGE QUEUE");
        myTarget.CheckForMyStateMachine();

        CharacterStateMachineManager stateMachine = myTarget.GetCharacterStateMachineManager();

        StateData[] stateChangeArray = stateMachine.GetCurrentStateChangeQueue().ToArray();
        for (int i = 0; i < stateChangeArray.Length; ++i)
        {
            GUILayout.Label(stateChangeArray[i].GetPrimaryState().ToString());
        }

        GUILayout.Space(5);

        GUILayout.Label("SECONDARY STATE CHANGE QUEUE");
        for (int i = 0; i < stateChangeArray.Length; ++i)
        {
            GUILayout.Label(stateChangeArray[i].GetSecondaryState().ToString());
        }

        GUILayout.Space(5);

        if (GUILayout.Button("ClearQueue"))
        {
            stateMachine.ResetStateQueue();
        }

        //GUILayout.Space(5);
        //GUILayout.Label("PRIMARY STATES");

        //for (int i = 0; i < (int)PrimaryState.PRIMARYSTATE_COUNT; ++i)
        //{
        //    PrimaryState currPrimaryState = (PrimaryState)i;
        //    if (GUILayout.Button("Add " + currPrimaryState.ToString() + " Queue"))
        //    {
        //        myTarget.AddStateDataToQueue();
        //    }
        //}

        //GUILayout.Label("SECONDARY STATES");

        //for (int i = 0; i < (int)SecondaryState.SECONDARYSTATE_COUNT; ++i)
        //{
        //    SecondaryState currSecondaryState = (SecondaryState)i;
        //    if (GUILayout.Button("Add " + currSecondaryState.ToString() + " Queue"))
        //    {
        //        myTarget.AddSecondaryState(i);
        //    }
        //}

        //if (GUILayout.Button("Try Setting RandomStates"))
        //{
        //    myTarget.TrySetRandomStates();
        //}
    }
}
