
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    public GameObject zoomedOutBook;
    public GameObject glitchedBookOrigin;

    //Binoculars
    public SpriteRenderer item4DropOff;
    public GameObject glitchedBookZoomedIn;
    public SpriteRenderer item4DropoffWindow;
    public GameObject zoomedInMan;
    public GameObject zoomedInWindow;

    public Vector2 usedItemsDropoff;

    //zoom in script
    public ZoomInScript zoomInScript;

    //The hitbox to move the inv
    public SpriteRenderer invMoveHitbox;

    //the whole inventory manager (will move everything involved and in the invintory)
    public GameObject invManagerGO;

    public GameObject rightUIArrow;

    //the movement for the background of the inv coroutines
    private Coroutine moveInvOut;
    private Coroutine moveInvIn;

    //the lerp objects for the inv to move to
    public GameObject invLerpOutObject;
    public GameObject invLerpInObject;

    public GameObject arrowLerpOutObject;
    public GameObject arrowLerpInObject;

    //text master script
    public TextMasterScript textScript;

    //is the marble head case unlocked
    private bool caseUnlocked = false;
    public GameObject headCaseHitbox;
    public GameObject marbleHeadZoomedIn;

    //marble head smashes glass
    public SpriteRenderer winowBreakHitbox;
    public GameObject brokenWindow;
    public GameObject brokenWindowHitbox;
    public GameObject nonBrokenWindow;

    //torn photo dropoff
    public SpriteRenderer tornPhotoDropoff;
    public SpriteRenderer zoomedInTornPhotoDropoff;
    public GameObject zoomeInTornPhoto;
    public GameObject showPhoto1;
    public GameObject showPhoto2;
    public GameObject showPhoto3;
    //tornPhoto zoomed in changes
    public GameObject zoomedInphoto1;
    public GameObject zoomedInphoto2;
    public GameObject zoomedInphoto3;

    //fire effects script
    public FireEffects fireEffects;
    //zoominscript
    public ZoomInScript zoominscript;
    //audioScript
    public AudioManager audioScript;

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
        
        //move inv out
        if (invMoveHitbox.bounds.Contains(mousePos))
        {
            //coroutine larp inv over
            //move the whole INVENTORY MANAGER
            if (moveInvOut != null)
            {

            }
            else if (moveInvOut == null && moveInvIn == null) 
            {
                moveInvOut = StartCoroutine(MoveInvOut());
            }
        }
        //move inv in
        else
        {
            //lerp it back
            //move the whole INVENTORY MANAGER
            if (moveInvIn != null || moveInvOut == null)
            {

            }
            else if (moveInvIn == null && moveInvOut != null)
            {
                moveInvIn = StartCoroutine(MoveInvIn());
            }
        }

    }

    IEnumerator MoveInvOut()
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * 3f;
            invManagerGO.transform.position = Vector2.Lerp(invLerpInObject.transform.position, invLerpOutObject.transform.position, t);
            rightUIArrow.transform.position = Vector2.Lerp(arrowLerpInObject.transform.position, arrowLerpOutObject.transform.position, t);
            yield return null;
        }
        
    }

    IEnumerator MoveInvIn()
    {
        moveInvOut = null;
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * 3f;
            invManagerGO.transform.position = Vector2.Lerp(invLerpOutObject.transform.position, invLerpInObject.transform.position, t);
            rightUIArrow.transform.position = Vector2.Lerp(arrowLerpOutObject.transform.position, arrowLerpInObject.transform.position, t);
            yield return null;
        }
        moveInvIn = null;
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
                            //Marble head
                            if (i == 5)
                            {
                                if (caseUnlocked == true)
                                {
                                    inInventoryItems[i].transform.position = inventorySlots[s].transform.position;
                                    itemTagInInventory[s] = i;
                                    isItemInInventory[i] = true;
                                    inInventoryItems[i].SetActive(true);
                                    inWorldinventoryItems[i].SetActive(false);

                                    marbleHeadZoomedIn.SetActive(false);
                                }
                                else
                                {
                                    return;
                                }
                            }
                            Debug.Log("item slot " + s + " has been filled");
                            //StartCoroutine(PickingUpItemAnim());
                            inInventoryItems[i].transform.position = inventorySlots[s].transform.position;
                            itemTagInInventory[s] = i;
                            isItemInInventory[i] = true;
                            inInventoryItems[i].SetActive(true);
                            inWorldinventoryItems[i].SetActive(false);

                            //AUDIO GO HERE
                            audioScript.PlaySoundEffect(audioScript.itemPickup);

                            //the glitched book
                            if (i == 3)
                            {
                                zoomedOutBook.SetActive(false);
                                glitchedBookOrigin.transform.position = inventorySlots[s].transform.position;
                            }

                            //show that the inv goes past just the few items
                            if (s >= 5)
                            {
                                moveInvOut = StartCoroutine(MoveInvOut());
                            }
                            
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

                caseUnlocked = true;

                headCaseHitbox.SetActive(false);

                textScript.MakeTextVisible("Unlocked");

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
                inWorldinventoryItems[8].SetActive(true);

                //get rid of the used item
                inInventoryItems[itemID].transform.position = usedItemsDropoff;

                Debug.Log("item 1 has been given away");

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
                itemTagInInventory[returnTag] = null;
                inInventoryItems[itemID].SetActive(false);

                

                //fire effects start
                fireEffects.FireStarted();
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
                inInventoryItems[itemID].transform.position = inventorySlots[returnTag].transform.position;
            }
            else if (item4DropoffWindow.bounds.Contains(inInventoryItems[itemID].transform.position))
            {
                zoomedInWindow.SetActive(false);
                zoomedInMan.SetActive(true);
                zoomInScript.SwitchUiCanvas();
                inInventoryItems[itemID].transform.position = inventorySlots[returnTag].transform.position;
            }
            else
            {
                inInventoryItems[itemID].transform.position = inventorySlots[returnTag].transform.position;
            }
        }
        //Marble Head
        else if (itemID == 5)
        {
            if (winowBreakHitbox.bounds.Contains(inInventoryItems[itemID].transform.position))
            {
                inInventoryItems[itemID].SetActive(false);
                nonBrokenWindow.SetActive(false);
                brokenWindow.SetActive(true);
                brokenWindowHitbox.SetActive(true);
            }
            else
            {
                inInventoryItems[itemID].transform.position = inventorySlots[returnTag].transform.position;
            }
        }
        //Torn Photos
        else if (itemID == 6 || itemID == 7 || itemID == 8)
        {
            if (tornPhotoDropoff.bounds.Contains(inInventoryItems[itemID].transform.position) || zoomedInTornPhotoDropoff.bounds.Contains(inInventoryItems[itemID].transform.position) && zoomeInTornPhoto.activeInHierarchy)
            {
                //photo 1
                if (itemID == 6)
                {
                    showPhoto1.SetActive(true);
                    itemTagInInventory[returnTag] = null;
                    inInventoryItems[itemID].SetActive(false);
                    inInventoryItems[itemID].transform.position = usedItemsDropoff;
                    zoomedInphoto1.SetActive(true);

                }
                //photo 2
                else if (itemID == 7)
                {
                    showPhoto2.SetActive(true);
                    itemTagInInventory[returnTag] = null;
                    inInventoryItems[itemID].SetActive(false);
                    inInventoryItems[itemID].transform.position = usedItemsDropoff;
                    zoomedInphoto2.SetActive(true);
                }
                //photo 3
                else if (itemID == 8)
                {
                    showPhoto3.SetActive(true);
                    itemTagInInventory[returnTag] = null;
                    inInventoryItems[itemID].SetActive(false);
                    inInventoryItems[itemID].transform.position = usedItemsDropoff;
                    zoomedInphoto3.SetActive(true);
                }
            }
            else
            {
                inInventoryItems[itemID].transform.position = inventorySlots[returnTag].transform.position;
            }
        }

    }
}
