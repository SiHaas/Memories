using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnvironment : MonoBehaviour
{
    // Start is called before the first frame update
    private bool touchedTestEnvironment = false;
    int zPosition = -31;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collision triggered");
        if (other.tag == "SpawnTestEnvironment")
        {
            Debug.Log("tag recognized");
        
            
                SceneLogic.TestEnvironmentSpawnAction.Invoke(zPosition);
            zPosition = zPosition - 10;
                Debug.Log("action invoked");
              

            }
        }
    }

