using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreneFaider : MonoBehaviour {
	[Header("Number of points on screen")]
	[SerializeField]
	private int _numWidth;
	[SerializeField]
	private int _numHeight;
	[SerializeField]
	private Image _tileImage;
	[SerializeField]
	RectTransform _faidCanvas;
	List<RectTransform> tilesList = new List<RectTransform>();
	//To find width and height of canvas use RectTransform.sizeDelta
	void Start()
	{
		PointsInstantiate ();
	}

	void PointsInstantiate()
	{
		float screenWidth = _faidCanvas.sizeDelta.x;
		float screenHeight = _faidCanvas.sizeDelta.y;
		float intervalX = screenWidth / _numWidth;
		float intervalY = screenHeight / _numHeight;
		float startX = -screenWidth/2 + intervalX/2 ;
		float startY = screenHeight/2 - intervalY/2;

		for (int i = 0; i < _numWidth; i++) {
			for (int j = 0; j < _numHeight; j++) {
				GameObject tile = Instantiate (_tileImage.gameObject) as GameObject;
				tile.transform.SetParent (_faidCanvas);
				tile.GetComponent<RectTransform> ().anchoredPosition3D = new Vector3 (startX,startY,0);
				tile.GetComponent<RectTransform> ().localScale = Vector3.one;
				tilesList.Add (tile.GetComponent<RectTransform> ());
				startY -= intervalY;
			}
			startX += intervalX;
			startY = screenHeight/2 - intervalY/2;
		}
	}
	void Update()
	{
		
	}
}
