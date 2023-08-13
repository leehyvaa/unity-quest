using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : Mover,IlivingThingsSet
{
    [SerializeField]
    private int hitPoints;
    public Vector2 moveDir;

    public int HitPoints { get { return hitPoints; } set{ hitPoints = value; }}

    void Start()
    {
        moveDir = Vector2.zero;
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
      
        //�����ϴٰ� nearby�� true�� ������ �̵����� �ϳ� �������� ����
        //�÷��̾� �����̼��� ���� �̵��� �������� �ű��
        //�����ϴٰ� Ű���� away�� ���� ��� �ڽ��� �����̼����� �����̵���Ų��.


        // ������ ���� ���
        Vector3 movement = new Vector3(moveDir.x, 0f, moveDir.y);
        movement.Normalize();


        if (movement.x == 0 && movement.z == 0)
            return;
        if (movement.x < movement.z)
        {
            if(movement.z < -movement.x)
            {
                transform.Translate(new Vector3(-1,0,0) * 1 * Time.deltaTime);
            }
            else
            {
                transform.Translate(new Vector3(0, 0, 1) * 1 * Time.deltaTime);
            }
        }
        else
        {
            if (movement.z > -movement.x)
            {
                transform.Translate(new Vector3(1, 0, 0) * 1 * Time.deltaTime);
            }
            else
            {
                transform.Translate(new Vector3(0, 0, -1) * 1 * Time.deltaTime);
            }
        }



        if (Nearby())
        {
            //���⼭ ���� �ϳ� ��� ���� ������ 0�̸� state�� attack���� 
        }
    }

    private void Attack()
    {
        //��ġ�� �ش� ��󿡰� �������� �ְ� �ڽ��� State�� ����� ����
    }

    
}
