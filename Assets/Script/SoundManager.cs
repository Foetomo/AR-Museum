using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Image soundOnIcon;
    [SerializeField] Image soundOffIcon;
    [SerializeField] AudioSource bgmSource;


    private bool muted = false;

    private void Awake()
    {
        bgmSource = GameObject.FindGameObjectWithTag("bgm").GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
        if (!PlayerPrefs.HasKey("muted"))
        {
            PlayerPrefs.SetInt("muted", 0);
            Load();
        }
        else
        {
            Load();
        }
        UpdateButtonIcon();
        PlayStopBGM();
    }

    void PlayStopBGM() // buat cek lagi di mute atau ngga.
    {
        if (muted) // kalo iya di mute audio source bgmnya
        {
            bgmSource.Pause();
        }
        else // kalo ngga di play audio source bgmnya
        {
            bgmSource.Play();
        }
    }

    public void OnButtonPress()
    {
        if (muted == false)
        {
            muted = true;
        }
        else
        {
            muted = false;
        }
        PlayStopBGM();
        Save();
        UpdateButtonIcon();
    }

    public void SimpanBGM()
    {
        Save();
    }

    private void UpdateButtonIcon()
    {
        if(muted == false)
        {
            soundOnIcon.enabled = true;
            soundOffIcon.enabled = false;
        }
        else
        {
            soundOnIcon.enabled = false;
            soundOffIcon.enabled = true;
        }
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
