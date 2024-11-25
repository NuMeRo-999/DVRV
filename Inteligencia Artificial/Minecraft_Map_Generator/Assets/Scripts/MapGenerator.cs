using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int width = 100;
    public int height = 20;
    public int depth = 100;
    public int seed = 300000;
    public int detail = 20;

    [Range(0, 100)] public float treeProbability = 0.1f;

    public GameObject[] blocks; // 0: Grass, 1: Dirt, 2: Stone, 3: Bedrock, 4: Trunk, 5: Leaves

    void Start()
    {
        seed = Random.Range(0, 1000000);
        GenerateMap();
    }

    public void GenerateMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < depth; z++)
            {
                int localHeight = (int)(Mathf.PerlinNoise((x / 2f + seed) / detail, (z / 2f + seed) / detail) * detail);
                for (int y = 0; y < localHeight; y++)
                {
                    GameObject blockToInstantiate;

                    // Determinar tipo de bloque
                    if (y == localHeight - 1)
                    {
                        blockToInstantiate = blocks[0]; // Grass
                    }
                    else if (y >= localHeight - 4)
                    {
                        blockToInstantiate = blocks[1]; // Dirt
                    }
                    else if (y > 0)
                    {
                        blockToInstantiate = blocks[2]; // Stone
                    }
                    else
                    {
                        blockToInstantiate = blocks[3]; // Bedrock
                    }

                    Instantiate(blockToInstantiate, new Vector3(x, y, z), Quaternion.identity, transform);

                    // Generar árboles en bloques de grass
                    if (blockToInstantiate == blocks[0] && Random.Range(0, 100) < treeProbability)
                    {
                        CreateTree(new Vector3(x, y, z));
                    }
                }
            }
        }
    }

    public void CreateTree(Vector3 position)
    {
        // Crear tronco del árbol
        int trunkHeight = Random.Range(3, 6); // Altura aleatoria para el tronco
        for (int i = 1; i <= trunkHeight; i++)
        {
            Instantiate(blocks[4], position + Vector3.up * i, Quaternion.identity, transform);
        }

        // Crear hojas
        Vector3[] leafOffsets = new Vector3[]
        {
            new Vector3(1, trunkHeight - 1, 0), new Vector3(-1, trunkHeight - 1, 0),
            new Vector3(0, trunkHeight - 1, 1), new Vector3(0, trunkHeight - 1, -1),
            new Vector3(1, trunkHeight, 0), new Vector3(-1, trunkHeight, 0),
            new Vector3(0, trunkHeight, 1), new Vector3(0, trunkHeight, -1),
            new Vector3(0, trunkHeight + 1, 0), // Parte superior de las hojas
            new Vector3(1, trunkHeight, 1), new Vector3(-1, trunkHeight, -1),
            new Vector3(1, trunkHeight, -1), new Vector3(-1, trunkHeight, 1)
        };

        foreach (Vector3 offset in leafOffsets)
        {
            Instantiate(blocks[5], position + offset, Quaternion.identity, transform);
        }
    }
}
