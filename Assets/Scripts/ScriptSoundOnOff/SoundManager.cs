using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
        [SerializeField] Image soundOn;
        [SerializeField] Image soundOff;
        private bool muted = false;

    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("muted"))
        {
                PlayerPrefs.SetInt("muted", 0);
                Load();
        }
        else
            Load();
        UpdateButtonIcon();
        AudioListener.pause = muted;
    }

    public void OnButtonPress()
    {
        if (muted == false)
        {
            muted = true;
            AudioListener.pause = true;
        }

        else{
            muted = false;
            AudioListener.pause = false;
        }
        UpdateButtonIcon();
    }
    private void UpdateButtonIcon()
    {
        if (muted == false)
        {
            soundOn.enabled = true;
            soundOff.enabled = false;
        }
        else{
            soundOn.enabled = false;
            soundOff.enabled = true;
        }
        Save();
    }
    private void Load()
    {
        muted = PlayerPrefs.GetInt("muted") == 1;
    }
    private void Save()
    {
        PlayerPrefs.SetInt("muted", muted ? 1 : 0);
    }
}