using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerO2 : MonoBehaviour
{
    public float MaxO2;
    public float ActualO2;

    private bool CanDecreasing = true;//Com esse bool true, o O2 cai. Se false, ele para de cair
    public bool GameOver = false;

    void Start()
    {
        ActualO2 = MaxO2;
    }

    // Update is called once per frame
    void Update()
    {
        DecreasingO2();
        Script_GameController.instance.ControllO2Bar(MaxO2, ActualO2);//Modo de chamar a fun��o do GameController, ap�s ter feito o singleton
    }

    void DecreasingO2()
    {
        if (CanDecreasing)//Se o O2 pode ser decrescido...
        {
            ActualO2 -= Time.deltaTime;//...isso vai acontecer por segundos

            if(ActualO2 <= 0)//Se acabar...
            {
                Debug.Log("Acabou o O2");
                CanDecreasing = false;//...Ele n�o pode mais ser decrescido
                GameOver = true;//Boleano respons�vel para que o outro script saiba que o O2 acabou e, dessa forma, de game over
            }
        }
    }
    IEnumerator IncreaseO2(float factor)//IEnumerator s�o fun��es especiais que envolvem tempo em segundos
    {
        float MaxIncreased = ActualO2 + factor;//O fator que ser� aumentado ser� o O2 atual + um parametro na chamada da fun��o
        
        CanDecreasing = false;//Interromper o fluxo de O2 para poder encher

        if (MaxIncreased > MaxO2)
            MaxIncreased = MaxO2;//Se for maior que o maximo, ele recebe o maximo
        while(ActualO2 < MaxIncreased)
        {
            ActualO2 += 0.1f;
            yield return new WaitForSeconds(0.001f);//Para dar um efeito de "encher a barra", o oxig�nio atual ser� incrementado ap�s um curto periodo de tempo
        }//Pense de forma descontruido em voc� encher um copo d'agua: a cada 0.1 segundos, voc� bota 1 ml de agua no copo, por exemplo
        CanDecreasing = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CapsuleO2") && !GameOver)//Se colidir com a capsula e ainda n�o tiver dado game over
        {
            StartCoroutine(IncreaseO2(5));//Modo de chamar um IEnumerator
            Destroy(collision.gameObject);
        }
    }

}
