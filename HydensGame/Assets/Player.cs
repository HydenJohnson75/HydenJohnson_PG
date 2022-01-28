using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Player Script
    private int player_Hp = 100;
    internal enum player_States { moving, idle, takingDmg, healing, dead };
    internal player_States my_State = player_States.idle;
    private float current_Speed;
    private float walking_Speed = 2f;
    private float running_Speed = 6f;
    private float mouse_Sensitivity_X = 90f;
    private float jump_Power = 6f;
    Animator player_Animation;
    Camera player_Camera;
    GameObject main_Cam;

    FPS_Camera my_Camera;
    SphereCollider panel_Collider;
    Transform door_Position;

    // Start is called before the first frame update
    void Start()
    {
        current_Speed = walking_Speed;
        player_Animation = GetComponentInParent<Animator>();
        player_Camera = GetComponentInChildren<Camera>();
        main_Cam = player_Camera.gameObject;
        my_Camera = GetComponentInChildren<FPS_Camera>();
        my_Camera.you_Belong_To_Me(this);
        panel_Collider = FindObjectOfType<Terminal_Script>().GetComponent<SphereCollider>();

    }

    // Update is called once per frame
    void Update()
    {
        player_Animation.SetBool("walking_Forward", false);
        player_Animation.SetBool("running", false);
        player_Animation.SetBool("jumping", false);

        if (should_Move_Forward())
        {
            move_Forward();
        }


        if (should_Move_Backward())
        {
            move_Backward();
        }

        if (should_Strafe_Left())
        {
            strafe_Left();
        }

        if (should_Strafe_Right())
        {
            strafe_Right();
        }


        turn(Input.GetAxis("Horizontal"));
        adjust_Camera(Input.GetAxis("Vertical"));

        if (isRunning())
        {
            current_Speed  = run(current_Speed);
        }
        else
        {
            current_Speed = walking_Speed; 
        }

        if (jumped())
        {
            jump();
        }

        //if (is_Crouching())
        //{
        //    crouch();
        //}
        //if(main_Cam.transform.position.y <= 0.72)
        //{
        //    main_Cam.transform.position = new Vector3(transform.position.x, (0.92f * Time.deltaTime), transform.position.z);
        //}

        if (Input.GetKeyDown(KeyCode.E))
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position + 0.7f * Vector3.up + 0.5f * transform.forward, 0.5f);

            foreach(Collider c in colliders)
            {
                I_Interactable Interact_Script = c.GetComponent<I_Interactable>();
                if (Interact_Script != null)
                {
                    Interact_Script.Interact();
                }
            }

        }

    }

    private bool isRunning()
    {
        return Input.GetKey(KeyCode.LeftShift);
    }

    private float run(float current_Speed)
    {
        current_Speed = running_Speed;
        player_Animation.SetBool("running", true);
        player_Animation.SetBool("walking_Forward", false);
        player_Animation.SetBool("jumping", false);
        return running_Speed;
        
    }

    private void adjust_Camera(float vertical_Adjustment)
    {
        my_Camera.adjust_Vertical_Angle(vertical_Adjustment);  
    }

    private void turn(float mouse_Turn_Value_X)
    {
        transform.Rotate(Vector3.up, mouse_Sensitivity_X * mouse_Turn_Value_X * Time.deltaTime);
    }

    private void move_Backward()
    {
        transform.position -= current_Speed * transform.forward * Time.deltaTime;
    }

    private bool should_Move_Backward()
    {
        return Input.GetKey(KeyCode.S);
    }

    private void strafe_Left()
    {
        transform.position -= current_Speed * transform.right * Time.deltaTime;
    }

    private bool should_Strafe_Left()
    {
        return Input.GetKey(KeyCode.A);
    }

    private void strafe_Right()
    {
        transform.position += current_Speed * transform.right * Time.deltaTime;
    }

    private bool should_Strafe_Right()
    {
        return Input.GetKey(KeyCode.D);
    }

    private void move_Forward()
    {
        // move in frame rate independance using s = u*t
        transform.position += current_Speed * transform.forward * Time.deltaTime;
        player_Animation.SetBool("walking_Forward", true);
        player_Animation.SetBool("running", false);
        player_Animation.SetBool("jumping", false);
    }

    private bool should_Move_Forward()
    {
        return Input.GetKey(KeyCode.W);
    }
    
    private bool jumped()
    {
        return Input.GetKey(KeyCode.Space);
    }

    private void jump()
    {
        transform.position += jump_Power * transform.up * Time.deltaTime;
        player_Animation.SetBool("walking_Forward", false);
        player_Animation.SetBool("running", false);
        player_Animation.SetBool("jumping", true);
    }

    private bool is_Crouching()
    {
        return Input.GetKey(KeyCode.C);
    }

    private void crouch()
    {
        main_Cam.transform.position = new Vector3(transform.position.x, (0.7f * Time.deltaTime) ,transform.position.z);
    }

    private void interact()
    {
        
    }

    private void OnTriggerEnter(Collider panel_Collider)
    {
        print("Event Is taking place");

        
        
    }
}
