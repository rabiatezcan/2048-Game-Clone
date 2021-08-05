using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    [SerializeField] Cell[] cells;
    [SerializeField] GameObject fillPrefab;
    public static int ticker;
    public static Action<MoveDirection> slide;

    #region UI elements
    public int score;
    [SerializeField] Text scoreText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private int winningScore;
    [SerializeField] private GameObject winningPanel;
    private bool hasWon; 
    #endregion

    public Color[] fillColors;
    private void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        StartSpawnFill();
        StartSpawnFill();
        gameOverPanel.SetActive(false);
        winningPanel.SetActive(false);
    }

    void Update()
    {
       GetInput();
    }

    public void GetInput()
    {
        if(Input.GetKeyDown(KeyCode.A))
            SpawnFill();
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            CountTicker();
            slide(MoveDirection.Up);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            CountTicker();
            slide(MoveDirection.Right);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            CountTicker();
            slide(MoveDirection.Left);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            CountTicker();
            slide(MoveDirection.Down);
        }
    }
    public void CountTicker()
    {
        ticker++;
        if (ticker % 4 == 0)
        {
            SpawnFill();
        }
    }
    public void SpawnFill()
    {
        bool isFull = true;
        for (int i = 0; i < cells.Length; i++)
        {
            if (cells[i].fill == null)
            {
                isFull = false;
            }
        }
        if (isFull)
        {
            GameOverCheck();
        }
        float chance = UnityEngine.Random.Range(0f, 1f);
        int whichSpawn = UnityEngine.Random.Range(0, cells.Length);
        int tempFillValue = 0;
        if (cells[whichSpawn].transform.childCount == 0)
        {
            if (chance < .2f)
            {
                return;
            }
            else if (chance < .8f)
            {
                tempFillValue = 2;
            }
            else
            {
                tempFillValue = 4;
            }

            GameObject tempFill = Instantiate(fillPrefab, cells[whichSpawn].transform);
            Fill tempFillText = tempFill.GetComponent<Fill>();
            cells[whichSpawn].GetComponent<Cell>().fill = tempFillText;
            tempFillText.FillValueUpdate(tempFillValue);
        }
        else
        {
            SpawnFill();
            return;
        }
    }

    public void StartSpawnFill()
    {
        int whichSpawn = UnityEngine.Random.Range(0, cells.Length);
        if (cells[whichSpawn].transform.childCount == 0)
        {
            GameObject tempFill = Instantiate(fillPrefab, cells[whichSpawn].transform);
            Fill tempFillText = tempFill.GetComponent<Fill>();
            cells[whichSpawn].GetComponent<Cell>().fill = tempFillText;
            tempFillText.FillValueUpdate(2);
        }
    }

    public void ScoreUpdate(int score)
    {
        this.score += score;
        scoreText.text = this.score.ToString();
    }

    public void GameOverCheck()
    {
        gameObject.SetActive(false);
        gameOverPanel.transform.GetChild(2).gameObject.GetComponent<Text>().setTe
        gameOverPanel.SetActive(true);
    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void WinningCheck(int highestFill)
    {
        if(hasWon)
            return;
        if (highestFill == winningScore)
        {
            winningPanel.SetActive(true);
            hasWon = true;
        }
    }

    public void KeepPlaying()
    {
        winningPanel.SetActive(false);
    }
}