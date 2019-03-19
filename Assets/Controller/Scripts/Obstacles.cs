using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstacles : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        //if the player hits one obstacle, it's game over
        if (col.gameObject.tag == Constants.PlayerTag)
        {
            GameManager.Instance.Die();
            //Destroy(col.gameObject); // look into this
            if (GameManager.Instance.GameState == GameState.Dead)
            {
                SceneManager.LoadScene("GameOverScene");
            }
        }
    }
}
