using FMOD.Studio;
using FMODUnity;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class SoundSystemManager : MonoBehaviour
{
    [Header("Volume")]
    [Range(0, 1)]
    public float masterVolume = 1;
    [Range(0, 1)]
    public float musicVolume = 1;
    [Range(0, 1)]
    public float ambienceVolume = 1;
    [Range(0, 1)]
    public float narrativeVolume = 1;
    [Range(0, 1)]
    public float sfxVolume = 1;

    private Bus masterBus;
    private Bus musicBus;
    private Bus ambienceBus;
    private Bus narrativeBus;
    private Bus sfxBus;

    public static SoundSystemManager instance { get; private set; }

    private List<EventInstance> eventInstances;

    private List<StudioEventEmitter> eventEmitters;

    private EventInstance ambientEventInstance;

    private EventInstance musicEventInstance;

    private EventInstance narrativeEventInstance;

    private EventInstance sfxEventInstance;

    private void Awake()
    {
        if (instance != null)
        {
            //Destroy(this.gameObject);
            Debug.LogError("Multiple Sound System Managers Found at " + instance.gameObject.name);
            return;
        }


        instance = this;

        DontDestroyOnLoad(this);

        eventInstances = new List<EventInstance>();
        eventEmitters = new List<StudioEventEmitter>();

        masterBus = RuntimeManager.GetBus("bus:/");
        musicBus = RuntimeManager.GetBus("bus:/Music");
        ambienceBus = RuntimeManager.GetBus("bus:/Ambience");
        narrativeBus = RuntimeManager.GetBus("bus:/Narrative");
        sfxBus = RuntimeManager.GetBus("bus:/SFX");
    }

    private void Start()
    {
        InitializeOnSceneUpdate();
        InitializeSFX(FMODEvents.instance.footstepsSFX);
    }

    private void Update()
    {
        masterBus.setVolume(masterVolume);
        musicBus.setVolume(musicVolume);
        ambienceBus.setVolume(ambienceVolume);
        narrativeBus.setVolume(narrativeVolume);
        sfxBus.setVolume(sfxVolume);
    }

    public void PlayOneShot(EventReference sound, Vector3 worldPosition)
    {
        RuntimeManager.PlayOneShot(sound, worldPosition);
    }

    public void InitializeAmbience(EventReference ambientEventReference)
    {
        if (ambientEventReference.IsNull)
        {
            return;
        }

        ambientEventInstance = CreateInstance(ambientEventReference);
        ambientEventInstance.start();
    }

    public void InitializeMusic(EventReference musicEventReference)
    {
        if (musicEventReference.IsNull)
        {
            return;
        }

        musicEventInstance = CreateInstance(musicEventReference);
        musicEventInstance.start();
    }

    public void InitializeNarrative(EventReference narrativeEventReference)
    {
        if (narrativeEventReference.IsNull)
        {
            return;
        }

        narrativeEventInstance = CreateInstance(narrativeEventReference);
    }

    public void InitializeSFX(EventReference sfxEventReference)
    {
        if (sfxEventReference.IsNull)
        {
            return;
        }

        sfxEventInstance = CreateInstance(sfxEventReference);
    }

    public void InitializeOnSceneUpdate()
    {
        StopNarrative();
        StopMusic();
        StopAmbience();

        InitializeNarrative(FMODEvents.instance.narrative);
        InitializeAmbience(FMODEvents.instance.ambience);
        InitializeMusic(FMODEvents.instance.music);
    }

    public void StartAmbience()
    {
        ambientEventInstance.start();
    }

    public void StopAmbience()
    {
        ambientEventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    public void StartNarrative()
    {
        narrativeEventInstance.start();
    }

    public void StopNarrative()
    {
        //narrativeEventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    public void StartMusic()
    {
        musicEventInstance.start();
    }

    public void StopMusic()
    {
        musicEventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    public void StartSFX()
    {
        sfxEventInstance.start();
    }

    public void StopSFX()
    {
        sfxEventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    public EventInstance CreateInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventInstances.Add(eventInstance);
        return eventInstance;
    }

    public StudioEventEmitter InitializeEventEmitter(EventReference eventReference, GameObject emitterGameObject)
    {
        if (emitterGameObject.GetComponent<StudioEventEmitter>() != null)
        {
            Debug.LogAssertion("An emitter already exists!!!?" + emitterGameObject);
        }

        StudioEventEmitter emitter = emitterGameObject.AddComponent<StudioEventEmitter>();
        emitter.EventReference = eventReference;
        eventEmitters.Add(emitter);
        return emitter;
    }

    public void SetCurrentNarrativeAudio(NarrativeArea narrativeAudio)
    {
        narrativeEventInstance.setParameterByName("NarrativeArea", (float) narrativeAudio);
    }

    public void SetCurrentMusicAudio(MusicArea musicAudio)
    {
        musicEventInstance.setParameterByName("MusicArea", (float)musicAudio);
    }

    public void SetCurrentSFXAudio(SFXArea sfxAudio)
    {
        sfxEventInstance.setParameterByName("SFXArea", (float)sfxAudio);
    }

    private void CleanUp()
    {
        foreach (EventInstance eventInstance in eventInstances)
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();
        }

        foreach (StudioEventEmitter emitter in eventEmitters)
        {
            emitter.Stop();
        }

        StopNarrative();
        StopMusic();
        StopSFX();
    }

    private void OnDestroy()
    {
        CleanUp();
    }
}
