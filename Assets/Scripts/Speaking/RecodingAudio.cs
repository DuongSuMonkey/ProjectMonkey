using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecodingAudio : IRecodingAudio
{
   private AudioClip AudioRecord;
   private string filePath;
   private string fileName;
   private const int FREQUENCY = 44100; // Tần số ghi âm - 44,100Hz là tần số chuẩn cho âm thanh CD
    public AudioClip StartRecord(IGenerateRandomName generateRandomName)
    {
        fileName = generateRandomName.GenerateRandomString();
        AudioRecord = Microphone.Start(null, false, 5, FREQUENCY);
        return this.AudioRecord;
    }
    public string StopRecord()
    {
        Microphone.End(null);
        filePath = Application.persistentDataPath + "/" + fileName;
        SavWav.SaveWav(filePath, AudioRecord);
        return this.filePath;
    }
}
