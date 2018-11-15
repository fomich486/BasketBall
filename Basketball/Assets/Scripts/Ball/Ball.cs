using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
	[SerializeField]
	ParticleSystem ps_failExplosion;
	[SerializeField]
	ParticleSystem ps_success;
	[SerializeField]
	private float _lifeTime;
	bool failDestroy = true;
	void Start () {
		_lifeTime += Time.time;
	}

	void Update () {
		if (Time.time > _lifeTime && failDestroy) {
			Die (true);
		}
	}
	void OnTriggerExit2D(Collider2D coll)
	{
		if (coll.tag == "Score") {
            //Congrant particle system
            coll.gameObject.SetActive(false);
			failDestroy = false;
			Die (false);
		}
	}

	void Die(bool fail)
	{
		Vector3 psInstPos = new Vector3 (Camera.main.transform.position.x, Camera.main.transform.position.y, 0f);
		if (fail) {
			//fail
			//Destroy (Instantiate (ps_failExplosion.gameObject, psInstPos, Quaternion.identity), ps_failExplosion.main.duration);
			//invoke fail event
			FindObjectOfType<AfterShot>().SetReadySprite();
			if(FindObjectOfType<CameraScript> ().Failed != null)
				FindObjectOfType<CameraScript> ().Failed.Invoke();
			Destroy (gameObject);
		} else if (!fail) {
			//success 
			//Destroy (Instantiate (ps_success, transform.position, Quaternion.identity), ps_success.main.duration);
			if(FindObjectOfType<CameraScript> ().Scored != null)
				FindObjectOfType<CameraScript> ().Scored.Invoke();
			Destroy (gameObject,2f);
		}

	}
}
