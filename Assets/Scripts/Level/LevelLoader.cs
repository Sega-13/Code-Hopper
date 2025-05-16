using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    private Button button;
    public string levelName;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }
    public void OnClick()
    {
        LevelStatus levelStatus = LevelManager.Instance.GetLevelStatus(levelName);
        switch (levelStatus)
        {
            case LevelStatus.Locked:
                Debug.Log("Can't Play this level till you unlock it");
                break;
            case LevelStatus.UnLocked:
                SceneManager.LoadScene(levelName);
                break;
            case LevelStatus.Completed:
                SceneManager.LoadScene(levelName);
                break;

        }
    }
   
}
