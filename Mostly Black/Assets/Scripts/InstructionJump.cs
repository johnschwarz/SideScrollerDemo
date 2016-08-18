using UnityEngine;
using System.Collections;

public class InstructionJump : MonoBehaviour {

    private bool SawMe = false;
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag.Equals("Player") && SawMe == false)
        {
            StartCoroutine(            PlayerStats.instance.ChangeInstructions("press UP arrow to jump", 3.5f));
            SawMe = true;
            
        }
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            StartCoroutine(PlayerStats.instance.ChangeInstructions("", 0));
        }
    }
}
