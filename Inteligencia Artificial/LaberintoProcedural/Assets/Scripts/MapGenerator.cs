using System.Collections;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int xMax, zMax;
    public GameObject piece;
    public GameObject[,] map;
    public int limit;

    void Start()
    {
        map = new GameObject[xMax, zMax];
        StartCoroutine(GenMapMedium(0, 0));
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

    public IEnumerator GenMapMedium(int x, int z)
    {
        limit--;
        Transform newPiece = Instantiate(piece, new Vector3(x, 0, z), Quaternion.identity).transform;
        yield return new WaitForEndOfFrame();
        if (limit > 0)
        {
            bool complete = false;
            int count = 0;
            while (!complete && count < 50)
            {
                int num = Random.Range(0, 100);
                if (num < 25 && !Physics.Raycast(newPiece.position, newPiece.forward, 6))
                { StartCoroutine(GenMapMedium(x, z + 5)); complete = true; }
                else if (num < 50 && !Physics.Raycast(newPiece.position, newPiece.forward * -1, 6))
                { StartCoroutine(GenMapMedium(x, z - 5)); complete = true; }
                else if (num < 75 && !Physics.Raycast(newPiece.position, newPiece.right, 6))
                { StartCoroutine(GenMapMedium(x + 5, z)); complete = true; }
                else if (num < 90 && !Physics.Raycast(newPiece.position, newPiece.right * -1, 6))
                { StartCoroutine(GenMapMedium(x - 5, z)); complete = true; }
            }


        }
    }
}
