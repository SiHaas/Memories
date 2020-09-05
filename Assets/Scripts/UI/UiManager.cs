using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public enum UiState
{
    None,
    MemoryList,
    AddMemory,
    AddMemoryFromMenu,
    ShowMemory,
    EditMemory
}

public class UiManager : MonoBehaviour
{
    [SerializeField]
    private UserDummy userDummy;

    [SerializeField]
    private Transform memoriesContentHolder;

    [SerializeField]
    private Button burgerMenuButton;

    [SerializeField]
    private Button menuBackButton;

    [SerializeField]
    private Button editButton;

    [SerializeField]
    private Button saveButton;

    [SerializeField]
    private Button addButton;


    // UI basic elements
    [SerializeField]
    private GameObject fullUiReference;
    [SerializeField]
    private GameObject memMem;
    [SerializeField]
    private GameObject memMemCapsule;
    [SerializeField]
    private GameObject memMemCapsuleList;
    [SerializeField]
    private GameObject memoryItemPrefab;

    // details/add/edit
    [SerializeField]
    private GameObject[] addEditObjects;
    [SerializeField]
    private GameObject[] showObjects;


    // actions / events
    public static Action AddMemoryItemAction;
    public static Action<string> ShowMemoryItemAction;

    // input fields
    [SerializeField]
    private TMP_InputField headInput;
    [SerializeField]
    private TMP_InputField contentInput;

    // detail text fields
    [SerializeField]
    private TextMeshProUGUI headText;
    [SerializeField]
    private TextMeshProUGUI createdText;
    [SerializeField]
    private TextMeshProUGUI contentText;
       

    private List<MemoryItem> memories;
    private UiState uiState;

    private MemoryItem currentItem;


    [SerializeField]
    private Image flaskImage;
    [SerializeField]
    private Sprite[] flaskSprites;
    private int currentFlask;
    [SerializeField]
    private Button leftFlaskButton;
    [SerializeField]
    private Button rightFlaskButton;

    void Start()
    {
        fullUiReference?.SetActive(false);
        memMem?.SetActive(false);
        memMemCapsule?.SetActive(false);
        memMemCapsuleList?.SetActive(false);

        // add listener        
        burgerMenuButton.onClick.AddListener(OnBurgerMenuButtonClicked);
        menuBackButton.onClick.AddListener(OnMenuBackButtonClicked);
        editButton.onClick.AddListener(OnEditButtonClicked);
        saveButton.onClick.AddListener(OnSaveButtonClicked);
        addButton.onClick.AddListener(() => 
        {
            OnAddMemory();

            // override ui state
            uiState = UiState.AddMemoryFromMenu;
        });

        rightFlaskButton.onClick.AddListener(() => NextImageSprite(false));
        leftFlaskButton.onClick.AddListener(() => NextImageSprite(true));

        AddMemoryItemAction += OnAddMemory;
        ShowMemoryItemAction += OnShowMemory;


        // init memories
        memories = new List<MemoryItem>();


        uiState = UiState.None;
    }

    private void ShowEditUI(bool show)
    {
        foreach (var item in addEditObjects)
        {
            item.SetActive(show);
        }

        saveButton.gameObject.SetActive(show);
    }

    private void ShowDetailsUI(bool show)
    {
        foreach (var item in showObjects)
        {
            item.SetActive(show);
        }

        editButton.gameObject.SetActive(show);
    }

    private void OnAddMemory()
    {
        userDummy.StopMovement();

        GameObject memory = Instantiate(memoryItemPrefab);
        currentItem = memory.GetComponent<MemoryItem>();

        contentInput.text = string.Empty;
        headInput.text = string.Empty;
        currentFlask = 0;
        flaskImage.sprite = flaskSprites[0];

        fullUiReference.SetActive(true);
        memMem.SetActive(true);
        memMemCapsuleList.SetActive(false);
        addButton.gameObject.SetActive(false);

        ShowEditUI(true);
        ShowDetailsUI(false);
        uiState = UiState.AddMemory;
    }

    private void OnEditButtonClicked()
    {        
        if (currentItem != null)
        {
            // update edit text elements
            headInput.text = currentItem.Data.Title;
            contentInput.text = currentItem.Data.Text;

            ShowEditUI(true);
            ShowDetailsUI(false);
            uiState = UiState.EditMemory;
        }
        else
        {
            Debug.LogError("Could not find currentItem");
        }
    }

