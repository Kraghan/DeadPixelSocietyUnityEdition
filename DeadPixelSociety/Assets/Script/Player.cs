using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpriterDotNetUnity;

public class Player : MonoBehaviour {

    Rigidbody2D body;
    public float maxSpeed;
    public float runSpeed;
    public float axisSpeed;
    public float jumpHeight;
    private UnityAnimator animator;
    private bool toRight;
    private int contactCounter;
    private int jumpCounter;
    public bool animate;
    
    public PlayerStateMachine stateMachineMouvementV;
    public PlayerStateMachine stateMachineMouvementH;
    //public PlayerStateMachineAction stateMachineAction;

    // Use this for initialization
    void Start () {
        contactCounter = 0;
        jumpCounter = 0;
        toRight = true;
        animator = GetComponent<SpriterDotNetBehaviour>().Animator;
        body = GetComponent<Rigidbody2D>();
        stateMachineMouvementH.SStart();
        stateMachineMouvementH.SetPlayer(this);

        stateMachineMouvementV.SStart();
        stateMachineMouvementV.SetPlayer(this);
    }
	
	// Update is called once per frame
	void Update ()
    {
        stateMachineMouvementH.SUpdate();
        stateMachineMouvementV.SUpdate();
    }

    void FixedUpdate()
    {
        stateMachineMouvementH.SFixedUpdate();
        stateMachineMouvementV.SFixedUpdate();
    }

    public void IdleState()
    {
        stateMachineMouvementH.PushState(0);
    }

    public void WalkState()
    {
        stateMachineMouvementH.PushState(1);
    }

    public void RunState()
    {
        stateMachineMouvementH.PushState(2);
    }

    public void OnTheGroundState()
    {
        stateMachineMouvementV.PushState(0);
    }

    public void FallingState()
    {
        stateMachineMouvementV.PushState(1);
    }

    public void JumpState()
    {
        stateMachineMouvementV.PushState(2);
    }

    public void DoubleJumpState()
    {
        stateMachineMouvementV.PushState(3);
    }

    public void MoveToLeft(bool run = false)
    {
        if(toRight)
            transform.localScale = new Vector3(transform.localScale.x * -1.0f, transform.localScale.y, transform.localScale.z);
        toRight = false;
        body.velocity = new Vector2( - (run ? runSpeed : maxSpeed), body.velocity.y);
    }

    public void MoveToRight(bool run = false)
    {
        if (!toRight)
            transform.localScale = new Vector3(transform.localScale.x * -1.0f, transform.localScale.y, transform.localScale.z);
        toRight = true;
        body.velocity = new Vector2(run ? runSpeed : maxSpeed, body.velocity.y);
    }

    public void Jump(float variator)
    {
        jumpCounter++;
        if(body.velocity.y < 0.0f)
            body.AddForce(new Vector2(0.0f, (-body.velocity.y) + (jumpHeight * variator)));
        else
            body.AddForce(new Vector2(0.0f, (jumpHeight * variator)));
    }

    public Rigidbody2D GetBody()
    {
        return body;
    }

    public UnityAnimator GetAnimator()
    {
        if (animator == null)
            animator = GetComponent<SpriterDotNetBehaviour>().Animator;
        return animator;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        jumpCounter = 0;
        ++contactCounter;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        --contactCounter;
    }

    public bool IsOnTheGround()
    {
        return contactCounter > 0;
    }

    public int GetJumpCounter()
    {
        return jumpCounter;
    }

    public void RearmAnimation()
    {
        PlayerState state = (PlayerState)stateMachineMouvementH.CurrentState();
        state.StartAnimation();
    }

    public bool IsAnimated()
    {
        return animate;
    }

    public void D()
    {
        Debug.Log(contactCounter);
    }
}