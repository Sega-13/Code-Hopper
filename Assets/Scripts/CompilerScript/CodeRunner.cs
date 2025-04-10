using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class CodeRunner : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_InputField codeInputField;
    public TMP_Text outputText;

    [Header("JDoodle API Info")]
    public string clientId = "your_client_id";
    public string clientSecret = "your_client_secret";

    private string jdoodleEndpoint = "https://api.jdoodle.com/v1/execute";

    public void OnRunCodeClicked()
    {
        string userCode = codeInputField.text;
        StartCoroutine(RunCode(userCode));
    }

    IEnumerator RunCode(string code)
    {
        var requestData = new JDoodleRequest
        {
            clientId = clientId,
            clientSecret = clientSecret,
            script = code,
            language = "csharp",
            versionIndex = "4"
        };

        string json = JsonUtility.ToJson(requestData);
        UnityWebRequest request = new UnityWebRequest(jdoodleEndpoint, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            JDoodleResponse response = JsonUtility.FromJson<JDoodleResponse>(request.downloadHandler.text);
            outputText.text = response.output;
            if (!string.IsNullOrEmpty(response.output) &&
           !response.output.ToLower().Contains("exception") &&
           !response.output.ToLower().Contains("error"))
            {
                Debug.Log("Execution Successful!");
                PuzzleEvents.Instance.OnCodeSuccess.InvokeEvent(); // Trigger success event
            }
            else
            {
                codeInputField.interactable = true;
                Debug.Log("Code executed with error: " + response.output);
                PuzzleEvents.Instance.OnCodeFailure.InvokeEvent();
                // Optionally trigger OnCodeFailure event
            }

            Debug.Log("Success" + outputText.text);
        }
        else
        {
            outputText.text = "Error: " + request.error;
            Debug.Log("@@@@" + outputText.text);
        }
    }

    [System.Serializable]
    public class JDoodleRequest
    {
        public string clientId;
        public string clientSecret;
        public string script;
        public string language;
        public string versionIndex;
    }

    [System.Serializable]
    public class JDoodleResponse
    {
        public string output;
        public int statusCode;
        public string memory;
        public string cpuTime;
    }
}
