using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour    
{
    // References
    public Camera fpsCam;
    public Transform waeponOutput;
    private AudioSource gunAudio;
    private GameManager gameManager;

    // bullet
    public GameObject projectilePrefab;

    // bullet force
    public float shootForce;

    // shooting characteristics
    public float damage = 1f;
    public float fireRate = 0.25f;
    private float nextFire;

    // Start is called before the first frame update
    void Start()
    {
        gunAudio = GetComponent<AudioSource>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Launch a projectile from player
        if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            Shoot();
        }

        if (GameManager.gameIsOver)
        {
            gunAudio.Stop();
        }
    }

    void Shoot()
    {
        gunAudio.Play();
        nextFire = Time.time + fireRate;

        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); //Just a ray through the middle of current view

        RaycastHit hit;
        Vector3 targetPoint;

        // check if ray hits something
        if (Physics.Raycast(ray, out hit))
        {
            // Logging what's hit and where
            Debug.Log(hit.transform.name);
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(75); //Just a point far away from the player
        }

        // direction from attackPoint to targetPoint
        Vector3 direction = targetPoint - waeponOutput.position;

        // Instantiate bullet/projectile
        GameObject currentProjectile = Instantiate(projectilePrefab, waeponOutput.position, Quaternion.identity); 
        currentProjectile.transform.forward = direction.normalized;

        // Add forces to bullet
        currentProjectile.GetComponent<Rigidbody>().AddForce(direction.normalized * shootForce, ForceMode.Impulse);

        // Currently not-existing effects :3

    }
}
