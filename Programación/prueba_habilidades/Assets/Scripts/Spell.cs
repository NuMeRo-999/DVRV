using UnityEngine;

public class Spell : MonoBehaviour
{
    public GameObject skillPrefab; // Prefab de la habilidad
    public GameObject previewPrefab; // Prefab de la previsualización (círculo rojo)
    public float previewRadius = 2f; // Radio editable de la previsualización
    public LayerMask groundLayer; // Capa del suelo para colocar la previsualización

    private GameObject previewInstance;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TogglePreview(true);
        }

        if (previewInstance)
        {
            UpdatePreviewPosition();
            
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E))
            {
                CastSkill();
            }
        }
    }

    void TogglePreview(bool state)
    {
        if (state && previewInstance == null)
        {
            previewInstance = Instantiate(previewPrefab);
            previewInstance.transform.localScale = new Vector3(previewRadius * 2, 1, previewRadius * 2);
        }
        else if (!state && previewInstance)
        {
            Destroy(previewInstance);
        }
    }

    void UpdatePreviewPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y + 10, Input.mousePosition.z));
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, groundLayer))
        {
            previewInstance.transform.position = hit.point;
        }
    }

    void CastSkill()
    {
        if (previewInstance)
        {
            Instantiate(skillPrefab, previewInstance.transform.position, Quaternion.identity);
            Destroy(previewInstance);
        }
    }
}
