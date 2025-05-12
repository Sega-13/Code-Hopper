using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCodePuzzle", menuName = "Code Puzzle")]
public class CodePuzzle : ScriptableObject
{
    public string puzzleName;

    [TextArea(5, 15)]
    public string codeWithBug; // This is what appears in the InputField

    [TextArea(5, 15)]
    public string expectedOutput; // Optional: use to check if user fixes it correctly

    //public string description; // Optional: UI display or hint
    public int difficulty; // Use thi
}
