using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewHint", menuName = "Hints/ProgressiveHintData")]
public class HintData : ScriptableObject
{
    public string levelName;       
    public PuzzleHint[] puzzles;
}
[Serializable]
public class PuzzleHint
{
    public string puzzleId;
    [TextArea]
    public string[] hintSteps;
}
