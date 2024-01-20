using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawnerspawner : MonoBehaviour
{
    private Transform ownTransform;
    [SerializeField] private int spawnCount;
    public GameObject spawnerPrefab;
    [SerializeField] private int spawnRange;
    public Transform playerTransform;
    [SerializeField] private int minDistanceFromPlayer;

    // Start is called before the first frame update
    void Start()
    {
        ownTransform = GetComponent<Transform>();
        playerTransform = GameObject.Find("Player").transform;
        SpawningFunction(spawnRange);
    }
    
    Vector3 RandomPosition(int range)
    {
        RaycastHit hit;
        Vector3 position;
        do
        {
            position = ownTransform.position +
                       new Vector3(Random.Range(-range, range), 0, Random.Range(-range, range));


            if (Physics.Raycast(position, Vector3.down, out hit))
            {
                position.y = hit.point.y;
            }
        } while (Vector3.Distance(position, playerTransform.position) < minDistanceFromPlayer);

        return position;
    }
    
    void SpawnRandom(int quantity, int range)
    {
        for (var i = 0; quantity > i; ++i)
        {
            Instantiate(spawnerPrefab, RandomPosition(range),Quaternion.identity);
        }
    }

    void SpawningFunction(int area)
    {

        SpawnRandom(spawnCount,area);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}