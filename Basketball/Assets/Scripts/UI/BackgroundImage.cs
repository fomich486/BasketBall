using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundImage : MonoBehaviour {
	[SerializeField]
	Image backgroundImage;
	[SerializeField]
	List<Sprite> sprites;
	int num = 0;
	void Start()
	{
		SetNewBackground ();
	}
	public void SetNewBackground()
	{
		//backgroundImage.sprite = sprites [num];
		num++;
	}
}
