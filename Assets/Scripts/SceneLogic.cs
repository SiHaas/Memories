using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneLogic : MonoBehaviour
{
    public static UnityAction StartGameAction { get; set; }
    // Start is called before the first frame update

    private void Awake()
    {
        StartGameAction += StartGameFunction;
      
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

}
