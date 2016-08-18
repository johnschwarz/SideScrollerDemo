using UnityEngine;
using System.Collections;

public class CheckForPlayerFallingDeath : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.tag.Equals("Player"))
        {
        
            PlayerStats.instance.gameOver = true;
            AudioManager.instance.PlayImpact();
        } 
    }
}
