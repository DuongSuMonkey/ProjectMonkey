using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecordAudio : MonoBehaviour
{
    public Button recordButton;
    [SerializeField] private bool isRecording = false;
    [SerializeField] private AudioClip recordedClip;

    // Các biến để lưu đường dẫn lưu trữ file ghi âm
    [SerializeField] private string filename = "recordedClip.wav";
    [SerializeField] private string filepath;

    const int FREQUENCY = 44100; // Tần số ghi âm - 44,100Hz là tần số chuẩn cho âm thanh CD

    void Start()
    {
        int minFreq, maxFreq;
        Microphone.GetDeviceCaps(null, out minFreq, out maxFreq); // Lấy thông số tần số từ thiết bị âm thanh
        recordButton.GetComponentInChildren<TextMeshProUGUI>().text = "Start Recording";
        if (minFreq == 0 && maxFreq == 0)
        {
            Debug.LogError("Không tìm thấy thiết bị ghi âm.");
            return;
        }

        recordButton.onClick.AddListener(StartStopRecording);
        //filepath = Application.persistentDataPath + "/" + filename;
        filepath = "D:/ProjectMonkey/RecordAudio" + "/" + filename; // Đường dẫn lưu file ghi âm
    }

    void StartStopRecording()
    {
        if (!isRecording)
        {
            recordedClip = Microphone.Start(null, false, 10, FREQUENCY); // Ghi âm trong vòng 10 giây

            // Khi bắt đầu ghi âm, thay đổi text của button thành "Stop Recording"
            recordButton.GetComponentInChildren<TextMeshProUGUI>().text = "Stop Recording";
        }
        else
        {
            Microphone.End(null); // Dừng ghi âm

            // Khi dừng ghi âm, thay đổi text của button thành "Start Recording"
            recordButton.GetComponentInChildren<TextMeshProUGUI>().text = "Start Recording";

            // Lưu file ghi âm
            SavWav.SaveWav(filepath, recordedClip);
        }

        isRecording = !isRecording; // Đảo ngược trạng thái ghi âm
    }
}
