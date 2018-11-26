using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiramidSpawner : MonoBehaviour {
    //object to spawn
    
    public float size;
    [SerializeField]
    public Transform spawnPrimitiv;
    [SerializeField]
    public int sideLength;
    public List<GameObject> trngls = new List<GameObject>();
    public List<GameObject> collections = new List<GameObject>();
    [SerializeField]
    private List<Transform> points;
    [SerializeField]
    LayerMask layer;

    private void Start()
    {
        spawnPrimitiv.localScale = new Vector3(size, size);
        SpawnPiramid();
        CreatePuzzle();
        RandomPos();
    }

    public void SpawnPiramid()
    {
        float primitivSizeX = spawnPrimitiv.transform.localScale.x;
        //float r = Mathf.Sqrt(3f) * sideLength / 6f;
        //float r1 = Mathf.Sqrt(3f) * sideLength / 6f;
        float r = Mathf.Sqrt(3f) * primitivSizeX*sideLength / 6f;
        float r1 = Mathf.Sqrt(3f) * primitivSizeX*sideLength / 6f;
        int level = sideLength;
        Vector2 horizontalDelta = Vector2.up * 0.25f * primitivSizeX + Vector2.right * 0.5f * primitivSizeX;
        Vector2 verticalDelta = Vector2.up * 0.8f * primitivSizeX + Vector2.right * 0.5f * primitivSizeX;
        Vector2 centerDelta = Vector2.zero;
        print(sideLength % 2);
        if (sideLength % 2 == 0)
            centerDelta = new Vector2(sideLength*primitivSizeX / 2 - 0.5f*primitivSizeX, r1);
        else
            centerDelta = new Vector2(sideLength * primitivSizeX / 2-0.5f*primitivSizeX, r1 - 0.5f * primitivSizeX);
        //  print(string.Format("direction x = {0}, y = {1}, modul = {2}", direction.x, direction.y, direction.magnitude));
        for (int i = 0; i < sideLength; i++)
        {

            for (int j = 0; j < level; j++)
            {

                Vector2 pos_spawn = j * Vector2.right*primitivSizeX + i * verticalDelta - centerDelta;
                GameObject gam = Instantiate(spawnPrimitiv.gameObject, pos_spawn, Quaternion.identity) as GameObject;
                trngls.Add(gam);

            }
            for (int j = 0; j < level - 1; j++)
            {

                Vector2 pos_spawn = j * Vector2.right*primitivSizeX + horizontalDelta + i * verticalDelta - centerDelta;
                GameObject gam = Instantiate(spawnPrimitiv.gameObject, pos_spawn, Quaternion.Euler(0f, 0f, 180f)) as GameObject;
                trngls.Add(gam);
            }
            level--;
        }
    }

    void CreatePuzzle()
    {
        while (trngls.Count != 0)
        {
            Color color = RandomColor.GetRandomColor();
            int listCount = trngls.Count;
            int rand = Random.Range(0, listCount);
            Vector2 size = 2f * Vector2.one;
            Collider2D[] colls = Physics2D.OverlapBoxAll(trngls[rand].transform.position, size, 0f, layer);
            GameObject collection = new GameObject();
           
            collection.transform.position = trngls[rand].transform.position;
            collection.name = "Collection";
            foreach (Collider2D c in colls)
            {
                c.transform.SetParent(collection.transform);
                c.GetComponent<SpriteRenderer>().color = color;
                trngls.Remove(c.gameObject);
            }
            collections.Add(collection);
        }
    }
    void RandomPos()
    {
        foreach (GameObject g in collections)
        {
            if(points.Count>0)
                g.transform.position = points[Random.Range(0, points.Count)].position;
            else
                g.transform.position = Vector3.zero;
        }
    }


}
