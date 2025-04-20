using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public GameObject bulletImpactPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Instantiate(bulletImpactPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
