using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHPHealth : MonoBehaviour
{
    private MagicPower MP;
    public int health;
    // Start is called before the first frame update
    void Start()
    {
        MP = GetComponent<MagicPower>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Health();
        }
    }

    public void Health()
    {
        if (MP != null)
        {
            if (MP.currentMagicPower < 30) return;
        }
        if(GetComponent<PlayerHealth>().currentHeath == GetComponent<PlayerHealth>().maxHeath) 
        { 
            return; 
        }
        GetComponent<PlayerHealth>().HPHealth(health);
        if (MP.currentMagicPower < 30)
        {
            MP.currentMagicPower = 0;
        }
        else
        {
            MP.currentMagicPower -= 30;
        }

        MP.magicBar.SetMagic(MP.currentMagicPower);
    }

}
