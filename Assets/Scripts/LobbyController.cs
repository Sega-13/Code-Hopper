using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyController : MonoBehaviour
{
    [SerializeField] private GameObject LevelSelectPanel;
    [SerializeField] private GameObject GameMenu;
    void Start()
    {
        
    }

    public void Play()
    {
        LevelSelectPanel.SetActive(true);
        GameMenu.SetActive(false);
    }
}
