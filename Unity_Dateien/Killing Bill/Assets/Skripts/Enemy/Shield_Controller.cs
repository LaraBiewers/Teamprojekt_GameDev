using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.XR.WSA;

public class Shield_Controller : MonoBehaviour
{
    [SerializeField] private int Health;
    [SerializeField] private int maxHP;
    private Renderer rend;

    
    private Texture ShieldBase;
    private Texture ShieldLightCracked;
    private Texture ShieldMidCracked;
    private Texture ShieldFullCracked;
    
    
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        ShieldBase = Resources.Load<Texture>("Shield_Base");
        ShieldLightCracked = Resources.Load<Texture>("Shield_Lightly_Cracked");
        ShieldMidCracked = Resources.Load<Texture>("Shield_Mild_Cracked");
        ShieldFullCracked = Resources.Load<Texture>("Shield_Fully_Cracked");
        maxHP = 100;
        Health = maxHP;
    }

    private void ShieldCheck(int h)
    {
        if (h >= 85)
        {
            rend.material.mainTexture = ShieldBase;
        }
        else if(h >= 70)
        {
            rend.material.mainTexture = ShieldLightCracked;
        }
        else if (h >= 35)
        {
            rend.material.mainTexture = ShieldMidCracked;
        }
        else if (h > 0)
        {
            rend.material.mainTexture = ShieldFullCracked;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        ShieldCheck(Health);
        Health = Health > 100 ? 100 : Health;
        Health = Health < 0 ? 0 : Health;
    }
}
