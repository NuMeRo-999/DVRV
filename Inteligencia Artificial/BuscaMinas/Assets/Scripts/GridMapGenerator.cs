using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMapGenerator : MonoBehaviour
{

    public GameObject gridPiece;
    public int width = 10;
    public int height = 10;

    void Start()
    {
        for (int x = 0; x < width; x++)
        {
            for (int j = 0; j < height; j++)
            {
                GameObject grid = Instantiate(gridPiece, new Vector3(0.16f * j, 0.16f * x, 0), Quaternion.identity);
                grid.transform.parent = this.transform;
            }
        }
    }

    void Update()
    {
        
    }
}
