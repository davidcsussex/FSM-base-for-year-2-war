using UnityEngine;

public class HatScript : MonoBehaviour
{

    float timeToDie = 3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeToDie = 3;
    }

    // Update is called once per frame
    void Update()
    {
        timeToDie -= Time.deltaTime;
        if( timeToDie < 0 )
        {
            Destroy(gameObject);
        }
        
    }
}
