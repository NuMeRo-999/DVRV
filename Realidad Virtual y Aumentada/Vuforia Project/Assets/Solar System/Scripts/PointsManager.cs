using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsManager : MonoBehaviour
{
    public int points = 0;
    public List<GameObject> planets;

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
                AddPoints(10);
                planet.SetActive(false);
                ActivateRandomPlanet();
                break;
            }
        }
    }

    void AddPoints(int amount)
    {
        points += amount;
        Debug.Log("Points: " + points);
    }

    void ActivateRandomPlanet()
    {
        int randomIndex = Random.Range(0, planets.Count);
        planets[randomIndex].SetActive(true);
    }
}
