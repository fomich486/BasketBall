using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {

    public void PauseSet(float rate)
    {
        Time.timeScale = rate;
        print("Executed");
    }
}
