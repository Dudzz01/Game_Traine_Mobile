using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Player : MonoBehaviour
{
    
    public Rigidbody2D rig;
    private float speed_v;
    public float speed_h;
    private bool jump;
    private PlayerO2 PO2;

    // Start executa o que esta dentro dele através da inicializacao do objeto
    void Start()
    {
        PO2 = gameObject.GetComponent<PlayerO2>();//Acessando o script do oxigênio, e todas as suas variaveis e metodos publicos
        speed_v = 20; //Velocidade Vertical do player
       
    }
        
    

    // Update executa o que esta dentro dele a todo instante to jogo
    /*void Update()
    {
        
    }*/

    void FixedUpdate() 
    {
        if (!PO2.GameOver)//Se não deu game over no script do PlayerO2
        {
            if (jump == true)
            {
                rig.AddForce(new Vector2(0, speed_v), ForceMode2D.Impulse);
                jump = false;
            }
            Movement();
        }
        else//Se deu...
        {
            Debug.Log("Game Over!");//Game Over, e, assim, bloqueia a movimentação do player(já que o if não vai mais ser lido)
        }
    }

    void Movement()
    {
        rig.velocity = new Vector2(Input.acceleration.x * speed_h * Time.fixedDeltaTime, rig.velocity.y);//rig.velocity.y garante que esse eixo não vai mudar com essa linha
    }//Input.acceleration é a classe responsável pelo acelerometro. Ao chamar .x, ela usa apenas esse eixo para a movimentação do player
    void OnCollisionEnter2D(Collision2D collision) // Um metodo que basicamente verifica a colisao do nosso player com qualquer outro objeto que tenha um colisor
    {
        if(collision.gameObject.tag == "Ground") // Se o player colidir com um objeto que tenha um colisor e tenha a etiqueta "Ground", executará um impulso
        {
            jump = true;
        }
        else
        {
            jump = false;
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        //Efeito de atravessar a tela: Existem 2 colisores como filhos da camera (ou seja, onde a camera for, eles irão atras).
        //Nesses if's, o código está checando em qual borda o player encostou, para enviá-lo para o exato oposto da tela, Somando ou
        //Diminuindo um valor pequeno, apenas para garantir que, nesse "teletransporte", ele venha a se chocar com a outra borda
        if (col.CompareTag("RightBorder"))
        {
            transform.position = new Vector3((transform.position.x * -1) + 0.1f, transform.position.y, transform.position.z);
        }
        if (col.CompareTag("LeftBorder"))
        {
            transform.position = new Vector3((transform.position.x * -1) - 0.1f, transform.position.y, transform.position.z);
        }
        //Obs.: Precisei desativar a cinemachine pois a cam estava seguindo o player também no eixo x, e assim, a logica não funcionava
    }


}
