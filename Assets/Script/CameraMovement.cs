using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float panDistance = 5f;
    public float panTime = 0.5f;
    public Pandirection panDirection;

    void Update()
    {


        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            CameraManager.instance.panCameraOnContact(panDistance, panTime, Pandirection.Up, false);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            CameraManager.instance.panCameraOnContact(panDistance, panTime, Pandirection.Down, false);
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow) ||
            Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow)
            )
        {
            CameraManager.instance.panCameraOnContact(0, panTime, Pandirection.Up, true);
        }
    }
}
