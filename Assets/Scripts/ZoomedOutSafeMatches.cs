using UnityEngine;
using System.Collections.Generic;

public class ZoomedOutSafeMatches : MonoBehaviour
{
    public SafePuzzle spScript;
    public List<SpriteRenderer> safeKeys;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < spScript.SafeIconSlots.Count; i++)
        {
            SpriteRenderer sIcons = spScript.SafeIconSlots[i].GetComponent<SpriteRenderer>();
            safeKeys[i].sprite = sIcons.sprite;
        }
    }
}
