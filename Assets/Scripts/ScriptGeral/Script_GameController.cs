using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Script_GameController : MonoBehaviour
{
    public static Script_GameController instance;
    [SerializeField] private Image O2Bar;
    [SerializeField] private Text ScoreTxt;
    [SerializeField] private Text CoinTxt;
    [SerializeField] private Animator GamOvAnim;
    [SerializeField] private Animator BarAnim;
    [SerializeField] private GameObject HighLine;
    [SerializeField] private GameObject TutorialObj;
    private AudioSource AS;
    [SerializeField] private AudioClip GameOverClip;

    private float ActualScore;
    private int coin;

    private void Awake()
    {
        instance = this; //A partir dessa linha, todas as variaveis e funcoes publicas podem ser acessadas de outros scripts
    }
    
    public void ControllO2Bar(float max, float actual)
    {       
        O2Bar.fillAmount = actual / max; //fillAmount e o metodo da Unity que controla o preenchimento de Images devidamente configurados(ao selecionar um image e atribuir um sprite, selecionar o ImageType Filled)
    } //fillAmount retorna um valor entre 0 e 1. Por isso essa divisao: Se max = 10, com actual= 10, fillAmount ==1, com actual = 5, fillAmount == 0.5 e assim vai
    
    public void Score(Transform playerPos)
    {
        if(playerPos.position.y > ActualScore)
        {
            ActualScore = playerPos.position.y;
            ScoreTxt.text = ActualScore.ToString("F2");
        }
    }
    void Start()
    {
        Application.targetFrameRate = 60;
        if (PlayerPrefs.HasKey("HighScore"))//Sempre que uso a funcao HasKey(), estou checando se, em algum momento do jogo, ja criei
        {//Alguma chave (ou seja, variavel especial) de PlayerPrefs com esse nome 
            Instantiate(HighLine, HighLine.transform.position * PlayerPrefs.GetFloat("HighScore"), HighLine.transform.rotation);
        }
        if (PlayerPrefs.HasKey("ShowTutorial"))//Se o jogo ja mostrou o tutorial uma vez
        {
            TutorialObj.SetActive(false);//ele ira desativar o objeto responsavel pelo tutorial
        }
        else if(!PlayerPrefs.HasKey("ShowTutorial"))//Caso contrario
        {
            TutorialObj.SetActive(true);//esse objeto estara ativo
        }
        AS = gameObject.GetComponent<AudioSource>();
    }
    public void BarAnimator(int anim)
    {
        BarAnim.SetInteger("transition", anim);
        //if(anim == 2)
        //{
        //    BarAnim.SetInteger("transition", 0);
        //}
    }
    public IEnumerator GameOver()
    {
        GamOvAnim.SetTrigger("die");//Chamo animacao de game over
        Debug.Log("Morreu");
        AS.PlayOneShot(GameOverClip);
        if (!PlayerPrefs.HasKey("HighScore") || ActualScore > PlayerPrefs.GetFloat("HighScore"))//PlayerPrefs sao variaveis especiais da Unity
            PlayerPrefs.SetFloat("HighScore", ActualScore);//Que diferente das normais, nao perdem suas informacoes entre cenas
        int TotalC = coin;
        if (PlayerPrefs.HasKey("TotalCoins"))
        {
            TotalC += PlayerPrefs.GetInt("TotalCoins");
        }
        if (!PlayerPrefs.HasKey("ShowTutorial"))
        {
            PlayerPrefs.SetInt("ShowTutorial", 1);
        }
        PlayerPrefs.SetInt("TotalCoins", TotalC);
        yield return new WaitForSeconds(2f); //2 segundos e o tempo da animacao     
        Time.timeScale = 0;//apos a anim, pauso o jogo para que nao fique tocando nenhum efeito sonoro
    }
    public void RefreshCoin()
    {
        coin++;
        CoinTxt.text = " X " + coin.ToString();
    }
    public void ToScene(int index)
    {
        SceneManager.LoadScene(index);
        Time.timeScale = 1;
    }
}