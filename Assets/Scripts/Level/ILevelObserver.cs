using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILevelObserver
{
    void OnLevelCompleted(string levelName);
}
