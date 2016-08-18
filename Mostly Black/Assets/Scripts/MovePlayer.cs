using UnityEngine;
using System.Collections;

public class MovePlayer : MonoBehaviour {

    [SerializeField]
    Rigidbody2D _Rigid2D;

    [SerializeField]
    Transform _Trans;

    [SerializeField]
    Transform _Groundcheck;
    public LayerMask groundCheckLayer;
    public bool grounded;

    public float speed;

    ShootPlayer shootP;

    private float timeStampForFlight;
    public float coolDownForFlight = 2;
    private bool hasSetTimeSpamp;
    public bool isLeft;

    public float jumpPower;
    public bool playerIsJumping;
    public bool playerIsFlying;
    public bool playerIsRunning;

    public float flightPower;

    

    Animator _anima;

    void Awake()
    {
        _anima = gameObject.GetComponent<Animator>();
        shootP = gameObject.GetComponent<ShootPlayer>();
    }


    public bool landedSoundPlay = true;
    void UpdateGroundedStatus()
    {
        //1
        grounded = Physics2D.OverlapCircle(_Groundcheck.position, 0.05f, groundCheckLayer);

        //2
        _anima.SetBool("isGrounded", grounded);

        if (grounded && landedSoundPlay)
        {
            AudioManager.instance.PlayStep();
            landedSoundPlay = false;
        }
        if (!grounded)
        { landedSoundPlay = true; }
        
    }


    private bool leftArrowDown;
    private bool rightArrowDown;
    private bool upArrow;
    private bool upArrowShift;
    void movementInput()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            leftArrowDown = true;
            _Trans.localScale = new Vector3(-1, 1, 1);
           isLeft = true;
            if (!playerIsJumping && !playerIsFlying && shootP.FinishedShooting)
            {
                _anima.Play("PlayerRun");
                playerIsRunning = true;
               if (grounded)
                    AudioManager.instance.PlayStep();
            }
            else
            {
                playerIsRunning = false;
            }
        }
        else { leftArrowDown = false; }


        if (Input.GetKey(KeyCode.RightArrow))
        {
            rightArrowDown = true;
            _Trans.localScale = new Vector3(1, 1, 1);
            isLeft = false;
            isLeft = false;

            if (!playerIsJumping && !playerIsFlying && shootP.FinishedShooting)
            {
                _anima.Play("PlayerRun");
                playerIsRunning = true;
                if (grounded)
                    AudioManager.instance.PlayStep();
            }
            else
            {
                playerIsRunning = false;
            }
        }
        else { rightArrowDown = false; }

        if ((Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.UpArrow) && PlayerStats.instance.currentSpiritCound >= 0 && !playerIsJumping))
        {

            upArrowShift = true;
            
            
            playerIsFlying = true;
            _anima.Play("PlayerJump");
            AudioManager.instance.PlayFly();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && !playerIsFlying && !playerIsJumping)
        {
            upArrow = true;
            playerIsJumping = true;
            _anima.Play("PlayerJump");
            AudioManager.instance.PlayJump();
            Jump();
            StartCoroutine(IJUMOCOOLDWOWN());
        }
        else
        {
            upArrow = false;
            upArrowShift = false;
            
            if (!hasSetTimeSpamp)
            {
                timeStampForFlight = Time.time + coolDownForFlight;
                hasSetTimeSpamp = true;
            }
            if (timeStampForFlight <= Time.time)
            {
                playerIsFlying = false;
                hasSetTimeSpamp = false;
            }
        }
    }

    IEnumerator IJUMOCOOLDWOWN()
    {
        yield return new WaitForSeconds(1f);
        playerIsJumping = false;
    }

    void Jump()
    {
        _Rigid2D.AddForce(new Vector2(0, speed * jumpPower));
    }


    void Movement()
    {
        // for left
        if (leftArrowDown)
        {
            _Rigid2D.AddForce(new Vector2(-speed, 0));
        }

        //for right
        if (rightArrowDown)
        {
            _Rigid2D.AddForce(new Vector2(speed,0 ));
        }

        if (upArrowShift)
        {
            _Rigid2D.AddForce(new Vector2(0, speed * flightPower));
            PlayerStats.instance.currentSpiritCound--;
           
        }
        else { PlayerStats.instance.currentSpiritCound++;  }

        if (Input.GetKey(KeyCode.DownArrow))
        {

            _Rigid2D.velocity = new Vector2 (0, _Rigid2D.velocity.y - speed/4);
        }
        
    }



	void Update ()
    {
        movementInput();
	}

    void FixedUpdate()
    {
        Movement();
        UpdateGroundedStatus();
    }
}
