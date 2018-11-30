using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageble : MonoBehaviour {

    public void TakeDamage()
    {
        Destroy(gameObject);
    }
}
