using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_Enemy : MonoBehaviour
{
     [SerializeField] private List<GameObject> AllEnemies = new List<GameObject>(); //Lista de todos os Power-ups que podemos ter no jogo
    private List<GameObject> ActualEnemies = new List<GameObject>();//Lista dos power-ups que o player ainda pode pegar no jogo

    private int PlatToCreate;//Parametro de quantas plataformas sao necessarias para gerar um Power up (ex: Daqui a 6 plat, vira um power up)
    private int CurrentPlat;//Contador de quantas plataformas ja foram geradas

    [SerializeField] private int Y_Factor;
    void Start()
    {
        ReferenceAllPowerUps();
        //No inicio do jogo, sera adicionado a lista dos power ups atuais todos os power up de AllPowers
        PlatToCreate = Random.Range(3, 8);
        
    }
    void ReferenceAllPowerUps()
    {
        for (int i = 0; i < AllEnemies.Count; i++)
        {
            ActualEnemies.Add(AllEnemies[i]);
        }
    }
    public void CreateEnemy(Transform PlatPos)//Esse metodo sera chamado a toda reposicao de uma nova plataforma
    {    
        CurrentPlat++;
        if (CurrentPlat >= PlatToCreate)
        {
            int EnemyToCreate = Random.Range(0, ActualEnemies.Count);//Sorteio do index do power up a ser instanciado
            Instantiate(ActualEnemies[EnemyToCreate], new Vector3(Random.Range(-6.8f,6.8f),PlatPos.position.y + 30, PlatPos.position.z), PlatPos.rotation); 
            CurrentPlat = 0;
            PlatToCreate = Random.Range(5, 10);
        }
    }
}
