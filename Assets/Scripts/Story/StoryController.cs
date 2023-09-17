using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryController : MonoBehaviour
{
    [SerializeField] private List<PageController> pages;
    [SerializeField] private int currentIndex=0;
    public bool isStart=true;
    private void Start()
    {
        if (isStart)
        {
            foreach (PageController page in pages)
            {
                page.gameObject.SetActive(false);
            }
            pages[0].gameObject.SetActive(true);
        }
    }
    private void Reset()
    {
        LoadComponents();
    }
    public void LoadComponents()
    {
       LoadPages();

    }
    private void LoadPages()
    {
        PageController[] page = GetComponentsInChildren<PageController>();
        pages.AddRange(page);
    }
    public void NextPage()
    {
        pages[currentIndex].gameObject.SetActive(false);
        currentIndex++;
        pages[currentIndex].gameObject.SetActive(true);

    }
    
}
