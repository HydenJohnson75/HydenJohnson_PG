using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

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
    focal_Point my_Focal_Point;
    private bool is_Grounded = true;
    Vector3 last_Position;
    List<Gun_Script> my_Guns;
    Gun_Script my_Gun;
    bool hasBuff;
    bool isInteracting = false;
    private NavMeshAgent navMeshAgent;
    private Rigidbody rb;




    private IEnumerator Countdown()
    {
        float duration = 2f; // 3 seconds you can change this 
                             //to whatever you want
        float normalizedTime = 0;
        while (normalizedTime <= 1f)
        {
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }
    }

        private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

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
        my_Guns = GetComponentsInChildren<Gun_Script>().ToList();
        hasBuff = false;
        setup_guns(my_Guns);
        my_Gun = activate_gun(0);
        rb = GetComponent<Rigidbody>();
    }



    private void setup_guns(List<Gun_Script> my_guns)
    {
        my_guns[0].setup(new Vector3(0.0829f, -0.067f, 0.238f),0,0);
        my_guns[1].setup( new Vector3(0.100f, -0.064f, 0.133f),0.1f,0.2f);

    }

    private Gun_Script activate_gun(int v)
    {
        foreach (Gun_Script gun in my_Guns)
            gun.gameObject.SetActive(false);

        my_Guns[v].gameObject.SetActive(true);
        return my_Guns[v];
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
            if(rb.velocity.y < 0)
            {
                navMeshAgent.enabled = true;
            }
             
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
                if (Interact_Script != null)
                {
                    Interact_Script.Interact();
                }
                else
                {

                }

            }

        }

        if (my_Gun == my_Guns[0] && Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
        
        if(my_Gun == my_Guns[1] && Input.GetMouseButton(0))
        {
            Shoot();
        }

        if (Input.GetMouseButton(1))
        {
            ADS();
        }
        else
        {
            noADS();
        }

        /*Collider[] wall_Clips = Physics.OverlapCapsule(transform.position - Vector3.up * 0.45f, transform.position + Vector3.up * 0.45f, 0.1f);

        foreach (Collider wall in wall_Clips)
        {

            if (wall.transform.tag != "Floor")
            {
                transform.position = last_Position;
            }

        }*/


        if (jumped() && is_Grounded == true)
        {
            jump();

            
            
        }

        if (Input.GetKeyDown(KeyCode.P))
            navMeshAgent.enabled = true;

        //if (!is_Grounded)
        //{
        //    StartCoroutine(Countdown()); 
        //    navMeshAgent.enabled = true;   
        //}

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            my_Gun = activate_gun(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            my_Gun = activate_gun(1);
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
        navMeshAgent.enabled = false;
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

        if(collision.gameObject.name == "SciFiGunLightBlue")
        {
            Destroy(collision.gameObject);
            my_Gun = activate_gun(1);
        }

    }

    private void OnCollisionStay(Collision collision)
    {
        
    }

    public void Shoot()
    {
        my_Gun.Shoot();
    }

    public void ADS()
    {
        my_Gun.ADS();
    }

    public void noADS()
    {
        my_Gun.noADS();
    }

    internal bool gotBuff()
    {
        return hasBuff;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Start")
        {
            isInteracting = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.name == "Start")
        {
            isInteracting = false;
        }
    }

    internal bool playerInteract()
    {
        return isInteracting;
    }


    internal bool jumpTimer()
    {
        bool timerReady = false;
        float my_Timer = 2f;

        my_Timer -= Time.deltaTime;

        if(my_Timer <= 0)
        {
            timerReady = true;
            my_Timer = 2f;
        }

        return timerReady;
    }
}
