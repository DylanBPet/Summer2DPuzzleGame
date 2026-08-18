
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;

public class Invintory : MonoBehaviour
{
    public List<GameObject> inWorldinventoryItems;

    public List<GameObject> inInventoryItems;

    public List<GameObject> inventorySlots;

    int?[] itemTagInInventory;
    bool[] isItemInInventory;

    private Vector2 mousePos;

    private int? itemPickedUpFromInventoryID;

    //key
    public SpriteRenderer item0DropOff;
    //eye
    public SpriteRenderer item1DropOff;
    public GameObject cat;

    //ITEM 2 has no drop off (blacklight)
    
    //glitched book
    public SpriteRenderer item3DropOff;

    //Binoculars
    public SpriteRenderer item4DropOff;
    public GameObject glitchedBookZoomedIn;
    public SpriteRenderer item4DropoffWindow;
    public GameObject zoomedInMan;
    public GameObject zoomedInWindow;

    public Vector2 usedItemsDropoff;

    //zoom in script
    public ZoomInScript zoomInScript;

    void Start()
    {

        itemTagInInventory = new int?[inventorySlots.Count];
        isItemInInventory = new bool[inWorldinventoryItems.Count];
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
            for (int i = 0; i < inWorldinventoryItems.Count; i++)
            {
               SpriteRenderer InvItemSr = inWorldinventoryItems[i].GetComponent<SpriteRenderer>();
                SpriteRenderer inInvItemSR = inInventoryItems[i].GetComponent<SpriteRenderer>();
                if (InvItemSr.bounds.Contains(mousePos) && isItemInInventory[i] == false && inWorldinventoryItems[i].activeInHierarchy)
                {
                    //the item is not in inventory
                    Debug.Log("player has clicked item number " + i);
                    for (int s = 0; s < itemTagInInventory.Length; s++)
                    {
                        if (itemTagInInventory[s] == null)
                        {
                            Debug.Log("item slot " + s + " has been filled");
                            //StartCoroutine(PickingUpItemAnim());
                            inInventoryItems[i].transform.position = inventorySlots[s].transform.position;
                            itemTagInInventory[s] = i;
                            isItemInInventory[i] = true;
                            inInventoryItems[i].SetActive(true);
                            inWorldinventoryItems[i].SetActive(false);
                            break;
                        }
                    }  
                }
                else if (inInvItemSR.bounds.Contains(mousePos) && isItemInInventory[i] == true)
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
        //this will be the item in inventory blinking for a second when you first pickit up
        yield return null;

    }


    //this controls what the item does when the player picks it up
    private void InventoryItemMovment(int? itemNumber, int invSlotNum)
    {
        int itemID = itemNumber.Value;
        if (Mouse.current.leftButton.isPressed)
        {
            //item is being carried around
            inInventoryItems[itemID].transform.position = mousePos;
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
        //Key
        if (itemID == 0)
        {
            if (item0DropOff.bounds.Contains(inInventoryItems[itemID].transform.position))
            {
                inInventoryItems[itemID].transform.position = usedItemsDropoff;

                Debug.Log("item 0 has been given away");

                itemTagInInventory[returnTag] = null;
            }
            else
            {
                //not in right spot, return to inventory slot
                inInventoryItems[itemID].transform.position = inventorySlots[returnTag].transform.position;
            }
        }
        //Eyeball
        else if (itemID == 1)
        {
            if (item1DropOff.bounds.Contains(inInventoryItems[itemID].transform.position))
            {
                //the cat will run away
                cat.SetActive(false);
                //make blacklight visible
                inWorldinventoryItems[2].SetActive(true);

                //get rid of the used item
                inInventoryItems[itemID].transform.position = usedItemsDropoff;

                Debug.Log("item 0 has been given away");

                itemTagInInventory[returnTag] = null;
            }
            else
            {
                inInventoryItems[itemID].transform.position = inventorySlots[returnTag].transform.position;
            }
               
        }
        //Blacklight
        else if (itemID == 2)
        {

            inInventoryItems[itemID].transform.position = inventorySlots[returnTag].transform.position;
            
        }
        //Glitched Book
        else if (itemID == 3)
        {
            if (item3DropOff.bounds.Contains(inInventoryItems[itemID].transform.position))
            {
                //remove dropped book and return player to the main scene, activate the "flames"... however you decide to show that
                itemTagInInventory[itemPickedUpFromInventoryID.Value] = null;
                inInventoryItems[3].SetActive(false);
            }
            else
            {
                inInventoryItems[itemID].transform.position = inventorySlots[returnTag].transform.position;
            }
        }
        //binoculars 
        else if (itemID == 4)
        {
            if (item4DropOff.bounds.Contains(inInventoryItems[itemID].transform.position))
            {
                zoomInScript.allWalls.SetActive(false);
                glitchedBookZoomedIn.SetActive(true);
                zoomInScript.SwitchUiCanvas();
            }
            else if (item4DropoffWindow.bounds.Contains(inInventoryItems[itemID].transform.position))
            {
                zoomedInWindow.SetActive(false);
                zoomedInMan.SetActive(true);
                zoomInScript.SwitchUiCanvas();
            }
            else
            {
                inInventoryItems[itemID].transform.position = inventorySlots[returnTag].transform.position;
            }
        }

    }
}
