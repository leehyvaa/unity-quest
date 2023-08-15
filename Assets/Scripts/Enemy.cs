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
                    //�ֺ��� �����ϰ� ���࿡ ������ ���ִ� ������Ʈ�� ������ �ڵ����� �� ����
                    //������ �����ϸ� �÷��̾�� ���۱��� �ְ� ������ġ�� ������ ��
                    //���� �Լ��� ����
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



        // ������ ���� ���
        Vector3 movement = new Vector3(moveDir.x, 0f, moveDir.y);
        movement.Normalize();




        if (Vector3.Distance(transform.position,target.position) < 5f)
        {

        }





        if (Nearby())
        {
            Location = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.z));
            MoveStack--;
            //���⼭ ���� �ϳ� ��� ���� ������ 0�̸� state�� attack���� 
        }
    }

    public void Hit(int _value)
    {
        hitPoints -= _value;
    }

}
