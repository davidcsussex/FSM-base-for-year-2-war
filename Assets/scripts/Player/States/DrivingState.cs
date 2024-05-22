
using UnityEngine;
namespace Player
{
    public class DrivingState : State
    {

        // constructor
        public DrivingState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            base.Enter();
            player.anim.SetBool("Drive", true);

            EnterVehicle();

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


            //move player to driving position based on vehicle type
            player.transform.localPosition = player.vehicle.GetComponent<MoveSteerVehicle>().playerDrivingOffset;

        }

        void ControlCar()
        {
            float rotation = Input.GetAxis("Horizontal");
            rotation *= Time.deltaTime * 100f;
            //player.vehicle.transform.Rotate(0, rotation, 0);

            float vel = Input.GetAxis("Vertical") * player.drivingForce;
            //player.vehicle.GetComponent<Rigidbody>().AddForce(player.vehicle.transform.forward * vel);

            player.transform.rotation = player.vehicle.transform.rotation;

        }


        void EnterVehicle()
        {
            //player.rb.isKinematic = true;
            //player.transform.parent = player.vehicle.transform;

            player.transform.SetParent(player.vehicle.transform);

            //player.transform.position = player.vehicle.transform.position + new Vector3(0, 0.3f, 0.25f);


            //player.rb.isKinematic = true;
            //player.rb.useGravity = false;
            player.collider1.enabled = false;
            player.transform.rotation = player.vehicle.transform.rotation;
            player.cc.enabled = false;
            player.agent.enabled = false;

            player.cam.GetComponent<CameraScript>().height = player.vehicle.GetComponent<MoveSteerVehicle>().cameraHeightOffset;
            player.cam.GetComponent<CameraScript>().distance = player.vehicle.GetComponent<MoveSteerVehicle>().cameraDistanceOffset;

        }
        void CheckExitVehicle()
        {
            if( Input.GetKeyDown(KeyCode.E))
            {
                sm.ChangeState(player.standingState);
                player.transform.SetParent(null, false);
                player.transform.localPosition = new Vector3(0, 0, 0);

                //exit player away from vehicle
                player.transform.position = player.vehicle.transform.position + new Vector3(0.0f, 0.0f, 2.75f);
                //player.rb.isKinematic = false;
                //player.rb.useGravity = false;
                player.collider1.enabled = true;
                player.isTouchingVehicle = false;
                player.vehicle.GetComponent<MoveSteerVehicle>().drivable = false;
                player.cc.enabled = true;

            }
        }

    }
}