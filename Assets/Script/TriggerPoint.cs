using UnityEngine;

public class TriggerPoint : MonoBehaviour
{
    public bool isTrigger;
    private Collider2D triggerCollider;
    public float triggerRange;
    private void Start()
    {
        // 获取触发器的碰撞体
        triggerCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        // 检测触发器范围内是否有玩家
        Collider2D[] colliders = Physics2D.OverlapBoxAll(triggerCollider.bounds.center, triggerCollider.bounds.size, 0f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.tag == "Player")
            {
                isTrigger = true;
                return; // 如果检测到玩家，就直接退出循环
            }
        }
        // 如果没有检测到玩家，将 isTrigger 设为 false
        isTrigger = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, triggerRange); // 画AttackPoint 的辅助线
    }
}
