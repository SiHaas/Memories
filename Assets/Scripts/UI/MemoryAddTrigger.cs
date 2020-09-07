using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures;

public class MemoryAddTrigger : MonoBehaviour
{
    [SerializeField]
    private TapGesture Touched;

    private void OnEnable()
    {
        //TwoFingerMoveGesture.Transformed += twoFingerTransformHandler;
        Touched = GetComponent<TapGesture>();
        Touched.Tapped += touched;
        
    }
    private void OnMouseDown()
    {
        Debug.Log("Test");
        UiManager.AddMemoryItemAction.Invoke();
    }
    private void touched(object sender, System.EventArgs e)
    {
        UiManager.AddMemoryItemAction.Invoke();
    }
}

