using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_GameController : MonoBehaviour
{
    public static Script_GameController instance;
    public Image O2Bar;

    private void Awake()
    {
        instance = this;//A partir dessa linha, todas as variaveis e funções publicas podem ser acessadas de outros scripts
    }
    public void ControllO2Bar(float max, float actual)
    {       
        O2Bar.fillAmount = actual / max;//fillAmount é o metodo da Unity que controla o preenchimento de Images devidamente configurados(ao selecionar um image e atribuir um sprite, selecionar o ImageType Filled)
    }//fillAmount retorna um valor entre 0 e 1. Por isso essa divisão: Se max = 10, com actual= 10, fillAmount ==1, com actual = 5, fillAmount == 0.5 e assim vai
    void Start()
    {
        Application.targetFrameRate = 60;
    }

   
}
