using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Player : MonoBehaviour
{
    
    
    public GameObject right;
    public GameObject left;
    public Rigidbody2D rig; // RigidBody2D do player
    private float speed_v; // Velocidade Vertical do player
    private float speed_h; // Velocidade Horizontal do player
    private float o2FreezeCount;
    private bool jump; // Variavel que possibilita o jump
    private bool isAlive; // Variavel que define se o player está vivo ou morto
    private PlayerO2 PO2;
    public bool canImpulse;
    public float impulseTimer, impulseCount;
    public float invulnerabilidadeCount;

    public bool invulneravel;
    
   
    

    // Start executa o que esta dentro dele através da inicializacao do objeto
    void Start()
    {
        impulseTimer = 1.5f;
        canImpulse = false;
        speed_h = 1000;
        speed_v = 25; //Velocidade Vertical do player
        isAlive = true; // Player está vivo
        PO2 = gameObject.GetComponent<PlayerO2>();//Acessando o script do oxigênio, e todas as suas variaveis e metodos publicos
        invulneravel = false;
    }
        
    // Update executa o que esta dentro dele a todo instante to jogo
    void Update()
    {
      right.transform.position = new Vector3(right.transform.position.x,this.gameObject.transform.position.y,right.transform.position.z);
      left.transform.position = new Vector3(left.transform.position.x,this.gameObject.transform.position.y,left.transform.position.z);
      freezeO2();
      Script_GameController.instance.Score(transform);
      
      if (isAlive == false)
      {
        Destroy(this.gameObject); // Player é destruido
      } 
    }

    void FixedUpdate() 
    {

        if (PO2.getGameOver() == true)//Se deu game over
        {
            Script_GameController.instance.StartCoroutine("GameOver");
            Destroy(this.gameObject);
        }
        if(rig.velocity.y >26) // Condicao para que o player consiga no maximo pular com 20 de velocidade( nao consegue ter uma velocidade maior que isso em seu pulo, utilizei esse codigo para corrigir um bug)
        {
            rig.velocity = new Vector2(0,22); // Caso o player ultrapasse a velocidade 20, a velocidade dele vertical fica 16
        }
        if(jump == true && rig.velocity.y <= 26)
        {
            rig.AddForce(new Vector2(0,speed_v), ForceMode2D.Impulse);
            jump = false;
        }
        Movement();
        impulso();
    }

    public void Movement()//Input.acceleration é a classe responsável pelo acelerometro. Ao chamar .x, ela usa apenas esse eixo para a movimentação do player
    {
        rig.velocity = new Vector2(Input.acceleration.x * speed_h * Time.fixedDeltaTime, rig.velocity.y);//rig.velocity.y garante que esse eixo não vai mudar com essa linha
    }
    void OnCollisionEnter2D(Collision2D collision) // Um metodo que basicamente verifica a colisao do nosso player com qualquer outro objeto que tenha um colisor
    {
         if(collision.gameObject.tag == "Ground" && collision.gameObject.transform.position.y+1 < this.transform.position.y) // Se o player colidir com um objeto que tenha um colisor e tenha a etiqueta "Ground" e o y dele for maior que o y da plataforma que esta colidindo, executará um impulso
         {
            jump = true;
         }
         else
         {
             jump = false;
         }

         if(collision.gameObject.tag == "Enemy"|| collision.gameObject.tag == "EnemyBullet")
         {
             if(!invulneravel)
             {
                Script_GameController.instance.StartCoroutine("GameOver");
                isAlive = false; // mata o jogador
             }
             if(invulneravel)
             {
                Destroy(collision.gameObject);
             }
         }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Efeito de atravessar a tela: Existem 2 colisores como filhos da camera (ou seja, onde a camera for, eles irão atras).
        //Nesses if's, o código está checando em qual borda o player encostou, para enviá-lo para o exato oposto da tela, Somando ou
        //Diminuindo um valor pequeno, apenas para garantir que, nesse "teletransporte", ele venha a se chocar com a outra borda
        if (col.CompareTag("RightBorder"))
        {
            transform.position = new Vector3((transform.position.x * -1) + (float)0.3, transform.position.y, transform.position.z);
        }
        if (col.CompareTag("LeftBorder"))
        {
            transform.position = new Vector3((transform.position.x * -1) - (float)0.3, transform.position.y, transform.position.z);
        }
        //Obs.: Precisei desativar a cinemachine pois a cam estava seguindo o player também no eixo x, e assim, a logica não funcionava
          if(col.gameObject.tag == "Enemy" || col.gameObject.tag == "EnemyBullet")
          {
            if(!invulneravel)
             {
                Script_GameController.instance.StartCoroutine("GameOver");
                isAlive = false; // mata o jogador
             }
            if(invulneravel)
            {
                Destroy(col.gameObject);
            }
          }
        if (col.CompareTag("HighScore"))
        {
            //Efeito Sonoro feliz
            Destroy(col.gameObject);
        }
          
    }

    public void setO2FreezeCount(float o2FreezeCount)
    {
        this.o2FreezeCount = o2FreezeCount;
    }
    
    void freezeO2() //Método para congelar o oxigenio do player
    {
        if (o2FreezeCount > 0) 
        {
            PO2.setDecreasingO2(false);
            o2FreezeCount -= Time.deltaTime;
        }
        if(o2FreezeCount <= 0) 
        {
            PO2.setDecreasingO2(true);
        }
    }
    void impulso()
    {
        if(impulseCount > 0)
        {   
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            invulneravel = true;
            rig.AddForce(new Vector2(rig.velocity.x, 30f),ForceMode2D.Impulse);
            impulseCount -= Time.deltaTime;
        }else if(impulseCount <= 0)
        {
            invulneravel = false;
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
   

   

   

  
    

}
