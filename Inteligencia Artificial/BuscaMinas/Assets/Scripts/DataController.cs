using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlDatosJuego : MonoBehaviour
{

    public int width
    {
        get => width;
        set => width = value;
    }

    public int heigth
    {
        get => heigth;
        set => heigth = value;
    }

    public int bombCount
    {
        get => bombCount;
        set => bombCount = value;
    }

    private void Awake()
    {
        int numInstancias = FindObjectsOfType<ControlDatosJuego>().Length;
        if (numInstancias > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

}