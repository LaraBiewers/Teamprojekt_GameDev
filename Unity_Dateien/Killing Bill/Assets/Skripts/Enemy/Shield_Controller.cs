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
        ShieldLightCracked = Resources.Load<Texture>("Materials/Shield_Lightly_Cracked");
        ShieldMidCracked = Resources.Load<Texture>("Materials/Shield_Mild_Cracked");
        ShieldFullCracked = Resources.Load<Texture>("Materials/Shield_Fully_Cracked");
        maxHP = 100;
        Health = maxHP;
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
        ShieldCheck(Health);
        Health = Health > 100 ? 100 : Health;
        Health = Health < 0 ? 0 : Health;
    }
}
