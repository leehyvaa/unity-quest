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
                    //�ֺ��� �����ϰ� ���࿡ ������ ���ִ� ������Ʈ�� ������ �ڵ����� �� ����
                    //������ �����ϸ� �÷��̾�� ���۱��� �ְ� ������ġ�� ������ ��
                    //���� �Լ��� ����
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
        
      
        //�����ϴٰ� nearby�� true�� ������ �̵����� �ϳ� �������� ����
        //�÷��̾� �����̼��� ���� �̵��� �������� �ű��
        //�����ϴٰ� Ű���� away�� ���� ��� �ڽ��� �����̼����� �����̵���Ų��.


        // ������ ���� ���
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
            
            //���⼭ ���� �ϳ� ��� ���� ������ 0�̸� state�� attack���� 
        }
    }

    private void Attack()
    {
        transform.position = new Vector3(Location.x, 1.5f, Location.y);
        //��ġ�� �ش� ��󿡰� �������� �ְ� �ڽ��� State�� ����� ����

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
