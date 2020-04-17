using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInput : MonoBehaviour
{


    private const float DEADZONE = 100f;

    public static MobileInput Instance { set; get; }

    private bool tap, swipeRight, swipeLeft, swipeDown, swipeUp;
    private Vector2 swipeDelta, startTouch;

    public bool Tap { get { return tap; } }
    public Vector2 SwipeDelta { get { return swipeDelta; } }
    public bool SwipeRight { get { return swipeRight; } }
    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeDown { get { return swipeDown; } }
    public bool SwipeUp { get { return swipeUp; } }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        //Resetting all the booleans
        tap = swipeDown = swipeLeft = swipeRight = swipeUp = false;

        // Lets check for inputs
        #region Standalone Inputs
        if (Input.GetMouseButtonDown(0))
        {
            tap = true;
            startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            startTouch = swipeDelta = Vector2.zero;
        }

        #endregion

        #region Mobile Inputs
        if (Input.touches.Length != 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                tap = true;
                startTouch = Input.mousePosition;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                startTouch = swipeDelta = Vector2.zero;
            }

        }


        #endregion

        //calculate distance
        swipeDelta = Vector2.zero;
        if (startTouch != Vector2.zero)
        {
            //check with mobile
            if (Input.touches.Length != 0)
            {
                swipeDelta = Input.touches[0].position - startTouch;
            }
            else if (Input.GetMouseButton(0))
            {
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
            }
        }

        //Lets check if we are beyond the deadzone 
        if (swipeDelta.magnitude > DEADZONE)
        {
            //This is a confirmed swipe
            float x = swipeDelta.x;
            float y = swipeDelta.y;

            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                // either left or right
                if (x < 0)
                {
                    swipeLeft = true;
                }
                else
                {
                    swipeRight = true;
                }
            }
            else
            {
                //up or down
                if (y < 0)
                {
                    swipeDown = true;
                }
                else
                {
                    swipeUp = true;
                }
            }

            startTouch = swipeDelta = Vector2.zero;
        }

    }



    /* public bool tap, swipLeft, swipeRight, swipUp, swipDown;
     public Vector2 swipeDelta, startTouch; //st = keep memory of the postion where u sart, sd = postion current
     public const float DEADZONE = 100.0f;

     public static MobileInput Instance { set; get; }

     public bool Tap { get { return tap; } }
     public Vector2 SwipeDelta { get { return swipeDelta; } }
     public bool SwipLeft { get { return swipLeft; } }
     public bool SwipeRight { get { return swipeRight; } }
     public bool SwipUp { get { return swipUp; } }
     public bool SwipDown { get { return swipDown; } }

     public void Awake()
     {
         Instance = this;
     }

     public void Update()
     {
         //reseting all the booleans right to left 
         tap = swipeRight = swipLeft = swipDown = swipUp = false;

         //checking input
         #region Standalone INputs
         if (Input.GetMouseButtonDown(0))
         {

             tap = true;
             startTouch = Input.mousePosition;
         }else if(Input.GetMouseButtonUp(0))
         {
             startTouch = swipeDelta = Vector2.zero;
         }
         #endregion

         #region Mobile INputs
         if (Input.touches.Length != 0)
         {
             if(Input.touches[0].phase == TouchPhase.Began)
             {
                 tap = true;
                 startTouch = Input.mousePosition;
             }
             else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
             {
                 startTouch = swipeDelta = Vector2.zero;
             }
         }
         #endregion

         //clacluate distance 

         if(startTouch != Vector2.zero) 
         {
             //check if mobile
             if(Input.touches.Length != 0)
             {
                 swipeDelta = Input.touches[0].position - startTouch; //current position
             }
             //check if it is pc 
             else if(Input.GetMouseButton(0))
             {
                 swipeDelta = (Vector2)Input.mousePosition - startTouch; //current position
             }
         }

         //check  we are swiping up left down up or right
         if(swipeDelta.magnitude > DEADZONE)
         {
             //this is  a confiremd swipe
             float x = swipeDelta.x;
             float y = swipeDelta.y;

             if(Mathf.Abs(x) > Mathf.Abs(y))
             {
                 //left &right
                 if(x < 0) //negative
                 {
                     swipLeft = true;
                 }
                 else
                 {
                     swipeRight = true;
                 }
             }
             else
             {
                 //up & down
                 if (y < 0) //negative
                 {
                     swipDown = true;
                 }
                 else
                 {
                     swipUp = true;
                 }
             }
         }
         startTouch = swipeDelta = Vector2.zero; //reset to normal postion





     }*/

}
