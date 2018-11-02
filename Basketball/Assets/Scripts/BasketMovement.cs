using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketMovement : MonoBehaviour {
	[SerializeField]
	float A;
	[SerializeField]
	float w;
	float startY;
	float phi0 = 0f; 
	void Start()
	{
		startY = transform.localPosition.y;
		int score = int.Parse (FindObjectOfType<Score> ()._scoreTxt.text);
		int coff = score / 5;
		if (coff < 5 && coff > 0) {
			phi0 = Random.Range (0f, 360f);
			w = coff;
		} else if (coff == 0) {
		
		}
		else {
			phi0 = Random.Range (0f,360f);
			w = 5;
		}

	}
	// Update is called once per frame
	void Update () {
		float Y = 	startY +  A*Mathf.Sin (w * Time.time+phi0);
		transform.localPosition = new Vector3 (transform.localPosition.x, Y, transform.localPosition.z);
	}
}
