
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
namespace Hitler
{
    public class IdleState : State
    {

        float timeToThrow;

        // constructor
        public IdleState(HitlerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            base.Enter();

            enemy.rb.isKinematic=false;  // enable rb
            enemy.pistol.SetActive(false);

            Debug.Log("started idle state");
            //player.vel.x = player.vel.z = 0;
            //player.PlayAnim("arthur_stand", 0, 0);
            //player.xv = player.yv = 0;

            //player.anim.SetBool("jump", false);
            enemy.anim.SetBool("run", false);
            //player.anim.SetBool("idle", true);
            //player.PlayAnim("idle_01");

            timeToThrow = Random.Range(1, 3);

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

            if( timeToThrow > 0 )
            {
                timeToThrow -= Time.deltaTime;
            }
            else
            {
                //random shake fist instead of moving to point

                int rr = Random.Range(0, 10);
                Debug.Log("rand=" + rr);
                if (rr >= 8)
                {
                    sm.ChangeState(enemy.shakeFistState);
                    Debug.Log("SHAKE");
                }
                else
                {
                    sm.ChangeState(enemy.moveToPointState);
                }
            }
            if (Input.GetKey("m"))
            {
                timeToThrow = 0; //do throw instantly
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