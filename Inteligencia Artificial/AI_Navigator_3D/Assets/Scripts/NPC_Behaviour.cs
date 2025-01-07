using UnityEngine;
using UnityEngine.AI;

public class NPC_Behaviour : MonoBehaviour
{

    [SerializeField] private Vector3 destination;

    void Start()
    {
        GetComponent<NavMeshAgent>().SetDestination(destination);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                destination = hit.point;
            }
            GetComponent<NavMeshAgent>().SetDestination(destination);
        }
    }
}
