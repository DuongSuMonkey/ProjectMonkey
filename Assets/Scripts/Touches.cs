using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Touches : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private List<TextMeshProUGUI> txtContent;
    [SerializeField] private bool isTouch=false;

    public bool IsTouch { get => isTouch; }

    public void OnPointerClick(PointerEventData eventData)
    {
        isTouch = true;
        gameObject.SetActive(false);
        var text=   GetComponentInChildren<TextMeshProUGUI>();
        for (int i = 0; i < txtContent.Count; i++) {
            if (text.text== txtContent[i].text)
            {
                txtContent[i].color = Color.red;
                Debug.Log(i);
            }
     
        }
        
    }


}
