using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ZoomInScript : MonoBehaviour
{
    //the players mouse
    private Vector2 mousePos;

    //walls zoomed out
    public GameObject allWalls;

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
    public FlowerScript flowerPuzzleScript;

    //Bookshelf
    public SpriteRenderer bookshelf;
    public GameObject bookshelfZoomIn;

    //Looking OutsideWindow
    public SpriteRenderer windowHitbox;
    public GameObject windowZoomInMan;
    public int randomNumber;
    public CurtainBehavior curtainScript;

    //blackLight Puzzle
    public SpriteRenderer blackLightPuzzleHitbox;
    public GameObject blackLightZoomedIn;

    //zoom in on bookshelf rules
    public SpriteRenderer bookshelfRulesSR;
    public GameObject bookshelfRules;

    //Compass
    public SpriteRenderer compassSR;
    public GameObject compassZoomedIn;


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
            if (allWalls.activeInHierarchy)
            {
                //go to safe puzzle
                if (safe.bounds.Contains(mousePos))
                {
                    allWalls.SetActive(false);
                    safeZoomIn.SetActive(true);
                    SwitchUiCanvas();
                }

                //zoom in on jellyfish painting
                if (jellyFishPainting.bounds.Contains(mousePos))
                {
                    allWalls.SetActive(false);
                    jellyFishPaintingZoomIn.SetActive(true);
                    SwitchUiCanvas();
                }

                //go to flower puzzle
                if (flowerPuzzleScript.flowerPuzzleSolved != true)
                {
                    if (flower.bounds.Contains(mousePos))
                    {
                        allWalls.SetActive(false);
                        flowerZoomIn.SetActive(true);
                        SwitchUiCanvas();
                    }
                }
                else
                {
                    return;
                }
                
                //To bookshelf zoomed in
                if (bookshelf.bounds.Contains(mousePos))
                {
                    allWalls.SetActive(false);
                    bookshelfZoomIn.SetActive(true);
                    SwitchUiCanvas();
                }

                //window
                if (windowHitbox.bounds.Contains(mousePos))
                {
                    if (curtainScript.curtainSpriteIndex == 0)
                    {
                        allWalls.SetActive(false);
                        windowZoomInMan.SetActive(true);
                        SwitchUiCanvas();
                    }
                    else
                    {
                        return;
                    }
                }

                //blachlightPuzzle
                if (blackLightPuzzleHitbox.bounds.Contains(mousePos))
                {
                    allWalls.SetActive(false);
                    blackLightZoomedIn.SetActive(true);
                    SwitchUiCanvas();
                }

                //Bookshelf Hint paper
                if (bookshelfRulesSR.bounds.Contains(mousePos))
                {
                    allWalls.SetActive(false);
                    bookshelfRules.SetActive(true);
                    SwitchUiCanvas();
                }

                //Compass
                if (compassSR.bounds.Contains(mousePos))
                {
                    allWalls.SetActive(false);
                    compassZoomedIn.SetActive(true);
                    SwitchUiCanvas();
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
        allWalls.SetActive(true);
    }
}
