using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryAddTrigger : MonoBehaviour
{
    private void OnMouseDown()
    {
        Debug.Log("Test");
        UiManager.AddMemoryItemAction.Invoke();
    }
}
