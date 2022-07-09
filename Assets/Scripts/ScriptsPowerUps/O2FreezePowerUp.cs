using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O2FreezePowerUp : MonoBehaviour
{
    
    public GameObject player;
    private int powerUpTime;
    
    void Start()
    {
        player = GameObject.Find("player");
        powerUpTime = 5; //tempo que o powerUp dura, no caso, 5 segundos
    }

    void OnTriggerEnter2D(Collider2D col) 
    {
        if (col.CompareTag("Player"))
        {
            player.GetComponent<Script_Player>().setO2FreezeCount(powerUpTime); //Acessa a variavel o2FreezeCount do player e muda o valor dela para o powerUpTime
            Destroy(this.gameObject); //Destroi o powerUp
        }
    }
   
}
