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
        //무브하다가 nearby가 true가 나오면 이동턴을 하나 쓴것으로 간주
        //플레이어 로케이션을 새로 이동한 지점으로 옮긴다
        //조작하다가 키보드 away가 나온 경우 자신의 로케이션으로 순간이동시킨다.
        
        
        if(Nearby())
        {
            //여기서 스택 하나 까고 만약 스택이 0이면 state를 attack으로 
        }
    }

    private void Attack()
    {
        //서치한 해당 대상에게 데미지를 주고 자신의 State를 종료로 변경
    }

    
}
