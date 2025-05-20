using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CodeRunner : MonoBehaviour
{
    [Header("UI Feedback")]
    [SerializeField] private Button runButton;
    [SerializeField] private GameObject loaderPrefab;
    private GameObject activeLoader;

    [Header("UI Elements")]
    public TMP_InputField codeInputField;
    public TMP_Text outputText;

    [Header("JDoodle API Info")]
    public string clientId = "your_client_id";
    public string clientSecret = "your_client_secret";


    [Header("Puzzle Data")]
    public List<CodePuzzle> codePuzzles;
    private int currentPuzzleIndex = 0;

    private string jdoodleEndpoint = "https://api.jdoodle.com/v1/execute";

    public void OnRunCodeClicked()
    {
        runButton.interactable = false;
        activeLoader = Instantiate(loaderPrefab, runButton.transform.parent);
        activeLoader.transform.SetAsLastSibling();
        string userCode = codeInputField.text;
        string wrappedCode = WrapCodeIfNeeded(userCode); // Wrap user input in a class
        StartCoroutine(RunCode(wrappedCode));
    }
    public void SetPuzzle(int index)
    {
        if (index >= 0 && index < codePuzzles.Count)
        {
            currentPuzzleIndex = index;
            Debug.Log("currentPuzzleIndex" + currentPuzzleIndex);
            codeInputField.text = codePuzzles[index].codeWithBug;
            Debug.Log("!!@ "+codeInputField.text);
        }
        else
        {
            Debug.LogWarning("Invalid puzzle index");
        }
    }
    private string WrapCodeIfNeeded(string userCode)
    {
        bool hasMain = userCode.Contains("Main(");
        bool hasClass = userCode.Contains("class");
        bool isFull = hasMain || hasClass;

        if (isFull)
        {
            return userCode;
        }

        // Wrap snippet
        return@"
        using System;

        public class Program {
        " + IndentLines(userCode.Trim(), "    ") + @"

        public static void Main() {
        var game = new Program();
        Console.WriteLine(game.StartGame());
        }
    }";
    }

    private string IndentLines(string input, string indent)
    {
        var lines = input.Split(new[] { "\r\n", "\r", "\n" }, System.StringSplitOptions.None);
        for (int i = 0; i < lines.Length; i++)
        {
            lines[i] = indent + lines[i];
        }
        return string.Join("\n", lines);
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
        if (activeLoader) activeLoader.GetComponent<UIRotate>().Close();
        runButton.interactable = true;
        if (request.result == UnityWebRequest.Result.Success)
        {
            JDoodleResponse response = JsonUtility.FromJson<JDoodleResponse>(request.downloadHandler.text);
            outputText.text = response.output;
            CodePuzzle activePuzzle = codePuzzles[currentPuzzleIndex];
            bool hasNoError = !string.IsNullOrEmpty(response.output) &&
                              !response.output.ToLower().Contains("exception") &&
                              !response.output.ToLower().Contains("error");

            bool isLogicallyCorrect = string.IsNullOrEmpty(activePuzzle.expectedOutput) ||
                                      response.output.Trim() == activePuzzle.expectedOutput.Trim();

            if (hasNoError || isLogicallyCorrect)
            {
                Debug.Log("Execution Successful and Logically Correct!");
                PuzzleEvents.Instance.OnCodeSuccess.InvokeEvent();
            }
            else
            {
                codeInputField.interactable = true;
                Debug.Log("Code executed with logical or syntax error: " + response.output);
                PuzzleEvents.Instance.OnCodeFailure.InvokeEvent();
            }

            Debug.Log("Final Output:\n" + outputText.text);
        }
        else
        {
            outputText.text = "Error: " + request.error;
            Debug.Log("HTTP Error: " + outputText.text);
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
   