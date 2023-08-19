using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchesController : Texts
{
    [SerializeField] private List<TouchUI> touches;
    public int currentlyDraggedItemIndex = -1;
    public event Action<int> OnDescriptionRequested,
            OnItemActionRequested,
            OnStartDragging;
    private void Start()
    {
        AddEventTouch();
        foreach (var touch in touches)
        {
            touch.gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        ChangeTimeTouch();
        ChangeTouch();
    }
    public void AddEventTouch()
    {
        foreach (var item in touches)
        {
            item.OnItemClicked += HandleItemSelection;
        }

    }
    private void HandleItemSelection(TouchUI touch)
    {
        int index = touches.IndexOf(touch);
        for (int i = 0; i < txtsContent.Count; i++)
        {
            if (touch.TxtContent.text == txtsContent[i].text)
            {
                txtsContent[i].color = UnityEngine.Color.red;
            }
        }
            touch.Select();
        timer = timeChange;
        if (index == -1)
            return;
        OnDescriptionRequested?.Invoke(index);
    }
    public void ChangeTimeTouch()
    {
        if (ChangeTextColor.Instance.IsFinal)
        {
            if (currentIndex < touches.Count)
            {
                timer += Time.deltaTime;
            }
            else
            {
                return;
            }
        }
    }
    public void ChangeTouch()
    {
        if (timer >= timeChange && currentIndex < touches.Count)
        {
            foreach (var touch in touches)
            {
                touch.gameObject.SetActive(false);
            }
            touches[currentIndex].gameObject.SetActive(true);
            currentIndex++;
            timer = 0.0f;
        }
    }
        
}
