
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SafePuzzle : MonoBehaviour
{
    public List<Sprite> NumSprites;
    public List<Sprite> PicSprites;
    public List<GameObject> SafeIconSlots;

    private Vector2 mousePos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    }

    public void SwitchSafeIcons(InputAction.CallbackContext context)
    {
        if (context.performed == true)
        {
            for (int i = 0; i < SafeIconSlots.Count; i++)
            {
                SpriteRenderer SafeTouchableIcons = SafeIconSlots[i].GetComponent<SpriteRenderer>();

                if (SafeTouchableIcons.bounds.Contains(mousePos))
                {
                    if (i <= 3) //Its the top row
                    {
                        SwitchingTopIcons(i, SafeTouchableIcons);
                    }
                    else if (i >= 4) //its the bottom row
                    {
                        SwitchingBotIcons(i, SafeTouchableIcons);
                    }
                } 
            }
        }
    }

    public void SwitchingTopIcons(int i, SpriteRenderer slotSprite)
    {
        for (int s = 0; s < PicSprites.Count; s++)
        {
            if (PicSprites[s] == slotSprite.sprite)
            {
                Debug.Log("sprite " + i + " Equals Index " + s);
                if (s == 3)
                {
                    slotSprite.sprite = PicSprites[0];
                    return;
                }
                else
                {
                    slotSprite.sprite = PicSprites[s + 1];
                    return;
                }
            }
        }
    }

    public void SwitchingBotIcons(int i, SpriteRenderer slotSprite)
    {
        for (int s = 0; s < NumSprites.Count; s++)
        {
            if (NumSprites[s] == slotSprite.sprite)
            {
                Debug.Log("sprite " + i + " Equals Index " + s);
                if (s == 9)
                {
                    slotSprite.sprite = NumSprites[0];
                    return;
                }
                else
                {
                    slotSprite.sprite = NumSprites[s + 1];
                    return;
                }
            }
        }
    }

}
