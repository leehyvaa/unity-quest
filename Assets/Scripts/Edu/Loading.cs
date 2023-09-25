using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    AsyncOperation async;

    float delayTime = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadingNextScene(GameManager.Instance.nextSceneName));
    }

    // Update is called once per frame
    void Update()
    {
        DelayTime();
    }

    IEnumerator LoadingNextScene(string sceneName)
    {
        async = SceneManager.LoadSceneAsync(sceneName);
        
        //true면 바로 씬이 변경된다 따라서 false로 놔두고 연출 후에 true로 변경
        async.allowSceneActivation = false;

        while (async.progress < 0.9f)
        {
            

            //진행도가 0.9보다 작을땐 무한하게 돌다가
            yield return true;
        }

        while(async.progress >= 0.9f)
        {
            yield return new WaitForSeconds(0.1f);
            
            //로딩씬에서 최소 2초동안 대기하다 나감
            if(delayTime > 2.0f)
                break;
        }

        async.allowSceneActivation = true;
    }

    void DelayTime()
    {
        delayTime += Time.deltaTime;
    }
}
