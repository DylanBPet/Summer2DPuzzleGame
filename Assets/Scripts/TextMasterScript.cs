using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TextMasterScript : MonoBehaviour
{
    public TextMeshProUGUI onScreenText;
    public Image cloud1;
    public Image cloud2;

    Coroutine textVisibilityCoroutine;

    private Vector2 mousePos;

    //LargeDoorHitbox
    public SpriteRenderer largeDoorHitbox;

    //far away bookshelf
    public SpriteRenderer farBookshelfHitbox;

    //the main area
    public GameObject mainRoom;

    //window man hitbox
    public SpriteRenderer windowManHitbox;
    public GameObject windowZoomedIn;

    //glitched book dropoff hitbox
    public SpriteRenderer glitchedBookDropoff;
    public GameObject windowManZoomedIn;

    //marble face lock
    public SpriteRenderer glassCase;
    public GameObject glassCasehitbox;

    //outside window No man
    public GameObject windowNoMan;
    public SpriteRenderer windowNoManHitbox;

    //cat hint
    public GameObject catSettingHint;
    public SpriteRenderer catSettingHintSR;


    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed == true)
        {
            if (mainRoom.activeInHierarchy)
            {
                if (largeDoorHitbox.bounds.Contains(mousePos))
                {
                    MakeTextVisible("Its Locked");
                }
            
           
                if (farBookshelfHitbox.bounds.Contains(mousePos))
                {
                    MakeTextVisible("Its too far");
                }

                if (glassCase.bounds.Contains(mousePos) && glassCasehitbox.activeInHierarchy)
                {
                    MakeTextVisible("I'll need a key");
                }

                if (catSettingHint.activeInHierarchy)
                {
                    if (catSettingHintSR.bounds.Contains(mousePos))
                    {
                        MakeTextVisible("She wants something to play with...");
                    }
                }

            }
            else if (windowZoomedIn.activeInHierarchy)
            {
                if (windowManHitbox.bounds.Contains(mousePos))
                {
                    MakeTextVisible("Is someone there?");
                }
            }
            else if (windowManZoomedIn.activeInHierarchy)
            {
                if (glitchedBookDropoff.bounds.Contains(mousePos))
                {
                    MakeTextVisible("What does he want?");
                }
            }
            else if (windowNoMan.activeInHierarchy)
            {
                if (windowNoManHitbox.bounds.Contains(mousePos))
                {
                    MakeTextVisible("He's gone");
                }
            }
            
        }
    }
    public void MakeTextVisible(string newText)
    {
        onScreenText.text = newText;
        if (textVisibilityCoroutine != null)
        {
            StopCoroutine(textVisibilityCoroutine);
        }
        textVisibilityCoroutine = StartCoroutine(ChangeTextVisibility());
    }

    IEnumerator ChangeTextVisibility()
    {
        float a = 0;
        while (a < 1)
        {
            a += Time.deltaTime * 0.9f;
            cloud1.color = new Color(0, 0, 0, a);
            cloud2.color = new Color(0, 0, 0, a);
            onScreenText.color = new Color(1, 1, 1, a);
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        while (a > 0)
        {
            a -= Time.deltaTime * 0.9f;
            cloud1.color = new Color(0, 0, 0, a);
            cloud2.color = new Color(0, 0, 0, a);
            onScreenText.color = new Color(1, 1, 1, a);
            yield return null;
        }
    }
}
