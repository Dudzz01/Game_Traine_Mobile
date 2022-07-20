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
    [SerializeField] private Animator GamOvAnim;
    [SerializeField] private GameObject HighLine;
    private AudioSource AS;
    [SerializeField] private AudioClip GameOverClip;

    private float ActualScore;

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
        if (PlayerPrefs.HasKey("HighScore"))
        {
            Instantiate(HighLine, HighLine.transform.position * PlayerPrefs.GetFloat("HighScore"), HighLine.transform.rotation);
        }
        AS = gameObject.GetComponent<AudioSource>();
    }
    public IEnumerator GameOver()
    {
        GamOvAnim.SetTrigger("die");
        Debug.Log("Morreu");
        AS.PlayOneShot(GameOverClip);
        yield return new WaitForSeconds(3f);
        if(!PlayerPrefs.HasKey("HighScore") || ActualScore > PlayerPrefs.GetFloat("HighScore"))
            PlayerPrefs.SetFloat("HighScore", ActualScore);
        SceneManager.LoadScene(0);
    }
}