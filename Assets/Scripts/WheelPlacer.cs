using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelPlacer : MonoBehaviour
{
    [SerializeField]
    GameObject wheel;

    GameObject w1;
    GameObject w2;

    private void OnEnable()
    {
        EventManager.placeWheel += PlaceWheels;
    }

    private void OnDisable()
    {
        EventManager.placeWheel -= PlaceWheels;
    }

    void PlaceWheels(Vector3 front, Vector3 back,GameObject car)
    {
        //car.transform.localScale = new Vector3(1f, 1f, 1f);
        Destroy(w1);
        Destroy(w2);
        w1 = Instantiate(wheel);
        w1.transform.position = front + car.transform.position;
        w1.transform.eulerAngles = new Vector3(car.transform.eulerAngles.x,0,car.transform.eulerAngles.z) ;
        w1.transform.eulerAngles += new Vector3(90, 0, 0);
        w1.transform.parent = car.transform;
        w1.transform.localPosition = new Vector3(w1.transform.localPosition.x, w1.transform.localPosition.y-1, 0);
        w2 = Instantiate(wheel);
        w2.transform.position = back + car.transform.position;
        w2.transform.rotation = car.transform.rotation;
        w2.transform.eulerAngles += new Vector3(90, 0, 0);
        w2.transform.parent = car.transform;
        w2.transform.localPosition = new Vector3(w2.transform.localPosition.x, w2.transform.localPosition.y-1, 0);
        
        // car.transform.localScale = new Vector3(1f,1f,1f);
    }
}
