using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{

    [SerializeField]
    Transform target;

    [SerializeField]
    float smoothSpeed;

    [SerializeField]
    Vector3 offset;

    Vector3 desiredPosition;
    Vector3 smoothedPosition;

    // Update is called once per frame
    void LateUpdate()
    {
        desiredPosition = target.position + offset;
        smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed*Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
