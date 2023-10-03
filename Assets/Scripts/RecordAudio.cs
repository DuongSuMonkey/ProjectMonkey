using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecordAudio : MonoBehaviour
{
    [SerializeField] private Button recordButton;
    [SerializeField] private bool isRecording = false;
    [SerializeField] private AudioClip recordedClip;
    [SerializeField] private string fileName;
    [SerializeField] private string filePath;

    private const int FREQUENCY = 44100; // Tần số ghi âm - 44,100Hz là tần số chuẩn cho âm thanh CD

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
    }

    void StartStopRecording()
    {
        if (!isRecording)
        {
            fileName = GenerateRandomString();
            recordedClip = Microphone.Start(null, false,5, FREQUENCY); // Ghi âm trong vòng 10 giây

            // Khi bắt đầu ghi âm, thay đổi text của button thành "Stop Recording"
            recordButton.GetComponentInChildren<TextMeshProUGUI>().text = "Stop Recording";
        }
        else
        {
            Microphone.End(null); // Dừng ghi âm

            // Khi dừng ghi âm, thay đổi text của button thành "Start Recording"
            recordButton.GetComponentInChildren<TextMeshProUGUI>().text = "Start Recording";
            filePath = Application.persistentDataPath + "/" + fileName;
            // Lưu file ghi âm
            SavWav.SaveWav(filePath, recordedClip);
        }

        isRecording = !isRecording; // Đảo ngược trạng thái ghi âm
    }
    public  string GetRecordingTime()
    {
        // Lấy thời gian hiện tại của hệ thống
        float recordingTime = Time.time;

        // Chuyển thời gian thành định dạng chuỗi
        string recordingTimeString = recordingTime.ToString("mm:ss");

        return recordingTimeString;
    }
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
}
