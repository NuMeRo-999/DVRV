using System;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject Cubemap;

    public int width = 30;
    public int height = 30;
    public int depth = 30;

    public int seed = 3000;
    public int detail = 20;


    void Start()
    {
        GenerateMap();
    }

    void Update()
    {
        
    }

    void OnDrawGizmos()
    {
        
        
    }

    private void GenerateMap(){
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < depth; j++)
            {
                height = (int)(Mathf.PerlinNoise((i / 2f + seed) / detail, (j / 2f  + seed) / detail) * detail);
                Debug.Log("Height: " + height);

                for (int k = 0; k < height; k++)
                {
                    GameObject cube = Instantiate(Cubemap, new Vector3(i, k, j), Quaternion.identity);
                    cube.transform.parent = this.transform;
                }
            }
        }
    }
}
