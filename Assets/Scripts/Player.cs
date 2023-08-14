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
                    //�ֺ��� �����ϰ� ���࿡ ������ ���ִ� ������Ʈ�� ������ �ڵ����� �� ����
                    //������ �����ϸ� �÷��̾�� ���۱��� �ְ� ������ġ�� ������ ��
                    //���� �Լ��� ����
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
            

        // ������ ���� ���
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
            //���⼭ ���� �ϳ� ��� ���� ������ 0�̸� state�� attack���� 
        }
    }

    private void Attack()
    {
        //��ġ�� �ش� ��󿡰� �������� �ְ� �ڽ��� State�� ����� ����

    }

    
    public void TurnEnd()
    {
        MoverState = State.END;
    }
}
