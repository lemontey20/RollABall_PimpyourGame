using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//always put this when calling for new scences
using UnityEngine.Audio;//always put this when calling for audio files

public class Mainmenu : MonoBehaviour
{
    public void PlayGame() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);//load the active scene and the next one
    }

    public void QuitGame()
    { 
        Debug.Log("QUIT!");//print quit after pressing the quit button
        Application.Quit();  
    }

    public AudioMixer AudioMixer;//using the audiomixer and liking it to the volume management setting
    public void SetVolume(float volume)// the volume is a float value
    {
        AudioMixer.SetFloat("volume", volume);
    }
}

