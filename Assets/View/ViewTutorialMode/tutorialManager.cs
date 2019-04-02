using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class tutorialManager : MonoBehaviour
{
    public List<tutorialMode> Tutorials = new List<tutorialMode>();

    public Text tutText; //show the tips/messages to the player as they go through the game

    private static tutorialManager instance;

    public static tutorialManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<tutorialManager>();
            }
            if (instance == null)
                Debug.Log("Tutorial Manager not found");

            return instance;
        }
    }

    private tutorialMode currentTutorial;

    // Start is called before the first frame update
    public void Start()
    {
        setNextTutorial(0);
    }

    // Update is called once per frame
    public void Update()
    {
        if (currentTutorial)
            currentTutorial.checkIfHappening();
    }

    //When a specific tutorial is complete
    public void TutorialComplete()
    {
        setNextTutorial(currentTutorial.Order + 1); // +1 so that it moves on to the next tutorial
    }
    public void setNextTutorial(int currentOrder)
    {
        currentTutorial = getTutorialByOrder(currentOrder);

        if(!currentTutorial)
        {
            //message (or call function that displays message) that says "you have finished all the tutorials"
            TutorialModeFinish();
            return;
        }

        tutText.text = currentTutorial.Explanation;
    }
    public void TutorialModeFinish()
    {
        tutText.text = "You will now head back to the main menu in a few seconds";

        //This is so the the congratulating message can remain on screen instead of going to main menu suddenly
        Invoke("BackToMainMenu", 5.0f);

    }

    public void BackToMainMenu()
    {
        //Once the tutorial is finished head back to the main menu
       
        SceneManager.LoadScene("MainMenuScene");
    }
    public tutorialMode getTutorialByOrder(int order)
    {
        for (int i = 0; i < Tutorials.Count; i++)
        {
            if (Tutorials[i].Order == order)
            {
                return Tutorials[i];
            }
        }
        return null;
    }
}
