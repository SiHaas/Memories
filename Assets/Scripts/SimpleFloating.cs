using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFloating : MonoBehaviour
{

    public bool animPos = true;
    public Vector3 posAmplitude = Vector3.one * 2f;
    public Vector3 posSpeed = Vector3.one * 0.2f;

    public bool animRot = true;
    public Vector3 rotAmplitude = Vector3.one * 10;
    public Vector3 rotSpeed = Vector3.one * 0.13f;

    public bool animScale = false;
    public Vector3 scaleAmplitude = Vector3.one * 0.1f;
    public Vector3 scaleSpeed = Vector3.one;

    private Vector3 origPos;
    private Vector3 origRot;
    private Vector3 origScale;

    private float startAnimOffset = 0;


    /**
     * Awake
     */
    void Awake()
    {
        origPos = transform.position;
        origRot = transform.eulerAngles;
        origScale = transform.localScale;
        startAnimOffset = Random.Range(0f, 540f);        // so that the xyz anims are already offset from each other since the start
    }

    /**
     * Update
     */
    void Update()
    {
        /* position */
        if (animPos)
        {
            Vector3 pos;
            pos.x = origPos.x + posAmplitude.x * Mathf.Sin(posSpeed.x * Time.time + startAnimOffset);
            pos.y = origPos.y;
            pos.z = origPos.z;
            transform.position = pos;
        }

        /* rotation */
        if (animRot)
        {
            Vector3 rot;
            rot.x = origRot.x + rotAmplitude.x * Mathf.Sin(rotSpeed.x * Time.time + startAnimOffset);
            rot.y = origRot.y + rotAmplitude.y * Mathf.Sin(rotSpeed.y * Time.time + startAnimOffset);
            rot.z = origRot.z + rotAmplitude.z * Mathf.Sin(rotSpeed.z * Time.time + startAnimOffset);
            transform.eulerAngles = rot;
        }

        /* scale */
        if (animScale)
        {
            Vector3 scale;
            scale.x = origScale.x * (1 + scaleAmplitude.x * Mathf.Sin(scaleSpeed.x * Time.time + startAnimOffset));
            scale.y = origScale.y * (1 + scaleAmplitude.y * Mathf.Sin(scaleSpeed.y * Time.time + startAnimOffset));
            scale.z = origScale.z * (1 + scaleAmplitude.z * Mathf.Sin(scaleSpeed.z * Time.time + startAnimOffset));
            transform.localScale = scale;
        }
    }
}