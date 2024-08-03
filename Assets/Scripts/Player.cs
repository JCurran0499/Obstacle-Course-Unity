using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameObject endPlatform;
    [SerializeField] float acceleration = 8;
    [SerializeField] int losingScore = 10;
    [SerializeField] bool mainGame = true;
    int score = 0;

    Vector3 originalPos;
    GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello world!");
        originalPos = transform.position;
        manager = GetComponent<GameManager>();

        if (mainGame)
        {
            endPlatform.GetComponent<MeshRenderer>().material.color = Color.red;
        }
        else {
            endPlatform.GetComponent<MeshRenderer>().material.color = Color.green;
        }
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
            GetComponent<GameManager>().WinGame();
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
