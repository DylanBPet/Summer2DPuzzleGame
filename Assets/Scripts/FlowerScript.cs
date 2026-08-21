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
    public List<int> flowerOrder;

    //A bool to see if all petals are gone (to provide an answer)
    public bool? flowerPuzzleSolved = null;

    //Tracking the mouse position
    public Vector2 mousePos;

    //the flower puzzle screen
    public GameObject flowerPuzzle;

    //the middle button (to reset Puzzle)
    public SpriteRenderer flowerMiddle;

    public GameObject pulseingMiddle;
    Coroutine flowerCenterPulse;
    public AnimationCurve flowerPulseCurve;

    //when solved, change the zoomed flower sprite to the eye
    public GameObject eyeITEM;
    public SpriteRenderer zoomedOutFlower;

    void Start()
    {
        //gets the starting positions of the petals
        for (int i = 0; i < flowerPetals.Count; i++)
        {
           Transform flowerPetalsTransform = flowerPetals[i].GetComponent<Transform>();
           flowerPetalStartingLocation.Add(flowerPetalsTransform.transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {   
        mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        if (flowerPuzzleSolved == false)
        {
            if (flowerCenterPulse != null)
            {

            }
            else
            {
                StartCoroutine(BeginFlowerPulse());
            }
        }
    }

    IEnumerator BeginFlowerPulse()
    {
        if (flowerCenterPulse != null)
        {

        }
        else
        {
            yield return flowerCenterPulse = StartCoroutine(FlowerCenterPulse());
            flowerCenterPulse = null;
        }
    }

    IEnumerator FlowerCenterPulse()
    {
        float t = 0;
        while (t <= 1)
        {
            pulseingMiddle.transform.localScale = Vector2.one * flowerPulseCurve.Evaluate(t);
            t += Time.deltaTime;
            yield return null;
        }
    }


    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed == true)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame && flowerPuzzleSolved == null || Mouse.current.leftButton.wasPressedThisFrame && flowerPuzzleSolved == false)
            {
                RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
                if (hit.collider != null)
                {
                    int i = flowerPetals.IndexOf(hit.collider.gameObject);
                    if (i != -1)
                    {
                        BoxCollider2D bc = flowerPetals[i].GetComponent<BoxCollider2D>();
                        bc.enabled = false;
                        StartCoroutine(PetalStartFalling(i));
                        Debug.Log("Petal " + i + " Has Been Clicked");

                        flowerOrder.Add(i);
                        if (flowerPuzzleSolved == null)
                        {
                            CheckIfCorrect();
                        }
                    }
                }
                if (flowerMiddle.bounds.Contains(mousePos))
                {
                    ResetFlower();
                }
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
        flowerPuzzleSolved = null;
        flowerCenterPulse = null;
        for (int i = 0; i < flowerPetalStartingLocation.Count; ++i)
        {
            flowerPetals[i].transform.position = flowerPetalStartingLocation[i];
            flowerOrder.Clear();
            BoxCollider2D bc = flowerPetals[i].GetComponent<BoxCollider2D>();
            bc.enabled = true;
        }
        StopAllCoroutines();
    }

    public void CheckIfCorrect()
    {
        //N=0 NE=1 E=2 ES=3 S=4 SW=5 W=6 WN=7
        //The player can pluck from L to S, OR S to L
        //Combination (L to S) is SW, WN, NE, N, W, S, ES, E
        //Combination (S to L) is E, ES, S, W, NE, N, WN, SW
        if (flowerOrder.Count == 8)
        {
            if (flowerOrder[0] == 5 && flowerOrder[1] == 7 && flowerOrder[2] == 1 && flowerOrder[3] == 0 && flowerOrder[4] == 6 && flowerOrder[5] == 4 && flowerOrder[6] == 3 && flowerOrder[7] == 2
               || flowerOrder[0] == 2 && flowerOrder[1] == 3 && flowerOrder[2] == 4 && flowerOrder[3] == 6 && flowerOrder[4] == 1 && flowerOrder[5] == 0 && flowerOrder[6] == 7 && flowerOrder[7] == 5)
            {
                Debug.Log("You Solved the Puzzle!");
                flowerPuzzleSolved = true;
                eyeITEM.SetActive(true);
            }
            else
            {
                Debug.Log("Incorrect");
                flowerPuzzleSolved = false;
            }
        }
        else
        {
            return;
        }
    }
}
