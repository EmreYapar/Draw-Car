using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMover : MonoBehaviour
{

    bool shouldMove;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ReduceSpeed());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!shouldMove)
            return;
        this.GetComponent<Rigidbody>().AddForce(Vector3.right*30);
        transform.localEulerAngles = new Vector3(0, 0, transform.localEulerAngles.z);
        transform.position = new Vector3(transform.position.x, transform.position.y, 286);
    }

    public void Stop()
    {
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    public void Levitate()
    {
        shouldMove = false;
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
        if (!collision.transform.CompareTag("Ground"))
            return;
        
            shouldMove = false;
    }

    IEnumerator ReduceSpeed()
    {
        while(!shouldMove)
        {
          //  this.GetComponent<Rigidbody>().velocity += Vector3.down*2;
            yield return null;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
            shouldMove = true;
    }
}
