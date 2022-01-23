﻿using System;
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

    // Start is called before the first frame update
    void Start()
    {
        current_Speed = walking_Speed;
        player_Animation = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        player_Animation.SetBool("walking_Forward", false);
        player_Animation.SetBool("running", false);

        if (should_Move_Forward())
        {
            move_Forward();
        }


        if (should_Move_Backward())
        {
            move_Backward();
        }


        turn(Input.GetAxis("Horizontal"));
        //lookAround(Input.GetAxis("Vertical"));

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
        return running_Speed;
        
    }

    private void lookAround(float mouse_Look_Value_Y)
    {
        transform.Rotate(Vector3.right, mouse_Sensitivity_X * mouse_Look_Value_Y * Time.deltaTime);
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

    private void move_Forward()
    {
        // move in frame rate independance using s = u*t
        transform.position += current_Speed * transform.forward * Time.deltaTime;
        player_Animation.SetBool("walking_Forward", true);
        player_Animation.SetBool("running", false);
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
    }
}
