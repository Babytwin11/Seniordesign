
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
namespace S3
{
    public class player_controler : NetworkBehaviour
    {
        // variable declaration
        public bool isGrounded; // used as our jumping function so when we are on the ground we can hop
        public bool isCrouching;// makes sure we are in crouching
        public GameObject sphereprefab;
        public Transform spherespawn;

        // i assume private means you can't change them inside the unity edior and public means you can
        public float speed;
        private float w_speed = 0.05f; // walk speed
        private float r_speed = 0.3f; // run speed
        public float timer, timer_max;



        private float c_speed = 0.025f; // crouch speed
        public float rotSpeed; // rotation speed
        public float jumpHeight; // how high you can jump
                                 // actuall compnoents for our character
        Rigidbody rb;

        public Animator anim;
        CapsuleCollider col_size;


        //For Camera 
        public Transform player_camera, centerpoint;
        private float mouseX, mouseY, zoomSpeed = 2, mouseYPostion = 1;
        public float zoom, zoom_min = -2, zoom_max = -5;

        // Use this for initialization
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            anim = GetComponent<Animator>();
            anim = anim.GetComponent<Animator>();
            col_size = GetComponent<CapsuleCollider>();
            isGrounded = true;
            //for camera
            zoom = zoom_max;
            timer = timer_max;
        }
        [Command]
        void CmdFire()
        {
            
            //Create the bullet from the prefab
            GameObject Sphere = (GameObject)Instantiate(sphereprefab, spherespawn.position, spherespawn.rotation);

            //Add velocity to the bullet
            Sphere.GetComponent<Rigidbody>().velocity = Sphere.transform.forward * 2.0f;

            //Spawn the bullet on the Clients
            NetworkServer.Spawn(Sphere);

            //Destroy the bullet after 2 seconds
            Destroy(Sphere, 5);
           
        }

        // Update is called once per frame
        void Update()
        {

            if (!isLocalPlayer)
            {
                Debug.Log("playercontrollerlocalplayer");
                return;
            }










            //Toggle Crouch
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                if (isCrouching)
                {
                    isCrouching = false;
                    anim.SetBool("isCrouching", false);
                    col_size.height = 4;
                    col_size.center = new Vector3(0, 1, 0);
                }
                else
                {
                    isCrouching = true;
                    anim.SetBool("isCrouching", true);
                    speed = c_speed;
                    col_size.height = 1;
                    col_size.center = new Vector3(0, 0.5f, 0);
                }
            }
            var z = Input.GetAxis("Vertical") * speed;
            var y = Input.GetAxis("Horizontal") * rotSpeed;
            transform.Translate(0, 0, z);
            transform.Rotate(0, y, 0);

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
            {
                rb.AddForce(0, jumpHeight, 0);
                anim.SetTrigger("isJumping");
                isCrouching = false;
                isGrounded = true;
            }


            if (Input.GetKeyDown(KeyCode.Q) && isGrounded == true)
            {


                CmdFire();

                //rb.AddForce(0, jumpHeight, 0);
                anim.SetTrigger("isPunchingleft");
                isCrouching = false;
                isGrounded = true;
            }

            if (Input.GetKeyDown(KeyCode.E) && isGrounded == true)
            {



                CmdFire();



                // rb.AddForce(0, jumpHeight, 0);
                anim.SetTrigger("isPunchingright");
                isCrouching = false;
                isGrounded = true;
            }


            if (isCrouching)

