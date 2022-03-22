using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    private int ammo = 10;
    private int ammo_Reserves = 200;
    Canvas my_canvas;
    Text ammo_Counter;
    GameObject my_Text;
    Text interact_Text;
    public GameObject player;  
    Player my_Player;
    Image buff;
    Text operator_Text;
    bool is_Empty = false;
    bool hasBuff;
    public AudioClip reloadingClip;
    private AudioSource reloadingSource;


    // Start is called before the first frame update
    void Start()
    {
        my_canvas = GetComponentInChildren<Canvas>();
        ammo_Counter = my_canvas.GetComponentInChildren<Text>();
        my_Text = GameObject.FindGameObjectWithTag("Interact");
        my_Player = player.GetComponent<Player>();
        interact_Text = my_Text.GetComponent<Text>();
        ammo_Counter.text = ammo + "/" + ammo_Reserves;
        buff = my_canvas.gameObject.transform.Find("Buff").GetComponentInChildren<Image>();
        operator_Text = my_canvas.gameObject.transform.Find("Operator_Text").GetComponentInChildren<Text>();

        reloadingSource = gameObject.AddComponent<AudioSource>();
        reloadingSource.playOnAwake = false;
        reloadingSource.clip = reloadingClip;
        reloadingSource.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        interact_Text.enabled = false;

        if (Input.GetMouseButtonDown(0) && is_Empty == false)
        {
            ammo--;
            my_Player.Shoot();
        }

        ammo_Counter.text = ammo.ToString();

        if(ammo <= 0)
        {
            is_Empty = true;
        }

        if (is_Empty)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                ammo = 10;
                is_Empty = false;
            }
        }

        if(hasBuff == false)
        {
            buff.enabled = false;
            operator_Text.enabled = false;
        }
        else if(hasBuff == true)
        {
            buff.enabled = true;
            operator_Text.enabled = true;
        }

        if (my_Player.gotBuff())
        {
            hasBuff = true;
        }


        if (my_Player.playerInteract())
        {
            interact_Text.enabled = true;
        }
        else if(my_Player.playerInteract() == false)
        {
            interact_Text.enabled = false;
        }
    }
}
