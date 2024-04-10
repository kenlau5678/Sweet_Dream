using DG.Tweening; // DOTween动画库
using System.Collections;
using UnityEngine;

public class hourglass : MonoBehaviour
{
    // 指针对象
    public GameObject hourhand; // 时针
    public GameObject minutehand; // 分针

    // 旋转控制标志
    public bool rotateFlag = true; // 控制时分针是否旋转

    // 旋转角度
    public float hourAngles; // 时针旋转角度
    public float minuteAngles; // 分针旋转角度

    // 动画控制器
    public Animator animator; // 控制沙漏动画的Animator组件

    // 内部状态变量
    private int isstay = 0; // 标记玩家是否在沙漏触发区域内
    private float cooldownDuration = 1f; // 交互冷却时间，单位为秒
    private float lastInteractionTime = -Mathf.Infinity; // 上一次交互的时间，初始化为负无穷，表示还未进行过交互

    // 当玩家进入触发区域
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") // 检查触发器的对象是否为玩家
        {
            isstay = 1; // 更新状态为在区域内
        }
    }

    // 当玩家离开触发区域
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player") // 检查触发器的对象是否为玩家
        {
            isstay = 0; // 更新状态为不在区域内
        }
    }

    private void Update()
    {
        // 如果玩家在区域内，按下了E键，并且距离上次交互时间超过了冷却时间
        if (isstay == 1 && Input.GetKeyDown(KeyCode.E) && Time.time >= lastInteractionTime + cooldownDuration)
        {
            lastInteractionTime = Time.time; // 更新上次交互时间为当前时间

            // 启动时针和分针的旋转协程
            StartCoroutine(hourhand.GetComponent<HandRotation>().RotationCoroutine());
            StartCoroutine(minutehand.GetComponent<HandRotation>().RotationCoroutine());
            // 触发相机抖动效果
            CameraShake.Instance.shakeCameraWithFrequency(1f, 2f, 1f);
            // 根据旋转标志切换动画状态
            if (rotateFlag == true)
            {
                animator.SetTrigger("Rotate"); // 触发旋转动画
                rotateFlag = false; // 更新旋转标志
            }
            else
            {
                animator.SetTrigger("Return"); // 触发返回动画
                rotateFlag = true; // 更新旋转标志
            }
        }
    }
}