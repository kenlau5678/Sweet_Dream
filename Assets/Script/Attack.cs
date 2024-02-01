using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //Attack
    public Transform attackPoint; // AttackPoint 物件
    float nextAttackTime = 0f;
    public float AttackRate = 2f;
    public float attackRange = 0.5f;
    public int attackDamage = 25;
    public LayerMask enemyLayers; //敌人layer
    //bool canmove = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Attack Interval
        if (Time.time >= nextAttackTime)
        {
            //canmove = true;
            if (Input.GetKeyDown(KeyCode.J))
            {
                Attack();
                //canmove = false;


                nextAttackTime = Time.time + 1f / AttackRate;
            }
        }
    }

    //Attack Function
    void Attack()
    {
        //animator.SetTrigger("Attack");//Trigger parameter of animater
        //在OverlapCircleAll中传递圆的位置、半径和层掩码参数，以获取所有与圆重叠的collider。
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(25);//enemy take damage
            Debug.Log("attack");
        }
    }

    // Gizmos 辅助线框 类
    private void OnDrawGizmos()
    {
        if (attackPoint == null)
        { return; }
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange); // 画AttackPoint 的辅助线
    }
}
