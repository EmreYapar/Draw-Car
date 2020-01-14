using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{



    List<CheckPoint> checkPoints = new List<CheckPoint>();

    int currentCheckpoint;
    int lastCheckpointNumber;

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
        int number = 0; ;
       foreach(var t in GetComponentsInChildren<Transform>())
        {
            checkPoints.Add(new CheckPoint(t, number++));
        }
    }

    void CheckpointPassed()
    {
        if (checkPoints[currentCheckpoint].number == lastCheckpointNumber)
            return;

        lastCheckpointNumber = checkPoints[currentCheckpoint].number;
        checkPoints[currentCheckpoint].isPassed = true;
        currentCheckpoint++;
    }

    void FallDown(Transform car)
    {
        car.transform.position = checkPoints[currentCheckpoint].transform.position;
    }

    class CheckPoint
    {
       public bool isPassed;
       public Transform transform;
       public int number;

        public CheckPoint(Transform transform, int number)
        {
            this.transform = transform;
            this.number = number;
        }

        
    }


}
