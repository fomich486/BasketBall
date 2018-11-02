using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Score : MonoBehaviour {
	[SerializeField]
	public  Text _scoreTxt;
	public UnityEvent spawnField;

	public void SetScore()
	{
		int score = int.Parse(_scoreTxt.text);
		score++;
		_scoreTxt.text = score.ToString ();
		if (score % 5 == 0) {
			if (spawnField != null) {
				spawnField.Invoke ();
			}
		}	
	}
}
