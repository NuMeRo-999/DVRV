using UnityEngine;

public class GrappleTester : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GrappleEffect GappleEffect;
    [SerializeField] private float maxRayDistance = 50f;
    [SerializeField] private LayerMask whatIsGrappleable;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;

        if (GappleEffect == null)
            GappleEffect = GetComponent<GrappleEffect>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Vector3 rayDirection = mainCamera.transform.forward;
            Debug.DrawRay(mainCamera.transform.position, rayDirection * maxRayDistance, Color.red, 2f); // Dibuja el rayo en el Editor

            if (Physics.Raycast(mainCamera.transform.position, rayDirection, out hit, maxRayDistance, whatIsGrappleable))
            {
                Debug.Log("Hit: " + hit.collider.name); // Imprime el nombre del objeto con el que colisiona
                GappleEffect.StartSwing(hit.point); // Envía el punto de impacto al gancho
            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            GappleEffect.StopSwing();
        }
    }

    void OnDrawGizmos()
    {
        if (mainCamera != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(mainCamera.transform.position, mainCamera.transform.forward * maxRayDistance);
        }
    }
}