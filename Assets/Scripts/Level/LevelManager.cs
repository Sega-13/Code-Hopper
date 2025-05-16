using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;
    public static LevelManager Instance { get { return instance; } }
    public string[] Levels;
    private event Action<string> LevelCompletedEvent;
    private LevelProgressManager levelProgressManager;
    private LevelTransitionManager levelTransitionManager;
    private List<ILevelObserver> observers = new List<ILevelObserver>();
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        levelProgressManager = new LevelProgressManager();
        levelTransitionManager = new LevelTransitionManager();

        levelProgressManager.Initialize(Levels);
    }
    
    private void Start()
    {
        if (GetLevelStatus(Levels[0]) == LevelStatus.Locked)
        {
            SetLevelStatus(Levels[0], LevelStatus.UnLocked);
        }
    }

    public void RegisterObserver(ILevelObserver observer)
    {
        if (!observers.Contains(observer))
        {
            observers.Add(observer);
        }
    }
    public void UnregisterObserver(ILevelObserver observer)
    {
        if (observers.Contains(observer))
        {
            observers.Remove(observer);
        }
    }
    public void NotifyObservers(string levelName)
    {
        var observersCopy = new List<ILevelObserver>(observers);
        foreach (var observer in observersCopy)
        {
            observer.OnLevelCompleted(levelName);
        }
    }
    public void MarkCurrentLevelComplete()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SetLevelStatus(currentScene.name, LevelStatus.Completed);
        //unlock next level

        int currentSceneIndex = Array.FindIndex(Levels, level => level == currentScene.name);
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex < Levels.Length)
        {
            SetLevelStatus(Levels[nextSceneIndex], LevelStatus.UnLocked);
        }
        NotifyObservers(currentScene.name);
    }

    public LevelStatus GetLevelStatus(string level)
    {
        LevelStatus levelStatus = (LevelStatus)PlayerPrefs.GetInt(level, 0);
        return levelStatus;
    }

    public void SetLevelStatus(string level, LevelStatus levelStatus)
    {
        PlayerPrefs.SetInt(level, (int)levelStatus);
        Debug.Log("Setting Level : " + level + "Status : " + levelStatus);
    }
    public void ReloadLevel()
    {
        levelTransitionManager.ReloadCurrentLevel();
    }
  
    public void Exit()
    {
        levelTransitionManager.ExitGame();
    }
    public void LoadNextScene()
    {
        levelTransitionManager.LoadNextScene();
    }
}
