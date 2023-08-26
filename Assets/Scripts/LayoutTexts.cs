using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LayoutTexts : MonoBehaviour
{
    // Start is called before the first frame update
    public float spacing=10;
    public List<TextMeshProUGUI> texts;
    private void Reset()
    {

        TextMeshProUGUI[] textt=GetComponentsInChildren<TextMeshProUGUI>();
        texts.AddRange(textt);
        RectTransform rectTransform = GetComponent<RectTransform>();

        // Xác định chiều rộng tối đa mà hàng có thể chứa
        float maxWidth = rectTransform.sizeDelta.x;
        float currentWidth = -rectTransform.sizeDelta.x / 2;
        float currentHeight = -rectTransform.sizeDelta.y / 2;

        foreach (TextMeshProUGUI text in texts)
        {
            text.autoSizeTextContainer = true;
            Vector2 preferredSize = text.GetPreferredValues(text.text);
            // Lấy kích thước của đối tượng text
            RectTransform textRectTransform = text.GetComponent<RectTransform>();
            float textWidth = textRectTransform.sizeDelta.x;
            float textHeight = textRectTransform.sizeDelta.y;
         
            // Kiểm tra nếu độ dài hiện tại + độ dài của đối tượng vượt quá chiều rộng tối đa
            if ( currentWidth + textWidth > maxWidth)
            {
                // Xuống dòng
                currentWidth = -rectTransform.sizeDelta.x / 2;
                currentHeight += textHeight;
            }

            // Đặt vị trí của đối tượng text
            textRectTransform.anchoredPosition = new Vector2(currentWidth + textWidth / 2, -currentHeight - textHeight / 2);
            textRectTransform.pivot = new Vector2(0.5f, 0.5f);
            
            
            // Cập nhật chiều rộng hiện tại
            currentWidth += textWidth+ spacing;
        }
    }
    void Start()
    {
        
    }
}
