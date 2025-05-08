using UnityEngine;

public class BurnScript : MonoBehaviour
{

    float timer = 6;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = 3;
    }

    // Update is called once per frame
    void Update()
    {

        timer -= Time.deltaTime;
        if( timer < 0 )
        {
            Destroy(gameObject);
        }
    }
}
