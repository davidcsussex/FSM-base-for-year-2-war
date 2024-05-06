
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
namespace Player
{
    public class DrivingState : State
    {

        float snapDelay;

        // constructor
        public DrivingState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            base.Enter();
            player.anim.SetBool("Drive", true);

            player.rb.isKinematic = true;
            //player.transform.parent = player.vehicle.transform;
            //player.transform.position = player.vehicle.transform.position + new Vector3(0, 0.3f, 0.25f);
            player.rb.isKinematic = true;
            player.rb.useGravity = false;
            player.collider1.enabled = false;
            player.transform.rotation = player.vehicle.transform.rotation;

            snapDelay = 2.2f;




        }

        public override void Exit()
        {
            base.Exit();
            player.anim.SetBool("Drive", false);
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            
        }

        public override void LateUpdate()
        {
            if (snapDelay < 0)
            {
                player.transform.position = player.vehicle.transform.position + new Vector3(0, 0.3f, 0.25f);
            }
            else
            {
                snapDelay -= Time.deltaTime;
            }



        }



        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();


            //snap player to driving position

            //player.transform.position = player.drivingPosition;

        }
    }
}