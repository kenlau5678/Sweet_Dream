using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{

    public static CameraManager instance;

    [SerializeField] private CinemachineVirtualCamera[] _allVirtualCameras;

    [SerializeField] private float _fallPanAmount = 0.25f;
    [SerializeField] private float _fallYPanTime = 0.35f;
    public float _fallSpeedYDampingChangeThreshold = -15f;

    public bool IsLerpingYDamping { get;private set; }
    public bool LerpedFromPlayerFalling { get; set; }
    private Coroutine _lerpYPanCoroutine;
    private Coroutine _panCameraCoroutine;
    private CinemachineVirtualCamera _currentCamera;
    private CinemachineFramingTransposer _framingTransposer;
    private float _normYPanAmout;

    private Vector2 _startingTrackedObjectOffset;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        for (int i =0; i < _allVirtualCameras.Length; i++)
        {
            if (_allVirtualCameras[i].enabled)
            {
                _currentCamera = _allVirtualCameras[i];
                _framingTransposer = _currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
            }
        }

        _normYPanAmout = _framingTransposer.m_YDamping;
        _startingTrackedObjectOffset = _framingTransposer.m_TrackedObjectOffset;


    }
    private bool _isPanning = false;
    private Coroutine _panCoroutine;
    private Vector3 _originalOffset;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && !_isPanning)
        {
            _isPanning = true;
            _panCoroutine = StartCoroutine(PanAndReturn(2f, 0.25f, Vector3.up));
        }
        else if (Input.GetKeyDown(KeyCode.S) && !_isPanning)
        {
            _isPanning = true;
            _panCoroutine = StartCoroutine(PanAndReturn(2f, 0.25f, Vector3.down));
        }
        else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            _isPanning = false;
            if (_panCoroutine != null)
            {
                StopCoroutine(_panCoroutine);
            }
            _panCoroutine = StartCoroutine(ReturnToOriginalPosition(0.25f));
        }
        
    }

    IEnumerator PanAndReturn(float yOffset, float panTime, Vector3 direction)
    {
        _originalOffset = _framingTransposer.m_TrackedObjectOffset;
        Vector3 currentOffset = _framingTransposer.m_TrackedObjectOffset;
        Vector3 endOffset = currentOffset + yOffset * direction;

        float elapsedTime = 0f;
        while (elapsedTime < panTime)
        {
            elapsedTime += Time.deltaTime;
            Vector3 lerpedOffset = Vector3.Lerp(currentOffset, endOffset, elapsedTime / panTime);
            _framingTransposer.m_TrackedObjectOffset = lerpedOffset;
            yield return null;
        }

        _framingTransposer.m_TrackedObjectOffset = endOffset; // 确保到达终点位置
    }

    IEnumerator ReturnToOriginalPosition(float returnTime)
    {
        Vector3 currentOffset = _framingTransposer.m_TrackedObjectOffset;

        float elapsedTime = 0f;
        while (elapsedTime < returnTime)
        {
            elapsedTime += Time.deltaTime;
            Vector3 lerpedOffset = Vector3.Lerp(currentOffset, _originalOffset, elapsedTime / returnTime);
            _framingTransposer.m_TrackedObjectOffset = lerpedOffset;
            yield return null;
        }

        _framingTransposer.m_TrackedObjectOffset = _originalOffset; // 确保回到原始位置
    }


    #region Lerp the Y Damping
    public void LerpYDamping(bool isPlayerFalling)
    {
        _lerpYPanCoroutine = StartCoroutine(LerpYAction(isPlayerFalling));
    }

    private IEnumerator LerpYAction(bool isPlayerFalling)
    {
        IsLerpingYDamping = true;
        float startDampAmount = _framingTransposer.m_YDamping;
        float endDampAmount = 0f;
        if (isPlayerFalling)
        {
            endDampAmount = _fallPanAmount;
            LerpedFromPlayerFalling = true;
        }
        else
        {

            endDampAmount = _normYPanAmout;
        }

        float elapsedTime = 0f;
        while (elapsedTime < _fallYPanTime)
        {
            elapsedTime += Time.deltaTime;
            float lerpedPanAmount = Mathf.Lerp(startDampAmount, endDampAmount, elapsedTime / _fallYPanTime);
            _framingTransposer.m_YDamping = lerpedPanAmount;

            yield return null;
        }
        IsLerpingYDamping = false;
    }

    #endregion

    #region Pan Camera

    public void panCameraOnContact(float panDistance, float panTime, Pandirection panDirection, bool panToStartingPos)
    {
        _panCameraCoroutine = StartCoroutine(PanCamera(panDistance, panTime, panDirection, panToStartingPos));
    }

    private IEnumerator PanCamera(float panDistance, float panTime, Pandirection panDirection, bool panToStartingPos) 
    {
        Vector2 endPos = Vector2.zero;
        Vector2 startingPos = Vector2.zero;
        if(!panToStartingPos)
        {
            switch(panDirection)
            {
                case Pandirection.Up:
                    endPos = Vector2.up; break;
                case Pandirection.Down:
                    endPos = Vector2.down; break;
                case Pandirection.Left:
                    endPos = Vector2.left; break;
                case Pandirection.Right:
                    endPos = Vector2.right; break;
                default: break;

            }

            endPos *= panDistance;
            startingPos = _startingTrackedObjectOffset;

            endPos += startingPos;
        }

        else
        {
            startingPos = _framingTransposer.m_TrackedObjectOffset;
            endPos = _startingTrackedObjectOffset;
        }

        float elapsedTime = 0f;
        while(elapsedTime<panTime)
        {
            elapsedTime += Time.deltaTime;
            Vector3 panLerp = Vector3.Lerp(startingPos, endPos, (elapsedTime/panTime));
            _framingTransposer.m_TrackedObjectOffset = panLerp;

            yield return null;
        }
    }
    #endregion

}
