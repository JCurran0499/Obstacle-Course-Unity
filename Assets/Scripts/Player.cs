using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameManager manager;
    [SerializeField] float acceleration = 8;

    bool frozen = false;

    Vector3 originalPos;

    void Start()
    {
        originalPos = transform.position;
    }

    void Update()
    {
        PlayerMove();   
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Floor" && 
            collision.gameObject.tag != "BackWall" &&
            collision.gameObject.tag != "EndPlatform" &&
            manager.GameOngoing()
        ) 
        {
            manager.TakeHit();

            if (manager.LivesLeft() <= 0) 
            {
                Debug.Log("You have lost");
                manager.LoseGame();
            }
            else 
            {
                frozen = true;
                transform.position = originalPos;
            }
        }

        else if (collision.gameObject.tag == "EndPlatform" && manager.GameOngoing())
        {
            Debug.Log("You win!");
            manager.WinGame();
        }
    }


    private void PlayerMove()
    {
        if (!frozen)
        {
            transform.Translate(
                Input.GetAxis("Horizontal") * acceleration * Time.deltaTime, 0,
                Input.GetAxis("Vertical") * acceleration * Time.deltaTime
            );
        }
        else
        {
            if (
                Input.GetKeyDown(KeyCode.W) ||
                Input.GetKeyDown(KeyCode.S) ||
                Input.GetKeyDown(KeyCode.D) ||
                Input.GetKeyDown(KeyCode.A)
            )
            {
                frozen = false;
            }
        }
    }
}
