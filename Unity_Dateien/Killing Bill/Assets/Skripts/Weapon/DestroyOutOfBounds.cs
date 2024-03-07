using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{

    private float Bound = 40.0f;

    // Update is called once per frame
    void Update()
    {
        // Destroy Projectile when out of Map
        if ((transform.position.z > Bound) || (transform.position.z < -Bound) || (transform.position.x > Bound) || (transform.position.x < -Bound))
        {
            Destroy(gameObject);
        }
    }
}
