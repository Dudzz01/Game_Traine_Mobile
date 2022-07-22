using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_Coin : MonoBehaviour
{
   [SerializeField] private List<GameObject> listCoin = new List<GameObject>();//Lista das moedas que vou spawnar
    
    [SerializeField] private GameObject coin; // objeto moeda que vou spawnar

    private string state_spawn_coin; // estilos geometricos em que os grupos de moedas serao spawnados

    private int aux_pos_coin; // variavel auxiliar para as posicoes das moedas spawnadas

    private int aux_pos_coin_y; // variavel auxiliar para as posicoes y das moedas spawnadas

    private float random_pos_first_coin; // primeira posicao aleatoria da primeira moeda spawnada do grupo de moedas

    private float timer_spawn; // tempo de spawn

    private bool enable_spawn; // permitir spawn de moedas

    private bool enable_putCoinList; // permitir a adicao de moedas na lista
    
    void Start()
    {
        enable_putCoinList = true;
        enable_spawn = false;
        timer_spawn = 0;
        state_spawn_coin = "Default";
        random_pos_first_coin = Random.Range(-5,3);
    }

    // Update is called once per frame
    void Update()
    {
      if(enable_putCoinList == true)
      {
          putCoinInList();
          enable_putCoinList = false;
      }
       

       if(enable_spawn == true)
       {
         createCoin("SquareForm");
         enable_spawn = false;
       }
    }

    public void putCoinInList()
    {
        
     // se o contador for menor que 0.55, ele adicionará instancias de moedas em uma lista de moedas, essa adicao ocorre enquanto o contador for menor que meio segundo (0.55)
       
          for(int j = 0; j<12; j++)
          {
                 listCoin.Add(coin);
          }
          for(timer_spawn = 0; timer_spawn<3; timer_spawn+=Time.deltaTime)
          {
                    enable_spawn = false;
          }
       
                    enable_spawn = true;
        

    }

    public void createCoin(string state_spawn)
    {
        this.state_spawn_coin = state_spawn;

        switch(state_spawn_coin)
        {
          case "SquareForm":
               float[] pos_x = new float[3];
               float[] pos_y = new float[4];
               int aux_x = 0;
               int aux_y = 0;
               
               
               foreach(GameObject coinss in listCoin)
               {
                 pos_x[0] = random_pos_first_coin+aux_pos_coin;
                 pos_x[1] = random_pos_first_coin+aux_pos_coin;
                 pos_x[2] = random_pos_first_coin+aux_pos_coin;
                 pos_y[0] = this.gameObject.transform.position.y+aux_pos_coin_y;
                 pos_y[1] = this.gameObject.transform.position.y+aux_pos_coin_y;
                 pos_y[2] = this.gameObject.transform.position.y+aux_pos_coin_y;
                 pos_y[3] = this.gameObject.transform.position.y+aux_pos_coin_y;
                 aux_pos_coin+=2;
                 
                 ////////////////////////////////////////////////////
                 Instantiate(coinss,new Vector2(pos_x[aux_x],pos_y[aux_y]), Quaternion.identity);

                 if(aux_y >= 3)
                 {
                    aux_x +=1;
                    aux_y = 0;
                    aux_pos_coin = 0;
                    aux_pos_coin_y+=5;
                    
                 }
                 else
                 {
                    aux_y+=1;
                 }

               }
              
              listCoin.Clear();
              
              
               
                  
          break;

          case "LineForm":
          
          break;

          case "DiagonalForm":
          
          break;

        }
    }
}
