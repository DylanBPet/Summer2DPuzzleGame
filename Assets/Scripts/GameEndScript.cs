using System.Collections;
using TMPro;
using UnityEngine;

public class GameEndScript : MonoBehaviour
{
    public TextMeshProUGUI paragraphOne;
    public TextMeshProUGUI paragraphTwo;
    public TextMeshProUGUI paragraphThree;

    public GameObject buttonOne;
    public GameObject buttonTwo;
    public GameObject buttonThree;

    public void StartFinalCoroutine()
    {
        StartCoroutine(ShowParagraphOne());
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
        }
        yield return null;
    }

    public void ThrownNewspaper()
    {
        buttonThree.SetActive(false);
        //make the newspaper spin
    }
}