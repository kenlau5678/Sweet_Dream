using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicPower : MonoBehaviour
{
    public MagicBar magicBar;
    public int maxMagicPower = 100;
    public int currentMagicPower;
    // Start is called before the first frame update
    void Start()
    {
        if (magicBar == null) return;
        currentMagicPower = maxMagicPower;
        magicBar.SetMaxMagic(maxMagicPower);
        StartCoroutine("MagicRevert");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator MagicRevert()
    {
        //while (currentMagicPower < maxMagicPower)
        //{
        //    currentMagicPower += 20;
        //    magicBar.SetMagicPower(currentMagicPower);
        //    Debug.Log(currentMagicPower);
        //    yield return new WaitForSeconds(3);
        //}

        while (true)
        {
            currentMagicPower += 20;
            if (currentMagicPower > maxMagicPower)
            {
                currentMagicPower = maxMagicPower;
            }
            magicBar.SetMagic(currentMagicPower);
            Debug.Log(currentMagicPower);
            yield return new WaitForSeconds(3);
        }
    }
}
