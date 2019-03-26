using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstacles : MonoBehaviour
{
    private ScoreManager theScoreManager; //inheritance of public variables in ScoreManager.cs
    public float score;
    public void Start()
    {
        theScoreManager = FindObjectOfType<ScoreManager>();

    }
    public void Update()
    {
        score = theScoreManager.scoreCount;
    }

    void OnTriggerEnter(Collider col)
    {

        //if the player hits one obstacle, it's game over
        if (col.gameObject.tag == Constants.PlayerTag)
        {
            theScoreManager.scoreIncreasing = false;
            if(PlayerPrefs.HasKey("score"))
            {
                score = PlayerPrefs.GetFloat("score");
            }

            GameManager.Instance.Die();
            //Destroy(col.gameObject); // look into this
            if (GameManager.Instance.GameState == GameState.Dead)
            {
                SceneManager.LoadScene("SubmitScene");
            }
        }
    }
}
