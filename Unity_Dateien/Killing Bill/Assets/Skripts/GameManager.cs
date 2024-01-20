using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int score;
    public int health;
    public TextMeshProUGUI scoreText;
    public Button restart;
    public TextMeshProUGUI gameOverText;
    public Slider HealthBar;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        updateScore(0);

        health = 100;
        HealthBar = GameObject.Find("HealthBar").GetComponent<Slider>();

        
    }

    // Update is called once per frame
    void Update()
    {
        if(health == 0)
        {
            Time.timeScale = 0.0f;
            gameOverText.gameObject.SetActive(true);
            restart.gameObject.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
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

    public void Restart()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
