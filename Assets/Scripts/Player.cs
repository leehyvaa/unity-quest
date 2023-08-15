using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : Mover,IlivingThingsSet
{
    [SerializeField]
    private int hitPoints;
    public Vector2 moveDir;
    private AttackDir attackDir;

    public int HitPoints { get { return hitPoints; } set{ hitPoints = value; }}

    enum AttackDir
    {
        Up,
        Down,
        Left,
        Right,
        NONE,
    }

    void Start()
    {
        moveDir = Vector2.zero;
        HitPoints = 5;
        attackDir = AttackDir.NONE;
    }


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
                    Attack();
                }
                break;
            case State.END:
                break;
            default:
                break;
        }

   
  
    }


    public void Hit(int _value)
    {
        hitPoints -= _value;
    }



    protected override void Move()
    {

        if (MoveStack <= 0)
            MoverState = State.ATTACK;
            

        // 움직임 벡터 계산
        Vector3 movement = new Vector3(moveDir.x, 0f, moveDir.y);
        movement.Normalize();


        if (movement.x == 0 && movement.z == 0)
        {
            transform.position = new Vector3(Location.x, 1.5f, Location.y);
            return;
        }


        if (movement.x < movement.z)
        {
            if(movement.z < -movement.x)
            {
                transform.Translate(new Vector3(-1,0,0) * 1 * Time.deltaTime);
                attackDir = AttackDir.Left;
            }
            else
            {
                transform.Translate(new Vector3(0, 0, 1) * 1 * Time.deltaTime);
                attackDir = AttackDir.Up;
            }
        }
        else
        {
            if (movement.z > -movement.x)
            {
                transform.Translate(new Vector3(1, 0, 0) * 1 * Time.deltaTime);
                attackDir = AttackDir.Right;
            }
            else
            {
                transform.Translate(new Vector3(0, 0, -1) * 1 * Time.deltaTime);
                attackDir = AttackDir.Down;
            }
        }


        

        if (Nearby())
        {
            Location = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.z));
            MoveStack--;
            //여기서 스택 하나 까고 만약 스택이 0이면 state를 attack으로 
        }
    }

    private void Attack()
    {
        //서치한 해당 대상에게 데미지를 주고 자신의 State를 종료로 변경

    }

    
    public void TurnEnd()
    {
        MoverState = State.END;
    }
}
