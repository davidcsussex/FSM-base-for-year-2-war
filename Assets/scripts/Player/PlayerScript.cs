using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.InputSystem;

namespace Player
{


    public class PlayerScript : MonoBehaviour
    {
        [HideInInspector]
        public StateMachine sm;

        [HideInInspector]
        public Rigidbody rb;

        [HideInInspector]
        public Animator anim;

        public BoxCollider collider1;

        // Add your variables holding the different player states here
        public StandingState standingState;
        public JumpingState jumpingState;
        public WalkingState walkingState;
        public DrivingState drivingState;
        public DelayState delayState;

        bool isGrounded;
        public bool isTouchingVehicle;

        public GameObject vehicle;

        public float moveSpeed = 100;
        public float drivingForce = 4000;


        // Start is called before the first frame update
        void Start()
        {
            sm = gameObject.AddComponent<StateMachine>();
            rb = GetComponent<Rigidbody>();
            anim = GetComponent<Animator>();

            // set up the variables for your new states here
            standingState = new StandingState(this, sm);
            jumpingState = new JumpingState(this, sm);
            delayState = new DelayState(this, sm);
            walkingState = new WalkingState(this, sm);
            drivingState = new DrivingState(this, sm);

            // initialise the statemachine with the default state
            sm.Init(standingState);

            //initialise variables
            isGrounded = false;
            isTouchingVehicle = false;
        }

        // Update is called once per frame
        public void Update()
        {
            sm.CurrentState.HandleInput();
            sm.CurrentState.LogicUpdate();
        }

        public void LateUpdate()
        {

            sm.CurrentState.LateUpdate();
        }

        void FixedUpdate()
        {
            sm.CurrentState.PhysicsUpdate();
        }


        public bool IsGrounded()
        {
            Vector2 lineStart, lineEnd;

            lineStart = transform.position;
            lineEnd = transform.position;
            lineEnd.y -= 0.1f;

            Color lineCol = Color.red;

            RaycastHit2D hit = Physics2D.Linecast( lineStart, lineEnd, 1 << LayerMask.NameToLayer("Ground") );


            if ( hit.collider != null )
            {
                lineCol = Color.green;
                print("landed");
            }

            Debug.DrawLine(lineStart, lineEnd, lineCol);

            return hit.collider;


        }

        private void OnGUI()
        {
            string text = "Current state = " + sm.GetState();

            if( sm.GetState() == drivingState )
            {
                text += "\nPress E to exit vehicle";
            }


            GUILayout.BeginArea(new Rect(10f, 10f, 1600f, 1600f));
            GUILayout.Label($"<color=white><size=24>{text}</size></color>");
            GUILayout.EndArea();
        }


        public Vector3 GetMovement()
        {
            return new Vector3(-Input.GetAxis("Horizontal"), 0, -Input.GetAxis("Vertical"));
        }

        public bool CheckForMovement()
        {
            float mag = GetMovement().magnitude;

            if (mag > 0.05f)
            {
                return true;

            }
            return false;
        }

        public bool CanEnterVehicle()
        {
            return isTouchingVehicle;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if ( collision.gameObject.tag == "Car")
            {
                isTouchingVehicle = true;
                vehicle = collision.gameObject;
                

            }
        }
    }

}