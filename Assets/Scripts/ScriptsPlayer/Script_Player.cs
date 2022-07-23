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
    private float o2FreezeCount; // sistema powerup travaO2
    private bool jump; // Variavel que possibilita o jump
    private bool isAlive; // Variavel que define se o player está vivo ou morto
    private PlayerO2 PO2;
    public bool canImpulse; // sistema powerup impulse
    public float impulseTimer, impulseCount;  // sistema powerup impulse
    public float invulnerabilidadeCount;  // sistema powerup impulse

    public bool invulneravel;  // sistema powerup impulse
    
    private bool impulsing; // o player esta impulsionando( por conta do powerup)

    private bool freezing; // o player esta congeland (por conta do powerup)

    private float vel_limit; // limite da velocidade vertical do player

    private bool enable_pick;
    [SerializeField] private AudioClip JumpClip;
    [SerializeField] private AudioClip CoinClip;
    [SerializeField] private AudioClip HighScoreClip;
    [SerializeField] private AudioClip PowerUpClip;
    [SerializeField] private AudioClip EnemyHitClip;

    // Start executa o que esta dentro dele através da inicializacao do objeto
    void Start()
    {
        impulseTimer = 1.5f;
        canImpulse = false;
        speed_h = 1000;
        speed_v = 28; //Velocidade Vertical do player
        isAlive = true; // Player está vivo
        PO2 = gameObject.GetComponent<PlayerO2>();//Acessando o script do oxigênio, e todas as suas variaveis e metodos publicos
        invulneravel = false;
        vel_limit = 32; // padrao normal da velocidade do player
        enable_pick = false;
    }
        
    // Update executa o que esta dentro dele a todo instante to jogo
    void Update()
    {
      //Debug.Log(rig.velocity.y);
      right.transform.position = new Vector3(right.transform.position.x,this.gameObject.transform.position.y,right.transform.position.z);
      left.transform.position = new Vector3(left.transform.position.x,this.gameObject.transform.position.y,left.transform.position.z);
      
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
        if(rig.velocity.y >vel_limit && enable_pick == false) // Condicao para que o player consiga no maximo pular com 20 de velocidade( nao consegue ter uma velocidade maior que isso em seu pulo, utilizei esse codigo para corrigir um bug)
        {
            rig.velocity = new Vector2(0,vel_limit-4); // Caso o player ultrapasse a velocidade 20, a velocidade dele vertical fica 16
        }
        if(jump == true && rig.velocity.y <= vel_limit)
        {
            PO2.PlaySound(JumpClip);
            rig.AddForce(new Vector2(0,speed_v), ForceMode2D.Impulse);
            jump = false;
        }
        Movement();
        freezeO2();
        impulso();
        if(enable_pick == true)
        {
        pickCoin();
        enable_pick = false;
        }
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
             if(!invulneravel )
             {
                Script_GameController.instance.StartCoroutine("GameOver");
                isAlive = false; // mata o jogador
             }
             if(invulneravel )
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
            transform.position = new Vector3((transform.position.x * -1) - (float)0.5, transform.position.y, transform.position.z);
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
                isAlive = true;
                PO2.PlaySound(EnemyHitClip);
                Destroy(col.gameObject);
            }
          }
        if (col.CompareTag("HighScore"))
        {
            PO2.PlaySound(HighScoreClip);
            Destroy(col.gameObject);
        }
        if (col.CompareTag("ImpulsePowerUp") || col.CompareTag("O2FreezePowerUp"))
            PO2.PlaySound(PowerUpClip);        
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
            
            invulneravel = true;
            
            o2FreezeCount -= Time.deltaTime;
        }
        else if(o2FreezeCount <= 0) 
        {
            PO2.setDecreasingO2(true);
            
            if(getImpulsing() == false)
            {
            invulneravel = false;
            setFreezing(false);
            }
            else if(getImpulsing() == true && getFreezing() == false)
            {
                
            }
            else if(getFreezing() == true && getImpulsing() == true)
            {
                 setFreezing(false);
            }
            else if(getImpulsing() == false && getFreezing() == true)
            {
                invulneravel = false;
                setFreezing(false);
            }
            else if(getFreezing() == false && getImpulsing() == false)
            {
                invulneravel = false;
            }
            else if(getFreezing() == true)
            {
                invulneravel = false;
                setFreezing(false);
            }
        }
    }

    void impulso() // impulso powerup
    {
        if(impulseCount > 0)
        {   
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            invulneravel = true;
           
            rig.AddForce(new Vector2(rig.velocity.x, 30f),ForceMode2D.Impulse);
            impulseCount -= Time.deltaTime;
        }
        else if(impulseCount <= 0)
        {
            if(getFreezing() == false)
            {
                //se nao esta ocorrendo o uso do powerup de congelamento de o2
            
            invulneravel = false;
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            setImpulsing(false);
            }
            else if(getFreezing() == true && getImpulsing() == false)
            {
                //se esta ocorrendo o uso de powerup de congelamento de O2 e nao esta ocorrendo o o powerup de impulso
            }
            else if(getFreezing() == true && getImpulsing() == true)
            {
                setImpulsing(false);
            }
            else if(getFreezing() == false && getImpulsing() == true)
            {
                //se nao esta ocorrendo o uso de powerup de congelamento de O2 e esta ocorrendo o o powerup de impulso
                
                invulneravel = false;
                this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                setImpulsing(false);
            }
            else if(getFreezing() == false && getImpulsing() == false)
            {
                 Debug.Log("ta chamand liks");
                 invulneravel = false;
                 
            }
            else if(getImpulsing() == true)
            {
                 invulneravel = false;
                this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                setImpulsing(false);
            }
        }
    }

    public void setEnablePickCoin(bool enable_pick) // metodo setter que seta o valor da permissao de pegar a moeda
    {
        this.enable_pick = enable_pick;
    }

    public void pickCoin()
    {    // se a moeda for pega...
        
            PO2.PlaySound(CoinClip);
            vel_limit = 40f; // o limitador da velocidade do player aumenta
         if(rig.velocity.y <0) // se no momento em que o player pega a moeda, ele estiver indo para baixo, ou seja, a velocidade dele é negativa
         {
            rig.AddForce(new Vector2(rig.velocity.x, (rig.velocity.y*-1)+25f),ForceMode2D.Impulse); // fazemos uma conta aritmetica que basicamente anula a velocidade dele negativa com a positiva, e adicionando a forca que ele quer q va para cima( o speed v)
            Debug.Log("Pego moeda");
            
         }
         else
         {
           rig.AddForce(new Vector2(rig.velocity.x, 12f),ForceMode2D.Impulse); // da o impulso para cima ao pegar a moeda
           Debug.Log("Pego moeda");
           
         }
        
    }

    public void setFreezing(bool freezing){
        this.freezing = freezing;
    }

    public bool getFreezing()
    {
        return this.freezing;
    }

    public void setImpulsing(bool impulsing){
        this.impulsing = impulsing;
    }

    public bool getImpulsing()
    {
        return this.impulsing;
    }    
}