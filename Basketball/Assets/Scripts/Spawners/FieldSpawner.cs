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
    int i = 0;


	public void spawnGameFields (){

    }

}
