using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//always put this because UI in the scene


public class LifePointsCounter : MonoBehaviour
{
    // Start is called before the first frame update
    // Start is called before the first frame update
    int life;//create a new integer variable called life
    int initLifePoints = 3;//initialize this value to 5
    [SerializeField] Text lifePointsText;//print in the Unity Editor lifePointsText
    public GameObject loseTextObject;//declare the UI lose text object
    private void printLife()
    {
        if(life <= 3)//if the life score is under 3
        {
            lifePointsText.color = Color.red;//change the UI so that it becomes red
        }
        else
        {
            lifePointsText.color = Color.green;//otherwise let's keep it green
        }
        lifePointsText.text = string.Concat("Life = ",life.ToString());//print on the screen the concatenation of life and it's value

    }
    private void Start() 
    {
        life = initLifePoints;//at the beginning of the game, affect the initialize value to the life variable
        printLife();
    }
    public void addPoints(int points)//Adding points system
    {
        life = life + points;
        printLife();
    }
    public void delPoints(int points) //Deleting points system
    {
        life = life - points;
        printLife();
        if(life <= 0) //if life under or equal 0, then the game is over !
        {
            GameObject player = GameObject.Find("Player"); //and print the highscore text
            player.GetComponent<PlayerController>().youLoose(); // the player lost    
        }
    }
}
