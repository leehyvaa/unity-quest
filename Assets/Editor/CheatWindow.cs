using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class CheatWindow : EditorWindow
{
    string[] cheatList = new string[]
    {
        "ġƮ",           // �ε��� 0
        "��� ����",      // 1
        "����Ʈ ����",    // 2
        
    };

    static int selectIndex = 0;

    int getInt = 0;
    string getString = "";

    [MenuItem("Menu2023/CheatMenu/ġƮ ���â", false, 0)]
    static public void OpenCheatWindow()
    {
        //�ڿ� false ���� �ɼǵ��� ���̶���Ʈ�� �ֳ� ������ �״� �߿����� ����
        CheatWindow getWindow = EditorWindow.GetWindow<CheatWindow>(false, "Cheat Window", true); ; ;
    }


    private void OnGUI()
    {
        GUILayout.Space(10.0f);

        int getIndex = EditorGUILayout.Popup(selectIndex, cheatList,GUILayout.MaxWidth(200.0f));
        
        if(selectIndex != getIndex)
            selectIndex = getIndex;

        string cheatText = "";

        //Begin�� ������ �׻� end�� ¦���� �پ�ٴ�
        GUILayout.BeginHorizontal(GUILayout.MaxWidth(300.0f));
        {    //:cheat key
            if (selectIndex == 0)
            {
                GUILayout.Label("ġƮŰ �Է�", GUILayout.Width(70.0f));
                getString = EditorGUILayout.TextField(getString,GUILayout.Width(100.0f));
                cheatText = string.Format("ġƮŰ : {0}", getString);
            }
            else if (selectIndex == 1)
            {
                GUILayout.Label("���", GUILayout.Width(70.0f));
                getString = EditorGUILayout.TextField(getInt.ToString(), GUILayout.Width(100.0f));
                int.TryParse(getString, out getInt);
                cheatText = string.Format("ġƮŰ : {0}", getInt);
            }
            else if (selectIndex == 2)
            {
                GUILayout.Label("����Ʈ", GUILayout.Width(70.0f));
                getString = EditorGUILayout.TextField(getInt.ToString(), GUILayout.Width(100.0f));
                int.TryParse(getString, out getInt);
                cheatText = string.Format("����Ʈ : {0}", getInt);
            }
        }
        GUILayout.EndHorizontal();


        GUILayout.Space(20.0f);
        GUILayout.BeginHorizontal(GUILayout.MaxWidth(800.0f));
        {
            GUILayout.BeginVertical(GUILayout.MaxWidth(300.0f));
            {
                GUILayout.BeginHorizontal(GUILayout.MaxWidth(300.0f));
                {
                    //ĭ ������ �η��� \n ����� ���ص� ����
                    if (GUILayout.Button("\n����\n", GUILayout.Width(100.0f)))
                    {
                        //������ ��Ÿ�����̰�, �������� Title�� �ƴ� ��
                        if(EditorApplication.isPlaying &&
                            EditorSceneManager.GetActiveScene().name != "Title")
                        {
                            getInt = 0;
                            getString = "";
                            // : ���� �ۿ�
                            Debug.Log(cheatText);
                        }
                    }
                }
                GUILayout.EndHorizontal();


                GUILayout.BeginHorizontal(GUILayout.MaxWidth(300.0f));
                {
                    if(GUILayout.Button("\n��׶���\n����\n",GUILayout.Width(100.0f)))
                    {
                        //��Ÿ���߿� ��Ŀ���� ���ο� ���� ������ ������
                        Application.runInBackground = true;
                    }
                    if (GUILayout.Button("\n��׶���\n���� ����\n", GUILayout.Width(100.0f)))
                    {
                        //false �� �� ��Ŀ���� ������ ���α׷��� ����
                        Application.runInBackground = false;
                    }
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndVertical();
        }
        GUILayout.EndHorizontal();
    }
}