    private void OnSaveButtonClicked()
    {
        if (currentItem == null)
            return;

        if(uiState == UiState.AddMemory || uiState == UiState.AddMemoryFromMenu)
        {
            currentItem.transform.parent = memoriesContentHolder;

            currentItem.Data = new MemoryItemData()
            {
                Created = DateTime.Now,
                Id = Guid.NewGuid().ToString(),
                Title = headInput.text,
                Text = contentInput.text,
                FlaskType = GetFlaskType(currentFlask)
            };

            currentItem.UpdateItem(currentItem.Data.Title, flaskSprites[currentFlask]);

            memories.Add(currentItem);
            currentItem = null;
            
            currentFlask = 0;

            ShowEditUI(false);
            ShowDetailsUI(false);
            uiState = UiState.MemoryList;
            memMemCapsuleList.SetActive(true);
            memMem.SetActive(false);
            saveButton.gameObject.SetActive(true);
        }
        else if(uiState == UiState.EditMemory)
        {
            currentItem.Data.Title = headInput.text;
            currentItem.Data.Text = contentInput.text;
            currentItem.Data.FlaskType = GetFlaskType(currentFlask);
            currentItem.UpdateItem(currentItem.Data.Title, flaskSprites[currentFlask]);

            ShowEditUI(false);
            ShowDetailsUI(true);

            uiState = UiState.ShowMemory;

            contentText.text = currentItem.Data.Text;
            headText.text = currentItem.Data.Title;            
        }
    }

    private FlaskType GetFlaskType(int idx)
    {
        switch (idx)
        {
            default:
            case 0:
                return FlaskType.Content;
            case 1:
                return FlaskType.Excitement;
            case 2:
                return FlaskType.Joy;
            case 3:
                return FlaskType.Kind;
            case 4:
                return FlaskType.Love;
            case 5:
                return FlaskType.Pride;
        }
    }

    private void OnShowMemory(string id)
    {
        currentItem = GetMemory(id);

        contentText.text = currentItem.Data.Text;
        createdText.text = $"Created " + currentItem.Data.Created.ToShortDateString();
        flaskImage.sprite = flaskSprites[(int) currentItem.Data.FlaskType];
        headText.text = currentItem.Data.Title;

        ShowEditUI(false);
        ShowDetailsUI(true);
        memMemCapsuleList.SetActive(false);
        memMem.SetActive(true);
        addButton.gameObject.SetActive(false);
        
        uiState = UiState.ShowMemory;
    }

    private MemoryItem GetMemory(string id)
    {
        return memories.Find(x => x.Data.Id.Equals(id));
    }

    private void OnDestroy()
    {
        // remove listener
        burgerMenuButton.onClick.RemoveListener(OnBurgerMenuButtonClicked);
        menuBackButton.onClick.RemoveListener(OnMenuBackButtonClicked);
        editButton.onClick.RemoveListener(OnEditButtonClicked);
        saveButton.onClick.RemoveListener(OnSaveButtonClicked);
        addButton.onClick.RemoveListener(() =>
        {
            uiState = UiState.AddMemoryFromMenu;
            OnAddMemory();
        });

        rightFlaskButton.onClick.RemoveListener(() => NextImageSprite(false));
        leftFlaskButton.onClick.RemoveListener(() => NextImageSprite(true));

        AddMemoryItemAction -= OnAddMemory;
        ShowMemoryItemAction -= OnShowMemory;
    }
    
    private void OnBurgerMenuButtonClicked()
    {
        uiState = UiState.MemoryList;
        userDummy.StopMovement();

        fullUiReference.SetActive(true);
        memMemCapsuleList.SetActive(true);
        editButton.gameObject.SetActive(false);
        saveButton.gameObject.SetActive(false);
        addButton.gameObject.SetActive(true);
    }

    private void OnMenuBackButtonClicked()
    {
        switch(uiState) 
        {
            case UiState.AddMemory:
                fullUiReference.SetActive(false);
                userDummy.StartMovement();
                Destroy(currentItem.gameObject);
                currentItem = null;
                break;
            case UiState.AddMemoryFromMenu:
                userDummy.StartMovement();
                memMem.SetActive(false);
                memMemCapsuleList.SetActive(true);
                ShowEditUI(false);
                ShowDetailsUI(false);
                Destroy(currentItem.gameObject);
                addButton.gameObject.SetActive(true);
                currentItem = null;
                break;
            case UiState.MemoryList:
                fullUiReference.SetActive(false);
                userDummy.StartMovement();
                break;
            case UiState.ShowMemory:
                memMem.SetActive(false);
                memMemCapsuleList.SetActive(true);
                uiState = UiState.MemoryList;
                editButton.gameObject.SetActive(false);
                saveButton.gameObject.SetActive(false);
                addButton.gameObject.SetActive(true);
                currentItem = null;
                break;
            case UiState.EditMemory:
                ShowEditUI(false);
                ShowDetailsUI(true);
                uiState = UiState.ShowMemory;
                break;
        }
    }

    private void NextImageSprite(bool left)
    {
        if(left && currentFlask > 0)
        {
            currentFlask--;
        }
        else if(!left && currentFlask < flaskSprites.Length - 1)
        {
            currentFlask++;
        }

        flaskImage.sprite = flaskSprites[currentFlask];
    }
}
