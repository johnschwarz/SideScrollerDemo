using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Linq;

public class PlayerStats : MonoBehaviour {

    private static PlayerStats Instance;
    public static PlayerStats instance {
        get { return Instance; }
    }

    [SerializeField]
    Slider spirtSlider;

    [SerializeField]
    GameObject gameOverGO;
    [SerializeField]
    Text scoreLabel;
    MovePlayer movePlayerScr;

    [SerializeField]
    GameObject levelIntroGO;
    [SerializeField]
    Text levelText;
    [SerializeField]
    Text timerText;

    [SerializeField]
    Text instructionsText;

  
    //add a fade
    public IEnumerator ChangeInstructions(string inString, float inTime)
    {
  
        instructionsText.text = inString;
        yield return new WaitForSeconds(inTime);
       
        instructionsText.text = "";
    }

    private float _CurrentSpiritCount;
    private float _MaxSpiritCount = 50;
    public float currentSpiritCound
    {
        get { return _CurrentSpiritCount; }
        set
        {
          
            _CurrentSpiritCount = value;
           
        }
    }

    private int CurrentWorld =1;
    public int CurrentLevel =1;


    public bool shouldShowLevel = true;
    //World updates and level resters every fourth level 
    public IEnumerator ShowLevel()
    {
        timeTillExpired = 45;
        shouldShowLevel = false;
        if (SceneManager.GetActiveScene().buildIndex +1 != CurrentLevel)
        {
            CurrentLevel++;
        }
        if ((CurrentLevel-1) % 4 == 0)
        {
            if (SceneManager.GetActiveScene().buildIndex != 0)
            {
                CurrentWorld++;
            }
            CurrentLevel = 1;
        }    
            levelIntroGO.SetActive(true);
            levelText.text = "World " + CurrentWorld + " - Stage " + CurrentLevel;
            yield return new WaitForSeconds(2);
            levelIntroGO.SetActive(false);
    }
    
    //for life display and instance
    void Awake()
    {
        healthIndicator = GameObject.FindGameObjectsWithTag("HealthIcon").OrderBy(go => go.name).ToArray();
        
        movePlayerScr = GameObject.FindGameObjectWithTag("Player").GetComponent<MovePlayer>();
        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        
           else {
            Instance = this;
        }
       DontDestroyOnLoad(this.gameObject);
    }

    //stats, time, gameover, and active life
	void Start ()
    {
        instructionsText.text = "";
        currentSpiritCound = 50;
        Gold = 0;
        spirtSlider.value = _CurrentSpiritCount;
        spirtSlider.maxValue = _MaxSpiritCount;
        spirtSlider.minValue = 0;

        Health = healthIndicator.Length;
        gameOverGO.SetActive(false);
        Time.timeScale = 1f;

        foreach (GameObject i in healthIndicator)
        {
            i.SetActive(true);
        }
    }

    
    private float timeTillExpired = 45;
    public float timer;
    private float timeStamp;
    void CountDownForEachLevel()
    {
        timeTillExpired -= Time.deltaTime;
        int tempdisplay = Mathf.RoundToInt(timeTillExpired);
        timerText.text = tempdisplay.ToString();
        if (timeTillExpired <= 0)
        { gameOver = true; }
    }

    void Update()
    {
        if (shouldShowLevel)
        {
            StartCoroutine(ShowLevel());
        }

        CountDownForEachLevel();
       

    }

    //for pause when hit/hitting
    public float slowTimeInterval;
    public IEnumerator ISlowTime()
    {
        Time.timeScale = 0.1f;
        yield return new WaitForSeconds(slowTimeInterval);
        Time.timeScale = 1f;
    }

    void FixedUpdate () {
        if (!gameOver)
        {   
            spirtSlider.value = currentSpiritCound;
            spirtSlider.maxValue = _MaxSpiritCount;
            spirtSlider.minValue = 0;

            if (currentSpiritCound >= _MaxSpiritCount)
            {
                currentSpiritCound = _MaxSpiritCount;
            }
        }
        else if (gameOver)
        {
            Time.timeScale = 0.1f;
            gameOverGO.SetActive(true);
        }
    }

    //used in the UI and when you lose
    public void StartOver()
    {
        StartCoroutine(ChangeInstructions("", 0));
        shouldShowLevel = true;
        Gold = Gold/2;
        Kills = Kills / 2;
        int indexSC = SceneManager.GetActiveScene().buildIndex;
        gameOver = false;
        SceneManager.LoadScene(indexSC);
        Time.timeScale = 1f;
        gameOverGO.SetActive(false);
        Health = healthIndicator.Length-1;
    }

    
    //gold is the goods/collected crosses
    private int gold;
    private int kills;
    public int Kills
    {
        get { return kills; }
        set
        {
            kills = value;
            scoreLabel.text = "Kills: " + kills + "\nGoods: " + gold;
        }
    }
    public int Gold
    {
        get { return gold; }
        set
        {
            gold = value;
            scoreLabel.text = "Kills: " + kills+ "\nGoods: " + gold;
        }
    }

    public Text healthLabel;
    private GameObject[] healthIndicator;
    private int health;
    public bool gameOver = false;
    public int Health
    {
        get { return health; }
        set
        {
            if (value < health)
            {
                Camera.main.GetComponent<CamShake>().Shake();
                AudioManager.instance.PlayImpact();
                StartCoroutine(ISlowTime());
            }

            health = value;
           

            if (health <= 0 && !gameOver)
            {
                gameOver = true;
                //GameObject gameOverText = GameObject.FindGameObjectWithTag("GameOver");
               // gameOverText.GetComponent<Animator>().SetBool("gameOver", true);
            }

            for (int i = 0; i < healthIndicator.Length; i++)
            {
                if (i < Health)
                {
                    healthIndicator[i].SetActive(true);
                }
                else
                {
                    healthIndicator[i].SetActive(false);
                }
            }
        }
    }
   



}
