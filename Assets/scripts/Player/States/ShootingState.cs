
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using UnityEngine.InputSystem.XR;
using Unity.VisualScripting.FullSerializer;
namespace Player
{
    public class ShootingState : State
    {

        Vector3 mov;
        
        
        float currentAngle;
        float currentAngleVelocity;
        bool canShoot;

        bool flameThrower;

        // constructor
        public ShootingState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            base.Enter();

            //Debug.Log("start walking");
            canShoot = false;

            flameThrower = true;


            if (flameThrower)
            {
                player.gun.SetActive(false);
                player.anim.SetBool("Flame", true);

            }
            else
            {
                player.anim.SetBool("Shoot", true);
            }
        }

        public override void Exit()
        {
            base.Exit();

            player.anim.SetBool("Shoot", false);
            player.anim.SetBool("Flame", false);
            player.flameThrowerFX.SetActive(false);

        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();


            /*
            mov = player.GetMovement();

            if (player.CheckForMovement() == false)
            {
                sm.ChangeState(player.standingState);
            }
            */

            CheckForShoot();


        }


        void CheckForShoot()
        {
            if( flameThrower)
            {

                if( player.ShootButtonHeld()==false )
                {
                    player.anim.SetBool("Flame", false);

                }
                else
                {
                    player.anim.SetBool("Flame", true);

                }
                return;
            }
            if (player.ShootButtonPressed() && canShoot)
            {
                canShoot = false;

                if( flameThrower)
                {
                    player.anim.SetBool("Flame", true);

                }
                else
                {
                    player.anim.SetBool("Shoot", true);

                }
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            
            //Debug.Log("doing run");
        }

        public void ShootStart()
        {
            if( flameThrower )
            {
                //enable flame
                player.flameThrowerFX.SetActive(true);

            }
            else
            {
                canShoot = false; //player can't fire again yet

                GameObject bullet = GameObject.Instantiate(player.bulletPrefab, player.shootPoint.position, player.shootPoint.transform.rotation);

                bullet.GetComponent<Rigidbody>().linearVelocity = bullet.transform.forward * 10;
                bullet.transform.rotation = player.shootPoint.transform.rotation;

            }

        }

        public void CanReshoot()
        {
            //player.anim.SetBool("Shoot", false);
            canShoot = true; //player can fire again

        }

        public void ShootEnded()
        {
            if (player.ShootButtonHeld())
            {
            }
            else
            {
                //got to the end of the shoot animation without pressing the shoot button
                sm.ChangeState(player.standingState);

                canShoot = true; //player can fire again

            }

        }



    }
}