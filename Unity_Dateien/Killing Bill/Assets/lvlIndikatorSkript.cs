using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lvlIndikatorSkript : MonoBehaviour
{
    public int currentLvL = 1;
    public bool shouldSurvive = false;
    
    void Awake()
    {
        GameObject[] indikators = GameObject.FindGameObjectsWithTag("lvlIndikator");
        if (indikators.Length > 1 && !shouldSurvive)
        {
            Destroy(this.gameObject);
        }
    }
}
