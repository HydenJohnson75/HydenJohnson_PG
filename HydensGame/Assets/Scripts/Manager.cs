using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Manager : MonoBehaviour
{
    bool generatingInitialMobs = true;
    holoControl[] all_holos;
    Code_Machine_Manager cMM;
    public GameObject secretDoor;
    Open_Secret_Door my_SecretDoor;
    public GameObject player;
    Player my_Player;
    Renderer panel_Render;
    Material panel_Mat;
    GameObject colorChange;
    internal bool gameOn;
    float timer = 20;
    public GameObject boss;
    BossScript my_Boss;
    List<AI_Controller> all_Enemies;
    public GameObject enemy;
    Vector3 spawn_Loc1 = new Vector3(-110.270f, 1.233f, -20.899f);
    Vector3 spawn_Loc2 = new Vector3(-66.989f, 1.224f, -20.004f);
    float waitTime;
    float startWaitTime = 6f;
    public Transform startPoint1;
    public Transform startPoint2;
    public Transform[] movePoints1;
    public Transform[] movePoints2;
    private Gun_Script playersActiveGun;
    internal UI_Manager ui_Manager;
    bool isPlayerDead;
    bool isBossDead;

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
        waitTime = startWaitTime;
        my_Boss.addManager(this);
        ui_Manager = GameObject.Find("UIManager").GetComponent<UI_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (generatingInitialMobs)
        {
            if (waitTime <= 0)
            {
                spawnAI1();
                spawnAI2();
                waitTime = startWaitTime;
                if (all_Enemies.Count >= 12)
                {
                    generatingInitialMobs = false;
                }
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }

        if(my_Player.giveCurrentHP() <= 0)
        {
            isPlayerDead = true;
        }
        else
        {
            isPlayerDead = false;
        }


        if (isPlayerDead)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if(my_Boss.giveHP() <= 0)
        {
            isBossDead = true;
        }

        else
        {
            isBossDead = false;
        }

        if(isBossDead == true)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        }

        findPlayerGun(my_Player.giveActiveGun());
        
        foreach(holoControl f in all_holos)
        {
            f.findDmg(playersActiveGun.giveGunDmg());
        }

        if (cMM.currentNumberCheck() == 1234)
        {
            my_SecretDoor.open_Door();
        }

        if (my_Player.gotBuff())
        {
            panel_Mat.SetColor("_EmissionColor", Color.red);
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

        print(playersActiveGun.gameObject);
    }

    internal bool playerHasBuff()
    {
        return my_Player.gotBuff();
    }

    internal void spawnAI1()
    {

        GameObject new_AI = Instantiate(enemy, spawn_Loc1, Quaternion.identity);

        AI_Controller new_Ai_Controller = new_AI.GetComponent<AI_Controller>();

        if (new_Ai_Controller)
        {
            new_Ai_Controller.addManager(this, 0);
            all_Enemies.Add(new_Ai_Controller);
        }

    }

    internal void spawnAI2()
    {

        GameObject new_AI = Instantiate(enemy, spawn_Loc2, Quaternion.identity);

        AI_Controller new_Ai_Controller = new_AI.GetComponent<AI_Controller>();

        if (new_Ai_Controller)
        {
            new_Ai_Controller.addManager(this, 1);
            all_Enemies.Add(new_Ai_Controller);
        }

    }

    internal Transform givePlayerTransform()
    {
        return player.transform;
    }

    internal Transform giveStartPoint(AI_Controller enemy) 
    {
        if (enemy.spawn_point_index == 0)
        {
            return startPoint1;
        }
        else
            return startPoint2;
    }

    internal Transform[] giveMovePoints(AI_Controller enemy)
    {
        if (enemy.spawn_point_index == 0)
        {
            return movePoints1;
        }
        else
            return movePoints2;
    }

    internal void Im_Dead(AI_Controller AI)
    {
        all_Enemies.Remove(AI);
        
        if(all_Enemies.Count < 12)
        {
            if (AI.spawn_point_index == 0)
            {
                spawnAI1();
            }
            else
            {
                spawnAI2();
            }
        }

    }

    private void findPlayerGun(Gun_Script activeGun)
    {
        playersActiveGun = activeGun;
    }

    internal Gun_Script givePlayerGun()
    {
        return playersActiveGun;
    }

    internal bool giveGameOn()
    {
        return gameOn;
    }

    internal void setGameOn(bool allShot)
    {
        gameOn = allShot;
    }

}
