using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{

    //ui arrows
    public GameObject moveWallUi;
  

    //zoom in script holder
    public GameObject playerGO;

    //settings
    public GameObject fullSettings;
    public GameObject backButton;
    public GameObject settingsButton;

    public GameObject creditScreen;

    public TextMeshProUGUI sfxVolumeNumber;
    public TextMeshProUGUI musicVolumeNumber;

    //settingComponents
    public Slider sfxSlider;
    public Slider musicSlider;

    //things settings can change
    public AudioManager audioScript;

    private float displayedSfxVolumeNumber;
    private float displayedMusicVolumeNumber;

    ///////////////////////////////////////////////Hints
    public GameObject hintSettings;
    public GameObject hintSettingsButton;
    public List<GameObject> settingHintsTurnOn;


    void Update()
    {
        //change the decimal number to a whole number
        displayedSfxVolumeNumber = audioScript.sfxVolume * 100;
        displayedMusicVolumeNumber = audioScript.musicVolume * 100;

       
        //display that number                       F0 gets rid of decimals
        sfxVolumeNumber.text = displayedSfxVolumeNumber.ToString("F0");
        musicVolumeNumber.text = displayedMusicVolumeNumber.ToString("F0");

        //allow the slider to change the numbers
        audioScript.sfxVolume = sfxSlider.value;
        audioScript.musicVolume = musicSlider.value;
    }

    public void OpenSettings()
    {
        //cant interact with map
        moveWallUi.SetActive(false);
        
        playerGO.SetActive(false);

        //open settigns
        fullSettings.SetActive(true);

        //change buttons
        backButton.SetActive(true);
        settingsButton.SetActive(false);
        hintSettingsButton.SetActive(false);

    }

    public void CloseSettings()
    {
        //interact with map
        moveWallUi.SetActive(true);
        
        playerGO.SetActive(true);

        //close settigns
        fullSettings.SetActive(false);

        //also close hint settings
        hintSettings.SetActive(false);

        //change buttons
        backButton.SetActive(false);
        settingsButton.SetActive(true);
        hintSettingsButton.SetActive(true);
    }

    public void ToCredits()
    {
        backButton.SetActive(false);
        creditScreen.SetActive(true);
        fullSettings.SetActive(false);
    }

    public void BackToSettings()
    {
        backButton.SetActive(true);

        creditScreen.SetActive(false);

        fullSettings.SetActive(true);
    }

    public void OpenDylanLinkedin()
    {
        Application.OpenURL("https://www.linkedin.com/in/dylan-petroff-77248b2ba/");
    }

    public void OpenSabiLinkedin()
    {
        Application.OpenURL("https://www.linkedin.com/in/sabina-beisenbekova-245b893ab?utm_source=share_via&utm_content=profile&utm_medium=member_ios");
    }

    public void OpenJDSherbertITCH()
    {
        Application.OpenURL("https://jdsherbert.itch.io/");
    }

    public void OpenVixLinkedin()
    {
        Application.OpenURL("https://www.linkedin.com/in/vix-sak-1707a2252/");
    }
    
    public void ToHintSettings()
    {
        //cant interact with map
        moveWallUi.SetActive(false);

        playerGO.SetActive(false);

        //open hint settigns
        hintSettings.SetActive(true);

        //change buttons
        backButton.SetActive(true);
        settingsButton.SetActive(false);
        hintSettingsButton.SetActive(false);
    }

    public void TurnOnOrOffHints()
    {
        for (int i = 0; i < settingHintsTurnOn.Count; i++)
        {
            settingHintsTurnOn[i].SetActive(!settingHintsTurnOn[i].activeSelf);
        }
    }

}
