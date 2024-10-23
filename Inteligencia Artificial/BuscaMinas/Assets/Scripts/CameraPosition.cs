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
        this.transform.position = new Vector3(((float)width * 0.16f / 2) - 0.08f, ((float)height * 0.16f / 2) - 0.08f, -10);
        Camera.main.orthographicSize = (Mathf.Max(width, height) * 0.16f / 2) + 0.2f;
    }

    void Update()
    {

    }
}
