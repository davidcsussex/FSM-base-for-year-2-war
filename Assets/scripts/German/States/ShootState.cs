
using UnityEngine;
namespace Hitler
{
    public class ShootState : State
    {

        //float timeToShakeFist;
        //float exitStateTime = 3;
        int shotsFired;
        // constructor


        public GameObject bulletPrefab;
        public ShootState(HitlerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            //timeToShakeFist = Random.Range(0.4f, 3.5f);
            //exitStateTime = 0;
            shotsFired = 0;
            base.Enter();
            enemy.pistol.SetActive(true);

            enemy.anim.SetBool("shoot",true);


        }

        public override void Exit()
        {
            base.Exit();

            //enemy.anim.SetBool("shakefist", false);

            //player.anim.SetBool("stand", false );
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }


        public void ShootStart()
        {
            Debug.Log("doshoot!!!");
            GameObject bullet= GameObject.Instantiate(enemy.bulletPrefab,enemy.shootPoint.transform.position,enemy.pistol.transform.rotation);
            bullet.GetComponent<Rigidbody>().linearVelocity = bullet.transform.forward * 10;
            bullet.transform.rotation = enemy.pistol.transform.rotation;
            shotsFired++;


        }
        public void ShootEnded()
        {
            //enemy.anim.SetTrigger("shoot");
            if( shotsFired >= 8 )
            {
                enemy.anim.SetBool("shoot",false);
                shotsFired = 0;
                sm.ChangeState(enemy.idleState);


            }


        }


        public override void LogicUpdate()
        {
            base.LogicUpdate();
            float turnSpeed = 3;
            Quaternion targetRotation;
            targetRotation = Quaternion.LookRotation(enemy.lookAtTarget.transform.position - enemy.transform.position);
            enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, targetRotation, 5 * turnSpeed * Time.deltaTime);



            /*

            timeToShakeFist -= Time.deltaTime;
            if (timeToShakeFist < 0 && exitStateTime == 0)
            {
                enemy.anim.SetBool("shakefist", true);
                exitStateTime = 3;
            }

            if (exitStateTime > 0)
            {
                exitStateTime -= Time.deltaTime;
                if (exitStateTime < 0)
                {
                    sm.ChangeState(enemy.idleState);
                }
            }
            */
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

    }
}