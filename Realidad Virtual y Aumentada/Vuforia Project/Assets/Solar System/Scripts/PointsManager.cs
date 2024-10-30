using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsManager : MonoBehaviour
{
    public int points = 0;
    public List<GameObject> planets;
    public TextMeshProUGUI Pointstext;
    public TextMeshProUGUI PlanetText;
    //public Timer timer;


    public float countdownTime;
    public TextMeshProUGUI timerText;
    [SerializeField] public float currentTime;

    void Start()
    {
        currentTime = countdownTime;

        //timer = new Timer();
        GetRandomPlanet();
        //Debug.Log(timer.currentTime);
    }

    void Update()
    {

        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            int seconds = Mathf.FloorToInt(currentTime);
            timerText.text = string.Format("{0:00}", seconds);
        }
        else
        {
            timerText.text = "¡Tiempo terminado!";
        }

        if (currentTime <= 0)
        {
            currentTime = countdownTime;
            GetRandomPlanet();
            AddPoints(-5);
        }

    }

    public void CheckAnswer(string planetName)
    {
        if (PlanetText.text == planetName)
        {
            AddPoints(10);
            GetRandomPlanet();
        }
        else
        {
        }
    }

    public void AddPoints(int amount)
    {
        points += amount;
        Pointstext.text = "Puntos: " + points;
    }

    void GetRandomPlanet()
    {
        int randomIndex = Random.Range(0, planets.Count);
        GameObject selectedPlanet = planets[randomIndex];
        selectedPlanet.SetActive(true);
        PlanetText.text = selectedPlanet.name;
    }
}
