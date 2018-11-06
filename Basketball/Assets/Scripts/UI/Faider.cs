using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Faider : MonoBehaviour {
	//[SerializeField]
	//Style style = Style.Primitiv;
	int styles = 8;
	[Header("Number of points on screen")]
	[SerializeField]
	private int _numWidth = 5;
	[SerializeField]
	private int _numHeight = 5;
	Vector2 tileSize;
	[SerializeField]
	private Image _tileImage;
	[SerializeField]
	RectTransform _faidCanvas;
	List<RectTransform> tilesList = new List<RectTransform>();
	[Range(0.01f,10f)]
	[SerializeField]
	float delta=0.02f;
	// screenX / screenY
	float sizeCoef;
	//To find width and height of canvas use RectTransform.sizeDelta

	public void FadeIn()
	{
		PointsInstantiate ();
		HowToFade (true);
	}
	void FadeOut()
	{
		HowToFade (false);
	}
	void HowToFade(bool apeare)
	{
		int a = Random.Range (0, styles);
		print ("a = " + a);
		if (a == 0)
			StartCoroutine (FadeLeftRightPrimitiv (apeare));
		else if (a == 1)
			StartCoroutine (FadeRightLeftPrimitiv (apeare));
		else if (a == 2)
			StartCoroutine (FadeUpDownPrimitiv (apeare));
		else if (a == 3)
			StartCoroutine (FadeDownUpPrimitiv (apeare));
		else if (a == 4)
			StartCoroutine (FadeDiagonalUpLeft (apeare));
		else if (a == 5)
			StartCoroutine (FadeDiagonalBottomRight (apeare));
		else if(a == 6)
			StartCoroutine (FadeDiagonalBottomLeft(apeare));
		else if(a == 7)
			StartCoroutine (FadeDiagonalUpRight(apeare));
			
	}
	void Start()
	{

	}

	void PointsInstantiate()
	{
		float screenWidth = _faidCanvas.sizeDelta.x;
		float screenHeight = _faidCanvas.sizeDelta.y;
		float intervalX = screenWidth / _numWidth;
		float intervalY = screenHeight / _numHeight;
		tileSize = new Vector2 (intervalX, intervalY);
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
				tile.GetComponent<RectTransform> ().sizeDelta = Vector2.zero; 
				tilesList.Add (tile.GetComponent<RectTransform> ());
				startY -= intervalY;
			}
			startX += intervalX;
			startY = screenHeight/2 - intervalY/2;
		}
	}
	void CoroutineUtility(bool apeare)
	{
		if (apeare) {
			FindObjectOfType<BackgroundImage> ().SetNewBackground ();
			FadeOut ();
		} else
			DestroyList ();
	}
	void DestroyList()
	{
		foreach (RectTransform tile in tilesList) {
			Destroy (tile.gameObject);
		}
		tilesList.Clear ();
	}
	void SizeChange(bool apeare, RectTransform tile)
	{
		if(apeare)
			tile.sizeDelta = tileSize;
		else
			tile.sizeDelta = Vector2.zero;
	}
	#region Primitiv Faiders
	//All fade same and together
	IEnumerator FadePrimitiv(bool apeare)
	{
		if(apeare)
			RandomColor ();
		while (tilesList [0].sizeDelta.x > 0) {
			foreach (RectTransform tile in tilesList) {
				tile.sizeDelta -= delta * Vector2.one;
			}
			yield return null;
		}
		CoroutineUtility(apeare);
	}
	//Change line size from  left to right (whene option yield from up-left to right)
	IEnumerator FadeLeftRightPrimitiv(bool apeare)
	{
		if(apeare)
			RandomColor ();
		for (int i = 0; i < _numWidth; i++) {
			for (int j = 0; j < _numHeight; j++) {
				RectTransform tile = tilesList [i*_numHeight + j].GetComponent<RectTransform> ();
				SizeChange(apeare, tile);
			}
			yield return new WaitForSeconds(delta);
		}
		CoroutineUtility(apeare);
	}
	//Change line size from  right to left (whene option yield from up-left to right)
	IEnumerator FadeRightLeftPrimitiv(bool apeare)
	{
		if(apeare)
			RandomColor ();
		for (int i = _numWidth - 1; i >= 0; i--) {
			for (int j = _numHeight - 1; j >= 0; j--) {
				RectTransform tile = tilesList [i * _numHeight + j].GetComponent<RectTransform> ();
				SizeChange(apeare, tile);
			}
			yield return new WaitForSeconds (delta);
		}
		CoroutineUtility(apeare);
	}

	IEnumerator FadeUpDownPrimitiv(bool apeare)
	{
		if(apeare)
			RandomColor ();
		for (int i = 0; i < _numHeight; i++) {
			for (int j = 0; j < _numWidth; j++) {
				RectTransform tile = tilesList [i + j * _numHeight].GetComponent<RectTransform> ();
				SizeChange(apeare, tile);
		
			}
			yield return new WaitForSeconds (delta);
		}
		CoroutineUtility(apeare);
	}

	IEnumerator FadeDownUpPrimitiv(bool apeare)
	{
		if(apeare)
			RandomColor ();
		for (int i = 0; i < _numHeight; i++) {
			for (int j = 0; j < _numWidth; j++){
				RectTransform tile = tilesList [(_numWidth*_numHeight-1)-i - j*_numHeight].GetComponent<RectTransform> ();
				SizeChange(apeare, tile);
			}
			yield return new WaitForSeconds(delta);
		}
		CoroutineUtility(apeare);
	}
	#endregion

	#region Diagonal
	IEnumerator FadeDiagonalUpLeft(bool apeare)
	{
		if(apeare)
			RandomColor ();
		int n = (_numWidth + _numHeight) - 1;
		int k = 1;
		int step = _numHeight - 1;
		int coef = 0;
		int diffNxM = Mathf.Abs (_numWidth - _numHeight);
		for (int i = 0; i < n; i++) {
			for (int j = 0; j < k; j++) {
				RectTransform tile = tilesList[(i + coef) + j*step].GetComponent<RectTransform> ();
				SizeChange(apeare, tile);
			}
			//square
			if (_numWidth == _numHeight) {
				if (i < step) {
					k++;
			
				} else if (i >= step) {
					k--;
					coef += step;
				}
			}
			//not square
			else if (_numWidth < _numHeight) {
				if (i < step) {
					if(i<_numWidth - 1)
						k++;

				} else if (i >= step) {
					k--;
					coef += step;
				}
			}

			else if (_numWidth > _numHeight) {
				if (i < step) {
					k++;

				} else if (i >= step) {
					if (i >= step + diffNxM) {
						k--;
					}
					coef += step;
				}
			}

			yield return new WaitForSeconds(delta);
		}
		CoroutineUtility(apeare);
	}

	IEnumerator FadeDiagonalBottomRight(bool apeare)
	{
		if(apeare)
			RandomColor ();
		int n = (_numWidth + _numHeight) - 1;
		int k = 1;
		int step = _numHeight - 1;
		int coef = 0;
		int diffNxM = Mathf.Abs (_numWidth - _numHeight);
		for (int i = 0; i < n; i++) {
			for (int j = 0; j < k; j++) {
				int number = (_numWidth*_numHeight-1) - ((i + coef) + j * step);
				RectTransform tile = tilesList[number].GetComponent<RectTransform> ();
				SizeChange(apeare, tile);
			}
			//square
			if (_numWidth == _numHeight) {
				if (i < step) {
					k++;

				} else if (i >= step) {
					k--;
					coef += step;
				}
			}
			//not square
			else if (_numWidth < _numHeight) {
				if (i < step) {
					if(i<_numWidth - 1)
						k++;

				} else if (i >= step) {
					k--;
					coef += step;
				}
			}

			else if (_numWidth > _numHeight) {
				if (i < step) {
					k++;

				} else if (i >= step) {
					if (i >= step + diffNxM) {
						k--;
					}
					coef += step;
				}
			}

			yield return new WaitForSeconds(delta);
		}
		CoroutineUtility(apeare);
	}

	IEnumerator FadeDiagonalBottomLeft(bool apeare)
	{
		if(apeare)
			RandomColor ();
		int n = (_numWidth + _numHeight) - 1;
		int k = 1;
		int step = _numHeight + 1;
		int coef = 0;
		int counter =0;
		int diffNxM = Mathf.Abs (_numWidth - _numHeight);
		for (int i = 0; i < n; i++) {
//			print ("k = " + k);
//			print ("i = " + i);
			for (int j = 0; j < k; j++) {
				int number = (_numHeight - 1) - (i - coef) + j * step;
				RectTransform tile = tilesList[number].GetComponent<RectTransform> ();
				SizeChange(apeare, tile);
			}
			//square
			if (_numWidth == _numHeight) {
				if (i < _numHeight - 1) {
					k++;

				} else if (i >=_numHeight - 1) {
					k--;
					coef = i+1 +(1+counter*_numHeight);
					counter++;
				}
			}
			//not square
			else if (_numWidth < _numHeight) {
				if (i < _numWidth - 1) {
					if(i<_numWidth - 1)
						k++;

				} else if (i >= _numHeight - 1) {
					k--;
					coef = i+1 +(1+counter*_numHeight);
					counter++;
				}
			}

			else if (_numWidth > _numHeight) {
				if (i <_numHeight - 1) {
					k++;

				} else if (i >= _numHeight - 1) {
					if (i >= _numHeight - 1 + diffNxM) {
						k--;
					}
					coef = i+1 +(1+counter*_numHeight);
					counter++;
				}
			}

			yield return new WaitForSeconds(delta);
		}
		CoroutineUtility(apeare);
	}

	IEnumerator FadeDiagonalUpRight(bool apeare)
	{
		if(apeare)
			RandomColor ();
		int n = (_numWidth + _numHeight) - 1;
		int k = 1;
		int step = _numHeight + 1;
		int coef = 0;
		int counter =0;
		int diffNxM = Mathf.Abs (_numWidth - _numHeight);
		for (int i = 0; i < n; i++) {
			//			print ("k = " + k);
			//			print ("i = " + i);
			for (int j = 0; j < k; j++) {
				int number = ((_numHeight )*(_numWidth-1)) + (i - coef) - j * step;
				RectTransform tile = tilesList[number].GetComponent<RectTransform> ();
				SizeChange(apeare, tile);
			}
			//square
			if (_numWidth == _numHeight) {
				if (i < _numHeight - 1) {
					k++;

				} else if (i >=_numHeight - 1) {
					k--;
					coef = i+1 +(1+counter*_numHeight);
					counter++;
				}
			}
			//not square
			else if (_numWidth < _numHeight) {
				if (i < _numWidth - 1) {
					if(i<_numWidth - 1)
						k++;

				} else if (i >= _numHeight - 1) {
					k--;
					coef = i+1 +(1+counter*_numHeight);
					counter++;
				}
			}

			else if (_numWidth > _numHeight) {
				if (i <_numHeight - 1) {
					k++;

				} else if (i >= _numHeight - 1) {
					if (i >= _numHeight - 1 + diffNxM) {
						k--;
					}
					coef = i+1 +(1+counter*_numHeight);
					counter++;
				}
			}

			yield return new WaitForSeconds(delta);
		}
		CoroutineUtility(apeare);
	}
		


	#endregion
	void RandomColor()
	{
		foreach (RectTransform tile in tilesList) {
			float rand = Random.Range (0f, 1f);
			Color col = new Color (rand,rand,rand);
			tile.GetComponent<Image> ().color = col;
		}
	}
}

enum Style
{
	Primitiv = 1,
	LeftRightPrimitiv,
	RightLeftPrimitiv,
	UpDownPrimitiv,
	DownUpPrimitiv,
	DiagonalUpLeft,
	DiagonalBottomRight,
	DiagonalBottomLeft,
	DiagonalUpRight
}
