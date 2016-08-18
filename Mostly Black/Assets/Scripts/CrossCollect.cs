using UnityEngine;
using System.Collections;

public class CrossCollect : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag.Equals("Player"))
        {

            PlayerStats.instance.Gold++;
            //make new sfx
            AudioManager.instance.PlayThrow();
            Destroy(this.gameObject);
        }
    }
}
