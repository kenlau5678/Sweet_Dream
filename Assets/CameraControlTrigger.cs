using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.TerrainTools;

public class CameraControlTrigger : MonoBehaviour
{
    public CustomInspectorObjects customInspectorObjects;
    private Collider2D _coll;
    private void Start()
    {
        _coll = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(customInspectorObjects.panCameraOnContact)
            {
                CameraManager.instance.panCameraOnContact(customInspectorObjects.panDistance, customInspectorObjects.panTime, customInspectorObjects.panDirection, false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (customInspectorObjects.panCameraOnContact)
            {
                CameraManager.instance.panCameraOnContact(customInspectorObjects.panDistance, customInspectorObjects.panTime, customInspectorObjects.panDirection, true);
            }
        }
    }
}

[System.Serializable]

public class CustomInspectorObjects
{
    public bool swapCameras = false;
    public bool panCameraOnContact = false;

    [HideInInspector] public CinemachineVirtualCamera cameraOnLeft;
    [HideInInspector] public CinemachineVirtualCamera cameraOnRight;
    [HideInInspector] public Pandirection panDirection;

    [HideInInspector] public float panDistance = 3f;
    [HideInInspector] public float panTime = 0.35f;

}
public enum Pandirection
{
    Left,
    Right,
    Up,
    Down
}

[CustomEditor(typeof(CameraControlTrigger))]
public class MyScriptEditor:Editor
{
    CameraControlTrigger cameraControlTrigger;
    private void OnEnable()
    {
        cameraControlTrigger = (CameraControlTrigger)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if(cameraControlTrigger.customInspectorObjects.swapCameras)
        {
            cameraControlTrigger.customInspectorObjects.cameraOnLeft = EditorGUILayout.ObjectField("Camera on Left", cameraControlTrigger.customInspectorObjects.cameraOnLeft,
                typeof(CinemachineVirtualCamera), true) as CinemachineVirtualCamera;
            cameraControlTrigger.customInspectorObjects.cameraOnRight = EditorGUILayout.ObjectField("Camera on Right", cameraControlTrigger.customInspectorObjects.cameraOnRight,
                typeof(CinemachineVirtualCamera), true) as CinemachineVirtualCamera;
        }

        if(cameraControlTrigger.customInspectorObjects.panCameraOnContact)
        {
            cameraControlTrigger.customInspectorObjects.panDirection = (Pandirection)EditorGUILayout.EnumPopup("Camera Pan Direction", 
                cameraControlTrigger.customInspectorObjects.panDirection);
            cameraControlTrigger.customInspectorObjects.panDistance = EditorGUILayout.FloatField("Pan Distance", cameraControlTrigger.customInspectorObjects.panDistance);
            cameraControlTrigger.customInspectorObjects.panTime = EditorGUILayout.FloatField("Pan Time", cameraControlTrigger.customInspectorObjects.panTime);

        }

        if(GUI.changed)
        {
            Debug.Log(1);
            EditorUtility.SetDirty(cameraControlTrigger);
        }
    }
}