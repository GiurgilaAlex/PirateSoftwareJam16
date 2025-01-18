using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int enemiesAliveCount;
    public int objectsToDefendCount;

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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
