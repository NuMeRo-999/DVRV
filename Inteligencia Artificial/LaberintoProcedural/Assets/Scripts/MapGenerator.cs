using System.Collections;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int xMax, zMax;
    public GameObject piece;
    public GameObject[,] map;
    
    void Start()
    {
        map = new GameObject[xMax, zMax];
        StartCoroutine(GenMapBasic());
    }

    void Update()
    {
        
    }

    public IEnumerator GenMapBasic()
    {
        for (int x = 0; x < xMax; x++)
        {
            for (int z = 0; z < zMax; z++)
            {
                if (Random.Range(0, 100) < 50)
                {
                    Instantiate(piece, new Vector3(x * 5, 0, z * 5), Quaternion.identity);
                    yield return new WaitForSeconds(0.05f);
                }
            }
        }
    }
}
