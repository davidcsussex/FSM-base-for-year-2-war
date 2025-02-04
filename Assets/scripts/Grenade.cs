using UnityEditor;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public GameObject explosionPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Rigidbody>().linearVelocity.magnitude > 0.2f)
        {
            transform.Rotate(2.5f, 1.0f, 0.5f);
        }

    }


    private void OnTriggerEnter(Collider collision)
    {
        print("hit something" + collision.gameObject.tag);


        if ( collision.gameObject.tag != "Enemy" )
        {

            Instantiate(explosionPrefab, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }


    }
}
