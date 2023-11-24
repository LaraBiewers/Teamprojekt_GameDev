using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour    
{

    public GameObject projectilePrefab;
    private Quaternion parentRotationEuler;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Launch a projectile from the player
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Transform parentTransform = transform.parent;

            // DO NOT USE CAPSULES AS BULLETS; ELSE WEIRD THINGS
            GameObject projectile = Instantiate(projectilePrefab, transform.position, parentTransform.rotation);
        }
    }
}
