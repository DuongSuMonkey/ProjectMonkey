using UnityEngine;

public interface IRecodingAudio
{
    AudioClip StartRecord(IGenerateRandomName generateRandomName);
    string StopRecord();
}