using UnityEngine;

public class FoodNinja : MonoBehaviour
{

    public GameObject[] food;

    void Start()
    {
        InvokeRepeating("SpawnFood", 1f, 2f);
    }

    void SpawnFood()
    {
        int index = Random.Range(0, food.Length);
        Vector3 spawnPosition = new Vector3(Random.Range(-10f, 10f), 10f, 0);
        Instantiate(food[index], spawnPosition, Quaternion.identity);
    }
    
}
