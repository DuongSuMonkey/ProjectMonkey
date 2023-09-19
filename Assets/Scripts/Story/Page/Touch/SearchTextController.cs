using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class SearchTextController : ISearchText
{

    public void Search(TouchUI TouchUI, List<TextMeshProUGUI> txtContents, MonoBehaviour obj)
    {
        for (int i = 0; i < txtContents.Count; i++)
        {
            string textContent = Regex.Replace(txtContents[i].text, @"[,.;!?]", "");
            string doubleTextContent = "";
            if (i < txtContents.Count - 1)
            {
                doubleTextContent = Regex.Replace(txtContents[i].text, @"[,.;!?]", "") + " " + Regex.Replace(txtContents[i+1].text, @"[,.;!?]", "");
            }
            if (TouchUI.txtContent.text.Equals(textContent))
            {
                txtContents[i].color = Color.red;
                txtContents[i].GetComponent<Animator>().SetTrigger("isHightlight");
                obj.StartCoroutine(OriginalTextColorCoroutine(txtContents[i]));
            }else if (TouchUI.txtContent.text.Equals(doubleTextContent))
            {
                txtContents[i].color = Color.red;
                txtContents[i+1].color = Color.red;
                txtContents[i].GetComponent<Animator>().SetTrigger("isHightlight");
                txtContents[i+1].GetComponent<Animator>().SetTrigger("isHightlight");
                obj.StartCoroutine(OriginalTextColorCoroutine(txtContents[i]));
                obj.StartCoroutine(OriginalTextColorCoroutine(txtContents[i+1]));
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
        if (textcontent.color == Color.red)
        {
            textcontent.color = Color.black;
        }
    }
}
