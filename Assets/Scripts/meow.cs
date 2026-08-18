using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class meow : MonoBehaviour
{
    public SpriteRenderer bellHitbox;

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

    private float moveTime = 1;

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        if (bellHitbox.bounds.Contains(mousePos) && Mouse.current.leftButton.wasPressedThisFrame)
        {
            BellHit();
        }

        //checking every meow spawned
        for (int i = 0; i < meowList.Count; i++)
        {

            if (moveTime >= 0.1f)
            {
                moveTime -= Time.deltaTime * 0.5f;
                meowList[i].transform.position += new Vector3(0, 0.1f, 0);

                if (moveTime >= 0)
                {
                    moveTime = 1;
                }
            }

            //if they are x distance, delete them
            float distance = Vector2.Distance(meowList[i].transform.position, spawnPos.position);
            if (distance > 3)
            {
                GameObject meow = meowList[i];
                meowList.Remove(meowList[i]);
                Destroy(meow);
            }
        }

    }

    public void BellHit()
    {
        //make a random rotation for the meow prefab
        float r = 0;
        r = Random.Range(-10, 10);
        meowRotation.transform.eulerAngles = new Vector3(0, 0, r);
        
        //spawn meow prefab
        spawnedMeow = Instantiate(meowPrefab, spawnPos.transform.position, meowRotation.transform.rotation);
        meowList.Add(spawnedMeow);
    }
}
