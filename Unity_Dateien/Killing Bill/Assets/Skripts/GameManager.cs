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
    public int scoreGoal;
    public int health;
    public static bool gameIsOver = false;

    // Managing GameFlow. IMPORTENT, DO NOT DELETE!
    private bool isWon;
    private bool isDead;

    // GameOverScreen
    public GameObject winningScreenText;
    public GameObject losingScreenText;
    public GameObject gameOverUI;
    public GameObject continueToNextLevelButton;

    // Other UI Elements
    public TextMeshProUGUI scoreText;
    public Slider HealthBar;
    public RawImage Crossheir;

    // SoundControll
    public AudioSource DyingSound;



    // Start is called before the first frame update
    void Start()
    {

        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position.Set(1, 1.5f, 1);
        
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
            continueToNextLevelButton.SetActive(true);
            gameOverScreen();
            Debug.Log("You won the Game!");
        }
        
        // Handling GameLosing
        if(health == 0 && !isDead)
        {
            isDead = true;
            DyingSound.Play();
            losingScreenText.SetActive(true);
            gameOverScreen();
            Debug.Log("Player is Dead!");
        }
        
        if (Input.GetKeyDown(KeyCode.N))
        {
            killAll();
        }

    }

    // Töte alles -> Game wird gewonnen
    public void killAll()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] sheep = GameObject.FindGameObjectsWithTag("SheepCell");

        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        foreach (GameObject shep in sheep)
        {
            Destroy(shep);
        }

        updateScore(scoreGoal);
    }

    // GENIALE METHODE: noch ohne Funktion
    private void regenScene()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] sheep = GameObject.FindGameObjectsWithTag("SheepCell");
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("LvLTiles");

        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }foreach (GameObject shep in sheep)
        {
            Destroy(shep);
        }foreach (GameObject tile in tiles)
        {
            Destroy(tile);
        }


        health = 100;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position.Set(1, 1.5f, 1);

        GetComponentInParent<TileManagement1>().currentLevel++;
        GetComponentInParent<TileManagement1>().MakeRoom();
        GetComponentInParent<TileManagement1>().spawnEntities();
        
    }

    // Update Score-UI-Text
    public void updateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    // Player taking Damage
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
    
    // GameOverScreen functionality 0 (Just in case of winning)
    public void continueGame()
    {
        GameObject lvlIndikator = GameObject.FindGameObjectWithTag("lvlIndikator");
        ++lvlIndikator.GetComponent<lvlIndikatorSkript>().currentLvL;
        lvlIndikator.GetComponent<lvlIndikatorSkript>().shouldSurvive = true;
        
        DontDestroyOnLoad(lvlIndikator);
        
        restart();
    }

    public void startNewGame()
    {
        GameObject lvlIndikator = GameObject.FindGameObjectWithTag("lvlIndikator");
        lvlIndikator.GetComponent<lvlIndikatorSkript>().currentLvL = 1;
        
        lvlIndikator.GetComponent<lvlIndikatorSkript>().shouldSurvive = true;
        
        DontDestroyOnLoad(lvlIndikator);
        restart();
    }

    // GameOverScreen functionality 1
    private void restart()
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
