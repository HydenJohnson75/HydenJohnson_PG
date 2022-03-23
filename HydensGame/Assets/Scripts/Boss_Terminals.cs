using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Terminals : MonoBehaviour, I_Shootable
{

    bool isShot = false;
    private Manager my_Man;

    public void Ive_Been_Shot()
    {
        isShot = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal bool Shot()
    {
        return isShot;
    }

    internal void addManager(Manager manager)
    {
        my_Man = manager;
    }

    internal void setIsShot(bool amIShot)
    {
        isShot = amIShot;
    }
}
