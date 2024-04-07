using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.XR.WSA;

public class Shield_Controller : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] public const int maxHP = 100;
    public int miniVirusDmg = maxHP;
    [SerializeField] private int _bulletDamage;
    private Renderer rend;

    
    private Texture ShieldBase;
    private Texture ShieldLightCracked;
    private Texture ShieldMidCracked;
    private Texture ShieldFullCracked;

    public static bool shieldShouldBreak = false;
    
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        ShieldLightCracked = Resources.Load<Texture>("Materials/Shield_Lightly_Cracked");
        ShieldMidCracked = Resources.Load<Texture>("Materials/Shield_Mild_Cracked");
        ShieldFullCracked = Resources.Load<Texture>("Materials/Shield_Fully_Cracked");
        health = maxHP;
    }

    private void ShieldCheck()
    {
        switch (health)
        {
            case >= maxHP:
                break;
            case >= 70:
                rend.material.mainTexture = ShieldLightCracked;
                break;
            case >= 35:
                rend.material.mainTexture = ShieldMidCracked;
                break;
            case > 0:
                rend.material.mainTexture = ShieldFullCracked;
                break;
            case <=0:
                shieldShouldBreak = true; // Needed for SoundControll
                Destroy(gameObject);
                break;
        }
    }

    public void registerMinivirusHit()
    {
        TakeShieldDamage(maxHP);
    }

    public void registerBulletHit()
    {
        TakeShieldDamage(_bulletDamage);
    }
    

    void TakeShieldDamage(int amount)
    {
        Debug.Log("shield: taking damage");
        health -= amount;
        ShieldCheck();
    }
}
