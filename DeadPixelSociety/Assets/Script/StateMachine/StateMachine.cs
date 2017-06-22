using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour {

    private Stack<State> stack;

    // The first state will be the initial state
    public State[] StatesPatern;

	// Use this for initialization
	public void SStart ()
    {
        stack = new Stack<State>();
        for (int i = 0; i < StatesPatern.GetLength(0); ++i)
        {
            StatesPatern[i].SStart();
        }
        stack.Push(StatesPatern[0]);
	}
	
	// Update is called once per frame
	public void SUpdate ()
    {
        stack.Peek().SUpdate();
	}

    public void SFixedUpdate()
    {
        stack.Peek().SFixedUpdate();
    }

    public void PushState(int indexState)
    {
        stack.Push(StatesPatern[indexState]);
        stack.Peek().OnEnter();
    }

    public State PopState()
    {
        stack.Peek().OnExit();
        return stack.Pop();
    }

    public State CurrentState()
    {
        return stack.Peek();
    }
}
