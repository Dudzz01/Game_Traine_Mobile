using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_PUp : MonoBehaviour
{
    [SerializeField] private List<GameObject> AllPowers = new List<GameObject>(); //Lista de todos os Power-ups que podemos ter no jogo
    private List<GameObject> ActualPowers = new List<GameObject>();//Lista dos power-ups que o player ainda pode pegar no jogo

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
        for (int i = 0; i < AllPowers.Count; i++)
        {
            ActualPowers.Add(AllPowers[i]);
        }
    }
    public void CreatePower(Transform PlatPos)//Esse metodo sera chamado a toda reposicao de uma nova plataforma
    {    
        CurrentPlat++;
        if (CurrentPlat >= PlatToCreate)
        {
            int PowerToCreate = Random.Range(0, ActualPowers.Count);//Sorteio do index do power up a ser instanciado
            Instantiate(ActualPowers[PowerToCreate], PlatPos.position + Vector3.up * Y_Factor, PlatPos.rotation); 
            //Cria��o de um power up na posicao da plataforma "PlatToCreate-esima" + um pouco pra cima
            
            CurrentPlat = 0;
            if (ActualPowers[PowerToCreate].CompareTag("ImpulsePowerUp"))//So pra balancear, se o power up for o do impulso, PlatToCreate
                PlatToCreate = Random.Range(18, 26);//Sera "mais distante", pra evitar que o player ultrapasse outros power ups 
            else
                PlatToCreate = Random.Range(5, 10);
            ActualPowers.Remove(ActualPowers[PowerToCreate]);//Remocao do power up instanciado, para evitar repeticoes
            if (ActualPowers.Count == 0)//Se a lista dos power ups zerar...
            {
                ReferenceAllPowerUps();//...E so referenciar os mesmos valores de novo, e o ciclo continua
            }
        }
    }
}
