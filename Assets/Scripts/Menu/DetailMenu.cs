using BookCurlPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetailMenu : MonoBehaviour
{
    [SerializeField] private Button listenBtn;
    [SerializeField] private Button readBtn;
    [SerializeField] private BookPro bookPro;
    [SerializeField] private MenuController menuPanel;
    [SerializeField] private SpeakingController recordAudio;
    private void Start()
    {
        listenBtn.onClick.AddListener(()=>Listen());
        readBtn.onClick.AddListener(()=>Read());
        bookPro = FindAnyObjectByType<BookPro>();
        recordAudio=FindAnyObjectByType<SpeakingController>();
        menuPanel = FindAnyObjectByType<MenuController>();
        bookPro.gameObject.SetActive(false);
        menuPanel.gameObject.SetActive(false);
        recordAudio.gameObject.SetActive(false);
    }
    private void Listen()
    {
        bookPro.gameObject.SetActive(true);
        menuPanel.gameObject.SetActive(true);
       // this.gameObject.SetActive(false);
    }
    private void Read()
    {
        bookPro.gameObject.SetActive(true);
        menuPanel.gameObject.SetActive(false);
        recordAudio.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }

}
