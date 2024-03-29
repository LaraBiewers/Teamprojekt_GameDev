using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.XR.WSA;

public class Shield_Controller : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int maxHP;
    [SerializeField] private int _bulletDamage;
    private Renderer rend;

    
    private Texture ShieldBase;
    private Texture ShieldLightCracked;
    private Texture ShieldMidCracked;
    private Texture ShieldFullCracked;
    
    
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        ShieldLightCracked = Resources.Load<Texture>("Materials/Shield_Lightly_Cracked");
        ShieldMidCracked = Resources.Load<Texture>("Materials/Shield_Mild_Cracked");
        ShieldFullCracked = Resources.Load<Texture>("Materials/Shield_Fully_Cracked");
        maxHP = 100;
        health = maxHP;
    }

    private void ShieldCheck(int h)
    {
        switch (h)
        {
            case >= 70:
                rend.material.mainTexture = ShieldLightCracked;
                break;
            case >= 35:
                rend.material.mainTexture = ShieldMidCracked;
                break;
            case > 0:
                rend.material.mainTexture = ShieldFullCracked;
                break;
            default:
                Destroy(gameObject);
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        ShieldCheck(health);
        health = health > 100 ? 100 : health;
        health = health < 0 ? 0 : health;
    }

    void OnParticleCollision(GameObject other)
    {
        // if (other.CompareTag("Projectile"))
        //{
        //Destroy(other.gameObject);
        TakeShieldDamage(_bulletDamage);
        //}
    }

    private void OnCollision(Collision other)
    {
        if (other.gameObject.CompareTag("MiniVirus"))
        {
            TakeShieldDamage(maxHP);
        }
    }
    void TakeShieldDamage(int amount)
    {
        health -= amount;
    }
}
