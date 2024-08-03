using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] bool gameOngoing = true;
    [SerializeField] string mainScene = "MainScene";
    [SerializeField] string victoryScene = "VictoryScene";

    // Update is called once per frame
    void Update()
    {
        CheckQuit();
        CheckRestart();
    }

    private void CheckQuit()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void CheckRestart()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !gameOngoing)
        {
            SceneManager.LoadScene(mainScene);
        }
    }


    public bool GameOngoing()
    {
        return gameOngoing; 
    }


    public void WinGame()
    {
        SceneManager.LoadScene(victoryScene);
    }

    public void LoseGame()
    {
        gameOngoing = false;
    }
}
