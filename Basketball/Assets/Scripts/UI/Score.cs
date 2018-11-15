using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Score : MonoBehaviour {
	[SerializeField]
	public  Text _scoreTxt;

	public void SetScore()
	{
		int score = int.Parse(_scoreTxt.text);
		score++;
		_scoreTxt.text = score.ToString ();
		if (score % 2 == 0) {
			print ("Must be invoked");
			FindObjectOfType<Faider> ().FadeIn ();
		}	
	}
}
