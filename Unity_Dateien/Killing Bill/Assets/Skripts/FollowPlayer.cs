using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    
    // Deklariere wem die Kamera folgt
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Kamerabewegung und Offset
        transform.position = player.transform.position;
        transform.rotation = player.transform.rotation;
    }
}
