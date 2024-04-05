using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    private FSM manager;
    private Parameter parameter;
    
    private float timer;
    public IdleState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.animator.Play("Idle");
    }

   public void OnUpdate()
   {
        timer += Time.deltaTime;
        if(timer >= parameter.idleTime)
        {
            manager.TransitionState(StateType.Attack);
        }
   }

   public void OnExit()
   {
        timer = 0;//清零计时器
   }
}

public class AttackState : IState
{
    private FSM manager;
    private Parameter parameter;

    private AnimatorStateInfo info;//获取动画播放进度

    public AttackState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.animator.Play("Attack");
    }

   public void OnUpdate()
   {
        info = parameter.animator.GetCurrentAnimatorStateInfo(0); //实时获取动画状态 
        if(info.normalizedTime >= 0.95f) //动画播放完成
        {
            manager.TransitionState(StateType.Idle);
        }
   }

   public void OnExit()
   {

   }

   
}
public class ShootState : IState
{
    private FSM manager;
    private Parameter parameter;

    private AnimatorStateInfo info;//获取动画播放进度

    public ShootState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        
    }

   public void OnUpdate()
   {
        info = parameter.animator.GetCurrentAnimatorStateInfo(0); //实时获取动画状态 
        if(info.normalizedTime >= 0.95f) //动画播放完成
        {
            manager.TransitionState(StateType.FireBall);
        }
   }
   public void OnExit()
   {

   }

   
}