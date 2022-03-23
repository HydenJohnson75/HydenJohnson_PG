using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Terminal_Manager : MonoBehaviour
{

    Boss_Terminals[] terminals;
    Boss_Terminals [] selectedTerminals;
    private Manager my_Man;
    bool terminals_Set = false;


    // Start is called before the first frame update
    void Start()
    {

        terminals = gameObject.GetComponentsInChildren<Boss_Terminals>();
        
        my_Man = GameObject.Find("Manager").GetComponent<Manager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (my_Man.playerHasBuff())
        {
            if (!terminals_Set)
            {
                
                selectedTerminals = selectTerminals(terminals);
                terminals_Set = true;
            }

            if(terminals_Set && !my_Man.giveGameOn())
            {
                changeTermColor(selectedTerminals);
            }

           
        }

        if (terminals_Set)
        {
            for (int i = 0; i < selectedTerminals.Length; i++)
            {
                if (selectedTerminals[0].Shot() && selectedTerminals[1].Shot() && selectedTerminals[2].Shot() && selectedTerminals[3].Shot())
                {
                    my_Man.gameOn = true;
                    selectedTerminals[0].setIsShot(false);
                    selectedTerminals[1].setIsShot(false);
                    selectedTerminals[2].setIsShot(false);
                    selectedTerminals[3].setIsShot(false);
                    terminals_Set = false;
                    resetTermColor(selectedTerminals);
                    
                }
            }
        }

        
        
        
    }

    public int randomNumber()
    {

        int randomNumber = Random.Range(0, 9);

        return randomNumber;
        
    }


    private Boss_Terminals[] selectTerminals(Boss_Terminals[] my_BT)
    {
        Boss_Terminals[] my_Terms = new Boss_Terminals[4];

        for(int j = 0; j < my_Terms.Length; j++)
        {
            my_Terms[j] = my_BT[Random.Range(0,9)];
        }



        return my_Terms;
    }

    private void changeTermColor(Boss_Terminals[] selectedTerms)
    {
        Material panel_Mat;

        for (int i = 0; i < selectedTerminals.Length; i++)
        {
            
            panel_Mat = selectedTerminals[i].GetComponentInChildren<Renderer>().material;

            panel_Mat.EnableKeyword("_EmissionColor");

            panel_Mat.SetColor("_EmissionColor", Color.red);
            
        }
    }

    private void resetTermColor(Boss_Terminals[] selectedTerms)
    {
        Material panel_Mat;
        Color originalColor = new Color(3.43034577f, 1.99667299f, 0, 1);

        for (int i = 0; i < selectedTerminals.Length; i++)
        {

            panel_Mat = selectedTerminals[i].GetComponentInChildren<Renderer>().material;

            panel_Mat.EnableKeyword("_EmissionColor");

            panel_Mat.SetColor("_EmissionColor", originalColor);

        }
    }
}
