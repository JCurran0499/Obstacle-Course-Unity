using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHit : MonoBehaviour
{
    [SerializeField] float flashRate = 0.5f;
    bool isRed = false;
    float timeRed = 0;

    Color originalColor;

    void Start()
    {
        originalColor = GetComponent<MeshRenderer>().material.color;
    }

    void Update()
    {
        if (isRed && (Time.time > timeRed + flashRate)) {
            isRed = false;
            GetComponent<MeshRenderer>().material.color = originalColor;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && enabled) {
            GetComponent<MeshRenderer>().material.color = Color.red;
            timeRed = Time.time;
            isRed = true;
        }
    }
}
