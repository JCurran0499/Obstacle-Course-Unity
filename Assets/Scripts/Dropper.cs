using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    [SerializeField] float wait = 3;

    public Transform corner1;
    public Transform corner2;

    MeshRenderer mesh;
    Rigidbody body;
    Vector3 originalPos;
    Quaternion originalRot;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        body = GetComponent<Rigidbody>();
        originalPos = transform.position;
        originalRot = transform.rotation;
        
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
        if (!collision.gameObject.tag.Equals("Player"))
        {
            body.velocity = Vector3.zero;
            body.angularVelocity = Vector3.zero;
            transform.position = randomCoord();
            transform.rotation = originalRot;
        }
    }


    // Helper Methods \\

    ///<summary>
    ///Generates a random position for the dropper within the arena.
    ///</summary>
    private Vector3 randomCoord()
    {
        float xRange = corner2.position.x - corner1.position.x;
        float zRange = corner2.position.z - corner1.position.z;

        float newX = (Random.value * xRange) + corner1.position.x;
        float newZ = (Random.value * zRange) + corner1.position.z;
        return new Vector3(newX, originalPos.y, newZ);
    }
}
