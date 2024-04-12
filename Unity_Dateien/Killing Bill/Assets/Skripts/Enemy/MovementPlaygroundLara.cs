using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlaygroundLara : MonoBehaviour
{

    public float speed;
    public float directionChangeInterval;
    public float maxHeadingChange;
    public float rotationSpeed;

    private int randomTurnCounter;
    private bool shouldTurnToPlayer = false;

    CharacterController controller;
    float heading;
    Vector3 targetRotation;
    Vector3 lastPosition;

    // Start is called before the first frame update
    void Start()
    {
        randomTurnCounter = Random.Range(1,4);
        controller = GetComponent<CharacterController>();

        // Set random initial rotation
        heading = Random.Range(0, 360);
        transform.eulerAngles = new Vector3(0, heading, 0);

        lastPosition = transform.position;

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
            transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, targetRotation, Time.deltaTime * rotationSpeed * directionChangeInterval);
        }

        var forward = transform.TransformDirection(Vector3.forward);
        controller.SimpleMove(forward * speed);

        // BewegungsRoutine starten wenn der NPC an einer Stelle festh�ngt
        if (transform.position == lastPosition)
        {
            NewHeadingRoutine();
        }
        else
        {
            lastPosition = transform.position;
        }
    }

    /// <summary>
    /// Repeatedly calculates a new direction to move towards.
    /// Use this instead of MonoBehaviour.InvokeRepeating so that the interval can be changed at runtime.
    /// </summary>
    IEnumerator NewHeading()
    {
        while (true)
        {
            if (randomTurnCounter > 0)
            {
                //Debug.Log("going random");

                NewHeadingRoutine();
                randomTurnCounter--;
                shouldTurnToPlayer = false;
            }
            else
            {
                Debug.Log("going to Player");

                // Sch�fchen sollen nicht auf Spieler zulaufen
                if (!CompareTag("SheepCell"))
                {
                    TurnToPlayerRoutine();
                }
                shouldTurnToPlayer = true;
                randomTurnCounter = Random.Range(1, 4);
            }
            yield return new WaitForSeconds(directionChangeInterval);
        }
    }

    // Calculates a new direction to move towards.
    void NewHeadingRoutine()
    {
        var floor = transform.eulerAngles.y - maxHeadingChange;
        var ceil = transform.eulerAngles.y + maxHeadingChange;
        heading = Random.Range(floor, ceil);
        targetRotation = new Vector3(0, heading, 0);
    }

    void TurnToPlayerRoutine()
    {
        //heading = Random.Range(90, 270);
        //targetRotation = new Vector3(0, heading, 0);
        
        GameObject player = GameObject.FindWithTag("Player");

        Vector3 direction = player.transform.position - transform.position;
        direction.y = 0f;

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
}
