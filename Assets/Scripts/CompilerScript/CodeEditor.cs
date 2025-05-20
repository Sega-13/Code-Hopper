using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CodeEditor : MonoBehaviour
{
    public TMP_InputField codeInputField;
    public TMP_Text codeDisplayText;

    void Start()
    {
        codeInputField.onValueChanged.AddListener(UpdateCodeDisplay);
    }

    void UpdateCodeDisplay(string newText)
    {
        codeDisplayText.text = newText;  // Update display text
    }
}
