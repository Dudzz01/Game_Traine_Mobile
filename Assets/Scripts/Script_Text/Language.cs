using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Language : MonoBehaviour
{
    private Text Sentence;
    [SerializeField][TextArea] private string Port;
    [SerializeField] [TextArea] private string Engl;
    [SerializeField] [TextArea] private string Espa;
    [SerializeField] [TextArea] private string Fran;
    [SerializeField] [TextArea] private string Ital;

    private void Awake()
    {
        Sentence = GetComponent<Text>();
    }
    void Start()
    {
        RefreshTexts();
    }

    public void RefreshTexts()
    {
        if(Sentence != null)
        {
            switch (PlayerPrefs.GetInt("Language"))
            {
                case 0:
                    Sentence.text = Port;
                    break;
                case 1:
                    Sentence.text = Engl;
                    break;
                case 2:
                    Sentence.text = Espa;
                    break;
                case 3:
                    Sentence.text = Fran;
                    break;
                case 4:
                    Sentence.text = Ital;
                    break;
            }
        }
        else
        {
            Debug.Log(Sentence.name);
        }
    }
}
