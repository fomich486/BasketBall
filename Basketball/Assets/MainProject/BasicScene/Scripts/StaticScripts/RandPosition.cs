using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandPosition : MonoBehaviour {

    //return vector.zero if non ortographic cam
    //or return rand pos in ortographic cam
    public static Vector3 GetRandPos()
    {
        Vector3 pos = Vector3.zero;
        if (Camera.main.orthographic)
        {
            float height = Camera.main.orthographicSize * 2f;
            float width = Camera.main.aspect * height;
            float x = Random.Range(-width / 2, width/2);
            float y = Random.Range(-height / 2, height / 2);
            pos = new Vector3(x, y, 0f);
            return pos;
        }
        return pos;
    }
    public static Vector3 TriangelsPosRandom()
    {
        Vector3 pos = Vector3.zero;
        if (Camera.main.orthographic)
        {
            float height = Camera.main.orthographicSize * 2f;
            float width = Camera.main.aspect * height;
            int rand = Random.Range(0, 2);
            int coef;
            coef = (rand == 0) ?  1 :  -1;
            float x = Random.Range(coef*width / 2.5f,coef* width / 4f);
            float y = Random.Range(-height / 3f, height / 3f);
            pos = new Vector3(x, y, 0f);
            return pos;
        }
        return pos;
    }
}
