using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGravityTrigger : MonoBehaviour
{
    public int UpOrDown=1;
    bool flag = false;
    public PlayerMovement Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(flag && Input.GetKeyDown(KeyCode.E))
        {
            Player.ChangeG(UpOrDown);
            Debug.Log(1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            flag = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            flag = false;
        }
    }


}
