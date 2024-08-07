using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameManager manager;
    [SerializeField] float acceleration = 8;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip success;

    bool frozen = false;

    Vector3 originalPos;
    AudioSource audioSource;

    void Start()
    {
        originalPos = transform.position;
        audioSource = GetComponent<AudioSource>();
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
            audioSource.PlayOneShot(crash, 0.1f);
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
            audioSource.PlayOneShot(success);
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
                KeyTriggered(new KeyCode[] {
                    KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D,
                    KeyCode.UpArrow, KeyCode.DownArrow,
                    KeyCode.LeftArrow, KeyCode.RightArrow
                })
            )
            {
                frozen = false;
            }
        }
    }

    private bool KeyTriggered(KeyCode[] keys)
    {
        foreach (KeyCode key in keys) { 
            if (Input.GetKeyDown(key)) 
            { 
                return true; 
            }
        }

        return false;
    }
}
