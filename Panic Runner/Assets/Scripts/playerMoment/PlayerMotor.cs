using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 moveCharacter;

    private float characterSpeed = 5.0f; //5meter per frame
    private float verticalVelocity = 0.0f;
    private float gravity = 12.0f;


    // Start is called before the first frame update
    public void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    public void Update()
    {
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
    }
}
