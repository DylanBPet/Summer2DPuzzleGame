using UnityEngine;
using System.Collections.Generic;

public class LoadEverything : MonoBehaviour
{
    public List<GameObject> puzzlesToLoad;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < puzzlesToLoad.Count; i++)
        {
            puzzlesToLoad[i].gameObject.SetActive(true);
            //Debug.Log("Puzzle " + i + "Has been Loaded");
        }
        for (int i = 0; i < puzzlesToLoad.Count; i++)
        {
            puzzlesToLoad[i].gameObject.SetActive(false);
            //Debug.Log("Puzzle " + i + "Has been turned off");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
