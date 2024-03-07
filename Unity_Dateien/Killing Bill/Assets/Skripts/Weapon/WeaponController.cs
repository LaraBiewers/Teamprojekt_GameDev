using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour    
{
    // References
    public Camera fpsCam;
    public Transform waeponOutput;
    private AudioSource gunAudio;

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
    }

    // Update is called once per frame
    void Update()
    {
        // Transform WeaponPosition = transform.parent;

        // Launch a projectile from player
        if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            Shoot();
            // GameObject projectile = Instantiate(projectilePrefab, transform.position, WeaponPosition.rotation);
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

            // hit Antibody -> inflict damage
            DetectColisionAntibody target = hit.transform.GetComponent<DetectColisionAntibody>();

            if (target != null)
            {
                target.TakeDamage(damage);
            }
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
