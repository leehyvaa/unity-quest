using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    NONE,
    MOVE,
    ATTACK,
    END,
}


public abstract class Mover : MonoBehaviour
{
    [SerializeField]
    private float moveInterval;
    [SerializeField]
    private Vector2 location;
    [SerializeField]
    private int moveStack;
    [SerializeField]
    private State moverState = State.NONE;


<<<<<<< HEAD
    public float MoveInterval { get { return moveInterval; } set { moveInterval = value; } }
=======
    public int MoveInterval { get { return moveInterval; } set { moveInterval = value; } }
>>>>>>> 744b0c7b9661f15db7299219304918d713d7edbd
    public Vector2 Location { get { return location; } set { location = value; } }
    public int MoveStack { get { return moveStack; } set { moveStack = value; } }
    public State MoverState { get { return moverState; } set { moverState = value; } }



    protected virtual bool Nearby()
    {
<<<<<<< HEAD
        if(Vector2.Distance(location, new Vector2(transform.position.x,transform.position.z)) >moveInterval/2)
=======
        float dis = Vector3.Distance(transform.position, location);
        
        if(dis > 0.5f)
>>>>>>> 744b0c7b9661f15db7299219304918d713d7edbd
        {
            return true;
        }

        return false;
    }
    protected abstract void Move();


}
