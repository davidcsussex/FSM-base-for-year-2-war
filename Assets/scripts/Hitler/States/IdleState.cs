
using UnityEngine;
namespace Hitler
{
    public class IdleState : State
    {


        // constructor
        public IdleState(HitlerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            base.Enter();

            enemy.rb.isKinematic=false;  // enable rb


            Debug.Log("started idle state");
            //player.vel.x = player.vel.z = 0;
            //player.PlayAnim("arthur_stand", 0, 0);
            //player.xv = player.yv = 0;

            //player.anim.SetBool("jump", false);
            //player.anim.SetBool("run", false);
            //player.anim.SetBool("idle", true);
            //player.PlayAnim("idle_01");

        }

        public override void Exit()
        {
            base.Exit();

            //player.anim.SetBool("stand", false );
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

    

        public override void LogicUpdate()
        {
            base.LogicUpdate();


            if (Input.GetKey("x"))
            {
                sm.ChangeState(enemy.chaseState);
            }

            if (Input.GetKey("m"))
            {
                sm.ChangeState(enemy.moveToPointState);
            }




            //UIscript.ui.DrawText("standing");

            //player.MovePlayer();


            //player.CheckForFall();
            //player.CheckForRun();
            /*
            if( player.CheckForLanding() == true )
            {
                player.vel.y = -0.5f;
            }

            player.CheckForJump();
            //player.CheckForShoot();
            




            //player.CheckForCrouch();
            //player.CheckForLadderClimb();   // climbing ladder overrides crouch
            player.UpdateCC();
*/


        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            //player.CheckForLand();
        }
    }
}