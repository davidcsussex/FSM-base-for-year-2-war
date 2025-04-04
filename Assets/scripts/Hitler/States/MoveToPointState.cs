
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
        Vector3 targetPoint;
        NavMeshHit hit;
        int attempts;


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

            recheckTime = 2;

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

            //OldLogic();
            NewLogic();
        }


        void NewLogic()
        {

            if (recheckTime > 0)
            {
                recheckTime -= Time.deltaTime;
                return;
            }


            if (targetSet == false)
            {
                int attemptLoop = 1;

                while (attemptLoop < 256)
                {
                    // get a random point around player
                    GetRandomPoint();

                    if (ValidDistance() == true)
                    {
                        if (TargetReachable() == true)
                        {
                            if ( ValidLineOfSight() == true )
                            {
                                enemy.testSphere.transform.position = targetPoint;
                                attempts = attemptLoop;
                                attemptLoop = 256;

                                enemy.agent.enabled = true;
                                enemy.rb.isKinematic = true;  // disable rb
                                enemy.agent.destination = targetPoint;
                                targetSet = true;
                                enemy.anim.SetBool("run", true);
                            }
                        }
                    }

                    attemptLoop++;
                }
            }

            if (targetSet == true)
            {
                //enemy is running to a point, check for destination reached

                if (AgentReachedDestination() == true)
                {
                    targetSet = false;
                    recheckTime = 1;

                    //enemy.sm.ChangeState(enemy.throwState);
                    enemy.sm.ChangeState(enemy.shootState);
                }
            }


            GUIScript.gui.text = "took " + attempts + "  attempts";




        }


        bool ValidDistance()
        {
            // ensure the enemy is far enough away from the new random point
            float dist = (enemy.transform.position - targetPoint).magnitude;
            if (dist > 2)
            {
                return true;
            }
            return false;
        }


            void GetRandomPoint()
        {
            //create a random point on edge of circle of specified radius around player
            float radius = 8;
            var vector2 = Random.insideUnitCircle.normalized * radius;
            vector2.x += enemy.lookAtTarget.transform.position.x;
            vector2.y += enemy.lookAtTarget.transform.position.z;
            targetPoint = new Vector3(vector2.x, 0.5f, vector2.y);
        }

        bool ValidPointOnNavmesh()
        {
            //check to see if point is reachable on navmesh
            if (NavMesh.SamplePosition(targetPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                return true;
            }
            return false;
        }

        //check to see if there is a clear line of sight to the new point
        bool ValidLineOfSight()
        {
            if( enemy.TestSphereCast(enemy.transform.position, targetPoint, 0.6f) == true )
            {

                return false;
            }
            return true;
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