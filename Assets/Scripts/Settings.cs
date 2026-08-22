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

    }

    public void CloseSettings()
    {
        //interact with map
        moveWallUi.SetActive(true);
        
        playerGO.SetActive(true);

        //close settigns
        fullSettings.SetActive(false);

        //change buttons
        backButton.SetActive(false);
        settingsButton.SetActive(true);
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

}
