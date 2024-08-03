using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public MeshRenderer endPlatform;
    public GameManager manager;
    [SerializeField] float acceleration = 8;
    [SerializeField] int losingScore = 10;
    int score = 0;

    Vector3 originalPos;

    void Start()
    {
        originalPos = transform.position;
        endPlatform.material.color = Color.red;
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
            score++;
            Debug.Log("You have bumped into something " + score + " times");

            if (score >= losingScore) 
            {
                Debug.Log("You have lost");
                manager.LoseGame();
            }
            else 
            {
                transform.position = originalPos;
            }
        }

        else if (collision.gameObject.tag == "EndPlatform" && manager.GameOngoing())
        {
            Debug.Log("You win!");
            endPlatform.material.color = Color.green;
            manager.WinGame();
        }
    }


    private void PlayerMove()
    {
        transform.Translate(
            Input.GetAxis("Horizontal") * acceleration * Time.deltaTime, 0,
            Input.GetAxis("Vertical") * acceleration * Time.deltaTime
        );
    }
}
