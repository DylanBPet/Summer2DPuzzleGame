using System.Collections;
using UnityEngine;

public class FireEffects : MonoBehaviour
{
    public GameObject[] fireEffects;

    public GameObject[] text;

    private bool pulsing;

    public float pulseSpeed;

    private Coroutine firePulsing;

    public bool fireStarted = false;

    public GameObject blackScreen;
    public GameObject changeWallArrows;

    //to zoom back out once the fire sounds are over
    public ZoomInScript zoominscript;
    public AudioManager audioScript;

    //window hitboxes
    public GameObject toOutsideWindowNOMANHitbox;
    public GameObject toOutsideWindowMANHitbox;
    public GameObject outsideMANItemDropoff;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pulsing == false)
        {
            if (firePulsing != null)
            {

            }
            else
            {
                firePulsing = StartCoroutine(PulsingFire());
                //stop it from happening more than once
                pulsing = true;
            }      
        }
        else
        {
            
        }
    }

    public void FireStarted()
    {
        StartCoroutine (StartingFireNoises());
        for (int i = 0; i < fireEffects.Length; i++)
        {
            fireEffects[i].SetActive(true);
        }
        for (int i = 0; i < text.Length; i++)
        {
            text[i].SetActive(true);
        }

        pulsing = false;
        fireStarted = true;

        //get rid of the hitbox that takes you to outside with the man
        toOutsideWindowMANHitbox.SetActive(false);

        //activate hitbox that takes you outside with NO MAN
        toOutsideWindowNOMANHitbox.SetActive(true);

        //turn off hitbox for zoomed in MAN item dropoff
        outsideMANItemDropoff.SetActive(false);
    }

    IEnumerator PulsingFire()
    {
        //do pulse thing
        float t = 0.4f;
        while (t < 0.7f)
        {
            t += Time.deltaTime * pulseSpeed;
            for (int i = 0; i < fireEffects.Length; i++)
            {
                SpriteRenderer fireSR = fireEffects[i].GetComponent<SpriteRenderer>();
                fireSR.color = new Color(1, 1, 1, t);
            }
            yield return null;
        }
        t = 0.7f;
        while (t > 0.4)
        {
            t -= Time.deltaTime * pulseSpeed;
            for (int i = 0; i < fireEffects.Length; i++)
            {
                SpriteRenderer fireSR = fireEffects[i].GetComponent<SpriteRenderer>();
                fireSR.color = new Color(1, 1, 1, t);
            }
            yield return null;
        }
        //restart it
        firePulsing = null;
        pulsing = false;
    }

    IEnumerator StartingFireNoises()
    {
        blackScreen.SetActive(true);
        changeWallArrows.SetActive(false);

        yield return new WaitForSeconds(0.5f);
        audioScript.PlaySoundEffect(audioScript.gettingOutMatch);
        yield return new WaitForSeconds(3f);
        audioScript.PlaySoundEffect(audioScript.lightingMatch);
        yield return new WaitForSeconds(3f);
        audioScript.PlaySoundEffect(audioScript.whoosingOfFlame);
        yield return new WaitForSeconds(1f);
        audioScript.PlayBackgroundNoise(audioScript.lowIntenseFire);

        changeWallArrows.SetActive(true);
        blackScreen.SetActive(false);
        zoominscript.zoomOutUiCanvasButton();
        yield return null;
    }
}
