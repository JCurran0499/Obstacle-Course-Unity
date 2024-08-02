using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Dropper : MonoBehaviour
{
    [SerializeField] float wait = 3;

    MeshRenderer mesh;
    Rigidbody body;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        body = GetComponent<Rigidbody>();
        
        mesh.enabled = false;
        body.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > wait)
        {
            mesh.enabled = true;
            body.useGravity = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Floor")
        {
            body.constraints = RigidbodyConstraints.FreezePositionY;
        }
    }
}
