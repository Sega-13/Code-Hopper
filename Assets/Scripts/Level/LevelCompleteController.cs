using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelCompleteController : MonoBehaviour
{
    [SerializeField] private LevelComplete levelComplete;
    [SerializeField] private CollectManager collectManager;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            collectManager.SaveData();
            CompleteLevel();
        }
    }

    private void CompleteLevel()
    {
        LevelManager.Instance.MarkCurrentLevelComplete();
        levelComplete.gameObject.SetActive(true);

    }
}
