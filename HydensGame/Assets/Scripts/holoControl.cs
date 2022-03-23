using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class holoControl : MonoBehaviour, I_Shootable
{
    enum Boss_State { Open, Closed, Opening, Closing }

    Boss_State currently = Boss_State.Closed;

    // Start is called before the first frame update

    public Transform a, b;
    private float boss_Speed = 1f;
    private int dmgTaken;
    private BossScript my_Boss;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        switch (currently)
        {
            case Boss_State.Opening:

                a.localPosition += boss_Speed * Vector3.left * Time.deltaTime;
                b.localPosition += boss_Speed * Vector3.left * Time.deltaTime;

                if (a.localPosition.x < -5.6f)
                {
                    currently = Boss_State.Open;

                }
                break;

            case Boss_State.Closing:

                a.localPosition -= boss_Speed * Vector3.left * Time.deltaTime;
                b.localPosition -= boss_Speed * Vector3.left * Time.deltaTime;

                if(a.localPosition.x > 0f)
                {
                    currently = Boss_State.Closed;
                }
                break;

        }
        
    }

    internal void open_Door()
    {
        if (currently != Boss_State.Open && currently == Boss_State.Closed)
        {
            currently = Boss_State.Opening;
        }
    }

    internal void close_Door()
    {
        if (currently != Boss_State.Closed && currently == Boss_State.Open)
        {
            currently = Boss_State.Closing;
        }
    }

    public void Ive_Been_Shot()
    {
        if (my_Boss.giveGameOn())
        {
            my_Boss.takeDmg(dmgTaken);
        }
        
    }



    internal void findDmg(int gunDmg)
    {
        dmgTaken = gunDmg;
    }

    internal void addBoss(BossScript boss)
    {
        my_Boss = boss;
    }
}
