using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour //这是一个怪物AI巡逻的script
{
    public float patrolRadius = 5f;
    public float patrolTime = 3f;

    public NavMeshAgent navMeshAgent;
    private Vector3 startPosition;
    private bool isPatrolling;
    private float timer;

/*    void Start()
    {
        navMeshAgent.enabled = true; // 激活NavMeshAgent
        navMeshAgent = GetComponent<NavMeshAgent>();
        startPosition = transform.position;
        isPatrolling = true;
        timer = patrolTime;

        // 设置巡逻目标点
        SetRandomPatrolDestination();
    }

    void Update()
    {
        if (isPatrolling)
        {
            Patrol();
        }
    }

    void Patrol()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            // 倒计时结束，重新设置巡逻目标点
            SetRandomPatrolDestination();
            timer = patrolTime;
        }

        // 检测是否接近巡逻目标点
        if (Vector3.Distance(transform.position, navMeshAgent.destination) < 0.1f)
        {
            // 到达巡逻目标点，重新设置巡逻目标点
            SetRandomPatrolDestination();
        }
    }

    void SetRandomPatrolDestination()
    {
        // 随机生成巡逻目标点在patrolRadius半径内
        Vector2 randomPoint = Random.insideUnitCircle * patrolRadius;
        Vector3 patrolDestination = startPosition + new Vector3(randomPoint.x, 0f, randomPoint.y);
        
        // 设置NavMeshAgent的目标点
        navMeshAgent.SetDestination(patrolDestination);
    }
*/
}
