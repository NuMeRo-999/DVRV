using UnityEngine;

public class Cube : MonoBehaviour
{
    void Start()
    {
        if(Physics.Raycast(transform.position, transform.up) && Physics.Raycast(transform.position, transform.forward) 
        && Physics.Raycast(transform.position, transform.right) && Physics.Raycast(transform.position, -transform.forward) && Physics.Raycast(transform.position, -transform.right))
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        
    }
}
