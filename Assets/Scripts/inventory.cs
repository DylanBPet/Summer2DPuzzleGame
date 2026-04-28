
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Invintory : MonoBehaviour
{
    public List<GameObject> InventoryItems;

    public List<Sprite> InventorySprites;

    public List<GameObject> InventorySlots;

    private Vector2 mousePos;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    }

    public void ItemCollectied(InputAction.CallbackContext context)
    {
        if (context.performed == true)
        {
            for (int i = 0; i < InventoryItems.Count; i++)
            {
               SpriteRenderer InvItemSr = InventoryItems[i].GetComponent<SpriteRenderer>();
                if (InvItemSr.bounds.Contains(mousePos))
                {
                    for (int s = 0; s < InventorySlots.Count; s++)
                    {
                        SpriteRenderer InvSlotSr = InventorySlots[s].GetComponent<SpriteRenderer>();
                        if (InvSlotSr.sprite == null)
                        {
                            InvSlotSr.sprite = InventorySprites[i];
                            return;
                        }

                    }  
                }
            }
        }
    }
}
