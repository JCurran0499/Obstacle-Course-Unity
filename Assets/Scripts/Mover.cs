using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] float acceleration = 8;
    int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello world!");
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Floor")
        {
            score++;
            Debug.Log("You have bumped into something " + score + " times");
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
