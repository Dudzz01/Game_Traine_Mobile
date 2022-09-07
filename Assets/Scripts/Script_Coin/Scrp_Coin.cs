using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrp_Coin : MonoBehaviour
{
     
    private void OnTriggerEnter2D(Collider2D col) 
    {
        if(col.CompareTag("Player"))
        {
            col.GetComponent<Script_Player>().setEnablePickCoin(true); // habilita as efeitos que a moeda causam ao colidir com o player
            Destroy(this.gameObject); // destroi a moeda
        }
    }
}
