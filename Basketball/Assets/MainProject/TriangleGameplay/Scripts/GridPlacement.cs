using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPlacement :PiramidSpawner {

    private void Start()
    {
        SpawnPiramid();
    }
    public Vector3 CheckPlacement(Vector2 point)
    {
        float distnance = 100;
        Vector3 placePos = Vector3.zero;
        foreach (GameObject g in trngls)
        {
            if (Vector2.Distance(g.transform.position, point) < distnance) {
                distnance = Vector2.Distance(g.transform.position, point);
                placePos = g.transform.position;
            }
        }
        return placePos;
    }
}
