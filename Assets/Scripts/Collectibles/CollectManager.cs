using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class CollectManager : MonoBehaviour
{
    [SerializeField] private HintManager hintManager;
    [SerializeField] private TextMeshProUGUI feedbackText;
    [SerializeField] private HintUIController hintUIController;

  
    public TextMeshProUGUI crystalText;
    public TextMeshProUGUI coinText;

    public int coinCount { get; private set; }
    public int crystalCount { get; private set; }

    private void Awake()
    {
        if(PlayerPrefs.GetInt("Coins", coinCount) == 0)
        {
            coinCount = 10;
        }
        else
        {
            coinCount = PlayerPrefs.GetInt("Coins", coinCount);
        }
        if (PlayerPrefs.GetInt("Crystals", crystalCount) == 0)
        {
            crystalCount = 10;
        }
        else
        {
            crystalCount = PlayerPrefs.GetInt("Crystals", crystalCount);
        }
        
        
        UpdateUI();
       
    }

    private void Start()
    {
        LoadData();
        UpdateUI();
    }
    public void AddCoin(int amount)
    {
        coinCount += amount;
        Debug.Log("Coins: " + coinCount);
        UpdateUI();
    }

    public void AddCrystal(int amount)
    {
        crystalCount += amount;
        Debug.Log("Crystals: " + crystalCount);
        UpdateUI();
      
    }

    public void GrantReward(string rewardName)
    {
        Debug.Log("Reward Granted: " + rewardName);
       
    }
    public void RemoveCoin(int amount)
    {
        coinCount -= amount;
        UpdateUI();
    }
    public void RemoveCrystal(int amount)
    {
        crystalCount -= amount;
        UpdateUI();
    }
    public void UpdateUI()
    {
        if (coinText != null) coinText.text = "+" + coinCount;
        if (crystalText != null) crystalText.text = "+" + crystalCount;
    }
    public void SaveData()
    {
        PlayerPrefs.SetInt("Coins", coinCount);
        PlayerPrefs.SetInt("Crystals", crystalCount);
        PlayerPrefs.Save();
    }

    public void LoadData()
    {
        coinCount = PlayerPrefs.GetInt("Coins", coinCount);
        crystalCount = PlayerPrefs.GetInt("Crystals", crystalCount);
    }

    public void ShowHint()
    {
        if (!hintManager.HasMoreHints() && coinCount >= 10)
        {
            feedbackText.gameObject.SetActive(true);
            StartCoroutine(ShowFeedback("All hints used."));
            return;
        }
        if (coinCount >= 10)
        {
            feedbackText.gameObject.SetActive(true);
            bool shown = hintUIController.OnShowNextHint();
            if (shown)
            {
                coinCount -= 10; // Deduct coins per hint reveal
                UpdateUI();
            }
        }
        else
        {
            feedbackText.gameObject.SetActive(true);
            StartCoroutine(ShowFeedback("Not enough coins for hint."));
        }
    }
    IEnumerator ShowFeedback(string msg)
    {
        feedbackText.text = msg;
        feedbackText.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f); 

        feedbackText.gameObject.SetActive(false);
    }

    // Optional: Clear data for testing
    public void ResetData()
    {
        PlayerPrefs.DeleteAll();
        coinCount = 0;
        crystalCount = 0;
        UpdateUI();
    }
}
