using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookToCamera : MonoBehaviour
{
       
    void FixedUpdate()
    {
        transform.LookAt(Camera.main.transform.position);        
    }
}
