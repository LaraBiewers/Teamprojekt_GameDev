using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour    
{

    public Camera fpsCam;
    public GameObject projectilePrefab;

    private AudioSource gunAudio;

    public float fireRate = 0.25f;

    private float nextFire;

    // Start is called before the first frame update
    void Start()
    {
        gunAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Launch a projectile from the player
        if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        gunAudio.Play();
        nextFire = Time.time + fireRate;

        //Transform parentTransform = transform.parent;
        //GameObject projectile = Instantiate(projectilePrefab, transform.position, parentTransform.rotation);

        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit))
        {
            Debug.Log(hit.transform.name);
        }

    }
}
