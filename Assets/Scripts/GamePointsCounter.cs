using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//always put this because UI in the scene

public class GamePointsCounter : MonoBehaviour
{
    // Start is called before the first frame update
    // Start is called before the first frame update
    int score;//create a new integer variable named score
    [SerializeField] Text gamePointsText;//print in the unity editor the gamePointsText

    private void Start() 
    {
        score = 0;//initialize this value to 0 at the beginning of the game
        gamePointsText.text = "Score = 0";//print SCORE
    }
    // Update is called once per frame
    public void addPoints(int points)//then add the function add point to this variable
    {
        score = score + points;
        gamePointsText.text = string.Concat("Score = ",score.ToString());//and print the result as a concatenation of the score and the value to print
    }
     public int getScore()//print the score
    {
        return(score);//then return to score
    }
}
