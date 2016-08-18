using UnityEngine;
using System.Collections;

public class ShootPlayer : MonoBehaviour {

    [SerializeField]
    GameObject spiritBulletGO;
    Transform parentTrans;
    MovePlayer movePlayerScript;

    Animator _anima;

    [SerializeField]
    float shootspawnDistance;
    public float shootforce;

    public bool FinishedShooting = true;

    void Awake()
    {
        _anima = gameObject.GetComponent<Animator>();
        parentTrans = gameObject.GetComponent<Transform>();
        movePlayerScript = gameObject.GetComponent<MovePlayer>();
    }

    private bool canFire;



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && PlayerStats.instance.currentSpiritCound >= 10 )
        {

            FinishedShooting = false;
            canFire = true;
            movePlayerScript.playerIsRunning = false;
            _anima.Play("PlayerThrow");
           
        }
    }

    public void ControlShooting()
    {

        FinishedShooting = true;
        
    }

    //is called in the animation
    public void Fire()
    {

        if (canFire)
        {
            if (movePlayerScript.isLeft)
            {
                GameObject temp = (GameObject)Instantiate(spiritBulletGO, new Vector3(parentTrans.position.x + -shootspawnDistance, parentTrans.position.y, 0), Quaternion.identity);
                temp.GetComponent<Rigidbody2D>().velocity = new Vector2(-shootforce, 0);
                PlayerStats.instance.currentSpiritCound = 0;
            }
            else
            {
                GameObject temp = (GameObject)Instantiate(spiritBulletGO, new Vector3(parentTrans.position.x + shootspawnDistance, parentTrans.position.y, 0), Quaternion.identity);
                temp.GetComponent<Rigidbody2D>().velocity = new Vector2(shootforce, 0);
                PlayerStats.instance.currentSpiritCound = 0;
            }
            canFire = false;
        }
    }



}
