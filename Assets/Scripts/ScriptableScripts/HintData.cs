using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewHint", menuName = "Hints/ProgressiveHintData")]
public class HintData : ScriptableObject
{
    public string levelName;       // Must match scene name
    public PuzzleHint[] puzzles;
   // [TextArea] public string[] hintSteps; // Progressive hints
}
[Serializable]
public class PuzzleHint
{
    public string puzzleId;
    [TextArea]
    public string[] hintSteps;
}
