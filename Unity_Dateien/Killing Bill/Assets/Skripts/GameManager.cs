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
    public int health;
    public static bool gameIsOver = false;
    private bool isDead;

    // GameOverScreen
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
        if(health == 0 && !isDead)
        {
            isDead = true;
            gameOver();
            Debug.Log("Player is Dead!");
        }
    }

    public void gameOver()
    {
        gameIsOver = true;

        gameOverUI.SetActive(true);
        Crossheir.gameObject.SetActive(false);

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

    public void restart()
    {
        gameIsOver = false;

        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        Debug.Log("Restart");
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        
        Debug.Log("Main Menu");
    }

    public void quit()
    {
        Application.Quit();

        Debug.Log("Quit");
    }
}
