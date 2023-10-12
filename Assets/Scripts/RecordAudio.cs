using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class RecordAudio : MonoBehaviour
{
    [SerializeField] private Button recordButton;
    [SerializeField] private bool isRecording = false;
    [SerializeField] private AudioClip recordedClip;
    [SerializeField] private string fileName;
    [SerializeField] private string filePath;
    [SerializeField] private Button playButton;
    [SerializeField] private TextMeshProUGUI score;
    private const int FREQUENCY = 44100; // Tần số ghi âm - 44,100Hz là tần số chuẩn cho âm thanh CD
    private ICallApi callApi;

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

        recordButton.onClick.AddListener(StartStopRecording);
        playButton.onClick.AddListener(PlayRecordedAudio);
        playButton.gameObject.SetActive(false);
        callApi = new CallApi();
        score.gameObject.SetActive(false);
    }

    [System.Obsolete]
    void StartStopRecording()
    {
        if (!isRecording)
        {
            fileName = GenerateRandomString();
            recordedClip = Microphone.Start(null, false,5, FREQUENCY);

            // Khi bắt đầu ghi âm, thay đổi text của button thành "Stop Recording"
        }
        else
        {
            Microphone.End(null); // Dừng ghi âm

            filePath = Application.persistentDataPath + "/" + fileName;
            // Lưu file ghi âm
            SavWav.SaveWav(filePath, recordedClip);
            playButton.gameObject.SetActive(true);
            StartCoroutine(callApi.PostApiRequest(filePath + ".wav","dog",score));

        }

        isRecording = !isRecording;
    }
    //public  string GetRecordingTime()
    //{
    //    // Lấy thời gian hiện tại của hệ thống
    //    float recordingTime = Time.time;

    //    // Chuyển thời gian thành định dạng chuỗi
    //    string recordingTimeString = recordingTime.ToString("mm:ss");

    //    return recordingTimeString;
    //}
    public  string GenerateRandomString()
    {
        // Tạo một mảng các kí tự
        char[] chars = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

        // Tạo một chuỗi kí tự ngẫu nhiên có độ dài từ 5 đến 10 kí tự
        string randomString = "";
        for (int i = 0; i < Random.Range(5, 10); i++)
        {
            randomString += chars[Random.Range(0,chars.Length)];
        }

        return randomString;
    }
    void PlayRecordedAudio()
    {
        PlayAudio();
    }

    public void PlayAudio()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(recordedClip);
    }

}
