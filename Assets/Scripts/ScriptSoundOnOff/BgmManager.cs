using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmManager : MonoBehaviour
{
    public static BgmManager instance;
    private AudioSource AS;
    [SerializeField] AudioClip BgmIntro;
    [SerializeField] private AudioClip BgmLoop;
    private void Awake()
    {
        if(instance == null)//Se nao existe nenhum objeto da cena que tenha a classe referenciada 
        {
            instance = this;//A gente referencia a classe
            DontDestroyOnLoad(instance);//E não permite que esse objeto seja destruido entre cenas
            AS = GetComponent<AudioSource>();
            StartCoroutine("StartLoop");
        }
        else//Se ja existe essa classe referenciada
        {
            Destroy(gameObject);//a gente Destroy o gameObject, pois já existe um deles na cena 
        }
        //if e else necessarios pra garantir que nunca tenham 2 ou + obj tocando a mesma musica
    }

   IEnumerator StartLoop()
    {
        float TimeToWait = BgmIntro.length;
        yield return new WaitForSeconds(TimeToWait - 1.5f);
        AS.loop = true;
        AS.clip = BgmLoop;
        
        AS.Play();
    }
}
