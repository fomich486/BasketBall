using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerLife : MonoBehaviour {
	[SerializeField]
	private int balls;
	[SerializeField]
	private Text ballAmountText;
	// Use this for initialization
	void Start () {
		ballAmountText.text = balls.ToString ();
	}

	public void TrowFaild()
	{
		if (balls > 1) {
			balls--;
			print (string.Format ("Balls : {0}", balls));
			ballAmountText.text = balls.ToString ();
		} else if(balls == 1) {
			balls--;
			ballAmountText.text = balls.ToString ();
			GameOver ();
		}
	}
	void GameOver()
	{
		
	}
}
