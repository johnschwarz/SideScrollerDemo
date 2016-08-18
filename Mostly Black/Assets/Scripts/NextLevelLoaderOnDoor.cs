using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NextLevelLoaderOnDoor : MonoBehaviour
{
    private bool LoadNextLevel;
    private bool ReadyToLeave;

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag.Equals("Player"))
        {
            ReadyToLeave = true;
            shouldStart = true;

        }
    }

    void OnTriggerExit2D(Collider2D other)
    {

        if (other.gameObject.tag.Equals("Player"))
        {
            ReadyToLeave = false;
            shouldStart = false;
        }
    }

    private bool shouldStart;
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.DownArrow) && ReadyToLeave)
        {
            ChangeLevel();

        }

    }

    void ChangeLevel()
    {
        int indexSC = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(indexSC + 1);
        PlayerStats.instance.shouldShowLevel = true;
        

    }
    
}
