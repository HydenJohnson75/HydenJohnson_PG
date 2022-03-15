using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Manager : MonoBehaviour
{
    holoControl[] all_holos;
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
    List<AI_Controller> all_Enemies;
    public GameObject enemy;
    Vector3 spawn_Loc = new Vector3(-106.300f, 1.197f, -43.419f);

    // Start is called before the first frame update
    void Start()
    {
        all_holos = FindObjectsOfType<holoControl>();
        cMM = FindObjectOfType<Code_Machine_Manager>();
        my_SecretDoor = secretDoor.GetComponent<Open_Secret_Door>();
        my_Player = player.GetComponent<Player>();
        colorChange = GameObject.Find("Gate Access Machine (1)");
        panel_Render = colorChange.GetComponent<Renderer>();
        panel_Mat = panel_Render.GetComponent<Renderer>().material;
        panel_Mat.EnableKeyword("_EmissionColor");
        my_Boss = boss.GetComponent<BossScript>();
        all_Enemies = new List<AI_Controller>();
        gameOn = false;
        spawnAI();
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
            foreach (holoControl holo in all_holos)
                holo.open_Door();
        }
        if(timer <= 0)
        {
            gameOn = false;
            print("Game Over");

            timer += 20;
            foreach (holoControl holo in all_holos)
                holo.close_Door();
        }
    }

    internal bool playerHasBuff()
    {
        return my_Player.gotBuff();
    }

    internal void spawnAI()
    {
        for(int i = 0; i<5; i++)
        {
            GameObject new_AI = Instantiate(enemy, spawn_Loc, Quaternion.identity);

            AI_Controller new_Ai_Controller = new_AI.GetComponent<AI_Controller>();

            if (new_Ai_Controller)
            {
                new_Ai_Controller.addManager(this);
                all_Enemies.Add(new_Ai_Controller);
            }
        }
        
    }

    internal Transform givePlayerTransform()
    {
        return player.transform;
    }

}
