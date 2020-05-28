using UnityEngine;

public class TouchExample2 : MonoBehaviour
{
    public GameObject testCube;
  
    // Prints number of fingers touching the screen
    void Update()
    {
        var cubeRenderer = testCube.GetComponent<Renderer>();
        var fingerCount = 0;
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
            {
                fingerCount++;
            }
        }
        if (fingerCount > 0)
        {
            cubeRenderer.material.SetColor("_Color", Color.red);
            print("User has " + fingerCount + " finger(s) touching the screen");
        }
    }
}