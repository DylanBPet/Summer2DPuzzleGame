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
            a += Time.deltaTime * 0.8f;
            cloud1.color = new Color(0, 0, 0, a);
            cloud2.color = new Color(0, 0, 0, a);
            onScreenText.color = new Color(1, 1, 1, a);
            yield return null;
        }

        yield return new WaitForSeconds(1);

        while (a > 0)
        {
            a -= Time.deltaTime * 0.8f;
            cloud1.color = new Color(0, 0, 0, a);
            cloud2.color = new Color(0, 0, 0, a);
            onScreenText.color = new Color(1, 1, 1, a);
            yield return null;
        }
    }
}
