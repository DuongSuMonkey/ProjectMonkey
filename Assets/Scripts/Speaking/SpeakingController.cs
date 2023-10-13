using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class SpeakingController : MonoBehaviour
{
    [SerializeField] private Button recordButton;
    [SerializeField] private bool isRecording = false;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip recordedClip;
    [SerializeField] private string fileName;
    [SerializeField] private string filePath;
    [SerializeField] private Button playButton;
    [SerializeField] private TextMeshProUGUI score;
    private ICallApi callApi;
    private IPlayBackAudio playBackAudio;
    private IGenerateRandomName generateRandomName;
    private IRecodingAudio recodingAudio;

    [System.Obsolete]
    void Start()
    {
        int minFreq, maxFreq;
        Microphone.GetDeviceCaps(null, out minFreq, out maxFreq); // Lấy thông số tần số từ thiết bị âm thanh
        if (minFreq == 0 && maxFreq == 0)
        {
            Debug.LogError("Không tìm thấy thiết bị ghi âm.");
            return;
        }
        audioSource = GetComponent<AudioSource>();
        recordButton.onClick.AddListener(StartStopRecording);
        playButton.onClick.AddListener(PlayRecordedAudio);
        playButton.gameObject.SetActive(false);
        score.gameObject.SetActive(false);
        callApi = new CallApi();
        generateRandomName = new GenerateRandomName();
        playBackAudio = new PlayBackAudio();
        recodingAudio= new RecodingAudio();
    }

    [System.Obsolete]
    private void StartStopRecording()
    {
        if (!isRecording)
        {
            recordedClip = recodingAudio.StartRecord(generateRandomName);

        }
        else
        {
            filePath = recodingAudio.StopRecord();
            playButton.gameObject.SetActive(true);
            StartCoroutine(callApi.PostApiRequest(filePath + ".wav","dog",score));
        }

        isRecording = !isRecording;
    }
  
    private void PlayRecordedAudio()
    {
        playBackAudio.PlayAudio(audioSource,recordedClip);
    }
}
