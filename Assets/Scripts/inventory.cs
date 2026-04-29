
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Invintory : MonoBehaviour
{
    public List<GameObject> InventoryItems;

    public List<GameObject> InventorySlots;

    public List<Vector2> InvSlotsOrignialPositions;

    private Vector2 mousePos;

    void Start()
    {
        for (int i = 0; i < InventorySlots.Count; i++)
        {
            InvSlotsOrignialPositions.Add(InventorySlots[i].transform.position);
        }
    }
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        for (int i = 0; i < InventorySlots.Count; i++)
        {
            SpriteRenderer InvSlotSr = InventorySlots[i].GetComponent<SpriteRenderer>();
            if (InvSlotSr.bounds.Contains(mousePos) && Mouse.current.leftButton.isPressed)
            {
                InventorySlots[i].transform.position = mousePos;
            }
            else
            {
                InventorySlots[i].transform.position = InvSlotsOrignialPositions[i];
            }
        
    }
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
                            InvSlotSr.sprite = InvItemSr.sprite;
                            return;
                        }
                    }  
                }
            }
        }
    }
}
