using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColor : MonoBehaviour {

    public static Color GetColorWhiteBlue()
    {
        //black-white
        float rand = Random.Range(0f, 1f);
        return new Color(rand,rand,rand);
    }
    public static Color GetRandomColor()
    {
        float randColor = Random.Range(0f, 1f);
        List<float> list = new List<float>() { randColor, 0f, 1f };
        float [] array = new float[3];
        while (list.Count != 0)
        {
            int randomListElement = Random.Range(0, list.Count);
            array[list.Count - 1] = list[randomListElement];
            list.Remove(list[randomListElement]);
        }
        return new Color(array[0], array[1], array[2]);
    }
}
