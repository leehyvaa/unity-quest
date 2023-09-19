using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameCenter : MonoBehaviour
{
    [Header("프리팹")]
    [SerializeField]
    GameObject playerPrefab;

    [SerializeField]
    GameObject monsterPrefab;


    [Header("플레이어 정보")]
    [SerializeField]
    int[] playerMaxHPs;
    [SerializeField]
    Transform playerRootTrans;


    [Header("몬스터 정보")]
    [SerializeField]
    int[] monsterMaxHPs;
    [SerializeField]
    Transform monsterRootTrans;





    Player[] players;
    Monster[] Monsters;

    Dictionary<int, int> playerIdDic = new Dictionary<int, int>();

    // Start is called before the first frame update
    void Start()
    {
        CreatePlayers(playerMaxHPs.Length);
    }

    // Update is called once per frame
    void Update()
    {
//#if _DEBUG
        TEST_UpdateInputKey();
//#endif
    }

    void CreatePlayers(int count)
    {
        Debug.Log("[GAMECENTER Create Players : #"+  count);
        players = new Player[count];
        for (int i = 0; i < count; i++)
        {

            int id = i + 1;


            players[i] = CreatePlayer(id, playerMaxHPs[i]);
            playerIdDic.Add(id, i);
        }
    }


    Player CreatePlayer(int id, int maxHP)
    {
        GameObject gb = Instantiate(playerPrefab, playerRootTrans);
        gb.name = "player_" + id;
        gb.transform.position = playerRootTrans.position;
        gb.transform.rotation = playerRootTrans.rotation;

        Player player = gb.GetComponent<Player>();
        player.SetData(id,maxHP);
        player.SetData(id, maxHP);//경고예시
        player.SetCallback(OnDied_Player);

;


        Debug.Log("[GAMECENTER Create Players ID " + id);
        return player;

    }


    void AttackPlayer(int playerID, int amount)
    {
        int index = playerIdDic[playerID];
        players[index].Hurt(amount);
    }

    //공격을 당하거나 죽임을 당하거나 같은 이벤트들은 On으로 시작한는 함수로
    void OnDied_Player(int playerID)
    {
        //플레이어는 센터를 모르지만 이 함수로 인해 센터가 플레이어가 죽은걸 알게됨
        Debug.Log("[GAMECENTER] OnDied_Player : " + playerID);
        CheckGameEnd();
    }

    void CheckGameEnd()
    {
        Debug.Log("게임 종료 여부 판단");
    }


    void TEST_UpdateInputKey()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("[GAMECENTER] TEST_UpdateInputKey : " + KeyCode.Space);
            AttackPlayer(1, 10);
        }
    }
}


