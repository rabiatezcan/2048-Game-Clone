using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    [SerializeField] Cell[] cells;
    [SerializeField] GameObject fillPrefab;
    public static Action<MoveDirection> slide;
    public int score;

    #region UI elements

    [SerializeField] Text scoreText;
    [SerializeField] private Text tempScoreTxt;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private int winningScore;
    [SerializeField] private GameObject winningPanel;
    public Color[] fillColors;

    #endregion

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

        float randomSpawnTime = UnityEngine.Random.Range(1.5f, 3);
        StartCoroutine(CallSpawnFill(randomSpawnTime));
    }

    void Update()
    {
        GetInput();
        GameOverCheck();
    }

    IEnumerator CallSpawnFill(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            SpawnFill();
        }
    }

    public void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            slide(MoveDirection.Up);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            slide(MoveDirection.Right);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            slide(MoveDirection.Left);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            slide(MoveDirection.Down);
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

    public void SpawnFill()
    {
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
    
    public void ScoreUpdate(int score)
    {
        this.score += score;
        scoreText.text = this.score.ToString();
    }

    public void GameOverCheck()
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
            gameObject.SetActive(false);
            CreateScoreText(gameOverPanel);
            gameOverPanel.SetActive(true);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void WinningCheck(int highestFill)
    {
        if (highestFill == winningScore)
        {
            gameObject.SetActive(false);
            CreateScoreText(winningPanel);
            winningPanel.SetActive(true);
        }
    }

    public void KeepPlaying()
    {
        winningPanel.SetActive(false);
        scoreText.transform.parent = gameObject.transform;
        gameObject.SetActive(true);
    }

    public void CreateScoreText(GameObject panel)
    {
        tempScoreTxt.transform.parent = panel.transform;
        tempScoreTxt.text = scoreText.text;
        tempScoreTxt.transform.localPosition = Vector3.zero;
    }
}