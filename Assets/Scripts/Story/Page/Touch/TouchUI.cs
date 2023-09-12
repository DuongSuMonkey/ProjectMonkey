using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchUI : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI txtContent;
    [SerializeField] public AudioSource audioSource;
    [SerializeField] public AudioClip audioClip;
    [SerializeField] public Animator animator;
    [SerializeField] public Image background;
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
        background= GetComponentInChildren<Image>();
        animator= GetComponentInChildren<Animator>();   
    }
    public void Select()
    {
        Vector3 canvasPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        gameObject.transform.position = new Vector3(canvasPos.x, canvasPos.y, 0);
        background.enabled = true;
        txtContent.enabled = true;
        animator.SetTrigger("Appear");
        audioSource.PlayOneShot(audioClip); 
    }
    public void HideTouch()
    {
        background.enabled = false;
        txtContent.enabled = false;
    }
    public void StartCoroutineDestroyTouch()
    {
        StartCoroutine(DestroyTouch());
    }
    IEnumerator DestroyTouch()
    {
        yield return new WaitForSeconds(audioClip.length);
        Destroy(gameObject);
    }
}
