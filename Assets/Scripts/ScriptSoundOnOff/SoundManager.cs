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
    }
}
