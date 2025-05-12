using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class CodePuzzleManager : MonoBehaviour
{
    [SerializeField] private CodeRunner codeRunner;
    [SerializeField] private List<CodePuzzle> puzzles;
    [SerializeField] private List<GameObject> Traps;
    [SerializeField] private CodePuzzle currentPuzzle; // Assign a ScriptableObject in Inspector
    [SerializeField] private TMP_InputField codeInputField;
    [SerializeField] private TextMeshProUGUI puzzleTitleText;
    [SerializeField] private TextMeshProUGUI result;
    [SerializeField] private GameObject CodeBlock;
    
    [SerializeField] private GameObject Refixmssgbox;
    [SerializeField] private Button Run;
    [SerializeField] private Button Hint;
    [SerializeField] private Button Refix;
    [SerializeField] private Button Restart;
    [SerializeField] private GameObject chestBox;
    [SerializeField] private CollectManager collectManager;
    [SerializeField] private HintManager hintManager;
    
    float displayDuration = 5f;
    private Coroutine displayRoutine;
    private bool hasMadeMistake = false;
    private bool puzzleSolved = false;
    private int currentPuzzleIndex = 0;
    private int trapIndex =0;

    void Start()
    {
        if (puzzles.Count > 0)
        {
            SetActivePuzzle(0); // Start with first puzzle
        }
    }
    public void SetActivePuzzle(int index)
    {
        if (index >= 0 && index < puzzles.Count)
        {
            currentPuzzleIndex = index;
            codeRunner.SetPuzzle(currentPuzzleIndex);
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
    public void LoadPuzzle(int index)
    { 
        if (index >= 0 && index < puzzles.Count)
        {

            currentPuzzle = puzzles[index];
            currentPuzzleIndex = index;

            // Reset UI
            result.gameObject.SetActive(false);
            codeInputField.gameObject.SetActive(true);
            codeInputField.text = currentPuzzle.codeWithBug;
            puzzleTitleText.text = currentPuzzle.puzzleName;

            Run.gameObject.SetActive(true);
            Hint.gameObject.SetActive(true);
            Refix.gameObject.SetActive(false);
            Restart.gameObject.SetActive(false);
            Refixmssgbox.gameObject.SetActive(false);

            CodeBlock.SetActive(true);

            // Reset flags
            hasMadeMistake = false;
            puzzleSolved = false;
            /*currentPuzzle = puzzles[index];
            currentPuzzleIndex = index;
            codeInputField.text = currentPuzzle.codeWithBug;

            if (puzzleTitleText != null)
                puzzleTitleText.text = currentPuzzle.puzzleName;

            CodeBlock.SetActive(true);*/
        }
    }
    /*public void LoadPuzzle(CodePuzzle puzzle)
    {
        currentPuzzle = puzzle;
        codeInputField.text = puzzle.codeWithBug;

        if (puzzleTitleText != null)
            puzzleTitleText.text = puzzle.puzzleName;
    }*/
    public void RegisterMistake()
    {
        hasMadeMistake = true;
    }
    public void OnPuzzleCompleted(bool isCorrect)
    {
        if (puzzleSolved) return; // Prevent triggering multiple times
        puzzleSolved = true;

        if (isCorrect && !hasMadeMistake)
        {
            Coroutine showChest = StartCoroutine (ShowChest());
        }
        else
        {
            Debug.Log("No chest. Player made a mistake or incorrect answer.");
        }
    }

    IEnumerator ShowChest()
    {
        yield return new WaitForSeconds(5);
        chestBox.gameObject.SetActive(true);
        Debug.Log("Chest revealed! Puzzle solved in one go!");
        // Add animation/sound here
    }
    private void OnSuccess()
    {
        OnPuzzleCompleted(true);
        codeInputField.gameObject.SetActive(false);
        result.gameObject.SetActive(true);
        displayRoutine = StartCoroutine(ShowAndHide(result));
        collectManager.AddCrystal(10);
    }
    private void OnFailure()
    {
        RegisterMistake();
        codeInputField.gameObject.SetActive(false);
        result.gameObject.SetActive(true);
        Run.gameObject.SetActive(false);
        Hint.gameObject.SetActive(false); 
        Refix.gameObject.SetActive(true);
        Restart.gameObject.SetActive(true);
    }
    private IEnumerator ShowAndHide(TextMeshProUGUI result)
    {
        yield return new WaitForSeconds(displayDuration);
        CodeBlock.gameObject.SetActive(false);
        result.gameObject .SetActive(false);
        if (trapIndex >= 0 && trapIndex < Traps.Count)
        {
            GameObject trap = Traps[trapIndex];
            Traps[trapIndex].gameObject.SetActive(false);
            Debug.Log($"Trap at index {trapIndex} removed.");
        }
        trapIndex++;
        Debug.Log("TrapIndex : "+ trapIndex);
    }
    public void OnRefixButtonClick()
    {
        if (collectManager.crystalCount >= 10)
        {
            collectManager.RemoveCrystal(10);
            Refix.gameObject.SetActive(false);
            Restart.gameObject.SetActive(false);
            result.gameObject.SetActive(false);
            codeInputField.gameObject.SetActive(true);
            LoadPuzzle(currentPuzzleIndex);
            Run.gameObject.SetActive(true);
            Hint.gameObject.SetActive(true);
        }
        else
        {
            Refixmssgbox.gameObject.SetActive(true);
           
        }
        
    }
    public void OnRefixPanelClose()
    {
        Refixmssgbox.gameObject.SetActive(false);
    }
    public void OnHintButtonClicked()
    {

    }
    public void RestartPuzzle()
    {
        LevelManager.Instance.ReloadLevel();
    }
    
}
