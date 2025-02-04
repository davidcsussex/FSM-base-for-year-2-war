
using UnityEngine;
namespace Hitler
{
    public class ThrowState : State
    {


        // constructor
        public ThrowState(HitlerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            base.Enter();

            Debug.Log("Started throw");


            enemy.StartCoroutine("ThrowGrenade");

            enemy.agent.enabled=false;
            enemy.rb.isKinematic=false;  // disable rb


            enemy.dummyGrenade.SetActive(true);
            enemy.handGrenade.SetActive(false);

            //enemy.agent.destination = enemy.lookAtTarget.transform.position; 

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


            // get distance between enemy and player. If in range change state to throw

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