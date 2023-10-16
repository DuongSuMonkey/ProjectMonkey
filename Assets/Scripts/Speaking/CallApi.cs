using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class CallApi : ICallApi
{
    private string url = "https://app.monkeyuni.net/lesson/api/v1/speech/verification?app_id=2&is_web=1";
    private string profileId = "11111";
    private  string lessonId = "0";
    [System.Obsolete]
    public IEnumerator PostApiRequest(string Audiofile,string sentence,TextMeshProUGUI scoreTxt)
    {
        
        // Tạo một form để chứa thông tin của yêu cầu POST
        WWWForm form = new WWWForm();
        form.AddField("profile_id", profileId);
        form.AddField("lesson_id", lessonId);
        form.AddBinaryData("audio_file", File.ReadAllBytes(Audiofile));
        form.AddField("sentence", sentence);

        // Gửi yêu cầu POST sử dụng UnityWebRequest
        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                string jsonResponse = www.downloadHandler.text;
                JObject jsonObject = JObject.Parse(jsonResponse);
                float score = (float) jsonObject["data"]["score"];
                scoreTxt.text ="Your Score:" +score;
                scoreTxt.color= Color.black;
                scoreTxt.gameObject.SetActive(true);
                Debug.Log("API response: " + www.downloadHandler.text);
            }
        }
    }

}
