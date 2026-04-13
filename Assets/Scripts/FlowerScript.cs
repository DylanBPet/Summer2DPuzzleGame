using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlowerScript : MonoBehaviour
{
    // Lists for the flower petals, thier starting location for resetting them, and the flower order to track which order they were pressed
    public List<GameObject> flowerPetals;
    public List<Vector3> flowerPetalStartingLocation;
    public List<int> FlowerOrder;

    //A bool to see if all petals are gone (to provide an answer)
    public bool answerProvided;

    //Tracking the mouse position
    public Vector2 mousePos;

    //The canvas tracker for playtesting
    public TextMeshProUGUI testingResponse;


    void Start()
    {
        //gets the starting positions of the petals
        for (int i = 0; i < flowerPetals.Count; i++)
        {
           flowerPetalStartingLocation.Add(flowerPetals[i].transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {   
        mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        /*
        for (int i = 0; i < flowerPetals.Count; i++)
        {
            
            SpriteRenderer sr = flowerPetals[i].GetComponent<SpriteRenderer>();

            if (sr.bounds.Contains(mousePos) && Mouse.current.leftButton.wasPressedThisFrame)
            {
                StartCoroutine(PetalStartFalling(i));
                Debug.Log("Petal " + i + " Has Been Clicked");
                FlowerOrder.Add(i);
            }
        }
        */

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider != null)
            {
                int i = flowerPetals.IndexOf(hit.collider.gameObject);
                if (i != -1)
                {
                     StartCoroutine(PetalStartFalling(i));
                    Debug.Log("Petal " + i + " Has Been Clicked");
                    FlowerOrder.Add(i);
                }
            }
        }

        //Combination is E, S, NE, W, N, ES, WN, SW
        if (FlowerOrder.Count == flowerPetals.Count && answerProvided == false)
        {
            answerProvided = true;
            if (FlowerOrder[0] == 2 && FlowerOrder[1] == 4 && FlowerOrder[2] == 1 && FlowerOrder[3] == 6 && FlowerOrder[4] == 0 && FlowerOrder[5] == 3 && FlowerOrder[6] == 7 && FlowerOrder[7] == 5)
            {
                Debug.Log("You Solved the Puzzle!");
                testingResponse.text = "Answer is Correct :)!";
            }
            else
            {
                Debug.Log("Incorrect");
                testingResponse.text = "Answer is Incorrect :(";
            }
        }

           
    }

    IEnumerator PetalStartFalling(int i)
    {
        while (flowerPetals[i].transform.position.y >= -7)
        {
            flowerPetals[i].transform.position += new Vector3 (0, -0.05f);
            yield return null;
        }
    }

    public void ResetFlower()
    {
        for (int i = 0; i < flowerPetalStartingLocation.Count; ++i)
        {
            flowerPetals[i].transform.position = flowerPetalStartingLocation[i];
            FlowerOrder.Clear();
            testingResponse.text = "Waiting for Answer";
            answerProvided = false;
        }
    }
}
