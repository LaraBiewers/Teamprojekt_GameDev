using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public int score;
    public int scoreGoal = 400;
    public int health;
    public static bool gameIsOver = false;

    // Managing GameFlow. IMPORTENT, DO NOT DELETE!
    private bool isWon;
    private bool isDead;

    // GameOverScreen
    public GameObject winningScreenText;
    public GameObject losingScreenText;
    public GameObject gameOverUI;

    // Other UI Elements
    public TextMeshProUGUI scoreText;
    public Slider HealthBar;
    public RawImage Crossheir;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        health = 100;

        updateScore(0);
        
        HealthBar = GameObject.Find("HealthBar").GetComponent<Slider>();
        Crossheir = GameObject.Find("Crossheir").GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        // Handling GameWinning
        if(score >= scoreGoal && !isWon)
        {
            isWon = true;
            winningScreenText.SetActive(true);
            gameOverScreen();
            Debug.Log("You won the Game!");
        }
        
        // Handling GameLosing
        if(health == 0 && !isDead)
        {
            isDead = true;
            losingScreenText.SetActive(true);
            gameOverScreen();
            Debug.Log("Player is Dead!");
        }
    }

    // Showing GameOverScreen
    public void gameOverScreen()
    {
        gameIsOver = true;

        gameOverUI.SetActive(true);
        Crossheir.gameObject.SetActive(false);
        HealthBar.gameObject.SetActive(false);

        Time.timeScale = 0.0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void updateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health < 0)
        {
            health = 0;
        }

        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        HealthBar.value = health;
    }

    // GameOverScreen functionality 1
    public void restart()
    {
        Debug.Log("Restart");

        gameIsOver = false;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // GameOverScreen functionality 2
    public void mainMenu()
    {
        Debug.Log("Main Menu");

        gameIsOver = false;
        SceneManager.LoadScene("MainMenu");
    }

    // GameOverScreen functionality 3
    public void quit()
    {
        Debug.Log("Quit");

        Application.Quit();
    }
}
