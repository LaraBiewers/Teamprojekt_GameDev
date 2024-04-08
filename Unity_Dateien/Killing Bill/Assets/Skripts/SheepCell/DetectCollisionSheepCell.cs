using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisionSheepCell : MonoBehaviour
{ 
    private GameManager gameManager;
    private WeaponController weaponController;

    public float health = 2f;
    public int amountNiniViruses = 3;
    private float projectileDamage;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        weaponController = GameObject.Find("Weapon").GetComponent<WeaponController>();
        projectileDamage = weaponController.damage;
    }

    private void OnParticleCollision(GameObject other)
    {
        TakeDamage(projectileDamage);
    }

    void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Vector3 pos = gameObject.transform.position;
        Destroy(gameObject);
        SpawnMiniViruses(pos);
    }

    void SpawnMiniViruses(Vector3 pos)
    {
        for(int i = 0; i < amountNiniViruses; i++)
        {
            var loadedObject = Resources.Load("Prefabs/miniVirus");
            Instantiate(loadedObject, pos, Quaternion.Euler(0, 0, 0));
        }
    }
}
