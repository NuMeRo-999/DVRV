using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GridMapGenerator : MonoBehaviour
{
    public static GridMapGenerator gen;
    public GameObject gridPiece;
    public int width = 10;
    public int height = 10;
    public int bombCount = 10;
    public GameObject[][] map;
    public GameObject gameOverPanel;

    void Start()
    {
        gen = this;
        gameOverPanel.SetActive(false);

        map = new GameObject[width][];
        for (int i = 0; i < width; i++)
        {
            map[i] = new GameObject[height];
        }

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                map[i][j] = Instantiate(gridPiece, new Vector2(0.16f * j, 0.16f * i), Quaternion.identity, this.transform);
                map[i][j].GetComponent<Piece>().x = i;
                map[i][j].GetComponent<Piece>().y = j;
            }
        }

        for (int i = 0; i < bombCount; i++)
        {
            int x = Random.Range(0, width);
            int y = Random.Range(0, height);
            if (!map[x][y].GetComponent<Piece>().isBomb)
            {
                map[x][y].GetComponent<Piece>().isBomb = true;
            }
            else
            {
                i--;
            }
        }
    }

    public int GetBombsAround(int x, int y)
    {
        int cont = 0;

        for (int i = x - 1; i <= x + 1; i++)
        {
            for (int j = y - 1; j <= y + 1; j++)
            {
                if (i >= 0 && i < width && j >= 0 && j < height)
                {
                    if (map[i][j].GetComponent<Piece>().isBomb)
                    {
                        cont++;
                    }
                }
            }
        }

        return cont;
    }

    public void GameOver()
    {
        ShowAllBombs();
        gameOverPanel.SetActive(true);
    }

    private void ShowAllBombs()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Piece piece = map[i][j].GetComponent<Piece>();
                if (piece.isBomb && !piece.isRevealed)
                {
                    piece.GetComponent<SpriteRenderer>().sprite = piece.bombSprite;
                }
            }
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
