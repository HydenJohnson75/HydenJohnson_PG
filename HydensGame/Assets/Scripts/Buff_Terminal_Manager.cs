using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Terminal_Manager : MonoBehaviour
{

    Boss_Terminals[] terminals;
    private Manager my_Man;


    // Start is called before the first frame update
    void Start()
    {
        terminals = this.GetComponentsInChildren<Boss_Terminals>();
        my_Man = GameObject.Find("Manager").GetComponent<Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        print(terminals.Length);

        if (my_Man.playerHasBuff())
        {
            /*for(int j = 0; j < 4; j++)
            {
                for (int i = 0; i < terminals.Length; i++)
                {
                    if (i == randomNumber())
                    {
                        print(terminals[i].name);
                    }
                }
            }*/
        }
    }

    public int randomNumber()
    {

        int randomNumber = Random.Range(0, 15);

        return randomNumber;
        
    }
}
