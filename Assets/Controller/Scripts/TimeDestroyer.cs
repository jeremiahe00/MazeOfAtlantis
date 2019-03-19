using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TimeDestroyer : MonoBehaviour
{
    private float rate = 1.0f;
    
    void Start()
    {
        if (rate > 0.25)
        {
            rate -= 0.075f;
        }
        Invoke("DestroyObject", LifeTime*rate);
    }

    void DestroyObject()
    {
        if (GameManager.Instance.GameState != GameState.Dead)
            Destroy(gameObject);
    }

    
    private float LifeTime = 10f;
    
}
