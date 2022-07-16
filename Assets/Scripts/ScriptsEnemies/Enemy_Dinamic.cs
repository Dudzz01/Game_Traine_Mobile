using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Dinamic : MonoBehaviour
{
    private float speed;
    private float contador;
    private bool verify_col; // verificando colissao e limitando movimentacao do enemy ao ocorrer isso
    
    
    void Start()
    {
        verify_col = false;
        this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-5,0);
    }

    
    /*void Update()
    {
        
    }*/

    void FixedUpdate() 
    {
       if(verify_col == false)
       {
       MovimentEnemy();
       }
       if(verify_col == true)
       {
           contadorTempVelY();
       }

    }
    void OnTriggerEnter2D(Collider2D col) 
    {

        if(col.CompareTag("Ground"))
        {
            verify_col = true;
            
            
            
           if(verify_col == true)
           {
                this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 3);
           }
           
        }
    }

    void MovimentEnemy()
    {
      
         if(transform.position.x <= -8)
         {
             this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(5,0);
         }
         else if(transform.position.x >= 7)
         {
            this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-5,0);
         }
         
    }

    void contadorTempVelY()
    {
           contador+=Time.deltaTime;

           if(contador>1)
           {
            this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-5,0);
            contador = 0;
            verify_col = false;
           }
    }
    
    
}
