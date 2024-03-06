using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DetectColision : MonoBehaviour
{

    private GameManager gameManager;

    public float health = 2f;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    Destroy(gameObject);
    //    gameManager.updateScore(100);
    //    Destroy(other.gameObject);
    //}

    public void TakeDamage (float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die ()
    {
        Destroy(gameObject);
        gameManager.updateScore(100);
    }

}
