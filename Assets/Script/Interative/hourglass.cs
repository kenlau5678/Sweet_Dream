using DG.Tweening; // DOTween������
using System.Collections;
using UnityEngine;

public class hourglass : MonoBehaviour
{
    // ָ�����
    public GameObject hourhand; // ʱ��
    public GameObject minutehand; // ����

    // ��ת���Ʊ�־
    public bool rotateFlag = true; // ����ʱ�����Ƿ���ת

    // ��ת�Ƕ�
    public float hourAngles; // ʱ����ת�Ƕ�
    public float minuteAngles; // ������ת�Ƕ�

    // ����������
    public Animator animator; // ����ɳ©������Animator���

    // �ڲ�״̬����
    private int isstay = 0; // �������Ƿ���ɳ©����������
    private float cooldownDuration = 1f; // ������ȴʱ�䣬��λΪ��
    private float lastInteractionTime = -Mathf.Infinity; // ��һ�ν�����ʱ�䣬��ʼ��Ϊ�������ʾ��δ���й�����

    // ����ҽ��봥������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") // ��鴥�����Ķ����Ƿ�Ϊ���
        {
            isstay = 1; // ����״̬Ϊ��������
        }
    }

    // ������뿪��������
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player") // ��鴥�����Ķ����Ƿ�Ϊ���
        {
            isstay = 0; // ����״̬Ϊ����������
        }
    }

    private void Update()
    {
        // �������������ڣ�������E�������Ҿ����ϴν���ʱ�䳬������ȴʱ��
        if (isstay == 1 && Input.GetKeyDown(KeyCode.E) && Time.time >= lastInteractionTime + cooldownDuration)
        {
            lastInteractionTime = Time.time; // �����ϴν���ʱ��Ϊ��ǰʱ��

            // ����ʱ��ͷ������תЭ��
            StartCoroutine(hourhand.GetComponent<HandRotation>().RotationCoroutine());
            StartCoroutine(minutehand.GetComponent<HandRotation>().RotationCoroutine());
            // �����������Ч��
            CameraShake.Instance.shakeCameraWithFrequency(1f, 2f, 1f);
            // ������ת��־�л�����״̬
            if (rotateFlag == true)
            {
                animator.SetTrigger("Rotate"); // ������ת����
                rotateFlag = false; // ������ת��־
            }
            else
            {
                animator.SetTrigger("Return"); // �������ض���
                rotateFlag = true; // ������ת��־
            }
        }
    }
}