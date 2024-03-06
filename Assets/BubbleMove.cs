using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleMove : MonoBehaviour
{
    public GameObject Bubble;
    public bool flag=true;
    public PanDirection panDirection;

    public float panDistance = 3f;
    public float panTime = 0.35f;
    public bool isTrigger=false;
    public enum PanDirection
    {
        Left,
        Right,
        Up,
        Down
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(isTrigger && Input.GetKeyDown(KeyCode.E)&&flag)
        {
            Vector3 targetPosition = Bubble.transform.position;

            switch (panDirection)
            {
                case PanDirection.Left:
                    targetPosition.x -= panDistance;
                    break;
                case PanDirection.Right:
                    targetPosition.x += panDistance;
                    break;
                case PanDirection.Up:
                    targetPosition.y += panDistance;
                    break;
                case PanDirection.Down:
                    targetPosition.y -= panDistance;
                    break;
            }

            Bubble.transform.DOMove(targetPosition, panTime);
            flag = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Player" )
        {
            isTrigger = true;
            Debug.Log(1);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            isTrigger = false;
            Debug.Log(0);
        }
    }
}
