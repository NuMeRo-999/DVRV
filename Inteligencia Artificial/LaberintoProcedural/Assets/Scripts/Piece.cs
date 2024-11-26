using UnityEngine;

public class Piece : MonoBehaviour
{
    void Start()
    {
        Invoke("CheckWalls", 5);
    }

    void Update()
    {

    }

    public void CheckWalls()
    {
        Vector3 raycastOrigin = transform.position + new Vector3(0, 3, 0);

        if (Physics.Raycast(raycastOrigin, transform.right * -1, 6))
        {
            Destroy(transform.GetChild(3).gameObject);
        }
        if (Physics.Raycast(raycastOrigin, transform.right, 6))
        {
            Destroy(transform.GetChild(2).gameObject);
        }
        if (Physics.Raycast(raycastOrigin, transform.forward * -1, 6))
        {
            Destroy(transform.GetChild(1).gameObject);
        }
        if (Physics.Raycast(raycastOrigin, transform.forward, 6))
        {
            Destroy(transform.GetChild(0).gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 raycastOrigin = transform.position + new Vector3(0, 3, 0);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(raycastOrigin, raycastOrigin + transform.right * -6);
        Gizmos.DrawLine(raycastOrigin, raycastOrigin + transform.right * 6);
        Gizmos.DrawLine(raycastOrigin, raycastOrigin + transform.forward * -6);
        Gizmos.DrawLine(raycastOrigin, raycastOrigin + transform.forward * 6);
    }
}
