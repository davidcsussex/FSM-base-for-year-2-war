using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMove : MonoBehaviour
{
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {

        if( Input.GetKey("k"))
        {
            rb.linearVelocity = new Vector3(-2, 0, 0);

        }

        if (Input.GetKey("l"))
        {
            rb.linearVelocity = new Vector3(2, 0, 0);

        }


    }
}
