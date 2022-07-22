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
                gameObject.GetComponent<Spawner_PUp>().CreatePower(collision.gameObject.transform); // Spawner de powerup
                gameObject.GetComponent<Spawner_Enemy>().CreateEnemy(collision.gameObject.transform);
                //Debug.Log(pos_y);
            }
            else if(collision.gameObject.name.StartsWith("ground"))
            {
                Destroy(collision.gameObject);
            }
        }

         switch(collision.gameObject.tag)
         {
            case "O2FreezePowerUp": 
            Destroy(collision.gameObject);
            break;
            case "Enemy":  
             Destroy(collision.gameObject);
            break;
            case "EnemyBullet":
            Destroy(collision.gameObject);
            break;
            case "ImpulsePowerUp":
            Destroy(collision.gameObject);
            break;
            case "CapsuleO2":
            Destroy(collision.gameObject);
            break;
            case "Player":
            Script_GameController.instance.StartCoroutine("GameOver");
            break;
            //default:
            //Debug.Log("Objeto nao encontrado para destruir");
            //break;
         }
      
    }
}
