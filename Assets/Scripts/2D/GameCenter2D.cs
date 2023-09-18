using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCenter2D : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject[] monsterPrefabs;
    [SerializeField] GameObject spawnerPrefab;
    [SerializeField] GameObject monBulletPrefab;
    [SerializeField] GameObject playerBulletPrefab;
    [SerializeField] GameObject explosionPrefab;



    [SerializeField] Transform playerRootTrans;
    [SerializeField] Transform spawnerRootTrans;
    [SerializeField] Transform BulletRootTrans;



    Player2D player;
    int playerMaxHP = 1;

 
    Spawner2D spawner;
    public List<Monster2D> monsters = new List<Monster2D>();
    List<Bullet2D> monBullets = new List<Bullet2D>();
    List<Bullet2D> playerBullets = new List<Bullet2D>();



    // Start is called before the first frame update
    void Start()
    {
        CreatePlayer();
        CreateSpawner();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void CreateSpawner()
    {
        GameObject obj = Instantiate(spawnerPrefab, spawnerRootTrans);
        obj.name = "Spawner";
        obj.transform.position = spawnerRootTrans.position;
        obj.transform.rotation = spawnerRootTrans.rotation;

        spawner = obj.GetComponent<Spawner2D>();
        spawner.SetCallback(Create_Monster);

    }


    void Create_Monster(int prefabID)
    {



        GameObject obj = Instantiate(monsterPrefabs[prefabID], spawner.transform.GetChild(2).position, spawner.transform.rotation, spawner.transform);

        Monster2D monster = obj.GetComponent<Monster2D>();
        monsters.Add(monster);
        monster.SetData(monsters.Count-1, 1);
        monster.SetCallback(MonsterShot);

        Debug.Log(monsters.Count - 1);
    }

    void MonsterShot(int monId)
    {
        //for (int i = 0; monBullets.Count > i; i++)
        //{
        //    if (monBullets[i] != null)
        //        monBullets.Remove(monBullets[i]);
        //}
        GameObject obj = Instantiate(monBulletPrefab, monsters[monId].transform.position, monsters[monId].transform.rotation, BulletRootTrans.transform);

        Bullet2D bullet = obj.GetComponent<Bullet2D>();
        monBullets.Add(bullet);

        Vector2 dir = player.transform.position - monsters[monId].transform.position;
        bullet.SetData(false,dir.normalized,1);
        bullet.SetCallback(PlayerHit);
    }




    void CreatePlayer()
    {

        GameObject pl = Instantiate(playerPrefab, playerRootTrans);
        pl.name = "player";
        pl.transform.position = playerRootTrans.position;
        pl.transform.rotation = playerRootTrans.rotation;

        player = pl.GetComponent<Player2D>();
        player.SetData(playerMaxHP);
        player.SetCallback(OnDied_Player);
        player.SetCallback_Shot(PlayerShot);

    }

    void PlayerShot()
    {
        //for (int i = 0; playerBullets.Count> i; i++)
        //{
        //    if (playerBullets[i] != null)
        //        playerBullets.Remove(playerBullets[i]);
        //}

        GameObject obj = Instantiate(monBulletPrefab, player.transform.position, player.transform.rotation, BulletRootTrans.transform);

        Bullet2D bullet = obj.GetComponent<Bullet2D>();
        playerBullets.Add(bullet);

        Vector2 dir = Vector2.up;
        bullet.SetData(true,dir.normalized,1); //damage�� �÷��̾���ݷ�
        bullet.SetCallback(MonsterHit);

    }

    void OnDied_Player()
    {
        //��������
    }

    void PlayerHit(Transform my,int damage)
    {
        player.Hurt(damage);
        Instantiate(explosionPrefab, player.transform.position, player.transform.rotation);

    }

    void MonsterHit(Transform trans,int damage)
    {
        Monster2D mon = trans.GetComponent<Monster2D>();

        for (int i = 0; i < monsters.Count; i++)
        {
            if (monsters[i].ID == mon.ID)
            {
                monsters[i].Hurt(damage);
                Instantiate(explosionPrefab, monsters[i].transform.position, monsters[i].transform.rotation);
                //monsters.RemoveAt(i);
                //���� ����Ʈ���� ��������� ������ �׷� ��� id������ �ϴ� int �ϳ��� �����ϰ�
                //���� ������ int�� ++��Ű�鼭 id�� �ο��ϰ�
                //�Ѿ˻������ for���� �����鼭 id�� �´� ���͸� ã�� �Ѿ��� �����ؾ���
                //��ųʸ��� ���� ���� ���ҰͰ���
            }

        }
    }


}
