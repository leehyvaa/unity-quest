using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner2D : MonoBehaviour
{
    public delegate void Callback_CreateMonster(int id);
    Callback_CreateMonster createMonster;

    [SerializeField]Transform leftPos;
    [SerializeField] Transform rightPos;
    Transform spawner;

    bool moveDir;
    float timer;
    int monsterCount1;

    GameObject[] monsterPrefabs;


    // Start is called before the first frame update
    void Start()
    {
        leftPos = transform.GetChild(0);
        rightPos = transform.GetChild(1);
        spawner = transform.GetChild(2);
        moveDir = true;

        timer = 0;
        monsterCount1 = 0;
    }

    private void FixedUpdate()
    {

        if (moveDir)
        {
            spawner.position = spawner.position + Vector3.right * Time.deltaTime * 8f;
            if (spawner.position.x > rightPos.position.x)
            {
                moveDir = false;
            }
        }
        else
        {
            spawner.position = spawner.position + Vector3.left * Time.deltaTime * 8f;
            if (spawner.position.x < leftPos.position.x)
            {
                moveDir = true;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 1.2f)
        {
            SpawnMonster();
            timer = 0;
        }
    }

    void SpawnMonster()
    {
        if(monsterCount1 < 10)
        {
            createMonster?.Invoke(0);

        }
        else
        {
            createMonster?.Invoke(1);

            monsterCount1 = 0;
        }
    }


    public void SetCallback(Callback_CreateMonster callback_createMonster)
    {
        if(createMonster != null)
        {
            Debug.Log("이미 설정된 callback_createMonster");
                return;
        }
        createMonster = callback_createMonster;
    }
    
}
