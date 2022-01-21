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
    private float turn_Speed = 90f;
    private float mouse_Sensitivity_X = 180f;
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


        if (should_Turn_Left())
        {
            turn_Left();
        }

        turn(Input.GetAxis("Horizontal"));
        //lookAround(Input.GetAxis("Vertical"));

        if (isRunning())
        {
            run(current_Speed);
        }

    }

    private bool isRunning()
    {
        return Input.GetKey(KeyCode.LeftShift);
    }

    private void run(float current_Speed)
    {
        current_Speed = running_Speed;
    }

    private void lookAround(float mouse_Look_Value_Y)
    {
        transform.Rotate(Vector3.right, mouse_Sensitivity_X * mouse_Look_Value_Y * Time.deltaTime);
    }

    private void turn(float mouse_Turn_Value_X)
    {
        transform.Rotate(Vector3.up, mouse_Sensitivity_X * mouse_Turn_Value_X * Time.deltaTime);
    }

    private bool should_Turn_Left()
    {
        return Input.GetKey(KeyCode.A);
    }

    private void turn_Left()
    {
        transform.Rotate(Vector3.up, -turn_Speed * Time.deltaTime);
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
    }

    private bool should_Move_Forward()
    {
        return Input.GetKey(KeyCode.W);
    }
}
