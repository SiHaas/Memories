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

    [SerializeField]
    private float BiomeExposure = 0.2f;

    [SerializeField]
    Color VGColor = new Color(0.06f, 0.6f, 1f, 1f);
    [SerializeField]
    Color VBColor = new Color(0f, 1f, 1f, 1f);
    [SerializeField]
    Color VFColor = new Color(0.7f, 0.46f, 0f, 1f);
    [SerializeField]
    Color VCColor = new Color(0.21f, 0.22f, 0.37f, 1f);
    [SerializeField]
    Color VSColor = new Color(0f, 0.17f, 0.48f, 1f);
    [SerializeField]
    Color EGColor = new Color(0.79f, 0.57f, 0.4f, 1f);
    [SerializeField]
    Color ECColor = new Color(0.43f, 0.35f, 1f, 1f);
    [SerializeField]
    Color EPColor = new Color(0.23f, 1f, 0.23f, 1f);
    [SerializeField]
    Color ENColor = new Color(0f, 0.65f, 0f, 1f);

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
        RenderSettings.skybox.SetColor("_Tint", VGColor);
        RenderSettings.skybox.SetFloat("_Exposure", BiomeExposure);
    }

    private void TestEnvironmentSpawnFuction(int z)
    {
        
        //Instantiate(testEnvironment, new Vector3(0, 0, z), Quaternion.identity);
        Debug.Log("spawn triggered");
    }

    private void VG0Function()
    {
        RenderSettings.skybox.SetColor("_Tint", VGColor);
        RenderSettings.skybox.SetFloat("_Exposure", BiomeExposure);
    }
    private void VG1Function()
    {
        RenderSettings.skybox.SetColor("_Tint", VGColor);
        RenderSettings.skybox.SetFloat("_Exposure", BiomeExposure);
    }
    private void VB0Function()
    {
        RenderSettings.skybox.SetColor("_Tint", VBColor);
        RenderSettings.skybox.SetFloat("_Exposure", BiomeExposure); //1f
    }
    private void VB1Function()
    {
        RenderSettings.skybox.SetColor("_Tint", VBColor);
        RenderSettings.skybox.SetFloat("_Exposure", BiomeExposure);
    }
    private void VF0Function()
    {
        RenderSettings.skybox.SetColor("_Tint", VFColor);
        RenderSettings.skybox.SetFloat("_Exposure", BiomeExposure); //1f
    }
    private void VF1Function()
    {
        RenderSettings.skybox.SetColor("_Tint", VFColor);
        RenderSettings.skybox.SetFloat("_Exposure", BiomeExposure);
    }
    private void VC0Function()
    {
        RenderSettings.skybox.SetColor("_Tint", VCColor);
        RenderSettings.skybox.SetFloat("_Exposure", BiomeExposure); //1f
    }
    private void VC1Function()
    {
        RenderSettings.skybox.SetColor("_Tint", VCColor);
        RenderSettings.skybox.SetFloat("_Exposure", BiomeExposure); 
    }
    private void VS0Function()
    {
        RenderSettings.skybox.SetColor("_Tint", VSColor);
        RenderSettings.skybox.SetFloat("_Exposure", BiomeExposure); //1f
    }
    private void VS1Function()
    {
        RenderSettings.skybox.SetColor("_Tint", VSColor);
        RenderSettings.skybox.SetFloat("_Exposure", BiomeExposure);
    }
    private void EG0Function()
    {
        RenderSettings.skybox.SetColor("_Tint", EGColor);
        RenderSettings.skybox.SetFloat("_Exposure", BiomeExposure);
    }
    private void EG1Function()
    {
        RenderSettings.skybox.SetColor("_Tint", EGColor);
        RenderSettings.skybox.SetFloat("_Exposure", BiomeExposure);
    }
    private void EN0Function()
    {
        RenderSettings.skybox.SetColor("_Tint", ENColor);
        RenderSettings.skybox.SetFloat("_Exposure", BiomeExposure);
    }
    private void EN1Function()
    {
        RenderSettings.skybox.SetColor("_Tint", ENColor);
        RenderSettings.skybox.SetFloat("_Exposure", BiomeExposure);
    }
    private void EC0Function()
    {
        RenderSettings.skybox.SetColor("_Tint", ECColor);
        RenderSettings.skybox.SetFloat("_Exposure", BiomeExposure);
    }
    private void EC1Function()
    {
        RenderSettings.skybox.SetColor("_Tint", ECColor);
        RenderSettings.skybox.SetFloat("_Exposure", BiomeExposure);
    }
    private void EP0Function()
    {
        RenderSettings.skybox.SetColor("_Tint", EPColor);
        RenderSettings.skybox.SetFloat("_Exposure", BiomeExposure);
    }
    private void EP1Function()
    {
        RenderSettings.skybox.SetColor("_Tint", EPColor);
        RenderSettings.skybox.SetFloat("_Exposure", BiomeExposure);
    }

}
