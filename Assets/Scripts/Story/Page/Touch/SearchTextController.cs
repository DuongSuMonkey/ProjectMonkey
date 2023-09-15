using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class SearchTextController : ISearchText
{
    public void Search(TouchUI touch, List<TextMeshProUGUI> txtsContent, MonoBehaviour obj)
    {
        for (int i = 0; i < txtsContent.Count; i++)
        {
            string textcontent = Regex.Replace(txtsContent[i].text, @"[,.;!?]", "");
            if (touch.txtContent.text== textcontent)
            {
                txtsContent[i].color = UnityEngine.Color.red;
                txtsContent[i].GetComponent<Animator>().SetTrigger("isCheck");
                obj.StartCoroutine(OriginalTextColorCoroutine(txtsContent[i]));
            }
        }
    }

    public IEnumerator OriginalTextColorCoroutine(TextMeshProUGUI textContent)
    {
        yield return new WaitForSeconds(1f);
        OriginalTextColor(textContent);
    }

    public void OriginalTextColor(TextMeshProUGUI textcontent)
    {
        if (textcontent.color == UnityEngine.Color.red)
        {
            textcontent.color = UnityEngine.Color.black;
        }
    }
}
