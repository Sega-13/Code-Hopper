using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HintUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hintTextUI;
    [SerializeField] private GameObject hintPanel;
    [SerializeField] private HintManager hintManager;

    private void Start()
    {
        hintManager.InitializeHintIndicesForCurrentLevel();
    }

    public bool OnShowNextHint()
    {
        bool hintAvailable = hintManager.TryGetNextHint(out string hint);

        hintTextUI.text = hintAvailable ? hint : "No more hints!";
        hintPanel.SetActive(true); // Show regardless of hint availability

        return hintAvailable;
    }

    public void OnCloseHintPanel()
    {
        hintTextUI.gameObject.SetActive(false);
        hintPanel.SetActive(false);
    }
}
