using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;
using UnityEngine;
using UnityEngine.UI;

public class FoodNinja : MonoBehaviour
{

    public GameObject[] food;
    public GameObject bullet;
    public GameObject ARCamera;
    public float speed = 2000f;

    [SerializeField] private ARPlaneManager arPlaneManager; 
    private List<ARPlane> planes = new List<ARPlane>();
    private bool empezarGenerar = false;
    public float alturaPlanoDetectado;

    public Sprite buttonSprite;
    public Sprite buttonSpritePressed;
    public Image Button;

    void Start()
    {
        InvokeRepeating("SpawnFood", 1f, 2f);
    }

    void Update()
    {
      
            Debug.Log(" alturaPlanoDetectado: " + alturaPlanoDetectado);
        if (empezarGenerar)
        {
            GameObject[] food = GameObject.FindGameObjectsWithTag("Food");
            foreach (GameObject f in food)
            {
                if (f.transform.position.y < alturaPlanoDetectado)
                {
                    Destroy(f);
                }
            }
        }
    }

    void SpawnFood()
    {
        if(empezarGenerar)
        {
            int index = Random.Range(0, food.Length);
            Vector3 spawnPosition = new Vector3(Random.Range(-5, 5), 10f, 4f);
            Instantiate(food[index], spawnPosition, Quaternion.identity);
        }
    }

    public void Shoot()
    {
        GameObject bullet = Instantiate(this.bullet, ARCamera.transform.position, Quaternion.Euler(ARCamera.transform.rotation.eulerAngles.x, ARCamera.transform.rotation.eulerAngles.y + 90, ARCamera.transform.rotation.eulerAngles.z));
        bullet.GetComponent<Rigidbody>().AddForce(ARCamera.transform.forward * speed);
    }
    
    private void OnEnable(){
        arPlaneManager.planesChanged += PlanesFound;
    }

    private void OnDisable(){
        arPlaneManager.planesChanged -= PlanesFound;
    }

    private void PlanesFound(ARPlanesChangedEventArgs datosPlanos){
        if(datosPlanos.added != null && datosPlanos.added.Count > 0){
            planes.AddRange(datosPlanos.added);
        }

        foreach(ARPlane plane in planes){
            if(plane.extents.x * plane.extents.y >= 1){
                alturaPlanoDetectado = plane.center.y;
                DetenerDeteccionPlanos();
            }
        }
    }

    public void DetenerDeteccionPlanos(){
        arPlaneManager.requestedDetectionMode = UnityEngine.XR.ARSubsystems.PlaneDetectionMode.None;
        
        foreach (ARPlane plane in planes)
        {
            plane.gameObject.SetActive(false);
        }

        empezarGenerar = true;
    }

    public void changeButtonSprite()
    {
        
    }
}
