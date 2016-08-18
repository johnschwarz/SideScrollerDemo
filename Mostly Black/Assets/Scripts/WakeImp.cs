using UnityEngine;
using System.Collections;

public class WakeImp : MonoBehaviour {

    private Animator _Anim;

    ImpFly IFScript;

    AudioSource parentaS;


    //imp prefab has 2 colliders

    void Start()
    {
        parentaS = gameObject.GetComponentInParent<AudioSource>();
        IFScript = gameObject.GetComponentInParent<ImpFly>();
        _Anim = gameObject.GetComponentInParent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
    
                _Anim.Play("ImpAwake");
            AudioManager.instance.PlayImpSound(parentaS);
            IFScript.isRandomFlying = true;
            IFScript.isAwake = true;
        }

        if (other.gameObject.tag.Equals("bullet"))
        {

            _Anim.Play("ImpDeath");
            StartCoroutine(IDie());
            
        }
    }

    public void WakeImpFromSleep()
    {
        _Anim.Play("ImpAwake");
        AudioManager.instance.PlayImpSound(parentaS);
        IFScript.isRandomFlying = true;
        IFScript.isAwake = true;
    }

    IEnumerator IDie()
    {
        
        yield return new WaitForSeconds(2f);
        AudioManager.instance.PlayImpSound(parentaS);
        Destroy(this.gameObject.transform.parent.gameObject);
        PlayerStats.instance.Kills++;
    }
}
