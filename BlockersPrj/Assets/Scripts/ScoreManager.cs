using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public Text ScoreText;
    public Text HighScoreText;

    public float scoreCount;
    public float HighScoreCount;

    public float pointPerSecond;
    public bool scoreIncreasing;

    public bool PowerupDouble;


    // Start is called before the first frame update
    void Start()
    {
        HighScoreCount = Properties.getInstance().getHighestScore();

    }

    // Update is called once per frame
    void Update()
    {
        
        if (scoreIncreasing)
        {
            scoreCount += pointPerSecond * Time.deltaTime;
        }


        if (scoreCount > HighScoreCount)
        {
            HighScoreCount = scoreCount;
        }

        ScoreText.text = "Score: " + Mathf.Round(scoreCount) ;
        HighScoreText.text = "HighScore: " + Mathf.Round(HighScoreCount);

    }

    public void AddScore(int pointToAdd)
    {
        if (PowerupDouble)
        {
            scoreCount += 2 * pointToAdd;
        }
        else
        {
            scoreCount += pointToAdd;
        }
    }


}
