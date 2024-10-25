using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public List<GameObject> backgrounds; // Lista de fondos disponibles

    void Start()
    {
        // Selecciona un fondo aleatorio y activa solo ese
        foreach (GameObject background in backgrounds)
        {
            background.SetActive(false);
        }
        GameObject selectedBackground = backgrounds[Random.Range(0, backgrounds.Count)];
        selectedBackground.SetActive(true);

        // Ajusta el tamaño del fondo al de la cámara
        AdjustBackgroundToCamera(selectedBackground);
    }

    void AdjustBackgroundToCamera(GameObject background)
    {
        // Obtiene la altura y el ancho de la cámara en unidades del mundo
        float camHeight = 2f * Camera.main.orthographicSize;
        float camWidth = camHeight * Camera.main.aspect;

        // Obtiene el tamaño original del sprite del fondo
        SpriteRenderer sr = background.GetComponent<SpriteRenderer>();
        float bgWidth = sr.sprite.bounds.size.x;
        float bgHeight = sr.sprite.bounds.size.y;

        // Calcula la escala necesaria para que el fondo ocupe toda la pantalla
        float scaleX = camWidth / bgWidth;
        float scaleY = camHeight / bgHeight;

        // Ajusta la escala del fondo
        background.transform.localScale = new Vector3(scaleX, scaleY, 1);
    }
}
