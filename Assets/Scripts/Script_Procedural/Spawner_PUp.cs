using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_PUp : MonoBehaviour
{
    [SerializeField] private List<GameObject> AllPowers = new List<GameObject>(); //Lista de todos os Power-ups que podemos ter no jogo
    
    private int PlatToCreate;//Parametro de quantas plataformas sao necessarias para gerar um Power up (ex: Daqui a 6 plat, vira um power up)
    private int CurrentPlat;//Contador de quantas plataformas ja foram geradas

    [SerializeField] private int Y_Factor;
    void Start()
    {
        
        PlatToCreate = Random.Range(3, 8);
        
        
    }
    
    public void CreatePower(Transform PlatPos)//Esse metodo sera chamado a toda reposicao de uma nova plataforma
    {    
        CurrentPlat++;
        if (CurrentPlat >= PlatToCreate)
        {
            
            int Sorter = Random.Range(1, 21);
            Debug.Log(Sorter);
            if(Sorter <= 5 && Sorter > 0)
            {
                Debug.Log("ESSE E PWP AZUL");
                Instantiate(AllPowers[2], PlatPos.position + Vector3.up * Y_Factor, PlatPos.rotation);//Capsula de O2
                PlatToCreate = Random.Range(5, 9);
            }
            else if(Sorter <= 10 && Sorter > 5)
            {//Impulso
                
                Debug.Log("ESSE E IMPULSO");
                Instantiate(AllPowers[1], PlatPos.position + Vector3.up * Y_Factor, PlatPos.rotation); // Impulso
                PlatToCreate = Random.Range(10, 15);
            }
            else if(Sorter>10)
            {
                Debug.Log("ESSE E OXIGENIO");
                Instantiate(AllPowers[0], PlatPos.position + Vector3.up * Y_Factor, PlatPos.rotation);
                PlatToCreate = Random.Range(5, 9);
            }
            CurrentPlat = 0;
            
        }
    }
}
