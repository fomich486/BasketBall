using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiramidSpawner : MonoBehaviour {
    //object to spawn
    [SerializeField]
    public Transform spawnPrimitiv;
    [SerializeField]
    public int sideLength;
    public List<GameObject> trngls = new List<GameObject>();
    [SerializeField]
    LayerMask layer;

    private void Start()
    {
        SpawnPiramid();
        CreatePuzzle();
    }

    public void SpawnPiramid()
    {
        float r = Mathf.Sqrt(3f) * sideLength / 6f;
        float r1 = Mathf.Sqrt(3f) * sideLength / 6f;
        Vector2 centerDelta;
        print(sideLength % 2);
        if (sideLength % 2 == 0)
            centerDelta = new Vector2(sideLength / 2 - 0.5f, r1);
        else
            centerDelta = new Vector2(sideLength / 2, r1 - 0.5f);
        //  print(string.Format("direction x = {0}, y = {1}, modul = {2}", direction.x, direction.y, direction.magnitude));
        int level = sideLength;
        Vector2 horizontalDelta = Vector2.up * 0.25f + Vector2.right * 0.5f;
        Vector2 verticalDelta = Vector2.up * 0.8f + Vector2.right * 0.5f;
        for (int i = 0; i < sideLength; i++)
        {

            for (int j = 0; j < level; j++)
            {

                Vector2 pos_spawn = j * Vector2.right + i * verticalDelta - centerDelta;
                GameObject gam = Instantiate(spawnPrimitiv.gameObject, pos_spawn, Quaternion.identity) as GameObject;
                trngls.Add(gam);

            }
            for (int j = 0; j < level - 1; j++)
            {

                Vector2 pos_spawn = j * Vector2.right + horizontalDelta + i * verticalDelta - centerDelta;
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
            float randCollor = Random.Range(0f, 1f);
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
        }
    }


}
