using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInput : MonoBehaviour
{
    public static CharacterInput Static;
    CharacterMovement playerScript;
    public bool isJump = false, isDown = false, isLeft = false, isRight = false;
    Touch currentTouch;
    float minSwipeDistY = 40;
    float minSwipeDistX = 30;
    private Vector2 startPos;
    int touches;
   
    void Start()
    {
        Static = this;
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>();

        swipe_Initial_X = 0.0f;
        swipe_Initial_Y = 0.0f;
        swipe_Final_X = 0.0f;
        swipe_Final_Y = 0.0f;
        present_Input_X = 0.0f;
        present_Input_Y = 0.0f;


    }
    float lastThrowTime;

    public bool doubleTap;
    float doubleTapTime;

    Vector2 startMousePosition;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            startMousePosition = Input.mousePosition;

        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (Vector2.Distance(startMousePosition, Input.mousePosition) < 10)
            {
                doubleTap = true;
            }
        }


        if (Input.GetKeyDown(KeyCode.UpArrow))
        {

            doubleTap = true;

        }

        if (Input.touchCount > 0 && Input.GetTouch(0).tapCount > 1)
        {
            doubleTap = true;
        }

        // to slide or roll the player
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
     //       RollPlayer();
        }//...................................

        // to Jump the player
        SpaceBar_Pressed();
        //.................................
        if (Input.GetKeyDown(KeyCode.D))
        {
      // playerScript.RightSwitch();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
      // playerScript.LeftSwitch();
        }

        // for Swipe Control ..........................

        if (Input.GetKeyDown(KeyCode.Mouse0) && toucheCount == 0)
        {
            swipe_Initial_X = Input.mousePosition.x;
            swipe_Initial_Y = Input.mousePosition.y;

            toucheCount = 1;

        }
        if (toucheCount == 1)
        {

            swipe_Final_X = Input.mousePosition.x;
            swipe_Final_Y = Input.mousePosition.y;



        }
        swipeDirection();

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {

            toucheCount = 0;

        }
        //.........................................
    }

    void Down_Button_Action_Perfrom(string ButtonName)
    {
        switch (ButtonName)
        {
            case "Left":
                isLeft = true;
            
                break;
            case "Right":
                isRight = true;
      
                break;
            case "Jump":
                isJump = true;
                break;
            case "Down":
                isDown = true;
                break;
        }
    }

    void UP_Button_Action_Perfrom(string ButtonName)
    {

        switch (ButtonName)
        {
            case "Left":
         
                break;
            case "Right":
       
                break;
            case "Jump":
                isJump = false;
                break;
            case "Down":
                isDown = false;
                break;
        }
    }

    // when SpaceBar  Pressed Jump the player
    public void SpaceBar_Pressed()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJump = true;
           
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJump = false;
        }
    }
  
    float tSensitivity = 15;


    private float swipe_Initial_X, swipe_Final_X;
    private float swipe_Initial_Y, swipe_Final_Y;
    public int toucheCount;
    private float present_Input_X, present_Input_Y;
    private float angle;
    private float swipe_Distance;
    public bool swipeDown, swipeUp, swipeRight, swipeLeft;


    void swipeDirection()
    {

        if (toucheCount != 1)
            return;
        present_Input_X = swipe_Final_X - swipe_Initial_X;
        present_Input_Y = swipe_Final_Y - swipe_Initial_Y;
        angle = present_Input_Y / present_Input_X;

        swipe_Distance = Mathf.Sqrt(Mathf.Pow((swipe_Final_Y - swipe_Initial_Y), 2) + Mathf.Pow((swipe_Final_X - swipe_Initial_X), 2));

        if (swipe_Distance <= (Screen.width / tSensitivity))
            return;


        if ((present_Input_X >= 0 || present_Input_X <= 0) && present_Input_Y > 0 && (angle > 1 || angle < -1))
        { //...... Swipe Jump  
            swipeUp = true;
            //           InputController.Static.isJump = true;
            Debug.Log("^^^^^^^^^^^^^^^^^");
            toucheCount = -1;
            
        }
        else if (present_Input_X > 0 && (present_Input_Y >= 0 || present_Input_Y <= 0) && (angle < 1 && angle >= 0 || angle > -1 && angle <= 0))
        {//.........Swipe Right 
            swipeRight = true;
            //  playerScript.RightSideMoving();
            Debug.Log(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
            toucheCount = -1;
           // playerScript.RightSwitch();
        }
        else if (present_Input_X < 0 && (present_Input_Y >= 0 || present_Input_Y <= 0) && (angle > -1 && angle <= 0 || angle >= 0 && angle < 1))
        {//........Swipe Left
            swipeLeft = true;
            //playerScript.LeftSideMoving();
            Debug.Log("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
            toucheCount = -1;
           // playerScript.LeftSwitch();
        }
        else if ((present_Input_X >= 0 || present_Input_X <= 0) && present_Input_Y < 0 && (angle < -1 || angle > 1))
        {//..........Swipe Down 
            swipeDown = true;
            //InputController.Static.RollPlayer();
            Debug.Log("vvvvvvvvvvvvvvvvvvvvv");
            toucheCount = -1;

        }
        else
            toucheCount = 0;

    }

}

