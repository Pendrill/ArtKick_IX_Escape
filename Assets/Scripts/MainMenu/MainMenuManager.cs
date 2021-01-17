using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenuManager : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject Credits;
    public GameObject ModeSelect;

    public GameObject Asteroid;
    public GameObject Ship;

    public Text scoreTracker;
    // Start is called before the first frame update
    void Start()
    {
        scoreTracker.text = "Your current single player highscore is: " + PlayerPrefs.GetInt("score");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnOnCredits()
    {
        Credits.SetActive(true);
        MainMenu.SetActive(false);
    }

    public void TurnOnMainMenu()
    {
        MainMenu.SetActive(true);
        Credits.SetActive(false);
    }

    public void TurnOnModeSelect()
    {
        MainMenu.SetActive(false);
        Asteroid.SetActive(false);
        Ship.SetActive(false);
        ModeSelect.SetActive(true);
    }

    public void SinglePlayerMode()
    {
        SceneManager.LoadScene(1);
    }

    public void MultiplayerMode()
    {
        SceneManager.LoadScene(2);
    }
}
