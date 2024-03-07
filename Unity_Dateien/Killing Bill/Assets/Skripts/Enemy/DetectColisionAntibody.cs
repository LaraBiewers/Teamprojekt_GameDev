using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DetectColisionAntibody : MonoBehaviour
{
    private GameManager gameManager;
    private WeaponController WeaponController;

    public float health = 2f;
    private float projectileDamage;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        WeaponController = GameObject.Find("Weapon").GetComponent<WeaponController>();
        projectileDamage = WeaponController.damage;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            Destroy(other.gameObject);
            TakeDamage(projectileDamage);
        }
    }

    void TakeDamage (float amount)
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
