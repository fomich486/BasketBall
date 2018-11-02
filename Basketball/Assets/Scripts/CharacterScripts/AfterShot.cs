using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AfterShot : MonoBehaviour {
	[SerializeField]
	UnityEvent lateCamUpdate;
	[SerializeField]
	Transform ballSpawnPos;
	[SerializeField]
	private FieldSpawner _spawner;
	SpriteRenderer _sr;
	Throw _throw;
	[Space]
	[Header("CharecterSprites")]
	[SerializeField]
	Sprite jumping;
	[SerializeField]
	Sprite readyToShoot;
	[SerializeField]
	Sprite Shooted;

	void Start()
	{
		_sr = GetComponent<SpriteRenderer> ();
		_throw = GetComponent<Throw> ();
	}
 
	public void ShotCompleated()
	{
		StartCoroutine (MoveToNextPos ());
	}
	IEnumerator MoveToNextPos()
	{
		_sr.sprite = jumping;
		float lerp = 0f;
		Vector3 currentPosition = transform.position;
		Vector3 nextPosition = new Vector3 (-currentPosition.x, currentPosition.y + _spawner.YdistanceBetweenField, 0f);
		while (transform.position.y < nextPosition.y - 0.02) {
			transform.position = Vector3.Lerp (currentPosition, nextPosition, lerp);
			lerp += 0.02f;
			if (lerp == 0.5f) {
				
			}
			yield return null;
		}
		transform.position = nextPosition;
		_sr.sprite = readyToShoot;
		_sr.flipX = !_sr.flipX;
		ballSpawnPos.transform.localPosition = new Vector3 (-ballSpawnPos.transform.localPosition.x, ballSpawnPos.transform.localPosition.y, ballSpawnPos.transform.localPosition.z);
		_throw.enabled = true;
		lateCamUpdate.Invoke ();
	}
	public void SetShootedSprite()
	{
		_sr.sprite = Shooted;
	}
	public void SetReadySprite()
	{
		_throw.enabled = true;
		_sr.sprite = readyToShoot;
	}
}
