using DG.Tweening;
using System.Collections;
using UnityEngine;

public class hourglass : MonoBehaviour
{
    public GameObject hourhand;
    public GameObject minutehand;
    public bool rotateFlag = true;
    public float hourAngles;
    public float minuteAngles;
    public Animator animator;
    private int isstay = 0;
    private float cooldownDuration = 1f; // Duration of cooldown in seconds
    private float lastInteractionTime = -Mathf.Infinity; // Track the last interaction time

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isstay = 1;
            Debug.Log(isstay);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isstay = 0;
            Debug.Log(isstay);
        }
    }

    private void Update()
    {
        if (isstay == 1 && Input.GetKeyDown(KeyCode.E) && Time.time >= lastInteractionTime + cooldownDuration)
        {
            lastInteractionTime = Time.time; // Update the last interaction time

            Debug.Log("OK");
            StartCoroutine(hourhand.GetComponent<HandRotation>().RotationCoroutine());
            StartCoroutine(minutehand.GetComponent<HandRotation>().RotationCoroutine());
            CameraShake.Instance.shakeCameraWithFrequency(1f, 2f, 1f);
            if (rotateFlag == true)
            {
                animator.SetTrigger("Rotate");
                rotateFlag = false;
            }
            else
            {
                animator.SetTrigger("Return");
                rotateFlag = true;
            }
        }
    }
}
