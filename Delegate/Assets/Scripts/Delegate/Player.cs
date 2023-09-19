using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public delegate void Callback_OnDied(int id);
    Callback_OnDied onDied;


    public int ID { get; private set; }
    public int MaxHp { get; private set; }
    public int CurHp { get; private set; }

    public enum State { ALIVE, DIE,}
    public State CurState { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetData(int id, int maxHP)
    {
        if (ID != 0)
        {
            Debug.LogWarning(string.Format("[Player ID : {0}] �̹� ������ SetData() - name : {1} ",ID,name));
            return;
        }

        ID = id;
        MaxHp = maxHP;
        CurHp = maxHP;
        CheckAlive();
    }

    public void SetCallback(Callback_OnDied callback_OnDied)
    {
        if(onDied != null)
        {
            Debug.LogWarning(string.Format("[Player ID : {0}] �̹� ������ SetCallback() - name : {1} ", ID, name));
            return;
        }
        onDied = callback_OnDied;
        //�������� ������ +�� �ְ� �ϳ��� ��Ī�Ҷ� =�� �ִ´�
        //onDied += Die;
    }


    void Die(int id)
    {
        Debug.Log("�׾��� ����");
    }

    public void Hurt(int amount)
    {
        CurHp -= amount;

        Debug.Log(string.Format ("[Player ID : {0}] Hurt ({1} ) - curHP:{2}" , ID, amount,CurHp));

        CheckAlive();
    }

    void CheckAlive()
    {
        if(CurHp <= 0)
        {
            CurHp = 0;
            //�׾��� !!
            ChangeState(State.DIE);
             
            
            onDied?.Invoke(ID); // if(onDied!=null) onDied(ID);
            
            return;
        }
        //��Ҵ� !!
        ChangeState(State.ALIVE);
   
    }

    void ChangeState(State newState)
    {
        if (CurState == newState) return;

        Debug.Log(string.Format("[Player ID : {0} Change State : {1}->{2}", ID, CurState, newState));
        CurState = newState;

    }
}
