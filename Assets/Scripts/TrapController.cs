using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    [SerializeField] private GameObject CodeBlock;
    [SerializeField] private int puzzleIndex = 0;
    [SerializeField] private string puzzleId;
    [SerializeField] private HintManager hintManager;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            Debug.Log("Collided");
            CodeBlock.SetActive(true);
            CodePuzzleManager manager = FindObjectOfType<CodePuzzleManager>();
            if (manager != null )
            {
                manager.LoadPuzzle(puzzleIndex); // Call LoadPuzzle here
                hintManager.SetCurrentPuzzle(puzzleId);
                hintManager.ResetHints(puzzleId);
                puzzleIndex++;
                Debug.Log("@@@@@@@"+puzzleIndex);
            }

        }
    }
}
