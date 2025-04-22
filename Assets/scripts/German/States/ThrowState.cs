
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.RuleTile.TilingRuleOutput;
using Unity.IO.LowLevel.Unsafe;

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


            enemy.StartCoroutine(ThrowGrenade());
            //enemy.StartCoroutine(TestCo());


            enemy.agent.enabled=false;
            enemy.rb.isKinematic=false;  // disable rb
            enemy.handGrenade.SetActive(true);


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
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        IEnumerator ThrowGrenade()
        {
            Quaternion targetRotation;
            bool doLook = true;


            while (doLook == true)
            {
                targetRotation = Quaternion.LookRotation(enemy.lookAtTarget.transform.position - enemy.transform.position);
                enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, targetRotation, 5 * enemy.speed * Time.deltaTime);


                //get angle between enemy and player. If >0.9999 then enemy is looking at player
                Vector3 dir = (enemy.lookAtTarget.transform.position - enemy.transform.position).normalized;
                float dot = Vector3.Dot(dir, enemy.transform.forward);

                if (dot > 0.995f)
                {
                    doLook = false;
                    //print("dot look=" + dot);
                }
                yield return null;
            }
            // Smoothly rotate towards the target point.


            //anim.SetTrigger("throw");
            enemy.anim.SetTrigger("throw");


            yield return new WaitForSeconds(0.5f);

            enemy.anim.ResetTrigger("throw");

            enemy.handGrenade.SetActive(false);

            yield return new WaitForSeconds(1.5f);

            sm.ChangeState(enemy.idleState);




            yield return null;


        }
    }
}