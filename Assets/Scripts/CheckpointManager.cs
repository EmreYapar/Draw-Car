using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{



    List<CheckPoint> checkPoints = new List<CheckPoint>();

    int currentCheckpoint;

    private void OnEnable()
    {
        EventManager.checkpointPassed += CheckpointPassed;
        EventManager.fallDown += FallDown;
    }

    private void OnDisable()
    {
        EventManager.checkpointPassed -= CheckpointPassed;
        EventManager.fallDown -= FallDown;
    }

    private void Awake()
    {
       foreach(var t in GetComponentsInChildren<Transform>())
        {
            checkPoints.Add(new CheckPoint(t));
        }
    }

    void CheckpointPassed()
    {
        if (checkPoints[currentCheckpoint + 1].isPassed)
            return;


        checkPoints[currentCheckpoint++].isPassed = true;
    }

    void FallDown(Transform car)
    {
        car.transform.position = checkPoints[currentCheckpoint].transform.position;
    }

    class CheckPoint
    {
        public bool isPassed;
       public Transform transform;

        public CheckPoint(Transform transform)
        {
            this.transform = transform;
        }

        
    }


}
