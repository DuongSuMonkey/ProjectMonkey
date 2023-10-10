using UnityEngine;

public class ConfigManager : MonoBehaviour
{
    void Start()
    {
        // Lấy thông tin độ phân giải màn hình
        int screenWidth = Screen.width;
        int screenHeight = Screen.height;
        string resolution = $"{screenWidth}x{screenHeight}";

        // Lấy cài đặt đồ họa hiện tại
        string graphics = QualitySettings.GetQualityLevel().ToString();

        // Lấy trạng thái âm thanh
        bool sound = AudioListener.volume > 0;

        // Lấy loại điều khiển
        string control = "Keyboard";

        // Tạo đối tượng Config và gán thông tin cấu hình
        Config config = new Config();
        config.resolution = resolution;
        config.graphics = graphics;
        config.sound = sound;
        config.control = control;

        // Lưu cấu hình vào file JSON
        string json = JsonUtility.ToJson(config);
        System.IO.File.WriteAllText("config.json", json);
    }
}

