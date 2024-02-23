using UnityEngine;

public class TriggerPoint : MonoBehaviour
{
    public bool isTrigger;
    private Collider2D triggerCollider;
    public float triggerRange;
    private void Start()
    {
        // ��ȡ����������ײ��
        triggerCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        // ��ⴥ������Χ���Ƿ������
        Collider2D[] colliders = Physics2D.OverlapBoxAll(triggerCollider.bounds.center, triggerCollider.bounds.size, 0f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.tag == "Player")
            {
                isTrigger = true;
                return; // �����⵽��ң���ֱ���˳�ѭ��
            }
        }
        // ���û�м�⵽��ң��� isTrigger ��Ϊ false
        isTrigger = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, triggerRange); // ��AttackPoint �ĸ�����
    }
}
