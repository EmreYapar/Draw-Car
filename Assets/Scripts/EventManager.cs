using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{


    public delegate void OnPlaceWheels(Vector3 frontPos, Vector3 backPos,GameObject car);
    public static OnPlaceWheels placeWheel;

    public static void PlaceWheels(Vector3 frontPos, Vector3 backPos, GameObject car)
    {
        placeWheel(frontPos,backPos,car);
    }

}
