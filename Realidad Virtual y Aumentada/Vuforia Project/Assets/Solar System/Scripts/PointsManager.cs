using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsManager : MonoBehaviour
{
    public int points = 0;
    public List<GameObject> planets;
    public TextMeshProUGUI text;

    void Start()
    {
        foreach (GameObject planet in planets)
        {
            planet.SetActive(false);
        }
        ActivateRandomPlanet();
    }

    void Update()
    {
        foreach (GameObject planet in planets)
        {
            if (planet.activeInHierarchy)
            {
                //AddPoints(10, );
                planet.SetActive(false);
                ActivateRandomPlanet();
                break;
            }
        }
    }

    public void AddPoints(int amount)
    {
        points += amount;
        text.text = "Points: " + points;
    }

    void ActivateRandomPlanet()
    {
        int randomIndex = Random.Range(0, planets.Count);
        planets[randomIndex].SetActive(true);
    }
}
