using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpriterDotNetUnity;

public class Player : MonoBehaviour {

    Rigidbody2D body;
    public float maxSpeed;
    public float runSpeed;
    public float jumpHeight;
    private UnityAnimator animator;
    private bool toRight;
    private uint jumpCounter;
    private uint floorContactCounter;
    public bool animate;
    public double timeUntilMaxJumpPower;
    private bool canJump;
    private bool freeze;
    private Vector2 oldVelocity;
    private bool justEntered;


    public PlayerStateMachine stateMachineMouvementV;
    public PlayerStateMachine stateMachineMouvementH;
    //public PlayerStateMachineAction stateMachineAction;

    // Use this for initialization
    void Start ()
    {
        // Physics
        body = GetComponent<Rigidbody2D>();
        floorContactCounter = 0;
        freeze = false;
        // End physics

        // Graphisms
        toRight = true;
        animator = GetComponent<SpriterDotNetBehaviour>().Animator;
        // End graphisms

        // Gameplay
        jumpCounter = 0;
        canJump = true;
        stateMachineMouvementH.SStart();
        stateMachineMouvementH.SetPlayer(this);

        stateMachineMouvementV.SStart();
        stateMachineMouvementV.SetPlayer(this);
        // End gameplay
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!freeze)
        {
            stateMachineMouvementH.SUpdate();
            stateMachineMouvementV.SUpdate();
        }
        else
        {
            body.velocity = new Vector2(0, 0);
        }
    }

    void FixedUpdate()
    {
        if (!freeze)
        {
            stateMachineMouvementH.SFixedUpdate();
            stateMachineMouvementV.SFixedUpdate();
        }
        else
        {
            body.velocity = new Vector2(0, 0);
        }
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
        Debug.Log(body.velocity.y);
        if(toRight)
            transform.localScale = new Vector3(transform.localScale.x * -1.0f, transform.localScale.y, transform.localScale.z);
        toRight = false;
        //body.AddForce(new Vector2(-(run ? runSpeed - Mathf.Abs(body.velocity.x) : maxSpeed - Mathf.Abs(body.velocity.x)), 0), ForceMode2D.Impulse);
        body.velocity = new Vector2( - (run ? runSpeed : maxSpeed), body.velocity.y);
    }

    public void MoveToRight(bool run = false)
    {
        Debug.Log(body.velocity.y);
        if (!toRight)
            transform.localScale = new Vector3(transform.localScale.x * -1.0f, transform.localScale.y, transform.localScale.z);
        toRight = true;

        //if(body.velocity.x )
        //body.AddForce(new Vector2((run ? runSpeed - Mathf.Abs(body.velocity.x) : maxSpeed - Mathf.Abs(body.velocity.x)), 0), ForceMode2D.Impulse);
        body.velocity = new Vector2(run ? runSpeed : maxSpeed, body.velocity.y);
    }

    public void Jump()
    {
        jumpCounter++;
        if(body.velocity.y < 0.0f)
            body.AddForce(new Vector2(0.0f, (-body.velocity.y) + jumpHeight ));
        else
            body.AddForce(new Vector2(0.0f, jumpHeight));
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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        ++floorContactCounter;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        --floorContactCounter;
    }

    public bool IsOnTheGround()
    {
        return floorContactCounter > 0;
    }

    public uint GetJumpCounter()
    {
        return jumpCounter;
    }

    public void ResetJumpCounter()
    {
        jumpCounter = 0;
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

    public bool CanJump()
    {
        return canJump;
    }

    public void DisallowJump()
    {
        canJump = false;
    }

    public void AllowJump()
    {
        canJump = true;
    }

    public void Freeze()
    {
        freeze = true;
        oldVelocity = body.velocity;
    }

    public void WarmUp()
    {
        freeze = false;
        body.velocity = oldVelocity;
    }

    public bool IsFrozen()
    {
        return freeze;
    }

    public void SetJustEntered(bool value)
    {
        justEntered = value;
    }

    public bool JustEntered()
    {
        return justEntered;
    }

    public void D()
    {
        Debug.Log("");
    }
}