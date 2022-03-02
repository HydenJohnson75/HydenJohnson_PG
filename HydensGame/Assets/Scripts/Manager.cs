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

    }

    // Update is called once per frame
    void Update()
    {

        if(cMM.currentNumberCheck() == 1234)
        {
            print("you solved the puzzle");

            my_SecretDoor.open_Door();     
        }

        if (my_Player.gotBuff())
        {
            panel_Mat.SetColor("_EmissionColor", Color.red);
        }
    }

    internal bool playerHasBuff()
    {
        return my_Player.gotBuff();
    }

}
