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

    public delegate void OnCheckpointPassed();
    public static OnCheckpointPassed checkpointPassed;

    public static void CheckpointPassed()
    {
        checkpointPassed();
    }

    public delegate void OnFallDown(Transform car);
    public static OnFallDown fallDown;

    public static void FallDown(Transform car)
    {
        fallDown(car);
    }

}
