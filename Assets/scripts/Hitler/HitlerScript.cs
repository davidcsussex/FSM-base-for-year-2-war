using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Hitler
{
    public class HitlerScript : MonoBehaviour
    {
        Animator anim;

        public GameObject grenadePrefab;
        public GameObject throwPoint;

        [HideInInspector]
        public GameObject lookAtTarget;

        [HideInInspector]
        public StateMachine sm;
        GameObject spawnedObject;
        public NavMeshAgent agent;

        

        public float speed;

        public IdleState idleState;
        public ChaseState chaseState;

        public ThrowState throwState;

        public Rigidbody rb;

        // Start is called before the first frame update
        void Start()
        {
            anim = GetComponent<Animator>();

            lookAtTarget = GameObject.FindGameObjectWithTag("Player");
            speed = 3;

            agent = GetComponent<NavMeshAgent>();
            agent.destination = lookAtTarget.transform.position; 
            agent.enabled=false;

            sm = gameObject.AddComponent<StateMachine>();

            idleState = new IdleState(this, sm);
            chaseState = new ChaseState(this, sm);
            throwState = new ThrowState(this, sm);

            rb = GetComponent<Rigidbody>();

            sm.Init(idleState);

        }

        // Update is called once per frame
        void Update()
        {
            //UI_Text1.Debug("Enemy State=" + sm.GetState());

            sm.CurrentState.HandleInput();
            sm.CurrentState.LogicUpdate();



            /*
            anim.SetBool("walk", false);
            anim.SetBool("shakefist", false);
            anim.SetBool("run", false);
            if (Input.GetKey("x"))
            {
                anim.SetBool("walk", true);
                agent.destination = lookAtTarget.transform.position; 

            }

            if (Input.GetKey("f"))
            {
                anim.SetBool("shakefist", true);

            }

            if (Input.GetKeyDown("t"))
            {
                StartCoroutine("ThrowGrenade");


            }


            if (Input.GetKey("r"))
            {
                anim.SetBool("run", true);

            }
            */
        }


        public void ThrowGrenadex()
        {
            //StartCoroutine("ThrowGrenadeCoRoutine");

        }

        IEnumerator ThrowGrenade()
        {
           

            Quaternion targetRotation;
            bool doLook = true;

            while( doLook==true )
            {
                targetRotation = Quaternion.LookRotation(lookAtTarget.transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);


                //get angle between enemy and player. If >0.9999 then enemy is looking at player
                Vector3 dir = (lookAtTarget.transform.position - transform.position).normalized;
                float dot = Vector3.Dot(dir, transform.forward);

                if( dot > 0.995f )
                {
                    doLook = false;
                    print("dot look=" + dot);
                }
                yield return null;
            }
            // Smoothly rotate towards the target point.


            anim.SetTrigger("throw");

            yield return new WaitForSeconds(0.5f);

            sm.ChangeState(idleState);

            


            yield return null;
        }

        public void DoThrow()
        {
            spawnedObject = Instantiate(grenadePrefab, throwPoint.transform.position, Quaternion.identity);
            spawnedObject.transform.parent = throwPoint.transform;

            Rigidbody rb = spawnedObject.GetComponent<Rigidbody>();
            rb.useGravity = true;
            rb.linearVelocity = (transform.forward * 6) + (transform.up * 4);
            spawnedObject.transform.parent = null;

        }
    }
}