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
        }
        for (int i = 0; i < puzzlesToLoad.Count; i++)
        {
            puzzlesToLoad[i].gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
