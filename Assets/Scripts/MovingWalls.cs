using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingWalls : MonoBehaviour
{
    public GameObject[] lerpCircles;
    public GameObject[] walls;
    public int currentWall;

    private Coroutine movingLeftCoroutine;
    private Coroutine movingRightCoroutine;

    public SpriteRenderer blackScreen;

    public AnimationCurve changingWallsCurve;

    //audio script
    public AudioManager audioScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentWall = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SwitchingWallRight()
    {
        if (movingLeftCoroutine != null || movingRightCoroutine != null)
        {
            return;
        }
        else
        {
            //start coroutine for moving walls left
            movingLeftCoroutine = StartCoroutine(StartWallMovementLeftAnim());
        }
    }

    public void SwitchingWallLeft()
    {
        if (movingRightCoroutine != null || movingLeftCoroutine != null)
        {
            return;
        }
        else
        {
            //start coroutine that move ALL walls right
            movingRightCoroutine = StartCoroutine(StartWallMovementRightAnim());
        }
    }
    IEnumerator StartWallMovementRightAnim()
    {
        if (movingRightCoroutine != null || movingLeftCoroutine != null)
        {

        }
        else
        {
            //start coroutine that move ALL walls right
            //StartCoroutine(DoFadeOut());
            yield return movingRightCoroutine = StartCoroutine(WallMovementRight());
            movingRightCoroutine = null;
        }
    }

    IEnumerator StartWallMovementLeftAnim()
    {
        if (movingLeftCoroutine != null || movingRightCoroutine != null)
        {

        }
        else
        {
            //start coroutine that move ALL walls right
            //StartCoroutine(DoFadeOut());
            yield return movingLeftCoroutine = StartCoroutine(WallMovementLeft());
            movingLeftCoroutine = null;
        }
    }
    IEnumerator WallMovementRight()
    {
        //audio goes here
        audioScript.PlaySoundEffect(audioScript.keyboardClicking);

        //Lerp ALL walls to the right
        float t = 0;
        while (t <= 1)
        {
            t += Time.deltaTime;
            //the current wall
            walls[currentWall].transform.position = Vector2.Lerp(walls[currentWall].transform.position, lerpCircles[2].transform.position, changingWallsCurve.Evaluate(t));

            //the wall to the right of current wall
            //if wall is 3, the wall to the right will be wall 0
            if (currentWall == 3)
            {
                walls[0].transform.position = Vector2.Lerp(walls[0].transform.position, lerpCircles[3].transform.position, changingWallsCurve.Evaluate(t));
            }
            else
            {
                walls[currentWall + 1].transform.position = Vector2.Lerp(walls[currentWall + 1].transform.position, lerpCircles[3].transform.position, changingWallsCurve.Evaluate(t));
            }

            //the wall to the left of current wall (the new current wall)
            if (currentWall == 0)
            {
                //if current wall is 0, the wall to the left will be 3
                walls[3].transform.position = Vector2.Lerp(walls[3].transform.position, lerpCircles[1].transform.position, changingWallsCurve.Evaluate(t));
            }
            else
            {
                //if wall is anything other than 0, you will be able to -1 from current wall and get correct number
                walls[currentWall - 1].transform.position = Vector2.Lerp(walls[currentWall - 1].transform.position, lerpCircles[1].transform.position, changingWallsCurve.Evaluate(t));
            }

            //Move the rightmost wall directly to the lerpCircles[0]
            //if currentwall is 2 or 3, adding 2 would be 4 (null error)
            //so when wall is 2, the wall we need to move over would be wall 0
            if (currentWall == 2)
            {
                walls[0].transform.position = lerpCircles[0].transform.position;
            }
            else if (currentWall == 3)
            {
                walls[1].transform.position = lerpCircles[0].transform.position;
            }
            else
            {
                walls[currentWall + 2].transform.position = lerpCircles[0].transform.position;
            }

            yield return null;
        }

        //change the current wall number
        //if the wall number is 0, the next would be 3 because moving right is counting down
        if (currentWall == 0)
        {
            currentWall = 3;
        }
        else
        {
            currentWall--;
        }
    }

    IEnumerator WallMovementLeft()
    {
        //audio goes here
        audioScript.PlaySoundEffect(audioScript.keyboardClicking);

        //Lerp ALL walls to the LEFT
        float t = 0;
        while (t <= 1)
        {
            t += Time.deltaTime;
            //the current wall
            walls[currentWall].transform.position = Vector2.Lerp(walls[currentWall].transform.position, lerpCircles[0].transform.position, changingWallsCurve.Evaluate(t));
            
            //the wall to the Right of current wall (the new current wall)
            if (currentWall == 3)
            {
                walls[0].transform.position = Vector2.Lerp(walls[0].transform.position, lerpCircles[1].transform.position, changingWallsCurve.Evaluate(t));
            }
            else
            {
                walls[currentWall + 1].transform.position = Vector2.Lerp(walls[currentWall + 1].transform.position, lerpCircles[1].transform.position, changingWallsCurve.Evaluate(t));
            }

            //the wall 2 to the right of current wall
            //if wall is 3, the wall 2 to the right will be wall 0
            if (currentWall == 2)
            {
                //if current wall is 2, the wall 2 to the left will be 0
                walls[0].transform.position = Vector2.Lerp(walls[0].transform.position, lerpCircles[2].transform.position, changingWallsCurve.Evaluate(t));
            }
            else if(currentWall == 3)
            {
                walls[1].transform.position = Vector2.Lerp(walls[1].transform.position, lerpCircles[2].transform.position, changingWallsCurve.Evaluate(t));
            }
            else
            {
                //if wall is anything other than 0, you will be able to -1 from current wall and get correct number
                walls[currentWall + 2].transform.position = Vector2.Lerp(walls[currentWall + 2].transform.position, lerpCircles[2].transform.position, changingWallsCurve.Evaluate(t));
            }

            //Move the left wall directly to the lerpCircles[0]
            //if currentwall is 0, Subtracting 1 would be -1 (null error)
            //so when wall is 0, the wall we need to move over would be wall 3
            if (currentWall == 0)
            {
                walls[3].transform.position = lerpCircles[3].transform.position;
            }
            else
            {
                walls[currentWall - 1].transform.position = lerpCircles[3].transform.position;
            }
            yield return null;
        }

        //change the current wall number
        //if the wall number is 3, the next would be 0 because moving left is counting up
        if (currentWall == 3)
        {
            currentWall = 0;
        }
        else
        {
            currentWall++;
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
        yield return new WaitForSeconds(0.3f);
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
}
