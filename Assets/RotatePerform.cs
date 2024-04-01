using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.Universal;

public class RotatePerform : MonoBehaviour
{
    bool canRotate = false;
    bool isRotating = false;
    public GameObject RotateObject;
    public float RotateAngle; // Define as positive for one direction, negative for the opposite
    public Light2D Light;
    public int maxCount = 5; // Maximum rotations allowed in one direction
    public  int currentCount = 0; // Current count of rotations
    public bool rotateForward = true; // True if rotating forward, false if rotating backward

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isRotating && canRotate && Input.GetKeyDown(KeyCode.E) && currentCount < maxCount)
        {
            isRotating = true;
            float angle = rotateForward ? RotateAngle : -RotateAngle; // Determine direction based on rotateForward
            RotateObject.transform.DORotate(new Vector3(0, 0, RotateObject.transform.eulerAngles.z + angle), 1f, RotateMode.FastBeyond360)
                .OnComplete(() => isRotating = false);
            currentCount++;
            if (currentCount >= maxCount)
            {
                rotateForward = !rotateForward; // Switch direction
                currentCount = 0; // Reset count for the new direction
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canRotate = true;
            Light.DOColor(Color.white, 1f); // Change the light color to white over 1 second
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canRotate = false;
            Light.DOColor(Color.black, 1f); // Optionally, revert the light color
        }
    }
}
