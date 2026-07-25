using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextMasterScript : MonoBehaviour
{
    public TextMeshProUGUI onScreenText;
    public Image cloud1;
    public Image cloud2;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MakeTextVisible(string newText)
    {
        onScreenText.text = newText;
        StartCoroutine(ChangeTextVisibility());
    }

    IEnumerator ChangeTextVisibility()
    {
        float a = 0;
        while (a < 1)
        {
            a += 0.2f;
            cloud1.color = new Color(1, 1, 1, a);
            cloud2.color = new Color(1, 1, 1, a);
            onScreenText.color = new Color(1, 1, 1, a);
            yield return null;
        }

        yield return new WaitForSeconds(2);

        while (a > 0)
        {
            a -= 0.2f;
            cloud1.color = new Color(1, 1, 1, a);
            cloud2.color = new Color(1, 1, 1, a);
            onScreenText.color = new Color(1, 1, 1, a);
            yield return null;
        }
    }
}
