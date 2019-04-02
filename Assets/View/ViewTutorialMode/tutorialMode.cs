using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialMode : MonoBehaviour
{
    public int Order; //Used for the program to determine which tutorial message to display

   [TextArea(3,10)] // creating a text space to write the explanations for the tutorial
    public string Explanation; //Used for explaining the moments in the tutorial

    public void Awake()
    {
        tutorialManager.Instance.Tutorials.Add(this);
    }

    //Checking the progress of the player through the tutorial
    public virtual void checkIfHappening() { }
}
