using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelComplete : MonoBehaviour
{
    [SerializeField] private Button buttonNext;
    [SerializeField] private Button buttonRestart;
    [SerializeField] private Button buttonExit;

    /*private void Awake()
    {
        buttonNext.onClick.AddListener(NextLevel);
        buttonRestart.onClick.AddListener(ReloadLevel);
    }*/
    public void ExitGame()
    {
        LevelLoader.Instance.ExitLevel();

    }
    public void ReloadLevel()
    {
        LevelLoader.Instance.ReloadLevel();
    }
    public void NextLevel()
    {
        LevelLoader.Instance.LoadNextScene();
    }
}
