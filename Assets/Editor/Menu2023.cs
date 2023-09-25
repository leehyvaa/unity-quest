using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; // editor
using UnityEditor.SceneManagement; //Scene 

public class Menu2023 : MonoBehaviour
{
    [MenuItem("Menu2023/Clear PlayerPrefs")]
    private static void Clear_PlayerPrefsAll()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Clear_PlayerPrefsAll");
    }


    [MenuItem("Menu2023/SubMenu/Select")]
    private static void SubMenu_Select()
    {
        Debug.Log("Sub Menu 1 - Select");
    }


    /*
    % -Ctrl
    # - Shift
    & - Alt
     */


    [MenuItem("Menu2023/SubMenu/HotKey Test 1 %#[")]
    private static void SubMenu_HotKey_1()
    {
        Debug.Log("HotKey Test : Ctrl + Shift + [");
    }


    //���Ͽ� �����ʸ��콺 �������� ������
    [MenuItem("Assets/Load Selected Scene")]
    private static void LoadSelectedScene()
    {
        var selected = Selection.activeObject;

        //��Ÿ���϶��� true �ƴҋ��� else�� ���� ����
        if(EditorApplication.isPlaying)
        {
            EditorSceneManager.LoadScene(AssetDatabase.GetAssetPath(selected));
        }
        else
            EditorSceneManager.OpenScene(AssetDatabase.GetAssetPath(selected));
    }
}
