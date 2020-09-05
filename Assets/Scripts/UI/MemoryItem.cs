using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum FlaskType
{
    Content = 0,
    Excitement,
    Joy,
    Kind,
    Love,
    Pride
}

public class MemoryItem : MonoBehaviour
{
    public MemoryItemData Data { get; set; }

    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private Image iconImage;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnMemoryItemClicked);
    }

    private void OnMemoryItemClicked()
    {
        UiManager.ShowMemoryItemAction.Invoke(Data.Id);
    }

    public void UpdateItem(string title, Sprite sprite)
    {
        text.text = title;
        iconImage.sprite = sprite;
    }
}
