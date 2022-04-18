using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuManagement : MonoBehaviour
{
    [Header("Menu Buttons")]
    public Button returnButton;
    public Button mainMenuButton;
    public Button quitButton;

    private Vector3 largeCursor;
    private Vector3 smallCursor;
    private int currentCursorPosition;

    void Start()
    {
        returnButton.GetComponent<Image>().color = Color.red;
        mainMenuButton.GetComponent<Image>().color = Color.white;
        quitButton.GetComponent<Image>().color = Color.white;

        largeCursor = new Vector3(1.1f, 1.1f, 1.1f);
        smallCursor = new Vector3(1f, 1f, 1f);
        currentCursorPosition = 1;

        returnButton.transform.localScale = largeCursor;
        mainMenuButton.transform.localScale = smallCursor;
        quitButton.transform.localScale = smallCursor;
    }

    public void HandleDirectionInput(int direction)
    {
        // 1 up
        // 3 down
        if(direction == 1)
        {
            if(currentCursorPosition == 1)
            {
                returnButton.GetComponent<Image>().color = Color.white;
                returnButton.transform.localScale = smallCursor;

                quitButton.GetComponent<Image>().color = Color.red;
                quitButton.transform.localScale = largeCursor;

                currentCursorPosition = 3;
            }
            else if(currentCursorPosition == 2)
            {
                mainMenuButton.GetComponent<Image>().color = Color.white;
                mainMenuButton.transform.localScale = smallCursor;

                returnButton.GetComponent<Image>().color = Color.red;
                returnButton.transform.localScale = largeCursor;

                currentCursorPosition = 1;
            }
            else if(currentCursorPosition == 3)
            {
                quitButton.GetComponent<Image>().color = Color.white;
                quitButton.transform.localScale = smallCursor;

                mainMenuButton.GetComponent<Image>().color = Color.red;
                mainMenuButton.transform.localScale = largeCursor;

                currentCursorPosition = 2;
            }
        }
        else if(direction == 3)
        {
            if(currentCursorPosition == 1)
            {
                returnButton.GetComponent<Image>().color = Color.white;
                returnButton.transform.localScale = smallCursor;

                mainMenuButton.GetComponent<Image>().color = Color.red;
                mainMenuButton.transform.localScale = largeCursor;

                currentCursorPosition = 2;
            }
            else if(currentCursorPosition == 2)
            {
                quitButton.GetComponent<Image>().color = Color.red;
                quitButton.transform.localScale = largeCursor;

                mainMenuButton.GetComponent<Image>().color = Color.white;
                mainMenuButton.transform.localScale = smallCursor;

                currentCursorPosition = 3;
            }
            else if(currentCursorPosition == 3)
            {
                quitButton.GetComponent<Image>().color = Color.white;
                quitButton.transform.localScale = smallCursor;

                returnButton.GetComponent<Image>().color = Color.red;
                returnButton.transform.localScale = largeCursor;

                currentCursorPosition = 1;
            }
        }
    }

    public void PauseMenuSeleciton()
    {
        if(currentCursorPosition == 2)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
        else if(currentCursorPosition == 3)
        {
            QuitGame();
        }
    }

    private void QuitGame()
    {
        Debug.Log("Quiting...");
        Application.Quit();
    }
}
