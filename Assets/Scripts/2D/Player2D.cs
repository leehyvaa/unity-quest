using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player2D : MonoBehaviour
{
    public delegate void Callback_OnDied();
    Callback_OnDied onDied;

    public delegate void Callback_PlayerShot();
    Callback_PlayerShot playerShot;



    float moveSpeed = 4f;

    Rigidbody2D rb;
    Vector2 input;

    int maxHP;
    int curHP;

    public Image imgHPBar;

    public enum State { ALIVE,DIE}
    public State curState { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        ShowHPBar(50);
    }

    void ShowHPBar(int hp)
    {
        imgHPBar.fillAmount =(float)hp/ (float)100 ;
    }

    private void FixedUpdate()
    {

        Vector2 moveVec = rb.position + input.normalized * Time.fixedDeltaTime * moveSpeed;
        rb.MovePosition(moveVec);

    }


    // Update is called once per frame
    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            playerShot?.Invoke();
        }
    }

    public void SetData(int maxHP)
    {
        this.maxHP = maxHP;
        curHP = maxHP;
    }

    public void SetCallback(Callback_OnDied callback_OnDied)
    {
        if(onDied != null)
        {
            Debug.Log("이미 설정된 callback_onDie");
            return;
        }

        onDied = callback_OnDied;
    }

    public void SetCallback_Shot(Callback_PlayerShot callback_Shot)
    {
        if (playerShot != null)
        {
            Debug.Log("이미 설정된 callback_playerShot");
            return;
        }

        playerShot = callback_Shot;
    }

    public void Hurt(int amount)
    {
        curHP -= amount;
        CheckAlive();

    }

    void CheckAlive()
    {
        if(curHP <= 0)
        {
            curHP = 0;
            ChangeState(State.DIE);

            onDied?.Invoke();
            Destroy(gameObject);

            return;
        }
        ChangeState(State.ALIVE);
    }

    void ChangeState(State newState)
    {
        if (curState == newState) return;

        curState = newState;
    }

}
