using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour
{

    public GameObject pauseMenuUI;
 

    public void mainMenu()
    {
        //Change later
        SceneManager.LoadScene("StartScreen2"); 
    }

    public void quitGame()
    {
        Debug.Log("Quiting...");
        Application.Quit();
    }
}
