using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public MeshRenderer endPlatform;
    public Canvas canvas;
    [SerializeField] readonly int life = 5;
    int livesLeft;

    bool gameOngoing = true;
    ObjectHit[] obstacles;
    RawImage[] hearts;

    void Start()
    {
        endPlatform.material.color = Color.red;
        obstacles = FindObjectsOfType<ObjectHit>();
        hearts = canvas.GetComponentsInChildren<RawImage>();
        livesLeft = life;
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

    public void TakeHit()
    {
        livesLeft--;
        hearts[livesLeft].enabled = false;
        Debug.Log("You bumped into something. " + livesLeft + " lives left");
    }


    public bool GameOngoing()
    {
        return gameOngoing; 
    }

    public int LivesLeft()
    {
        return livesLeft; 
    }



    public void WinGame()
    {
        gameOngoing = false;
        endPlatform.material.color = Color.green;

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

        while (livesLeft > 0)
        {
            livesLeft--;
            hearts[livesLeft].enabled = false;
        }
    }

    public void LoseGame()
    {
        gameOngoing = false;
    }
}
