using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ZoomInScript : MonoBehaviour
{

    //script for looking around
    public LookingAround lookAroundScript;

    //the players mouse
    private Vector2 mousePos;

    //wall zero zoomed out
    public GameObject wallZeroZoomedOut;
    //wall 1 zoomed out
    public GameObject wallOneZoomedOut;
    //wall 2 zoomed out
    public GameObject wallTwoZoomedOut;

    //the ui to change which wall you are looking at
    public GameObject changeWallUiArrows;

    //the ui to zoom back out of an object
    public GameObject zoomBackOutUiArrows;

    //the safe zoomed out and zoomed in mode
    public SpriteRenderer safe;
    public GameObject safeZoomIn;

    //the picture zoomed in and out mode
    public SpriteRenderer jellyFishPainting;
    public GameObject jellyFishPaintingZoomIn;

    //Flower puzzle
    public SpriteRenderer flower;
    public GameObject flowerZoomIn;

    //Bookshelf
    public SpriteRenderer bookshelf;
    public GameObject bookshelfZoomIn;

    //Looking OutsideWindow
    public SpriteRenderer windowHitbox;
    public GameObject windowZoomInNoMan;
    public GameObject windowZoomInMan;
    public int randomNumber;
    public CurtainBehavior curtainScript;

    //a list of all the zoomed in things so we can turn them all off at the same time
    public List<GameObject> zoomedInEverything;

    void Start()
    {
        
    }

    
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    }

    public void ZoomIntoObjects(InputAction.CallbackContext context)
    {
        if (context.performed == true)
        {
            if (lookAroundScript.walls[0].activeInHierarchy == true)
            {

                //go to safe puzzle
                if (safe.bounds.Contains(mousePos))
                {
                    wallZeroZoomedOut.SetActive(false);
                    safeZoomIn.SetActive(true);
                    SwitchUiCanvas();
                }

                //zoom in on jellyfish painting
                if (jellyFishPainting.bounds.Contains(mousePos))
                {
                    wallZeroZoomedOut.SetActive(false);
                    jellyFishPaintingZoomIn.SetActive(true);
                    SwitchUiCanvas();
                }

                //go to flower puzzle
                if (flower.bounds.Contains(mousePos))
                {
                    wallZeroZoomedOut.SetActive(false);
                    flowerZoomIn.SetActive(true);
                    SwitchUiCanvas();
                }
            }
            if (lookAroundScript.walls[1].activeInHierarchy == true)
            {
                //To bookshelf zoomed in
                if (bookshelf.bounds.Contains(mousePos))
                {
                    wallOneZoomedOut.SetActive(false);
                    bookshelfZoomIn.SetActive(true);
                    SwitchUiCanvas();
                }
            }
            if (lookAroundScript.walls[2].activeInHierarchy == true)
            {
                if (windowHitbox.bounds.Contains(mousePos))
                {
                    if (curtainScript.curtainSpriteIndex == 0)
                    {
                        wallTwoZoomedOut.SetActive(false);
                        randomNumber = Random.Range(1, 3);
                        if (randomNumber == 1)
                        {
                            windowZoomInNoMan.SetActive(true);
                        }
                        else if (randomNumber == 2)
                        {
                            windowZoomInMan.SetActive(true);
                        }
                        SwitchUiCanvas();
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }
    }

    public void SwitchUiCanvas()
    {
        changeWallUiArrows.SetActive(false);
        zoomBackOutUiArrows.SetActive(true);
    }

    public void zoomOutUiCanvasButton()
    {
        changeWallUiArrows.SetActive(true);
        zoomBackOutUiArrows.SetActive(false);
        for (int i = 0; i < zoomedInEverything.Count; i++)
        {
            zoomedInEverything[i].SetActive(false);
        }
        lookAroundScript.walls[lookAroundScript.wallListNumber].SetActive(true);
    }
}
