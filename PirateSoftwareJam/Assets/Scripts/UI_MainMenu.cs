using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour
{
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Options()
    {
        SceneManager.LoadScene(2);
    }

    public void LevelSelection()
    {
        SceneManager.LoadScene(3);
    }
}
