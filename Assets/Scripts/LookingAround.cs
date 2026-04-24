
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
    private Coroutine movingLeftCoroutine;

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
        if (movingLeftCoroutine != null)
        {

        }
        else
        {
            movingLeftCoroutine = StartCoroutine(StartMovingLefttAnim());
        }

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
        //do fade in out
        StartCoroutine(DoFadeOut());
        //tracks t
        float t = 0;
        while (walls[wallListNumber].transform.position != leftLerp.transform.position)
        {
            t += Time.deltaTime * 0.5f;
       
            //lerp from 0,0 to lefttLerp gameobject
            walls[wallListNumber].transform.position = Vector2.Lerp(walls[wallListNumber].transform.position, leftLerp.transform.position, t);
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
        //set its position to right lerp
        walls[wallListNumber].transform.position = rightLerp.transform.position;

        t = 0;
        while (walls[wallListNumber].transform.position != Vector3.zero)
        {
            t += Time.deltaTime * 0.5f;

            walls[wallListNumber].transform.position = Vector2.Lerp(walls[wallListNumber].transform.position, Vector2.zero, t);
            yield return null;
        }
    }

    IEnumerator DoFadeOut()
    {
        Color alpha = blackScreen.color;
        while (alpha.a < 1)
        {
            alpha = blackScreen.color;
            alpha.a += 0.015f;
            blackScreen.color = alpha;
            yield return null;
        }
        yield return new WaitForSeconds(0.15f);
        StartCoroutine(DoFadeIn());
    }

    IEnumerator DoFadeIn()
    {
        Color alpha = blackScreen.color;
        while (alpha.a > 0)
        {
            alpha = blackScreen.color;
            alpha.a -= 0.015f;
            blackScreen.color = alpha;
            yield return null;
        }
    }

    IEnumerator StartMovingLefttAnim()
    {
        if (movingLeftCoroutine != null)
        {

        }
        else
        {
            yield return movingLeftCoroutine = StartCoroutine(MovingLeftAnim());
            movingLeftCoroutine = null;
        }
    }
    IEnumerator MovingLeftAnim()
    {
        //do fade in out
        StartCoroutine(DoFadeOut());
        //tracks t
        float t = 0;
        while (walls[wallListNumber].transform.position != rightLerp.transform.position)
        {
            t += Time.deltaTime * 0.5f;

            //lerp from 0,0 to lefttLerp gameobject
            walls[wallListNumber].transform.position = Vector2.Lerp(walls[wallListNumber].transform.position, rightLerp.transform.position, t);
            yield return null;
        }

        //turn off the current wall gameobject
        walls[wallListNumber].SetActive(false);

        //increase the wall list number (going right)
        wallListNumber--;

        //if the wall list number is above the wall list count set it to 0 (back to the start)
        if (wallListNumber < 0)
        {
            wallListNumber = 3;
        }

        //set the new wall to active
        walls[wallListNumber].SetActive(true);
        //set its position to right lerp
        walls[wallListNumber].transform.position = leftLerp.transform.position;

        t = 0;
        while (walls[wallListNumber].transform.position != Vector3.zero)
        {
            t += Time.deltaTime * 0.5f;

            walls[wallListNumber].transform.position = Vector2.Lerp(walls[wallListNumber].transform.position, Vector2.zero, t);
            yield return null;
        }
    }
}
