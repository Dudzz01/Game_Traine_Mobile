using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Player : MonoBehaviour
{
    
    public Rigidbody2D rig;
    private float speed_v;
    private bool jump;
   

    // Start executa o que esta dentro dele através da inicializacao do objeto
    void Start()
    {
        
        speed_v = 20; //Velocidade Vertical do player
        Application.targetFrameRate = 60;
    }
        
    

    // Update executa o que esta dentro dele a todo instante to jogo
    /*void Update()
    {
        
    }*/

    void FixedUpdate() {
       
       if(jump == true)
       {
          rig.AddForce(new Vector2(0,speed_v), ForceMode2D.Impulse);
          jump = false;
       }

    }
    void OnCollisionEnter2D(Collision2D collision) // Um metodo que basicamente verifica a colisao do nosso player com qualquer outro objeto que tenha um colisor
    {
        if(collision.gameObject.tag == "Ground") // Se o player colidir com um objeto que tenha um colisor e tenha a etiqueta "Ground", executará um impulso
        {
           
                  jump = true;
        }
        else
        {
                  jump = false;
        }
    }

    
    
}
