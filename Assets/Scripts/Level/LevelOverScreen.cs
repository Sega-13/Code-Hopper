using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class LevelOverScreen : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button quitButton;

    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI crystalText;
    [SerializeField] private CollectManager collectManager;

    private void Start()
    {
        if (coinText != null) coinText.text = collectManager.coinCount.ToString();
        if (crystalText != null) crystalText.text = collectManager.crystalCount.ToString();
    }
    public void Restart()
    {
        if (restartButton != null)
        {
            LevelManager.Instance.ReloadLevel();
        }
    }
    public void ExitGame()
    {
        LevelManager.Instance.Exit();
    }
}
