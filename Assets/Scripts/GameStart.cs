using System.Collections;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public GameObject gameStartScreen;
    public GameObject titleCard;
    public GameObject mainScene;
    public GameObject textCanvas;

    public AudioManager audioScript;
    void Start()
    {
        StartCoroutine(StartScreen());
    }

    IEnumerator StartScreen()
    {
        audioScript.PlayBackgroundNoise(audioScript.whiteNoise);
        yield return new WaitForSeconds(1f);
        audioScript.PlaySoundEffect(audioScript.keyboardClicking);
        yield return new WaitForSeconds(0.2f);
        titleCard.SetActive(true);
        yield return new WaitForSeconds(3f);
        audioScript.PlaySoundEffect(audioScript.keyboardClicking);
        yield return new WaitForSeconds(0.2f);
        textCanvas.SetActive(true);
        yield return new WaitForSeconds(4f);
        audioScript.PlaySoundEffect(audioScript.keyboardClicking);
        audioScript.StopBackgroundMusic();
        yield return new WaitForSeconds(0.1f);
        audioScript.PlayBackgroundNoise(audioScript.backgroundMusic);
        yield return new WaitForSeconds(0.1f);
        mainScene.SetActive(true);
        gameStartScreen.SetActive(false);
        yield return null;
    }
}
