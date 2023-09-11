using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System;
using System.Collections;

public class TouchesController : Texts
{
    [SerializeField] private List<Blink> blinks;
    [SerializeField] private List<TouchUI> touches;
    [SerializeField] private PageController pageController;
    [SerializeField] private bool isFirst = true;
    [SerializeField] private IBlinkController blinkController;
    [SerializeField] private ITouchUIController touchUIController;
    private void Start()
    {
        blinkController = new BlinkController(blinks, touches);
        touchUIController = new TouchUIController(touches, currentIndex);
        AddEventBlink();
        HideAllTouches();
        HideAllBlinks();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        LoadPageController();
        LoadBlinks();
        LoadTouches();
        LoadTexts();
    }

    private void LoadBlinks()
    {
        blinks.AddRange(GetComponentsInChildren<Blink>());
    }

    private void LoadTouches()
    {
        foreach (var blink in blinks)
        {
            touches.Add(blink.GetComponentInChildren<TouchUI>());
        }
    }

    private void LoadPageController()
    {
        pageController = GetComponentInParent<PageController>();
    }

    public override void LoadTexts()
    {
        foreach (var changeTextColor in pageController.ChangeTextColor)
        {
            TextMeshProUGUI[] texts = changeTextColor.GetComponentsInChildren<TextMeshProUGUI>();
            AddText(texts);
        }
    }

    private void HideAllTouches()
    {
        foreach (var touch in touches)
        {
            touch.gameObject.SetActive(false);
        }
    }

    private void HideAllBlinks()
    {
        foreach (var blink in blinks)
        {
            blink.blinkEffect.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (pageController.IsFinal() && isFirst)
        {
            blinks[0].blinkEffect.gameObject.SetActive (true);
            isFirst = false;
        }
    }

    private void AddEventBlink()
    {
        foreach (var blink in blinks)
        {
            blink.OnItemClicked += HandleItemSelection;
        }
    }

    private void HandleItemSelection(Blink blink)
    {
        if (IsClick())
        {
            int index = blinks.IndexOf(blink);
            blink.isClick=true;
            blink.countClick++;
            touchUIController.SearchText(touches[index], txtsContent,this);
            if (blink.countClick > 1 || blink != touchUIController.GetBlink(blinks))
            {
                blinkController.ProcessDoubleClick(blink, index);
                return;
            }
            touchUIController.ShowTouchCurrent(blinks, touches, this);
        }
    }
    public bool IsClick()
    {
        return pageController.ChangeTextColor[pageController.ChangeTextColor.Count - 1].IsFinal;
    }
    public void ShowTouchNext()
    {
       touchUIController.ShowTouchNext(blinks); 
    }
    public void ShowTextCurrent()
    {
        touchUIController.ShowTouchCurrent(blinks,touches,this);
    }
}

