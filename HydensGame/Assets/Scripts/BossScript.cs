using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour, I_Shootable
{
    Manager my_Man;
    private int hp = 200;
    private Gun_Script playersActiveGun;

    public void Ive_Been_Shot()
    {

    }


    // Start is called before the first frame update
    void Start()
    {
        foreach(holoControl h in GetComponentsInChildren<holoControl>())
        {
            h.addBoss(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        playersActiveGun = my_Man.givePlayerGun();

        print(hp);

        if(hp <= 0)
        {
            print("boss dead");
        }
    }


    internal void addManager(Manager manager)
    {
        my_Man = manager;
    }

    internal bool giveGameOn()
    {
        return my_Man.giveGameOn();
    }

    internal void takeDmg(int dmg)
    {
        hp -= dmg;
    }

    internal int giveHP()
    {
        return hp;
    }
    
}
