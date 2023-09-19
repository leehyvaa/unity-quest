using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy3D : MonoBehaviour
{
    public GameObject target;
    public Transform player;
    public Transform coin;

    NavMeshAgent agent;

    Animator animator;

    Enemy3DState enemyState;

    // Start is called before the first frame update
    private void Awake()
    {

    }
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = target.transform.position;
        enemyState = Enemy3DState.Idle;
    }

    private void FixedUpdate()
    {
        switch (enemyState)
        {
            case Enemy3DState.Idle:
                {

                }
                break;
            case Enemy3DState.Chase:
                {

                }
                break;
            case Enemy3DState.Attack:
                {

                }
                break;

        }
    }

    // Update is called once per frame
    void Update()
    {
        switch(enemyState)
        {
            case Enemy3DState.Idle:
                {
                    if(coin != null)
                    {
                        SetState(Enemy3DState.Chase);
                    }
                }break;
            case Enemy3DState.Chase:
                {
                    float dis = Vector3.Distance(transform.position, player.position);
                    if (dis <= 5f)
                    {
                        target = player.gameObject;
                    }
                    else
                        target = coin.gameObject;

                    if(dis <= 3f)
                        SetState(Enemy3DState.Attack);
                }
                break;
            case Enemy3DState.Attack:
                {
                        SetState(Enemy3DState.Chase);

                }
                break;

        }

        agent.destination = target.transform.position;

        //animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    private void OnDrawGizmos()
    {
        if(agent == null)
        {
            return;
        }
        NavMeshPath path = agent.path;

        Gizmos.color = Color.blue;
        foreach(Vector3 corner in path.corners)
        {
            Gizmos.DrawSphere(corner, 0.4f);
        }

        Gizmos.color = Color.red;
        Vector3 pos = transform.position;

        foreach(Vector3 corner in path.corners)
        {
            Gizmos.DrawLine(pos,corner);
            pos = corner;
        }

    }
    private void SetState(Enemy3DState state)
    {
        enemyState = state;
        //animator.SetInteger("pState", (int)state);
    }

    enum Enemy3DState
    {
        Idle,
        Chase,
        Attack,
    }

}
