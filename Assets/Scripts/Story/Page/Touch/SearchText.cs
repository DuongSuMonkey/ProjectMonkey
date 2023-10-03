using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class SearchText : ISearchText
{
    private List<TextMeshProUGUI> txtContents;
    private MonoBehaviour obj;
    private List<TouchObject> touchesObject;
    private Coroutine originalTextColorCoroutine;
    private IPageController pageController;
    public SearchText(List<TextMeshProUGUI> txtContents, MonoBehaviour obj, List<TouchObject> touchesObject, IPageController pageController)
    {
        this.txtContents = txtContents;
        this.obj = obj;
        this.touchesObject = touchesObject;
        this.pageController = pageController;
    }
    public void Search(TouchObject touchObject)
    {
        for (int i = 0; i < txtContents.Count; i++)
        {
            string textContent = Regex.Replace(txtContents[i].text, @"[,.;!?]", "");
            string doubleTextContent = "";
            string textContentNoS = Regex.Replace(txtContents[i].text, @"[,.;!?s]", "");
            if (i < txtContents.Count - 1)
            {
                doubleTextContent = Regex.Replace(txtContents[i].text, @"[,.;!?]", "") + " " + Regex.Replace(txtContents[i+1].text, @"[,.;!?]", "");
            }
            if (touchObject.touchUI.txtContent.text.Equals(textContent) || touchObject.touchUI.txtContent.text.Equals(textContentNoS))
            {
                txtContents[i].color = Color.red;
                txtContents[i].GetComponent<Animator>().SetTrigger("isHightlight");
                if (originalTextColorCoroutine != null)
                {
                    obj.StopCoroutine(originalTextColorCoroutine);
                }
                originalTextColorCoroutine = obj.StartCoroutine(OriginalTextColorCoroutine(txtContents[i]));
            }
            else if (touchObject.touchUI.txtContent.text.Equals(doubleTextContent))
            {
                txtContents[i].color = Color.red;
                txtContents[i+1].color = Color.red;
                txtContents[i].GetComponent<Animator>().SetTrigger("isHightlight");
                txtContents[i+1].GetComponent<Animator>().SetTrigger("isHightlight");
                if (originalTextColorCoroutine != null)
                {
                    obj.StopCoroutine(originalTextColorCoroutine);
                }
                originalTextColorCoroutine = obj.StartCoroutine(OriginalTextColorCoroutine(txtContents[i]));
                originalTextColorCoroutine = obj.StartCoroutine(OriginalTextColorCoroutine(txtContents[i+1]));
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

    public void OnTouchSelected(TouchObject touchObject)
    {
        if (pageController.IsSyncFinish())
        {
            Search(touchObject);
        } 
    }
}
