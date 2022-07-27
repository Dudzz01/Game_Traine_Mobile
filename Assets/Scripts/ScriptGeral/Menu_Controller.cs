using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu_Controller : MonoBehaviour
{
    [SerializeField] private Text HighText;
    [SerializeField] private Text CoinText;

    [SerializeField] private Animator ShopAnim;
    [SerializeField] private Animator CreditsAnim;

    [SerializeField] private GameObject CreditsButton;
    [SerializeField] private GameObject ShopButton;
    private bool ButtonState = true;
    public void startGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void SetAnimShop(string anim)
    {
        ButtonState = !ButtonState;
        ShopAnim.SetTrigger(anim);//Chamada da animacao pelo parametro, que e declarado no OnClick do botao
        CreditsButton.SetActive(ButtonState);//Apenas para que nao aconteca interferencia entre as telas, desativo o botao dos 
    }//creditos enquanto a tela da loja esta ativa, e vice-versa
    public void SetAnimCredits(string anim)
    {
        ButtonState = !ButtonState;
        CreditsAnim.SetTrigger(anim);
        ShopButton.SetActive(ButtonState);
    }
    void Start()
    {
        if (PlayerPrefs.HasKey("HighScore"))
            HighText.text = "RECORDE\n" + PlayerPrefs.GetFloat("HighScore").ToString("F2");
        if (PlayerPrefs.HasKey("TotalCoins"))
            CoinText.text = " X " + PlayerPrefs.GetInt("TotalCoins").ToString();
    }
    /*
    // Update is called once per frame
    void Update()
    {
        
    }
    */
}
