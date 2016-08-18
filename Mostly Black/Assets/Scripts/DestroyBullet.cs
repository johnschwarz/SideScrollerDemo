using UnityEngine;
using System.Collections;

public class DestroyBullet : MonoBehaviour {
    
	void Start () {
        StartCoroutine(DestroyMe());
        AudioManager.instance.PlayThrow();
    }
	


    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(this.gameObject);
    }


    IEnumerator DestroyMe()
    {
        yield return new WaitForSeconds(0.5f);

        Destroy(this.gameObject);
        
    }
}
