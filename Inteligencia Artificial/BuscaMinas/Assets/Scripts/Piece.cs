using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public float x;
    public float y;
    public bool isBomb = false;
    public Sprite bombSprite;
    public Sprite[] numberSprites;

    void Start()
    {

    }

    void Update()
    {

    }

    public void OnMouseDown()
    {
        if (isBomb)
        {
            GetComponent<SpriteRenderer>().sprite = bombSprite;
        }
        else
        {
            Debug.Log("Bombs around: " + GridMapGenerator.gen.GetBombsAround((int)x, (int)y));
            transform.GetComponent<SpriteRenderer>().sprite = numberSprites[GridMapGenerator.gen.GetBombsAround((int)x, (int)y)];
        }
    }
}
