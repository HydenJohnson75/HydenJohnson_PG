using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject main_Player_Template;
    private Camera main_Fps_Camera;
    public GameObject bullet;
    public Player player_Script;
    public GameObject gun;
    Vector3 gun_Pos;


    // Start is called before the first frame update
    void Start()
    {
       main_Fps_Camera = main_Player_Template.GetComponentInChildren<Camera>();

       player_Script = main_Player_Template.GetComponent<Player>();

    }

    // Update is called once per frame
    void Update()
    {
        gun_Pos = gun.transform.position;

        //if (player_Script.Shoot())
        //{
            //bullet = Instantiate(bullet, gun_Pos, gun.transform.rotation);
            //bullet.transform.position = gun.transform.position + gun.transform.forward;
       // }

        
    }
}
