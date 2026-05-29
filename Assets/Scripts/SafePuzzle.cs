
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SafePuzzle : MonoBehaviour
{
    public List<Sprite> NumSprites;
    public List<Sprite> PicSprites;
    public List<GameObject> SafeIconSlots;

    public List<int> currentSafeCombination;
    public List<int> safeAnswerCombination;

    private Vector2 mousePos;
    public SpriteRenderer unlockHandle;

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
                    currentSafeCombination[i] = s;
                    return;
                }
                else
                {
                    slotSprite.sprite = PicSprites[s + 1];
                    currentSafeCombination[i] = s;
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
                    currentSafeCombination[i] = s;
                    return;
                }
                else
                {
                    slotSprite.sprite = NumSprites[s + 1];
                    currentSafeCombination[i] = s;
                    return;
                }
            }
        }
    }

    public void SafeOpen(InputAction.CallbackContext context)
    {
        if (context.performed == true)
        {
            if (unlockHandle.bounds.Contains(mousePos))
            {
                for (int i = 0; i < currentSafeCombination.Count; i++)
                {
                    if (currentSafeCombination[i] != safeAnswerCombination[i])
                    {
                        Debug.Log("Answer Incorrect");
                        return;
                    }
                }
                Debug.Log("Answer Correct");
            }
        }
    }


}