            {
                // crouch controls
                if (Input.GetKey(KeyCode.W))
                {
                    anim.SetBool("isWalking", true);
                    anim.SetBool("isRunning", false);
                    anim.SetBool("isIdle", false);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    anim.SetBool("isWalking", true);
                    anim.SetBool("isRunning", false);
                    anim.SetBool("isIdle", false);
                }
                else if (Input.GetKey(KeyCode.A))
                {

                    anim.SetBool("isWalking", true);
                    anim.SetBool("isRunning", false);
                    anim.SetBool("isIdle", false);
                }
                else if (Input.GetKey(KeyCode.D))
                {

                    anim.SetBool("isWalking", true);
                    anim.SetBool("isRunning", false);
                    anim.SetBool("isIdle", false);
                }
                else
                {
                    anim.SetBool("isWalking", false);
                    anim.SetBool("isRunning", false);
                    anim.SetBool("isIdle", true);
                }
            }
            else if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = r_speed;
                //running controls
                if (Input.GetKey(KeyCode.W))
                {
                    anim.SetBool("isWalking", false);
                    anim.SetBool("isRunning", true);
                    anim.SetBool("isIdle", false);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    anim.SetBool("isWalking", false);
                    anim.SetBool("isRunning", true);
                    anim.SetBool("isIdle", false);
                }
                else if (Input.GetKey(KeyCode.A))
                {

                    anim.SetBool("isWalking", false);
                    anim.SetBool("isRunning", true);
                    anim.SetBool("isIdle", false);
                }
                else if (Input.GetKey(KeyCode.D))
                {

                    anim.SetBool("isWalking", false);
                    anim.SetBool("isRunning", true);
                    anim.SetBool("isIdle", false);
                }
                else
                {
                    anim.SetBool("isWalking", false);
                    anim.SetBool("isRunning", true);
                    anim.SetBool("isIdle", false);
                }
            }







            else if (!isCrouching)
            {
                speed = w_speed;
                //standing controls
                if (Input.GetKey(KeyCode.W))
                {
                    anim.SetBool("isWalking", true);
                    anim.SetBool("isRunning", false);
                    anim.SetBool("isIdle", false);
                }
                else if (Input.GetKey(KeyCode.A))
                {

                    anim.SetBool("isWalking", true);
                    anim.SetBool("isRunning", false);
                    anim.SetBool("isIdle", false);
                }
                else if (Input.GetKey(KeyCode.D))
                {

                    anim.SetBool("isWalking", true);
                    anim.SetBool("isRunning", false);
                    anim.SetBool("isIdle", false);
                }






                else if (Input.GetKey(KeyCode.S))
                {
                    anim.SetBool("isWalking", true);
                    anim.SetBool("isRunning", false);
                    anim.SetBool("isIdle", false);
                }

                else
                {
                    anim.SetBool("isWalking", false);
                    anim.SetBool("isRunning", false);
                    anim.SetBool("isIdle", true);
                }
            }
        }
        //public override void OnStartClient()
        //{
        //    base.OnStartClient();

        //}


    }
}
/*
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
john walker
1 month ago
this is my script

using System.Collections;

using System.Collections.Generic;

using UnityEngine;



public class nightControl : MonoBehaviour {



    float speed = 4;

    float rotSpeed = 80;

    float gravity = 8;



    Vector3 movedir = Vector3.zero;



    CharacterController controller;

    Animator anim;



    void Start()

    {

        controller = GetComponent<CharacterController>();

        anim = GetComponent<Animator>();

    }



    void Update()

    {

        if (controller.isGrounded)

        {

            if (Input.GetKey (KeyCode.W))

            {

                movedir = new Vector3(0, 0, 1);

                movedir *= speed;

            }

        }

        movedir *= gravity * Time.deltaTime;

        controller.Move(movedir * Time.deltaTime);

    }



}﻿
        //camera control
        zoom += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        if (zoom > zoom_min)
        {
            zoom = zoom_min;
        }
        if (zoom < zoom_max)
        {
            zoom = zoom_max;
        }
        player_camera.transform.localPosition = new Vector3(0, 0, zoom);
        if (Input.GetMouseButton(1))
        {
            mouseX += Input.GetAxis("Mouse x");
            mouseY -= Input.GetAxis("Mouse Y");
            player_camera.rotation = Quaternion.Euler(0, mouseX, 0);

        }
        mouseY = Mathf.Clamp(mouseY, -20, 60);
        player_camera.LookAt(centerpoint);
        centerpoint.localRotation = Quaternion.Euler(mouseY, mouseX, 0);
        if (Input.GetAxis("Vertical")>0 | Input.GetAxis("Vertical") < 0)
        {

            Quaternion turnAngle = Quaternion.Euler(0, centerpoint.eulerAngles.y, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, turnAngle,Time.deltaTime * rotSpeed);
            //mouseX = 0;
            //mouseY = 0;
        }










        else if (!isCrouching)
        {




        }
    }

    




    void OnCollisionEnter()
    {
        isGrounded = true;
    }
}
/*
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(Animator))]
    public class ThirdPersonCharacter : MonoBehaviour
    {
        [SerializeField] float m_MovingTurnSpeed = 360;
        [SerializeField] float m_StationaryTurnSpeed = 180;
        [SerializeField] float m_JumpPower = 12f;
        [Range(1f, 4f)] [SerializeField] float m_GravityMultiplier = 2f;
        [SerializeField] float m_RunCycleLegOffset = 0.2f; //specific to the character in sample assets, will need to be modified to work with others
        [SerializeField] float m_MoveSpeedMultiplier = 1f;
        [SerializeField] float m_AnimSpeedMultiplier = 1f;
        [SerializeField] float m_GroundCheckDistance = 0.1f;

        Rigidbody m_Rigidbody;
        Animator m_Animator;
        bool m_IsGrounded;
        float m_OrigGroundCheckDistance;
        const float k_Half = 0.5f;
        float m_TurnAmount;
        float m_ForwardAmount;
        Vector3 m_GroundNormal;
        float m_CapsuleHeight;
        Vector3 m_CapsuleCenter;
        CapsuleCollider m_Capsule;
        bool m_Crouching;


        void Start()
        {
            m_Animator = GetComponent<Animator>();
            m_Rigidbody = GetComponent<Rigidbody>();
            m_Capsule = GetComponent<CapsuleCollider>();
            m_CapsuleHeight = m_Capsule.height;
            m_CapsuleCenter = m_Capsule.center;

            m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            m_OrigGroundCheckDistance = m_GroundCheckDistance;
        }


        public void Move(Vector3 move, bool crouch, bool jump)
        {

            // convert the world relative moveInput vector into a local-relative
            // turn amount and forward amount required to head in the desired
            // direction.
            if (move.magnitude > 1f) move.Normalize();
            move = transform.InverseTransformDirection(move);
            CheckGroundStatus();
            move = Vector3.ProjectOnPlane(move, m_GroundNormal);
            m_TurnAmount = Mathf.Atan2(move.x, move.z);
            m_ForwardAmount = move.z;

            ApplyExtraTurnRotation();

            // control and velocity handling is different when grounded and airborne:
            if (m_IsGrounded)
            {
                HandleGroundedMovement(crouch, jump);
            }
            else
            {
                HandleAirborneMovement();
            }

            ScaleCapsuleForCrouching(crouch);
            PreventStandingInLowHeadroom();

            // send input and other state parameters to the animator
            UpdateAnimator(move);
        }


        void ScaleCapsuleForCrouching(bool crouch)
        {
            if (m_IsGrounded && crouch)
            {
                if (m_Crouching) return;
                m_Capsule.height = m_Capsule.height / 2f;
                m_Capsule.center = m_Capsule.center / 2f;
                m_Crouching = true;
            }
            else
            {
                Ray crouchRay = new Ray(m_Rigidbody.position + Vector3.up * m_Capsule.radius * k_Half, Vector3.up);
                float crouchRayLength = m_CapsuleHeight - m_Capsule.radius * k_Half;
                if (Physics.SphereCast(crouchRay, m_Capsule.radius * k_Half, crouchRayLength))
                {
                    m_Crouching = true;
                    return;
                }
                m_Capsule.height = m_CapsuleHeight;
                m_Capsule.center = m_CapsuleCenter;
                m_Crouching = false;
            }
        }

        void PreventStandingInLowHeadroom()
        {
            // prevent standing up in crouch-only zones
            if (!m_Crouching)
            {
                Ray crouchRay = new Ray(m_Rigidbody.position + Vector3.up * m_Capsule.radius * k_Half, Vector3.up);
                float crouchRayLength = m_CapsuleHeight - m_Capsule.radius * k_Half;
                if (Physics.SphereCast(crouchRay, m_Capsule.radius * k_Half, crouchRayLength))
                {
                    m_Crouching = true;
                }
            }
        }


        void UpdateAnimator(Vector3 move)
        {
            // update the animator parameters
            m_Animator.SetFloat("Forward", m_ForwardAmount, 0.1f, Time.deltaTime);
            m_Animator.SetFloat("Turn", m_TurnAmount, 0.1f, Time.deltaTime);
            m_Animator.SetBool("Crouch", m_Crouching);
            m_Animator.SetBool("OnGround", m_IsGrounded);
            if (!m_IsGrounded)
            {
                m_Animator.SetFloat("Jump", m_Rigidbody.velocity.y);
            }

            // calculate which leg is behind, so as to leave that leg trailing in the jump animation
            // (This code is reliant on the specific run cycle offset in our animations,
            // and assumes one leg passes the other at the normalized clip times of 0.0 and 0.5)
            float runCycle =
                Mathf.Repeat(
                    m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime + m_RunCycleLegOffset, 1);
            float jumpLeg = (runCycle < k_Half ? 1 : -1) * m_ForwardAmount;
            if (m_IsGrounded)
            {
                m_Animator.SetFloat("JumpLeg", jumpLeg);
            }

            // the anim speed multiplier allows the overall speed of walking/running to be tweaked in the inspector,
            // which affects the movement speed because of the root motion.
            if (m_IsGrounded && move.magnitude > 0)
            {
                m_Animator.speed = m_AnimSpeedMultiplier;
            }
            else
            {
                // don't use that while airborne
                m_Animator.speed = 1;
            }
        }


        void HandleAirborneMovement()
        {
            // apply extra gravity from multiplier:
            Vector3 extraGravityForce = (Physics.gravity * m_GravityMultiplier) - Physics.gravity;
            m_Rigidbody.AddForce(extraGravityForce);

            m_GroundCheckDistance = m_Rigidbody.velocity.y < 0 ? m_OrigGroundCheckDistance : 0.01f;
        }


        void HandleGroundedMovement(bool crouch, bool jump)
        {
            // check whether conditions are right to allow a jump:
            if (jump && !crouch && m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Grounded"))
            {
                // jump!
                m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, m_JumpPower, m_Rigidbody.velocity.z);
                m_IsGrounded = false;
                m_Animator.applyRootMotion = false;
                m_GroundCheckDistance = 0.1f;
            }
        }

        void ApplyExtraTurnRotation()
        {
            // help the character turn faster (this is in addition to root rotation in the animation)
            float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
            transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
        }


        public void OnAnimatorMove()
        {
            // we implement this function to override the default root motion.
            // this allows us to modify the positional speed before it's applied.
            if (m_IsGrounded && Time.deltaTime > 0)
            {
                Vector3 v = (m_Animator.deltaPosition * m_MoveSpeedMultiplier) / Time.deltaTime;

                // we preserve the existing y part of the current velocity.
                v.y = m_Rigidbody.velocity.y;
                m_Rigidbody.velocity = v;
            }
        }


        void CheckGroundStatus()
        {
            RaycastHit hitInfo;
#if UNITY_EDITOR
            // helper to visualise the ground check ray in the scene view
            Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * m_GroundCheckDistance));
#endif
            // 0.1f is a small offset to start the ray from inside the character
            // it is also good to note that the transform position in the sample assets is at the base of the character
            if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, m_GroundCheckDistance))
            {
                m_GroundNormal = hitInfo.normal;
                m_IsGrounded = true;
                m_Animator.applyRootMotion = true;
            }
            else
            {
                m_IsGrounded = false;
                m_GroundNormal = Vector3.up;
                m_Animator.applyRootMotion = false;
            }
        }
    }
}
*/
/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controler : MonoBehaviour
{
    // variable declaration
    public bool isGrounded; // used as our jumping function so when we are on the ground we can hop
    public bool isCrouching;// makes sure we are in crouching
    // i assume private means you can't change them inside the unity edior and public means you can
    public float speed;
    private float w_speed = 0.05f; // walk speed
    private float r_speed = 0.1f; // run speed
    private float c_speed = 0.025f; // crouch speed
    public float rotSpeed; // rotation speed
    public float jumpHeight; // how high you can jump
    // actuall compnoents for our character
    Rigidbody rb;
    Animator anim;
    CapsuleCollider col_size;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        col_size = GetComponent<CapsuleCollider>();
        isGrounded = true;

    }

    // Update is called once per frame
    void Update()
    {
        //Toggle Crouch
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (isCrouching)
            {
                isCrouching = false;
                anim.SetBool("isCrouching", false);
                col_size.height = 2;
                col_size.center = new Vector3(0, 1, 0);
            }
            else
            {
                isCrouching = true;
                anim.SetBool("isCrouching", true);
                speed = c_speed;
                col_size.height = 1;
                col_size.center = new Vector3(0, 0.5f, 0);
            }
        }
        var z = Input.GetAxis("Vertical") * speed;
        var y = Input.GetAxis("Horizontal") * rotSpeed;
        transform.Translate(0, 0, z);
        transform.Rotate(0, y, 0);
        if (Input.GetKey(KeyCode.Space) && isGrounded == true)
        {
            rb.AddForce(0, jumpHeight, 0);
            anim.SetTrigger("isJumping");
            isCrouching = false;
            isGrounded = false;
        }
        if (isCrouching)
        {
            // crouch controls
            if (Input.GetKey(KeyCode.W))
            {
                anim.SetBool("isWalking", true);
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", false);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                anim.SetBool("isWalking", true);
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", false);
            }
            else
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", true);
            }
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = r_speed;
            //running controls
            if (Input.GetKey(KeyCode.W))
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", true);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", true);
            }
            else
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", true);
            }
        }
        else if (!isCrouching)
        {
            speed = w_speed;
            //standing controls
            if (Input.GetKey(KeyCode.W))
            {
                anim.SetBool("isWalking", true);
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", false);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                anim.SetBool("isWalking", true);
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", false);
            }
            else
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", true);
            }
        }
    }
    void OnCollisionEnter()
    {
        isGrounded = true;
    }
}
*/
