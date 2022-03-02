using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Player Script
    private int my_HP;
    internal enum player_States { moving, idle, takingDmg, healing, dead };
    internal player_States my_State = player_States.idle;
    private float current_Speed;
    private float walking_Speed = 2f;
    private float running_Speed = 6f;
    private float mouse_Sensitivity_X = 20f;
    Animator player_Animation;
    Camera player_Camera;
    GameObject main_Cam;
    FPS_Camera my_Camera;
    //SphereCollider panel_Collider;
    focal_Point my_Focal_Point;
    private bool is_Grounded = true;
    Vector3 last_Position;
    Gun_Script my_Gun;
    bool hasBuff;

    // Start is called before the first frame update
    void Start()
    {
        my_HP = 100;
        Cursor.lockState = CursorLockMode.Locked;
        current_Speed = walking_Speed;
        player_Animation = GetComponentInParent<Animator>();
        player_Camera = GetComponentInChildren<Camera>();
        main_Cam = player_Camera.gameObject;
        my_Camera = GetComponentInChildren<FPS_Camera>();
        my_Focal_Point = GetComponentInChildren<focal_Point>();
        my_Camera.you_Belong_To_Me(this);
       // panel_Collider = FindObjectOfType<Terminal_Script>().GetComponent<SphereCollider>();

        my_Gun = GetComponentInChildren<Gun_Script>();
        //panel_Mat = panel_Collider.GetComponent<Renderer>().material;
        //panel_Mat.EnableKeyword("_EmissionColor");

        hasBuff = false;
    }

    // Update is called once per frame
    void Update()
    {

        last_Position = transform.position;
        player_Animation.SetBool("walking_Forward", false);
        player_Animation.SetBool("running", false);
        player_Animation.SetBool("jumping", false);

        if (Input.GetKeyDown(KeyCode.T))
        {
            my_HP -= 10;
        }

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
            current_Speed = run(current_Speed);
        }
        else
        {
            current_Speed = walking_Speed;
        }

        


        if (transform.position.y < 1.42f)
        {
            is_Grounded = true;
        }
        else
        {
            is_Grounded = false;
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
            

            foreach (Collider c in colliders)
            {
                I_Interactable Interact_Script = c.GetComponent<I_Interactable>();
                if (Interact_Script != null )
                {
                    Interact_Script.Interact();

                }
                else
                {
                    print("event over");
                }

            }

        }

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        /*if (Input.GetMouseButton(1))
        {
            ADS();
        }*/

        Collider[] wall_Clips = Physics.OverlapCapsule(transform.position - Vector3.up * 0.45f, transform.position + Vector3.up * 0.45f, 0.1f);

        foreach(Collider wall in wall_Clips)
        {
            print(wall.transform.tag);

            if(wall.transform.tag != "Floor")
            {
                transform.position = last_Position;
            }
          
        }


        if (jumped() && is_Grounded == true)
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
        player_Animation.SetBool("jumping", false);
        return running_Speed;
        
    }

    private void adjust_Camera(float vertical_Adjustment)
    {
        //my_Camera.adjust_Vertical_Angle(vertical_Adjustment);
        my_Focal_Point.adjust_Vertical_Angle(vertical_Adjustment);
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
        return Input.GetKeyDown(KeyCode.Space);
    }

    private void jump()
    {
        GetComponent<Rigidbody>().AddForce(transform.TransformDirection(Vector3.up) * 300);
        player_Animation.SetBool("walking_Forward", false);
        player_Animation.SetBool("running", false);
        player_Animation.SetBool("jumping", true);
        is_Grounded = false;
    }

    private bool is_Crouching()
    {
        return Input.GetKey(KeyCode.C);
    }

    private void crouch()
    {
        main_Cam.transform.position = new Vector3(transform.position.x, (0.7f * Time.deltaTime) ,transform.position.z);
    }



    private void OnTriggerEnter(Collider panel_Collider)
    {
        //sprint("Event Is taking place");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Teleporter")
        {
            this.transform.position = new Vector3(-225.35f, 0, -11.01f);
        }

        if (collision.gameObject.name == "Teleporter_2")
        {
            this.transform.position = new Vector3(-90.5999985f, 1.58399999f, -85.101799f);
        }

        if(collision.gameObject.name == "Clovis_Symbol")
        {
            Destroy(collision.gameObject);
            hasBuff = true;
        }
    }

    public void Shoot()
    {
        my_Gun.Shoot();
    }

    public void ADS()
    {
        my_Gun.ADS();
    }

    internal bool gotBuff()
    {
        return hasBuff;
    }
}
