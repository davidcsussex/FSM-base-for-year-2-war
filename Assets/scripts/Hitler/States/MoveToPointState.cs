
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.RuleTile.TilingRuleOutput;
namespace Hitler
{
    public class MoveToPointState : State
    {
        float stopDistance;
        float recheckTime;
        bool targetSet,reachedTarget;


        // constructor
        public MoveToPointState(HitlerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            base.Enter();

            Debug.Log("Started move to point");

            reachedTarget = false;
            targetSet = false;

            enemy.agent.enabled = true;
            enemy.rb.isKinematic = true;  // disable rb
            //enemy.agent.destination = enemy.lookAtTarget.transform.position;




            //player.vel.x = player.vel.z = 0;
            //player.PlayAnim("arthur_stand", 0, 0);
            //player.xv = player.yv = 0;

            //player.anim.SetBool("jump", false);
            //player.anim.SetBool("idle", true);
            //player.PlayAnim("idle_01");

            stopDistance = Random.Range(6, 15);

            recheckTime = 0;

            //check for everything apart from ground

        }

        public override void Exit()
        {
            base.Exit();
            enemy.anim.SetBool("run", false);

            //player.anim.SetBool("stand", false );
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }



        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if( recheckTime  > 0 )
            {
                recheckTime -= Time.deltaTime;
                //return;
            }

            if (targetSet == false)
            {


                //create a random point on edge of circle of specified radius around player
                float radius = 8;
                var vector2 = Random.insideUnitCircle.normalized * radius;
                vector2.x += enemy.lookAtTarget.transform.position.x;
                vector2.y += enemy.lookAtTarget.transform.position.z;
                Vector3 targetPoint = new Vector3(vector2.x, 0.5f, vector2.y);
                enemy.testSphere.transform.position = targetPoint;

                NavMeshHit hit;
                if (NavMesh.SamplePosition(targetPoint, out hit, 1.0f, NavMesh.AllAreas))
                {
                    //check to ensure there are no objects at this point

                    if( TargetReachable()==true )
                    {
                        if (enemy.TestSphereCast(enemy.transform.position, targetPoint, 0.6f) == false)
                        {
                            targetSet = true;
                            Debug.Log("target is set");

                            enemy.agent.enabled = true;
                            enemy.rb.isKinematic = true;  // disable rb
                            enemy.agent.destination = targetPoint;// enemy.lookAtTarget.transform.position;
                            enemy.anim.SetBool("run", true);



                        }
                        else
                        {
                            Debug.Log("target not reachable object in way, trying another");
                            recheckTime = 1000;

                        }
                    }
                    else
                    {
                        Debug.Log("target not reachable, trying another");

                    }
                }

                    //GameObject o = GameObject.Instantiate(enemy.cubePrefab);
                    //o.transform.position = new Vector3(vector2.x, 0.5f, vector2.y);


                    //check target can be reached
                    //targetSet = true;
            }

            if( Input.GetKeyDown("r"))
            {
                targetSet = false;
                recheckTime = 1;

            }

            if ( AgentReachedDestination() == true )
            {
                targetSet = false;
                recheckTime = 1;

                enemy.sm.ChangeState(enemy.throwState);

            }




            // get distance between enemy and player. If in range change state to throw

            //float dist = Vector3.Distance(enemy.transform.position, enemy.lookAtTarget.transform.position);

            //if (dist < stopDistance)
            {
                //  enemy.sm.ChangeState(enemy.throwState);
            }
            //Debug.Log("dist=" + dist);


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


        //returns true if navmesh agent has reached target
        bool AgentReachedDestination()
        {
            if (enemy.agent.pathPending)
            {
                return false;
            }
            if (enemy.agent.remainingDistance > enemy.agent.stoppingDistance)
            {
                return false;
            }
            return true;

        }

        bool TargetReachable()
        {
            var path = new NavMeshPath();
            enemy.agent.CalculatePath(enemy.agent.destination, path);
            switch (path.status)
            {
                case NavMeshPathStatus.PathComplete:
                    Debug.Log("able to reach {target.name}.");
                    return true;
                case NavMeshPathStatus.PathPartial:
                    Debug.LogWarning("agent will only be able to move partway to {target.name}.");
                    break;
                default:
                    Debug.LogError("There is no valid path");
                    break;
            }
            return false;
        }


        bool CheckForObjectInWay()
        {
            /*
            //check for an object in a straight line from enemy to enemy destination

            Vector3 start = new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z );
            Vector3 end = enemy.agent.destination;

            return false;
            */
            return false;
        }


    }
}