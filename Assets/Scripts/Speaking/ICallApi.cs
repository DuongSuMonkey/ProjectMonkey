using System.Collections;
using TMPro;

public interface ICallApi
{
    IEnumerator PostApiRequest(string Audiofile, string sentence, TextMeshProUGUI scoreTxt);
}