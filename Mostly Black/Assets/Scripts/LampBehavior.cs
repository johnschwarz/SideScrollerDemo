using UnityEngine;
using System.Collections;

public class LampBehavior : MonoBehaviour {

    Animator thisAnimator;
    public bool isOn = false;
    
	void Start ()
    {
        thisAnimator = gameObject.GetComponent<Animator>();
        thisAnimator.Play("LampOff");
        
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (isOn && other.gameObject.tag.Equals("Imp"))
        {
            other.gameObject.GetComponent<WakeImp>().WakeImpFromSleep();
        }
    }


    //unused...
    IEnumerator IStart()
    {
        yield return new WaitForSeconds(Random.Range(1, 8));
        thisAnimator.Play("LampOff");
    }

 

}
