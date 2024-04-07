using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    
    public float speed;
    public float directionChangeInterval;
    public float maxHeadingChange;
    public float rotationSpeed;
    public int turnCounterMax;
    private int turnCounterCurrent;
    private bool shouldTurnToPlayer = false;

    CharacterController controller;
    float heading;
    Vector3 targetRotation;


    void Start()
    {
        turnCounterCurrent = turnCounterMax;
        controller = GetComponent<CharacterController>();

        // Set random initial rotation
        heading = Random.Range(0, 360);
        transform.eulerAngles = new Vector3(0, heading, 0);

        StartCoroutine(NewHeading());
    }
   

    void Update()
    {
        if (!shouldTurnToPlayer)
        {
            transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, targetRotation, Time.deltaTime * rotationSpeed * directionChangeInterval);   
        }
        else
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            transform.forward = Vector3.RotateTowards(transform.forward, player.transform.position - transform.position, 6, 5);
        }
        var forward = transform.TransformDirection(Vector3.forward);
        controller.SimpleMove(forward * speed);
    }

    /// <summary>
    /// Repeatedly calculates a new direction to move towards.
    /// Use this instead of MonoBehaviour.InvokeRepeating so that the interval can be changed at runtime.
    /// </summary>
    IEnumerator NewHeading()
    {
        while (true)
        {
            if( turnCounterCurrent > 0 )
            {
                NewHeadingRoutine();
                turnCounterCurrent--;
                shouldTurnToPlayer = false;
                Debug.Log("going random");

            }
            else
            {
                Debug.Log("going to Player");

                shouldTurnToPlayer = true;
                turnCounterCurrent = turnCounterMax;
            }
            yield return new WaitForSeconds(directionChangeInterval);
        }
    }

    private void PlayerFacingRoutine()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        //heading = player.transform.TransformDirection(player.transform.position).y;
        Debug.Log("Player directon: " +  heading);
        targetRotation = new Vector3(0, heading, 0);
    }

    /// <summary>
    /// Calculates a new direction to move towards.
    /// </summary>
    void NewHeadingRoutine()
    {
        var floor = transform.eulerAngles.y - maxHeadingChange;
        var ceil = transform.eulerAngles.y + maxHeadingChange;
        heading = Random.Range(floor, ceil);
        targetRotation = new Vector3(0, heading, 0);
    }

    


}
