using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬전환에 필요

public class GameManager : MonoBehaviour
{
    private static GameManager sInstance;

    public static GameManager Instance
    {
        get
        {
            if (sInstance == null)
            {
                GameObject newGameObject = new GameObject("_GameManager");
                sInstance = newGameObject.AddComponent<GameManager>();
            }
            return sInstance;
        }
    }


    public int changeScene = 0;
    //PlayerInfo같은 구조체를 만들어서 이에 대한 정보를 넣어놓을 수 있음


    private void Awake()
    {
        if (sInstance != null && sInstance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        sInstance = this;

        DontDestroyOnLoad(this.gameObject);
    }


    public void ChangeScene()
    {
        int sceneIndex = changeScene++ % 2;
        //string sceneName = string.Format("Scene_{0:2d}", scene);
        SceneManager.LoadScene(sceneIndex);
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);

    }

    // : 
    public string nextSceneName;
}
