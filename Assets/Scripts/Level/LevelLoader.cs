using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
   // public static LevelLoader Instance;
    private Button button;
    public string levelName;

    private void Awake()
    {
      /*  if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);*/
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
               // SoundManager.Instance.Play(Sounds.ButtonClick);
                SceneManager.LoadScene(levelName);
               // SoundManager.Instance.Play(Sounds.LevelStart);
                break;
            case LevelStatus.Completed:
              //  SoundManager.Instance.Play(Sounds.ButtonClick);
                SceneManager.LoadScene(levelName);
               // SoundManager.Instance.Play(Sounds.LevelStart);
                break;

        }
       // SceneManager.LoadScene(levelName);
    }
   
}
