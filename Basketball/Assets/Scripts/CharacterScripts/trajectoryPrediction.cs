using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trajectoryPrediction : MonoBehaviour {
	const int numberOfPoints = 20;
	[SerializeField]
	GameObject aimTile;
	[SerializeField]
	Rigidbody2D ball;
	private float _mass;
	private float _g;

	//Vector3 [] predictedPoints = new Vector3[numberOfPoints];
	float t;
	// Use this for initialization
	void Start () {
		_g = Physics2D.gravity.y;
		_mass = ball.mass;
	}
	public void GetPrediction(Vector3 p,Vector3 startPos)
	{
		Vector3 v0 = p / _mass;
		for (int i = 0; i < numberOfPoints; i++) {
			float x = startPos.x + v0.x * t;
			float y = startPos.y + v0.y * t + (_g * t * t) / 2f;
			Vector3 spawnPos = new Vector3 (x,y,0);
			GameObject aim = Instantiate (aimTile, spawnPos, Quaternion.identity) as GameObject;
			Destroy (aim, 0.02f);
			t += 0.05f;
		}
		t = 0;
	}
	// Update is called once per frame
	void Update () {
		
	}
}
