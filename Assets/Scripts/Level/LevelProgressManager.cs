using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProgressManager 
{
    public void Initialize(string[] levels)
    {
        foreach (var level in levels)
        {
            if (GetLevelStatus(level) == LevelStatus.Locked)
            {
                SetLevelStatus(level, LevelStatus.Locked);
            }
        }
    }

    public LevelStatus GetLevelStatus(string level)
    {
        return (LevelStatus)PlayerPrefs.GetInt(level, 0);
    }

    public void SetLevelStatus(string level, LevelStatus levelStatus)
    {
        PlayerPrefs.SetInt(level, (int)levelStatus);
        Debug.Log($"Setting Level: {level}, Status: {levelStatus}");
    }
}
