using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameOngoing = true;
    ObjectHit[] obstacles;

    void Start()
    {
        obstacles = FindObjectsOfType<ObjectHit>();
    }

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
        if (Input.GetKeyDown(KeyCode.Return) && !gameOngoing)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }


    public bool GameOngoing()
    {
        return gameOngoing; 
    }


    public void WinGame()
    {
        gameOngoing = false;
        foreach (ObjectHit obstacle in obstacles)
        {
            if (
                obstacle.gameObject.tag.Equals("Dropper") ||
                obstacle.gameObject.tag.Equals("Roller")
            )
            {
                obstacle.gameObject.SetActive(false);
            }

            else
            {
                obstacle.enabled = false;
            }
        }
    }

    public void LoseGame()
    {
        gameOngoing = false;
    }
}
