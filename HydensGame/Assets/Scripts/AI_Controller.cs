using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Controller : MonoBehaviour,I_Shootable
{
    Manager my_Manager;
    enum ai_State { Alive, Dying, Dead, Attacking, Searching, Idle}
    int enemy_HP;
    ai_State current_State;
    Animator ai_Animation;

    private Transform target;

    public float attack_Radius = 30f;

    private NavMeshAgent navMesh;

    public Transform[] patrolPoints;

    int currentPoint;

    float attackTimer;

    float death_Timer = 3f;



    // Start is called before the first frame update
    void Start()
    {
        current_State = ai_State.Idle;
        enemy_HP = 100;
        ai_Animation = GetComponentInParent<Animator>();
        navMesh = GetComponent<NavMeshAgent>();

        target = my_Manager.givePlayerTransform();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceTo = Vector3.Distance(transform.position, target.position);

        if(distanceTo <= attack_Radius && current_State != ai_State.Dead)
        {
            navMesh.isStopped = false;

            current_State = ai_State.Attacking;


            transform.LookAt(target);

            Vector3 goToTarget = Vector3.MoveTowards(transform.position,target.position,100f);

            navMesh.destination = goToTarget;
                
        }

        if (current_State == ai_State.Dead)
        {
            navMesh.velocity = Vector3.zero;
            navMesh.isStopped = true;
        }

        if (distanceTo > attack_Radius)
        {
            navMesh.velocity = Vector3.zero;
            navMesh.isStopped = true;
            current_State = ai_State.Alive;
        }

        

        print(enemy_HP);

        if(enemy_HP <= 0)
        {
            current_State = ai_State.Dead;
        }


        switch (current_State)
        {
            case ai_State.Dead:

                killAI();

                death_Timer -= Time.deltaTime;

                if(death_Timer <= 0)
                {
                    Destroy(gameObject);
                }

                break;

            case ai_State.Attacking:

                followPlayer();

                break;

            case ai_State.Alive:

                ai_Idle();

                break;

        }
    }

    private void ai_Idle()
    {
        ai_Animation.SetBool("is_Idle", true);
        ai_Animation.SetBool("is_Running", false);
        ai_Animation.SetBool("is_Dead", false);
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

    internal void followPlayer()
    {
        ai_Animation.SetBool("is_Idle", false);
        ai_Animation.SetBool("is_Running", true);
        ai_Animation.SetBool("is_Dead", false);
    }


}
