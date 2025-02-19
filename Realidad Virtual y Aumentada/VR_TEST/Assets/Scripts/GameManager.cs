using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public int totalRounds = 10;
    private int currentRound = 1;
    private int score = 0;
    private int remainingPins;
    public GameObject pinPrefab;
    public GameObject ball;
    public PinCounter pinCounter;
    private Vector3 ballStartPosition;
    private Transform[] pinPositions;
    private GameObject[] pins;

    void Start()
    {
        ballStartPosition = ball.transform.position;
        InitializePins();
        ResetRound();
    }

    void Update()
    {
        if (AllPinsDown())
        {
            StartCoroutine(NextRound());
        }
    }

    private void InitializePins()
    {
        pinPositions = pinPrefab.GetComponentsInChildren<Transform>();
        pins = new GameObject[pinPositions.Length - 1];
        
        for (int i = 1; i < pinPositions.Length; i++) // Ignorar el padre
        {
            pins[i - 1] = pinPositions[i].gameObject;
        }
    }

    private bool AllPinsDown()
    {
        remainingPins = 0;
        foreach (GameObject pin in pins)
        {
            if (pin.activeSelf && pin.transform.up.y > 0.5f && pin.CompareTag("Pin"))
            {
                remainingPins++;
            }
        }
        return remainingPins == 0;
    }

    private IEnumerator NextRound()
    {
        yield return new WaitForSeconds(2f);
        if (currentRound < totalRounds)
        {
            currentRound++;
            ResetRound();
        }
        else
        {
            Debug.Log("Fin del juego. Puntuación: " + score);
        }
    }

    private void ResetRound()
    {
        ball.transform.position = ballStartPosition;
        ball.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        
        foreach (GameObject pin in pins)
        {
            if (pin.CompareTag("FallenPin"))
            {
                pin.SetActive(false); // Eliminar pines caídos
            }
            else
            {
                pin.SetActive(true);
                pin.transform.position = pinPositions[System.Array.IndexOf(pins, pin) + 1].position;
                pin.transform.rotation = Quaternion.Euler(-90, 0, 0);
            }
        }
        
        pinCounter.ResetCounter();
    }

    public void UpdateScore(int pins)
    {
        score += pins;
    }
}
