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

    void Start()
    {
        pickedUpBookNumber = null;
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

                Debug.Log("Book " + pickedUpBookNumber + " Has Been Picked Up");
            }
        }
        
    }
    public void PuttingDownBook(InputAction.CallbackContext context)
    {
        if (context.canceled == true)
        {
            if (pickedUpBookNumber == null)
            {
                //do nothing
                return;
            }

            Debug.Log("Book " + pickedUpBookNumber + " Has Been Placed");

            int bookNumber = pickedUpBookNumber.Value;

            //find the closest rowSection to the book//

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
            //if the closest indext is not -1 (error) and is less than 2, move the book to the closest rowSection
            if (closestIndex != -1 && closestDist < 2)
            {
                books[bookNumber].transform.position = rowSections[closestIndex].transform.position;
            }
            //if it is out of range, move it back to its original position
            else
            {
                //if dist is more than a number, put it back at original position
                books[bookNumber].transform.position = originalPos;
            }
            //reset the book number that is being picked up back to unasigned
            pickedUpBookNumber = null;
        }
           
    }
    public void BookPickedUp(int? i)
    {
        int bookNumber = i.Value;
        books[bookNumber].transform.position = mousePos;
    }



}
