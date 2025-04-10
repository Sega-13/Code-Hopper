using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class CodePuzzleManager : MonoBehaviour
{
    [SerializeField] private CodePuzzle currentPuzzle; // Assign a ScriptableObject in Inspector
    [SerializeField] private TMP_InputField codeInputField;
    [SerializeField] private TextMeshProUGUI puzzleTitleText;
    [SerializeField] private TextMeshProUGUI result;
    [SerializeField] private GameObject CodeBlock;
    [SerializeField] private GameObject Trap;
    [SerializeField] private Button Run;
    [SerializeField] private Button Hint;
    [SerializeField] private Button Refix;
    [SerializeField] private Button Restart;
    
    float displayDuration = 5f;
    private Coroutine displayRoutine;

    void Start()
    {
        if (currentPuzzle != null && codeInputField != null)
        {
            LoadPuzzle(currentPuzzle);
        }
    }
    private void OnEnable()
    {
        PuzzleEvents.Instance.OnCodeSuccess.AddListener(OnSuccess);
        PuzzleEvents.Instance.OnCodeFailure.AddListener(OnFailure);
    }
    private void OnDisable()
    {
        PuzzleEvents.Instance.OnCodeSuccess.RemoveListener(OnSuccess);
        PuzzleEvents.Instance.OnCodeFailure.RemoveListener(OnFailure);
    }

    public void LoadPuzzle(CodePuzzle puzzle)
    {
        currentPuzzle = puzzle;
        codeInputField.text = puzzle.codeWithBug;

        if (puzzleTitleText != null)
            puzzleTitleText.text = puzzle.puzzleName;
    }

    private void OnSuccess()
    {
        codeInputField.gameObject.SetActive(false);
        displayRoutine = StartCoroutine(ShowAndHide(result));
    }
    private void OnFailure()
    {
        codeInputField.gameObject.SetActive(false);
        Run.gameObject.SetActive(false);
        Hint.gameObject.SetActive(false); 
        Refix.gameObject.SetActive(true);
        Restart.gameObject.SetActive(true);
    }
    private IEnumerator ShowAndHide(TextMeshProUGUI result)
    {
        yield return new WaitForSeconds(displayDuration);
        CodeBlock.gameObject.SetActive(false);
        Trap.gameObject.SetActive(false);
    }
    public void OnRefixButtonClick()
    {
        Refix.gameObject.SetActive(false);
        Restart.gameObject.SetActive(false);
        result.gameObject.SetActive(false);
        codeInputField.gameObject.SetActive(true);
        LoadPuzzle(currentPuzzle);
        /*result.gameObject.SetActive(false);
        Refix.gameObject.SetActive(false);
        codeInputField.gameObject.SetActive(true);
        result.gameObject.SetActive(false);
        //CodeBlock.gameObject.SetActive(true);*/
    }
    public void RestartPuzzle()
    {
        LevelLoader.Instance.ReloadLevel();
    }
    /*public string GetCorrectCode()
    {
        return currentPuzzle != null ? currentPuzzle.correctCode : "";
    }*/
}
