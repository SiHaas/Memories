﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures;

public class TouchCheck : MonoBehaviour {

    [SerializeField]
    private TapGesture TippyTap;

    [SerializeField]
    private GameObject Cat;

    [SerializeField]
    private GameObject Trigger1Text;

    [SerializeField]
    private GameObject addMemoryTrigger;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        //TwoFingerMoveGesture.Transformed += twoFingerTransformHandler;
        TippyTap = GetComponent<TapGesture>();
        TippyTap.Tapped += tapped;
        
    }

    private void OnDisable()
    {
        //TwoFingerMoveGesture.Transformed += twoFingerTransformHandler;
        TippyTap.Tapped += tapped;
    }

    private void tapped (object sender, System.EventArgs e)
    {
        
        Trigger1Text.SetActive(true);
        addMemoryTrigger.SetActive(true);
        //Color colorStart = Color.red;
        //var catRenderer = Cat.GetComponent<Renderer>();

        //catRenderer.material.color = Color.red;
    }
        // Update is called once per frame
        void Update()
    {
        
    }
}
