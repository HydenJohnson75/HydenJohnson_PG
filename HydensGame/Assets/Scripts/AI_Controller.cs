using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Controller : MonoBehaviour,I_Shootable
{
    Manager my_Manager;
    enum ai_State { Alive, Dying, Dead, Attacking, Searching, Idle}
    int enemy_HP;
    ai_State current_State;
    Animator ai_Animation;

    // Start is called before the first frame update
    void Start()
    {
        current_State = ai_State.Idle;
        enemy_HP = 100;
        ai_Animation = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        print(enemy_HP);

        if(enemy_HP <= 0)
        {
            current_State = ai_State.Dead;
        }


        switch (current_State)
        {
            case ai_State.Dead:

                killAI();

                break;

        }
    }

    internal void addManager(Manager manager)
    {
        my_Manager = manager;
    }

    public void Ive_Been_Shot()
    {
        enemy_HP -= 20;
    }

    internal void killAI()
    {
        ai_Animation.SetBool("is_Idle", false);
        ai_Animation.SetBool("is_Running", false);
        ai_Animation.SetBool("is_Dead", true);
    }
}
