using UnityEngine;
using System.Collections;

public class InstructionsThrow : MonoBehaviour {

    private bool SawMe = false;
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag.Equals("Player") && SawMe == false)
        {
            StartCoroutine(PlayerStats.instance.ChangeInstructions("press SPACE to throw", 3f));
            SawMe = true;

        }
    }
    void Update () {
        if (Input.GetKey(KeyCode.Space))
        {
            StartCoroutine(PlayerStats.instance.ChangeInstructions("", 0));
        }
    }
}
