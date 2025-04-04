
using UnityEngine;
namespace Hitler
{
    public class ChaseState : State
    {

        float stopDistance;

        // constructor
        public ChaseState(HitlerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            base.Enter();

            Debug.Log("Started chase");


            enemy.agent.enabled=true;
            enemy.rb.isKinematic=true;  // disable rb
            enemy.agent.destination = enemy.lookAtTarget.transform.position; 

            //player.vel.x = player.vel.z = 0;
            //player.PlayAnim("arthur_stand", 0, 0);
            //player.xv = player.yv = 0;

            //player.anim.SetBool("jump", false);
            //player.anim.SetBool("run", false);
            //player.anim.SetBool("idle", true);
            //player.PlayAnim("idle_01");

            stopDistance = Random.Range(6,15);

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

            float dist = Vector3.Distance( enemy.transform.position, enemy.lookAtTarget.transform.position );

            if( dist < stopDistance )
            {
                //enemy.sm.ChangeState( enemy.throwState );
                enemy.sm.ChangeState( enemy.shootState );
            }
            Debug.Log("dist=" + dist);


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