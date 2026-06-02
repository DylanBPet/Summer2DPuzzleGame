using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ZoomInScript : MonoBehaviour
{
    //the players mouse
    private Vector2 mousePos;

    //wall one zoomed out
    public GameObject wallOneZoomedOut;

    //the ui to change which wall you are looking at
    public GameObject changeWallUiArrows;

    //the ui to zoom back out of an object
    public GameObject zoomBackOutUiArrows;

    //the safe in zoomed out and zoomed in mode
    public SpriteRenderer safe;
    public GameObject safeZoomIn;

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
            if (safe.bounds.Contains(mousePos))
            {
                wallOneZoomedOut.SetActive(false);
                safeZoomIn.SetActive(true);
                SwitchUiCanvas();
            }



        }
    }
    
    public void SwitchUiCanvas()
    {
        changeWallUiArrows.SetActive(!changeWallUiArrows.activeSelf);
        zoomBackOutUiArrows.SetActive(!changeWallUiArrows.activeSelf);
    }

    public void zoomOutUiCanvasButton()
    {
        for (int i = 0; i < zoomedInEverything.Count; i++)
        {
            zoomedInEverything[i].SetActive(false);
        }
    }
}
