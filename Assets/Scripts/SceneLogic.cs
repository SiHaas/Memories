using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneLogic : MonoBehaviour
{
    public static UnityAction StartGameAction { get; set; }
    public static UnityAction <int> TestEnvironmentSpawnAction { get; set; }
    // Start is called before the first frame update
    public GameObject testEnvironment;
    private void Awake()
    {
        StartGameAction += StartGameFunction;
        TestEnvironmentSpawnAction += TestEnvironmentSpawnFuction;


        SceneLogic.StartGameAction.Invoke();

 
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void StartGameFunction()
    {
        Debug.Log("StartGameAction received");
    }

    private void TestEnvironmentSpawnFuction(int z)
    {
        
        //Instantiate(testEnvironment, new Vector3(0, 0, z), Quaternion.identity);
        Debug.Log("spawn triggered");
    }

}
