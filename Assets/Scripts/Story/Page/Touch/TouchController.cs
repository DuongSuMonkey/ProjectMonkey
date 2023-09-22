using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System;
using System.Collections;
using System.Reflection;

public class TouchController : Texts
{
    [SerializeField] private List<TouchObject> touchObjects;
    [SerializeField] private List<TouchUI> touchesUI;
    [SerializeField] private List<TouchUI> existingTouches = new List<TouchUI>();
    [SerializeField] private PageController pageController;
    [SerializeField] private bool canShowFirstBlink = true;
    [SerializeField] private ITouchUIHandler touchUIHandle;
    [SerializeField] private ISearchText searchTextController;
    private void Start()
    {
        searchTextController = new SearchTextController();
        touchUIHandle = new TouchUIHandler(touchesUI, currentIndex, existingTouches);
        AddEventTouch();
        HideAllTouches();
        HideAllBlinks();
    }
    private void Reset()
    {
        LoadComponents();
    }
    public void SetAnchors()
    {
        foreach (var touchObject in touchObjects)
        {
            touchObject.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            touchObject.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0);
        }
    }
    public void SetPosition()
    {
        foreach (var touchObject in touchObjects)
        {
            touchObject.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
        }

    }
    private void LoadComponents()
    {
        LoadPageController();
        LoadTouchObject();
        LoadTouches();
        SetPosition();
        SetScale();
        SetAnchors();
        LoadTexts();
    }
    public void SetScale()
    {
        foreach (var touchObject in touchObjects)
        {
            touchObject.GetComponent<RectTransform>().localScale = Vector3.one;
        }
    }
    private void LoadTouchObject()
    {
        var touchObjectsArray = GetComponentsInChildren<TouchObject>();
        for (int i = touchObjectsArray.Length - 1; i >= 0; i--)
        {
            touchObjects.Add(touchObjectsArray[i]);
        }
    }

    private void LoadTouches()
    {
        foreach (var touchObject in touchObjects)
        {
            touchesUI.Add(touchObject.GetComponentInChildren<TouchUI>());
        }
    }

    private void LoadPageController()
    {
        pageController = GetComponentInParent<PageController>();
    }

    public override void LoadTexts()
    {
        foreach (var syncText in pageController.SyncText)
        {
            TextMeshProUGUI[] texts = syncText.GetComponentsInChildren<TextMeshProUGUI>();
            AddText(texts);
        }
    }
    private void HideAllTouches()
    {
        foreach (var touch in touchesUI)
        {
            touch.HideTouch();
        }
    }

    private void HideAllBlinks()
    {
        foreach (var blink in touchObjects)
        {
            blink.blinkEffect.gameObject.SetActive(false);
        }
    }
    private void AddEventTouch()
    {
        foreach (var touch in touchObjects)
        {
            touch.OnClicked += HandleTouchSelection;
        }
    }

    private void HandleTouchSelection(TouchObject touchObject)
    {
        if (CanClick())
        {
            int index = touchObjects.IndexOf(touchObject);
            TouchSelection(touchObject, index);
        }
    }
    private void TouchSelection(TouchObject touchObject,int index)
    {
        touchObject.Select(touchUIHandle,touchObjects,touchesUI,index);
        SearchText(touchesUI[index]);
    }
    public bool CanClick()
    {
        return pageController.SyncText[pageController.SyncText.Count - 1].IsFinal;
    }
    private void SearchText(TouchUI touch)
    {
        searchTextController.Search(touch, txtContents, this);
    }
    public void ShowFirstBlink()
    {
        for (int i = 0; i < touchObjects.Count; i++)
        {
            if (touchObjects[i].hasBlink)
            {
                touchObjects[i].blinkEffect.gameObject.SetActive(true);
                currentIndex = i;
                touchUIHandle = new TouchUIHandler(touchesUI, currentIndex, existingTouches);
                canShowFirstBlink = false;
                break;
            }
        }
    }
    private void Update()
    {
        if (pageController.IsFinal() && canShowFirstBlink)
        {
            ShowFirstBlink();
        }
    }
}

