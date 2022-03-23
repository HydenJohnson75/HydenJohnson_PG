using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Controller : MonoBehaviour,I_Shootable
{
    Manager my_Manager;
    enum ai_State { Alive, Dying, Dead, Following, Searching, Idle, StartingPatrol, Attacking}
    internal int spawn_point_index = 0;
    int enemy_HP;
    [SerializeField]
    ai_State current_State;
    Animator ai_Animation;

    private Transform target;
    float follow_Radius = 20f;
    public float attack_Radius = 10f;
    private NavMeshAgent navMesh;
    public Transform[] patrolPoints;
    float death_Timer = 3f;
    private Transform startPoint;
    private Transform[] movePoints;
    private int randomPoint;



    // Start is called before the first frame update
    void Start()
    {
        enemy_HP = 100;
        ai_Animation = GetComponentInParent<Animator>();
        navMesh = GetComponent<NavMeshAgent>();
        target = my_Manager.givePlayerTransform();
        startPoint = my_Manager.giveStartPoint(this);
        movePoints = my_Manager.giveMovePoints(this);
        randomPoint = UnityEngine.Random.Range(0, movePoints.Length);
        current_State = ai_State.StartingPatrol;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceTo = Vector3.Distance(transform.position, target.position);

        if(distanceTo <= follow_Radius && distanceTo >= attack_Radius && current_State != ai_State.Dead)
        {
            navMesh.isStopped = false;

            current_State = ai_State.Following;


            transform.LookAt(target);

            Vector3 goToTarget = Vector3.MoveTowards(transform.position,target.position,100f);

            navMesh.destination = goToTarget;
                
        }

        if(distanceTo <= attack_Radius && current_State != ai_State.Dead)
        {
            current_State = ai_State.Attacking;
        }

        if (current_State == ai_State.Dead)
        {
            navMesh.velocity = Vector3.zero;
            navMesh.isStopped = true;
            my_Manager.Im_Dead(this);
        }

        if (distanceTo > follow_Radius && current_State != ai_State.StartingPatrol && current_State != ai_State.Attacking)
        {

            current_State = ai_State.Searching;
        }


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

            case ai_State.Following:

                followPlayer();

                break;

            case ai_State.Alive:

                ai_Idle();

                break;

            case ai_State.Searching:

                randomPatrol();

                break;

            case ai_State.StartingPatrol:

                startPatrol();

                break;

            case ai_State.Attacking:

                attackPlayer();

                break;

        }

        
    }

    private void ai_Idle()
    {
        ai_Animation.SetBool("is_Idle", true);
        ai_Animation.SetBool("is_Running", false);
        ai_Animation.SetBool("is_Dead", false);
        ai_Animation.SetBool("is_Attacking", false);
    }

    internal void addManager(Manager manager, int spawn_index)
    {
        my_Manager = manager;
        spawn_point_index = spawn_index;
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
        ai_Animation.SetBool("is_Attacking", false);
    }

    internal void followPlayer()
    {
        ai_Animation.SetBool("is_Idle", false);
        ai_Animation.SetBool("is_Running", true);
        ai_Animation.SetBool("is_Dead", false);
        ai_Animation.SetBool("is_Attacking", false);
        navMesh.isStopped = false;
    }

    internal void startPatrol()
    {
        ai_Animation.SetBool("is_Idle", false);
        ai_Animation.SetBool("is_Running", true);
        ai_Animation.SetBool("is_Dead", false);
        ai_Animation.SetBool("is_Attacking", false);

        transform.position = Vector3.MoveTowards(transform.position, startPoint.position, 3 * Time.deltaTime);

        transform.LookAt(startPoint);

        if (Vector3.Distance(transform.position, startPoint.position) < 1f)
        {
            navMesh.velocity = Vector3.zero;

            current_State = ai_State.Searching;
        }
    }

    internal void randomPatrol()
    {
        navMesh.enabled = false;
        navMesh.enabled = true;

        transform.position = Vector3.MoveTowards(transform.position, movePoints[randomPoint].position, 3 * Time.deltaTime);

        transform.LookAt(movePoints[randomPoint]);

        if (Vector3.Distance(transform.position, movePoints[randomPoint].position) <= 2f)
        {
            randomPoint = UnityEngine.Random.Range(0, movePoints.Length);
            
        }
    }

    internal void attackPlayer()
    {
        transform.LookAt(target);
        ai_Animation.SetBool("is_Idle", false);
        ai_Animation.SetBool("is_Running", false);
        ai_Animation.SetBool("is_Dead", false);
        ai_Animation.SetBool("is_Attacking", true);
        navMesh.isStopped = true;

    }

}
