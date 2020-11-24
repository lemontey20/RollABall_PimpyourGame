using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//always put this because UI in the scene

public class Timer : MonoBehaviour
{
    [SerializeField] Text timerText;//print in the unity editor timertext
    [SerializeField] public float timerValue;//create a new float variable
    float maxTimerValue = 11f;//set this value to 11 meter
    bool timerOn = false;  // by default, timer is inactive

    // Start is called before the first frame update
    private void Start() 
    {
        timerText.text = ""; // timer is inactive, no time to print
    }

    public void startTimer()
    {
        timerValue = maxTimerValue;
        timerOn = true;  // timer is active now
        timerText.color = Color.green;//at first the timer is green
        timerText.text = string.Concat("Chase !! ", maxTimerValue.ToString());
    }   

    public void stopTimer() // when timer stops, all enemies go back to the idle state
    {
        timerOn = false;  // timer becomes inactive
        timerText.text = "";
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy" );//look every object in the scene with the enemy tag
        foreach(GameObject enemy in enemies)  
        {
            enemy.GetComponent<EnemyController>().ChangeState("idle");//for each of them, change their state to idle state
        }
    }   

    // Update is called once per frame
    void Update()
    {
        if(timerOn == true)//if the timer is printed on the screen/is active. If not, do nothing
        {
            timerValue = timerValue - Time.deltaTime; // time between 2 frames
            int timerInt = (int)timerValue;  // transform float value into integer to be printed
            if(timerValue <= 0)
            {
                stopTimer();
            }
            else if(timerInt <= 3)//if the time is under 3 print it in red
            {
                timerText.color = Color.red;
                timerText.text = string.Concat("Chase !! ", timerInt.ToString());//print chase and the time lasting
            }
            else
            {
                timerText.color = Color.green;//if the time is upper 3 print it in green
                timerText.text = string.Concat("Chase !! ", timerInt.ToString());
            }
        }
    }
}
