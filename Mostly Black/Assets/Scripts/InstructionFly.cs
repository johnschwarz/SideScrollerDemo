using UnityEngine;
using System.Collections;

public class InstructionFly : MonoBehaviour {

    private bool SawMe = false;
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag.Equals("Player") && SawMe == false)
        {
            StartCoroutine(PlayerStats.instance.ChangeInstructions("hold LEFT SHIFT and UP ARROW to fly.  \nhint: You can jump after flying", 4f));
            SawMe = true;

        }
    }
}
