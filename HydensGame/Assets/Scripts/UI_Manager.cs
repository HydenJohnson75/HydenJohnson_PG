using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    private int ammo = 40;
    private int ammo_Reserves = 200;
    Canvas my_canvas;
    Text ammo_Counter;
    GameObject my_Text;
    Text interact_Text;
    Player my_Player;
    bool is_Empty = false;
    bool ammo_Res_Empty = false;

    // Start is called before the first frame update
    void Start()
    {
        my_canvas = GetComponentInChildren<Canvas>();
        ammo_Counter = my_canvas.GetComponentInChildren<Text>();
        my_Text = GameObject.FindGameObjectWithTag("Interact");
        interact_Text = my_Text.GetComponent<Text>();
        my_Player = GetComponent<Player>();
        ammo_Counter.text = ammo + "/" + ammo_Reserves;
    }

    // Update is called once per frame
    void Update()
    {
        print(interact_Text.text);

        if (Input.GetMouseButtonDown(0) && is_Empty == false)
        {
            ammo--;
        }

        ammo_Counter.text = ammo + "/" + ammo_Reserves;

        if(ammo <= 0)
        {
            is_Empty = true;
        }

        if(ammo_Reserves == 0)
        {
            ammo_Res_Empty = true;
        }

        if(ammo_Res_Empty == false)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                ammo_Reserves = ammo_Reserves - (40-ammo);
                ammo = 40;
                is_Empty = false;
            }
        }
    }
}
