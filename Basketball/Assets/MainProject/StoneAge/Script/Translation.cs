using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translation : MonoBehaviour {

    [SerializeField]
    float speed;

	void Update () {
        transform.Translate(Vector2.left * Time.deltaTime * speed);
	}
}
