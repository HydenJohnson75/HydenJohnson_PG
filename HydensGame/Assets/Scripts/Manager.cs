using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Manager : MonoBehaviour
{

    Code_Machine_Manager cMM;
    public GameObject secretDoor;
    Open_Secret_Door my_SecretDoor;
    public GameObject player;
    Player my_Player;
    Renderer panel_Render;
    Material panel_Mat;
    GameObject colorChange;
    bool gameOn;
    float timer = 20;
    public GameObject boss;
    BossScript my_Boss;

    // Start is called before the first frame update
    void Start()
    {
        cMM = FindObjectOfType<Code_Machine_Manager>();
        my_SecretDoor = secretDoor.GetComponent<Open_Secret_Door>();
        my_Player = player.GetComponent<Player>();
        colorChange = GameObject.Find("Gate Access Machine (1)");
        panel_Render = colorChange.GetComponent<Renderer>();
        panel_Mat = panel_Render.GetComponent<Renderer>().material;
        panel_Mat.EnableKeyword("_EmissionColor");
        my_Boss = boss.GetComponent<BossScript>();
        gameOn = false;
    }

    // Update is called once per frame
    void Update()
    {

        if(cMM.currentNumberCheck() == 1234)
        {
            my_SecretDoor.open_Door();
        }

        if (my_Player.gotBuff())
        {
            panel_Mat.SetColor("_EmissionColor", Color.red);
        }

        if(my_Player.playerInteract() && Input.GetKeyDown(KeyCode.E))
        {
            gameOn = true;
        }

        if (gameOn)
        {
            timer -= Time.deltaTime;
            print("Game On");
            my_Boss.open_Door();
        }

        if(timer <= 0)
        {
            print("Game Over");
            //my_Boss.close_Door();
        }
    }

    internal bool playerHasBuff()
    {
        return my_Player.gotBuff();
    }

}
