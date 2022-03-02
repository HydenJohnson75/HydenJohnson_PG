using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootable_For_Door : MonoBehaviour, I_Shootable
{
    public GameObject door_GO;
    private I_Actionable door;
    private Manager my_Man;
    public void Ive_Been_Shot()
    {

        if (my_Man.playerHasBuff())
        {
            door.open_Door();
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        door = door_GO.GetComponent<I_Actionable>();
        my_Man = GameObject.Find("Manager").GetComponent<Manager>();
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
