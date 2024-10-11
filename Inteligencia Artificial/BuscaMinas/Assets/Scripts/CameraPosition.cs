using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    public GameObject gridMapGenerator;

    void Start()
    {
        GridMapGenerator gridMapGeneratorScript = gridMapGenerator.GetComponent<GridMapGenerator>();
        int width = gridMapGeneratorScript.width;
        int height = gridMapGeneratorScript.height;
        this.transform.position = new Vector3(0.30f * (height - 1) / 2, 0.30f * (width - 1) / 2, -10);
    }

    void Update()
    {
        
    }
}
