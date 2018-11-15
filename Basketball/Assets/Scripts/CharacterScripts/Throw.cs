using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour {
	[SerializeField]
	public Transform _ball;
	[SerializeField]
	private Transform _spawnPos;
	[Range(1f,1000f)]
	[SerializeField]
	private float force;
	trajectoryPrediction trajPred;
	float delta = 0f;

//	[SerializeField]
//	GameObject check;
	// Use this for initialization
	void Start () {
		trajPred = GetComponent<trajectoryPrediction> ();
	}
	
	// Update is called once per frame
	void Update () {
		//Mouse controll
		Vector3 mp = Input.mousePosition;
		Vector3 worldPoint = Camera.main.ScreenToWorldPoint(mp);
		worldPoint = new Vector3 (0f, worldPoint.y, 0);
		Vector3 direction = worldPoint - _spawnPos.position;
		float charger = 0.75f + 0.25f * Mathf.Sin (delta);
		Vector3 p = direction.normalized * force * charger;
		if (Input.GetMouseButton (0)) {
			trajPred.GetPrediction (p, _spawnPos.position);
			delta += Time.deltaTime;
		}
		if (Input.GetMouseButtonUp (0)) {
			SpawnBall (p);
		}

		//TouchControll
//		if (Input.touchCount > 0) {
//			Touch touch = Input.GetTouch (0);
//			Vector3 worldPoint = Camera.main.ScreenToWorldPoint(touch.position);
//			worldPoint = new Vector3 (worldPoint.x, worldPoint.y+2f, 0);
//			Vector3 direction = worldPoint - _spawnPos.position;
//			float charger =0.75f + 0.25f * Mathf.Sin (delta);
//			Vector3 p = direction.normalized * force * charger;
//			trajPred.GetPrediction (p, _spawnPos.position);
//			if (touch.phase == TouchPhase.Ended) {
//				SpawnBall (p);
//			} else {
//				delta += Time.deltaTime;
//			}
//		}
	}

	void SpawnBall(Vector3 impuls)
	{
		GameObject ball = Instantiate (_ball.gameObject, _spawnPos.position, Quaternion.identity) as GameObject;
		ball.GetComponent<Rigidbody2D> ().AddForce (impuls,ForceMode2D.Impulse);
		GetComponent<AfterShot> ().SetShootedSprite ();
		delta = 0;
		this.enabled = false;
	}
}
