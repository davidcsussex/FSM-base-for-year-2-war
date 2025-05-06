using UnityEngine;

public class FlameScript : MonoBehaviour
{
    ParticleSystem ps;
    Player.PlayerScript playerScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ps = GetComponent<ParticleSystem>();

        GameObject p = GameObject.FindGameObjectWithTag("Player");
        playerScript = p.GetComponent<Player.PlayerScript>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //check to se if player has released the shoot button
        if( Input.GetKeyDown("s") || playerScript.ShootButtonHeld()==false)
        {
            //stop the flame playing

            var main = ps.main;
            main.loop = false;
            //main.duration = 0.5f;
            ps.Stop();
        }

        if( ps.isPlaying == false )
        {
            Destroy(gameObject);
        }


    }
}
