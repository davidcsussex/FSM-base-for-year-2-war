
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using UnityEngine.InputSystem.XR;
namespace Player
{
    public class WalkingState : State
    {

        Vector3 mov;
        
        
        float currentAngle;
        float currentAngleVelocity;
        


        // constructor
        public WalkingState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            base.Enter();

            //Debug.Log("start walking");
            player.anim.SetBool("Walk", true);
            player.isTouchingVehicle = false;
        }

        public override void Exit()
        {
            base.Exit();

            player.anim.SetBool("Walk", false);
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();


            mov = player.GetMovement();

            if (player.CheckForMovement() == false)
            {
                sm.ChangeState(player.standingState);
            }

            if( player.CanEnterVehicle() && player.reEnterVehicleTimer < 0 )
            {
                sm.ChangeState(player.enterCarState);
                return;
            }

            DoRun();


        }


        void DoRun()
        {
            /*
            player.rb.AddForce(mov * player.moveSpeed);
            player.rb.velocity = Vector3.ClampMagnitude(player.rb.velocity, 1.5f);

            // rotate to direction of movement
            var newRotation = Quaternion.LookRotation(mov).eulerAngles;
            newRotation.x = 0;
            newRotation.z = 0;
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.Euler(newRotation), Time.fixedDeltaTime * 10);

            //sm.transform.rotation = Quaternion.LookRotation( mov );
            */

            //capturing Input from Player

            float hMov = Input.GetAxisRaw("Horizontal");
            //Debug.Log("hm=" + hMov);

            Vector3 movement = new Vector3(hMov, 0, Input.GetAxisRaw("Vertical")).normalized;
            if (movement.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + player.cam.transform.eulerAngles.y;
                currentAngle = Mathf.SmoothDampAngle(currentAngle, targetAngle, ref currentAngleVelocity, player.rotationSmoothTime);
                player.transform.rotation = Quaternion.Euler(0, currentAngle, 0);
                Vector3 rotatedMovement = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward * 0.2f;
                player.cc.Move(rotatedMovement * player.walkSpeed * Time.deltaTime);
            }

        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            
            //Debug.Log("doing run");
        }


    }
}