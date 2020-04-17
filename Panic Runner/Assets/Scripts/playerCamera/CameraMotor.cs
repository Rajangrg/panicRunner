using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    public Transform lookAt; // looks at character
    public Vector3 startOffSet = new Vector3(0,2.0f,-2.5f); 
  /*  private Vector3 moveVector;

    private float transition = 0.0f;
    private float animationDuration = 3.0f;
    private Vector3 animationOffset = new Vector3(0,5.0f,5.0f); //5 meter closer*/

    private void Start()
    {
        transform.position = lookAt.position + startOffSet;

    }

    private void LateUpdate()
    {
     

        Vector3 desirePosition = lookAt.position + startOffSet;
        desirePosition.x = 0;
        transform.position = Vector3.Lerp(transform.position, desirePosition, Time.deltaTime);



     /*   if (transition > 1.0f)
        {
            transform.position = lookAt.position + startOffSet;
        }
        else
        {
            //animation at the start of game
            transform.position = Vector3.Lerp(animationOffset, desirePosition, transition);
            transition += (Time.deltaTime * 1) / animationDuration;
            transform.LookAt(lookAt.position + Vector3.up);


        }*/
    }



    // Start is called before the first frame update
 /*  void Start()
    {
        lookAt = GameObject.FindGameObjectWithTag("Player").transform;//find player
        startOffSet = transform.position - lookAt.position;
    }

    // Update is called once per frame
    void Update()
    {
        moveVector = lookAt.position + startOffSet;
        //x
        moveVector.x = 0; //always center
        //y
        moveVector.y = Mathf.Clamp(moveVector.y, 2, 5); //restrict from border specifc height(jump change)

      
        if(transition > 1.0f)
        {
            transform.position = moveVector; //no animation
        }
        else
        {
            //animation at the start of game
            transform.position = Vector3.Lerp(moveVector + animationOffset, moveVector, transition);
            transition += (Time.deltaTime * 1) / animationDuration;
            transform.LookAt(lookAt.position + Vector3.up);
 

        }

    }*/
}
