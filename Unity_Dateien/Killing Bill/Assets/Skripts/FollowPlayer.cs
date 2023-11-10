using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    
    // Deklariere wem die Kamera folgt
    public GameObject player;
    private Vector3 offset = new Vector3(0, 1, 0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Kamerabewegung und Offset
        transform.position = player.transform.position + offset;
        transform.rotation = player.transform.rotation;
    }
}
