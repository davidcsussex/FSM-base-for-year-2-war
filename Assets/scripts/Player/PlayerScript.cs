using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

namespace Player
{

    public enum PlayerAnimEvents
    {
        ShootStart,
        CanReshoot,
        ShootEnded
    }

    public enum WeaponTypes
    {
        Gun,
        Flame
    }

    public class PlayerScript : MonoBehaviour
    {
        [HideInInspector]
        public StateMachine sm;

        [HideInInspector]
        public Rigidbody rb;

        [HideInInspector]
        public Animator anim;

        [HideInInspector]
        public NavMeshAgent agent;

        [HideInInspector]
        public GameObject vehicle;

        public Transform shootPoint;
        public Transform flameThrowerShootPoint;
        public GameObject hat;  // link to hat attached to model
        public GameObject hatPrefab;    //new prefab hat spawned onto player when hit
        bool grenadeHit = false;// true if grenade has hit player
        Transform hatTransformParent;
        Vector3 hatTransformPosition;


        public GameObject bulletPrefab;
        public GameObject flameThrowerFXPrefab;
        
        
        public GameObject flameThrowerWeapon;
        public GameObject gunWeapon;


        public CapsuleCollider collider1;

        // Add your variables holding the different player states here
        public StandingState standingState;
        public JumpingState jumpingState;
        public WalkingState walkingState;
        public DrivingState drivingState;
        public EnterCarState enterCarState;
        public ShootingState shootingState;

        public DelayState delayState;

        bool isGrounded;
        public bool isTouchingVehicle;


        public Vector3 velocity;
        public float gravity = -18;
        float currentAngle;
        float currentAngleVelocity;
        Vector3 moveDir;


        public float moveSpeed = 100;
        public float drivingForce = 4000;
        public float walkSpeed = 10f;
        public float reEnterVehicleTimer;

        public Camera cam;
        public CharacterController cc;
        public float rotationSmoothTime;

        public WeaponTypes weaponType;
        public GameObject uiGun;
        public GameObject uiFlameThrower;


        // Start is called before the first frame update
        void Start()
        {
            sm = gameObject.AddComponent<StateMachine>();
            rb = GetComponent<Rigidbody>();
            anim = GetComponent<Animator>();
            cc = GetComponent<CharacterController>();
            agent = GetComponent<NavMeshAgent>();


            // set up the variables for your new states here
            standingState = new StandingState(this, sm);
            jumpingState = new JumpingState(this, sm);
            delayState = new DelayState(this, sm);
            walkingState = new WalkingState(this, sm);
            drivingState = new DrivingState(this, sm);
            enterCarState = new EnterCarState(this, sm);
            shootingState = new ShootingState(this, sm);
            collider1 = GetComponent<CapsuleCollider>();

            // initialise the statemachine with the default state
            sm.Init(standingState);

            agent.enabled = false;  // navmesh agent is off by default

            //initialise variables
            isGrounded = false;
            isTouchingVehicle = false;

            weaponType = WeaponTypes.Gun;
        }

        // Update is called once per frame
        public void Update()
        {
            GUIScript.gui.text = "Player State=" + sm.GetState();

            sm.CurrentState.HandleInput();
            sm.CurrentState.LogicUpdate();
            SwapWeapon();
            SetDebugSpeed();

            if( Input.GetButtonDown("Fire1"))
            {
                print("***fire 1 PRESSED***");
            }
            if (Input.GetButtonDown("Fire2"))
            {
                print("***fire 2 PRESSED***");
            }
            if (Input.GetButtonDown("Fire3"))
            {
                print("***fire 3 PRESSED***");
            }
            if (Input.GetButtonDown("Shoulder"))
            {
                print("***shoulder PRESSED***");
            }





        }

        public void LateUpdate()
        {

            sm.CurrentState.LateUpdate();

            DoRun();

        }

        void FixedUpdate()
        {
            sm.CurrentState.PhysicsUpdate();
        }


        public bool IsGrounded()
        {
            Vector2 lineStart, lineEnd;

            lineStart = transform.position;
            lineEnd = transform.position;
            lineEnd.y -= 0.1f;

            Color lineCol = Color.red;

            RaycastHit2D hit = Physics2D.Linecast( lineStart, lineEnd, 1 << LayerMask.NameToLayer("Ground") );


            if ( hit.collider != null )
            {
                lineCol = Color.green;
                print("landed");
            }

            Debug.DrawLine(lineStart, lineEnd, lineCol);

            return hit.collider;


        }

        private void OnGUI()
        {
            /*
            string text = "Current state = " + sm.GetState();

            if( sm.GetState() == drivingState )
            {
                text += "\nPress E to exit vehicle";
                text += "\nPress R to reset rotation";
            }

            if( CanEnterVehicle() == true )
            {
                text += "\nPress E to enter vehicle";
            }

            text += "\nRe Enter Veh timer=" + reEnterVehicleTimer;


            GUILayout.BeginArea(new Rect(10f, 10f, 1600f, 1600f));
            GUILayout.Label($"<color=white><size=24>{text}</size></color>");
            GUILayout.EndArea();
            */
        }


        public Vector3 GetMovement()
        {
            return new Vector3(-Input.GetAxis("Horizontal"), 0, -Input.GetAxis("Vertical"));
        }

