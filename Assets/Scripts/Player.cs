using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : Mover,IlivingThingsSet
{
    [SerializeField]
    private int hitPoints;
    private Vector2 moveDir;
    private ATTACK_DIR attackDir;

    public int HitPoints { get { return hitPoints; } set{ hitPoints = value; }}
    public Vector2 MoveDir { get { return moveDir; } set { moveDir = value; }}

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
        MoveStack = 2;
        MoveInterval = 1;
        transform.rotation = Quaternion.Euler(0f, 180f, 0f);

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
                Debug.Log("end");
                break;
            default:
                break;
        }


        PlayerDirection();
    }


    public void Hit(int _value)
    {
        hitPoints -= _value;
    }



    protected override void Move()
    {
        
      
        //무브하다가 nearby가 true가 나오면 이동턴을 하나 쓴것으로 간주
        //플레이어 로케이션을 새로 이동한 지점으로 옮긴다
        //조작하다가 키보드 away가 나온 경우 자신의 로케이션으로 순간이동시킨다.


        // 움직임 벡터 계산
        Vector3 movement = new Vector3(moveDir.x, 0f, moveDir.y);
        movement.Normalize();


        if (movement.x == 0 && movement.z == 0)
        {
            transform.position = new Vector3(Location.x, 1.5f, Location.y);
            return;
        }

        if (MoveStack <= 0)
            MoverState = State.ATTACK;


        if (movement.x < movement.z)
        {
            if(movement.z < -movement.x)
            {
                transform.Translate(new Vector3(-1,0,0) * 1 * Time.deltaTime, Space.World);
                attackDir = ATTACK_DIR.LEFT;
            }
            else
            {
                transform.Translate(new Vector3(0, 0, 1) * 1 * Time.deltaTime,Space.World);
                attackDir = ATTACK_DIR.UP;
            }

        }
        else
        {
            if (movement.z > -movement.x)
            {
                transform.Translate(new Vector3(1, 0, 0) * 1 * Time.deltaTime, Space.World);
                attackDir = ATTACK_DIR.RIGHT;
            }
            else
            {
                transform.Translate(new Vector3(0, 0, -1) * 1 * Time.deltaTime, Space.World);
                attackDir = ATTACK_DIR.DOWN;
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
        transform.position = new Vector3(Location.x, 1.5f, Location.y);
        //서치한 해당 대상에게 데미지를 주고 자신의 State를 종료로 변경

        Vector3 movement = new Vector3(moveDir.x, 0f, moveDir.y);
        movement.Normalize();


        if (movement.x == 0 && movement.z == 0)
        {
            return;
        }

        if (movement.x < movement.z)
        {
            if (movement.z < -movement.x)
                attackDir = ATTACK_DIR.LEFT;
            else
                attackDir = ATTACK_DIR.UP;
        }
        else
        {
            if (movement.z > -movement.x)
                attackDir = ATTACK_DIR.RIGHT;
            else
                attackDir = ATTACK_DIR.DOWN;
        }
    }

    private void PlayerDirection()
    {
        switch (attackDir)
        {
            case ATTACK_DIR.UP:
                transform.rotation = Quaternion.Euler(0f, 270f, 0f);
                break;
            case ATTACK_DIR.DOWN:
                transform.rotation = Quaternion.Euler(0f, 90f, 0f);
                break;
            case ATTACK_DIR.LEFT:
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                break;
            case ATTACK_DIR.RIGHT:
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                break;
            case ATTACK_DIR.NONE:
                break;
            default:
                break;
        }
    }

    
}
