using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DetectColision : MonoBehaviour
{
    public int score;
    public TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        updateScore(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        updateScore(100);
        Destroy(other.gameObject);
    }

    public void updateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " +  score;
    }

}
