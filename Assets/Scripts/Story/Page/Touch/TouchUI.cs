using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtContent;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] public AudioClip audioClip;
    public TextMeshProUGUI TxtContent { get => txtContent; }

    private void Reset()
    {
        LoadComponents();
    }
    public void LoadComponents()
    {
        txtContent = GetComponentInChildren<TextMeshProUGUI>();
        audioSource = GetComponent<AudioSource>();
        audioClip = audioSource.clip;
    }
    public void Select()
    {
        Vector3 canvasPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        gameObject.transform.position = new Vector3(canvasPos.x, canvasPos.y, 0);
        gameObject.SetActive(true);
        audioSource.PlayOneShot(audioClip); 
    }
    public void GetDestroy()
    {
        StartCoroutine(DestroyTouch());
    }
    IEnumerator DestroyTouch()
    {
        yield return new WaitForSeconds(audioClip.length);
        Destroy(gameObject);
    }
}
