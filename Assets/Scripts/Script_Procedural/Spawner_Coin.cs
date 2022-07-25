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

    private float  cont;  // contador do active spawn

    void Start()
    {
        cont = 0;
        enable_putCoinList = true;
        enable_spawn = false;
        timer_spawn = 0;
        state_spawn_coin = "Default";
        random_pos_first_coin = Random.Range(-6.8f,3);
    }

    // Update is called once per frame
    void Update()
    {
      if(enable_putCoinList == true)
      {
          putCoinInList();
          enable_putCoinList = false;
      }
             activeSpawn();
       if(enable_spawn == true)
       {
         createCoin(selectRandom());
         enable_spawn = false;
       }
    }

    public void putCoinInList()
    {
          for(int j = 0; j<12; j++)
          {
                 listCoin.Add(coin);
          }
    }

    public void activeSpawn()
    {
        cont+=Time.deltaTime; 
        if(cont >= 10)
        {
           enable_spawn = true;
           cont = 0;
        }             
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
               aux_pos_coin = 0;
               aux_pos_coin_y = 0;
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
                
          break;

          case "LineForm":
                float[] pos_y2 = new float[12];
                float[] pos_x2 = new float[1];
                int aux_x2 = 0;
                int aux_y2 = 0;
                int count_aux = 0;
                aux_pos_coin = 0;
                aux_pos_coin_y = 0;
                foreach(GameObject coinss in listCoin)
                {
                  count_aux++;
                  pos_x2[0] = random_pos_first_coin+aux_pos_coin;
                  pos_y2[aux_y2] = this.gameObject.transform.position.y+aux_pos_coin_y;
                 
                  ////////////////////////////////////////////////////
                  Instantiate(coinss,new Vector2(pos_x2[aux_x2],pos_y2[aux_y2]), Quaternion.identity);
                  aux_pos_coin_y+=3;
                  aux_y2+=1;

                  if(count_aux>=6)
                  {
                    break;
                  }
               }
          
          break;

          case "DiagonalForm":
                float[] pos_y3 = new float[12];
                float[] pos_x3 = new float[1];
                int aux_x3 = 0;
                int aux_y3 = 0;
                int count_aux3 = 0;
                aux_pos_coin = 0;
                aux_pos_coin_y = 0;
                foreach(GameObject coinss in listCoin)
                {
                  count_aux3++;
                  pos_x3[aux_x3] = random_pos_first_coin+aux_pos_coin;
                  pos_y3[aux_y3] = this.gameObject.transform.position.y+aux_pos_coin_y;
                 
                  ////////////////////////////////////////////////////
                  Instantiate(coinss,new Vector2(pos_x3[aux_x3],pos_y3[aux_y3]), Quaternion.identity);
                  aux_pos_coin_y+=3;
                  aux_y3+=1;
                  aux_pos_coin+=1;
                  if(count_aux3>=6)
                  {
                    break;
                  }
               }
          
          break;

        }
    }

    public string selectRandom()
    {
        int num_random = Random.Range(1,4);
        
        switch(num_random)
        {
          case 1:
                this.state_spawn_coin = "SquareForm";
          break;

          case 2: 
                this.state_spawn_coin = "LineForm";
          break;

          case 3: 
                this.state_spawn_coin = "DiagonalForm";
          break;

          default:
                 this.state_spawn_coin = "SquareForm";
          break;
        }
        return this.state_spawn_coin;
    }
      
    

    

}
