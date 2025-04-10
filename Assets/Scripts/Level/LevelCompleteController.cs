using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelCompleteController : MonoBehaviour
{
    [SerializeField] private LevelComplete levelComplete;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            CompleteLevel();
        }
    }

    private void CompleteLevel()
    {
        levelComplete.gameObject.SetActive(true);

    }
}
