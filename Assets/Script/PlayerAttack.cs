using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //Attack
    public Transform attackPoint; // AttackPoint ���
    public Animator animator;
    float nextAttackTime = 0f;
    public float AttackRate = 2f;
    public float attackRange = 0.5f;
    public int attackDamage = 25;
    public LayerMask enemyLayers; //����layer
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
        animator.SetTrigger("Attack");//Trigger parameter of animater
        //��OverlapCircleAll�д���Բ��λ�á��뾶�Ͳ�����������Ի�ȡ������Բ�ص���collider��
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(25);//enemy take damage
            Debug.Log("attack");
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Enemy"))
        {
            //enemy hit animation;
        }
    }
    // Gizmos �����߿� ��
    private void OnDrawGizmos()
    {
        if (attackPoint == null)
        { return; }
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange); // ��AttackPoint �ĸ�����
    }
}
