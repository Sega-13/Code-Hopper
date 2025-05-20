using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelCompleteController : MonoBehaviour,ILevelObserver
{
    [SerializeField] private LevelComplete levelComplete;
    [SerializeField] private CollectManager collectManager;

    private void OnEnable()
    {
        LevelManager.Instance.RegisterObserver(this);
    }

    private void OnDisable()
    {
        LevelManager.Instance.UnregisterObserver(this);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            LevelManager.Instance.MarkCurrentLevelComplete();
        }
    }

    private void CompleteLevel()
    {
        LevelManager.Instance.MarkCurrentLevelComplete();
        levelComplete.gameObject.SetActive(true);

    }

    void ILevelObserver.OnLevelCompleted(string levelName)
    {
        collectManager.SaveData();
        levelComplete.gameObject.SetActive(true);
        levelComplete.UpdateUI();
    }
}
