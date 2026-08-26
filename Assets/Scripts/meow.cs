using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class meow : MonoBehaviour
{
    public SpriteRenderer bellHitbox;
    public GameObject bell;

    private Vector2 mousePos;

    //the prefab
    public GameObject meowPrefab;

    //where it will spawn
    public Transform spawnPos;

    //the meow rotatoin assigned at random
    public Transform meowRotation;

    //arraylist of meows
    public List<GameObject> meowList;

    private GameObject spawnedMeow;

    //fire effect script
    public FireEffects fireScript;

    //key spawn
    public GameObject keyITEM;

    //audioscript
    public AudioManager audioScript;

    private bool keyIsSpawned = false;

    //cat animation script
    public CatAnimationScript catAnimScript;

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        if (bell.activeInHierarchy)
        {
            if (bellHitbox.bounds.Contains(mousePos) && Mouse.current.leftButton.wasPressedThisFrame && fireScript.fireStarted == false)
            {
                BellHit();
            }
            else if (bellHitbox.bounds.Contains(mousePos) && Mouse.current.leftButton.wasPressedThisFrame && fireScript.fireStarted == true && keyIsSpawned == false)
            {
                //key has been shown
                keyIsSpawned = true;

                BellHit();

                //summon the cat and spawn key
                //use CAT ANIMATOR
                catAnimScript.SpawnCatWithkey();

            }
        }
        

            //checking every meow spawned
            for (int i = 0; i < meowList.Count; i++)
            {
                meowList[i].transform.position += new Vector3(0, 0.01f, 0);

                //if they are x distance, delete them
                float distance = Vector2.Distance(meowList[i].transform.position, spawnPos.position);
                if (distance > 1.5f)
                {
                    GameObject meow = meowList[i];
                    meowList.Remove(meowList[i]);
                    Destroy(meow);
                }
            }

    }

    public void BellHit()
    {
        audioScript.PlaySoundEffect(audioScript.meow);
        //make a random rotation for the meow prefab
        float r = 0;
        r = Random.Range(-20, 20);
        meowRotation.transform.eulerAngles = new Vector3(0, 0, r);
        
        //spawn meow prefab
        spawnedMeow = Instantiate(meowPrefab, spawnPos.transform.position, meowRotation.transform.rotation);
        meowList.Add(spawnedMeow);
    }
}
