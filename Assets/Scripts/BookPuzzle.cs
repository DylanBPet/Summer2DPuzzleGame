using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BookPuzzle : MonoBehaviour
{
    public List<GameObject> books;

    private Vector2 mousePos;

    private int? pickedUpBookNumber = null;
    private Vector3 originalPos = new Vector3(0f, 0f, 0f);

    ///////// ROW AND SECTION VARIABLES
    public List<GameObject> rowSections;

    //used to track which book is where and to check for the answers
    //tracks the book number that is in that slot, or is null if empty
    private int?[] bookInSlot;
    //the slot that the currently held book has come from
    private int pickedUpSlot = -1; 

    //track the rows and columns
    private const int slotsPerRow = 5;
    private const float occupiedThreshold = 0.1f;
    private const float snapRange = 3f;

    public GameObject wall3ZoomedIn;
    void Start()
    {
        pickedUpBookNumber = null;

        wall3ZoomedIn.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        //track where it is when picked up in case they drag to invalid location

        //track what row it is on, lock the y axis for books on that row

        //Know where other books are so they can move over

        //Track how many books r on that row so they can have a max number

        if (pickedUpBookNumber != null)
        {
            //we set them in global variable so it can be used over multiple frames but they are only set once (in callbackcontext)
            BookPickedUp(pickedUpBookNumber);
            //doubley make sure to set BOTH to NULL during the PuttingDownBook callbackcontext so it is reset for the next book
        }

        

    }
    public void PickingUpBooks(InputAction.CallbackContext context)
    {
        EnsureInitialized();
        //When mouse is clicked and on top of a book in the array, that book becomes PickedUp
        for (int i = 0; i < books.Count; i++)
        {
            //get the sprite renderer for the current book
            SpriteRenderer bsr = books[i].GetComponent<SpriteRenderer>();

            //if current book has mouse on it and context is started (mouse has been pressed this frame)
            if (bsr.bounds.Contains(mousePos) && context.started == true)
            {
                //set the int? to be the number in the book list i as a reference
                pickedUpBookNumber = i;

                //track the original position of the book
                originalPos = books[i].transform.position;

                //Debug.Log("Book " + pickedUpBookNumber + " Has Been Picked Up");

                //find the selected book and what slot number it is in
                pickedUpSlot = FindSlotOfBook(i);

                if (pickedUpSlot != -1)
                {
                    //empty the slot of the book that has just been picked up 
                    //so that it == null, that way the code knows that spot is not OPEN for another book later or if the picked up book is placed in the same row (or the same book if it returns to originalPos)
                    bookInSlot[pickedUpSlot] = null;
                }
                break;
            }
        }
        
    }
    public void PuttingDownBook(InputAction.CallbackContext context)
    {
        EnsureInitialized();
        if (context.canceled == true)
        {
            if (pickedUpBookNumber == null)
            {
                //do nothing
                return;
            }

            //Debug.Log("Book " + pickedUpBookNumber + " Has Been Placed");

            int bookNumber = pickedUpBookNumber.Value;

            //set the indext to out of range so there is out of range errors
            int closestIndex = -1;

            //set the closest distance to THE MAX so there is no errors
            float closestDist = float.MaxValue;

            for (int i = 0; i < rowSections.Count; i++)
            {
                float dist = Vector2.Distance(books[bookNumber].transform.position, rowSections[i].transform.position);
                //if the new distance is closer than the previous ones, replace the index and closest dist
                if (dist < closestDist)
                {
                    closestDist = dist;
                    closestIndex = i;
                }
            }

            bool placed = false;

            //Debug.Log("ClosestIndex= " + closestIndex + " ClosestDist= " + closestDist + "SnapRange= " + snapRange);

            if (closestIndex != -1 && closestDist < snapRange)
            {
                //figure out the bounds of the row this slot belongs to

                //This takes the closest number the book is on (between 0 and 14) (1 for each bookshelf slot) 
                //and devides it by 5 (how many are in each row), c# will turn this into a whole number because it is an INT not a FLOAT
                //so when we devide anything less than 5 it will = 0 to say "Its on row 0!"
                //This is the same for row 1, anything above 5 will round to 1 to say "Its on row 1!" and so on
                int rowIndex = closestIndex / slotsPerRow;

                //we then need to take that index we just got and make it back into FIRST indext of the row (we do this to keep track of what row)
                //we JUST made the rowIndex a single digit number to say what row we are on (0, 1, 2) now we need to say what is the START ROW NUMBER (0, 5, 10)
                int rowStart = rowIndex * slotsPerRow;

                //we then add the number of slots per row to get the row length. in the previous step we find the start of the row, now we need to find the end (4, 9, 14)
                //we subract 1 from it because we are removing 1 book when we pick it up
                int rowEnd = rowStart + slotsPerRow - 1;

                //Debug.Log("rowIndex= " + rowIndex + " rowStart= " + rowStart + " rowEnd " + rowEnd);

                if (MakeRoomAtSlot(closestIndex, rowStart, rowEnd))
                {
                    //since makeRoomAtSlot has passed as true, there is now an open spot to place the book
                    //we can now place the book in the desired spot and set the bookInSlot int to the correct number
                    bookInSlot[closestIndex] = bookNumber;
                    books[bookNumber].transform.position = rowSections[closestIndex].transform.position;
                    placed = true;
                }
            }

            if (!placed)
            {
                //put the book back where it came from
                if (pickedUpSlot != -1)
                {
                    //reset the book int back into its original slot number
                    bookInSlot[pickedUpSlot] = bookNumber;
                }
                //place book transform back to original position
                books[bookNumber].transform.position = originalPos;
            }

            pickedUpBookNumber = null;
            pickedUpSlot = -1;
        }
    }

    
    public void BookPickedUp(int? i)
    {
        int bookNumber = i.Value;
        books[bookNumber].transform.position = mousePos;
    }

   private int FindSlotOfBook(int bookNumber)
   {
        //this gives the code the book number that we are picking up and returns the slot that the book is in
        //if we pick up book 4 it checks all the slots until the int matches 4
        //slot 1 = int 7, slot 2 = null, slot 3 = 12, slot 4 = 8, slot 5 = 4 (so the code knows we have just picked book 4, and that it was sitting in slot 5)
        for (int i = 0; i < bookInSlot.Length; i++)
        {
            if (bookInSlot[i] == bookNumber)
            {
                return i;
            }
        }
        //if we cannot find the book we return -1 to say the book was not found
        return -1;
   }

    private bool MakeRoomAtSlot(int targetSlot, int rowStart, int rowEnd)
    {
        //that slot is already open, do nothing
        if (bookInSlot[targetSlot] == null)
        {
            return true;
        }

        //look for the first empty slot to the right
        int rightEmpty = -1;
        
        //check each slot between the slot the book is being placed in and the slots to the right of the book (up until the end of the row)
        for (int i = targetSlot + 1; i <= rowEnd; i++)
        {
            //if any slots are null (meaning there is not book there) set rightEmpty to that slot and break the for loop
            if (bookInSlot[i] == null)
            {
                rightEmpty = i;
                break;
            }
        }

        //if rightEmpty is not -1 (meaning there was an empty spot in it)
        if (rightEmpty != -1)
        {
            //shift every book between targetSlot and the empty slot one to the right
            for (int i = rightEmpty; i > targetSlot; i--)
            {
                //set the int of movingBook to the same int value of the book to the LEFT of it (we do this because we are moving the books to the right
                //so the new values of the books to  the right will be the old values of the books to the left)
                int movingBook = bookInSlot[i - 1].Value;

                //set the value (int) of the book in i to the NEW value of the book (so we can track where the books go)
                bookInSlot[i] = movingBook;

                //move the book in the slot to the new spot
                books[movingBook].transform.position = rowSections[i].transform.position;

                //start the loop over until all books are moved over
            }
            //make the slot we are setting the book into null and pass true (we will set the new value in "PuttingDownBook")
            bookInSlot[targetSlot] = null;
            return true;
        }

        //no room to the right, try the first empty slot to the left
        int leftEmpty = -1;

        //check each slot between the target slot and the leftmost slot in that row (rowstart) and look for any slots that are null
        for (int i = targetSlot - 1; i >= rowStart; i--)
        {
            if (bookInSlot[i] == null)
            {
                //once it finds an open spot to the left it sets leftEmpty as that value and breaks the loop
                leftEmpty = i;
                break;
            }
        }

        if (leftEmpty != -1)
        {
            for (int i = leftEmpty; i < targetSlot; i++)
            {
                //move each book (gameobject), and bookInSlot(int) to the left, keeping track of where the book are and moving them over
                int movingBook = bookInSlot[i + 1].Value;
                bookInSlot[i] = movingBook;
                books[movingBook].transform.position = rowSections[i].transform.position;
            }
            //make the bookInSlot(int) null so we can now place the book and int into that slot (we will set the new value in "PuttingDownBook")
            bookInSlot[targetSlot] = null;
            return true;
        }
        
        //row is full both directions
        return false;
    }

    private void EnsureInitialized()
    {
        if (bookInSlot == null)
        {
            bookInSlot = new int?[rowSections.Count];
            for (int bookNum = 0; bookNum < books.Count; bookNum++)
            {
                //check the starting pos of each book in the books List
                Vector2 bookPos = books[bookNum].transform.position;
                for (int slot = 0; slot < rowSections.Count; slot++)
                {
                    //for each rowSections, check if the book position is within 0.1f, if it is, assign that book the number, if not, it stays null (it knows there is no book there)
                    if (Vector2.Distance(bookPos, rowSections[slot].transform.position) < occupiedThreshold)
                    {
                        bookInSlot[slot] = bookNum;
                        break;
                    }
                }
            }
        }

        /*
        for (int s = 0; s < bookInSlot.Length; s++)
        {
            Debug.Log("Slot " + s + " = " + (bookInSlot[s] == null ? "empty" : bookInSlot[s].ToString()));
        }
        */
    }
}
