
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookingAround : MonoBehaviour
{
    public List<GameObject> walls;

    public int wallListNumber;

    //the different objects to lerp to
    public GameObject leftLerp;
    public GameObject rightLerp;
    public GameObject upLerp;
    public GameObject downLerp;

    //The coroutine to start the next coroutine
    private Coroutine movingRightCoroutine;

    //The transition blackscreen
    public SpriteRenderer blackScreen;

    void Start()
    {
        wallListNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RotateRight()
    {
        if (movingRightCoroutine != null)
        {

        }
        else
        {
            movingRightCoroutine = StartCoroutine(StartMovingRightAnim());
        }     
    }
    public void RotateLeft()
    {

    }
    public void RotateUp()
    {

    }
    IEnumerator StartMovingRightAnim()
    {
        if (movingRightCoroutine != null)
        {

        }
        else
        {
            yield return movingRightCoroutine = StartCoroutine(MovingRightAnim());
            movingRightCoroutine = null;
        }
    }

    IEnumerator MovingRightAnim()
    {
        //tracks t
        float t = 0;
        while (walls[wallListNumber].transform.position != rightLerp.transform.position)
        {
            t += Time.deltaTime;
            
            //do the fading out effect
            Color alpha = blackScreen.color;
            alpha.a += t;
            blackScreen.color = alpha;

            //lerp from 0,0 to rightLerp gameobject
            walls[wallListNumber].transform.position = Vector2.Lerp(walls[wallListNumber].transform.position, rightLerp.transform.position, t);
            yield return null;
        } 

        //turn off the current wall gameobject
        walls[wallListNumber].SetActive(false);

        //increase the wall list number (going right)
        wallListNumber++;

        //if the wall list number is above the wall list count set it to 0 (back to the start)
        if (wallListNumber >= walls.Count)
        {
            wallListNumber = 0;
        }

        //set the new wall to active
        walls[wallListNumber].SetActive(true);
        //set its position to left lerp
        walls[wallListNumber].transform.position = leftLerp.transform.position;

        t = 0;
        while (walls[wallListNumber].transform.position != Vector3.zero)
        {
            t += Time.deltaTime;

            //This is too sudden!!!
            //change the fade back in!!
            Color alpha = blackScreen.color;
            alpha.a -= t;
            blackScreen.color = alpha;

            walls[wallListNumber].transform.position = Vector2.Lerp(walls[wallListNumber].transform.position, Vector2.zero, t);
            yield return null;
        }
        
    }
}
