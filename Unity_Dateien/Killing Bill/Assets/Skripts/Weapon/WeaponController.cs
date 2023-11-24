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
            parentRotationEuler = parentTransform.rotation;

            Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
        }
    }
}
