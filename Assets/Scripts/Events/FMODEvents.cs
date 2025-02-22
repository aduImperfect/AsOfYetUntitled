using FMOD;
using FMODUnity;
using System;
using System.Collections.Generic;
using UnityEngine;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Temple Emitter SFX")]
    [field: SerializeField]
    public EventReference templeEmitterSFX { get; private set; }

    [field: Header("Cow Emitter SFX")]
    [field: SerializeField]
    public EventReference cowEmitterSFX { get; private set; }

    [field: Header("Bullock Cart Emitter SFX")]
    [field: SerializeField]
    public EventReference bullockCartEmitterSFX { get; private set; }

    [field: Header("Birds Emitter SFX")]
    [field: SerializeField]
    public EventReference birdsEmitterSFX { get; private set; }
    
    [field: Header("Train Tracks Emitter SFX")]
    [field: SerializeField]
    public EventReference trainTracksEmitterSFX { get; private set; }

    [field: Header("Train Horn Emitter SFX")]
    [field: SerializeField]
    public EventReference trainHornEmitterSFX { get; private set; }

    [field: Header("Train Steam Emitter SFX")]
    [field: SerializeField]
    public EventReference trainSteamEmitterSFX { get; private set; }

    [field: Header("Wok Pan Fry Emitter SFX")]
    [field: SerializeField]
    public EventReference wokEmitterSFX { get; private set; }

    [field: Header("Cooker Emitter SFX")]
    [field: SerializeField]
    public EventReference cookerEmitterSFX { get; private set; }

    [field: Header("Banyan Tree Emitter SFX")]
    [field: SerializeField]
    public EventReference banyanTreeEmitterSFX { get; private set; }
    [field: Header("Main Menu Select SFX")]
    [field: SerializeField]
    public EventReference mainMenuSelectSFX { get; private set; }

    [field: Header("Main Menu Click SFX")]
    [field: SerializeField]
    public EventReference mainMenuClickSFX { get; private set; }

    [field: Header("Dodge Roll SFX")]
    [field: SerializeField]
    public EventReference dodgeRollSFX { get; private set; }

    [field: Header("Heavy Hit SFX")]
    [field: SerializeField]
    public EventReference heavyHitSFX { get; private set; }

    [field: Header("Footsteps SFX")]
    [field: SerializeField]
    public EventReference footstepsSFX { get; private set; }

    [field: Header("Swing SFX")]
    [field: SerializeField]
    public EventReference swingSFX { get; private set; }

    [field: Header("Attack SFX")]
    [field: SerializeField]
    public EventReference attackSFX { get; private set; }

    [field: Header("Door Open SFX")]
    [field: SerializeField]
    public EventReference doorOpenSFX { get; private set; }

    [field: Header("Door Close SFX")]
    [field: SerializeField]
    public EventReference doorCloseSFX { get; private set; }

    [field: Header("Cutscene_Level 5 SFX")]
    [field: SerializeField]
    public EventReference Cutscene_Level5_SFX { get; private set; }

    [field: Header("Jingle SFX")]
    [field: SerializeField]
    public EventReference JingleSFX { get; private set; }

    [field: Header("Narrative")]
    [field: SerializeField] public EventReference narrative;

    [field: Header("Ambience")]
    [field: SerializeField] public EventReference ambience;

    [field: Header("Music")]
    [field: SerializeField] public EventReference music;

    public static FMODEvents instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            UnityEngine.Debug.LogError("Multiple FMOD Events Found at " + instance.gameObject.name);
            //Destroy(this.gameObject);
            return;
        }

        instance = this;
    }
}
