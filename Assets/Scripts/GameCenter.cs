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
        //�÷��̾� ����
        playerPrefab = Resources.Load<GameObject>("Prefabs/Player");

        player = Instantiate<GameObject>(playerPrefab, new Vector3(Constants.MapSizeX - 1, 1.5f, Constants.MapSizeZ / 2), Quaternion.identity, transform);
        Player player1 = player.GetComponent<Player>();
        player1.Location= new Vector2(Constants.MapSizeX - 1, Constants.MapSizeZ / 2);
        
        
        mover.Add(player.GetComponent<Player>());
        Player p1 = player.GetComponent<Player>();
        p1.Location = new Vector2(Constants.MapSizeX - 1, Constants.MapSizeZ / 2);



        //���� ����
        playerPrefab = Resources.Load<GameObject>("Prefabs/Enemy");

        for (int i = 1; i <= 1; i++)
        {
            GameObject enemy = Instantiate<GameObject>(playerPrefab, new Vector3(Constants.MapSizeX - 15, 1.5f, Constants.MapSizeZ / 2), Quaternion.identity, transform);
            mover.Add(enemy.GetComponent<Enemy>());
            mover[i].Location = new Vector2(Constants.MapSizeX - 8, Constants.MapSizeZ / 2);
            mover[i].GetComponent<Enemy>().Target = player.transform;
        }

        //������ ����
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
            //�����ϰ� �ٷ� �ϰ� ���꽺���� �ش�
            mover[index].MoverState = State.MOVE;
            mover[index].MoveStack = 2; 

            //��ü���� �۾��Ŀ� �� ���带 �Ѵ� 
            //�̶� �ڱ� �ڽ��� myturn�� false�� �ϰ�
            //End�Լ��� true�� �ٲ۴�


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

                //���� �÷��̾ ���͸� �극��ũ �ƴҰ�� �ٽ� �ݺ���
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
���ӸŴ���
����Ŭ���� ����Ʈ�� ������ �ִٰ� ���ڿ��� ������� ���� �ش�.
�� ��ü�� ���� �Լ��� ȣ��� ��� ���� ���� ��ü���� �ش�.

�� �Ŵ���
������ Ÿ�� �������� �ʿ��� ��ġ�Ѵ�.
�ٴ��� Ÿ�Ϸ� �ϰ� �� ������ ���� ������ ��ġ
���� �� �쿡�� ��Ż�� ��ġ�Ѵ�.
�ȸ��� ���ɼ��� ����



����Ŭ���� (�߻�Ŭ����)

MoveInterval

Location
-��ġ����(x,y�� ���� POINT) //�ڱ� �ڽ��� ������

Nearby �Լ�
-bool ����üũ(POINT �� ������?, int _distance)
 Ű�Է��� stay ���³� away �����϶� üũ
 �ش� ���� �����̴� ��ü�� �������� ���� �����ǰ� ���Ͽ� 
 ���(���Ͻ�) �̻� �Ѿ���� true�� ��ȯ

Move (������Ʈ �ȿ� �� �Լ��� �ְ� , �� �Լ� ������ Nearby�Լ��� üũ)
-POINT ����(Direction ����, ������ ���� ���)
  �÷��̾ ����Ű�� ������ �������� �̵��ϸ鼭 ����üũ �Լ��� ����
  true�� �ش� ������ ������ �ڵ� �̵���Ű�� false�� ��� �����̵��� ��Ű��
  �ް������� ��Ű�� ���� ������ �ٽ� �̵���Ŵ


���� Ŭ������ Player , Enemy, Weapon(item)�� ��ӹ���




�÷��̾� Ŭ����
Wapons:List<Weapon>
HitPoints:(int) (ü���ε�?)

Attack(����,����)
���ݽ� ������ ��Ÿ��� üũ�Ͽ� �� ���� �ȿ� ���Ͱ� ������ ������
hit�Լ��� ȣ���Ѵ�.
Hit(�ƽ�������, ����)
Equip(String weaponName)
Move(����)



���ʹ� Ŭ���� (�߻�Ŭ���� - ���� ���ʹ� �߰�)
HitPoints(int )

Move(����)
��ҿ� �������� �̵��ϰ� �÷��̾ ���� ���� ������ ������ �߰���
�̵� ���� �Ÿ��� �÷��̾ ������ �� �ձ��� �̵� �� �� �÷��̾��� hit
�Լ��� ȣ���Ѵ�.
Hit(int maxDamage, ����)




Weapon(item) (�߻�Ŭ����)
pickedUp �÷��̾� �ֿ����� ����?
Location  �ٴڿ� ������ ��ġ

PickUpWeapon()  �÷��̾ ���� �� �����ϴ� �Լ�
DamageEnemy() 


������ �Ŵ����� �̱��� ���
static���� ���� �Ѿ �� ������ �ʴ� ��Ҹ� ó���Ѵ�

�� �Ŵ����� �����
���� ������ ���°� �ƴ϶� �� �ϳ��� ����� �ؼ� ����Ѵ�.
�� �� ���������� ���� ��ġ�ϴ� ��ҵ��� ������ ���� ������ �Ŵ������� ������

item Ŭ������ ����� use���� �����Լ��� �����
equip Ŭ������ Consumption(�Һ�) Ŭ������ �Ļ���Ų��.
item ����Ʈ�� ��� ���


���Ӽ��ʹ� �̱��������� ������ (������)
 - ����� �÷��̾� ��ü�� ���ʹ� ��ü ��
 - ������ �ִ°� ���Ƽ� ���� ��Ȱ�� �� �� ���� �ڵ�� �׷����� 

 */