using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Hitler
{
 
    //use an enum for each animation event instead of multiple methods - these enums can be configured in the inspector
    public enum EnemyAnimEvents
    {
        ShootStart,
        ShootEnd
    }

    public class HitlerScript : MonoBehaviour
    {
        public Animator anim;

        //prefabs
        public GameObject bulletPrefab;
        public GameObject grenadePrefab;
        public GameObject handGrenade;  // grenade attached to model
        public GameObject pistol;  // pistol attached to model
        public GameObject throwPoint;
        public GameObject shootPoint;
        public GameObject explosionPrefab;



        [HideInInspector]
        public GameObject lookAtTarget;

        [HideInInspector]
        public StateMachine sm;
        GameObject spawnedObject;
        public NavMeshAgent agent;
        public GameObject testSphere; //used to show  next target point
        public GameObject navCheckSphere; //used to show  next target point

        public GameObject model;

        public LayerMask groundLayer;


        public bool shootAnimationEnded;


        public float speed;


        //states
        public IdleState idleState;
        public ChaseState chaseState;
        public ThrowState throwState;
        public ShootState shootState;
        public DyingState dyingState;
        public ShakeFistState shakeFistState;
        public MoveToPointState moveToPointState;

        public Rigidbody rb;

        public bool dying;

        // Start is called before the first frame update
        void Start()
        {
            anim = GetComponent<Animator>();

            lookAtTarget = GameObject.FindGameObjectWithTag("Player");
            speed = 3;
            dying = false;

            agent = GetComponent<NavMeshAgent>();
            agent.destination = lookAtTarget.transform.position; 
            agent.enabled=false;

            sm = gameObject.AddComponent<StateMachine>();

            idleState = new IdleState(this, sm);
            chaseState = new ChaseState(this, sm);
            throwState = new ThrowState(this, sm);
            shootState = new ShootState(this, sm);
            shakeFistState = new ShakeFistState(this, sm);
            moveToPointState = new MoveToPointState(this, sm);
            dyingState = new DyingState(this, sm);

            rb = GetComponent<Rigidbody>();

            sm.Init(idleState);

            //handGrenade.SetActive(false);

            groundLayer = LayerMask.GetMask("Ground") + LayerMask.GetMask("Enemy Weapon");



        }

        // Update is called once per frame
        void Update()
        {
            GUIScript.gui.text = "hello2\n";

            GUIScript.gui.text = "Enemy State=" + sm.GetState();

            sm.CurrentState.HandleInput();
            sm.CurrentState.LogicUpdate();
        }



        /*
        IEnumerator ThrowGrenade()
        {
            Quaternion targetRotation;
            bool doLook = true;


            while ( doLook==true )
            {
                targetRotation = Quaternion.LookRotation(lookAtTarget.transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5* speed * Time.deltaTime);


                //get angle between enemy and player. If >0.9999 then enemy is looking at player
                Vector3 dir = (lookAtTarget.transform.position - transform.position).normalized;
                float dot = Vector3.Dot(dir, transform.forward);

                if( dot > 0.995f )
                {
                    doLook = false;
                    //print("dot look=" + dot);
                }
                yield return null;
            }
            // Smoothly rotate towards the target point.


            //anim.SetTrigger("throw");
            anim.SetTrigger("throw");


            yield return new WaitForSeconds(0.5f);

            anim.ResetTrigger("throw");

            handGrenade.SetActive(false);

            yield return new WaitForSeconds(1.5f);

            sm.ChangeState(idleState);

            


            yield return null;
        }
        */


        public void DoThrow()
        {
            // point at which to throw spawned grenade

            //spawn a new grenade prefab and disable the dummy one
            print("spawn grenade object");
            spawnedObject = Instantiate(grenadePrefab, throwPoint.transform.position, Quaternion.identity);
            spawnedObject.transform.parent = throwPoint.transform;
            spawnedObject.GetComponent<Rigidbody>().useGravity = false;
            Rigidbody rb = spawnedObject.GetComponent<Rigidbody>();
            rb.useGravity = true;
            rb.linearVelocity = (transform.forward * 6) + (transform.up * 4);
            spawnedObject.transform.parent = null;

            //handGrenade.SetActive(true);


        }


        public void EnableGrenadeInHand()
        {
        }

        public bool TestSphereCast( Vector3 start, Vector3 end, float size)
        {
            float maxDist = (end - start).magnitude;
            RaycastHit hit;
            Vector3 dir = (end-start).normalized;
            bool hasHit = Physics.SphereCast(start, 0.2f, dir, out hit, maxDist, ~groundLayer);

            Color color = new Color(1, 1, 1);
//            Debug.DrawLine(start, end, color, 1);
            Debug.DrawRay(start, dir*maxDist, color, 1);

            if( hasHit )
            {
                print("sphere hit " + hit.collider.name);
            }

            return hasHit;

        }

        
        //add a generic method to animation event and set enum for method in inspector
        public void AnimationEvent( EnemyAnimEvents testParam )
        {
            switch(testParam)
            {
                case EnemyAnimEvents.ShootStart:
                    shootState.ShootStart();
                    break;

                case EnemyAnimEvents.ShootEnd:
                    shootState.ShootEnded();
                    break;


            }

        }

        public void CheckForDeath()
        {
            if( dying == true )
            {
                sm.ChangeState(dyingState);
            }
        }
        public void RequestDeath()
        {
            dying = true;
        }
    }
}