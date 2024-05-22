
using UnityEngine;
using UnityEngine.AI;
namespace Player
{
    public class EnterCarState : State
    {

        float timeToGetInCar;
        bool doEnterCar, doWalkToCar;
        

        // constructor
        public EnterCarState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            timeToGetInCar = 2;
            doEnterCar = false;
            doWalkToCar = false;
            player.reEnterVehicleTimer = 10;

            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (doWalkToCar)
            {
                player.agent.enabled = true;
                player.agent.destination = player.vehicle.GetComponent<MoveSteerVehicle>().doorEntryPoint.transform.position;
                player.anim.SetBool("Walk", true);

                if(player.agent.remainingDistance <= 0.2f && (player.agent.pathPending==false))
                {
                    player.anim.SetBool("Walk", false);
                    player.PlayerEnterCarEvent();

                    //       doWalkToCar = false;
                    //     doEnterCar = true;
                    //   player.anim.SetBool("EnterCar", true);
                    // player.transform.rotation = player.vehicle.transform.rotation;
                    //player.transform.Rotate(0, 90, 0);
                }

            }
            else if( doEnterCar )
            {
                //player.transform.rotation = player.vehicle.transform.rotation;

            }
            else
            {
                timeToGetInCar -= Time.deltaTime;
                if (timeToGetInCar < 0)
                {
                    sm.ChangeState(player.standingState);
                }

                if (Input.GetKey("e"))
                {
                    doWalkToCar = true;

                }

                if (player.CheckForMovement() == true)
                {
                    //sm.ChangeState(player.walkingState);
                }
            }

        }


        //call this when player is inside vehicle
/*
        vehicle = hit.collider.gameObject;
                vehicle.GetComponent<MoveSteerVehicle>().drivable = true;
                cc.enabled = false;
*/


        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}