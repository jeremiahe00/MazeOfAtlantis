using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;

    public float scoreCount;
    public int pointsPerSecond;

    public bool scoreIncreasing;

    // Start is called before the first frame update
    private void Start()
    {
        scoreCount = 0;
        scoreText.text = "Score: " + (Mathf.Round(scoreCount * 100f) / 100f);
    }

    // Update is called once per frame
    void Update()
    {
        ScoreCapture();

        scoreText.text = "Score: " + (Mathf.Round(scoreCount * 100f) / 100f);
    }

    public void ScoreCapture()
    {
        if (scoreIncreasing)
        {
            scoreCount += pointsPerSecond * Time.deltaTime;
        }
        else
        {
            PlayerPrefs.SetFloat("scoreVariable", scoreCount);
        }
    }
}
