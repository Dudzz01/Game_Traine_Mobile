using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Static : MonoBehaviour
{
     private float shoot_time;
     public GameObject enemy_bullet;

     private float contador;
    [SerializeField]
    private float MaxRange;
    [SerializeField]
    private Transform Point;
     

      private bool verify_col; // verificando colissao e limitando movimentacao do enemy ao ocorrer isso

    // Start is called before the first frame update
    void Start()
    {
        verify_col = false;
    }

    // Update is called once per frame
    void Update()
    {
        shootEnemy();

        if(verify_col == true)
        {
          contadorTempVelY();
        }
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

    void OnTriggerEnter2D(Collider2D col) 
    {

        if(col.CompareTag("Ground"))
        {
             verify_col = true;

             if(verify_col == true)
             {
             this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 10);
             }
            
        }
    }
    private void FixedUpdate()
    {
        GetPlatformDown();
    }
    void GetPlatformDown()
    {     
        RaycastHit2D hit = Physics2D.Raycast(Point.position, Vector2.down, MaxRange);//Raycast é um colisor especial da unity, uma linha que
        //se inicia da posicao do primeiro argumento, segue a direcao do segundo e tem o comprimento do terceiro
        if(hit.collider != null && (!hit.transform.CompareTag("EnemyBullet") || !hit.transform.CompareTag("Enemy")))
        {
            if (hit.transform.CompareTag("Ground") || hit.transform.CompareTag("CapsuleO2") || hit.transform.CompareTag("O2FreezePowerUp") || hit.transform.CompareTag("ImpulsePowerUp"))//Se o raycast encostar no chao...
            {
                this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 10);//...ele se movera pra cima
            }
            
        }
        if(hit.collider == null)//Quando o raycast nao tocar em mais nada
            this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);//ele para o movimento
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(Point.position, Vector2.down * MaxRange);
    }
    void contadorTempVelY()
    {
           contador+=Time.deltaTime;

           if(contador>0.5f)
           {
            this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
            contador = 0;
            verify_col = false;
           }
    }
    

     
}
