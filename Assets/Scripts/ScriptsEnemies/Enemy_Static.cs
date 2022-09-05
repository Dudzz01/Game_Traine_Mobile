using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Static : MonoBehaviour
{
     private float shoot_time;
     private bool PermRayCast{get; set;} // Propriedade Autoimplementada
     public bool VerifyCollision{get; set;}
     public GameObject enemy_bullet;
    [SerializeField]
    private float MaxRange;
    [SerializeField]
    private Transform Point;
     

     

    // Start is called before the first frame update
    

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

    void OnTriggerEnter2D(Collider2D col) 
    {
      if(!VerifyCollision)
      {
            PermRayCast = true; //restricao da velocidade do enemy ativada
            if(col.CompareTag("Ground") || col.CompareTag("CapsuleO2") || col.CompareTag("O2FreezePowerUp") || col.CompareTag("ImpulsePowerUp")) // se verificar a colisao
            {
                 PermRayCast = false;//restricao da velocidade do enemy desativada          
                 VerifyCollision = true;//a colisao esta sendo verificada
            }
            
      }

      if(VerifyCollision)
      {
            this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 10);
            if(!col.CompareTag("Ground") && !col.CompareTag("CapsuleO2") && !col.CompareTag("O2FreezePowerUp") && !col.CompareTag("ImpulsePowerUp")) // se nao está mais colidindo...
            {           
                PermRayCast = true;//restricao da velocidade do enemy ativada        
                VerifyCollision = false;//colisao nao está mais sendo verificada
            }
      }
        
            
    }
    private void FixedUpdate()
    {
        GetPlatformDown();
    }
    void GetPlatformDown()
    {     
        RaycastHit2D hit = Physics2D.Raycast(Point.position, Vector2.down, MaxRange);//Raycast � um colisor especial da unity, uma linha que
        //se inicia da posicao do primeiro argumento, segue a direcao do segundo e tem o comprimento do terceiro
        if(hit.collider != null && (!hit.transform.CompareTag("EnemyBullet") || !hit.transform.CompareTag("Enemy")))
        {
            if (hit.transform.CompareTag("Ground") || hit.transform.CompareTag("CapsuleO2") || hit.transform.CompareTag("O2FreezePowerUp") || hit.transform.CompareTag("ImpulsePowerUp"))//Se o raycast encostar no chao...
            {
                this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 10);//...ele se movera pra cima
                
            }
            
        }
        if(hit.collider == null && PermRayCast == true)//Quando o raycast nao tocar em mais nada
            this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);//ele para o movimento
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(Point.position, Vector2.down * MaxRange);
    }
    
    

     
}
