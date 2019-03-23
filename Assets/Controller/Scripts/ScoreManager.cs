using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;

    public float scoreCount;
    public int pointsPerSecond;

    public bool scoreIncreasing;

    // Start is called before the first frame update
    void Start()
    {
        scoreCount = 0;
        scoreText.text = "Score: " + (Mathf.Round(scoreCount * 100f) / 100f);
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreIncreasing)
        {
            scoreCount += pointsPerSecond * Time.deltaTime;
        }
        else
        {
            PlayerPrefs.SetFloat("score", scoreCount);
        }

        scoreText.text = "Score: " + (Mathf.Round(scoreCount * 100f) / 100f);
    }

    public void Addscore(int PointsToAdd)
    {
        scoreCount += PointsToAdd;
    }
}
