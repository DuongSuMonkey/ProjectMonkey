using BookCurlPro;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    //public GameObject pannelMenu;
    //public Button pauseBtn;
    //public Button startAgainBtn;
    //public Button autoFlipBtn;
    //public Button shareBtn;
    //public Button exitBtn;
    //[Header("Pause")]
    //public bool isContinue=false;
    //public Sprite continueSprite;
    //public Image pauseImage;
    //public Sprite pauseSprite;
    //[Header("AutoFlip")]
    //public Sprite autiFlipOnSprite;
    //public Image autiFlipImage;
    //public Sprite autiFlipOffSprite;
    //public bool isFlip = false;
    //private void Start()
    //{
    //    autoFlipBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Auto Flip:On";
    //    AddListenerButton(pauseBtn,() => PauseOption());
    //    AddListenerButton(startAgainBtn ,() => StartAgainOption());
    //    AddListenerButton(autoFlipBtn, () => AutoFlipOption());
    //    AddListenerButton(shareBtn, () => ShareOption());
    //    AddListenerButton(exitBtn, () => ExitOption());
    //}
    //public  void AddListenerButton(Button button, Action onClickAction)
    //{
    //    button.onClick.AddListener(() => onClickAction());
    //}
    //public void PauseOption()
    //{
    //    if (isContinue)
    //    {
    //        pauseBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Continue";
    //        pauseImage.sprite = continueSprite;
    //        isContinue = !isContinue;
    //    }
    //    else
    //    {
    //        pauseBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Pause";
    //        pauseImage.sprite = pauseSprite;
    //        isContinue = !isContinue;
    //    }
    //}
    //public void StartAgainOption()
    //{
    //    Debug.Log("Start Again");
    //}
    //public void AutoFlipOption()
    //{
    //    if (isFlip)
    //    {
    //        autoFlipBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Auto Flip:On";
    //        autiFlipImage.sprite = autiFlipOnSprite;
    //        isFlip = !isFlip;
    //    }
    //    else
    //    {
    //        autoFlipBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Auto Flip:Off";
    //        autiFlipImage.sprite = autiFlipOffSprite;
    //        isFlip = !isFlip;
    //    }
    //}
    //public void ShareOption()
    //{
    //    Debug.Log("Share");
    //}
    //public void ExitOption()
    //{
    //    Debug.Log("Exit");
    //}
    [Header("Button")]
    public Button pauseBtn;
    public Button startAgainBtn;
    public Button autoFlipBtn;
    public Button shareBtn;
    public Button exitBtn;
    [Header("Image")]
    public Image flipImage;
    public Image pauseImage;
    private IMenuOptionHandler pauseOptionHandler;
    private IMenuOptionHandler startAgainOptionHandler;
    private IMenuOptionHandler autoFlipOptionHandler;
    private IMenuOptionHandler shareOptionHandler;
    private IMenuOptionHandler exitOptionHandler;
    
   [SerializeField] private BookPro bookPro;
    [SerializeField] private DetailMenu detailMenu;
    private void Start()
    {
        bookPro = FindAnyObjectByType<BookPro>();
        detailMenu = FindAnyObjectByType<DetailMenu>();

        // Create instances of the handler classes
        pauseOptionHandler = new PauseOptionHandler(pauseBtn, pauseImage);
        startAgainOptionHandler = new StartAgainOptionHandler();
        autoFlipOptionHandler = new AutoFlipOptionHandler(autoFlipBtn,flipImage);
        shareOptionHandler = new ShareOptionHandler();
        exitOptionHandler = new ExitOptionHandler(bookPro,detailMenu);
        autoFlipBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Auto Flip:On";
        // Add the handlers to the buttons' click events
        pauseBtn.onClick.AddListener(() => pauseOptionHandler.HandleOption());
        startAgainBtn.onClick.AddListener(() => startAgainOptionHandler.HandleOption());
        autoFlipBtn.onClick.AddListener(() => autoFlipOptionHandler.HandleOption());
        shareBtn.onClick.AddListener(() => shareOptionHandler.HandleOption());
        exitBtn.onClick.AddListener(() => exitOptionHandler.HandleOption());
    }
}

