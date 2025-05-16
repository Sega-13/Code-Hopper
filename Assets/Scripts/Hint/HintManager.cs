using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HintManager : MonoBehaviour
{

    public List<HintData> allHints;
    
    private PuzzleHint currentPuzzleHint;

   
    private Dictionary<string, int> hintIndices = new Dictionary<string, int>();
    private void Start()
    {
        InitializeHintIndicesForCurrentLevel();
    }
    public bool TryGetNextHint(out string hint)
    {
        hint = null;
        if (currentPuzzleHint == null || currentPuzzleHint.hintSteps == null || currentPuzzleHint.hintSteps.Length == 0)
        {
            return false;
        }

        string puzzleId = currentPuzzleHint.puzzleId;
        if (!hintIndices.ContainsKey(puzzleId))
            hintIndices[puzzleId] = 0;

        int index = hintIndices[puzzleId];

        if (index < currentPuzzleHint.hintSteps.Length)
        {
            hint = currentPuzzleHint.hintSteps[index];
            hintIndices[puzzleId] = index + 1;
            return true;
        }

        return false;
    }
    public void InitializeHintIndicesForCurrentLevel()
    {
        List<string> puzzleIds = GetPuzzleIdsForCurrentLevel();

        foreach (string id in puzzleIds)
        {
            if (!hintIndices.ContainsKey(id))
            {
                hintIndices[id] = 0; // Start each puzzle's hint index at 0
            }
        }

        Debug.Log($"[HintManager] Initialized hint indices for {puzzleIds.Count} puzzles.");
    }
    public List<string> GetPuzzleIdsForCurrentLevel()
    {
        string currentLevel = SceneManager.GetActiveScene().name;
        var hintData = allHints.FirstOrDefault(h => h.levelName == currentLevel);

        if (hintData != null && hintData.puzzles != null)
        {
            return hintData.puzzles.Select(p => p.puzzleId).ToList();
        }

        Debug.LogWarning("No HintData or puzzles found for level: " + currentLevel);
        return new List<string>();
    }
    public void SetCurrentPuzzle(string puzzleId)
    {
        string currentLevel = SceneManager.GetActiveScene().name;
        var hintData = allHints.FirstOrDefault(h => h.levelName == currentLevel);

        if (hintData != null)
        {
            currentPuzzleHint = hintData.puzzles.FirstOrDefault(p => p.puzzleId == puzzleId);

            if (currentPuzzleHint != null)
            {
                if (!hintIndices.ContainsKey(puzzleId))
                    hintIndices[puzzleId] = 0;

                Debug.Log($"[HintManager] Hints loaded for puzzle: {puzzleId}");
            }
            else
            {
                Debug.LogWarning($"[HintManager] Puzzle ID '{puzzleId}' not found in level: {currentLevel}");
            }
        }
        else
        {
            Debug.LogWarning($"[HintManager] No HintData found for level: {currentLevel}");
        }
    }

    public bool HasMoreHints()
    {
        if (currentPuzzleHint == null) return false;

        string puzzleId = currentPuzzleHint.puzzleId;
        int index = hintIndices.ContainsKey(puzzleId) ? hintIndices[puzzleId] : 0;

        return index < currentPuzzleHint.hintSteps.Length;
    }
    public void ResetHints(string puzzleId)
    {
        if (hintIndices.ContainsKey(puzzleId))
            hintIndices[puzzleId] = 0;
    }

}
