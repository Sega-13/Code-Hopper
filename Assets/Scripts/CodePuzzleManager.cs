using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CodePuzzleManager : MonoBehaviour
{
    private enum PuzzleState
    {
        Idle,
        Loading,
        WaitingForInput,
        RunningCode,
        Passed,
        Failed,
        Refixed,
        Completed
    }

    [SerializeField] private CodeRunner codeRunner;
    [SerializeField] private List<CodePuzzle> puzzles;
    [SerializeField] private List<GameObject> Traps;
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

    private PuzzleState currentState = PuzzleState.Idle;
    private Coroutine displayRoutine;
    private CodePuzzle currentPuzzle;
    private int currentPuzzleIndex = 0;
    private int trapIndex = 0;
    private bool hasMadeMistake = false;

    

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

    private void SetState(PuzzleState newState)
    {
        currentState = newState;

        switch (currentState)
        {
            case PuzzleState.Loading:
                SetupUIForPuzzle();
                break;

            case PuzzleState.WaitingForInput:
                Run.gameObject.SetActive(true);
                Hint.gameObject.SetActive(true);
                break;

            case PuzzleState.RunningCode:
                break;

            case PuzzleState.Passed:
                HandlePuzzlePassed();
                break;

            case PuzzleState.Failed:
                HandlePuzzleFailed();
                break;

            case PuzzleState.Refixed:
                LoadPuzzle(currentPuzzleIndex);
                break;

            case PuzzleState.Completed:
                break;
        }
    }

    public void LoadPuzzle(int index)
    {
        if (index < 0 || index >= puzzles.Count) return;

        currentPuzzle = puzzles[index];
        currentPuzzleIndex = index;
        codeRunner.SetPuzzle(currentPuzzleIndex);
        hasMadeMistake = false;

        SetState(PuzzleState.Loading);
    }

    private void SetupUIForPuzzle()
    {
        codeInputField.text = currentPuzzle.codeWithBug;
        puzzleTitleText.text = currentPuzzle.puzzleName;

        result.gameObject.SetActive(false);
        codeInputField.gameObject.SetActive(true);

        Run.gameObject.SetActive(true);
        Hint.gameObject.SetActive(true);
        Refix.gameObject.SetActive(false);
        Restart.gameObject.SetActive(false);
        Refixmssgbox.gameObject.SetActive(false);
        CodeBlock.SetActive(true);

        SetState(PuzzleState.WaitingForInput);
    }

    private void OnSuccess()
    {
        Run.gameObject.SetActive(false);
        Hint.gameObject.SetActive(false);
        SetState(PuzzleState.Passed);
        PuzzleEvents.Instance.OnPuzzleCompleted?.InvokeEvent(true);
    }

    private void OnFailure()
    {
        hasMadeMistake = true;
        SetState(PuzzleState.Failed);
        PuzzleEvents.Instance.OnPuzzleCompleted?.InvokeEvent(false);
    }

    private void HandlePuzzlePassed()
    {
        codeInputField.gameObject.SetActive(false);
        result.gameObject.SetActive(true);
        collectManager.AddCrystal(10);

        if (!hasMadeMistake)
        {
            StartCoroutine(ShowChest());
        }
        displayRoutine = StartCoroutine(ShowAndHide(result));
    }

    private void HandlePuzzleFailed()
    {
        codeInputField.gameObject.SetActive(false);
        result.gameObject.SetActive(true);
        Run.gameObject.SetActive(false);
        Hint.gameObject.SetActive(false);
        Refix.gameObject.SetActive(true);
        Restart.gameObject.SetActive(true);
    }

    public void OnRefixButtonClick()
    {
        if (collectManager.crystalCount >= 10)
        {
            collectManager.RemoveCrystal(10);
            SetState(PuzzleState.Refixed);
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

    public void RestartPuzzle()
    {
        LevelManager.Instance.ReloadLevel();
    }

    private IEnumerator ShowChest()
    {
        yield return new WaitForSeconds(5);
        chestBox.SetActive(true);
        Debug.Log("Chest revealed! Puzzle solved in one go!");
    }

    private IEnumerator ShowAndHide(TextMeshProUGUI resultText)
    {
        yield return new WaitForSeconds(5);
        CodeBlock.SetActive(false);
        resultText.gameObject.SetActive(false);

        if (trapIndex >= 0 && trapIndex < Traps.Count)
        {
            Traps[trapIndex].SetActive(false);
            Debug.Log($"Trap at index {trapIndex} removed.");
        }
        trapIndex++;
    }
}





