using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCenter : MonoBehaviour
{
    [SerializeField]
    private List<Mover> mover = new List<Mover>();
    private int index = 0;
    GameObject playerPrefab;
    GameObject MonsterPrefab;
    private GameObject player;

    private void Awake()
    {
        //플레이어 생성
        playerPrefab = Resources.Load<GameObject>("Prefabs/Player");

        player = Instantiate<GameObject>(playerPrefab, new Vector3(Constants.MapSizeX - 1, 1.5f, Constants.MapSizeZ / 2), Quaternion.identity, transform);
        mover.Add(player.GetComponent<Player>());
        Player p1 = player.GetComponent<Player>();
        p1.Location = new Vector2(Constants.MapSizeX - 1, Constants.MapSizeZ / 2);



        //몬스터 생성
        playerPrefab = Resources.Load<GameObject>("Prefabs/Enemy");

        for (int i = 1; i <= 1; i++)
        {
            GameObject enemy = Instantiate<GameObject>(playerPrefab, new Vector3(Constants.MapSizeX - 15, 1.5f, Constants.MapSizeZ / 2), Quaternion.identity, transform);
            mover.Add(enemy.GetComponent<Enemy>());
            mover[i].Location = new Vector2(Constants.MapSizeX - 8, Constants.MapSizeZ / 2);
            mover[i].GetComponent<Enemy>().Target = player.transform;
        }

        //아이템 생성
    }

    // Start is called before the first frame update
    void Start()
    {
       


        StartCoroutine(Turn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Turn()
    {
        while(true)
        {
            //시작하고 바로 턴과 무브스택을 준다
            mover[index].MoverState = State.MOVE;
            mover[index].MoveStack = 2; 

            //객체에서 작업후에 턴 엔드를 한다 
            //이때 자기 자신의 myturn을 false로 하고
            //End함수를 true로 바꾼다


            yield return new WaitUntil(() => mover[index].MoverState == State.END);
            mover[index].MoverState = State.NONE;

            while(true)
            {
                index++;
                if (mover.Count <= index)
                {
                    index = 0;
                }

                if(mover[index].GetComponent<Player>() != null || mover[index].GetComponent<Enemy>() != null)
                {
                    break;
                }

                //만약 플레이어나 몬스터면 브레이크 아닐경우 다시 반복문
            }

            
        }
       
    }

    public void EndButton()
    {
       player.GetComponent<Player>(). MoverState = State.END;
    }

    public void AttackButton()
    {

    }
}

