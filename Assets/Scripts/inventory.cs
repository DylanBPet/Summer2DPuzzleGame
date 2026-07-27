
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;

public class Invintory : MonoBehaviour
{
    public List<GameObject> inventoryItems;

    public List<GameObject> inventorySlots;

    int?[] itemTagInInventory;
    bool[] isItemInInventory;

    private Vector2 mousePos;

    private int? itemPickedUpFromInventoryID;

    public SpriteRenderer item0DropOff;

    public Vector2 usedItemsDropoff;

    void Start()
    {

        itemTagInInventory = new int?[inventorySlots.Count];
        isItemInInventory = new bool[inventoryItems.Count];
        for (int i = 0; i < itemTagInInventory.Length; i++)
        {
            itemTagInInventory[i] = null;
        }
        for (int i = 0; i < isItemInInventory.Length; i++)
        {
            isItemInInventory[i] = false;
        }

    }
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        if (itemPickedUpFromInventoryID != null)
        {
            int inventoryItemID = itemPickedUpFromInventoryID.Value;
            for (int i = 0; i < itemTagInInventory.Length; i++)
            {
                if (itemTagInInventory[i] == inventoryItemID)
                {
                    InventoryItemMovment(inventoryItemID, i);
                }
            }
        }

    }

    //the means of clicking the item from the map, but also when clicking it in inventory
    public void ItemCollectied(InputAction.CallbackContext context)
    {
        if (context.performed == true)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
               SpriteRenderer InvItemSr = inventoryItems[i].GetComponent<SpriteRenderer>();
                if (InvItemSr.bounds.Contains(mousePos) && isItemInInventory[i] == false)
                {
                    //the item is not in inventory
                    Debug.Log("player has clicked item number " + i);
                    for (int s = 0; s < itemTagInInventory.Length; s++)
                    {
                        if (itemTagInInventory[s] == null)
                        {
                            Debug.Log("item slot " + s + " has been filled");
                            StartCoroutine(PickingUpItemAnim(s, i));
                            itemTagInInventory[s] = i;
                            isItemInInventory[i] = true;
                            break;
                        }
                    }  
                }
                else if (InvItemSr.bounds.Contains(mousePos) && isItemInInventory[i] == true)
                {
                    //the item is already in inventory
                    itemPickedUpFromInventoryID = i;
                }
            }
        }
    }

    //the "animation" of the item going into inventory
    IEnumerator PickingUpItemAnim(int invSlotNum, int itemNum)
    {
        float t = 0;
        Vector2 originalPos = inventoryItems[itemNum].transform.position;
        while (t < 1)
        {
            inventoryItems[itemNum].transform.position = Vector2.Lerp(originalPos, inventorySlots[invSlotNum].transform.position, t);
            t += 0.8f * Time.deltaTime;
            yield return null;
        }
        itemTagInInventory[invSlotNum] = itemNum;

    }


    //this controls what the item does when the player picks it up
    private void InventoryItemMovment(int? itemNumber, int invSlotNum)
    {
        int itemID = itemNumber.Value;
        if (Mouse.current.leftButton.isPressed)
        {
            //item is being carried around
            inventoryItems[itemID].transform.position = mousePos;
        }
        else
        {
            //item has been dropped
            ItemDropped(itemID, invSlotNum);
            itemPickedUpFromInventoryID = null;
        }
    }

    //this is checked when an item is picked up from inventory, then dropped. when dropped, it checks this list for what to do
    private void ItemDropped(int itemID, int returnTag)
    {
        if (itemID == 0)
        {
            if (item0DropOff.bounds.Contains(inventoryItems[itemID].transform.position))
            {
                inventoryItems[itemID].transform.position = usedItemsDropoff;

                Debug.Log("item 0 has been given away");

                itemTagInInventory[returnTag] = null;
            }
            else
            {
                //not in right spot, return to inventory slot
                inventoryItems[itemID].transform.position = inventorySlots[returnTag].transform.position;
            }
        }
        else if (itemID == 1)
        {
            inventoryItems[itemID].transform.position = inventorySlots[returnTag].transform.position;
        }
        else if (itemID == 2)
        {
            inventoryItems[itemID].transform.position = inventorySlots[returnTag].transform.position;
        }
    }
}
