using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody playerRB;

    private float horizontalInput;
    private float verticalInput;
    private float mouseX;
    private float mouseY;
    private bool isOnGround = true;

    public GameObject Camera;

    public float moveSpeed = 5.0f;
    public float rotationSpeed = 2.0f;
    public float jumpForce = 30;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        playerRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        // Move forward and sidewards
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed * verticalInput);
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed * horizontalInput);


        // Camera-Rotation
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed; //Vertikal
        mouseY += Input.GetAxis("Mouse Y") * rotationSpeed; //Horizontal

        mouseY = Mathf.Clamp(mouseY, -90f, 90f);

        Camera.transform.localRotation = Quaternion.Euler(-mouseY, 0f, 0f);
        transform.rotation = Quaternion.Euler(0f, mouseX, 0f);


        // Player jumping
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        isOnGround = true;
    }
}
