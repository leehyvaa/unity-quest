using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static FlappyBird;
using static Spawner2D;

public class Bullet2D : MonoBehaviour
{
    public delegate void Callback_Hit(Transform transform,int damage);
    Callback_Hit hit;

    float moveSpeed =5f;
    Vector2 moveDir;
    bool playerBullet = false;
    public int ID { get; private set; }

    Rigidbody2D rb;
    private int damage;

    float timer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        damage = 1;

        
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveDir * moveSpeed * Time.deltaTime);
       
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 7)
        {
            Destroy(gameObject);
        }
    }


    public void SetData(bool player,Vector2 dir,int damage)
    {
        playerBullet = player;
        moveDir = dir;
        this.damage = damage;

        if(!player)
        {
            moveSpeed = 3f;
        }
        else
        {
            transform.GetComponentInChildren<SpriteRenderer>().color = Color.cyan;

        }
    }

    public void SetCallback(Callback_Hit callback_hit)
    {
        if (hit != null)
        {
            Debug.Log("이미 설정된 callback_bullet");
            return;
        }
        hit = callback_hit;
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        

        if (other.gameObject.tag == "Player" && !playerBullet)
        {
            Debug.Log("Player hit");
            hit?.Invoke(other.transform, damage);
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "Monster" && playerBullet)
        {
            Debug.Log("Mon hit");
            hit?.Invoke(other.transform, damage);
            Destroy(gameObject);
        }
    }


}
