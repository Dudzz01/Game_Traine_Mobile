using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu_Controller : MonoBehaviour
{
    [SerializeField] private Text HighText;
    public void startGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    
    void Start()
    {
        if (PlayerPrefs.HasKey("HighScore"))
            HighText.text = "RECORDE\n" + PlayerPrefs.GetFloat("HighScore").ToString("F2");
    }
    /*
    // Update is called once per frame
    void Update()
    {
        
    }
    */
}
