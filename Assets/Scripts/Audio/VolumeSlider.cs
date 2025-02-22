
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private enum VolumeType
    {
        MASTER,
        MUSIC,
        AMBIENCE,
        SFX,
        NARRATIVE,
    }

    [Header("VolumeType")]
    [SerializeField] private VolumeType volumeType;

    private Slider volumeSlider;

    private void Awake()
    {
        volumeSlider = GetComponentInChildren<Slider>();
    }

    private void Update()
    {
        switch(volumeType)
        {
            case VolumeType.MASTER:
                volumeSlider.value = SoundSystemManager.instance.masterVolume;
                break;

            case VolumeType.MUSIC:
                volumeSlider.value = SoundSystemManager.instance.musicVolume;
                break;

            case VolumeType.AMBIENCE:
                volumeSlider.value = SoundSystemManager.instance.ambienceVolume;
                break;

            case VolumeType.SFX:
                volumeSlider.value = SoundSystemManager.instance.sfxVolume;
                break;

            case VolumeType.NARRATIVE:
                volumeSlider.value = SoundSystemManager.instance.narrativeVolume;
                break;
            default:
                Debug.LogWarning("Volume Type not found" + volumeType);
                break;
        }
    }

    public void OnSliderValueChanged()
    {
        switch (volumeType)
        {
            case VolumeType.MASTER:
                SoundSystemManager.instance.masterVolume = volumeSlider.value;
                break;

            case VolumeType.MUSIC:
                SoundSystemManager.instance.musicVolume = volumeSlider.value;
                break;

            case VolumeType.AMBIENCE:
                SoundSystemManager.instance.ambienceVolume = volumeSlider.value;
                break;

            case VolumeType.SFX:
                SoundSystemManager.instance.sfxVolume = volumeSlider.value;
                break;

            case VolumeType.NARRATIVE:
                SoundSystemManager.instance.narrativeVolume = volumeSlider.value;
                break;
            default:
                Debug.LogWarning("Volume Type not found" + volumeType);
                break;
        }
    }
}
