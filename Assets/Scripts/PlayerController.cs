using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_speed = 1f; //speed modifier
    
    private Rigidbody m_playerRigidbody = null; //reference to the players rigidbody

    private float m_movementX, m_movementY; //input vector components

    private int m_collectablesTotalCount, m_collectablesCounter; //everything we need to count the given collectables

    private Stopwatch m_stopwatch; //takes the time

    private int pointsCollectable1 = 10;//initialization of the type 1 collectable to 10 points
    private int pointsCollectable2 = 20;//initialization of the type 2 collectable to 20 points
    private int pointsCollectable3 = 30;//initialization of the type 3 collectable to 30 points
    private int lifeCollectable2 = 1;//initialization of the type 2 collectable to 1 life point
    private int lifeCollectable3 = 2;//initialization of the type 2 collectable to 2 life points
    private int lifeEnemy = 1;//initialization of the life point value if crossing an enemy

    public GameObject winTextObject;//declare the UI win text object
    public GameObject loseTextObject;//declare the UI lose text object
    private void Start()
    {
        m_playerRigidbody = GetComponent<Rigidbody>(); //get the rigidbody component

        m_collectablesTotalCount = m_collectablesCounter = GameObject.FindGameObjectsWithTag("Collectable").Length; //find all gameobjects in the scene which are tagged with "Collectable" and count them via Length property 
        
        m_stopwatch = Stopwatch.StartNew(); //start the stopwatch
        
        winTextObject.SetActive(false);//avoid the printing the winning UI at the start of the game
        loseTextObject.SetActive(false);//avoid printing the losing UI at the start of the game
    }

    public void youLoose()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");//find every object with the tag enemy
        foreach(GameObject enemy in enemies)  //for each enemy object in the ennemy class
        {
            enemy.SetActive(false);//change the enemy state so that they become chased
        }        
        loseTextObject.SetActive(true);//print the UI you lose on the screen        
    }
    
    public void youWin()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");//find every object with the tag enemy
        foreach(GameObject enemy in enemies)  //for each enemy object in the ennemy class
        {
            enemy.SetActive(false);//change the enemy state so that they become chased
        }        
        winTextObject.SetActive(true);//print the UI you lose on the screen        
    }

    public void endGame()
    {
        GameObject highScore = GameObject.Find("HighScoreText"); //and print the highscore text
        highScore.GetComponent<HighScore>().saveScore(); //save the actual score if it's superior to the previous high score
#if UNITY_EDITOR //the following code is only included in the unity editor
        UnityEditor.EditorApplication.ExitPlaymode();//exits the playmode
#endif
    }
    
    private void OnMove(InputValue inputValue)
    {
        Vector2 movementVector = inputValue.Get<Vector2>(); //get the input

        //split input vector in its two components
        m_movementX = movementVector.x;
        m_movementY = movementVector.y;
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(m_movementX, 0f, m_movementY); //translate the 2d vector into a 3d vector
        
        m_playerRigidbody.AddForce(movement * m_speed); //apply a force to the rigidbody
    }

    private void OnTriggerEnter(Collider other)//executed when the player hits another collider (which is set to 'is trigger')
    {
        if (other.gameObject.CompareTag("Collectable"))//has the other gameobject the tag "Collectable"
        {
            other.gameObject.SetActive(false); //set the hit collectable inactive

            m_collectablesCounter--; //count down the remaining collectables
            if (m_collectablesCounter == 0) //have we found all collectables? if so we won!
            {
                youWin();//print the UI you won on the screen
            }
            else
            {
                UnityEngine.Debug.Log($"You've already found {m_collectablesTotalCount - m_collectablesCounter} of {m_collectablesTotalCount} collectables!");

                int m_collectableType = other.GetComponent<Collectable>().GetCollectableType();//take the collectable type
                UnityEngine.Debug.Log("type collectable = ");
                UnityEngine.Debug.Log(m_collectableType);

                if( m_collectableType == 1)//if it's a type 1 collectable
                {
                    GameObject counter = GameObject.Find("GamePointsText");//find the game point text in the scene and affect it to the game counter
                    counter.GetComponent<GamePointsCounter>().addPoints(pointsCollectable1);    //take the actual value and add int point depending of the values affected to the type 1 collectable
                    GameObject audioSource = GameObject.Find("audioCollectable1"); //find the audio associated
                    AudioSource source = audioSource.gameObject.GetComponent<AudioSource>();
                    source.Play();                }      //play the sound associated to the type 1 collectable          
                else if( m_collectableType == 2)//if it's a type 2 collectable; same principle
                {
                    GameObject counter = GameObject.Find("GamePointsText");
                    counter.GetComponent<GamePointsCounter>().addPoints(pointsCollectable2);                     
                    counter = GameObject.Find("LifePointsText");
                    counter.GetComponent<LifePointsCounter>().addPoints(lifeCollectable2); 
                    GameObject audioSource = GameObject.Find("audioCollectable2");
                    AudioSource source = audioSource.gameObject.GetComponent<AudioSource>();
                    source.Play();    
                }
                else if( m_collectableType == 3)//if it's a type 3 collectable; same principle
                {
                    GameObject timer = GameObject.Find("TimerText");
                    timer.GetComponent<Timer>().startTimer();
                    GameObject counter = GameObject.Find("GamePointsText");
                    counter.GetComponent<GamePointsCounter>().addPoints(pointsCollectable3);                     
                    counter = GameObject.Find("LifePointsText");
                    counter.GetComponent<LifePointsCounter>().addPoints(lifeCollectable3); 
                    GameObject audioSource = GameObject.Find("audioCollectable3");
                    AudioSource source = audioSource.gameObject.GetComponent<AudioSource>();
                    source.Play();
                    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");//find every object with the tag enemy
                    foreach(GameObject enemy in enemies)  //for each enemy object in the ennemy class
                    {
                        enemy.GetComponent<EnemyController>().ChangeState("chased");//change the enemy state so that they become chased
                    }
                }

            }
        }
        else if (other.gameObject.CompareTag("Enemy")) //has the other gameobject the tag "Enemy" 
        {
            GameObject audioSource = GameObject.Find("audioEnemy");//associate the enemy sound
            AudioSource source = audioSource.gameObject.GetComponent<AudioSource>();
            source.Play();    // and play it
            if(other.gameObject.GetComponent<EnemyController>().GetState() == "chased") //if the enemy is chased
            {
                other.gameObject.SetActive(false);//you can make it disappear (as a collectable)
            }
            else
            {
                GameObject counter = GameObject.Find("LifePointsText"); //otherwise you are the chased one
                counter.GetComponent<LifePointsCounter>().delPoints(lifeEnemy); //then set the life counter so that it deletes your life when crossing an enemy
            }
        }
    }
}
