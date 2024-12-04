using UnityEngine;

public class FoodNinja : MonoBehaviour
{

    public GameObject[] food;
    public GameObject bullet;
    public GameObject ARCamera;
    public float speed = 2000f;

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

    public void Shoot()
    {
        GameObject bullet = Instantiate(this.bullet, ARCamera.transform.position, ARCamera.transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(ARCamera.transform.forward * speed);
    }
    
}
