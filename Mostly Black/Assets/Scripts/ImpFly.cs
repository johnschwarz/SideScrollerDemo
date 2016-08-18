using UnityEngine;
using System.Collections;

public class ImpFly : MonoBehaviour {

    //imp prefab has 2 colliders
    Rigidbody2D _RB2D;
    Animator _Anim;
    //used in WakeImp
    public bool isRandomFlying = false;
    public bool isAwake = false;

    [SerializeField]
    GameObject particleHit;

    Transform _SelfTrans;
    
    Transform _PlayerTrans;

    AudioSource aSource;

    WakeImp wakeImp;
    
	void Start ()
    {
        wakeImp = gameObject.GetComponentInChildren<WakeImp>();
        aSource = gameObject.GetComponent<AudioSource>();
        _SelfTrans = gameObject.GetComponent<Transform>();
        _Anim = gameObject.GetComponent<Animator>();
        _RB2D = gameObject.GetComponent<Rigidbody2D>();

        _PlayerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        _RB2D.gravityScale = 0.0f;
        
    }


    private bool changeDirection = true;
    private bool SatisfiedWithDirection = true;
    private bool setTime = true;
    float tempx;
    float tempy;
    public float changeDirectionTimer = 1;
    float timeStampForChangeDircetion;
    void CheckForFlight()
    {
        if (isRandomFlying && isAwake && !SwoopPlayer)
        {
            _RB2D.gravityScale = 0.8f;
            if (SatisfiedWithDirection)
            {
                _RB2D.velocity = new Vector3(0, 0, 0);
                _RB2D.AddForce(new Vector2(tempx, tempy));
                SatisfiedWithDirection = true;
                changeDirection = false;
                if (setTime)
                {
                    timeStampForChangeDircetion = Time.time + changeDirectionTimer;
                    setTime = false;
                }
                if (timeStampForChangeDircetion <= Time.time)
                {
                    changeDirection = true;
                    SatisfiedWithDirection = false;
                }

            }
            else if (changeDirection)
            {
                tempx = Random.Range(-20, 20);
                tempy = Random.Range(-20, 20);
                changeDirection = false;
                SatisfiedWithDirection = true;
                setTime = true;
                AudioManager.instance.PlayImpSound(aSource);

            }
        }
        else if (isAwake && !isRandomFlying && SwoopPlayer)
        {
            
                StartCoroutine(ISWOOP());
            
            
        }
    }

    public bool SwoopPlayer = false;
    public float swoopSpeed;

    private bool canSwoops;
    IEnumerator ISWOOP()
    {
        
        
        
        
        canSwoops = false;
        isRandomFlying = false;
        _RB2D.velocity = new Vector3(0, 0, 0);
        _RB2D.gravityScale = 0.0f;
        yield return new WaitForSeconds(1.2f);

        AudioManager.instance.PlayImpSound(aSource);


        _RB2D.AddForce((_PlayerTrans.position - _SelfTrans.position).normalized * swoopSpeed);

        canSwoops = true;
        

        isRandomFlying = true;
    }

    void FixedUpdate ()
    {
        CheckForFlight();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {

            _Anim.Play("ImpAwake");
            SwoopPlayer = false;
            PlayerStats.instance.currentSpiritCound = 0;
            PlayerStats.instance.Health--;
            //particleHit.SetActive(true);


            AudioManager.instance.PlayImpSound(aSource);

        }

        if (other.gameObject.tag.Equals("bullet"))
        {
            //Debug.Log( particleHit.isPlaying);
            particleHit.SetActive(true);
            //Debug.Log(particleHit.isPlaying);
            SwoopPlayer = false;
            isAwake = false;
            isRandomFlying = false;
            _RB2D.velocity = new Vector3(0, 0, 0);
            _Anim.Play("ImpDeath");
            StopAllCoroutines();
            Camera.main.GetComponent<CamShake>().Shake();
            AudioManager.instance.PlayImpact();
            StartCoroutine(PlayerStats.instance.ISlowTime());
            StartCoroutine(IDie());
            

        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
        if (isAwake) { 
            if (other.gameObject.tag.Equals("PlayerExitZone") && isRandomFlying && !SwoopPlayer)
            {
                isRandomFlying = false;
                isAwake = true;
                SwoopPlayer = true;
            }

            else if (other.gameObject.tag.Equals("PlayerExitZone") && SwoopPlayer)
            {
                isRandomFlying = false;
                isAwake = true;
                SwoopPlayer = true;
            }
            else
            {
                SwoopPlayer = true;
                isRandomFlying = false;
                isAwake = true;
            }
    }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isAwake)
        {

            if (other.gameObject.tag.Equals("PlayerExitZone") && !isRandomFlying && isAwake && !SwoopPlayer)
            {
                isRandomFlying = true;
                isAwake = true;
                SwoopPlayer = true;
            }

            if (other.gameObject.tag.Equals("PlayerExitZone") && SwoopPlayer)
            {
                isRandomFlying = false;
                isAwake = true;
                SwoopPlayer = true;
            }
        }
    }


    IEnumerator IDie()
    {
        
        aSource.Stop();
        AudioManager.instance.PlayImpSound(aSource);
        
        
        yield return new WaitForSeconds(1f);
        PlayerStats.instance.Kills++;
        Destroy(this.gameObject);
    }
}
