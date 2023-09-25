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
        
        //true�� �ٷ� ���� ����ȴ� ���� false�� ���ΰ� ���� �Ŀ� true�� ����
        async.allowSceneActivation = false;

        while (async.progress < 0.9f)
        {
            

            //���൵�� 0.9���� ������ �����ϰ� ���ٰ�
            yield return true;
        }

        while(async.progress >= 0.9f)
        {
            yield return new WaitForSeconds(0.1f);
            
            //�ε������� �ּ� 2�ʵ��� ����ϴ� ����
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
