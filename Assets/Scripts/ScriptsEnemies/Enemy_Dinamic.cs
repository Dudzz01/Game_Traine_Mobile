using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Dinamic : MonoBehaviour
{
    private float speed;
    void Start()
    {
        speed = 5f;
    }

    
    /*void Update()
    {
        
    }*/

    void FixedUpdate() 
    {

       MovimentEnemy();

    }
    void OnTriggerEnter2D(Collider2D col) 
    {
        if (col.CompareTag("RightBorder"))
        {
            speed = -5f;
        }
        if (col.CompareTag("LeftBorder"))
        {
            speed = 5f;
        }
    }

    void MovimentEnemy()
    {
      transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y); // Movimentacao
    }
    
}
