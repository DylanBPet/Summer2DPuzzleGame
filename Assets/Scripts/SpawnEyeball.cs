using UnityEngine;

public class SpawnEyeball : MonoBehaviour
{
    public GameObject eyeball;

    public FlowerScript flowerPuzzleScript;

    public void ShowEyeball()
    {
        eyeball.SetActive(true);
        

    }

    public void DontRepeatAnimation()
    {
        flowerPuzzleScript.flowerAnimator.SetBool("PuzzleSolved", false);
    }

    public void DontRepeatRetreat()
    {
        flowerPuzzleScript.flowerAnimator.SetBool("EyePickedUp", false);
    }

    public void TurnOnUI()
    {
        //turn back on zoom out ui
        flowerPuzzleScript.zoomOutUI.SetActive(true);
    }
}
