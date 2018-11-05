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
		//StartCoroutine (FadeUpDownPrimitiv());
		//Down - Up
		//StartCoroutine (FadeDownUpPrimitiv());

		//Diagonal
		//StartCoroutine (FadeDiagonalUpLeft());
		//StartCoroutine (FadeDiagonalBottomRight())
		//StartCoroutine (FadeDiagon;alBottomLeft());
		StartCoroutine (FadeDiagonalUpRight());

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
		for (int i = 0; i < _numWidth; i++) {
			for (int j = 0; j < _numHeight; j++) {
				RectTransform tile = tilesList [i*_numHeight + j].GetComponent<RectTransform> ();
				tile.sizeDelta =  Vector2.zero;;
			//Like an option but rally whery slow
			//	yield return null;
			}
			yield return new WaitForSeconds(delta);
		}
		DestroyList ();
	}
	//Change line size from  right to left (whene option yield from up-left to right)
	IEnumerator FadeRightLeftPrimitiv()
	{
		RandomColor ();
		for (int i = _numWidth - 1; i >= 0; i--) {
			for (int j = _numHeight - 1; j >= 0; j--) {
				RectTransform tile = tilesList [i * _numHeight + j].GetComponent<RectTransform> ();
				tile.sizeDelta = Vector2.zero;
				//Like an option but rally whery slow
				//	yield return null;
			}
			yield return new WaitForSeconds (delta);
		}
		DestroyList ();
	}

	IEnumerator FadeUpDownPrimitiv()
	{
		RandomColor ();
		for (int i = 0; i < _numHeight; i++) {
			for (int j = 0; j < _numWidth; j++) {
				RectTransform tile = tilesList [i + j * _numHeight].GetComponent<RectTransform> ();
				print (string.Format ("i + j *numHeight = {0}", i + j * _numHeight));
				tile.sizeDelta = Vector2.zero;
				//Like an option but rally whery slow
				//	yield return null;
			}
			yield return null;
		}
		DestroyList ();
	}

	IEnumerator FadeDownUpPrimitiv()
	{
		RandomColor ();
		for (int i = 0; i < _numHeight; i++) {
			for (int j = 0; j < _numWidth; j++){
				RectTransform tile = tilesList [(_numWidth*_numHeight-1)-i - j*_numHeight].GetComponent<RectTransform> ();
				print (string.Format ("i + j *numHeight = {0}", i + j * _numHeight));
				tile.sizeDelta =Vector2.zero;
				//Like an option but rally whery slow
			}
			yield return new WaitForSeconds(delta);
		}
		DestroyList ();
	}
	#endregion

	#region Diagonal
	IEnumerator FadeDiagonalUpLeft()
	{
		RandomColor ();
		int n = (_numWidth + _numHeight) - 1;
		int k = 1;
		int step = _numHeight - 1;
		int coef = 0;
		int diffNxM = Mathf.Abs (_numWidth - _numHeight);
		for (int i = 0; i < n; i++) {
			for (int j = 0; j < k; j++) {
				print (string.Format ("(i + coef) + j*step = {0}",(i + coef) + j*step));
				RectTransform tile = tilesList[(i + coef) + j*step].GetComponent<RectTransform> ();

				tile.sizeDelta = Vector2.zero;
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
		DestroyList ();
	}

	IEnumerator FadeDiagonalBottomRight()
	{
		RandomColor ();
		int n = (_numWidth + _numHeight) - 1;
		int k = 1;
		int step = _numHeight - 1;
		int coef = 0;
		int diffNxM = Mathf.Abs (_numWidth - _numHeight);
		for (int i = 0; i < n; i++) {
			for (int j = 0; j < k; j++) {
				print (string.Format ("(i + coef) + j*step = {0}",(i + coef) + j*step));
				int number = (_numWidth*_numHeight-1) - ((i + coef) + j * step);
				RectTransform tile = tilesList[number].GetComponent<RectTransform> ();

				tile.sizeDelta = Vector2.zero;
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
		DestroyList ();
	}

	IEnumerator FadeDiagonalBottomLeft()
	{
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
				print (string.Format ("number = {0}",number));
				RectTransform tile = tilesList[number].GetComponent<RectTransform> ();

				tile.sizeDelta = Vector2.zero;
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
		DestroyList ();
	}

	IEnumerator FadeDiagonalUpRight()
	{
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
				print (string.Format ("number = {0}",number));
				RectTransform tile = tilesList[number].GetComponent<RectTransform> ();

				tile.sizeDelta = Vector2.zero;
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
		DestroyList ();
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
