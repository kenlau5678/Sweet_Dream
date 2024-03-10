using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScale : MonoBehaviour
{
    
    public Transform triggerPoint;
    public Vector3 startScale;
    public Vector3 scale;
    public float changeTime = 1f;
    public GameObject KeyUI;
    public float moveDistance;
    Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = KeyUI.transform.position;
        startScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerPoint.GetComponent<TriggerPoint>().isTrigger == true)
        {
            transform.DOScale(scale, changeTime);
            KeyUI.SetActive(true);
            KeyUI.transform.DOMoveY(startPosition.y + moveDistance, changeTime);
        }
        else
        {
            transform.DOScale(startScale, changeTime);
            KeyUI.transform.DOMoveY(startPosition.y, changeTime);
            
        }

        if (Vector3.Distance(KeyUI.transform.position, startPosition) < 0.1f)
        {
            KeyUI.SetActive(false);
        }

    }

}
