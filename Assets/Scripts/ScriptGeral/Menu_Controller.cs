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
    public void startGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void SetAnimShop(string anim)
    {
        ShopAnim.SetTrigger(anim);
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
