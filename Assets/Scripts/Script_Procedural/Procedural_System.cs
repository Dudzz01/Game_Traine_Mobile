 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Procedural_System : MonoBehaviour
{

     
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject spawner_plat;

    private float pos_y;
   

    // Update is called once per frame
    void Update()
    {
        pos_y = spawner_plat.GetComponent<Spawner_Plataform>().GetPosY();
    }

    void OnTriggerEnter2D(Collider2D collision) // Um metodo que basicamente verifica a colisao do nosso destroyer com qualquer outro objeto que tenha um colisor
    {
        if(collision.gameObject.tag == "Ground" ) 
        {
         //Destroy(collision.gameObject);
         if(collision.gameObject.name.StartsWith("Plataform") || collision.gameObject.name.StartsWith("Plataform2") || collision.gameObject.name.StartsWith("Plataforma3"))
         {
          collision.gameObject.transform.position = new Vector2(Random.Range(-8f,8f), pos_y-1 );
          Debug.Log(pos_y);
         }
         else if(collision.gameObject.name.StartsWith("ground"))
         {
            Destroy(collision.gameObject);
         }
        }
       
        
        
        
    }
}
