using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCenter : MonoBehaviour
{
    [SerializeField]
    private List<Mover> mover = new List<Mover>();
    private int index = 0;
    GameObject playerPrefab;
    GameObject MonsterPrefab;
    private GameObject player;

    private void Awake()
    {
        //플레이어 생성
        playerPrefab = Resources.Load<GameObject>("Prefabs/Player");

        player = Instantiate<GameObject>(playerPrefab, new Vector3(Constants.MapSizeX - 1, 1.5f, Constants.MapSizeZ / 2), Quaternion.identity, transform);
        Player player1 = player.GetComponent<Player>();
        player1.Location= new Vector2(Constants.MapSizeX - 1, Constants.MapSizeZ / 2);
        
        
        mover.Add(player.GetComponent<Player>());
        Player p1 = player.GetComponent<Player>();
        p1.Location = new Vector2(Constants.MapSizeX - 1, Constants.MapSizeZ / 2);



        //몬스터 생성
        playerPrefab = Resources.Load<GameObject>("Prefabs/Enemy");

        for (int i = 1; i <= 1; i++)
        {
            GameObject enemy = Instantiate<GameObject>(playerPrefab, new Vector3(Constants.MapSizeX - 15, 1.5f, Constants.MapSizeZ / 2), Quaternion.identity, transform);
            mover.Add(enemy.GetComponent<Enemy>());
            mover[i].Location = new Vector2(Constants.MapSizeX - 8, Constants.MapSizeZ / 2);
            mover[i].GetComponent<Enemy>().Target = player.transform;
        }

        //아이템 생성
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
            //시작하고 바로 턴과 무브스택을 준다
            mover[index].MoverState = State.MOVE;
            mover[index].MoveStack = 2; 

            //객체에서 작업후에 턴 엔드를 한다 
            //이때 자기 자신의 myturn을 false로 하고
            //End함수를 true로 바꾼다


            yield return new WaitUntil(() => mover[index].MoverState == State.END);
            mover[index].MoverState = State.NONE;

            while(true)
            {
                index++;
                if (mover.Count <= index)
                {
                    index = 0;
                }

                if(mover[index].GetComponent<Player>() != null || mover[index].GetComponent<Enemy>() != null)
                {
                    break;
                }

                //만약 플레이어나 몬스터면 브레이크 아닐경우 다시 반복문
            }

            
        }
       
    }

    public void EndButton()
    {
       player.GetComponent<Player>(). MoverState = State.END;
    }

    public void AttackButton()
    {

    }
}



/*
게임매니저
무버클래스 리스트를 가지고 있다가 각자에게 순서대로 턴을 준다.
그 객체의 엔드 함수가 호출될 경우 턴을 다음 객체에게 준다.

맵 매니저
지정한 타일 프리팹을 맵에다 설치한다.
바닥은 타일로 하고 그 주위를 월로 돌려서 설치
맵의 좌 우에는 포탈을 설치한다.
안만들 가능성이 높음



무버클래스 (추상클래스)

MoveInterval

Location
-위치변수(x,y를 가진 POINT) //자기 자신의 포지션

Nearby 함수
-bool 근접체크(POINT 맵 포지션?, int _distance)
 키입력이 stay 상태나 away 상태일때 체크
 해당 턴을 움직이는 객체의 포지션을 맵의 포지션과 비교하여 
 경계(디스턴스) 이상 넘어오면 true를 반환

Move (업데이트 안에 이 함수를 넣고 , 이 함수 내에서 Nearby함수로 체크)
-POINT 무브(Direction 방향, 도착할 맵의 경계)
  플레이어가 방향키를 누르면 그쪽으로 이동하면서 근접체크 함수를 돌림
  true면 해당 방향의 땅으로 자동 이동시키고 false일 경우 순간이동을 시키던
  뒷걸음질을 시키던 원래 땅으로 다시 이동시킴


무버 클래스를 Player , Enemy, Weapon(item)이 상속받음




플레이어 클래스
Wapons:List<Weapon>
HitPoints:(int) (체력인듯?)

Attack(방향,랜덤)
공격시 무기의 사거리를 체크하여 그 범위 안에 몬스터가 있으면 몬스터의
hit함수를 호출한다.
Hit(맥스데미지, 랜덤)
Equip(String weaponName)
Move(방향)



에너미 클래스 (추상클래스 - 여러 에너미 추가)
HitPoints(int )

Move(랜덤)
평소엔 랜덤으로 이동하고 플레이어가 감지 범위 안으로 들어오면 추격함
이동 가능 거리에 플레이어가 있으면 그 앞까지 이동 한 후 플레이어의 hit
함수를 호출한다.
Hit(int maxDamage, 랜덤)




Weapon(item) (추상클래스)
pickedUp 플레이어 주웠는지 여부?
Location  바닥에 떨어진 위치

PickUpWeapon()  플레이어가 접근 시 장착하는 함수
DamageEnemy() 


데이터 매니저에 싱글톤 사용
static으로 씬이 넘어갈 때 변하지 않는 요소를 처리한다

씬 매니저를 만들고
씬을 여러개 쓰는게 아니라 씬 하나를 재시작 해서 사용한다.
이 때 스테이지에 따라 배치하는 요소들의 데이터 값을 데이터 매니저에서 가져옴

item 클래스를 만들고 use등의 가상함수를 만들고
equip 클래스와 Consumption(소비) 클래스로 파생시킨다.
item 리스트로 묶어서 사용


게임센터는 싱글톤사용하지 말라함 (이유모름)
 - 멤버로 플레이어 객체나 에너미 객체 등
 - 가지고 있는게 많아서 따로 재활용 할 수 없는 코드라 그럴수도 

 */