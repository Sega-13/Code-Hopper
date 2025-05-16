using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelComplete : MonoBehaviour, ILevelObserver
{
    [SerializeField] private Button buttonNext;
    [SerializeField] private Button buttonRestart;
    [SerializeField] private Button buttonExit;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI crystalText;
    [SerializeField] private CollectManager collectManager;

    private void OnEnable()
    {
        LevelManager.Instance.RegisterObserver(this);
    }

    private void OnDisable()
    {
        LevelManager.Instance.UnregisterObserver(this);
    }
    public void ExitGame()
    {
        LevelManager.Instance.Exit();

    }
    public void ReloadLevel()
    {
        LevelManager.Instance.ReloadLevel();
    }
    public void NextLevel()
    {
        LevelManager.Instance.LoadNextScene();
    }

    void ILevelObserver.OnLevelCompleted(string levelName)
    {
        if (coinText != null) coinText.text = collectManager.coinCount.ToString();
        if (crystalText != null) crystalText.text = collectManager.crystalCount.ToString();
    }
}
