using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public int x;
    public int y;
    public bool isBomb = false;
    public Sprite bombSprite;
    public Sprite flagSprite;
    public Sprite normalPieceSprite;
    public Sprite[] numberSprites;

    [SerializeField] private bool isFlagged = false;
    [SerializeField] private bool isRevealed = false;

    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isFlagged && !isRevealed)
            {
                RevealPiece();
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            ToggleFlag();
        }
    }

    private void RevealPiece()
    {
        isRevealed = true;

        if (isBomb)
        {
            GetComponent<SpriteRenderer>().sprite = bombSprite;
        }
        else
        {
            int bombsAround = GridMapGenerator.gen.GetBombsAround(x, y);
            GetComponent<SpriteRenderer>().sprite = numberSprites[bombsAround];

            if (bombsAround == 0)
            {
                RevealAdjacent();
            }
        }
    }

    private void RevealAdjacent()
    {
        for (int i = x - 1; i <= x + 1; i++)
        {
            for (int j = y - 1; j <= y + 1; j++)
            {
                if (i >= 0 && i < GridMapGenerator.gen.width && j >= 0 && j < GridMapGenerator.gen.height)
                {
                    Piece adjacentPiece = GridMapGenerator.gen.map[i][j].GetComponent<Piece>();

                    if (!adjacentPiece.isRevealed && !adjacentPiece.isBomb)
                    {
                        adjacentPiece.RevealPiece();
                    }
                }
            }
        }
    }

    private void ToggleFlag()
    {
        if (!isRevealed)
        {
            isFlagged = !isFlagged;

            if (isFlagged)
            {
                GetComponent<SpriteRenderer>().sprite = flagSprite;
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = normalPieceSprite;
            }
        }
    }
}