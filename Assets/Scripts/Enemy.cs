using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Mover, IlivingThingsSet
{
    [SerializeField]
    private int hitPoints;
    private Vector2 moveDir;
    private Transform target;
    private ATTACK_DIR attackDir;
    
    public int HitPoints { get { return hitPoints; } set { hitPoints = value; } }
    public Vector2 MoveDir { get { return moveDir; } set { moveDir = value; } }
    public Transform Target { get { return target; } set { target = value; } }

    enum ATTACK_DIR
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        NONE,
    }

    void Start()
    {
        attackDir = ATTACK_DIR.NONE;
        moveDir = Vector2.zero;
    }


    // Update is called once per frame
    void Update()
    {
        switch (MoverState)
        {
            case State.NONE:
                break;
            case State.MOVE:
                {
                    Move();
                }
                break;
            case State.ATTACK:
                {
                    //주변을 감지하고 만약에 공격할 수있는 오브젝트가 없으면 자동으로 턴 종료
                    //감지에 성공하면 플레이어에게 조작권을 주고 공격위치를 지정한 후
                    //공격 함수를 실행
                }
                break;
            case State.END:
                break;
            default:
                break;
        }
    }



    protected override void Move()
    {
        if (MoveStack == 0)
            MoverState = State.ATTACK;



        // 움직임 벡터 계산
        Vector3 movement = new Vector3(moveDir.x, 0f, moveDir.y);
        movement.Normalize();




        if (Vector3.Distance(transform.position,target.position) < 5f)
        {

        }





        if (Nearby())
        {
            Location = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.z));
            MoveStack--;
            //여기서 스택 하나 까고 만약 스택이 0이면 state를 attack으로 
        }
    }

    public void Hit(int _value)
    {
        hitPoints -= _value;
    }

}
