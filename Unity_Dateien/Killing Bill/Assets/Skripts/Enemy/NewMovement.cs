
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NewMovement : MonoBehaviour
{ 

    private float speed = 15;

    public LayerMask whatIsGround;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    // Start is called before the first frame update
    void Start()
    {
        Patroling();
        
    }

    // Update is called once per frame
    void Update()
    {
        Patroling();
    }

    private void Patroling()
    {
        if (!walkPointSet) 
            SearchWalkPoint();

        transform.position = Vector3.MoveTowards(transform.position, walkPoint, speed * Time.deltaTime);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }
}
