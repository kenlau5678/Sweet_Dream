using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallManager : MonoBehaviour
{
    [SerializeField] GameObject[] FireBallPrefabs;
    Animator anim;
   public void SpawnFireBall()//生成
    {
        int temp = Random.Range(0, 10), r=0;
        if(temp>=7)
        {
            r = 1;//BlueFireball
        }
        else
        { r = 0;}//NormalBall
        GameObject fireBall = Instantiate(FireBallPrefabs[r],transform);//直接生成在manager的子物件
        fireBall.transform.position = new Vector3(Random.Range(-10f,13f), Random.Range(6f,10f), 0f);
    }
}
