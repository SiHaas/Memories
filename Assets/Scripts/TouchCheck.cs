using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures;

public class TouchCheck : MonoBehaviour { 

     public TapGesture TippyTap;
    public GameObject Cat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        //TwoFingerMoveGesture.Transformed += twoFingerTransformHandler;
        TippyTap = GetComponent<TapGesture>();
        TippyTap.Tapped += tapped;
        Debug.Log("miau");
    }

    private void OnDisable()
    {
        //TwoFingerMoveGesture.Transformed += twoFingerTransformHandler;
        TippyTap.Tapped += tapped;
    }

    private void tapped (object sender, System.EventArgs e)
    {
        Debug.Log("Tap");
        Color colorStart = Color.red;
        var catRenderer = Cat.GetComponent<Renderer>();

        catRenderer.material.color = Color.red;
    }
        // Update is called once per frame
        void Update()
    {
        
    }
}
