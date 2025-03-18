using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var particle = gameObject.GetComponent<ParticleSystem>();
        //Destroy(gameObject, particle.main.duration);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
