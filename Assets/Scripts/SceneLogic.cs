using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneLogic : MonoBehaviour
{
    public static UnityAction StartGameAction { get; set; }
    public static UnityAction VG0 { get; set; }
    public static UnityAction VG1 { get; set; }
    public static UnityAction VB0 { get; set; }
    public static UnityAction VB1 { get; set; }
    public static UnityAction VF0 { get; set; }
    public static UnityAction VF1 { get; set; }
    public static UnityAction VC0 { get; set; }
    public static UnityAction VC1 { get; set; }
    public static UnityAction VS0 { get; set; }
    public static UnityAction VS1 { get; set; }
    public static UnityAction EG0 { get; set; }
    public static UnityAction EG1 { get; set; }
    public static UnityAction EN0 { get; set; }
    public static UnityAction EN1 { get; set; }
    public static UnityAction EC0 { get; set; }
    public static UnityAction EC1 { get; set; }
    public static UnityAction EP0 { get; set; }
    public static UnityAction EP1 { get; set; }
    public static UnityAction <int> TestEnvironmentSpawnAction { get; set; }
    // Start is called before the first frame update
    public GameObject testEnvironment;
    private void Awake()
    {
        StartGameAction += StartGameFunction;
        TestEnvironmentSpawnAction += TestEnvironmentSpawnFuction;

        VG0 += VG0Function;
        VG1 += VG1Function;
        VB0 += VB0Function;
        VB1 += VB1Function;
        VF0 += VF0Function;
        VF1 += VF1Function;
        VC0 += VC0Function;
        VC1 += VC1Function;
        VS0 += VS0Function;
        VS1 += VS1Function;
        EG0 += EG0Function;
        EG1 += EG1Function;
        EN0 += EN0Function;
        EN1 += EN1Function;
        EC0 += EC0Function;
        EC1 += EC1Function;
        EP0 += EP0Function;
        EP1 += EP1Function;




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

    private void VG0Function()
    {
        Debug.Log("StartGameAction received");
    }
    private void VG1Function()
    {
        Debug.Log("StartGameAction received");
    }
    private void VB0Function()
    {
        Debug.Log("StartGameAction received");
    }
    private void VB1Function()
    {
        Debug.Log("StartGameAction received");
    }
    private void VF0Function()
    {
        Debug.Log("StartGameAction received");
    }
    private void VF1Function()
    {
        Debug.Log("StartGameAction received");
    }
    private void VC0Function()
    {
        Debug.Log("StartGameAction received");
    }
    private void VC1Function()
    {
        Debug.Log("StartGameAction received");
    }
    private void VS0Function()
    {
        Debug.Log("StartGameAction received");
    }
    private void VS1Function()
    {
        Debug.Log("StartGameAction received");
    }
    private void EG0Function()
    {
        Debug.Log("StartGameAction received");
    }
    private void EG1Function()
    {
        Debug.Log("StartGameAction received");
    }
    private void EN0Function()
    {
        Debug.Log("StartGameAction received");
    }
    private void EN1Function()
    {
        Debug.Log("StartGameAction received");
    }
    private void EC0Function()
    {
        Debug.Log("StartGameAction received");
    }
    private void EC1Function()
    {
        Debug.Log("StartGameAction received");
    }
    private void EP0Function()
    {
        Debug.Log("StartGameAction received");
    }
    private void EP1Function()
    {
        Debug.Log("StartGameAction received");
    }

}
