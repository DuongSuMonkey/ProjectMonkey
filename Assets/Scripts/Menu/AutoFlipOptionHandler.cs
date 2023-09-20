using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AutoFlipOptionHandler : IMenuOptionHandler
{
    private Button button;
    private bool isFlip = false;
    private Sprite flipOnSprite;
    private Sprite flipOffSprite;
    public Image flipImage;

    public AutoFlipOptionHandler(Button button, Image flipimage)
    {
        this.button = button;
        flipOnSprite = Resources.Load<Sprite>("images 1/FlipBookImg");
        flipOffSprite = Resources.Load<Sprite>("images 1/FlipBook_Normal");
        this.flipImage = flipimage;
    }

    public void HandleOption()
    {
        if (isFlip)
        {
            button.GetComponentInChildren<TextMeshProUGUI>().text = "Auto Flip:On";
            flipImage.sprite = flipOnSprite;
            isFlip =!isFlip;
        }
        else
        {
            button.GetComponentInChildren<TextMeshProUGUI>().text = "Auto Flip:Off";
            flipImage.sprite = flipOffSprite;
            isFlip = !isFlip;
        }
    }
}
