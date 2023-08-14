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
    private int moveInterval;
    [SerializeField]
    private Vector2 location;
    [SerializeField]
    private int moveStack;
    [SerializeField]
    private State moverState = State.NONE;


    public int MoveInterval { get { return moveInterval; } set { moveInterval = value; } }
    public Vector2 Location { get { return location; } set { location = value; } }
    public int MoveStack { get { return moveStack; } set { moveStack = value; } }
    public State MoverState { get { return moverState; } set { moverState = value; } }



    protected virtual bool Nearby()
    {
        float dis = Vector3.Distance(transform.position, location);
        
        if(dis > 0.5f)
        {
            return true;
        }

        return false;
    }
    protected abstract void Move();


}
