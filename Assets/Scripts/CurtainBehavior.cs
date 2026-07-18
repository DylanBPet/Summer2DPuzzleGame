using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CurtainBehavior : MonoBehaviour
{
    public List<Sprite> curtainSprites;
    public SpriteRenderer curtainSR;
    public List<SpriteRenderer> curtainHitbox;

    public bool curtainOpening;
    public int curtainSpriteIndex;

    public GameObject wall3;

    public Vector2 mousePos;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        curtainSpriteIndex = 0;
        curtainOpening = true;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        if (wall3.activeInHierarchy && Mouse.current.leftButton.wasPressedThisFrame)
        {
            for (int i = 0; i < curtainHitbox.Count; i++)
            {
                if (curtainHitbox[i].bounds.Contains(mousePos))
                {
                    if (curtainOpening)
                    {
                        curtainSpriteIndex++;
                        curtainSR.sprite = curtainSprites[curtainSpriteIndex];
                        if (curtainSpriteIndex == 2)
                        {
                            curtainOpening = false;
                        }
                    }
                    else if (curtainOpening == false)
                    {
                        curtainSpriteIndex--;
                        curtainSR.sprite = curtainSprites[curtainSpriteIndex];
                        if (curtainSpriteIndex == 0)
                        {
                            curtainOpening = true;
                        }
                    }

                }
            }
        }
    }
}
