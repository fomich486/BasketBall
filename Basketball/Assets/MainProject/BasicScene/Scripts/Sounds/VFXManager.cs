using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour {
    public static VFXManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void PlaySound(AudioClip audio)
    {
        instance.GetComponent<AudioSource>().PlayOneShot(audio);
    }

}
