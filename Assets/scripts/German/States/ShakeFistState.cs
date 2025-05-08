
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
namespace Hitler
{
    public class ShakeFistState : State
    {

        float timeToShakeFist;
        float exitStateTime = 3;

        // constructor
        public ShakeFistState(HitlerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            timeToShakeFist = Random.Range(0.4f, 3.5f);
            exitStateTime = 0;
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();

            enemy.anim.SetBool("shakefist", false);

            //player.anim.SetBool("stand", false );
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }



        public override void LogicUpdate()
        {
            enemy.CheckForDeath();


            base.LogicUpdate();
            float turnSpeed = 3;
            Quaternion targetRotation;
            targetRotation = Quaternion.LookRotation(enemy.lookAtTarget.transform.position - enemy.transform.position);
            enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, targetRotation, 5 * turnSpeed * Time.deltaTime);


            timeToShakeFist -= Time.deltaTime;
            if(timeToShakeFist < 0 && exitStateTime == 0 )
            {
                enemy.anim.SetBool("shakefist", true);
                exitStateTime = 3;
            }

            if( exitStateTime > 0 )
            {
                exitStateTime-=Time.deltaTime;
                if( exitStateTime < 0 )
                {
                    sm.ChangeState(enemy.idleState);
                }
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}