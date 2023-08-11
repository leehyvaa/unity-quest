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
    private int moveInterval;
    [SerializeField]
    private POINT location;
    [SerializeField]
    private int moveStack;
    [SerializeField]
    private State moverState = State.NONE;


    public int MoveInterval { get { return moveInterval; } set { moveInterval = value; } }
    public POINT Location { get { return location; } set { location = value; } }
    public int MoveStack { get { return moveStack; } set { moveStack = value; } }
    public State MoverState { get { return moverState; } set { moverState = value; } }



    protected virtual bool Nearby()
    {
        return true;
    }
    protected abstract void Move();


}
