using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.Universal;

public class MovePlatform : MonoBehaviour
{
    public Vector3 moveDirection = Vector3.right; // 移动方向
    public float moveDistance = 5f; // 移动距离
    public float moveDuration = 2f; // 移动时间
    private bool isPlayerNearby = false; // 玩家是否靠近
    private bool hasMoved = false; // 平台是否已经移动
    public GameObject MoveObject; // 要移动的对象
    public GameObject delObject;
    public float lightIntensityOnEnter = 4f; // Desired intensity when the player enters the trigger zone
    public float lightIntensityOnExit = 0.5f;  // Desired intensity when the player exits the trigger zone
    public float lightChangeDuration = 1f;   // Duration of the intensity change

    // Start is called before the first frame update
    void Start()
    {
        // 初始化时不需要立即执行任何动作
    }

    // Update is called once per frame
    void Update()
    {
        // 如果玩家靠近并且按下了E键，且平台尚未移动，则移动平台
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E) && !hasMoved)
        {
            hasMoved = true; // 确保平台只移动一次

            // 计算目标位置
            Vector3 targetPosition = MoveObject.transform.position + moveDirection.normalized * moveDistance;

            // 使用 DOTween 移动到目标位置
            MoveObject.transform.DOMove(targetPosition, moveDuration).SetEase(Ease.InOutQuad);
            if(delObject) delObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 当玩家进入触发区域时，设置isPlayerNearby为true
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
        // 当玩家离开触发区域时，设置isPlayerNearby为false
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
