using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour, I_Shootable
{
    Manager my_Man;
    private int maxHp = 200;
    private int currentHp;
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

        currentHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        playersActiveGun = my_Man.givePlayerGun();

        print(currentHp);

        if(currentHp <= 0)
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
        currentHp -= dmg;
    }

    internal int giveHP()
    {
        return currentHp;
    }
    
    internal int giveMaxHP()
    {
        return maxHp;
    }
}
