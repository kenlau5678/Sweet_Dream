using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.Universal;

public class MovePlatform : MonoBehaviour
{
    public Vector3 moveDirection = Vector3.right; // �ƶ�����
    public float moveDistance = 5f; // �ƶ�����
    public float moveDuration = 2f; // �ƶ�ʱ��
    private bool isPlayerNearby = false; // ����Ƿ񿿽�
    private bool hasMoved = false; // ƽ̨�Ƿ��Ѿ��ƶ�
    public GameObject MoveObject; // Ҫ�ƶ��Ķ���
    public GameObject delObject;
    public float lightIntensityOnEnter = 4f; // Desired intensity when the player enters the trigger zone
    public float lightIntensityOnExit = 0.5f;  // Desired intensity when the player exits the trigger zone
    public float lightChangeDuration = 1f;   // Duration of the intensity change

    // Start is called before the first frame update
    void Start()
    {
        // ��ʼ��ʱ����Ҫ����ִ���κζ���
    }

    // Update is called once per frame
    void Update()
    {
        // �����ҿ������Ұ�����E������ƽ̨��δ�ƶ������ƶ�ƽ̨
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E) && !hasMoved)
        {
            hasMoved = true; // ȷ��ƽֻ̨�ƶ�һ��

            // ����Ŀ��λ��
            Vector3 targetPosition = MoveObject.transform.position + moveDirection.normalized * moveDistance;

            // ʹ�� DOTween �ƶ���Ŀ��λ��
            MoveObject.transform.DOMove(targetPosition, moveDuration).SetEase(Ease.InOutQuad);
            if(delObject) delObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ����ҽ��봥������ʱ������isPlayerNearbyΪtrue
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = true;
            var light2D = GetComponent<Light2D>();
            if (light2D != null)
            {
                DOTween.To(() => light2D.intensity, x => light2D.intensity = x, lightIntensityOnEnter, lightChangeDuration);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // ������뿪��������ʱ������isPlayerNearbyΪfalse
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = false;
            var light2D = GetComponent<Light2D>();
            if (light2D != null)
            {
                DOTween.To(() => light2D.intensity, x => light2D.intensity = x, lightIntensityOnExit, lightChangeDuration);
            }
        }
    }
}
