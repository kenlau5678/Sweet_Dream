using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//枚举所有状态
public enum StateType
{
    Idle,
    Attack,
    Shoot,
    FireBall,
    BeHit,
    Death
}

[Serializable]
public class  Parameter
{
    public int health;
    public float moveSpeed;
    public float idleTime;
    public Transform target;
    public Animator animator;
    
}
public class FSM : MonoBehaviour
{
    public Parameter parameter;
    private IState currentState;
    private Dictionary<StateType,IState> states = new Dictionary<StateType, IState>();
    // Start is called before the first frame update
    void Start()
    {
        states.Add(StateType.Idle,new IdleState(this));//将自己的引用传给状态
        states.Add(StateType.Attack,new AttackState(this));
        states.Add(StateType.Shoot,new ShootState(this));
        
        TransitionState(StateType.Idle); //设置初始状态为idle

        parameter.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        currentState.OnUpdate();//持续运行当前函数的update
    }

    public void TransitionState(StateType type)
    {
        if(currentState != null)
            currentState.OnExit();
        currentState = states[type];
        currentState.OnEnter();
    }

    //public void FlipToPlayer(Transform target){}
}
