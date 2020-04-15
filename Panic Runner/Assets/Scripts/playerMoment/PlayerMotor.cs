using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private float characterSpeed = 5.0f; //5meter per frame
    private float jumpForce = 4.0f;
    private float gravity = 12.0f;

    private float animationDuration = 3.0f;
    private int desiredLane = 1; //0 = left 1 = middle & 2 = right
    private float verticalVelocity;
    private const float LANE_DISTANCE = 3.0f;
    private const float TURN_SPEED = 0.05f;

    private Animator anim;

    private bool jump = false;

    // Start is called before the first frame update
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
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


    private void Update()
    {
     

        //gather input where we should be
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLane(false); // not going right so false Move left

        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveLane(true);//Move right
        }

        //calulate  where we should be 
       Vector3 targetPosition = transform.position.z * Vector3.forward;

        if(desiredLane == 0)
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

        


        //calcualte y
        if (IsGrounded())
        {
            verticalVelocity = -0.1f;
        

            if (Input.GetKeyDown(KeyCode.Space))
            {
               
                verticalVelocity = jumpForce;
             
            }
        }
        else
        {
          
            verticalVelocity -= (gravity * Time.deltaTime);
            //fast fall
            if (Input.GetKeyDown(KeyCode.Space))
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
        if(direction != Vector3.zero)
        {
            direction.y = 0;
            transform.forward = Vector3.Lerp(transform.forward, direction, TURN_SPEED);
        }
    }

    private void MoveLane(bool goingRight)
    {
        desiredLane += goingRight ? 1 : -1; //add one if right and minus on less
        desiredLane = Mathf.Clamp(desiredLane, 0, 2); //border restriction bcoz right is 2
    }

    private bool IsGrounded()
    {
        Ray groundray = new Ray(new Vector3(controller.bounds.center.x, (controller.bounds.center.y - controller.bounds.extents.y) + 0.2f, controller.bounds.center.z), Vector3.down);
        Debug.DrawRay(groundray.origin, groundray.direction, Color.cyan, 1.0f);
        return Physics.Raycast(groundray, 0.2f + 0.1f);
    }
}
