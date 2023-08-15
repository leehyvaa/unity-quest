using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct POINT
{
    public int x;
    public int y;
}

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


    public float MoveInterval { get { return moveInterval; } set { moveInterval = value; } }
    public Vector2 Location { get { return location; } set { location = value; } }
    public int MoveStack { get { return moveStack; } set { moveStack = value; } }
    public State MoverState { get { return moverState; } set { moverState = value; } }



    protected virtual bool Nearby()
    {
        if(Vector2.Distance(location, new Vector2(transform.position.x,transform.position.z)) >moveInterval/2)
        {
            return true;
        }

        return false;
    }
    protected abstract void Move();


}
