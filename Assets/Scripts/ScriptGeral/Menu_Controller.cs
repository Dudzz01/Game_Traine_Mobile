using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu_Controller : MonoBehaviour
{
    [SerializeField] private Text HighText;
    [SerializeField] private Text CoinText;
    [SerializeField] private Toggle[] AllToggles;
    [SerializeField] private Slider Sens;

    [SerializeField] private Animator ShopAnim;
    [SerializeField] private Animator CreditsAnim;
    [SerializeField] private Animator OptionsAnim;

    [SerializeField] private GameObject CreditsButton;
    [SerializeField] private GameObject ShopButton;
    [SerializeField] private GameObject OptButton;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject imageRecord;

    private bool ButtonState = true;
    private float distanceBeetweenImages = 0;

    private Language[] allLangs;
    public void startGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void SetAnimShop(string anim)
    {
        ButtonState = !ButtonState;
        ShopAnim.SetTrigger(anim);//Chamada da animacao pelo parametro, que e declarado no OnClick do botao
        CreditsButton.SetActive(ButtonState);//Apenas para que nao aconteca interferencia entre as telas, desativo o botao dos 
        OptButton.SetActive(ButtonState);
    }//creditos enquanto a tela da loja esta ativa, e vice-versa
    public void SetAnimCredits(string anim)
    {
        ButtonState = !ButtonState;
        CreditsAnim.SetTrigger(anim);
        ShopButton.SetActive(ButtonState);
        OptButton.SetActive(ButtonState);
    }
    public void SetAnimOptions(string anim)
    {
        ButtonState = !ButtonState;
        OptionsAnim.SetTrigger(anim);
        ShopButton.SetActive(ButtonState);
        CreditsButton.SetActive(ButtonState);
        allLangs = FindObjectsOfType<Language>();
        if (allLangs.Length > 0)
        {
            for (int i = 0; i < allLangs.Length; i++)
                allLangs[i].RefreshTexts();
        }
    }
    void Start()
    {
        AllToggles[PlayerPrefs.GetInt("Language")].isOn = true;
        if (PlayerPrefs.HasKey("HighScore"))
        {
            RefreshRecord();
        }
        if (PlayerPrefs.HasKey("TotalCoins"))
            CoinText.text = " X " + PlayerPrefs.GetInt("TotalCoins").ToString();
        if (!PlayerPrefs.HasKey("Sensibility"))
            PlayerPrefs.SetFloat("Sensibility", 1f);

        Sens.value = PlayerPrefs.GetFloat("Sensibility");
        
    }
    void RefreshRecord()
    {
        
        switch (PlayerPrefs.GetInt("Language"))
        {
            case 0:
                HighText.text = HighText.text = "RECORDE\n" + PlayerPrefs.GetFloat("HighScore").ToString();
                distanceBeetweenImages = imageRecord.transform.position.y - playButton.transform.position.y;
                HighText.transform.position = new Vector2(HighText.transform.position.x,distanceBeetweenImages+playButton.transform.position.y);
                break;
            case 1:
                HighText.text = HighText.text = "HIGH SCORE\n" + PlayerPrefs.GetFloat("HighScore").ToString();
                distanceBeetweenImages = imageRecord.transform.position.y - playButton.transform.position.y;
                HighText.transform.position = new Vector2(HighText.transform.position.x,distanceBeetweenImages+playButton.transform.position.y);
                break;
            case 2:
                HighText.text = HighText.text = "RECORD\n" + PlayerPrefs.GetFloat("HighScore").ToString();
                distanceBeetweenImages = imageRecord.transform.position.y - playButton.transform.position.y;
                HighText.transform.position = new Vector2(HighText.transform.position.x,distanceBeetweenImages+playButton.transform.position.y);
                break;
            case 3:
                HighText.text = HighText.text = "ENGERISTREMENT\n" + PlayerPrefs.GetFloat("HighScore").ToString();
                distanceBeetweenImages = imageRecord.transform.position.y - playButton.transform.position.y;
                HighText.transform.position = new Vector2(HighText.transform.position.x,distanceBeetweenImages+playButton.transform.position.y);
                break;
            case 4:
                HighText.text = HighText.text = "RECORD\n" + PlayerPrefs.GetFloat("HighScore").ToString();
                distanceBeetweenImages = imageRecord.transform.position.y - playButton.transform.position.y;
                HighText.transform.position = new Vector2(HighText.transform.position.x,distanceBeetweenImages+playButton.transform.position.y);
                break;
        }
    }
    public void SetLanguage(int lang)
    {
        allLangs = FindObjectsOfType<Language>();
        PlayerPrefs.SetInt("Language", lang);
        RefreshRecord();
        if (allLangs.Length > 0)
        {
            for (int i = 0; i < allLangs.Length; i++)
                allLangs[i].RefreshTexts();
        }


    }
    
    public void SetSensibility()
    {   
        PlayerPrefs.SetFloat("Sensibility", Sens.value);
        //Debug.Log(Sens.fillRect.);
    }
}
