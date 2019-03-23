using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstacles : MonoBehaviour
{
    private ScoreManager theScoreManager; //inheritance of public variables in ScoreManager.cs
    public float score;
    private void Start()
    {
        theScoreManager = FindObjectOfType<ScoreManager>();

    }

    private void Update()
    {
        score = theScoreManager.scoreCount;
    }
    //score = Mathf.Round(theScoreManager.scoreCount * 100f) / 100f;
    //scoreText.text = "Score: " + score;
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
