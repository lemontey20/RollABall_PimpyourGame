using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//always put this when using a UI element

public class HighScore : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Text highScoreText;//print in the unity editor a Text named highScoreText
    private int highScore = 0;//initialize this variable as an integer to 0
    void Start()
    {
        highScore = PlayerPrefs.GetInt("highscore");//charge the last highscore
        highScoreText.text = string.Concat("High score = ",highScore.ToString());//print of the screen a mix of highcore and the result of last best score
    }

    // Update is called once per frame
    public void saveScore()//creation of a function named savescore
    {
        GameObject counter = GameObject.Find("GamePointsText");//create a counter that finds every time the GamePointsText is called in the unity scene
        int score  = counter.GetComponent<GamePointsCounter>().getScore();//get this object and affect the score
        if(score > highScore)//if the actual score is > to the last highscore, then it's a new record, so print it
            {
            highScoreText.text = string.Concat("New record !!!! = ",score.ToString());
            PlayerPrefs.SetInt("highscore", score);
            }
    }
}
