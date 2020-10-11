using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures;

public class StartStopBoat : MonoBehaviour
{
    [SerializeField]
    private TapGesture Touchy;
    [SerializeField]
    private UserDummy userDummy;

    private int driving = 1;

    private void OnEnable()
    {
        //TwoFingerMoveGesture.Transformed += twoFingerTransformHandler;
        Touchy = GetComponent<TapGesture>();
        Touchy.Tapped += touched;

    }
    private void OnMouseDown()
    {
        Debug.Log("Test");
        userDummy.StopMovement();
    }
    private void touched(object sender, System.EventArgs e)
    {
        if(driving == 1)
        {
            userDummy.StopMovement();
            driving = 0;
        }
        else
        {
            userDummy.StartMovement();
            driving = 1;
        }
        //UserDummy.Speed = 0f;
        
    }
}

