using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Types;

public class Monster2D : MonoBehaviour
{
    public delegate void Callback_MonsterShot(int id);
    Callback_MonsterShot monsterShot;

    float moveSpeed =1.5f;
    public float MoveSpeed { get { return moveSpeed; } set {  moveSpeed = value; } }

    [SerializeField] int curHP;

    public enum State { ALIVE,DIE}
    public State curState { get; protected set; }

    public int ID { get;private set; }

    Rigidbody2D rb;
    float shotTimer;

    // Start is called before the first frame update
    protected virtual void Start()
    {
     
        shotTimer = 4f;
        rb= GetComponent<Rigidbody2D>();
    }

    protected virtual void FixedUpdate()
    {
        rb.MovePosition(rb.transform.position + Vector3.down * Time.fixedDeltaTime * moveSpeed);

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        shotTimer += Time.deltaTime;
        if (shotTimer > 5f)
        {
            monsterShot?.Invoke(ID);
            shotTimer = 0f;
        }
    }

    public void SetData(int id,int maxHP)
    {
        ID = id;
        this.curHP = maxHP;
    }

    public void SetCallback(Callback_MonsterShot callback_monsterShot)
    {
        if(monsterShot != null)
        {
            Debug.Log("이미 설정된 callback_monsterShot");
            return;
        }
        monsterShot = callback_monsterShot;

    }

    public void Hurt(int amount)
    {
        curHP -= amount;
        if(curHP <= 0)
        {
            Destroy(gameObject);
        }

    }



    void ChangeState(State newState)
    {
        if (curState == newState) return;

        
        curState = newState;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Finish")
        {
            Destroy(gameObject);
        }
    }
}
