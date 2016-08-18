using UnityEngine;
using System.Collections;

public class LampExitCheck : MonoBehaviour {

    Animator thisAnimator;
    LampBehavior lampBS;



    void Start()
    {
        thisAnimator = gameObject.GetComponentInParent<Animator>();
        lampBS = gameObject.GetComponentInParent<LampBehavior>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            lampBS.isOn = true;
            thisAnimator.Play("LampOn");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            lampBS.isOn = false;
            thisAnimator.Play("LampOff");
        }
    }

    
}
