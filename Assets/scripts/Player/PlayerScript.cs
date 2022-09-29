using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.VFX;

namespace Player
{


    public class PlayerScript : MonoBehaviour
    {
        public StateMachine sm;

        // Add your variables holding the different player states here
        public StandingState standingState;


        

        // Start is called before the first frame update
        void Start()
        {
            sm = gameObject.AddComponent<StateMachine>();

            // set up the variables for your new states here
            standingState = new StandingState(this, sm);
            
            // initialise the statemachine with the default state
            sm.Init(standingState);
        }

        // Update is called once per frame
        public void Update()
        {
            sm.CurrentState.HandleInput();
            sm.CurrentState.LogicUpdate();
        }

        public bool ReadSpaceBar()
        {
            if( Input.GetKey("space"))
            {
                return true;
            }
            return false;

        }



        void FixedUpdate()
        {
            sm.CurrentState.PhysicsUpdate();
        }
    }

}