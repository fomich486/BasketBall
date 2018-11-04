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
	[Range(0.01f,10f)]
	[SerializeField]
	float delta;
	// screenX / screenY
	float sizeCoef;
	//To find width and height of canvas use RectTransform.sizeDelta
	void Start()
	{
		PointsInstantiate ();
		//primitiv
		//StartCoroutine (FadePrimitiv());
		//Left - Right
		//StartCoroutine (FadeLeftRightPrimitiv());
		//Right - Left
		//StartCoroutine (FadeRightLeftPrimitiv());
		//Up - Down
		StartCoroutine (FadeUpDownPrimitiv());

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
				//position
				tile.GetComponent<RectTransform> ().anchoredPosition3D = new Vector3 (startX,startY,0);
				//scale
				tile.GetComponent<RectTransform> ().localScale = Vector3.one;
				//size
				tile.GetComponent<RectTransform> ().sizeDelta = new Vector2 (intervalX, intervalY);
				tilesList.Add (tile.GetComponent<RectTransform> ());
				startY -= intervalY;
			}
			startX += intervalX;
			startY = screenHeight/2 - intervalY/2;
		}
	}
	void DestroyList()
	{
		foreach (RectTransform tile in tilesList) {
			Destroy (tile.gameObject);
		}
		tilesList.Clear ();
	}
	#region Primitiv Faiders
	//All fade same and together
	IEnumerator FadePrimitiv()
	{
		RandomColor ();
		while (tilesList [0].sizeDelta.x > 0) {
			foreach (RectTransform tile in tilesList) {
				tile.sizeDelta -= delta * Vector2.one;
			}
			yield return null;
		}
		DestroyList ();
	}
	//Change line size from  left to right (whene option yield from up-left to right)
	IEnumerator FadeLeftRightPrimitiv()
	{
		RandomColor ();
		while(tilesList[_numWidth*_numHeight - 1].sizeDelta.x > 0)
			for (int i = 0; i < _numWidth; i++) {
				for (int j = 0; j < _numHeight; j++) {
					RectTransform tile = tilesList [i*_numHeight + j].GetComponent<RectTransform> ();
				tile.sizeDelta -= delta * Vector2.one;
				//Like an option but rally whery slow
				//	yield return null;
				}
				yield return new WaitForSeconds(0.05f);
			}
		DestroyList ();
	}
	//Change line size from  right to left (whene option yield from up-left to right)
	IEnumerator FadeRightLeftPrimitiv()
	{
		RandomColor ();
		while(tilesList[_numWidth*_numHeight - 1].sizeDelta.x > 0)
			for (int i = _numWidth-1; i >= 0; i--) {
				for (int j = _numHeight-1; j >= 0; j--) {
					RectTransform tile = tilesList [i*_numHeight + j].GetComponent<RectTransform> ();
					tile.sizeDelta -= delta * Vector2.one;
					//Like an option but rally whery slow
					//	yield return null;
				}
				yield return new WaitForSeconds(0.05f);
			}
		DestroyList ();
	}

	IEnumerator FadeUpDownPrimitiv()
	{
		RandomColor ();
		while(tilesList[_numWidth*_numHeight - 1].sizeDelta.x > 0)
			for (int i = 0; i < _numHeight; i++) {
				for (int j = 0; j < _numWidth; j++) {
					RectTransform tile = tilesList [i + j*_numHeight].GetComponent<RectTransform> ();
					print (string.Format ("i + j *numHeight = {0}", i + j * _numHeight));
					tile.sizeDelta -= delta * Vector2.one;
					//Like an option but rally whery slow
					//	yield return null;
				}
				yield return null;
			}
		DestroyList ();
	}

	#endregion
	void RandomColor()
	{
		foreach (RectTransform tile in tilesList) {
			Color col = new Color (1f,Random.Range (0f, 1f),0f );
			tile.GetComponent<Image> ().color = col;
		}
	}
}
