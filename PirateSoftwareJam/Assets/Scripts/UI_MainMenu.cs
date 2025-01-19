using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour
{
    [SerializeField] SceneTransition sceneTransition;

    public void BackToMainMenu()
    {
        sceneTransition.StartLoad(0);
    }

    public void Options()
    {
        sceneTransition.StartLoad(1);
    }

    public void LevelSelection()
    {
        sceneTransition.StartLoad(2);
    }

    public void StartGame()
    {
        sceneTransition.StartLoad(3);
    }
}
