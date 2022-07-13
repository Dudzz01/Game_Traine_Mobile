using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpulsePowerup : MonoBehaviour
{

    public GameObject player;
    private float time;
   

   private void FixedUpdate() {
    Impulse();
   }


   void Impulse()
   {
       if(time<=1.7 && player.GetComponent<Script_Player>().active_pwp == true)
       {
        player.GetComponent<Rigidbody2D>().AddForce(new Vector2(player.GetComponent<Rigidbody2D>().velocity.x, 1.5f),ForceMode2D.Impulse);
        time+=Time.deltaTime;
       }
       else if(time>=1.7)
       {
          player.GetComponent<Script_Player>().active_pwp = false;
          Destroy(this.gameObject);
       }
   }

   private void OnTriggerEnter2D(Collider2D col) {
    
    if(col.CompareTag("Player"))
    {
        player.GetComponent<Script_Player>().active_pwp = true;
        this.gameObject.transform.position = new Vector2(0,-20);
    }

   }
  
  
  
}
