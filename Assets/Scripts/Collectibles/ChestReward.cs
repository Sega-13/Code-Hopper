using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChestReward : MonoBehaviour
{
    [Header("Chest Sprites")]
    public Image chestImage;
    public Sprite closedChestSprite;
    public Sprite openChestSprite;
    public CollectManager collectManager;

    [Header("Reward Display")]
    public GameObject rewardPanel;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI crystalsText;

    [Header("Reward Ranges")]
    public int minCoins = 10;
    public int maxCoins = 50;
    public int minCrystals = 1;
    public int maxCrystals = 5;

    private bool isOpened = false;
    private float displayDuration = 3f;

    public void OnChestTapped()
    {
        if (isOpened) return;

        isOpened = true;
        chestImage.gameObject.SetActive(false);

        // Change chest sprite
        chestImage.sprite = openChestSprite;

        // Generate random rewards
        int coins = Random.Range(minCoins, maxCoins + 1);
        int crystals = Random.Range(minCrystals, maxCrystals + 1);

        //Save
        collectManager.AddCoin(coins);
        collectManager.AddCrystal(crystals);
        collectManager.SaveData();
        collectManager.UpdateUI();

        // Update UI
        coinsText.text = "+" + coins.ToString();
        crystalsText.text = "+" + crystals.ToString();

        // Show reward panel
        rewardPanel.SetActive(true);
        Coroutine display = StartCoroutine(ShowAndHide(rewardPanel));

        // TODO: Add coins/crystals to player's inventory system
    }
    IEnumerator ShowAndHide(GameObject rewardPanel)
    {
        yield return new WaitForSeconds(displayDuration);
        rewardPanel.SetActive(false);
    }
}
