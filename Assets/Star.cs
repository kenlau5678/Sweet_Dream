using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public Transform CheckRangePoint;
    public float RangeMax;
    public float RangeMin;
    public Transform Player;
    public float flyspeed = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(Player.position, transform.position) <= RangeMax && Vector2.Distance(Player.position, transform.position) >= RangeMin)
        {
            Debug.Log(1);
            if (Input.GetKeyDown(KeyCode.H))
            {
                Vector3 direction = (transform.position - Player.position).normalized;
                Vector3 newPosition = Player.position + direction * flyspeed;
                Player.GetComponent<Rigidbody2D>().MovePosition(newPosition);
            }
        }
    }


   
    private void OnDrawGizmos()
    {
        if (CheckRangePoint == null)
        { return; }
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(CheckRangePoint.position, RangeMax); 
        Gizmos.DrawWireSphere(CheckRangePoint.position, RangeMin); 
    }
}
