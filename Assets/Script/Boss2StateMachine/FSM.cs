using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//枚举所有状态
public enum StateType
{
    Intro,
    Idle,
    Attack,
    Shoot,
    FireRain,
    BeHit,
    Death
}

[Serializable]
public class  Parameter //管理boss的各个参数
{
    public int health;
    public float moveSpeed;
    //public float chaseSpeed;
    public float idleTime;
    public Transform target;
    // public Transform[] patrolPoints;
    // public Transform[] chasePoints;
    public Animator animator;
    public float angerValue;//怒气值
    public LayerMask targetLayer;
    public Transform attackPoint;
    public float AttackArea; 
    public bool getHit;
    
}
public class FSM : MonoBehaviour
{
    public Parameter parameter; //管理boss的各个参数
    private IState currentState;
    private Dictionary<StateType,IState> states = new Dictionary<StateType, IState>();
    // Start is called before the first frame update
    void Start()
    {
        states.Add(StateType.Intro,new IntroState(this));
        states.Add(StateType.Idle,new IdleState(this));//将自己的引用传给状态
        states.Add(StateType.Attack,new AttackState(this));//声明键值对
        states.Add(StateType.Shoot,new ShootState(this));
        states.Add(StateType.FireRain,new FireRainState(this));
        states.Add(StateType.BeHit,new BeHitState(this));
        states.Add(StateType.Death,new DeathState(this));
        
        TransitionState(StateType.Idle); //设置初始状态为idle

        parameter.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        currentState.OnUpdate();//持续运行当前函数的update
        if(Input.GetKeyDown(KeyCode.Return))
        {
            parameter.getHit = true;
        }
        
    }

    public void TransitionState(StateType type)
    {
        if(currentState != null)
            currentState.OnExit();
        currentState = states[type];
        currentState.OnEnter();
    }

    //写通用代码在此处
    //public void FlipToPlayer(Transform target){} 

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            parameter.target = other.transform;
        }
        Debug.Log("touch Player");
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            parameter.target = null;
        }
    }

    // private void OnDrawGizmos() { //绘制图像
    //     Gizmos.DrawWireSphere(parameter.attackPoint.position,parameter.AttackArea);
    // }
}
