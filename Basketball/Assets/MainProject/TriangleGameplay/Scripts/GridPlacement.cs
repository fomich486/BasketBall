using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPlacement :PiramidSpawner {

    private void Start()
    {
        spawnPrimitiv.localScale = new Vector3(size, size);
        SpawnPiramid();
    }
    public Vector3 CheckPlacement(Vector2 point,float rotZ)
    {
        float distnance = 100;
        Vector3 placePos = Vector3.zero;
        foreach (GameObject g in trngls)
        {
            if (Vector2.Distance(g.transform.position, point) < distnance && g.transform.rotation.z == rotZ) {
                distnance = Vector2.Distance(g.transform.position, point);
                placePos = g.transform.position;
            }
        }
        return placePos;
    }
}
