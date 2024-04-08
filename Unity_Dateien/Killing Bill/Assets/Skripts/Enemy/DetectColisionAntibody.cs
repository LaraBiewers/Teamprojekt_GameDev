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

    public static bool AntibodyShouldDie = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        WeaponController = GameObject.Find("Weapon").GetComponent<WeaponController>();
        projectileDamage = WeaponController.damage;
    }

    void OnParticleCollision(GameObject other)
    {
        if(other.CompareTag("Projectile"))
        {
            if (GetComponentInChildren<Shield_Controller>() != null)
            {
                Shield_Controller shield_Controller = GetComponentInChildren<Shield_Controller>();
                shield_Controller.registerBulletHit();
            }
            else
            {
                TakeDamage(projectileDamage);
            }
        }
    }

    void TakeDamage (float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            AntibodyShouldDie = true; // Needed for SoundControll
            Die();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("MiniVirus"))
        {
            if (GetComponentInChildren<Shield_Controller>() != null)
            {
                Shield_Controller shield_Controller = GetComponentInChildren<Shield_Controller>();
                shield_Controller.registerMinivirusHit();
                Destroy(other.gameObject);
            }
        }
    }

    void Die ()
    {
        Destroy(gameObject);
        gameManager.updateScore(100);
    }

}
