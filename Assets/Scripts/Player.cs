using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mover,IlivingThingsSet
{
    [SerializeField]
    private int hitPoints;
    

    public int HitPoints { get { return hitPoints; } set{ hitPoints = value; }}

    void Start()
    {

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
        
        
        if(Nearby())
        {
            //���⼭ ���� �ϳ� ��� ���� ������ 0�̸� state�� attack���� 
        }
    }

    private void Attack()
    {
        //��ġ�� �ش� ��󿡰� �������� �ְ� �ڽ��� State�� ����� ����
    }

    
}
