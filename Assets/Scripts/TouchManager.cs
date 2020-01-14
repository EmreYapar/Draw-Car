using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
        void OnEnable()
        {
            Lean.Touch.LeanTouch.OnFingerTap += HandleFingerTap;
        Lean.Touch.LeanTouch.OnGesture += LeanTouch_OnGesture;
        Lean.Touch.LeanTouch.OnFingerDown += LeanTouch_OnFingerDown;
        Lean.Touch.LeanTouch.OnFingerUp += LeanTouch_OnFingerUp;
        Lean.Touch.LeanTouch.OnFingerSet += LeanTouch_OnFingerSet;
        }

    [SerializeField]
    DrawLine drawLine;

    private void LeanTouch_OnFingerSet(Lean.Touch.LeanFinger obj)
    {
        if (!obj.IsOverGui)
            return;
        drawLine.Draw(obj.GetWorldPosition(10));
    }

    bool isTouching;

    private void LeanTouch_OnFingerUp(Lean.Touch.LeanFinger obj)
    {
        
        drawLine.OnFingerUp();
        isTouching = false;
    }

    private void LeanTouch_OnFingerDown(Lean.Touch.LeanFinger obj)
    {
        if (!obj.IsOverGui)
            return;
        drawLine.OnFingerDown(obj.GetWorldPosition(10));
        isTouching = true;
    }

    private void LeanTouch_OnGesture(List<Lean.Touch.LeanFinger> obj)
    {
        
    }

    void OnDisable()
        {
            Lean.Touch.LeanTouch.OnFingerTap -= HandleFingerTap;
        }

        void HandleFingerTap(Lean.Touch.LeanFinger finger)
        {
            Debug.Log("You just tapped the screen with finger " + finger.Index + " at " + finger.ScreenPosition);
        }
}
