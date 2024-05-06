
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

            Debug.Log("start walking");
            player.anim.SetBool("Walk", true);
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

            if( player.CanEnterVehicle() )
            {
                sm.ChangeState(player.drivingState);
            }


        }


        void DoRun()
        {

            player.rb.AddForce(mov * player.moveSpeed);
            player.rb.velocity = Vector3.ClampMagnitude(player.rb.velocity, 1.5f);

            // rotate to direction of movement
            var newRotation = Quaternion.LookRotation(mov).eulerAngles;
            newRotation.x = 0;
            newRotation.z = 0;
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.Euler(newRotation), Time.fixedDeltaTime * 10);

            //sm.transform.rotation = Quaternion.LookRotation( mov );

        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            DoRun();
            Debug.Log("doing run");
        }
    }
}