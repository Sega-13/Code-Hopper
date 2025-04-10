using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader Instance;
    private Button button;
    public string levelName;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }
    public void OnClick()
    {
        SceneManager.LoadScene(levelName);
    }
    public void ReloadLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
    public void ExitLevel()
    {
        SceneManager.LoadScene(0);
        Application.Quit();
        Debug.Log("Game Exited");
    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
