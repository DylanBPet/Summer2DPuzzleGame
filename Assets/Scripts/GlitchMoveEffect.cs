using UnityEngine;

public class GlitchMoveEffect : MonoBehaviour
{
    public GameObject glitch;
    public GameObject originalPosition;

    private float time;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time >= 0.2f)
        {
            glitch.transform.position = (Vector2)originalPosition.transform.position + Random.insideUnitCircle * (0.1f);
            time = 0;
        }
        
    }

}
