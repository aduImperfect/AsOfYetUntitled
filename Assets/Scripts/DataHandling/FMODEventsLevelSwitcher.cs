using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODEventsLevelSwitcher : MonoBehaviour
{
    [field: Header("Narrative")]
    [field: SerializeField] public EventReference narrative { get; private set; }

    [field: Header("Ambience")]
    [field: SerializeField] public EventReference ambience { get; private set; }

    [field: Header("Music")]
    [field: SerializeField] public EventReference music { get; private set; }
    public static FMODEventsLevelSwitcher instance { get; private set; }

    private void Awake()
    {

        if (instance != null)
        {
            UnityEngine.Debug.LogError("Multiple FMOD Events Found at " + instance.gameObject.name);
            return;
        }

        instance = this;

        //InitializeFMODMains();
    }

    private void Start()
    {
        InitializeFMODMains();
    }

    private void InitializeFMODMains()
    {
        FMODEvents.instance.ambience = ambience;
        FMODEvents.instance.music = music;
        FMODEvents.instance.narrative = narrative;

        SoundSystemManager.instance.InitializeOnSceneUpdate();
    }
}
