using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{ 
    private float speed = 5.0f;
    private float turnSpeed = 15.0f;
    private float horizontalInput;
    private float verticalInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // Get playerInput
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // Move forward
        transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput);
        // Rotate sideward
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);

    }
}
