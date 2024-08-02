using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject endPlatform;
    [SerializeField] float acceleration = 8;
    [SerializeField] int losingScore = 10;
    int score = 0;
    bool gameWon = false;
    bool gameLost = false;

    Vector3 originalPos;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello world!");
        endPlatform.GetComponent<MeshRenderer>().material.color = Color.red;
        originalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Floor" && 
            collision.gameObject.tag != "BackWall" &&
            collision.gameObject.tag != "EndPlatform" &&
            !gameWon && !gameLost
        ) 
        {
            score++;
            Debug.Log("You have bumped into something " + score + " times");

            if (score >= losingScore) 
            {
                Debug.Log("You have lost");
                gameLost = true;
                // Application.Quit();
            }
            else 
            {
                transform.position = originalPos;
            }
        }

        else if (collision.gameObject.tag == "EndPlatform" && !gameWon && !gameLost)
        {
            Debug.Log("You win!");
            gameWon = true;
            endPlatform.GetComponent<MeshRenderer>().material.color = Color.green;
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
