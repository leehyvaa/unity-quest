using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSM : StateMachine
{
    public float speed = 4f;
    public float jumpForce = 14f;
    public Rigidbody rigidbody;
    public SpriteRenderer spriteRenderer;

    public Transform cameraArm;
    public Transform characterBody;

    [HideInInspector]
    public PlayerIdle idleState;
    [HideInInspector]
    public PlayerMoving movingState;
    [HideInInspector]
    public PlayerJumping jumpingState;

    private void Awake()
    {
        idleState = new PlayerIdle(this);
        movingState = new PlayerMoving(this);
        jumpingState = new PlayerJumping(this);
    }

    protected override BaseState GetInitialState()
    {
        return idleState;
    }
}
