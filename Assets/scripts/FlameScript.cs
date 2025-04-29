using UnityEngine;

public class FlameScript : MonoBehaviour
{
    public GameObject flameEffect;
    ParticleSystem ps;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ps = flameEffect.GetComponent<ParticleSystem>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetKeyDown("s"))
        {

            var main = ps.main;
            main.loop = false;
            //main.duration = 0.5f;
            ps.Stop();
        }
        
    }
}
