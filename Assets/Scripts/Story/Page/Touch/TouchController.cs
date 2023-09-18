using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System;
using System.Collections;
using System.Reflection;

public class TouchesController : Texts
{
    [SerializeField] private List<TouchObject> touchObjects;
    [SerializeField] private List<TouchUI> touchesUI;
    [SerializeField] private PageController pageController;
    [SerializeField] private bool isFirst = true;
    [SerializeField] private ITouchUIHandler touchUIController;
    [SerializeField] private ISearchText searchTextController;
    [SerializeField] private List<TouchUI> existingTouches=new List<TouchUI>();
    private void Start()
    {
        searchTextController = new SearchTextController();
        touchUIController = new TouchUIHandler(touchesUI, currentIndex, existingTouches);
        AddEventTouch();
        HideAllTouches();
        HideAllBlinks();
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
    private void Reset()
    {
        LoadComponents();
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
        //var touchObjectsArray = GetComponentsInChildren<TouchObject>();
        //for (int i = touchObjectsArray.Length - 1; i >= 0; i--)
        //{
        //    touchObjects.Add(touchObjectsArray[i]);
        //}
        touchObjects.AddRange(GetComponentsInChildren<TouchObject>());
    }

    private void LoadTouches()
    {
        //var touchObjectsArray = GetComponentsInChildren<TouchUI>();
        //for (int i = touchObjectsArray.Length - 1; i >= 0; i--)
        //{
        //    //var touchObject = touchObjects[i];
        //    touchesUI.Add(touchObjectsArray[i]);
        //}
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

    private void Update()
    {
        if (pageController.IsFinal() && isFirst)
        {
            for (int i = 0; i < touchObjects.Count; i++)
            {
                if (touchObjects[i].isBlink)
                {
                    touchObjects[i].blinkEffect.gameObject.SetActive(true);
                    isFirst = false;
                    break;
                }
            }
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
            int index = this.touchObjects.IndexOf(touchObject);
            TouchSelection(touchObject, index);
        }
    }
    private void TouchSelection(TouchObject touchObject,int index)
    {
        touchObject.isClick = true;
        touchObject.countClick++;
        SearchText(touchesUI[index]);
        HideAllTouch();
        if (touchObject.countClick > 1 || touchUIController.GetTouch() != touchUIController.GetTouch())
        {
            ProcessDoubleClick(touchObject, index);
            return;
        }
        ShowTouchCurrent();
    }
    public bool CanClick()
    {
        return pageController.SyncText[pageController.SyncText.Count - 1].IsFinal;
    }
    public void ShowBlinkNext()
    {
       touchUIController.ShowBlinkNext(touchObjects); 
    }
    public void ShowTouchCurrent()
    {
        touchUIController.ShowTouchCurrent(touchObjects,touchesUI);
    }
    private void SearchText(TouchUI touch)
    {
        searchTextController.Search(touch, txtContents, this);
    }
    private void ProcessDoubleClick(TouchObject touchObject, int index)
    {
        touchUIController.ProcessDoubleClick(touchObject, index);
    }
    public void HideAllTouch()
    {
        touchUIController.HideAllTouch();
    }
}

