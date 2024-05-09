
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

            player.transform.SetParent(player.vehicle.transform);

            //player.transform.position = player.vehicle.transform.position + new Vector3(0, 0.3f, 0.25f);
            

            player.rb.isKinematic = true;
            player.rb.useGravity = false;
            player.collider1.enabled = false;
            player.transform.rotation = player.vehicle.transform.rotation;

            snapDelay = 0.2f;




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
            ControlCar();
            CheckExitVehicle();

            
        }

        public override void LateUpdate()
        {

            

        }



        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();


            //snap player to driving position
            player.transform.localPosition = player.vehicle.GetComponent<MoveSteerVehicle>().playerDrivingOffset;// new Vector3(-0.3f, 0.3f, 0.25f);
            //player.transform.position = player.drivingPosition;

        }

        void ControlCar()
        {
            float rotation = Input.GetAxis("Horizontal") * 100f;

            rotation *= Time.deltaTime;
            //player.vehicle.transform.Rotate(0, rotation, 0);

            float vel = Input.GetAxis("Vertical") * player.drivingForce;
            //player.vehicle.GetComponent<Rigidbody>().AddForce(player.vehicle.transform.forward * vel);
        }

        void CheckExitVehicle()
        {
            if( Input.GetKeyDown(KeyCode.E))
            {
                sm.ChangeState(player.standingState);
                player.transform.SetParent(null, false);
                player.transform.localPosition = new Vector3(0, 0, 0);
                player.transform.position = player.vehicle.transform.position + new Vector3(0.0f, 0.0f, 2.75f);
                player.rb.isKinematic = false;
                player.rb.useGravity = true;
                player.collider1.enabled = true;
                player.isTouchingVehicle = false;
                player.vehicle.GetComponent<MoveSteerVehicle>().drivable = false;

            }
        }

    }
}