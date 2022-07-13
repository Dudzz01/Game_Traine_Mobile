using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpulsoPowerUp : MonoBehaviour
{
    private GameObject player;
    public float poderImpulso;
    
    void Start()
    {
        player = GameObject.Find("player");
        poderImpulso = 50f;
    }
    private void OnTriggerEnter2D(Collider2D col) 
    {
        if(col.CompareTag("Player"))
        {
            player.GetComponent<Script_Player>().impulseCount = player.GetComponent<Script_Player>().impulseTimer;
            Destroy(this.gameObject);
        }
    }
}
