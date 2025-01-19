using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int enemiesAliveCount;
    public int objectsToDefendCount;
    private bool isPaused = false;
    private GameObject pauseScreen;
    private SceneTransition sceneTransition;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        sceneTransition = GameObject.Find("Scene Transition").GetComponent<SceneTransition>();
        if (SceneManager.GetActiveScene().buildIndex > 2)
        {
            pauseScreen = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
        }
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex > 2)
        {
            //while player is loaded into levels

            //pause the game. 
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                isPaused = !isPaused;
                if (isPaused)
                {
                    Time.timeScale = 0f;
                    pauseScreen.SetActive(true);
                    AudioListener.pause = true;
                }
                else if (!isPaused)
                {
                    Time.timeScale = 1f;
                    pauseScreen.SetActive(false);
                    AudioListener.pause = false;
                }
            }
        }
    }
    public void EnemyKilled()
    {
        enemiesAliveCount--;

        if(enemiesAliveCount == 0)
        {
            //Open the door or something I dunno
        }
    }

    public void ObjectToDefendKilled()
    {
        // The player lost the crystall ball
        ResetLevel();
    }

    public void ResetLevel()
    {
        sceneTransition.StartLoad(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToNextLevel()
    {
        sceneTransition.StartLoad(SceneManager.GetActiveScene().buildIndex+1);
    }
}
