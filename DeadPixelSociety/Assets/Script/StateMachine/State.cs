using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
    
    Do not overload Monofunction ! Use Sfunction !

  */

public abstract class State : MonoBehaviour{

    // Use this for initialization
    abstract public void SStart();

    // Update is called once per frame
    abstract public void SUpdate();

    abstract public void SFixedUpdate();

    abstract public void OnEnter();

    abstract public void OnExit();
}
