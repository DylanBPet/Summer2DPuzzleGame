using UnityEngine;

public class CatAnimationScript : MonoBehaviour
{
    public Animator catAnimator;

    public GameObject key;

    public GameObject catWithoutKey;
    public GameObject catWithKey;

    public Animator catWithEyeballAnimator;
    public GameObject sleepingCat;
    public GameObject catWithEyeball;

    public void SpawnCatWithkey()
    {
        catWithKey.SetActive(true);
        catAnimator.SetBool("BellHasRung", true);
    }

    public void KeyCatIdle()
    {
        catAnimator.SetBool("BellHasRung", false);
    }

    public void SpawnKey()
    {
        //spawn the key ITEM
        key.SetActive(true);

        //change the cat so its the one without the key showing
        catWithoutKey.SetActive(true);
        catWithKey.SetActive(false);
    }

    public void CatGivenEyeball()
    {
        sleepingCat.SetActive(false);
        catWithEyeball.SetActive(true);

        //start animation
        catWithEyeballAnimator.SetBool("CatGivenEye", true);
    }

    public void CatIsOffscreen()
    {
        catWithEyeball.SetActive(false);
        catWithEyeballAnimator.SetBool("CatGivenEye", false);
    }


}
