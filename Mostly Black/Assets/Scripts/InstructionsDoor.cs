using UnityEngine;
using System.Collections;

public class InstructionsDoor : MonoBehaviour {

    private bool SawMe = false;
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag.Equals("Player") && SawMe == false)
        {
            StartCoroutine(PlayerStats.instance.ChangeInstructions("press DOWN arrow to exit", 3.5f));
            SawMe = true;

        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            StartCoroutine(PlayerStats.instance.ChangeInstructions("", 0));
        }
    }
}
