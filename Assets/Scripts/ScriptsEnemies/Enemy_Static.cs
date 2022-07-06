using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Static : MonoBehaviour
{
     private float shoot_time;
     public GameObject enemy_bullet;

    // Start is called before the first frame update
   /* void Start()
    {
        
    }*/

    // Update is called once per frame
    void Update()
    {
        shootEnemy();
    }

    void shootEnemy()
    {
        shoot_time = shoot_time+Time.deltaTime;

           
           if(shoot_time>1)
           {
             Instantiate(enemy_bullet, this.transform.position,Quaternion.identity);
             shoot_time = 0;
           }
    }

     
}
