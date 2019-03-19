using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlaySceneClick()
    {
        SceneManager.LoadScene("PlayGame");
    }

    public void LeaderboardSceneClick()
    {
        SceneManager.LoadScene("LeaderboardScene");
    }

    public void StatsSceneClick()
    {
        SceneManager.LoadScene("StatsScene");
    }

    public void SettingsSceneClick()
    {
        SceneManager.LoadScene("SettingsScene");
    }

    public void TutorialModeSceneClick()
    {
        SceneManager.LoadScene("TutorialScene");
    }

}
