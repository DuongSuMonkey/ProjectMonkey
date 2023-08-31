using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject pannelMenu;
    public Button pauseBtn;
    public Button startAgainBtn;
    public Button autoFlipBtn;
    public Button shareBtn;
    public Button exitBtn;
    private void Start()
    {
        autoFlipBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Auto Flip:Off";
        AddButon(pauseBtn,() => PauseOption());
        AddButon(startAgainBtn ,() => StartAgainOption());
        AddButon(autoFlipBtn, () => AutoFlipOption());
        AddButon(shareBtn, () => ShareOption());
        AddButon(exitBtn, () => ExitOption());
    }
    public  void AddButon(Button button, Action onClickAction)
    {
        button.onClick.AddListener(() => onClickAction());
    }
    public void PauseOption()
    {
        pauseBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Continue";
    }
    public void StartAgainOption()
    {
        Debug.Log("Start Again");
    }
    public void AutoFlipOption()
    {
        autoFlipBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Auto Flip:On";
        Debug.Log("Auto Flip:On ");
    }
    public void ShareOption()
    {
        Debug.Log("Share");
    }
    public void ExitOption()
    {
        Debug.Log("Exit");
    }
}

