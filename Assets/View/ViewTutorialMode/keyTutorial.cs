using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyTutorial : tutorialMode
{
    public List<string> keys = new List<string>();
    public override void checkIfHappening()
    {
        for (int i = 0; i < keys.Count; i++)
        {
            if(Input.inputString.Contains(keys[i]))
            {
                keys.RemoveAt(i);
                break;
            }
        }
        if (keys.Count == 0)
            tutorialManager.Instance.TutorialComplete();
    }

}
