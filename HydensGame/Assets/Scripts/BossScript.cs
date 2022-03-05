using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    private Transform bossShieldOne;
    private Transform bossShieldTwo;
    private Transform bossShieldThree;
    private Transform projector;
    private Transform projectorTwo;
    private Transform projectorThree;
    private float move_Speed = 1f;
    enum Boss_State { Open, Closed, Opening, Closing }

    Boss_State currently = Boss_State.Closed;

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        switch (currently)
        {

        }
    }


    public void open_Door()
    {
        if (currently != Boss_State.Open)
        {
            currently = Boss_State.Opening;
        }
    }

    /*internal void close_Door()
    {
        if (currently != Boss_State.Closed )
        {
            currently = Boss_State.Closing;
        }
    }*/
}
