using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Procedural_System : MonoBehaviour
{

    
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision) // Um metodo que basicamente verifica a colisao do nosso destroyer com qualquer outro objeto que tenha um colisor
    {
        if(collision.gameObject.tag == "Ground" ) 
        {
           Destroy(collision.gameObject);
           //Instantiate(collision.gameObject, new Vector3(Random.Range(this.gameObject.transform.position.x+20,this.gameObject.transform.)), );  Irei finalizar esse codigo a noite
           
        }
       
        
        
        
    }
}
