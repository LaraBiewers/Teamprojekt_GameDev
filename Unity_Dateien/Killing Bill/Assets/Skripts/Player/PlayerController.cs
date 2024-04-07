using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Unity.UI;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    private Rigidbody playerRB;

    private float horizontalInput;
    private float verticalInput;
    private float mouseX;
    private float mouseY;
    private bool isOnGround = true;

    public GameObject Camera;
    private GameManager gameManager;

    public float moveSpeed = 5.0f;
    public float rotationSpeed = 1.0f;

    private float normalDrag;
    public float jumpDrag = -1f;
    private int jumpCounter = 2;
    public float jumpForce_First = 30;
    public float jumpForce_Second = 20;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerRB = GetComponent<Rigidbody>();

        normalDrag = playerRB.drag;
    }

    // Update is called once per frame
    void Update()
    {

        // Move forward and sidewards
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        Vector3 inputDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        transform.Translate(inputDirection * Time.deltaTime * moveSpeed);


        // Camera-Rotation
        if (!GameManager.gameIsOver)
        {
            mouseX += Input.GetAxis("Mouse X") * rotationSpeed; //Vertikal
            mouseY += Input.GetAxis("Mouse Y") * rotationSpeed; //Horizontal

            mouseY = Mathf.Clamp(mouseY, -90f, 90f);

            Camera.transform.localRotation = Quaternion.Euler(-mouseY, 0f, 0f);
            transform.rotation = Quaternion.Euler(0f, mouseX, 0f);
        }

        // Player jumping singleTime
        //if (Input.GetButtonDown("Jump") && isOnGround)
        //{
        //   playerRB.AddForce(Vector3.up * jumpForce_First, ForceMode.Impulse);
        //    isOnGround = false;
        //}

        // Player jumping doubleTime
        if (Input.GetButtonDown("Jump") && jumpCounter != 0)
        {

            if (jumpCounter == 2)
            {
                playerRB.AddForce(Vector3.up * jumpForce_First, ForceMode.Impulse);
            }
            if (jumpCounter == 1)
            {
                playerRB.AddForce(Vector3.up * jumpForce_Second, ForceMode.Impulse);
            }

            playerRB.drag = jumpDrag;

            jumpCounter--;
            isOnGround = false;
        }

        if (isOnGround)
        {
            jumpCounter = 2;
            playerRB.drag = normalDrag;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag(("Shield")))
        {
            gameManager.TakeDamage(20);
        }
        isOnGround = true;
    }

}
