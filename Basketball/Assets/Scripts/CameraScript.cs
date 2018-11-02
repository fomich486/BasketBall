using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraScript : MonoBehaviour {

	public UnityEvent Scored;
	public UnityEvent Failed;
	[SerializeField]
	private FieldSpawner _spawner; 

	public void LateCameraUpdate()
	{
		StartCoroutine (camUpdate ());
	}
	IEnumerator camUpdate()
	{
		float lerp = 0f;
		Vector3 currentPosition = transform.position;
		Vector3 nextPosition = new Vector3 (transform.position.x, currentPosition.y + _spawner.YdistanceBetweenField, transform.position.z);
		while (transform.position.y < nextPosition.y - 0.05) {
			transform.position = Vector3.Lerp (currentPosition, nextPosition, lerp);
			lerp += 0.05f;
			yield return null;
		}
		transform.position = nextPosition;
	}
}
