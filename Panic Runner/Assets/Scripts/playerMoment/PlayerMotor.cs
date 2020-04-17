using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    public CharacterController controller;
    public float characterSpeed = 5.0f; //5meter per frame
    public float jumpForce = 4.0f;
    public float gravity = 12.0f;

    //player idle
    private bool isRunning = false;


    //public float animationDuration = 3.0f;
    public int desiredLane = 1; //0 = left 1 = middle & 2 = right
    public float verticalVelocity;
    public const float LANE_DISTANCE = 3.0f;
    public const float TURN_SPEED = 0.05f;

    public Animator anim;



    // Start is called before the first frame update
    public void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
  


    public void Update()
    {
        if (!isRunning)
            return;
        

        //gather mobile where we should be
        if (MobileInput.Instance.SwipeLeft)
        {
            MoveLane(false); // not going right so false Move left

        }
        if (MobileInput.Instance.SwipeRight)
        {
            MoveLane(true);//Move right
        }

        /*    //gather input where we should be
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveLane(false); // not going right so false Move left

            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveLane(true);//Move right
            }*/

        //calulate  where we should be 
        Vector3 targetPosition = transform.position.z * Vector3.forward;

        if (desiredLane == 0)
        {
            targetPosition += Vector3.left * LANE_DISTANCE;
        }
        else if (desiredLane == 2)
        {
            targetPosition += Vector3.right * LANE_DISTANCE;
        }

        //cal move delta
        Vector3 moveCharacter = Vector3.zero;
        moveCharacter.x = (targetPosition - transform.position).normalized.x * characterSpeed;

        bool isGrounded = IsGrounded();
        anim.SetBool("Grounded", isGrounded);

        //calcualte y
        if (isGrounded)
        {
            //for jump
            verticalVelocity = -0.1f;

            // if (Input.GetKeyDown(KeyCode.Space))
            if (MobileInput.Instance.SwipeUp)
            {
                anim.SetTrigger("Jump");
                verticalVelocity = jumpForce;
            }
            else if (MobileInput.Instance.SwipeDown) 
            {
                //for slide
                StartSliding();
                Invoke("StopSliding", 1.0f); //one sec stop
            }
        }
        else
        {
            verticalVelocity -= (gravity * Time.deltaTime);
            //fast fall
            //if (Input.GetKeyDown(KeyCode.Space))
            if (MobileInput.Instance.SwipeDown)
            {
                verticalVelocity = -jumpForce;

            }
        }

        moveCharacter.y = verticalVelocity;
        moveCharacter.z = characterSpeed;


        //move the character
        controller.Move(moveCharacter * Time.deltaTime);

        //rotate the character based on the direction
        Vector3 direction = controller.velocity;
        if (direction != Vector3.zero)
        {
            direction.y = 0;
            transform.forward = Vector3.Lerp(transform.forward, direction, TURN_SPEED);
        }
    }

    private void StartSliding()
    {
        anim.SetBool("Sliding", true);
        controller.height /= 2.5f;
        controller.center = new Vector3(controller.center.x, controller.center.y/2.5f, controller.center.z);
    }

    private void StopSliding()
    {
        anim.SetBool("Sliding", false);
        controller.height *= 2.5f;
        controller.center = new Vector3(controller.center.x, controller.center.y*2.5f, controller.center.z);
    }
    public void StartRunning()
    {

        isRunning = true;
        anim.SetTrigger("StartRunning");

    }
    public void MoveLane(bool goingRight)
    {
        desiredLane += goingRight ? 1 : -1; //add one if right and minus on less
        desiredLane = Mathf.Clamp(desiredLane, 0, 2); //border restriction bcoz right is 2
    }

    public bool IsGrounded()
    {
        Ray groundray = new Ray(new Vector3(controller.bounds.center.x, (controller.bounds.center.y - controller.bounds.extents.y) + 0.2f, controller.bounds.center.z), Vector3.down);
        Debug.DrawRay(groundray.origin, groundray.direction, Color.cyan, 1.0f);
        return Physics.Raycast(groundray, 0.2f + 0.1f);
    }

    private void Crash()
    {
        anim.SetTrigger("Death");
        isRunning = false;
    }

    private void OnControllerColliderHit( ControllerColliderHit hit)
    {
        switch (hit.gameObject.tag)
        {
            case "Obstacle":
                Crash();
                break;
        }
    }


    /* private void Update()
   {
       if(Time.time < animationDuration) //right now time is less than 3
       {
           controller.Move(Vector3.forward * characterSpeed * Time.deltaTime);
           return; //and stop update
       }

       //more than 3 
       if (controller.isGrounded)
       {
           verticalVelocity = -0.5f;
       }
       else
       {
           verticalVelocity -= gravity * Time.deltaTime; //if character doesnt touch ground.
       }

       moveCharacter = Vector3.zero;
       //X = left and right
       moveCharacter.x = Input.GetAxisRaw("Horizontal") * characterSpeed;

       //y = up and down
       moveCharacter.y = verticalVelocity;

       //z = forward and backward
       moveCharacter.z = characterSpeed;


       controller.Move((moveCharacter) * Time.deltaTime); //equal speed on all devices
   }*/

}
