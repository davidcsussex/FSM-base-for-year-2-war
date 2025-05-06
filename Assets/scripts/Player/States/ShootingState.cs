
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

        // constructor
        public ShootingState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            base.Enter();

            //Debug.Log("start walking");
            canShoot = false;



            if (player.weaponType == WeaponTypes.Flame)
            {
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
            //player.flameThrowerFX.SetActive(false);

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
            if( player.weaponType == WeaponTypes.Flame)
            {

                if( player.ShootButtonHeld()==false )
                {
                    player.anim.SetBool("Flame", false);
                    sm.ChangeState(player.standingState);
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

                if(player.weaponType == WeaponTypes.Flame)
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
            Debug.Log("do flame shoot start");

            if( player.weaponType == WeaponTypes.Flame )
            {
                //enable flame
                GameObject obj = GameObject.Instantiate(player.flameThrowerFXPrefab, player.flameThrowerShootPoint.transform.position, player.transform.rotation);

            }
            else
            {
                canShoot = false; //player can't fire again yet

                // don't spawn bullet if player is transitioning from idle as it will fire a bullet into the ground
                if (player.anim.GetCurrentAnimatorStateInfo(0).IsName("shoot") == true)
                {
                    GameObject bullet = GameObject.Instantiate(player.bulletPrefab, player.shootPoint.position, player.shootPoint.transform.rotation);
                    bullet.GetComponent<Rigidbody>().linearVelocity = bullet.transform.forward * 10;
                    bullet.transform.rotation = player.shootPoint.transform.rotation;
                }
                

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