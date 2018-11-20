using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartVolumeSettings : MonoBehaviour
{
    [SerializeField]
    Slider sl_VFX;
    [SerializeField]
    Slider sl_Music;
    [SerializeField]
    AudioSource as_VFX;
    [SerializeField]
    AudioSource as_Music;

    private void Awake()
    {
        //MUSIC
        sl_Music.value = Controller.instance.musicVolume;
        as_Music.volume = Controller.instance.musicVolume;
        //VFX
        sl_VFX.value = Controller.instance.vfxVolume;
        as_VFX.volume = Controller.instance.vfxVolume;
    }

    public void OnValueChangeVFX(float value)
    {
        Controller.instance.vfxVolume = value;
    }

    public void OnValueChangeMusic(float value)
    {
        Controller.instance.musicVolume = value;
    }
}
