using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet : MonoBehaviour
{
    
    public BoxCollider2D box_bullet; // mascara de colisao do tiro do inimigo estático

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

         void OnTriggerEnter2D(Collider2D col) // metodo para fazer o efeito de tiro, o tiro ultrapassa o inimigo dando um efeito de que o tiro sai do inimigo, e aos demais objetos do jogo ele realmente colide sem ultrapassar
        {
           if(col.CompareTag("Enemy"))
           {
             box_bullet.isTrigger = true;
           }
           else
           {
             box_bullet.isTrigger = false;
           }
        }
        

     
}
