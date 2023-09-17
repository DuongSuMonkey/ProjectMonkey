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
            string textcontent = Regex.Replace(txtContents[i].text, @"[,.;!?]", "");
            if (TouchUI.txtContent.text.Equals(textcontent))
            {
                txtContents[i].color = Color.red;
                txtContents[i].GetComponent<Animator>().SetTrigger("isHightlight");
                obj.StartCoroutine(OriginalTextColorCoroutine(txtContents[i]));
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
