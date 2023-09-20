using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseOptionHandler :IMenuOptionHandler
{
    private Button button;
    private bool isContinue = true;
    private Sprite continueSprite;
    private Sprite pauseSprite;
    private Image pauseImage;
    public PauseOptionHandler(Button button, Image pauseImage )
    {
        this.button = button;
        this.pauseImage = pauseImage;
        continueSprite = Resources.Load<Sprite>("images 1/Play");
        pauseSprite = Resources.Load<Sprite>("images 1/PauseImg");
    }

    public void HandleOption()
    {
        if (isContinue)
        {
            button.GetComponentInChildren<TextMeshProUGUI>().text = "Continue";
            pauseImage.sprite = continueSprite;
            isContinue =!isContinue;
        }
        else
        {
            button.GetComponentInChildren<TextMeshProUGUI>().text = "Pause";
            pauseImage.sprite = pauseSprite;
            isContinue = !isContinue;
        }
    }
}
