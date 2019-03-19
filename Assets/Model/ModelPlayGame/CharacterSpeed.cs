using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpeed : MonoBehaviour
{

    protected static float SideWaysSpeed = 1.1f;

    protected static float PDSpeed = 1.1f;
    protected static float Speed = 5.0f;
    protected static float forward = 1.0f;

    // Update is called once per frame
    protected void SpeedUpdate()
    {
        if (forward < 5.0f)
        {
            forward += 0.00020f;
        }
    }
}
