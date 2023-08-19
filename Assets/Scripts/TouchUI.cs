using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private bool isTouch=false;
    [SerializeField] private TextMeshProUGUI txtContent;
    public event Action<TouchUI> OnRightMouseBtnClick, OnItemClicked;
    public bool IsTouch { get => isTouch; }
    public TextMeshProUGUI TxtContent { get => txtContent; }

    private void Start()
    {
        txtContent = GetComponentInChildren<TextMeshProUGUI>();
    }
    public void OnPointerClick(PointerEventData pointerdata)
    {

        if (pointerdata.button == PointerEventData.InputButton.Right)
        {
            OnRightMouseBtnClick?.Invoke(this);
        }
        else
        {
            OnItemClicked?.Invoke(this);
        }
        
    }
    public void Select()
    {
        isTouch = true;
        gameObject.SetActive(false);
    }

}
