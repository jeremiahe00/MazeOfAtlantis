using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void PlayAgainSceneClick()
    {
        SceneManager.LoadScene("PlayGame");
    }

    public void MainMenuSceneClick()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
