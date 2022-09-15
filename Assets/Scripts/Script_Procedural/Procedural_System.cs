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

    private float actualPosY;//Posicao da segunda plataforma mais recente
    [SerializeField]
    private GameObject EmergencyPlatform;//Plataforma que, caso ocorra o bug de ser gerados plataformas muito longe das outras, sera instanciada entre as platforms


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
                collision.gameObject.transform.position = new Vector2(Random.Range(-6.3f,6.3f), pos_y-1 );
                gameObject.GetComponent<Spawner_PUp>().CreatePower(collision.gameObject.transform); // Spawner de powerup
                gameObject.GetComponent<Spawner_Enemy>().CreateEnemy(collision.gameObject.transform);
                //Debug.Log(pos_y);
                
                float delta_y = collision.gameObject.transform.position.y - actualPosY;
                //Debug.Log(delta_y);
                if(delta_y > 10f && actualPosY != 0)
                {
                    //Debug.Log("Plataforma inalcancavel");
                    Instantiate(EmergencyPlatform, (collision.gameObject.transform.position) + Vector3.down * (delta_y / 2), collision.gameObject.transform.rotation);
                }
                actualPosY = collision.gameObject.transform.position.y;
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
            case "Coin":
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
