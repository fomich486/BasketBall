using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldSpawner : MonoBehaviour {

	[SerializeField]
	private Transform _gameField;
	[Range(1,10)]
	public float YdistanceBetweenField;
	public int basketsToThrow;
	private Vector3 nextPoint = Vector3.zero;


	void Awake()
	{
		spawnGameFields ();
	}

	public void spawnGameFields (){
		for (int i = 0; i < basketsToThrow; i++) {
			Quaternion q = new Quaternion();
			if (i % 2 == 0)
				q = Quaternion.identity;
			else
				q.Set (0, 180, 0, 0);
			Vector3 spawnPos = nextPoint;
			nextPoint = spawnPos + Vector3.up *  YdistanceBetweenField;
			Instantiate (_gameField.gameObject, spawnPos, q);
		}
	}

}
