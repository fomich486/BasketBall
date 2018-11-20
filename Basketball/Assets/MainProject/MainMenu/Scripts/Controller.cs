using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
    public float musicVolume;
    public float vfxVolume;


    public static Controller instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}
