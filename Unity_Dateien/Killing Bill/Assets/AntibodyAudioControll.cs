using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntibodyAudioControll : MonoBehaviour
{

    public AudioSource BarrierBreakingSound;
    public AudioSource AntibodyDyingSound;

    // Update is called once per frame
    void Update()
    {

        if (Shield_Controller.shieldShouldBreak == true)
        {
            BarrierBreakingSound.Play();
            Shield_Controller.shieldShouldBreak = false;
        }

        if(DetectColisionAntibody.AntibodyShouldDie == true)
        {
            AntibodyDyingSound.Play();
            DetectColisionAntibody.AntibodyShouldDie = false;
        }

    }
}
