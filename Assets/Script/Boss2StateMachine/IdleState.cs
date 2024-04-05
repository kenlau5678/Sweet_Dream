using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroState : IState
{
    private FSM manager;
    private Parameter parameter;

    private AnimatorStateInfo info;//获取动画播放进度

    public IntroState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.animator.Play("Intro");
    }

   public void OnUpdate()
   {
        
   }

   public void OnExit()
   {

   }

   
}
public class IdleState : IState
{
    private FSM manager;
    private Parameter parameter;
    
    private float timer;
    public IdleState(FSM manager)//在构造函数中获取状态机对象并且获取状态机属性
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
        if(parameter.getHit)//受击检测
        {
            manager.TransitionState(StateType.BeHit);
        }
        if(timer >= parameter.idleTime)
        {
            manager.TransitionState(StateType.Shoot);
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
        if(parameter.getHit)//受击检测
        {
            manager.TransitionState(StateType.BeHit);
        }
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
        if(parameter.getHit)//受击检测
        {
            manager.TransitionState(StateType.BeHit);
        }
        if(info.normalizedTime >= 0.95f) //动画播放完成
        {
            //manager.TransitionState(StateType.FireBall);
        }
   }
   public void OnExit()
   {

   }

   
}
public class FireRainState : IState
{
    private FSM manager;
    private Parameter parameter;

    private AnimatorStateInfo info;//获取动画播放进度

    public FireRainState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.animator.Play("FireRain");
    }

   public void OnUpdate()
   {
        info = parameter.animator.GetCurrentAnimatorStateInfo(0); //实时获取动画状态 
        if(parameter.getHit)//受击检测
        {
            manager.TransitionState(StateType.BeHit);
        }
        if(info.normalizedTime >= 0.95f) //动画播放完成
        {
            manager.TransitionState(StateType.Idle);
        }
   }

   public void OnExit()
   {

   }

   
}
public class BeHitState : IState
{
    private FSM manager;
    private Parameter parameter;
    
    private AnimatorStateInfo info;
    public BeHitState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.animator.Play("BeHit");
    }

   public void OnUpdate()
   {
       info = parameter.animator.GetCurrentAnimatorStateInfo(0);
       if(parameter.health <= 0)
       {
            manager.TransitionState(StateType.Death);
       }
       else{}
   }

   public void OnExit()
   {
        parameter.getHit = false;
   }
}
public class DeathState : IState
{
    private FSM manager;
    private Parameter parameter;
    
    public DeathState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.animator.Play("Dead");
    }

   public void OnUpdate()
   {
       
   }

   public void OnExit()
   {

   }
}