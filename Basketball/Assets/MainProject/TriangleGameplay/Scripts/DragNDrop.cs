using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragNDrop : MonoBehaviour {
    [SerializeField]
    LayerMask layerTriangel;
    [SerializeField]
    LayerMask layerGrid;
    bool hold = false;
    GameObject holdObject;
    Vector3 lastPosition;
    private void Start()
    {
        
    }
    void Update () {
        //mouse controls
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButton(0) && !hold && InBounds())
        {
            //print("Inside, it's true");
            SetOrderInLayer(2);
            hold = true;
            if(AllElementFilled())
                GridUncheck();
        }
        if (Input.GetMouseButtonUp(0)&&hold)
        {
            SetOrderInLayer(1);
            hold = false;
            //return to last position if failed
            SpawnCheck();

        }

        if (hold)
        {
            holdObject.transform.position = mousePos;
            // print(gameObject.GetComponentInChildren<Transform>().name);
            
        }
        //Touch controll
      /*  if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(t.position);
            if (t.phase == TouchPhase.Began && !hold && InBounds())
            {
                //print("Inside, it's true");
                SetOrderInLayer(2);
                hold = true;
                if (AllElementFilled())
                    GridUncheck();
            }
            if (t.phase == TouchPhase.Ended && hold)
            {
                SetOrderInLayer(1);
                hold = false;
                //return to last position if failed
                SpawnCheck();

            }

            if (hold)
            {
                holdObject.transform.position = mousePos;
                // print(gameObject.GetComponentInChildren<Transform>().name);

            }
        }*/

	}

    bool InBounds()
    {
        // print(GetComponent<Collider2D>().OverlapPoint(mouseP));
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
       // Ray r = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        Debug.DrawRay(r.origin, r.direction*20f, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(r.origin, r.direction, Mathf.Infinity,layerTriangel);
        if (hit)
        {
           // print(string.Format("Hit name : {0}", hit.collider.name));
            holdObject = hit.collider.gameObject.transform.parent.gameObject;
            lastPosition = holdObject.transform.position;
            return true;
        }
        else
        {
           // print("Nothing!");
            return false;
        }
    }

    void BackToLastPos()
    {
        holdObject.transform.position = lastPosition;
    }

    void SpawnCheck()
    {
        for (int i = 0; i < holdObject.transform.childCount; i++)
        {
            var child = holdObject.transform.GetChild(i);
            RaycastHit2D hit = Physics2D.Raycast(child.position, Vector3.forward, Mathf.Infinity, layerGrid);
            if (hit)
            {
                if (!hit.collider.gameObject.GetComponent<GridElement>().filled && hit.transform.rotation.z == child.rotation.z)
                {

                }
                else
                {
                    BackToLastPos();
                    GridUncheck();
                    return;
                }

            }
            else
            {
                print("nothig hited");
                if (NoGrid())
                {
                    BackToLastPos();
                    GridUncheck();
                }
                return;
            }
        }
        print("CanBeSpawned");
        for (int i = 0; i < holdObject.transform.childCount; i++)
        {
            var child = holdObject.transform.GetChild(i);
            RaycastHit2D hit = Physics2D.Raycast(child.position, Vector3.forward, Mathf.Infinity, layerGrid);
            child.position = hit.transform.position;
            hit.collider.gameObject.GetComponent<GridElement>().filled = !hit.collider.gameObject.GetComponent<GridElement>().filled;

        }
    }

    void GridUncheck()
    {
        for (int i = 0; i < holdObject.transform.childCount; i++)
        {
            var child = holdObject.transform.GetChild(i);
            RaycastHit2D hit = Physics2D.Raycast(child.position, Vector3.forward, Mathf.Infinity, layerGrid);
            if(hit)
                hit.collider.gameObject.GetComponent<GridElement>().filled = !hit.collider.gameObject.GetComponent<GridElement>().filled;

        }
    }

   bool NoGrid()
    {
        for (int i = 0; i < holdObject.transform.childCount; i++)
        {
            var child = holdObject.transform.GetChild(i);
            RaycastHit2D hit = Physics2D.Raycast(child.position, Vector3.forward, Mathf.Infinity, layerGrid);
            if (hit)
                return true;
        }
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        holdObject.transform.position = mousePos;
        return false;
    }

    bool AllElementFilled()
    {
        for (int i = 0; i < holdObject.transform.childCount; i++)
        {
            var child = holdObject.transform.GetChild(i);
            RaycastHit2D hit = Physics2D.Raycast(child.position, Vector3.forward, Mathf.Infinity, layerGrid);
            if(hit)
                if  (hit.collider.gameObject.GetComponent<GridElement>().filled == false)
                    return false;
        }
        return true;
    }
    void SetOrderInLayer(int order)
    {
        for (int i = 0; i < holdObject.transform.childCount; i++)
        {
            var child = holdObject.transform.GetChild(i);
            child.GetComponent<SpriteRenderer>().sortingOrder = order;
        }
    }
}
