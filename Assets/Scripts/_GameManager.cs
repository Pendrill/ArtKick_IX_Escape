using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class _GameManager : MonoBehaviour
{
    public Text scoreText;
    public bool isMultiplayer = false;
    int score = 0;

    float timer = 0.0f;

    bool countScore = true;


    // Start is called before the first frame update
    void Start()
    {
        if(countScore)
        {
            UpdateScoreText();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(countScore)
        {
            CheckTime();
            UpdateScoreText();
        }

    }

    void CheckTime()
    {
        timer += Time.deltaTime;
        if(timer > 1f)
        {
            timer = 0f;
            score += 1;
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }

    public void UpdateScore(int increaseValue)
    {
        score += increaseValue;
    }

    public void StopScoreCount()
    {
        countScore = false;
    }

    public void GameOver()
    {
        PlayerPrefs.SetInt("score", score);
        PlayerPrefs.Save();
        SceneManager.LoadScene(0);

    }
}
