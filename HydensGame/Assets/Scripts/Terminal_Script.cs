using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminal_Script : MonoBehaviour, I_Interactable
{
    public GameObject my_DoorGO;
    private I_Actionable my_Door;
    

    public void Interact()
    {

        print("Opening door");

        my_Door.open_Door();
        //0.217
    }

    // Start is called before the first frame update
    void Start()
    {
        my_Door = my_DoorGO.GetComponent<I_Actionable>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  
}
