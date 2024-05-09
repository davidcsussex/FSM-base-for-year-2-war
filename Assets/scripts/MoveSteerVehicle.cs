using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class MoveSteerVehicle : MonoBehaviour
{
    public static int score;
    public static float maxHeight;
    public Transform bulletSpawnPoint;

    float horizontalInput;
    float verticalInput;
    float steeringAngle;

    public WheelCollider flw, frw, rlw, rrw;

    public Transform flt, frt, rlt, rrt;

    public Vector3 playerDrivingOffset; 

    public float maxSteerAngle = 30;
    public float motorForce = 50;
    public float brakeForce = 600f;

    public bool drivable;    //set by playerscript


    private Rigidbody rb;


    public TextMeshProUGUI scoreText;

    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject bullet;


    public int counter;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        maxHeight = 0;

        rb = GetComponent<Rigidbody>();

        // lower the vehicle centre of mass to reduce tipping over
        rb.centerOfMass = new Vector3(0, -0.7f, 0);
        counter = 0;
        drivable = false;
    }

    void Update()
    {


        //string heightString = maxHeight.ToString("F2");
        //scoreText.text = "Score: " + score.ToString() + "\nMax height: " + heightString + "m";


        //if (transform.position.y > maxHeight)
        {
            //  maxHeight = transform.position.y;
        }
    }

    // Update is called once per frame
    public void LateUpdate()
    {
        GameObject newBullet;
        if (Input.GetKeyDown("r"))
        {
            rb.rotation = Quaternion.identity;
        }

        if (Input.GetKeyDown("space"))
        {

            //newBullet = Instantiate(bullet);
            //newBullet.transform.position = bulletSpawnPoint.position;
            //newBullet.GetComponent<Rigidbody>().velocity = (transform.forward * 51) + (transform.up * 10);
        }


    }



    void FixedUpdate()
    {
        if (drivable)
        {
            GetInput();
            Steer();
            Accelerate();
            Brake();
            
        }
        UpdateWheelPoses();

    }



    public void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    private void Steer()
    {
        steeringAngle = maxSteerAngle * horizontalInput;
        flw.steerAngle = steeringAngle;
        frw.steerAngle = steeringAngle;
    }

    private void Accelerate()
    {
        if (verticalInput > 0)
        {
            flw.brakeTorque = 0;
            frw.brakeTorque = 0;
            rlw.brakeTorque = 0;
            rrw.brakeTorque = 0;


            flw.motorTorque = verticalInput * motorForce;
            frw.motorTorque = verticalInput * motorForce;
            rlw.motorTorque = verticalInput * motorForce;
            rrw.motorTorque = verticalInput * motorForce;
        }


    }

    void Brake()
    {
        Vector3 vel = rb.velocity;
        float magnitude = vel.magnitude;


        Vector3 localVel = transform.InverseTransformDirection(rb.velocity);
        print("lv=" + localVel);

        if (verticalInput < 0)
        {
            flw.motorTorque = verticalInput * motorForce;
            frw.motorTorque = verticalInput * motorForce;
        }



        if (verticalInput == 0)
        {
            flw.brakeTorque = brakeForce;
            frw.brakeTorque = brakeForce;
            rlw.brakeTorque = brakeForce;
            rrw.brakeTorque = brakeForce;
            flw.motorTorque = 0;
            frw.motorTorque = 0;
            rlw.motorTorque = 0;
            rrw.motorTorque = 0;
        }
        if (verticalInput < 0)
        {

            flw.brakeTorque = brakeForce * -verticalInput;
            frw.brakeTorque = brakeForce * -verticalInput;
            rlw.brakeTorque = brakeForce * -verticalInput;
            rrw.brakeTorque = brakeForce * -verticalInput;
            flw.motorTorque = 0;
            frw.motorTorque = 0;
            rlw.motorTorque = 0;
            rrw.motorTorque = 0;


            // apply brake or reverse
            // if almost stationary, don't brake but add negative force to make vehicel go into reverse
            flw.brakeTorque = 0;
            frw.brakeTorque = 0;
            rlw.brakeTorque = 0;
            rrw.brakeTorque = 0;

            flw.motorTorque = verticalInput * motorForce;
            frw.motorTorque = verticalInput * motorForce;
            rlw.motorTorque = verticalInput * motorForce;
            rrw.motorTorque = verticalInput * motorForce;

        }
    }

    private void UpdateWheelPoses()
    {
        UpdateWheelPose(flw, flt);
        UpdateWheelPose(frw, frt);
        UpdateWheelPose(rlw, rlt);
        UpdateWheelPose(rrw, rrt);
    }

    void UpdateWheelPose(WheelCollider col, Transform tr)
    {
        Vector3 pos = tr.position;
        Quaternion q = tr.rotation;
        col.GetWorldPose(out pos, out q);
        tr.position = pos;
        tr.rotation = q;
    }




}
