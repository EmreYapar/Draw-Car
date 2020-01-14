using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMover : MonoBehaviour
{

    bool isLevitating;
    Rigidbody rb;

    Vector3 localEuler;
    Vector3 position;

    string str_ground = "Ground";

    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isLevitating)
            return;

        if (isGrounded)
            rb.AddForce(Vector3.right * 30);
        else
        {
            rb.AddForce((Vector3.down + Vector3.left) * 20);
        }
            localEuler.z = transform.localEulerAngles.z;
        transform.localEulerAngles = localEuler;

        position = transform.position;
        // cube constant z;
        position.z = 286;
        transform.position = position;
    }

    public void Stop()
    {
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    public void Levitate()
    {
        isLevitating = true;
        Stop();
        this.GetComponent<Rigidbody>().isKinematic = true;
        this.transform.position += Vector3.up*6;
    }

    public void StopLevitate()
    {
        this.GetComponent<Rigidbody>().isKinematic = false;
    }

    
    private void OnCollisionExit(Collision collision)
    {
        if (!collision.transform.CompareTag(str_ground))
            return;

        isGrounded = false;

    }

    private void OnCollisionStay(Collision collision)
    {
        if (!collision.transform.CompareTag(str_ground))
            return;
            isGrounded = true;
        isLevitating = false;
    }

    string tag_checkpoint = "Checkpoint";
    string tag_death = "Death";



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tag_death))
            EventManager.FallDown(this.transform);
        else if(other.CompareTag(tag_checkpoint))
            EventManager.CheckpointPassed();
    }
}