        public bool CheckForMovement()
        {
            float mag = GetMovement().magnitude;

            if (mag > 0.05f)
            {
                return true;

            }
            return false;
        }

        public bool CanEnterVehicle()
        {
            reEnterVehicleTimer -= Time.deltaTime;

            return isTouchingVehicle;
        }


        
        private void OnControllerColliderHit( ControllerColliderHit hit )
        {
            if ( hit.collider.gameObject.tag == "Car")
            {
                isTouchingVehicle = true;
                vehicle = hit.collider.gameObject;
//                vehicle.GetComponent<MoveSteerVehicle>().drivable = true;
//                cc.enabled = false;



            }
        }


        public void CheckForDeath()
        {
            if( transform.position.y < -100 )
            {
                string currentSceneName = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene( currentSceneName );
            }
        }

        public void PlayerEnterCarEvent()
        {
            //animator calls this when animation finished
            vehicle.GetComponent<MoveSteerVehicle>().drivable = true;
            anim.SetBool("EnterCar", false);
            sm.ChangeState(drivingState);
            cc.enabled = false;
            agent.enabled = false;
        }


       


        void SetDebugSpeed()
        {
            // set speed
            if (Input.GetKeyDown("1"))
                Time.timeScale = 1f;

            if (Input.GetKeyDown("2"))
                Time.timeScale = 0.8f;

            if (Input.GetKeyDown("3"))
                Time.timeScale = 0.6f;

            if (Input.GetKeyDown("4"))
                Time.timeScale = 0.4f;

            if (Input.GetKeyDown("5"))
                Time.timeScale = 0.2f;

            if (Input.GetKeyDown("6"))
                Time.timeScale = 0.1f;

            if (Input.GetKeyDown("7"))
                Time.timeScale = 0.05f;

            if (Input.GetKeyDown("8"))
                Time.timeScale = 0.03f;

            if (Input.GetKeyDown("9"))
                Time.timeScale = 0.01f;

            if (Input.GetKeyDown("0"))
                Time.timeScale = 0.001f;
        }

        private void OnTriggerEnter( Collider collision )
        {
            if( collision.gameObject.tag == "Weapon" && grenadeHit==false)
            {
                grenadeHit = true;
                GameObject newHat = GameObject.Instantiate(hatPrefab, hat.transform.position, Quaternion.identity );
                newHat.GetComponent<Rigidbody>().linearVelocity = new Vector3(0, 10, 0);
                hat.SetActive(false);

                print("Grenade has hit player");
                StartCoroutine("ReplaceHat");
            }
            
        }

        IEnumerator ReplaceHat()
        {
            yield return new WaitForSeconds(5f);
            hat.SetActive(true);
            grenadeHit = false;
            yield return null;
        }

        public bool ShootButtonPressed()
        {
            if (Input.GetButtonDown("Fire3"))
            {
                return true;
            }
            return false;

        }
        public bool ShootButtonHeld()
        {
            if (Input.GetButton("Fire3"))
            {
                return true;
            }
            return false;

        }




        //generic script added to all animation events
        public void AnimationEvent(PlayerAnimEvents testParam)
        {
            switch (testParam)
            {
                case PlayerAnimEvents.ShootStart:
                    shootingState.ShootStart();
                    break;

                case PlayerAnimEvents.CanReshoot:
                    shootingState.CanReshoot();
                    break;


                case PlayerAnimEvents.ShootEnded:
                    shootingState.ShootEnded();
                    break;

            }

        }

        void SwapWeapon()
        {
            if (weaponType == WeaponTypes.Gun)
            {
                gunWeapon.SetActive(true);
                flameThrowerWeapon.SetActive(false);
                uiGun.SetActive(true);
                uiFlameThrower.SetActive(false);

            }
            else
            {
                gunWeapon.SetActive(false);
                flameThrowerWeapon.SetActive(true);
                uiGun.SetActive(false);
                uiFlameThrower.SetActive(true);

            }


            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (weaponType == WeaponTypes.Gun)
                {
                    weaponType = WeaponTypes.Flame;
                }
                else
                {
                    weaponType = WeaponTypes.Gun;

                }
            }
        }

        public void ApplyGravity()
        {

           
        }


        public void DoRun()
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

            float targetAngle=0;

            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            //Debug.Log("hm=" + hMov);

            Vector3 direction = new Vector3(h, 0, v).normalized;

            if (direction.magnitude >= 0.1f)
            {

                targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
                currentAngle = Mathf.SmoothDampAngle(currentAngle, targetAngle, ref currentAngleVelocity, rotationSmoothTime);

                moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward * 0.2f;



                //transform.rotation = Quaternion.Euler(0, currentAngle, 0);

                if (direction.magnitude >= 0.1f)
                {
                    transform.rotation = Quaternion.Euler(0, currentAngle, 0);
                }
                else
                {

                    moveDir = Vector3.zero;
                }
            }
            else
            {
                moveDir = Vector3.zero;

            }

            velocity.y += gravity * Time.deltaTime;
            if (cc.isGrounded)
            {
                velocity.y = -3;
            }


            cc.Move((moveDir * walkSpeed + velocity) * Time.deltaTime);


            //print("current angle=" + currentAngle);
            //print("target angle=" + targetAngle);
            //print("direction=" + direction);

        }

    }

}