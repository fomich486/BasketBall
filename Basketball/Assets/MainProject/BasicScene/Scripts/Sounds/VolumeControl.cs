using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeControl : MonoBehaviour {

    [SerializeField]
    AudioClip aud;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            VFXManager.instance.PlaySound(aud);
        }
    }
}
