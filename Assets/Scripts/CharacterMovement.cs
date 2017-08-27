using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

    public CharacterController ControlChar;
    public float VelocityChar;
    private float Vertical, EarthGravity;
    private Vector3 MovementAxis;
    public bool DeathOrNot;
    public Animator A;

    public int laneNumber = 1;
    public int lanesCount = 3;
    bool didChangeLastFrame = false;
    public float laneDistance = 1;
    public float firstLaneXPos = -1;
    public float deadZone = 0.1f;
    public float sideSpeed = 10;
    public static CharacterMovement Static;
    float tSensitivity = 5;
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
            //InputController.Static.isJump = true;
            Debug.Log("^^^^^^^^^^^^^^^^^");
            toucheCount = -1;
            if (Vertical < 0.2) Vertical = 2.5f;
            if (VelocityChar > 1) A.Play("J");
        }
        else if (present_Input_X > 0 && (present_Input_Y >= 0 || present_Input_Y <= 0) && (angle < 1 && angle >= 0 || angle > -1 && angle <= 0))
        {//.........Swipe Right 
            swipeRight = true;
            laneNumber++;
            if (laneNumber < 0) laneNumber = 0;
            else if (laneNumber >= lanesCount) laneNumber = lanesCount - 1;
            //playerScript.RightSideMoving();
            Debug.Log(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
            toucheCount = -1;
            if (VelocityChar != 0) A.Play("RS");

            //swipeRight = false;

        }
        else if (present_Input_X < 0 && (present_Input_Y >= 0 || present_Input_Y <= 0) && (angle > -1 && angle <= 0 || angle >= 0 && angle < 1))
        {//........Swipe Left
            swipeLeft = true;
            laneNumber--;
            if (laneNumber < 0) laneNumber = 0;
            else if (laneNumber >= lanesCount) laneNumber = lanesCount - 1;
            //playerScript.LeftSideMoving();
            Debug.Log("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
            toucheCount = -1;
            if (VelocityChar !=0) A.Play("LS");

            //swipeLeft = false;

        }
        else if ((present_Input_X >= 0 || present_Input_X <= 0) && present_Input_Y < 0 && (angle < -1 || angle > 1))
        {//..........Swipe Down 
            swipeDown = true;
            //InputController.Static.RollPlayer();
            Debug.Log("vvvvvvvvvvvvvvvvvvvvv");
            toucheCount = -1;
            if (VelocityChar != 0) A.Play("S");

        }
        else
            toucheCount = 0;

    }

    void Start()
    {
       // Static = this;
        //playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>();

        swipe_Initial_X = 0.0f;
        swipe_Initial_Y = 0.0f;
        swipe_Final_X = 0.0f;
        swipe_Final_Y = 0.0f;
        present_Input_X = 0.0f;
        present_Input_Y = 0.0f;
        A.Play("HHD");
        StartCoroutine(StartRunning(30));
    }
    IEnumerator StartRunning(float time)
    {
        yield return new WaitForSeconds(time);
        VelocityChar = 3;
        Vertical = 2;
        A.Play("R");
        // Code to execute after the delay
    }
    void FixedUpdate()
    {
        if (DeathOrNot)
            return;

        if (VelocityChar < 1)
            return;

        MovementAxis = Vector3.zero;
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

       

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {

            toucheCount = 0;

        }
        //.........................................
        //"Horizontal" is a default input axis set to arrow keys and A/D
        //We want to check whether it is less than the deadZone instead of whether it's equal to zero 

        float input = Input.GetAxis("Horizontal");   
        if (Mathf.Abs(input) > deadZone)
        {
            if (!didChangeLastFrame)
            {
                didChangeLastFrame = true; //Prevent overshooting lanes
                laneNumber += Mathf.RoundToInt(Mathf.Sign(input));
                if (laneNumber < 0) laneNumber = 0;
                else if (laneNumber >= lanesCount) laneNumber = lanesCount - 1;
            }
        }
        else {
            didChangeLastFrame = false;
            //The user hasn't pressed a direction this frame, so allow changing directions next frame.
        }

        Vector3 pos = transform.position;
        
        pos.x = Mathf.Lerp(pos.x, firstLaneXPos + laneDistance * laneNumber, Time.deltaTime * sideSpeed);
        transform.position = pos;
       

        if (ControlChar.isGrounded)
        {
           Vertical = -0.1f;
           Debug.Log("GGG");
        }
        else Vertical -= 0.1f;
        Debug.Log("NNN");
        Debug.Log("nnnn   "+laneNumber.ToString());
        swipeDirection();
        MovementAxis.y = Vertical;
        MovementAxis.z = VelocityChar;
       // MovementAxis.x = Input.GetAxisRaw("Horizontal") * VelocityChar;
        ControlChar.Move(MovementAxis * Time.deltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit CharHitProps)
    {
        if (CharHitProps.point.z > transform.position.z + ControlChar.radius / 2 && CharHitProps.gameObject.tag == "Respawn")
        {
            Debug.Log(CharHitProps.gameObject.name);
            VelocityChar = 0;
            A.Play("D");
            Invoke("CharDeath", 2);
        }
    }

    private void CharDeath()
    {
        Debug.Log("you are death");
        DeathOrNot = true;
        GetComponent<GameScore>().YouAreDeath();
    }
}
