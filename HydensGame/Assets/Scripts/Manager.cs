using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Manager : MonoBehaviour
{

    //private Camera main_Fps_Camera;
    Code_Machine_Manager cMM;
    public GameObject secretDoor;
    Open_Secret_Door my_SecretDoor;
    //public GameObject secretDoor;


    // Start is called before the first frame update
    void Start()
    {
        cMM = FindObjectOfType<Code_Machine_Manager>();
        my_SecretDoor = secretDoor.GetComponent<Open_Secret_Door>();
    }

    // Update is called once per frame
    void Update()
    {

        if(cMM.currentNumberCheck() == 1234)
        {
            print("you solved the puzzle");

            my_SecretDoor.open_Door();
        }
        
    }
}
