using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private int spawnRange;
    [SerializeField] private int anzahlSpawns;
    public GameObject prefab;
    private Transform ownTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        ownTransform = GetComponent<Transform>();
        Spawninfunction(anzahlSpawns, ownTransform);
    }

    Vector3 RandomPosition(float range)
    {
        return ownTransform.position + new Vector3(Random.Range(-range, range),2, Random.Range(-range, range));
    }
    void Spawninfunction (int count, Transform posi)
    {
        for (var i = 0; count > i; ++i)
        {
            Vector3 spawnPoint = RandomPosition(spawnRange);
            if (!Physics.CheckSphere(spawnPoint, 1f))
            {
                Instantiate(prefab, spawnPoint,Quaternion.identity, null);
            }
            else
            {
                --i;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}