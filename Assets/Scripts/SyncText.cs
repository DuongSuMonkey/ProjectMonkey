
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SyncText : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public AudioClip audioClip;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartCoroutine(PlayAudioClipWithTextSync());
    }

    private IEnumerator PlayAudioClipWithTextSync()
    {
        string fullText = textDisplay.text;
        textDisplay.text = "";

        foreach (char letter in fullText)
        {
            textDisplay.text += "<color=red>" + letter + "</color>";
            audioSource.PlayOneShot(audioClip);

            // Chờ cho âm thanh phát xong trước khi hiển thị chữ tiếp theo
            yield return new WaitForSeconds(audioClip.length);
        }
    }
}

