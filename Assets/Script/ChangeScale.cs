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
    // Start is called before the first frame update
    void Start()
    {
        startScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerPoint.GetComponent<TriggerPoint>().isTrigger == true)
        {
            transform.DOScale(scale, changeTime);
        }
        else
        {
            transform.DOScale(startScale, changeTime);
        }
    }

}
