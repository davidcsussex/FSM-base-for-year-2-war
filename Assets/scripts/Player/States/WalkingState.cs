
using Unity.Android.Gradle.Manifest;
using UnityEngine;

namespace Player
{
    public class WalkingState : State
    {

        Vector3 mov;
        
        
        


        // constructor
        public WalkingState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            base.Enter();

            //Debug.Log("start walking");
            player.anim.SetBool("Walk", true);
            player.isTouchingVehicle = false;
        }

        public override void Exit()
        {
            base.Exit();

            player.anim.SetBool("Walk", false);
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();


            mov = player.GetMovement();

            if (player.CheckForMovement() == false)
            {
                sm.ChangeState(player.standingState);
            }

            if( player.CanEnterVehicle() && player.reEnterVehicleTimer < 0 )
            {
                sm.ChangeState(player.enterCarState);
                return;
            }



        }


        

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            
            //Debug.Log("doing run");
        }


    }
}