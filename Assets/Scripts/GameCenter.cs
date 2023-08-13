using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCenter : MonoBehaviour
{
    private List<Mover> mover = new List<Mover>();
    private int index = 0;
    GameObject playerPrefab;
    private GameObject player;

    private void Awake()
    {
        //�÷��̾� ����
        playerPrefab = Resources.Load<GameObject>("Prefabs/Player");

        player = Instantiate<GameObject>(playerPrefab, new Vector3(Constants.MapSizeX - 1, 1.5f, Constants.MapSizeZ / 2), Quaternion.identity, transform);
        mover.Add(player.GetComponent<Player>());

        //���� ����
        //������ ����
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
            //�����ϰ� �ٷ� �ϰ� ���꽺���� �ش�
            mover[index].MoverState = State.MOVE;
            mover[index].MoveStack = 1; 

            //��ü���� �۾��Ŀ� �� ���带 �Ѵ� 
            //�̶� �ڱ� �ڽ��� myturn�� false�� �ϰ�
            //End�Լ��� true�� �ٲ۴�


            yield return new WaitUntil(() => mover[index].MoverState == State.END);
            mover[index].MoverState = State.NONE;

            while(true)
            {
                index++;
                if (mover.Count <= index)
                {
                    index = 0;
                }

                //���� �÷��̾ ���͸� �극��ũ �ƴҰ�� �ٽ� �ݺ���
            }

            
        }
       
    }
}

