using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
    
    AudioSource PlayerAudio;

    [SerializeField]
    AudioClip[] Jumps;
    [SerializeField]
    AudioClip[] Throws;
    [SerializeField]
    AudioClip[] Impacts;
    [SerializeField]
    AudioClip[] Steps;
    [SerializeField]
    AudioClip[] ImpSounds;


    public void PlayImpSound(AudioSource selfSource)
    {
        if (!selfSource.isPlaying)
        selfSource.PlayOneShot(ImpSounds[Random.Range(0, ImpSounds.Length)]);
      
    }

    public void PlayStep()
    {
        if (PlayerAudio == null)
        {
            PlayerAudio = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
        }

        if (!PlayerAudio.isPlaying)
        {
            PlayerAudio.volume = 0.2f;
            PlayerAudio.PlayOneShot(Steps[Random.Range(0, Steps.Length)]);
        }
    }

    public void PlayJump()
    {
        if (PlayerAudio == null)
        {
            PlayerAudio = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
        }
        PlayerAudio.volume = 1f;
        PlayerAudio.PlayOneShot(Jumps[Random.Range(0, Jumps.Length)]);
    }

    public void PlayFly()
    {
        if (PlayerAudio == null)
        {
            PlayerAudio = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
        }
        if (!PlayerAudio.isPlaying)
        {
            PlayerAudio.volume = 0.8f;
            PlayerAudio.PlayOneShot(Jumps[Random.Range(0, Jumps.Length)]);
        }
    }

    public void PlayThrow()
    {
        if (PlayerAudio == null)
        {
            PlayerAudio = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
        }
        PlayerAudio.volume = 1f;
        PlayerAudio.PlayOneShot(Throws[Random.Range(0, Throws.Length)]); }

    public void PlayImpact()
    {
        if (PlayerAudio == null)
        {
            PlayerAudio = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
        }
        PlayerAudio.volume = 0.2f;
        PlayerAudio.PlayOneShot(Impacts[Random.Range(0, Impacts.Length)]); }


    private static AudioManager Instance;
    public static AudioManager instance {
        get { return Instance; }    }
	void Awake() {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            Instance = this;
        }
        PlayerAudio = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
        PlayerAudio.Stop();

    }
	
}
