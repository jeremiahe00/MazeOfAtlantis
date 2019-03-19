using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{

    public void MainMenuSceneClick()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void ToggleAudio (GameObject Aud)
    {
        Aud.SetActive(Aud);
    }

    public void ToggleNotifications(GameObject Not)
    {
        Not.SetActive(Not);
    }

    public void NToggle(GameObject NTog)
    {
        NTog.SetActive(!NTog);
    }
}
