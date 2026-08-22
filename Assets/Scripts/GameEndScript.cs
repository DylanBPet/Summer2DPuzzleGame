using System.Collections;
using TMPro;
using UnityEngine;

public class GameEndScript : MonoBehaviour
{
    public GameObject gameEnd;

    public TextMeshProUGUI paragraphOne;
    public TextMeshProUGUI paragraphTwo;
    public TextMeshProUGUI paragraphThree;

    public GameObject buttonOne;
    public GameObject buttonTwo;
    public GameObject buttonThree;
    public GameObject buttonFour;

    public AudioManager audioScript;


    public GameObject newsPaper;
    public GameObject newsPaperEndDest;
    public GameObject newsPaperEndRotation;
    public GameObject newsPaperStartPos;

    public GameObject endCreditScreen;

    public void StartFinalCoroutine()
    {
        audioScript.StopBackgroundMusic();

        StartCoroutine(ShowParagraphOne());

        audioScript.PlayBackgroundNoise(audioScript.whiteNoise);
    }

    IEnumerator ShowParagraphOne()
    {
        yield return StartCoroutine(fadeIn(paragraphOne));
        //show button
        buttonOne.SetActive(true);
        yield return null;
    }

    public void StartParagraphTwo()
    {
        audioScript.PlaySoundEffect(audioScript.keyboardClicking);
        paragraphOne.color = Color.clear;
        buttonOne.SetActive(false);
        StartCoroutine(ShowParagraphTwo());
    }

    IEnumerator ShowParagraphTwo()
    {
        yield return StartCoroutine(fadeIn(paragraphTwo));
        //show button
        buttonTwo.SetActive(true);
        yield return null;
    }

    public void StartParagraphThree()
    {
        audioScript.PlaySoundEffect(audioScript.keyboardClicking);
        paragraphTwo.color = Color.clear;
        buttonTwo.SetActive(false);
        StartCoroutine(ShowParagraphThree());
    }

    IEnumerator ShowParagraphThree()
    {
        yield return StartCoroutine(fadeIn(paragraphThree));
        //show button
        buttonThree.SetActive(true);
        yield return null;
    }

    IEnumerator fadeIn(TextMeshProUGUI paragraph)
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * 0.33f;
            paragraph.color = new Color(1, 1, 1, t);
            yield return null;
        }  
    }

    public void ThrownNewspaper()
    {
        buttonThree.SetActive(false);
        audioScript.PlaySoundEffect(audioScript.keyboardClicking);
        buttonThree.SetActive(false);

       //the newspaper gets thrown onto the screen above the words of paragraph 3
       newsPaper.SetActive(true);
        StartCoroutine(ThrowNewsPaper());
        StartCoroutine(RotateNewsPaper());

    }

    IEnumerator ThrowNewsPaper()
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * 0.8f;
            newsPaper.transform.position = Vector2.Lerp(newsPaperStartPos.transform.position, newsPaperEndDest.transform.position, t);
            yield return null;
        }
        
    }

    IEnumerator RotateNewsPaper()
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * 0.5f;
            newsPaper.transform.rotation = Quaternion.Lerp(newsPaperStartPos.transform.rotation, newsPaperEndRotation.transform.rotation, t);
            yield return null;
        }
        buttonFour.SetActive(true);
    }

    public void ShowCredits()
    {
        endCreditScreen.SetActive(true);
        gameEnd.SetActive(false);
    }
}