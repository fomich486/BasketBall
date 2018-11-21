using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragNDrop : MonoBehaviour {

    bool hold = false;
    private void Start()
    {
        
    }
    void Update () {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButton(0) && InBounds(mousePos)&&!hold)
        {
            //print("Inside, it's true");
            hold = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            print("called");
            hold = false;
            transform.position =  FindObjectOfType<GridPlacement>().CheckPlacement(transform.position);
        }

        if (hold)
        {
            transform.position = mousePos;
        }
	}

    bool InBounds(Vector2 mouseP)
    {
       // print(GetComponent<Collider2D>().OverlapPoint(mouseP));
        return GetComponent<Collider2D>().OverlapPoint(mouseP);
    }

 
}
